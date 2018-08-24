using SIGE.Entidades;
using SIGE.Negocio.AdministracionSitio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionAdscripcion : System.Web.UI.Page
    {

        public string vClCatalogo
        {
            get { return (string)ViewState["vs_vClCatalogo"]; }
            set { ViewState["vs_vClCatalogo"] = value; }
        }

        public string vClTipoSeleccion
        {
            get { return (string)ViewState["vs_vClTipoSeleccion"]; }
            set { ViewState["vs_vClTipoSeleccion"] = value; }
        }

        public int vIdAdscripcion
        {
            get { return (int)ViewState["vs_vIdAdscripcion"]; }
            set { ViewState["vs_vIdAdscripcion"] = value; }
        }

        public XElement vXmlTipoSeleccion
        {
            get { return XElement.Parse((string)(ViewState["vs_vXmlTipoSeleccion"] ?? new XElement("SELECCION").ToString())); }
            set { ViewState["vs_vXmlTipoSeleccion"] = value.ToString(); }
        }
        public string vClTipoAdscripcion
        {
            get { return (string)ViewState["vs_vClTipoAdscripcion"]; }
            set { ViewState["vs_vClTipoAdscripcion"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClCatalogo = Request.QueryString["CatalogoCl"];
                if (String.IsNullOrEmpty(vClCatalogo))
                    vClCatalogo = "ADSCRIPCION";


                vClTipoAdscripcion = Request.QueryString["vClTipoAdscripcion"];
                if (String.IsNullOrEmpty(vClTipoAdscripcion))
                    vClTipoAdscripcion = "TODOS";

                vClTipoSeleccion = Request.QueryString["vClTipoSeleccion"];
                if (string.IsNullOrEmpty(vClTipoSeleccion))
                    vClTipoSeleccion = "TODOS";

                vXmlTipoSeleccion = new XElement("SELECCION",
                                                    new XElement("FILTRO",
                                                    new XAttribute("CL_TIPO", vClTipoSeleccion),
                                                    new XAttribute("CL_TIPO_PUESTO", vClTipoAdscripcion)));
            }

        }

        protected void grdAdscripcion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_ADSCRIPCION_PDE_Result> ListaAdscripcion = new List<SPE_OBTIENE_ADSCRIPCION_PDE_Result>();
            AdscripcionesNegocio negocio = new AdscripcionesNegocio();
            ListaAdscripcion = negocio.ObtieneAdscripciones();
            grdAdscripcion.DataSource = ListaAdscripcion;
        }

        protected void btnSeleccion_Click(object sender, EventArgs e)
        {
            //foreach (GridDataItem item in grdPuesto.SelectedItems)
            //{
            //    vIdPuesto = item.GetDataKeyValue("ID_PUESTO").ToString();

            //    vEmpleadosSeleccionados.Add(new E_OBTIENE_PUESTO_EMPLEADOS
            //    {
            //        ID_PUESTO = vIdPuesto,
            //        FG_ACTIVO = true,
            //    });

            //    var vXelements = vEmpleadosSeleccionados.Select(x =>
            //                             new XElement("PUESTO",
            //                             new XAttribute("ID_PUESTO", x.ID_PUESTO),
            //                             new XAttribute("FG_ACTIVO", x.FG_ACTIVO)
            //                  ));
            //    SELECCIONEMPLEADOS =
            //    new XElement("SELECCION", vXelements
            //    );
            //}

            //if (SELECCIONEMPLEADOS != null)
            //{
            //    ConfiguracionNotificacionNegocio puesto = new ConfiguracionNotificacionNegocio();
            //    E_RESULTADO vResultado = puesto.INSERTA_ACTUALIZA_EMPLEADO_PUESTO_PDE(SELECCIONEMPLEADOS.ToString(), vClUsuario, vNbPrograma);
            //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            //    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "onCloseWindow");

            //}
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona un puesto", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

            //}
        }

        protected void grdAdscripcion_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdAdscripcion.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}