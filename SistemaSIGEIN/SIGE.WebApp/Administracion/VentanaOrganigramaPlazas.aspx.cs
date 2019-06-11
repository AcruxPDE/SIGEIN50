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
    public partial class VentanaOrganigramaPlazas : System.Web.UI.Page
    {
        #region Variables 

        private int? vIdPlaza
        {
            get { return (int?)ViewState["vs_vIdPlaza"]; }
            set { ViewState["vs_vIdPlaza"] = value; }
        }

        private int? vIdDepartamento
        {
            get { return (int?)ViewState["vs_vIdDepartamento"]; }
            set { ViewState["vs_vIdDepartamento"] = value; }
        }

        private int? vNoNiveles
        {
            get { return (int?)ViewState["vs_vNoNiveles"]; }
            set { ViewState["vs_vNoNiveles"] = value; }
        }

        private int? vIdEmpresa
        {
            get { return (int?)ViewState["vs_vIdEmpresa"]; }
            set { ViewState["vs_vIdEmpresa"] = value; }
        }

        private string vClCampo
        {
            get { return (string)ViewState["vs_vClCampo"]; }
            set { ViewState["vs_vClCampo"] = value; }
        }

        #endregion


        #region Metodos 

        protected string ObtieneCssClass(bool pFgEmpleados, int pNoNivelDiferencia, string pClCss)
        {
            string vCssClass = String.IsNullOrWhiteSpace(pClCss) ? String.Empty : pClCss;

            if (pNoNivelDiferencia <= 10 && pNoNivelDiferencia >= 1)
                vCssClass = String.Format("cssNivel{0}{1} {2}", pNoNivelDiferencia, pFgEmpleados.ToString(), pClCss);

            //switch (pNoNivelDiferencia)
            //{
            //    case 1:
            //        vCssClass = "cssNivel1" + pFgEmpleados.ToString();
            //        break;
            //    case 2:
            //        vCssClass = "cssNivel2" + pFgEmpleados.ToString();
            //        break;
            //    case 3:
            //        vCssClass = "cssNivel3" + pFgEmpleados.ToString();
            //        break;
            //    case 4:
            //        vCssClass = "cssNivel4" + pFgEmpleados.ToString();
            //        break;
            //    case 5:
            //        vCssClass = "cssNivel5" + pFgEmpleados.ToString();
            //        break;
            //    case 6:
            //        vCssClass = "cssNivel6" + pFgEmpleados.ToString();
            //        break;
            //    case 7:
            //        vCssClass = "cssNivel7" + pFgEmpleados.ToString();
            //        break;
            //    case 8:
            //        vCssClass = "cssNivel8" + pFgEmpleados.ToString();
            //        break;
            //    case 9:
            //        vCssClass = "cssNivel9" + pFgEmpleados.ToString();
            //        break;
            //    case 10:
            //        vCssClass = "cssNivel10" + pFgEmpleados.ToString();
            //        break;
            //}

            return vCssClass;
        }

        //protected void CargarDatosEmpresas()
        //{
        //    EmpresaNegocio nEmpresa = new EmpresaNegocio();
        //    List<SPE_OBTIENE_C_EMPRESA_Result> vLstEmpresas = new List<SPE_OBTIENE_C_EMPRESA_Result>();
        //    vLstEmpresas.Add(new SPE_OBTIENE_C_EMPRESA_Result()
        //    {
        //        ID_EMPRESA = 0,
        //        NB_EMPRESA = "Todas"
        //    });

        //    vLstEmpresas.AddRange(nEmpresa.Obtener_C_EMPRESA());

        //    if (vLstEmpresas.Count == 2)
        //        vLstEmpresas.RemoveAt(0);

        //    cmbEmpresas.DataTextField = "NB_EMPRESA";
        //    cmbEmpresas.DataValueField = "ID_EMPRESA";
        //    cmbEmpresas.DataSource = vLstEmpresas;
        //    cmbEmpresas.DataBind();
        //    cmbEmpresas.SelectedIndex = 0;
        //}

        protected void CargarDatosOrganigrama(int? pIdEmpresa = null, bool? pFgMostrarEmpleados = false, int? pIdPlaza = null, int? pIdDepartamento = null, string pClCampo = null, int? pNoNiveles = null)
        {
            OrganigramaNegocio nOrganigrama = new OrganigramaNegocio();
            E_ORGANIGRAMA vOrganigrama = nOrganigrama.ObtieneOrganigramaPlazas(pIdPlaza, pIdEmpresa, (bool)pFgMostrarEmpleados, pIdDepartamento, pClCampo, pNoNiveles);

            lstAscendencia.DataTextField = "nbNodo";
            lstAscendencia.DataValueField = "idNodo";
            lstAscendencia.DataSource = vOrganigrama.lstNodoAscendencia.OrderByDescending(o => o.noNivel);
            lstAscendencia.DataBind();

            if (vOrganigrama.lstNodoDescendencia.Count == 0)
                vOrganigrama.lstNodoDescendencia.Add(new E_ORGANIGRAMA_NODO() { nbNodo = "No hay datos" });

            if (vOrganigrama.lstNodoDescendencia.Where(w => w.idNodoSuperior == null).Count() > 1)
            {
                //UtilMensajes.MensajeResultadoDB(rnMensaje, "Por favor selecciona un nodo raíz del selector de plazas.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                //lblMensaje.Style.Add("display", "block");
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Error en selección de plaza.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                lblMensaje.Style.Add("display", "block");
            }
            else
            {
                int vDiferenciaNievels = vOrganigrama.lstNodoDescendencia.Where(w => w.idNodoSuperior == null).FirstOrDefault().noNivelPuesto - vOrganigrama.lstNodoDescendencia.Where(w => w.idNodoSuperior == null).FirstOrDefault().noNivel;
                if (vDiferenciaNievels > 0)
                    foreach (var item in vOrganigrama.lstNodoDescendencia) item.noNivelPuesto = item.noNivelPuesto - vDiferenciaNievels;


                rocPlazas.GroupEnabledBinding.NodeBindingSettings.DataFieldID = "idNodo";
                rocPlazas.GroupEnabledBinding.NodeBindingSettings.DataFieldParentID = "idNodoSuperior";
                rocPlazas.RenderedFields.NodeFields.Add(new OrgChartRenderedField() { DataField = "nbNodo" });
                rocPlazas.GroupEnabledBinding.NodeBindingSettings.DataSource = vOrganigrama.lstNodoDescendencia;

                if ((bool)pFgMostrarEmpleados)
                {
                    rocPlazas.GroupEnabledBinding.GroupItemBindingSettings.DataFieldID = "idItem";
                    rocPlazas.GroupEnabledBinding.GroupItemBindingSettings.DataFieldNodeID = "idNodo";
                    rocPlazas.GroupEnabledBinding.GroupItemBindingSettings.DataSource = vOrganigrama.lstGrupo;
                }
                rocPlazas.DataBind();
                lblMensaje.Style.Add("display", "none");
            }
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var relativePath = "~/api/export/file";

                RadClientExportManager1.PdfSettings.ProxyURL = ResolveUrl(relativePath);
                RadClientExportManager1.PdfSettings.Author = "Telerik ASP.NET AJAX";

                if (Request.Params["idPlaza"] != null)
                {
                    vIdPlaza = int.Parse(Request.Params["idPlaza"].ToString());

                    if (Request.Params["IdEmpresa"] != null)
                    {
                        vIdEmpresa = int.Parse(Request.Params["IdEmpresa"].ToString());
                    }

                    if (Request.Params["IdDepartamento"] != null)
                    {
                        vIdDepartamento = int.Parse(Request.Params["IdDepartamento"].ToString());
                    }

                    if (Request.Params["IdCampo"] != null)
                    {
                        vClCampo = Request.Params["IdCampo"].ToString();
                    }

                    if (Request.Params["NoNiveles"] != null)
                    {
                        vNoNiveles = int.Parse(Request.Params["NoNiveles"].ToString());
                    }

                }
                // CargarDatosEmpresas();
                CargarDatosOrganigrama(vIdEmpresa, false, vIdPlaza, vIdDepartamento, vClCampo, vNoNiveles);
            }
        }

        protected void rocPlazas_NodeDataBound(object sender, OrgChartNodeDataBoundEventArguments e)
        {
            string vClCss = String.Empty;

            if (((E_ORGANIGRAMA_NODO)e.Node.DataItem).clTipoNodo == "STAFF")
                vClCss = "cssStaff";

            foreach (OrgChartGroupItem groupItem in e.Node.GroupItems)
                if (!String.IsNullOrWhiteSpace(((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem))
                    groupItem.CssClass = ((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem; // "cssVacante";


            //CssNivelDentroDeOrganigrama
            if (((E_ORGANIGRAMA_NODO)e.Node.DataItem).noNivelPuesto > ((E_ORGANIGRAMA_NODO)e.Node.DataItem).noNivel)
            {
                int vNivelDiferencia = ((E_ORGANIGRAMA_NODO)e.Node.DataItem).noNivelPuesto - ((E_ORGANIGRAMA_NODO)e.Node.DataItem).noNivel;

                vClCss = ObtieneCssClass(chkMostrarEmpleados.Checked ?? false, vNivelDiferencia, vClCss);
            }

            e.Node.CssClass = vClCss;
        }

        //protected void cmbEmpresas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    CargarDatosOrganigrama(vIdEmpresa, chkMostrarEmpleados.Checked, vIdPlaza, vIdDepartamento, vClCampo, vNoNiveles);
        //}

        protected void chkMostrarEmpleados_Click(object sender, EventArgs e)
        {
            CargarDatosOrganigrama(vIdEmpresa, chkMostrarEmpleados.Checked, vIdPlaza, vIdDepartamento, vClCampo, vNoNiveles);
        }

        //protected void ramOrganigrama_AjaxRequest(object sender, AjaxRequestEventArgs e)
        //{
        //    if (e.Argument == "seleccionPlaza")
        //    {
        //        CargarDatosOrganigrama(0, chkMostrarEmpleados.Checked, 1);
        //    }
        //}

        //protected int? ObtieneIdPlaza()
        //{
        //    int vIdPlaza = 0;
        //    if (int.TryParse(lstPlaza.SelectedValue, out vIdPlaza))
        //        return vIdPlaza;
        //    else
        //        return null;
        //}
    }
}