using Newtonsoft.Json;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.FYD
{
    public partial class ConsultasIndividuales : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";
        private int? vIdRol;

        public int vIdPeriodo {
            get { return (int)ViewState["vs_ci_id_periodo"]; }
            set { ViewState["vs_ci_id_periodo"] = value; }
        }

        private List<E_PERIODO_EVALUACION> oListaPeriodosFuente
        {
            get { return (List<E_PERIODO_EVALUACION>)ViewState["vs_ci_lista_periodos_fuente"]; }
            set { ViewState["vs_ci_lista_periodos_fuente"] = value; }
        }

        private List<E_PERIODO_EVALUACION> oListaPeriodosDestino
        {
            get { return (List<E_PERIODO_EVALUACION>)ViewState["vs_ci_lista_periodos_destino"]; }
            set { ViewState["vs_ci_lista_periodos_destino"] = value; }
        }

        public Guid vIdReporteIndividual {
            get { return (Guid)ViewState["vs_ci_id_reporte_individual"]; }
            set { ViewState["vs_ci_id_reporte_individual"] = value; }
        }

        private List<E_PERIODO_EVALUACION> oLstPeriodos
        {
            get { return (List<E_PERIODO_EVALUACION>)ViewState["vs_ci_lista_periodos"]; }
            set { ViewState["vs_ci_lista_periodos"] = value; }
        }

       
        #endregion

        #region Funciones

        private void CargarPeriodo()
        {
            ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
            var oPeriodo = neg.ObtenerPeriodoEvaluacion(vIdPeriodo);
            string vTiposEvaluacion = "";

            if (oPeriodo != null)
            {
                //txtNbPeriodo.InnerText = oPeriodo.DS_PERIODO;
                //txtNoPeriodo.InnerText = oPeriodo.CL_PERIODO;
<<<<<<< HEAD
                txtClPeriodo.InnerText = oPeriodo.CL_PERIODO;
                txtDsPeriodo.InnerText = oPeriodo.NB_PERIODO;
=======
                txtClPeriodo.InnerText = oPeriodo.NB_PERIODO;
                txtDsPeriodo.InnerText = oPeriodo.DS_PERIODO;
>>>>>>> DEV
                txtEstatus.InnerText = oPeriodo.CL_ESTADO_PERIODO;

                if (oPeriodo.FG_AUTOEVALUACION)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Autoevaluación" : String.Join(", ", vTiposEvaluacion, "Autoevaluacion");
                }

                if (oPeriodo.FG_SUPERVISOR)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Superior" : String.Join(", ", vTiposEvaluacion, "Superior");
                }

                if (oPeriodo.FG_SUBORDINADOS)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Subordinado" : String.Join(", ", vTiposEvaluacion, "Subordinado");
                }

                if (oPeriodo.FG_INTERRELACIONADOS)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Interrelacionado" : String.Join(", ", vTiposEvaluacion, "Interrelacionado");
                }

                if (oPeriodo.FG_OTROS_EVALUADORES)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Otros" : String.Join(", ", vTiposEvaluacion, "Otros");
                }

                txtTipoEvaluacion.InnerText = vTiposEvaluacion;
                if (oPeriodo.DS_NOTAS != null)
                {
                    if (oPeriodo.DS_NOTAS.Contains("DS_NOTA"))
                    {
                        txtNotas.InnerHtml = Utileria.MostrarNotas(oPeriodo.DS_NOTAS);
                    }
                    else
                    {
                        XElement vNotas = XElement.Parse(oPeriodo.DS_NOTAS);
                        if (vNotas != null)
                        {
                            vNotas.Name = vNbFirstRadEditorTagName;
                            txtNotas.InnerHtml = vNotas.ToString();
                        }
                    }
                }

                oLstPeriodos.Add(new E_PERIODO_EVALUACION
                {
                    ID_PERIODO = oPeriodo.ID_PERIODO,
                    CL_PERIODO = oPeriodo.CL_PERIODO,
                    NB_PERIODO = oPeriodo.NB_PERIODO,
                    DS_PERIODO = oPeriodo.DS_PERIODO,
                });

            }
        }

        private void CargarDatosGenerales()
        {
            ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();

            oListaPeriodosFuente = neg.ObtenerPeriodosEvaluacion();
            oListaPeriodosDestino = new List<E_PERIODO_EVALUACION>();

            //rlbPeriodosDisponibles.DataSource = oListaPeriodosFuente;
            //rlbPeriodosComparar.DataSource = oListaPeriodosDestino;

            //rlbPeriodosComparar.DataBind();
            //rlbPeriodosDisponibles.DataBind();
        }

        private void AgregarPeriodos(string pPeriodos)
        {
            List<E_SELECTOR_PERIODO> vLstPeriodos = JsonConvert.DeserializeObject<List<E_SELECTOR_PERIODO>>(pPeriodos);

            foreach (E_SELECTOR_PERIODO item in vLstPeriodos)
            {

                if (oLstPeriodos.Where(t => t.ID_PERIODO == item.idPeriodo).Count() == 0)
                {
                    oLstPeriodos.Add(new E_PERIODO_EVALUACION
                    {
                        ID_PERIODO = item.idPeriodo,
                        CL_PERIODO = item.clPeriodo,
                        NB_PERIODO = item.nbPeriodo,
                        DS_PERIODO = item.dsPeriodo,
                    });

                    ContextoReportes.oReporteIndividual.Where(t => t.vIdReporteIndividual == vIdReporteIndividual).FirstOrDefault().vListaPeriodos.Add(item.idPeriodo);
                }
            }
            rgPeriodosComparar.Rebind();
        }

        private void EliminarPeriodo(int pIdPeriodo)
        {
            ContextoReportes.oReporteIndividual.Where(t => t.vIdReporteIndividual == vIdReporteIndividual).FirstOrDefault().vListaPeriodos.Remove(pIdPeriodo);
            E_PERIODO_EVALUACION e = oLstPeriodos.Where(t => t.ID_PERIODO == pIdPeriodo).FirstOrDefault();

            if (e != null)
            {
                oLstPeriodos.Remove(e);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {
                vIdReporteIndividual = Guid.NewGuid();
                oLstPeriodos = new List<E_PERIODO_EVALUACION>();

                if (ContextoReportes.oReporteIndividual == null)
                {
                    ContextoReportes.oReporteIndividual = new List<E_REPORTE_INDIVIDUAL>();
                }

                if (Request.Params["IdPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());
                    CargarPeriodo();
                }

                ContextoReportes.oReporteIndividual.Add(new E_REPORTE_INDIVIDUAL { vIdReporteIndividual = vIdReporteIndividual, vIdPeriodo = vIdPeriodo });

                CargarDatosGenerales();
            }
        }

        protected void rgEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ConsultaIndividualNegocio neg = new ConsultaIndividualNegocio();
            rgEvaluados.DataSource = neg.ObtenerEvaluados(pIdPeriodo: vIdPeriodo, pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol);
           // rgEvaluados.DataSource = neg.ObtenerEvaluados(vIdPeriodo );
        }

        protected void rlbPeriodosDisponibles_Transferred(object sender, Telerik.Web.UI.RadListBoxTransferredEventArgs e)
        {
            /*if (e.SourceListBox.ID == "rlbPeriodosDisponibles")
            {
                List<int> oLista = e.Items.Select(t => int.Parse(t.Value)).ToList();
                ContextoReportes.oReporteIndividual.Where(t => t.vIdReporteIndividual == vIdReporteIndividual).FirstOrDefault().vListaPeriodos.AddRange(oLista);
            }

            if (e.SourceListBox.ID == "rlbPeriodosComparar")
            {
                List<int> oLista = e.Items.Select(t => int.Parse(t.Value)).ToList();
                foreach (int item in oLista)
                {
                    ContextoReportes.oReporteIndividual.Where(t => t.vIdReporteIndividual == vIdReporteIndividual).FirstOrDefault().vListaPeriodos.Remove(item);
                }
            }*/
        }

        protected void rgPeriodosComparar_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgPeriodosComparar.DataSource = oLstPeriodos;
        }

        protected void ramConsultasIndividuales_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "PERIODO")
                AgregarPeriodos(vSeleccion.oSeleccion.ToString());
        }

        protected void rgPeriodosComparar_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EliminarPeriodo(int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString()));
            }
        }

        protected void rgPeriodosComparar_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgPeriodosComparar.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgPeriodosComparar.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgPeriodosComparar.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgPeriodosComparar.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgPeriodosComparar.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
			
        }

        protected void rgEvaluados_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;
                foreach (GridColumn column in rgEvaluados.MasterTableView.RenderColumns)
                {
                    if (column is GridBoundColumn)
                    {
                        //this line will show a tooltip based on the CustomerID data field
                        gridItem["NB_PUESTO"].ToolTip = "Clave: " +
                        gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["CL_PUESTO"].ToString();

                        //This is in case you wish to display the column name instead of data field.
                        gridItem["NB_EMPLEADO"].ToolTip = "No. de empleado: " +
                       gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["CL_EMPLEADO"].ToString();

                        gridItem["CL_EMPLEADO"].ToolTip =
                       gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["NB_EMPLEADO_COMPLETO"].ToString();
                    }
                }
            }
        }
    }
}