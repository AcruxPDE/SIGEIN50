using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaEnvioSolicitud : System.Web.UI.Page
    {
        private List<E_CORREO_ELECTRONICO> vCorreosNombres
        {
            get { return (List<E_CORREO_ELECTRONICO>)ViewState["vsCorreosNombres"]; }
            set { ViewState["vsCorreosNombres"] = value; }
        }

        #region Varibles

        string vClTipoPlantilla
        {
            get { return (string)ViewState["vs_vClTipoPlantilla"]; }
            set { ViewState["vs_vClTipoPlantilla"] = value; }
        }

        private int? vIdPlantilla
        {
            get { return (int?)ViewState["vs_vIdPlantilla"]; }
            set { ViewState["vs_vIdPlantilla"] = value; }
        }

        E_PLANTILLA vPlantilla
        {
            get { return (E_PLANTILLA)ViewState["vs_vPlantilla"]; }
            set { ViewState["vs_vPlantilla"] = value; }
        }

        StringBuilder builder = new StringBuilder();

        #endregion

        private string EnvioCorreo(string pEmail, string pMensaje, string pAsunto)
        {
            Mail mail = new Mail(ContextoApp.mailConfiguration);
            mail.addToAddress(pEmail, "");
            RadProgressContext progress = RadProgressContext.Current;
            string vResultado = mail.SendCopia(pAsunto, String.Format("{0}", pMensaje));

           return vResultado;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["plantillaId"] != null)
                {
                    vIdPlantilla = int.Parse(Request.Params["plantillaId"]);
                    vClTipoPlantilla = (string)Request.QueryString["PlantillaTipoCl"];
                    txbPrivacidad.Content = ContextoApp.IDP.MensajeCorreoSolicitud.dsMensaje;
                    PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
                    vPlantilla = nPlantilla.ObtienePlantilla(vIdPlantilla, vClTipoPlantilla);
                }

                vCorreosNombres = new List<E_CORREO_ELECTRONICO>();
            }
        }

        protected void grdCorreos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdCorreos.DataSource = vCorreosNombres;
        }

        protected void btnAgregarCorreo_Click(object sender, EventArgs e)
        {
            if (txbCorreo.Text != "")
            {
                vCorreosNombres.Add(new E_CORREO_ELECTRONICO { NB_MAIL = txbCorreo.Text });
                grdCorreos.DataSource = vCorreosNombres;
                grdCorreos.Rebind();
                txbCorreo.Text = "";
            }
        }

        protected void grdCorreos_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                string vItCorreo = item.GetDataKeyValue("NB_MAIL").ToString();
                vCorreosNombres.RemoveAll(w => w.NB_MAIL == vItCorreo);
                grdCorreos.DataSource = vCorreosNombres;
                grdCorreos.Rebind();
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txbPrivacidad.Content != "")
            {
                if (vCorreosNombres.Count > 0)
                {
                    foreach(var item in vCorreosNombres)
                    {
                        builder.Append(item.NB_MAIL + ";");
                    }

                    var myUrl = ResolveUrl("~/Logon.aspx?FlProceso=");
                    string vUrl = ContextoUsuario.nbHost + myUrl + vPlantilla.FL_PLANTILLA.ToString() + "&ClProceso=ENVIOSOLICITUDPLANTILLA";
                    var vMsgUrl = String.Format("Acceso: <a href={0}>aquí</a><br/><br/>", vUrl);
                    var vMensaje = String.Format("Estimado(a) candidato:<br/><br/>{0}<br/><br/>{1}<br/><br/>Saludos cordiales.", txbPrivacidad.Content,vMsgUrl);
                    string vRespuesta = EnvioCorreo(builder.ToString(), vMensaje, "Solicitud empleo");
                    if (vRespuesta == "0")
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, "Proceso exitoso.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "CloseWindow");
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vRespuesta, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    }
                }
                else
                {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Ingrese los correos de destino.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Ingrese el mensaje que recibirán los candidatos.", E_TIPO_RESPUESTA_DB.WARNING , pCallBackFunction: null);
            }
        }
    }
}