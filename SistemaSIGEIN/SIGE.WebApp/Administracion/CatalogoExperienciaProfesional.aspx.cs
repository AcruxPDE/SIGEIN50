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
    public partial class CatalogoExperienciaProfesional : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        private int vID_AREA_INTERES
        {
            get { return (int)ViewState["vsID_AREA_INTERES"]; }
            set { ViewState["vsID_AREA_INTERES"] = value; }
        }

        private List<SPE_OBTIENE_C_AREA_INTERES_Result> ExperienciasProfesionales;



        protected void Page_Load(object sender, EventArgs e)
        {
            ExperienciasProfesionales = new List<SPE_OBTIENE_C_AREA_INTERES_Result>();
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!IsPostBack)
            {

            }

            AreaInteresNegocio negocio = new AreaInteresNegocio();
            ExperienciasProfesionales = negocio.Obtener_C_AREA_INTERES();

        }

        #region grdCatExperProf_NeedDataSource
        protected void grdCatExperProf_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCatExperProf.DataSource = ExperienciasProfesionales;
        }

        #endregion

        #region grdCatExperProf_ItemDataBound
        protected void grdCatExperProf_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCatExperProf.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCatExperProf.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCatExperProf.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCatExperProf.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCatExperProf.MasterTableView.ClientID);
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

            AreaInteresNegocio negocio = new AreaInteresNegocio();
            var valida_eliminacion = false;
            foreach (GridDataItem item in grdCatExperProf.SelectedItems)
            {
                valida_eliminacion = true;

                vID_AREA_INTERES = (int.Parse(item.GetDataKeyValue("ID_AREA_INTERES").ToString()));

                var x = negocio.Obtener_C_AREA_INTERES(id_area_interes: vID_AREA_INTERES).FirstOrDefault();

                E_RESULTADO vResultado = negocio.Elimina_C_AREA_INTERES(ID_AREA_INTERES: vID_AREA_INTERES, CL_AREA_INTERES: x.CL_AREA_INTERES, programa: vNbPrograma, usuario: vClUsuario);

                // E_RESULTADO vResultado= negocio.Elimina_C_ESCOLARIDAD(ID_ESCOLARIDAD: vIdEscolaridad, programa: "CatalogoCarrPosgrados.aspx", usuario: "felipe");

                // = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");


            }



        }
    }
}