using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class VentanaConfiguraFechaEnvioReplicas : System.Web.UI.Page
    {
        #region Variables
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement FECHASENVIOSOLICITUDES { get; set; }
        private string vClUsuario;
        private string vNbPrograma;

        public int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_IdPeriodo"]; }
            set { ViewState["vs_IdPeriodo"] = value; }
        }

        public List<E_FECHA_ENVIO_SOLICITUDES> lFechasEnvio
        {
            get { return (List<E_FECHA_ENVIO_SOLICITUDES>)ViewState["vs_lFechasEnvio"]; }
            set { ViewState["vs_lFechasEnvio"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            if (!IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoId"].ToString());
                }
            }
        }

        protected void rgFechas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio pNegocio = new PeriodoDesempenoNegocio();
            List<SPE_OBTIENE_PERIODO_REPLICAS_Result> lPerReplica = new List<SPE_OBTIENE_PERIODO_REPLICAS_Result>();
            lPerReplica = (pNegocio.ObtenerPeriodos(vIdPeriodo));
            rgFechas.DataSource = lPerReplica;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            lFechasEnvio = new List<E_FECHA_ENVIO_SOLICITUDES>();
            string vIdFechaActual="";

            foreach (GridDataItem item in rgFechas.MasterTableView.Items)
            {
                RadDatePicker rdtFechaEnvio = (RadDatePicker)item["rdtFechaEnvio"].FindControl("rdtFeEnvio");
                string vPeriodo = (item.GetDataKeyValue("ID_PERIODO").ToString());
                string vFecha = rdtFechaEnvio.SelectedDate == null? "" : rdtFechaEnvio.SelectedDate.Value.ToString("MM/dd/yyyy");
                if (vFecha != "")
                {
                    if (vFecha == DateTime.Now.ToString("MM/dd/yyyy"))
                    {
                        vIdFechaActual = vPeriodo;
                    }
                    lFechasEnvio.Add(new E_FECHA_ENVIO_SOLICITUDES
                        {
                            ID_PERIODO = vPeriodo,
                            FE_ENVIO_SOLICITUD = vFecha
                        });

                    var vXelements = lFechasEnvio.Select(x =>
                                           new XElement("FECHAS_ENVIO",
                                           new XAttribute("ID_PERIODO", x.ID_PERIODO),
                                           new XAttribute("FE_ENVIO", x.FE_ENVIO_SOLICITUD))
                                );
                    FECHASENVIOSOLICITUDES =
                    new XElement("ASIGNACION", vXelements
                    );
                }
              }
            if (FECHASENVIOSOLICITUDES != null)
            {
                PeriodoDesempenoNegocio pNegocio = new PeriodoDesempenoNegocio();
                E_RESULTADO vResultado = pNegocio.InsertaFeEnvioSolicitud(FECHASENVIOSOLICITUDES.ToString(), vClUsuario, vNbPrograma, "I");
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                {
                    if (vIdFechaActual != "")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "script", "OpenSendMessage(" + vIdFechaActual + ");", true);
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                }
            }
            else
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Asigne las fechas de envío de solicitudes.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
        }
    }
}