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
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.EvaluacionOrganizacional;
using Newtonsoft.Json;

namespace SIGE.WebApp.FYD
{
    public partial class ProgramasCapacitacion : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public bool vEditar
        {
            get { return (bool)ViewState["vs_vEditar"]; }
            set { ViewState["vs_vEditar"] = value; }
        }

        public bool vEliminar
        {
            get { return (bool)ViewState["vs_vEliminar"]; }
            set { ViewState["vs_vEliminar"] = value; }
        }


        List<E_PROGRAMA> vProgramas
        {
            get { return (List<E_PROGRAMA>)ViewState["vs_vProgramas"]; }
            set { ViewState["vs_vProgramas"] = value; }
        }

        public string validarDsNotas(string vdsNotas)
        {
            E_NOTAS pNota = null;
            if (vdsNotas != null)
            {
                XElement vNotas = XElement.Parse(vdsNotas.ToString());
                if (ValidarRamaXml(vNotas, "NOTA"))
                {
                    pNota = vNotas.Elements("NOTA").Select(el => new E_NOTAS
                    {
                        DS_NOTA = UtilXML.ValorAtributo<string>(el.Attribute("DS_NOTA")),
                        FE_NOTA = (DateTime?)UtilXML.ValorAtributo(el.Attribute("FE_NOTA"), E_TIPO_DATO.DATETIME),
                    }).FirstOrDefault();
                }

                if (pNota != null)
                {
                    if (pNota.DS_NOTA != null)
                    {
                        return pNota.DS_NOTA.ToString();
                    }
                    else return "";
                }
                else
                    return "";
            }
            else
            {
                return "";
            }
        }

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);

            if (foundEl != null)
            {
                return true;
            }
            return false;
        }

        public void SeguridadProcesos()
        {
            btnNuevo.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.B.A");
            vEditar = ContextoUsuario.oUsuario.TienePermiso("K.A.B.B.B");
            vEliminar = ContextoUsuario.oUsuario.TienePermiso("K.A.B.B.C");
            btnEditar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.B.D");
            btnCopiar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.B.E");
            btnTerminarPrograma.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.B.F");
            btnAvance.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.A.B.B.G");
        }


        private void CargarDatosDetalle(int? pIdPeriodo)
        {
            ProgramaNegocio nPrograma = new ProgramaNegocio();
            SPE_OBTIENE_C_PROGRAMA_Result vPrograma = nPrograma.ObtieneProgramasCapacitacion(pIdPrograma: pIdPeriodo).FirstOrDefault();

            if (vPrograma != null)
            {

                txtClPeriodo.Text = vPrograma.CL_PROGRAMA;
                txtDsPeriodo.Text = vPrograma.NB_PROGRAMA;
                txtClEstatus.Text = vPrograma.CL_ESTADO;
                txtTipo.Text = vPrograma.CL_TIPO_PROGRAMA;
                txtUsuarioMod.Text = vPrograma.CL_USUARIO_APP_MODIFICA;
                txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPrograma.FE_MODIFICA);

                if (vPrograma.DS_NOTAS != null)
                {
                    XElement vNotas = XElement.Parse(vPrograma.DS_NOTAS);

                    if (vNotas != null)
                    {
                        string vNotasTexto = validarDsNotas(vNotas.ToString());
                        txtNotas.InnerHtml = vNotasTexto;
                    }
                }
            }
        }


        private void seleccionarPeriodo()
        {
            rlvProgramas.SelectedItems.Clear();
            rlvProgramas.SelectedIndexes.Clear();
            rlvProgramas.CurrentPageIndex = 0;
            if (rlvProgramas.Items.Count > 0)
            {
                rlvProgramas.Items[0].Selected = true;
            }
            rlvProgramas.Rebind();

            string vIdPeriodoSeleccionado = rlvProgramas.Items[0].GetDataKeyValue("ID_PROGRAMA").ToString();
            if (vIdPeriodoSeleccionado != null)
            {
                CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                SeguridadProcesos();
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
                //txtClPeriodo.Text = "";
                //txtDsPeriodo.Text = "";
                //txtClEstatus.Text = "";
                //txtTipo.Text = "";
                //txtUsuarioMod.Text = "";
                //txtFechaMod.Text = "";
                //txtNotas.InnerHtml = "";
                rlvProgramas.Rebind();

                if(rlvProgramas.Items.Count != 0)
                {
                    string vIdPeriodoSeleccionado = rlvProgramas.SelectedItems[0].GetDataKeyValue("ID_PROGRAMA").ToString();

                    if (vIdPeriodoSeleccionado != null)
                    {
                        CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
                    }
                }                   
                
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
                CargarDatosDetalle(vIdPeriodoLista);

                //ProgramaNegocio nPrograma = new ProgramaNegocio();
                //SPE_OBTIENE_C_PROGRAMA_Result vPrograma = nPrograma.ObtieneProgramasCapacitacion(pIdPrograma: vIdPeriodoLista).FirstOrDefault();

                //txtClPeriodo.Text = vPrograma.CL_PROGRAMA;
                //txtDsPeriodo.Text = vPrograma.NB_PROGRAMA;
                //txtClEstatus.Text = vPrograma.CL_ESTADO;
                //txtTipo.Text = vPrograma.CL_TIPO_PROGRAMA;
                //txtUsuarioMod.Text = vPrograma.CL_USUARIO_APP_MODIFICA;
                //txtFechaMod.Text = String.Format("{0:dd/MM/yyyy}", vPrograma.FE_MODIFICA);

                //if (vPrograma.DS_NOTAS != null)
                //{
                //    XElement vNotas = XElement.Parse(vPrograma.DS_NOTAS);

                //    if (vNotas != null)
                //    {
                //        string vNotasTexto = validarDsNotas(vNotas.ToString());
                //        txtNotas.InnerHtml = vNotasTexto;
                //    }
                //}


            }

        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "ACTUALIZARLISTA")
            {
                seleccionarPeriodo();
            }
            else if (vSeleccion.clTipo == "ACTUALIZAR")
            {
                rlvProgramas.Rebind();
                string vIdPeriodoSeleccionado = rlvProgramas.SelectedItems[0].GetDataKeyValue("ID_PROGRAMA").ToString();
                if (vIdPeriodoSeleccionado != null)
                {
                    CargarDatosDetalle(int.Parse(vIdPeriodoSeleccionado));
                }
            }
        }

    }
}