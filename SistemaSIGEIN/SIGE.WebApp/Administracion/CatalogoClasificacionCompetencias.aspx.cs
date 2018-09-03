using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.Administracion;
using Telerik.Web.UI;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Xml.Linq;
using WebApp.Comunes;
using SIGE.WebApp.Comunes;
using SIGE.Entidades.Externas;
using System.Drawing;


namespace SIGE.WebApp.Administracion
{
    public partial class CatalogoClasificacionCompetencias : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdClasificacionCompetencia
        {
            set { ViewState["vs_vIdClasificacionCompetencia"] = value; }
            get { return (int)ViewState["vs_vIdClasificacionCompetencia"]; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ClasificacionCompetenciaNegocio negocio = new ClasificacionCompetenciaNegocio();

            foreach (GridDataItem item in grvClasificacionCompetencia.SelectedItems)
            {
                vIdClasificacionCompetencia = (int.Parse(item.GetDataKeyValue("ID_CLASIFICACION_COMPETENCIA").ToString()));
                E_RESULTADO vResultado = negocio.EliminaClasificacionCompetencia(pIdClasificacionCompetencia: vIdClasificacionCompetencia, pNbPrograma: vNbPrograma, pClUsuario: vClUsuario);

                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");
            }
        }

        protected void grvClasificacionCompetencia_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClasificacionCompetenciaNegocio negocio = new ClasificacionCompetenciaNegocio();
            grvClasificacionCompetencia.DataSource = negocio.ObtieneClasificacionCompetencia();
        }

        protected void grvClasificacionCompetencia_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grvClasificacionCompetencia.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grvClasificacionCompetencia.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grvClasificacionCompetencia.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grvClasificacionCompetencia.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grvClasificacionCompetencia.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
     
    }
}