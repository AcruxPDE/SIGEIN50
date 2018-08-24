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

namespace SIGE.WebApp.Administracion
{
    public partial class CatalogosEscolaridades : System.Web.UI.Page
    {
        //
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private String vClNivelEscolaridad
        {
            get { return (String)ViewState["vsCL_NIVEL_ESCOLARIDAD"]; }
            set { ViewState["vsCL_NIVEL_ESCOLARIDAD"] = value; }
        }

        private int vIdEscolaridad
        {
            get { return (int)ViewState["vsID_ESCOLARIDAD"]; }
            set { ViewState["vsID_ESCOLARIDAD"] = value; }
        }

        private Boolean vValidaEliminacion
        {
            get { return (Boolean)ViewState["vsvValidaEliminacion"]; }
            set { ViewState["vsvValidaEliminacion"] = value; }
        }
        private List<SPE_OBTIENE_C_ESCOLARIDAD_Result> VEscolaridades;
        List <SPE_OBTIENE_C_NIVEL_ESCOLARIDAD_Result >vNivel_Escolaridad;


        protected void Page_Load(object sender, EventArgs e)
        {
            VEscolaridades = new List<SPE_OBTIENE_C_ESCOLARIDAD_Result>();
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!IsPostBack)
            {
                vClNivelEscolaridad = "";
                vValidaEliminacion = false;
                NivelEscolaridadNegocio negocionivel = new NivelEscolaridadNegocio();
                vNivel_Escolaridad = negocionivel.Obtener_C_NIVEL_ESCOLARIDAD(FG_ACTIVO:true);

               // vNivel_Escolaridad = null;
                if (vNivel_Escolaridad !=null)
                {
                cmbEscolaridades.DataSource = vNivel_Escolaridad;//LLENAMOS DE DATOS EL GRID
                cmbEscolaridades.DataTextField = "CL_TIPO_ESCOLARIDAD";
                cmbEscolaridades.DataTextField = "DS_NIVEL_ESCOLARIDAD";
                cmbEscolaridades.DataValueField = "CL_NIVEL_ESCOLARIDAD";
                cmbEscolaridades.SelectedIndex = 2;
                cmbEscolaridades.DataBind();
                 vClNivelEscolaridad = cmbEscolaridades.SelectedValue;

              }
                EscolaridadNegocio negocio = new EscolaridadNegocio();
                VEscolaridades = negocio.Obtener_C_ESCOLARIDAD(CL_NIVEL_ESCOLARIDAD:vClNivelEscolaridad);
                //
            }
            else
            {
                if (vValidaEliminacion == true )
                {
                    EscolaridadNegocio negocio = new EscolaridadNegocio();
                    VEscolaridades = negocio.Obtener_C_ESCOLARIDAD(CL_NIVEL_ESCOLARIDAD: vClNivelEscolaridad);
                 
                
                }
                else

                    if ( vClNivelEscolaridad != "")
                    {
                        EscolaridadNegocio negocio = new EscolaridadNegocio();
                        VEscolaridades = negocio.Obtener_C_ESCOLARIDAD(CL_NIVEL_ESCOLARIDAD: vClNivelEscolaridad);
                    }
            }

        }

        #region grdEscolaridades_NeedDataSource
        protected void grdEscolaridades_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdEscolaridades.DataSource = VEscolaridades;

        }

        #endregion

        #region grdEscolaridades_ItemDataBound
        protected void grdEscolaridades_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                //  System.Web.UI.WebControls.CheckBox chkbx = (System.Web.UI.WebControls.CheckBox)item["checkbox"].FindControl("CheckBox1");
            }
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEscolaridades.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEscolaridades.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEscolaridades.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEscolaridades.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEscolaridades.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;

            }

        }
        #endregion

        #region OnItemCommand
        protected void OnItemCommand(object sender, GridCommandEventArgs e)
        {

            if (e.CommandName == "RowClick")
            {
   
            }
        }
        #endregion


        protected void btnEliminar_click(object sender, EventArgs e)
        {


            EscolaridadNegocio negocio = new EscolaridadNegocio();
         
            foreach (GridDataItem item in grdEscolaridades.SelectedItems)
            {
                vIdEscolaridad = (int.Parse(item.GetDataKeyValue("ID_ESCOLARIDAD").ToString()));
                E_RESULTADO vResultado = negocio.Elimina_C_ESCOLARIDAD(ID_ESCOLARIDAD: vIdEscolaridad, programa: vNbPrograma, usuario: vClUsuario);
                 string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
                vValidaEliminacion = true;
            }
         
        }
        protected void cmbEscolaridades_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            vClNivelEscolaridad = e.Value;
            EscolaridadNegocio negocio = new EscolaridadNegocio();
            VEscolaridades = negocio.Obtener_C_ESCOLARIDAD(CL_NIVEL_ESCOLARIDAD: vClNivelEscolaridad);;
            grdEscolaridades.DataSource = VEscolaridades;
            grdEscolaridades.Rebind();
        }
  
    }
}