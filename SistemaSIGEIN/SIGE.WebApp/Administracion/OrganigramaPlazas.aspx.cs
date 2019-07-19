using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class OrganigramaPlazas : System.Web.UI.Page
    {
        protected string ObtieneCssClass(bool pFgEmpleados, int vNivelDiferencia)
        {
            string vCssClass = "";
            switch(vNivelDiferencia)
            {
                case 1 :
                    vCssClass = "cssNivel1" + pFgEmpleados.ToString();
                break;
                case 2:
                   vCssClass = "cssNivel2" + pFgEmpleados.ToString();
                break;
                case 3:
                   vCssClass = "cssNivel3" + pFgEmpleados.ToString();
                break;
                case 4:
                   vCssClass = "cssNivel4" + pFgEmpleados.ToString();
                break;
                case 5:
                   vCssClass = "cssNivel5" + pFgEmpleados.ToString();
                break;
                case 6:
                   vCssClass = "cssNivel6" + pFgEmpleados.ToString();
                break;
                case 7:
                   vCssClass = "cssNivel7" + pFgEmpleados.ToString();
                break;
                case 8:
                   vCssClass = "cssNivel8" + pFgEmpleados.ToString();
                break;
                case 9:
                   vCssClass = "cssNivel9" + pFgEmpleados.ToString();
                break;
                case 10:
                   vCssClass = "cssNivel10" + pFgEmpleados.ToString();
                break;              
            }

            return vCssClass;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               // var relativePath = "~/api/export/file";

                //RadClientExportManager1.PdfSettings.ProxyURL = ResolveUrl(relativePath);
                //RadClientExportManager1.PdfSettings.Author = "Telerik ASP.NET AJAX";


                CargarDatosEmpresas();
               // CargarDatosOrganigrama();
            }
        }

        protected void CargarDatosEmpresas()
        {
            EmpresaNegocio nEmpresa = new EmpresaNegocio();
            List<SPE_OBTIENE_C_EMPRESA_Result> vLstEmpresas = new List<SPE_OBTIENE_C_EMPRESA_Result>();
            vLstEmpresas.Add(new SPE_OBTIENE_C_EMPRESA_Result()
            {
                ID_EMPRESA = 0,
                NB_EMPRESA = "Todas"
            });

            vLstEmpresas.AddRange(nEmpresa.Obtener_C_EMPRESA());

            if (vLstEmpresas.Count == 2)
                vLstEmpresas.RemoveAt(0);

            cmbEmpresas.DataTextField = "NB_EMPRESA";
            cmbEmpresas.DataValueField = "ID_EMPRESA";
            cmbEmpresas.DataSource = vLstEmpresas;
            cmbEmpresas.DataBind();
            cmbEmpresas.SelectedIndex = 0;
        }

        //protected void CargarDatosOrganigrama(int? pIdEmpresa = null, bool? pFgMostrarEmpleados = false, int? pIdPlaza = null)
        //{
        //    OrganigramaNegocio nOrganigrama = new OrganigramaNegocio();
        //    E_ORGANIGRAMA vOrganigrama = nOrganigrama.ObtieneOrganigramaPlazas(pIdPlaza, pIdEmpresa, (bool)pFgMostrarEmpleados);

        //    lstAscendencia.DataTextField = "nbNodo";
        //    lstAscendencia.DataValueField = "idNodo";
        //    lstAscendencia.DataSource = vOrganigrama.lstNodoAscendencia.OrderByDescending(o => o.noNivel);
        //    lstAscendencia.DataBind();

        //    if (vOrganigrama.lstNodoDescendencia.Count == 0)
        //        vOrganigrama.lstNodoDescendencia.Add(new E_ORGANIGRAMA_NODO() { nbNodo = "No hay datos" });

        //    if (vOrganigrama.lstNodoDescendencia.Where(w => w.idNodoSuperior == null).Count() > 1)
        //    {
        //        UtilMensajes.MensajeResultadoDB(rnMensaje, "Por favor selecciona un nodo raíz del selector de plazas.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
        //        lblMensaje.Style.Add("display", "block");
        //    }
        //    else
        //    {
        //        int vDiferenciaNievels = vOrganigrama.lstNodoDescendencia.Where(w => w.idNodoSuperior == null).FirstOrDefault().noNivelPuesto - vOrganigrama.lstNodoDescendencia.Where(w => w.idNodoSuperior == null).FirstOrDefault().noNivel;
        //        if (vDiferenciaNievels > 0)
        //            foreach (var item in vOrganigrama.lstNodoDescendencia) item.noNivelPuesto = item.noNivelPuesto - vDiferenciaNievels;


        //        rocPlazas.GroupEnabledBinding.NodeBindingSettings.DataFieldID = "idNodo";
        //        rocPlazas.GroupEnabledBinding.NodeBindingSettings.DataFieldParentID = "idNodoSuperior";
        //        rocPlazas.RenderedFields.NodeFields.Add(new OrgChartRenderedField() { DataField = "nbNodo" });
        //        rocPlazas.GroupEnabledBinding.NodeBindingSettings.DataSource = vOrganigrama.lstNodoDescendencia;

        //        if ((bool)pFgMostrarEmpleados)
        //        {
        //            rocPlazas.GroupEnabledBinding.GroupItemBindingSettings.DataFieldID = "idItem";
        //            rocPlazas.GroupEnabledBinding.GroupItemBindingSettings.DataFieldNodeID = "idNodo";
        //            rocPlazas.GroupEnabledBinding.GroupItemBindingSettings.DataSource = vOrganigrama.lstGrupo;
        //        }
        //        rocPlazas.DataBind();
        //        lblMensaje.Style.Add("display", "none");
        //    }
        //}

        //protected void rocPlazas_NodeDataBound(object sender, OrgChartNodeDataBoundEventArguments e)
        //{
        //    if (((E_ORGANIGRAMA_NODO)e.Node.DataItem).clTipoNodo == "STAFF")
        //        e.Node.CssClass = "cssStaff";

        //    foreach (OrgChartGroupItem groupItem in e.Node.GroupItems)
        //        if (!String.IsNullOrWhiteSpace(((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem))
        //            groupItem.CssClass = ((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem; // "cssVacante";


        //    //CssNivelDentroDeOrganigrama
        //    if (((E_ORGANIGRAMA_NODO)e.Node.DataItem).noNivelPuesto > ((E_ORGANIGRAMA_NODO)e.Node.DataItem).noNivel)
        //    {
        //        int vNivelDiferencia = ((E_ORGANIGRAMA_NODO)e.Node.DataItem).noNivelPuesto - ((E_ORGANIGRAMA_NODO)e.Node.DataItem).noNivel;

        //        e.Node.CssClass = ObtieneCssClass((bool)chkMostrarEmpleados.Checked, vNivelDiferencia);
        //        //if ((bool)chkMostrarEmpleados.Checked)
        //        //{
        //        //    if (vNivelDiferencia == 2)
        //        //        e.Node.CssClass = "cssNivel3Emp";
        //        //    if (vNivelDiferencia == 1)
        //        //        e.Node.CssClass = "cssNivel2Emp";
        //        //}
        //        //else
        //        //{
        //        //    if (vNivelDiferencia == 2)
        //        //        e.Node.CssClass = "cssNivel3";
        //        //    if (vNivelDiferencia == 1)
        //        //        e.Node.CssClass = "cssNivel2";
        //        //}
        //    }
        //}

        //protected void cmbEmpresas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    CargarDatosOrganigrama(int.Parse(cmbEmpresas.SelectedValue), chkMostrarEmpleados.Checked, ObtieneIdPlaza());
        //}

        //protected void chkMostrarEmpleados_Click(object sender, EventArgs e)
        //{
        //    CargarDatosOrganigrama(int.Parse(cmbEmpresas.SelectedValue), chkMostrarEmpleados.Checked, ObtieneIdPlaza());
        //}

        //protected void ramOrganigrama_AjaxRequest(object sender, AjaxRequestEventArgs e)
        //{
        //    if (e.Argument == "seleccionPlaza")
        //    {
        //        CargarDatosOrganigrama(int.Parse(cmbEmpresas.SelectedValue), chkMostrarEmpleados.Checked, ObtieneIdPlaza());
        //    }
        //}

        protected void cmbPlazaPuesto_SelectedIndexChanged (object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if(cmbPlazaPuesto.SelectedValue == "plaza")
            {
                lstPlaza.Visible = true;
                btnBuscarPuesto.Visible = true;
                btnLimpiarPuesto.Visible = true;
                lblEmpresa.Visible = true;
                cmbEmpresas.Visible = true;
                lstPuesto.Visible = false;
                btnBuscarSeleccionPuesto.Visible = false;
                btnLimpiarSeleccionPuesto.Visible = false;
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "Puesto", "<script> CleanPuestoSelection(); </script>");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ChangePuestoItem('', 'Seleccione');", true);
                //ClientScript.RegisterStartupScript(GetType(), "Puesto", "CleanPuestoSelection();", true);
                //ScriptManager.RegisterStartupScript(this.lstPlaza, GetType(), "Puesto", "ChangePuestoItem('', 'Seleccione')", true);
                lstPuesto.DataValueField = "";
                lstPuesto.DataTextField = "Seleccione";
                lstPuesto.Items.Clear();
                lstPuesto.Items.Add(new RadListBoxItem("Seleccione", ""));
                
            }
            else if(cmbPlazaPuesto.SelectedValue == "puesto")
            {
                lstPuesto.Visible = true;
                btnBuscarSeleccionPuesto.Visible = true;
                btnLimpiarSeleccionPuesto.Visible = true;
                lblEmpresa.Visible = true;
                cmbEmpresas.Visible = true;
                lstPlaza.Visible = false;
                btnBuscarPuesto.Visible = false;
                btnLimpiarPuesto.Visible = false;
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "Plaza", "<script> CleanPlazasSelection(); </script>");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "ChangePlazaItem('', 'Seleccione');", true);
                //ClientScript.RegisterStartupScript(GetType(),"Plaza", "CleanPlazasSelection();", true);
                //ScriptManager.RegisterStartupScript(this.lstPlaza, GetType(), "Plaza", "ChangePlazaItem('', 'Seleccione')", true);
                lstPlaza.DataValueField = "";
                lstPlaza.DataTextField = "Seleccione";
                lstPlaza.Items.Clear();
                lstPlaza.Items.Add(new RadListBoxItem("Seleccione", ""));
            }
            else
            {
                lstPlaza.Visible = false;
                btnBuscarPuesto.Visible = false;
                btnLimpiarPuesto.Visible = false;
                lblEmpresa.Visible = false;
                cmbEmpresas.Visible = false;
                lstPuesto.Visible = false;
                btnBuscarSeleccionPuesto.Visible = false;
                btnLimpiarSeleccionPuesto.Visible = false;
            }
        }

        //protected void btnGenerar(object sender, EventArgs e)
        //{
        //    System.Diagnostics.Debug.WriteLine("Llegue");
        //}

        protected int? ObtieneIdPlaza()
        {
            int vIdPlaza = 0;
            if (int.TryParse(lstPlaza.SelectedValue, out vIdPlaza))
                return vIdPlaza;
            else
                return null;
        }
    }
}