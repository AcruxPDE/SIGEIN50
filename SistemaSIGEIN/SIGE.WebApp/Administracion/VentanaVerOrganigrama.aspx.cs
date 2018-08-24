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
    public partial class VentanaVerOrganigrama : System.Web.UI.Page
    {
        private int vIdPuesto
        {
            get { return (int)ViewState["vs_vIdPuesto"]; }
            set { ViewState["vs_vIdPuesto"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["pIdPuesto"] != null)
                {
                    vIdPuesto = int.Parse(Request.Params["pIdPuesto"].ToString());
                    DescriptivoNegocio nDescriptivo = new DescriptivoNegocio();
                    E_DESCRIPTIVO vDescriptivo = nDescriptivo.ObtieneDescriptivo(vIdPuesto);
                    txtClPuesto.InnerText = vDescriptivo.CL_PUESTO + " - " + vDescriptivo.NB_PUESTO;

                    CargarDatosOrganigrama(null,true,vIdPuesto);
                }
            }
        }

        protected void CargarDatosOrganigrama(int? pIdEmpresa = null, bool? pFgMostrarEmpleados = false, int? pIdPuesto = null)
        {
            OrganigramaNegocio nOrganigrama = new OrganigramaNegocio();
            E_ORGANIGRAMA vOrganigrama = nOrganigrama.ObtieneOrganigramaPuestos(pIdPuesto, pIdEmpresa, (bool)pFgMostrarEmpleados);


            if (vOrganigrama.lstNodoDescendencia.Count == 0)
                vOrganigrama.lstNodoDescendencia.Add(new E_ORGANIGRAMA_NODO() { nbNodo = "No hay datos" });

            if (vOrganigrama.lstNodoDescendencia.Where(w => w.idNodoSuperior == null).Count() > 1)
            {
                UtilMensajes.MensajeResultadoDB(rwMensaje, "Por favor selecciona un nodo raíz de la lista de ascendencia.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            }

            else
            {
                rocPuestos.GroupEnabledBinding.NodeBindingSettings.DataFieldID = "idNodo";
                rocPuestos.GroupEnabledBinding.NodeBindingSettings.DataFieldParentID = "idNodoSuperior";
                rocPuestos.RenderedFields.NodeFields.Add(new OrgChartRenderedField() { DataField = "nbNodo" });
                rocPuestos.GroupEnabledBinding.NodeBindingSettings.DataSource = vOrganigrama.lstNodoDescendencia;

                if ((bool)pFgMostrarEmpleados)
                {
                    rocPuestos.GroupEnabledBinding.GroupItemBindingSettings.DataFieldID = "idItem";
                    rocPuestos.GroupEnabledBinding.GroupItemBindingSettings.DataFieldNodeID = "idNodo";
                    rocPuestos.GroupEnabledBinding.GroupItemBindingSettings.DataSource = vOrganigrama.lstGrupo;
                }
                rocPuestos.DataBind();
            }
        }


        protected void rocPuestos_NodeDataBound(object sender, Telerik.Web.UI.OrgChartNodeDataBoundEventArguments e)
        {
            if (((E_ORGANIGRAMA_NODO)e.Node.DataItem).clTipoNodo == "STAFF")
                e.Node.CssClass = "cssStaff";

            foreach (OrgChartGroupItem groupItem in e.Node.GroupItems)
                if (!String.IsNullOrWhiteSpace(((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem))
                    groupItem.CssClass = ((E_ORGANIGRAMA_GRUPO)groupItem.DataItem).cssItem;
        }
    }
}