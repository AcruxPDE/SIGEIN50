using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
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
    public partial class OrganigramaArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarDatosOrganigrama();
            }
        }

        protected void CargarDatosOrganigrama(bool? pFgMostrarEmpleados = false, int? pIdPlaza = null)
        {
            OrganigramaNegocio nOrganigrama = new OrganigramaNegocio();
            E_ORGANIGRAMA vOrganigrama = nOrganigrama.ObtieneOrganigramaAreas(pIdPlaza, (bool)pFgMostrarEmpleados);

            lstAscendencia.DataTextField = "nbNodo";
            lstAscendencia.DataValueField = "idNodo";
            lstAscendencia.DataSource = vOrganigrama.lstNodoAscendencia.OrderByDescending(o => o.noNivel);
            lstAscendencia.DataBind();

            if (vOrganigrama.lstNodoDescendencia.Count == 0)
                vOrganigrama.lstNodoDescendencia.Add(new E_ORGANIGRAMA_NODO() { nbNodo = "No hay datos" });

            if (vOrganigrama.lstNodoDescendencia.Where(w => w.idNodoSuperior == null).Count() > 1)
            {
                lblMensaje.Style.Add("display","block");
                //UtilMensajes.MensajeResultadoDB(rnMensaje, "Por favor selecciona un nodo raíz de la lista de ascendencia.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            }
            else
            {
                rocAreas.GroupEnabledBinding.NodeBindingSettings.DataFieldID = "idNodo";
                rocAreas.GroupEnabledBinding.NodeBindingSettings.DataFieldParentID = "idNodoSuperior";
                rocAreas.RenderedFields.NodeFields.Add(new OrgChartRenderedField() { DataField = "nbNodo" });
                rocAreas.GroupEnabledBinding.NodeBindingSettings.DataSource = vOrganigrama.lstNodoDescendencia;

                if ((bool)pFgMostrarEmpleados)
                {
                    rocAreas.GroupEnabledBinding.GroupItemBindingSettings.DataFieldID = "idItem";
                    rocAreas.GroupEnabledBinding.GroupItemBindingSettings.DataFieldNodeID = "idNodo";
                    rocAreas.GroupEnabledBinding.GroupItemBindingSettings.DataSource = vOrganigrama.lstGrupo;
                }
                rocAreas.DataBind();
                //lblMensaje.Visible = false;
                lblMensaje.Style.Add("display", "none");
            }
        }

        protected void chkMostrarPuestos_Click(object sender, EventArgs e)
        {
            CargarDatosOrganigrama(chkMostrarPuestos.Checked, ObtieneIdArea());
        }

        protected void rocAreas_NodeDataBound(object sender, OrgChartNodeDataBoundEventArguments e)
        {
            foreach (OrgChartGroupItem groupItem in e.Node.GroupItems)
                if (!String.IsNullOrWhiteSpace(((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem))
                    groupItem.CssClass = ((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem; // "cssVacante";
        }

        protected void ramOrganigrama_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "seleccionArea")
            {
                CargarDatosOrganigrama(chkMostrarPuestos.Checked, ObtieneIdArea());
            }
        }

        protected int? ObtieneIdArea()
        {
            int vIdArea = 0;
            if (int.TryParse(lstArea.SelectedValue, out vIdArea))
                return vIdArea;
            else
                return null;
        }

    }
}