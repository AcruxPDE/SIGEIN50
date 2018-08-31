using WebApp.Comunes;
using SIGE.Entidades;
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
using SIGE.Negocio.AdministracionSitio;

namespace SIGE.WebApp.Administracion
{
    public partial class DescriptivoPuestos : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vID_PUESTO
        {
            get { return (int)ViewState["vsID_PUESTO"]; }
            set { ViewState["vsID_PUESTO"] = value; }
        }

        private List<SPE_OBTIENE_M_PUESTO_Result> Puestos;

        public bool CopiarDe;
        public bool VistaPrevia;
        public bool vReporte;

        #endregion

        #region Funciones

        private void SeguridadProceso()
        {
           // btnAgregar.Enabled = ContextoUsuario.oUsuario.TienePermiso("C.B.A");
         
           // btnEditar.Enabled = ContextoUsuario.oUsuario.TienePermiso("C.B.B");
           // btnEliminar.Enabled = ContextoUsuario.oUsuario.TienePermiso("C.B.C");
           

            btnAgregarNomina.Enabled = ContextoUsuario.oUsuario.TienePermiso("C.A");
            btnEditarNomina.Enabled = ContextoUsuario.oUsuario.TienePermiso("C.B");
            btnEliminarNomina.Enabled = ContextoUsuario.oUsuario.TienePermiso("C.C");
            btnCopiarde.Enabled = CopiarDe = ContextoUsuario.oUsuario.TienePermiso("C.D");
            btnVistaPrevia.Enabled = VistaPrevia = ContextoUsuario.oUsuario.TienePermiso("C.E");
            btnReporte.Enabled = vReporte = ContextoUsuario.oUsuario.TienePermiso("C.F");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            SeguridadProceso();

            if (!IsPostBack)
            {
                //if (Request.Params["clOrigen"] != null)
                //{
                //    if (Request.Params["clOrigen"].ToString() == "NO")
                //    {
                //        btnAgregarNomina.Visible = true;
                //        btnEditarNomina.Visible = true;
                //        btnEliminarNomina.Visible = true;
                //    }
                    
                //}
                //else
                //{
                //    btnAgregar.Visible = true;
                //    btnEditar.Visible = true;
                //    btnEliminar.Visible = true;
                //    btnCopiarde.Visible = true;
                //    btnVistaPrevia.Visible = true;
                //    btnReporte.Visible = true;
                //}
            }
        }
      
        protected void grdDescriptivo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //if (Request.Params["clOrigen"] != null)
            //{
            //    if (Request.Params["clOrigen"].ToString() == "NO")
            //    {
            //        CamposNominaNegocio oNegocio = new CamposNominaNegocio();
            //        grdDescriptivo.DataSource = oNegocio.ObtienePuestosNominaDo();
            //    }
            //}
            //else
            //{
                PuestoNegocio nPuesto = new PuestoNegocio();
                grdDescriptivo.DataSource = nPuesto.ObtienePuestosGeneral();
           // }

        }

        protected void btnEliminar_click(object sender, EventArgs e)
        {
            PuestoNegocio nPuesto = new PuestoNegocio();

            foreach (GridDataItem item in grdDescriptivo.SelectedItems)
            {
                vID_PUESTO = (int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString()));

                E_RESULTADO vResultado = nPuesto.EliminaPuesto(pIdPuesto: vID_PUESTO, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "RebindGrid");
               
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/IDP/VentanaDescriptivoPuesto.aspx");
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

        protected void btnEliminarNomina_Click(object sender, EventArgs e)
        {
            CamposNominaNegocio oNegocio = new CamposNominaNegocio();
            foreach (GridDataItem item in grdDescriptivo.SelectedItems)
            {
                vID_PUESTO = (int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString()));

                E_RESULTADO vResultado = oNegocio.EliminaPuestoNominaDO(pID_PUESTO: vID_PUESTO);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "RebindGrid");

            }
        }
    }
}