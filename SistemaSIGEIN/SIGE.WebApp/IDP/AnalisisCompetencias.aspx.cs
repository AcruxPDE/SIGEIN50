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

namespace SIGE.WebApp.IDP
{
    public partial class AnalisisCompetencias : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vID_COMPETENCIAS
        {
            get { return (int)ViewState["vsID_COMPETENCIA"]; }
            set { ViewState["vsID_COMPETENCIA"] = value; }
        }

        private List<SPE_OBTIENE_C_COMPETENCIA_Result> vCompetencias;



        protected void Page_Load(object sender, EventArgs e)
        {
            vCompetencias = new List<SPE_OBTIENE_C_COMPETENCIA_Result>();
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            if (!IsPostBack)
            {

            }

            CompetenciaNegocio negocio = new CompetenciaNegocio();
            vCompetencias = negocio.ObtieneCompetencias();

        }

        #region grdCompetencias_NeedDataSource
        protected void grdCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCompetencias.DataSource = vCompetencias;

        }

        #endregion

        #region grdCompetencias_ItemDataBound
        protected void grdCompetencias_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
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
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCompetencias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCompetencias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCompetencias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCompetencias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCompetencias.MasterTableView.ClientID);
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

            //DepartamentoNegocio negocio = new DepartamentoNegocio();
            //var valida_eliminacion = false;
            //foreach (GridDataItem item in grdDepartamentos.SelectedItems)
            //{
            //    valida_eliminacion = true;

            //    vID_DEPARTAMENTO = (int.Parse(item.GetDataKeyValue("ID_DEPARTAMENTO").ToString()));

            //    var x = negocio.Obtener_M_DEPARTAMENTO(ID_DEPARTAMENTO: vID_DEPARTAMENTO).FirstOrDefault();

            //    E_RESULTADO vResultado = negocio.Elimina_M_DEPARTAMENTO(ID_DEPARTAMENTO: vID_DEPARTAMENTO, programa: "CatalogoAreas.aspx", usuario: "felipe");

            //    //   = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
            //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            //    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");


            //}

        }
    }
}