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
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;

namespace SIGE.WebApp.Administracion
{
    public partial class CatalogoCompetencias : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int? vIdEmpresa;

        public int vIdCompetencia
        {
            set { ViewState["vs_vIdCompetencia"] = value; }
            get { return (int)ViewState["vs_vIdCompetencia"]; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grvCompetencias.SelectedItems)
            {
                if (item != null)
                {
                    SPE_OBTIENE_C_COMPETENCIA_Result lista = new SPE_OBTIENE_C_COMPETENCIA_Result();

                    lista = getCompetencia(int.Parse(item.GetDataKeyValue("ID_COMPETENCIA").ToString()));

                    CompetenciaNegocio negocio = new CompetenciaNegocio();

                    E_RESULTADO vResultado = negocio.EliminaCompetenica(lista.ID_COMPETENCIA, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");

                }
            }
        }

        protected void grvCompetencias_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            CompetenciaNegocio negocio = new CompetenciaNegocio();
            grvCompetencias.DataSource = negocio.ObtieneCompetencias();
        }

        protected SPE_OBTIENE_C_COMPETENCIA_Result getCompetencia(int idCompetencia) {
            CompetenciaNegocio negocio = new CompetenciaNegocio();

            List<SPE_OBTIENE_C_COMPETENCIA_Result> lista = negocio.ObtieneCompetencias();

            var q = from o in lista
                    where o.ID_COMPETENCIA == idCompetencia
                    select new SPE_OBTIENE_C_COMPETENCIA_Result { 
                        ID_COMPETENCIA = o.ID_COMPETENCIA,
                        CL_COMPETENCIA = o.CL_COMPETENCIA,
                        NB_COMPETENCIA = o.NB_COMPETENCIA,
                        DS_COMPETENCIA = o.DS_COMPETENCIA,
                        CL_TIPO_COMPETENCIA = o.CL_TIPO_COMPETENCIA,
                        CL_CLASIFICACION = o.CL_CLASIFICACION,
                        FG_ACTIVO = o.FG_ACTIVO,
                        XML_CAMPOS_ADICIONALES = o.XML_CAMPOS_ADICIONALES
                    };

            return q.FirstOrDefault();
        }

        protected void grvCompetencias_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grvCompetencias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grvCompetencias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grvCompetencias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grvCompetencias.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grvCompetencias.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

    }
}