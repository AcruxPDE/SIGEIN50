using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class ProgramasCapacitacion : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        List<E_PROGRAMA> vProgramas
        {
            get { return (List<E_PROGRAMA>)ViewState["vs_vProgramas"]; }
            set { ViewState["vs_vProgramas"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {

            }
        }

        //protected void grdProgramaCapacitacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    ProgramaNegocio nPrograma = new ProgramaNegocio();
        //    vProgramas = parseListaProgramasCapacitacion(nPrograma.ObtieneProgramasCapacitacion());
        //    grdProgramaCapacitacion.DataSource = vProgramas;
        //}


        public List<E_PROGRAMA> parseListaProgramasCapacitacion(List<SPE_OBTIENE_C_PROGRAMA_Result> lista)
        {
            List<E_PROGRAMA> programas = new List<E_PROGRAMA>();
            if (lista.Count > 0)
            {
                foreach (var item in lista)
                {
                    programas.Add(new E_PROGRAMA
                    {
                        ID_PROGRAMA = item.ID_PROGRAMA,
                        ID_PERIODO = item.ID_PERIODO,
                        CL_PROGRAMA = item.CL_PROGRAMA,
                        NB_PROGRAMA = item.NB_PROGRAMA,
                        CL_TIPO_PROGRAMA = item.CL_TIPO_PROGRAMA,
                        CL_ESTADO = item.CL_ESTADO,
                        CL_AUTORIZACION = item.CL_AUTORIZACION,
                        DS_NOTAS = item.DS_NOTAS,
                        ID_DOCUMENTO_AUTORIZACION = item.ID_DOCUMENTO_AUTORIZACION,
                        CL_DOCUMENTO = item.CL_DOCUMENTO,
                        VERSION = item.VERSION,
                        NO_COMPETENCIAS = item.NO_COMPETENCIAS,
                        NO_PARTICIPANTES = item.NO_PARTICIPANTES,
                        FE_CREACION = item.FE_CREACION
                    });
                }
            }
            return programas;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ProgramaNegocio nPrograma = new ProgramaNegocio();
            foreach (RadListViewDataItem item in rlvProgramas.SelectedItems)
            {
                //GridDataItem item = grdProgramaCapacitacion.SelectedItems[0] as GridDataItem;
                int idPrograma = int.Parse(item.GetDataKeyValue("ID_PROGRAMA").ToString());
                E_RESULTADO vResultado = nPrograma.EliminaProgramaCapacitacion(idPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150);
                txtClPeriodo.Text = "";
                txtDsPeriodo.Text = "";
                txtClEstatus.Text = "";
                txtTipo.Text = "";
                txtUsuarioMod.Text = "";
                txtFechaMod.Text = "";

                rlvProgramas.Rebind();
            }
        }

        protected void btnTerminarPrograma_Click(object sender, EventArgs e)
        {
            ProgramaNegocio nPrograma = new ProgramaNegocio();

            foreach (RadListViewDataItem item in rlvProgramas.SelectedItems)
            {
                //GridDataItem vItem = grdProgramaCapacitacion.SelectedItems[0] as GridDataItem;
                int vIdPrograma = int.Parse(item.GetDataKeyValue("ID_PROGRAMA").ToString());

                E_RESULTADO vResultado = nPrograma.TerminarProgramaCapacitacion(vIdPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150);
                rlvProgramas.Rebind();
            }
        }

        //protected void grdProgramaCapacitacion_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridPagerItem)
        //    {
        //        RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

        //        PageSizeCombo.Items.Clear();
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
        //        PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdProgramaCapacitacion.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
        //        PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdProgramaCapacitacion.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
        //        PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdProgramaCapacitacion.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
        //        PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdProgramaCapacitacion.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
        //        PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdProgramaCapacitacion.MasterTableView.ClientID);
        //        PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
        //    }
        //}

        protected void rlvProgramas_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            ProgramaNegocio nPrograma = new ProgramaNegocio();
            vProgramas = parseListaProgramasCapacitacion(nPrograma.ObtieneProgramasCapacitacion());
            rlvProgramas.DataSource = vProgramas;
        }

        private void ordenarListView(string ordenamiento)
        {
            var campo = cmbOrdenamiento.SelectedValue;
            rlvProgramas.Items[0].FireCommandEvent(RadListView.SortCommandName, campo + ordenamiento);
        }

        protected void rbAscendente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAscendente.Checked)
            {
                ordenarListView(" ASC");
            }
        }

        protected void rbDescendente_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDescendente.Checked)
            {
                ordenarListView(" DESC");
            }
        }

        protected void rfFiltros_ApplyExpressions(object sender, RadFilterApplyExpressionsEventArgs e)
        {
            RadFilterListViewQueryProvider provider = new RadFilterListViewQueryProvider(new List<RadFilterGroupOperation>() { RadFilterGroupOperation.And, RadFilterGroupOperation.Or });
            provider.ProcessGroup(e.ExpressionRoot);

            if (provider.ListViewExpressions.Count > 0)
            {
                rlvProgramas.FilterExpressions.Add(provider.ListViewExpressions[0]);
            }
            else
            {
                rlvProgramas.FilterExpressions.Clear();
            }

            rlvProgramas.Rebind();
        }

        protected void rlvProgramas_ItemCommand(object sender, RadListViewCommandEventArgs e)
        {
            RadListViewDataItem item = e.ListViewItem as RadListViewDataItem;
            int vIdPeriodoLista = 0;
            if (int.TryParse(item.GetDataKeyValue("ID_PROGRAMA").ToString(), out vIdPeriodoLista))
            {
                ProgramaNegocio nPrograma = new ProgramaNegocio();
                SPE_OBTIENE_C_PROGRAMA_Result vPrograma = nPrograma.ObtieneProgramasCapacitacion(pIdPrograma: vIdPeriodoLista).FirstOrDefault();

                txtClPeriodo.Text = vPrograma.CL_PROGRAMA;
                txtDsPeriodo.Text = vPrograma.NB_PROGRAMA;
                txtClEstatus.Text = vPrograma.CL_ESTADO;
                txtTipo.Text = vPrograma.CL_TIPO_PROGRAMA;
                txtUsuarioMod.Text = vPrograma.CL_USUARIO_APP_MODIFICA;
                txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPrograma.FE_MODIFICA);
            }

        }

    }
}