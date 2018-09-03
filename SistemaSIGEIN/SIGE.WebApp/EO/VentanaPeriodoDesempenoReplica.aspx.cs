using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class VentanaPeriodoDesempenoReplica : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

        private List<E_PERIODOS_REPLICAS> lstPeriodosReplica
        {
            get { return (List<E_PERIODOS_REPLICAS>)ViewState["vs_lstPeriodosReplica"]; }
            set { ViewState["vs_lstPeriodosReplica"] = value; }
        }

        private List<E_PERIODOS_REPLICAS> lstReplicas
        {
            get { return (List<E_PERIODOS_REPLICAS>)ViewState["vs_lstReplicas"]; }
            set { ViewState["vs_lstReplicas"] = value; }
        }

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_ps_id_periodo"]; }
            set { ViewState["vs_ps_id_periodo"] = value; }
        }

        private string vClTipo
        {
            get { return (string)ViewState["vs_ps_cl_tipo"]; }
            set { ViewState["vs_ps_cl_tipo"] = value; }
        }

        private DateTime vFeInicio
        {
            get { return (DateTime)ViewState["vs_vFeInicio"]; }
            set { ViewState["vs_vFeInicio"] = value; }
        }

        private DateTime vFeFin
        {
            get { return (DateTime)ViewState["vs_vFeFin"]; }
            set { ViewState["vs_vFeFin"] = value; }
        }

        private string vClTipoAccion
        {
            get { return (string)ViewState["vs_vClTipoAccion"]; }
            set { ViewState["vs_vClTipoAccion"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (!Request.Params["PeriodoId"].ToString().Equals("null"))
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoId"].ToString());
                    var vPeriodoDesempeno = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: vIdPeriodo).FirstOrDefault();
                    txtPeriodo.InnerText = vPeriodoDesempeno.CL_PERIODO;
                    txtDescripcion.InnerText = vPeriodoDesempeno.DS_PERIODO;
                    txtInicio.InnerText = vPeriodoDesempeno.FE_INICIO.ToShortDateString().ToString();
                    txtFin.InnerText = vPeriodoDesempeno.FE_TERMINO.ToString().Substring(0,10);
                    vFeInicio = vPeriodoDesempeno.FE_INICIO;
                    vFeFin = (DateTime)vPeriodoDesempeno.FE_TERMINO;
                }
                else
                    vIdPeriodo = 0;

                lstPeriodosReplica = new List<E_PERIODOS_REPLICAS>();
                List<SPE_OBTIENE_PERIODOS_DESEMPENO_REPLICA_Result>  vlstObtenerPeriodos = nPeriodo.ObtienePeriodosReplicados(vIdPeriodo).ToList();
                if (vlstObtenerPeriodos.Count > 0)
                {
                    btnReplicarPeriodo.Enabled = false;
                    btnAceptar.Enabled = false;
                    vClTipoAccion = "A";

                    foreach (var item in vlstObtenerPeriodos)
                    {
                        lstPeriodosReplica.Add(new E_PERIODOS_REPLICAS
                        {
                            ID_PERIODO = item.ID_PERIODO,
                            ID_PERIODO_REPLICA = item.ID_PERIODO_REPLICA,
                            CL_PERIODO = item.CL_PERIODO,
                            DS_PERIODO = item.DS_PERIODO,
                            FE_INICIO = Convert.ToDateTime(item.FE_INICIO),
                            FE_TERMINO = Convert.ToDateTime(item.FE_TERMINO),
                            CL_ESTADO_PERIODO = item.CL_ESTADO_PERIODO
                    });
                    }
                }
            }
        }

        protected void btnReplicarPeriodo_Click(object sender, EventArgs e)
        {
            if (txtNumero.Text == null || txtNumero.Text == "")
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese el número de réplicas que desea crear. ", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            lstPeriodosReplica = new List<E_PERIODOS_REPLICAS>();
            var vPeriodoDesempeno = nPeriodo.ObtienePeriodosDesempeno(pIdPeriodo: vIdPeriodo).FirstOrDefault();

            if (vPeriodoDesempeno != null)
            {
                vFeInicio = vPeriodoDesempeno.FE_INICIO;
                vFeFin = (DateTime)vPeriodoDesempeno.FE_TERMINO;

                for (int i = 1; i <= int.Parse(txtNumero.Text); i++)
                {
                    lstPeriodosReplica.Add(new E_PERIODOS_REPLICAS
                        {
                            ID_PERIODO = vIdPeriodo,
                            ID_PERIODO_REPLICA = i,
                            CL_PERIODO = vPeriodoDesempeno.CL_PERIODO + "-" + i.ToString(),
                            NB_PERIODO = vPeriodoDesempeno.CL_PERIODO + "-" + i.ToString(),
                            DS_PERIODO = vPeriodoDesempeno.DS_PERIODO,
                            CL_ESTADO_PERIODO = "Abierto",
                            FE_INICIO = vFeInicio.AddDays(1).AddMonths(3),
                            FE_TERMINO = vFeFin.AddDays(1).AddMonths(3)
                        });

                    vFeInicio = vFeInicio.AddDays(1).AddMonths(3);
                    vFeFin = vFeFin.AddDays(1).AddMonths(3);
                }

                grdPeriodosReplica.Rebind();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ocurrio un error al procesar la solicitud. ", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            }

            //if(feInicio.SelectedDate==null || feFin.SelectedDate== null){
            //    UtilMensajes.MensajeResultadoDB(rwmMensaje, "La fecha de inicio y término son obligatorias", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            //    return;
            //}
            //E_RESULTADO vResultado = nPeriodo.InsertaPeriodoDesempenoReplica(vIdPeriodo, feInicio.SelectedDate, feFin.SelectedDate, vClUsuario, vNbPrograma, "");
            //string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            //grdPeriodosReplica.Rebind();
            //feInicio.SelectedDate = null;
            //feFin.SelectedDate = null;
        }

        protected void grdPeriodosReplica_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //grdPeriodosReplica.DataSource = nPeriodo.ObtienePeriodosReplicados(vIdPeriodo);
            grdPeriodosReplica.DataSource = lstPeriodosReplica;
        }

        protected void grdPeriodosReplica_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                int vIdPeriodoReplica = int.Parse(item.GetDataKeyValue("ID_PERIODO_REPLICA").ToString());
                int vIdPeriodo = int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString());
                 PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                if (vIdPeriodo > 0)
                {
                    if (vClTipoAccion == "A")
                    {
                        E_RESULTADO vResultado = nPeriodo.EliminaPeriodoDesempeno(vIdPeriodo);
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
                        grdPeriodosReplica.Rebind();
                    }
                    else
                    {
                        E_PERIODOS_REPLICAS vitem = lstPeriodosReplica.Where(w => w.ID_PERIODO_REPLICA == vIdPeriodoReplica).FirstOrDefault();
                        lstPeriodosReplica.Remove(vitem);
                        grdPeriodosReplica.Rebind();
                    }
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            lstReplicas = new List<E_PERIODOS_REPLICAS>();

          foreach(GridDataItem item in grdPeriodosReplica.MasterTableView.Items)
          {
              RadDatePicker vFechaInicio = (RadDatePicker)item.FindControl("rdpFechaInicio");
              RadDatePicker vFechaFin = (RadDatePicker)item.FindControl("rdpFechaFin");

              if (!vFechaInicio.SelectedDate.Equals("") & vFechaInicio.SelectedDate != null & !vFechaFin.SelectedDate.Equals("") & vFechaFin.SelectedDate != null)
              {
                  lstReplicas.Add( new E_PERIODOS_REPLICAS
                  {
                      ID_PERIODO_REPLICA = int.Parse(item.GetDataKeyValue("ID_PERIODO_REPLICA").ToString()),
                      CL_PERIODO=item.GetDataKeyValue("CL_PERIODO").ToString(),
                      FE_INICIO = (DateTime)vFechaInicio.SelectedDate,
                      FE_TERMINO = (DateTime)vFechaFin.SelectedDate                  
              });
              }
              else
              {
                  UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese la fecha de inicio y fin de cada período. ", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                  return;
              }
          }

          var vXelements = lstReplicas.Select(x =>
                                     new XElement("PERIODO",
                                     new XAttribute("ID_PERIODO_REPLICA", x.ID_PERIODO_REPLICA),
                                     new XAttribute("CL_PERIODO", x.CL_PERIODO),
                                     new XAttribute("FE_INICIO", x.FE_INICIO),
                                     new XAttribute("FE_TERMINO", x.FE_TERMINO)
                          ));

          XElement PERIODOS =
          new XElement("PERIODOS", vXelements
          );


          if (PERIODOS != null)
          {
              E_RESULTADO vResultado = nPeriodo.InsertaPeriodosReplica(vIdPeriodo, PERIODOS.ToString(), vClUsuario, vNbPrograma, "I");
              string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
              UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "closeWindow");
          }
          else
          {
              UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se han especificado las réplicas.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "closeWindow");
          }

        }
    }
}