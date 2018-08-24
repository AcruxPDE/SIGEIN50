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
    public partial class CatalogoTipoCompetencia : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public int vClTipoCompetencia
        {
            set { ViewState["vs_vClTipoCompetencia"] = value; }
            get { return (int)ViewState["vs_vClTipoCompetencia"]; }
        }

        private string usuario = "";
        private string programa = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //usuario = Contexto.clUsuario;
            //programa = Contexto.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grvTipoCompetencia.SelectedItems)
            {
            
                    SPE_OBTIENE_S_TIPO_COMPETENCIA_Result lista = new SPE_OBTIENE_S_TIPO_COMPETENCIA_Result();

                    //lista = getTipoCompetencia(item.GetDat("CL_TIPO_CATALOGO").ToString());
                    String pCL_tipo =item.GetDataKeyValue("CL_TIPO_COMPETENCIA").ToString();
                    TipoCompetenciaNegocio negocio = new TipoCompetenciaNegocio();

                    E_RESULTADO vResultado = negocio.EliminaTipoCompetencia(pClTipoCompetencia: pCL_tipo, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma);


                    ///E_RESULTADO vResultado = negocio.Elimina_M_DEPARTAMENTO(ID_DEPARTAMENTO: vID_DEPARTAMENTO, programa: "CatalogoAreas.aspx", usuario: "felipe");

                    //   = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindow");

            }
        }

        protected void grvTipoCompetencia_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            TipoCompetenciaNegocio negocio = new TipoCompetenciaNegocio();
            grvTipoCompetencia.DataSource = negocio.ObtieneTipoCompetencia();
        }

        protected SPE_OBTIENE_S_TIPO_COMPETENCIA_Result getTipoCompetencia(string clTipoCompetencia) {

            TipoCompetenciaNegocio negocio = new TipoCompetenciaNegocio();

            List<SPE_OBTIENE_S_TIPO_COMPETENCIA_Result> lista = negocio.ObtieneTipoCompetencia();

            var q = from o in lista
                    where o.CL_TIPO_COMPETENCIA == clTipoCompetencia
                    select new SPE_OBTIENE_S_TIPO_COMPETENCIA_Result
                    {
                        CL_TIPO_COMPETENCIA = o.CL_TIPO_COMPETENCIA,
                        NB_TIPO_COMPETENCIA = o.NB_TIPO_COMPETENCIA,
                        DS_TIPO_COMPETENCIA = o.DS_TIPO_COMPETENCIA,
                        FG_ACTIVO = o.FG_ACTIVO,
                        NB_ACTIVO = o.NB_ACTIVO
                    };

            return q.FirstOrDefault();
        }

        protected void grvTipoCompetencia_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grvTipoCompetencia.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grvTipoCompetencia.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grvTipoCompetencia.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grvTipoCompetencia.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grvTipoCompetencia.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}