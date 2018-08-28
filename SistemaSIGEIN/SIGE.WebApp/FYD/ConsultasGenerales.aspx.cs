using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Administracion;
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
    public partial class ConsultasGenerales : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private int? vIdRol;
        private string vNbFirstRadEditorTagName = "p";

        private XElement vSeleccion { get; set; }

        private List<E_EMPLEADO> ListaEmpleados
        {
            get { return (List<E_EMPLEADO>)ViewState["vs_cg_lista_empleados"]; }
            set { ViewState["vs_cg_lista_empleados"] = value; }
        }

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_cg_id_periodo"]; }
            set { ViewState["vs_cg_id_periodo"] = value; }
        }

        public Guid vIdReporteGlobal
        {
            get { return (Guid)ViewState["vs_cg_id_reporte_global"]; }
            set { ViewState["vs_cg_id_reporte_global"] = value; }
        }

        public Guid vIdReporteComparativo
        {
            get { return (Guid)ViewState["vs_cg_id_reporte_comparativo"]; }
            set { ViewState["vs_cg_id_reporte_comparativo"] = value; }
        }

        private E_REPORTE_GLOBAL oReporteGlobal
        {
            get { return (E_REPORTE_GLOBAL)ViewState["vs_cg_reporte_general"]; }
            set { ViewState["vs_cg_reporte_general"] = value; }
        }

        private List<E_PERIODO_EVALUACION> oLstPeriodos
        {
            get { return (List<E_PERIODO_EVALUACION>)ViewState["vs_cg_lista_periodos"]; }
            set { ViewState["vs_cg_lista_periodos"] = value; }
        }

        //private List<E_PERIODO_EVALUACION> oListaPeriodosDestino
        //{
        //    get { return (List<E_PERIODO_EVALUACION>)ViewState["vs_cg_lista_periodos_destino"]; }
        //    set { ViewState["vs_cg_lista_periodos_destino"] = value; }
        //}

        private List<E_EVALUADO> vLstEvaluados
        {
            get { return (List<E_EVALUADO>)ViewState["vs_cg_lista_evaluados"]; }
            set { ViewState["vs_cg_lista_evaluados"] = value; }
        }

        #endregion

        #region Fucniones

        protected void AgregarEvaluadosPorEmpleado(string pEvaluados)
        {
            List<E_SELECTOR_EMPLEADO> vEvaluados = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(pEvaluados);

            vSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO")));

            if (vEvaluados.Count > 0)
            {
                vSeleccion.Element("FILTRO").Add(vEvaluados.Select(s => new XElement("EMP", new XAttribute("ID_EMPLEADO", s.idEmpleado))));
                //AgregarEvaluados(vSeleccion);
            }

        }

        protected void AgregarEvaluadosPorPuesto(string pPuestos)
        {
            List<E_SELECTOR_PUESTO> vPuestos = JsonConvert.DeserializeObject<List<E_SELECTOR_PUESTO>>(pPuestos);
            vSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "FYD_PUESTO")));

            if (vPuestos.Count > 0)
            {
                vSeleccion.Element("FILTRO").Add(vPuestos.Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.idPuesto))));
                //AgregarEvaluados(vSeleccion);
            }
        }

        protected void AgregarEvaluadosPorArea(string pAreas)
        {
            List<E_SELECTOR_AREA> vAreas = JsonConvert.DeserializeObject<List<E_SELECTOR_AREA>>(pAreas);
            vSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "FYD_AREA")));

            if (vAreas.Count > 0)
            {
                vSeleccion.Element("FILTRO").Add(vAreas.Select(s => new XElement("AREA", new XAttribute("ID_AREA", s.idArea))));
                //AgregarEvaluados(vSeleccion);
            }
        }

        protected void GuardarEvaluados()
        {
            foreach (E_EVALUADO item in vLstEvaluados)
            {
                ContextoReportes.oReporteGlobal.Where(t => t.vIdReporteGlobal == vIdReporteGlobal).FirstOrDefault().vListaEmpleados.Add(item.ID_EMPLEADO);
            }
        }

        private void EliminarEmpleado(int pIdEmpleado)
        {
            ContextoReportes.oReporteGlobal.Where(t => t.vIdReporteGlobal == vIdReporteGlobal).FirstOrDefault().vListaEmpleados.Remove(pIdEmpleado);
            E_EVALUADO e = vLstEvaluados.Where(t => t.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

            if (e != null)
            {
                vLstEvaluados.Remove(e);
            }
        }

        private void EliminarPeriodo(int pIdPeriodo)
        {
            ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vListaPeriodos.Remove(pIdPeriodo);
            E_PERIODO_EVALUACION e = oLstPeriodos.Where(t => t.ID_PERIODO == pIdPeriodo).FirstOrDefault();

            if (e != null)
            {
                oLstPeriodos.Remove(e);
            }
        }

        private void CargarPeriodo()
        {
            ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
            SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result oPeriodo = neg.ObtenerPeriodoEvaluacion(vIdPeriodo);
            string vTiposEvaluacion = "";

            if (oPeriodo != null)
            {
                //txtPeriodo.InnerText = oPeriodo.DS_PERIODO;
                //txtClavePeriodo.InnerText = oPeriodo.CL_PERIODO;
                txtClPeriodo.InnerText = oPeriodo.NB_PERIODO;
                txtDsPeriodo.InnerText = oPeriodo.DS_PERIODO;
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

                ConsultaGeneralNegocio nConsulta = new ConsultaGeneralNegocio();
                vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
                vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
                vLstEvaluados = nConsulta.ObtieneEvaluados(vIdPeriodo, vIdEmpresa, vIdRol);
                GuardarEvaluados();

                oLstPeriodos.Add(new E_PERIODO_EVALUACION
                {
                    ID_PERIODO = oPeriodo.ID_PERIODO,
                    CL_PERIODO = oPeriodo.CL_PERIODO,
                    NB_PERIODO = oPeriodo.NB_PERIODO,
                    DS_PERIODO = oPeriodo.DS_PERIODO
                });

            }

            //oListaPeriodosFuente = neg.ObtenerPeriodosEvaluacion();
            //oListaPeriodosDestino = new List<E_PERIODO_EVALUACION>();
            //rlbPeriodosDisponibles.DataSource = oListaPeriodosFuente;
            //rlbPeriodosComparar.DataSource = oListaPeriodosDestino;
            //rlbPeriodosComparar.DataBind();
            //rlbPeriodosDisponibles.DataBind();

        }

        private void AgregarPeriodos(string pPeriodos)
        {
            List<E_SELECTOR_PERIODO> vLstPeriodos = JsonConvert.DeserializeObject<List<E_SELECTOR_PERIODO>>(pPeriodos);
            vSeleccion = new XElement("PERIODOS");

            foreach (E_SELECTOR_PERIODO item in vLstPeriodos)
            {

                if (oLstPeriodos.Where(t => t.ID_PERIODO == item.idPeriodo).Count() == 0)
                {
                    oLstPeriodos.Add(new E_PERIODO_EVALUACION
                    {
                        ID_PERIODO = item.idPeriodo,
                        CL_PERIODO = item.clPeriodo,
                        NB_PERIODO = item.nbPeriodo,
                        DS_PERIODO = item.dsPeriodo
                    });

                    ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vListaPeriodos.Add(item.idPeriodo);

                }
            }

            rgPeriodosComparar.Rebind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ListaEmpleados = new List<E_EMPLEADO>();
                oLstPeriodos = new List<E_PERIODO_EVALUACION>();

                if (Request.Params["IdPeriodo"] != null)
                {
                    vIdReporteGlobal = Guid.NewGuid();
                    vIdReporteComparativo = Guid.NewGuid();

                    if (ContextoReportes.oReporteGlobal == null)
                    {
                        ContextoReportes.oReporteGlobal = new List<E_REPORTE_GLOBAL>();
                    }

                    if (ContextoReportes.oReporteComparativo == null)
                    {
                        ContextoReportes.oReporteComparativo = new List<E_REPORTE_COMPARATIVO>();
                    }

                    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());

                    ContextoReportes.oReporteGlobal.Add(new E_REPORTE_GLOBAL { vIdReporteGlobal = vIdReporteGlobal, vIdPeriodo = vIdPeriodo });
                    ContextoReportes.oReporteComparativo.Add(new E_REPORTE_COMPARATIVO { vIdReporteComparativo = vIdReporteComparativo, vIdPeriodo = vIdPeriodo });
                    
                    
                    CargarPeriodo();
                }
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }

        protected void grdPersonas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdPersonas.DataSource = vLstEvaluados;
           
        }

        protected void ramConsultasGenerales_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "EMPLEADO")
                AgregarEvaluadosPorEmpleado(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "PUESTO")
                AgregarEvaluadosPorPuesto(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "DEPARTAMENTO")
                AgregarEvaluadosPorArea(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "PERIODO")
                AgregarPeriodos(vSeleccion.oSeleccion.ToString());
        }

        protected void grdPersonas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EliminarEmpleado(int.Parse(item.GetDataKeyValue("ID_EMPLEADO").ToString()));
            }
        }

        protected void rlbPeriodosDisponibles_Transferred(object sender, RadListBoxTransferredEventArgs e)
        {
            if (e.SourceListBox.ID == "rlbPeriodosDisponibles")
            {               
                List<int> oLista = e.Items.Select(t => int.Parse(t.Value)).ToList();
                ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vListaPeriodos.AddRange(oLista);
            }

            if (e.SourceListBox.ID == "rlbPeriodosComparar")
            {
                List<int> oLista = e.Items.Select(t => int.Parse(t.Value)).ToList();
                foreach (int item in oLista)
                {
                    ContextoReportes.oReporteComparativo.Where(t => t.vIdReporteComparativo == vIdReporteComparativo).FirstOrDefault().vListaPeriodos.Remove(item);
                }
            }
        }

        protected void rgPeriodosComparar_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgPeriodosComparar.DataSource = oLstPeriodos;
        }

        protected void rgPeriodosComparar_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EliminarPeriodo(int.Parse(item.GetDataKeyValue("ID_PERIODO").ToString()));
            }
        }

        protected void grdPersonas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdPersonas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdPersonas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdPersonas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdPersonas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdPersonas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
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

        protected void grdPersonas_ItemCreated(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;
                foreach (GridColumn column in grdPersonas.MasterTableView.RenderColumns)
                {
                    if (column is GridBoundColumn)
                    {
                        //this line will show a tooltip based on the CustomerID data field
                        gridItem["M_PUESTO_NB_PUESTO"].ToolTip = "Clave: " +
                        gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["CL_PUESTO"].ToString();

                        //This is in case you wish to display the column name instead of data field.
                        gridItem["NB_EMPLEADO_COMPLETO"].ToolTip = "No. de empleado: " +
                       gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["CL_EMPLEADO"].ToString();

                        gridItem["M_DEPARTAMENTO_NB_DEPARTAMENTO"].ToolTip = "Clave: " +
                        gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["CL_DEPARTAMENTO"].ToString();

                        gridItem["CL_EMPLEADO"].ToolTip =
                        gridItem.OwnerTableView.DataKeyValues[gridItem.ItemIndex]["NB_EMPLEADO_COMPLETO"].ToString();
                    }
                }
            }
        }
    }
}