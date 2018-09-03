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
    public partial class CatalogoNivelEscolaridad : System.Web.UI.Page
    {
        //
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdNivelEscolaridad
        {
            get { return (int)ViewState["vsID_NIVEL_ESCOLARIDAD"]; }
            set { ViewState["vsID_NIVEL_ESCOLARIDAD"] = value; }
        }

        private int vIdEscolaridad
        {
            get { return (int)ViewState["vsID_ESCOLARIDAD"]; }
            set { ViewState["vsID_ESCOLARIDAD"] = value; }
        }
       
  


        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!IsPostBack)
            {
            }
          

        }

        #region grdNivelEscolaridades_NeedDataSource
        protected void grdNivelEscolaridades_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            NivelEscolaridadNegocio negocionivel = new NivelEscolaridadNegocio();
            var vNivel_Escolaridades = negocionivel.Obtener_C_NIVEL_ESCOLARIDAD();
            grdNivelEscolaridades.DataSource = vNivel_Escolaridades;

        }

        #endregion

        #region grdNivelEscolaridades_ItemDataBound
        protected void grdNivelEscolaridades_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

            }
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdNivelEscolaridades.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdNivelEscolaridades.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdNivelEscolaridades.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdNivelEscolaridades.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdNivelEscolaridades.MasterTableView.ClientID);
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


            NivelEscolaridadNegocio negocionivel = new NivelEscolaridadNegocio();

            foreach (GridDataItem item in grdNivelEscolaridades.SelectedItems)
            {


                vIdNivelEscolaridad = (int.Parse(item.GetDataKeyValue("ID_NIVEL_ESCOLARIDAD").ToString()));
                E_RESULTADO vResultado = negocionivel.Elimina_C_NIVEL_ESCOLARIDAD(ID_NIVEL_ESCOLARIDAD: vIdNivelEscolaridad, programa: vNbPrograma, usuario: vClUsuario);

                // = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");

            }

        }


    }
}