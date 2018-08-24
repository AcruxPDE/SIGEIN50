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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDatosEmpresas();

                CargarDatosOrganigrama();
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

        protected void CargarDatosOrganigrama(int? pIdEmpresa = null, bool? pFgMostrarEmpleados = false, int? pIdPlaza = null)
        {
            OrganigramaNegocio nOrganigrama = new OrganigramaNegocio();
            E_ORGANIGRAMA vOrganigrama = nOrganigrama.ObtieneOrganigramaPlazas(pIdPlaza, pIdEmpresa, (bool)pFgMostrarEmpleados);

            lstAscendencia.DataTextField = "nbNodo";
            lstAscendencia.DataValueField = "idNodo";
            lstAscendencia.DataSource = vOrganigrama.lstNodoAscendencia.OrderByDescending(o => o.noNivel);
            lstAscendencia.DataBind();

            if (vOrganigrama.lstNodoDescendencia.Count == 0)
                vOrganigrama.lstNodoDescendencia.Add(new E_ORGANIGRAMA_NODO() { nbNodo = "No hay datos" });

            if (vOrganigrama.lstNodoDescendencia.Where(w => w.idNodoSuperior == null).Count() > 1)
            {
                //UtilMensajes.MensajeResultadoDB(rnMensaje, "Por favor selecciona un nodo raíz de la lista de ascendencia.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                lblMensaje.Style.Add("display", "block");
            }
            else
            {
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

        protected void rocPlazas_NodeDataBound(object sender, OrgChartNodeDataBoundEventArguments e)
        {
            if (((E_ORGANIGRAMA_NODO)e.Node.DataItem).clTipoNodo == "STAFF")
                e.Node.CssClass = "cssStaff";

            foreach (OrgChartGroupItem groupItem in e.Node.GroupItems)
                if (!String.IsNullOrWhiteSpace(((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem))
                    groupItem.CssClass = ((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem; // "cssVacante";
        }

        protected void cmbEmpresas_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CargarDatosOrganigrama(int.Parse(cmbEmpresas.SelectedValue), chkMostrarEmpleados.Checked, ObtieneIdPlaza());
        }

        protected void chkMostrarEmpleados_Click(object sender, EventArgs e)
        {
            CargarDatosOrganigrama(int.Parse(cmbEmpresas.SelectedValue), chkMostrarEmpleados.Checked, ObtieneIdPlaza());
        }

        protected void ramOrganigrama_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "seleccionPlaza")
            {
                CargarDatosOrganigrama(int.Parse(cmbEmpresas.SelectedValue), chkMostrarEmpleados.Checked, ObtieneIdPlaza());
            }
        }

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