using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class DescriptivoPuestosFactores : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement SELECCIONPUESTOS { get; set; }

        private int vID_PUESTO
        {
            get { return (int)ViewState["vsID_PUESTO"]; }
            set { ViewState["vsID_PUESTO"] = value; }
        }

        public Guid vIdConsultaGlobal
        {
            get { return (Guid)ViewState["vs_vIdConsultaGlobal"]; }
            set { ViewState["vs_vIdConsultaGlobal"] = value; }
        }

        private List<E_PUESTOS_CONSULTA> vLstPuestos
        {
            get { return (List<E_PUESTOS_CONSULTA>)ViewState["vs_vLstPuestos"]; }
            set { ViewState["vs_vLstPuestos"] = value; }
        }

        private List<SPE_OBTIENE_M_PUESTO_Result> Puestos;

        protected void Page_Load(object sender, EventArgs e)
        {
            Puestos = new List<SPE_OBTIENE_M_PUESTO_Result>();

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!IsPostBack)
            {
                vIdConsultaGlobal = new Guid();
                vLstPuestos = new List<E_PUESTOS_CONSULTA>();

                if (ContextoConsultaGlobal.oPuestosConfiguracion == null)
                {
                    ContextoConsultaGlobal.oPuestosConfiguracion = new List<E_PUESTOS_CONSULTA_GLOBAL>();
                }

                ContextoConsultaGlobal.oPuestosConfiguracion.Add(new E_PUESTOS_CONSULTA_GLOBAL { vIdParametroConfiguracionConsulta = vIdConsultaGlobal });
            }

            PuestoNegocio negocio = new PuestoNegocio();
            Puestos = negocio.ObtienePuestos();
        }

        protected void grdDescriptivo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDescriptivo.DataSource = Puestos;

        }

        protected void btnEliminar_click(object sender, EventArgs e)
        {
            PuestoNegocio negocio = new PuestoNegocio();

            foreach (GridDataItem item in grdDescriptivo.SelectedItems)
            {
            
                vID_PUESTO = (int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString()));

                E_RESULTADO vResultado = negocio.EliminaPuesto(pIdPuesto: vID_PUESTO, pNbPrograma: vNbPrograma, pClUsuario: vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, null);

            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IDP/VentanaDescriptivoPuesto.aspx");
        }

        protected void btnFactores_Click(object sender, EventArgs e)
        {
            ContextoConsultaGlobal.oPuestosConfiguracion.Where(w => w.vIdParametroConfiguracionConsulta == vIdConsultaGlobal).FirstOrDefault().vListaPuestos.Clear();

            if (grdDescriptivo.SelectedItems.Count > 0)
            {

                foreach (GridDataItem item in grdDescriptivo.SelectedItems)
                {
                    int vIdPuesto = int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString());
                    string vClPuesto = item.GetDataKeyValue("CL_PUESTO").ToString();
                    string vNbPuesto = item.GetDataKeyValue("NB_PUESTO").ToString();

                    //if (vLstPuestos.Where(w => w.ID_PUESTO == vIdPuesto).Count() == 0)
                    //{
                    //    vLstPuestos.Add(new E_PUESTOS_CONSULTA { ID_PUESTO = vIdPuesto });
                        ContextoConsultaGlobal.oPuestosConfiguracion.Where(w => w.vIdParametroConfiguracionConsulta == vIdConsultaGlobal).FirstOrDefault().vListaPuestos.Add(new E_PUESTOS_CONSULTA { ID_PUESTO = vIdPuesto, CL_PUESTO = vClPuesto, NB_PUESTO = vNbPuesto });
                  //  }

                }

                E_PUESTOS_CONSULTA_GLOBAL vLstContextoConsulta = ContextoConsultaGlobal.oPuestosConfiguracion.Where(w => w.vIdParametroConfiguracionConsulta == vIdConsultaGlobal).FirstOrDefault();
                vLstPuestos = vLstContextoConsulta.vListaPuestos;
                int vIdPuestoComparar = vLstPuestos.FirstOrDefault().ID_PUESTO;

                var vXelements = vLstPuestos.Select(x =>
                                                  new XElement("PUESTOS",
                                                      new XAttribute("ID_PUESTO", x.ID_PUESTO)
                                                      ));

                SELECCIONPUESTOS = new XElement("SELECCION", vXelements
              );

                PuestoNegocio negocio = new PuestoNegocio();
                bool? vFgCompatibles = negocio.ValidarConfiguracionPuestos(SELECCIONPUESTOS.ToString(), vIdPuestoComparar).FirstOrDefault().FG_CONGURADO;

                if (vFgCompatibles == true)
                {
                    ClientScript.RegisterStartupScript(GetType(), "script", "ShowFactoresForm();", true);
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "La configuración de los puestos seleccionados es diferente.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);

                }

            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Defina la lista de puestos a procesar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
            }
        }

        protected void grdDescriptivo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdDescriptivo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdDescriptivo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdDescriptivo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdDescriptivo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdDescriptivo.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}