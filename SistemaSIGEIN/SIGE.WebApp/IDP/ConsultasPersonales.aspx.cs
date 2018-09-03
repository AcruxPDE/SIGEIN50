using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Entidades;
using SIGE.WebApp.Comunes;
using System.IO;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;


namespace SIGE.WebApp.IDP
{
    public partial class ConsultasPersonales : System.Web.UI.Page
    {
        #region Variables
        public int vIdBateria
        {
            get { return (int)ViewState["vs_vIdBateria"]; }
            set { ViewState["vs_vIdBateria"] = value; }
        }

        private List<E_CONSULTA_DETALLE> vListaDetallada
        {
            get { return (List<E_CONSULTA_DETALLE>)ViewState["vs_vListaDetallada"]; }
            set { ViewState["vs_vListaDetallada"] = value; }
        }

        private List<E_CONSULTA_PERSONAL> vListaPersonal
        {
            get { return (List<E_CONSULTA_PERSONAL>)ViewState["vs_vListaPersonal"]; }
            set { ViewState["vs_vListaPersonal"] = value; }
        }

        private List<SPE_OBTIENE_COMPETENCIAS_CONSULTA> vListaCompetencia
        {
            get { return (List<SPE_OBTIENE_COMPETENCIAS_CONSULTA>)ViewState["vs_vListaCompetencia"]; }
            set { ViewState["vs_vListaCompetencia"] = value; }
        }

        private List<SPE_OBTIENE_FACTORES_CONSULTA> vListaFactores
        {
            get { return (List<SPE_OBTIENE_FACTORES_CONSULTA>)ViewState["vs_vListaFactores"]; }
            set { ViewState["vs_vListaFactores"] = value; }
        }

        public bool vFgConsultaparcial
        {
            get { return (bool)ViewState["vs_vFgConsultaparcial"]; }
            set { ViewState["vs_vFgConsultaparcial"] = value; }
        }

        private List<KeyValuePair<string, string>> oLstClavesPruebas
        {
            get { return (List<KeyValuePair<string, string>>)ViewState["vs_cp_claves_pruebas"]; }
            set { ViewState["vs_cp_claves_pruebas"] = value; }
        }

        

        #endregion

        public void CrearListaPruebas()
        {
            oLstClavesPruebas = new List<KeyValuePair<string, string>>();

            oLstClavesPruebas.Add(new KeyValuePair<string, string>("Personalidad laboral 1", "PL1"));
            oLstClavesPruebas.Add(new KeyValuePair<string, string>("Personalidad laboral 2", "PL2"));
            oLstClavesPruebas.Add(new KeyValuePair<string, string>("Estilo de pensamiento", "EP"));
            oLstClavesPruebas.Add(new KeyValuePair<string, string>("Intereses Personales", "IP"));
            oLstClavesPruebas.Add(new KeyValuePair<string, string>("Aptitud mental", "APTM"));
            oLstClavesPruebas.Add(new KeyValuePair<string, string>("Comunicación", "COM"));
            oLstClavesPruebas.Add(new KeyValuePair<string,string>("Colores","COLORES"));
            oLstClavesPruebas.Add(new KeyValuePair<string, string>("Técnico", "TECNICO"));
            oLstClavesPruebas.Add(new KeyValuePair<string, string>("TIVA", "TIVA"));
            oLstClavesPruebas.Add(new KeyValuePair<string, string>("Entrevista", "Entrevista"));
        }

        public string ObtenerClave(string pClave)
        {
            return oLstClavesPruebas.Where(t => t.Key.Equals(pClave)).FirstOrDefault().Value;
        }

        protected decimal? CalculaPorcentaje(decimal? pPorcentaje)
        {
            decimal? vPorcentaje = 0;
            if (pPorcentaje > 100)
                vPorcentaje = 100;
            else vPorcentaje = pPorcentaje;
            return vPorcentaje;
        }

        private void GenerarExcel()
        {
            ConsultaPersonalNegocio neg = new ConsultaPersonalNegocio();

            UDTT_ARCHIVO excel = neg.obtieneConsultaPersonalResumenDT(vIdBateria, vFgConsultaparcial, txtCandidato.InnerText, txtFolio.InnerText);

            if (excel.FI_ARCHIVO.Length != 0)
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + excel.NB_ARCHIVO);
                Response.BinaryWrite(excel.FI_ARCHIVO);
                Response.Flush();
                Response.End();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "No hay datos para exportar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
            }
        }


        private void GenerarExcelDetallada()
        {
            ConsultaPersonalNegocio neg = new ConsultaPersonalNegocio();

            UDTT_ARCHIVO excel = neg.obtieneConsultaPersonalDetalladaExcel(vIdBateria, txtCandidato.InnerText, txtFolio.InnerText);

            if (excel.FI_ARCHIVO.Length != 0)
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + excel.NB_ARCHIVO);
                Response.BinaryWrite(excel.FI_ARCHIVO);
                Response.Flush();
                Response.End();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "No hay datos para exportar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                CrearListaPruebas();

                if (Request.Params["pIdBateria"] != null && Request.Params["pIdBateria"] != "null")
                {
                    vIdBateria = int.Parse(Request.Params["pIdBateria"].ToString());

                    if (Request.Params["pClTipoConsulta"] != null)
                        if (Request.Params["pClTipoConsulta"] == "RESUMIDA")
                            pvResumen.Selected = true;
                        else if (Request.Params["pClTipoConsulta"] == "DETALLADA")
                            pvDetallada.Selected = true;

                    vFgConsultaparcial = ContextoApp.IDP.ConfiguracionIntegracion.FgIgnorarCompetencias;
                    Grupos();
                    ConsultaPersonalNegocio neg = new ConsultaPersonalNegocio();

                    vListaCompetencia = neg.obtenerCompetenciasConsulta();
                    vListaFactores = neg.obtenerFactoresConsulta();

                    PruebasNegocio pNegocio = new PruebasNegocio();
                    var ConsultaPersonal = pNegocio.ObtenienePruebasResultadosCandidatos(vIdBateria);
                    txtCandidato.InnerText = ConsultaPersonal.NB_CANDIDATO;
                    txtFolio.InnerText = ConsultaPersonal.CL_SOLICITUD;

                }
            }
        }

        protected void dgvResumen_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ConsultaPersonalNegocio neg = new ConsultaPersonalNegocio();
            vListaPersonal = neg.obtieneConsultaPersonalResumen(vIdBateria, vFgConsultaparcial).Select(s => new E_CONSULTA_PERSONAL
            {
                CL_CLASIFICACION = s.CL_CLASIFICACION,
                CL_COLOR = s.CL_COLOR,
                DS_COMPETENCIA = s.DS_COMPETENCIA,
                DS_NIVEL_COMPETENCIA_PERSONA = s.DS_NIVEL_COMPETENCIA_PERSONA,
                ID_COMPETENCIA = s.ID_COMPETENCIA,
                NB_COMPETENCIA = s.NB_COMPETENCIA,
                NO_BAREMO_FACTOR = s.NO_BAREMO_FACTOR,
                NO_BAREMO_PORCENTAJE = CalculaPorcentaje(s.NO_BAREMO_PORCENTAJE),
                NO_BAREMO_PROMEDIO = s.NO_BAREMO_PROMEDIO,
            }).OrderBy(s=> s.CL_COMPETENCIA).ToList();
            dgvResumen.DataSource = vListaPersonal;
        }

        protected void dgvResumen_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                string vColor = dataItem.GetDataKeyValue("CL_COLOR").ToString();

                if (vColor != "")
                {
                    //dataItem["NB_COMPETENCIA"].BackColor = System.Drawing.ColorTranslator.FromHtml(vColor);
                    //dataItem["DS_COMPETENCIA"].BackColor = System.Drawing.ColorTranslator.FromHtml(vColor);
                    //dataItem["CL_CLASIFICACION"].BackColor = System.Drawing.ColorTranslator.FromHtml(vColor);
                    //HtmlGenericControl container = (HtmlGenericControl)dataItem.FindControl("dvColor");
                    //container.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");

                    //var dynDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("dvColor") { ID = "dynDiv" };
                    //dynDiv.Style.Add(HtmlTextWriterStyle.BackgroundColor, "Red");




                    //foreach (GridGroupHeaderItem Header in dgvResumen.MasterTableView.GetItems(GridItemType.GroupHeader))
                    //{
                    //    if (Header.GroupIndex == dataItem.GroupIndex)
                    //    {
                    //        Header.BackColor = System.Drawing.ColorTranslator.FromHtml(vColor);
                    //    }
                    //}

                }
            }

            /* if (e.Item is GridGroupHeaderItem)
             {
                 GridGroupHeaderItem item = (GridGroupHeaderItem)e.Item;
                 DataRowView groupDataRow = (DataRowView)e.Item.DataItem;

                 string vColor = groupDataRow["CL_COLOR"].ToString();
                 item.BackColor = System.Drawing.ColorTranslator.FromHtml(vColor);
             }*/
        }

        protected void pgDetallada_NeedDataSource(object sender, PivotGridNeedDataSourceEventArgs e)
        {
            //ConsultaPersonalNegocio neg = new ConsultaPersonalNegocio();

            //List<E_CONSULTA_DETALLE> lstConsulta = neg.obtieneConsultaPersonalDetallada(vIdBateria).Select(s => new E_CONSULTA_DETALLE { 
            //CL_CLASIFICACION = s.CL_CLASIFICACION,
            //CL_COLOR = s.CL_COLOR,
            //CL_FACTOR = s.CL_FACTOR,
            //CL_TIPO_COMPETENCIA = s.CL_TIPO_COMPETENCIA,
            //CL_VARIABLE = s.CL_VARIABLE,
            //DS_COMPETENCIA = s.DS_COMPETENCIA,
            //DS_FACTOR = s.DS_FACTOR,
            //ICONO = ObtenerIconoDetalle(s.NO_VALOR),
            //ID_FACTOR = s.ID_FACTOR,
            //ID_COMPETENCIA = s.ID_COMPETENCIA,
            //ID_VARIABLE = s.ID_VARIABLE,
            //NO_VALOR= s.NO_VALOR,
            //NB_FACTOR = s.NB_FACTOR,
            //NB_ABREVIATURA = s.NB_ABREVIATURA,
            //NB_COMPETENCIA = s.NB_COMPETENCIA,
            //NB_PRUEBA = s.NB_PRUEBA,
            //NB_CLASIFICACION_COMPETENCIA = s.NB_CLASIFICACION_COMPETENCIA

            //}).ToList();
            //pgDetallada.DataSource = lstConsulta;
        }


        //protected string ObtenerIconoDetalle(decimal? pValor)
        //{
        //    string vImg = null;
        //    if (pValor == 1) 
        //        vImg = "Rojo";

        //    if (pValor == 2)
        //        vImg = "Amarillo";

        //    if (pValor == 3)
        //        vImg = "Verde";

        //    return vImg;
        //}

        protected void grdDetallada_ItemCreated(object sender, GridItemEventArgs e)
        {


            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                int vIdCompetencia = int.Parse(gridItem.GetDataKeyValue("ID_COMPETENCIA").ToString());
                string vDsCompetencia = vListaCompetencia.Where(t => t.ID_COMPETENCIA == vIdCompetencia).FirstOrDefault().DS_COMPETENCIA;
                string vClasificacion = vListaCompetencia.Where(t => t.ID_COMPETENCIA == vIdCompetencia).FirstOrDefault().NB_CLASIFICACION_COMPETENCIA;

                gridItem["NB_COMPETENCIA"].ToolTip = vDsCompetencia;
                gridItem["CL_COLOR"].ToolTip = vClasificacion;
            }
        }

        protected void grdDetallada_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            List<E_CONSULTA_DETALLE> vLstDetallada = new List<E_CONSULTA_DETALLE>();
            ConsultaPersonalNegocio neg = new ConsultaPersonalNegocio();
            //grdCapacitacion.Columns.Clear();
            //if (vFgCargarGrid)
            //{
            grdDetallada.DataSource = neg.obtieneConsultaPersonalDetallada(vIdBateria, ref vLstDetallada);
            vListaDetallada = vLstDetallada;
        }

        protected void grdDetallada_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            switch (e.Column.UniqueName)
            {
                case "ID_COMPETENCIA":
                    ConfigurarColumna(e.Column, 10, "No Competencia", false, false, true, false, false);
                    break;

                case "CL_COLOR":
                    ConfigurarColumna(e.Column, 40, "", true, false, false, true, false);
                    break;

                //case "NB_CLASIFICACION_COMPETENCIA":
                //    ConfigurarColumna(e.Column, 110, "", true, false, false, false, false);
                //    break;

                case "NB_COMPETENCIA":
                    ConfigurarColumna(e.Column, 150, "Competencia", true, false, false, false, false);
                    break;

                case "ExpandColumn": break;
                default:

                    ConfigurarColumna(e.Column, 40, "", true, true, false, true, true);
                    break;
            }
        }

        protected void Grupos()
        {

            grdDetallada.ClientSettings.Scrolling.AllowScroll = true;
            grdDetallada.ClientSettings.Scrolling.UseStaticHeaders = true;
            grdDetallada.ClientSettings.Scrolling.SaveScrollPosition = true;
            grdDetallada.ClientSettings.Scrolling.FrozenColumnsCount = 0;
            grdDetallada.ClientSettings.Scrolling.CountGroupSplitterColumnAsFrozen = false;

            GridColumnGroup columnGroupPer = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupPer);
            columnGroupPer.Name = "PL1";
            columnGroupPer.HeaderText = "Personalidad laboral 1";
            //columnGroupPer.Name = "Personalidad laboral 1";
            columnGroupPer.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            

            GridColumnGroup columnGroupPer2 = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupPer2);
            columnGroupPer2.Name = "PL2";
            columnGroupPer2.HeaderText = "Personalidad laboral 2";
            //columnGroupPer2.Name = "Personalidad laboral 2";            
            columnGroupPer2.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            

            GridColumnGroup columnGroupPen = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupPen);
            columnGroupPen.Name = "EP";
            columnGroupPen.HeaderText = "Estilo de pensamiento";
            //columnGroupPen.Name = "Estilo de pensamiento";
            columnGroupPen.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;


            GridColumnGroup columnGroupInt = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupInt);
            columnGroupInt.Name = "IP";
            columnGroupInt.HeaderText = "Intereses personales";
            //columnGroupInt.Name = "Intereses Personales";
            columnGroupInt.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;


            GridColumnGroup columnGroupApt1 = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupApt1);
            columnGroupApt1.Name = "APTM";
            columnGroupApt1.HeaderText = "Aptitud mental";
            //columnGroupApt1.Name = "Aptitud mental";
            columnGroupApt1.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;


            GridColumnGroup columnGroupComu = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupComu);
            columnGroupComu.Name = "COM";
            columnGroupComu.HeaderText = "Comunicación";
            //columnGroupComu.Name = "Comunicación";
            columnGroupComu.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;


            GridColumnGroup columnGroupAM = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupAM);
            columnGroupAM.Name = "COLORES";
            columnGroupAM.HeaderText = "Adaptación al medio";
            //columnGroupAM.Name = "Colores";
            columnGroupAM.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;


            GridColumnGroup columnGroupTPC = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupTPC);
            columnGroupTPC.HeaderText = "Tecnica PC";
            columnGroupTPC.Name = "TECNICO";
            //columnGroupTPC.Name = "Técnico";
            columnGroupTPC.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;


            GridColumnGroup columnGroupTiva = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupTiva);
            columnGroupTiva.Name = "TIVA";
            columnGroupTiva.HeaderText = "Tiva";
            columnGroupTiva.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

            GridColumnGroup columnGroupEntrevista = new GridColumnGroup();
            grdDetallada.MasterTableView.ColumnGroups.Add(columnGroupEntrevista);
            columnGroupEntrevista.Name = "Entrevista";
            columnGroupEntrevista.HeaderText = "Entrevista";
            columnGroupEntrevista.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;

        }

        private void ConfigurarColumna(GridColumn pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pCentrar, bool pGroup)
        {
            if (pGenerarEncabezado)
            {
                pEncabezado = GeneraEncabezado(pColumna);
            }

            pColumna.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumna.HeaderText = pEncabezado;
            pColumna.Visible = pVisible;

            if (pCentrar)
            {
                pColumna.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            }

            if (pGroup)
            {
                pColumna.Groupable = true;
                pColumna.ColumnGroupName = GeneraGroupColumn(pColumna);
            }

            if (pFiltrarColumna & pVisible)
            {
                if (pWidth <= 60)
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
                }
            }
            else
            {
                (pColumna as GridBoundColumn).AllowFiltering = false;
            }
        }



        private string GeneraGroupColumn(GridColumn pColumna)
        {
            int vResultado;
            string vGrupo = "";
        
            string vFactor = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));
             if (int.TryParse(vFactor, out vResultado))
            {
                var vDatosFactores = vListaFactores.Where(t => t.ID_FACTOR == vResultado).FirstOrDefault();

                if (vDatosFactores != null)
                {
                    vGrupo = ObtenerClave(vDatosFactores.DS_FACTOR);
                }
            }
            return vGrupo;
        }

        private string GeneraEncabezado(GridColumn pColumna)
        {
            int vResultado;
            string vEncabezado = "";

            string vFactor = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));
        
            if (int.TryParse(vFactor, out vResultado))
            {
                var vDatosFactores = vListaFactores.Where(t => t.ID_FACTOR == vResultado).FirstOrDefault();

                if (vDatosFactores != null)
                {
                    vEncabezado = "<div style=\"writing-mode: tb-rl;height: 130px;font-size: 8pt;\">" + vDatosFactores.NB_FACTOR + "</div>";
                }
            }
            return vEncabezado;
        }

        protected void dgvResumen_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                int vIdCompetencia = int.Parse(gridItem.GetDataKeyValue("ID_COMPETENCIA").ToString());
                string vClasificacion = vListaPersonal.Where(t => t.ID_COMPETENCIA == vIdCompetencia).FirstOrDefault().CL_CLASIFICACION;

                gridItem["CL_COLOR"].ToolTip = vClasificacion;
            }
        }

        protected void dgvResumen_ItemCommand(object sender, GridCommandEventArgs e)
        {
            
        }

        protected void btnExportResumen_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }

        protected void btnExportarDetalle_Click(object sender, EventArgs e)
        {
            GenerarExcelDetallada();
        }

        //protected void pgDetallada_CellDataBound(object sender, PivotGridCellDataBoundEventArgs e)
        //{
        //    if (e.Cell is PivotGridRowHeaderCell)
        //    {
        //        if (e.Cell.Controls.Count > 1)
        //        {
        //            (e.Cell.Controls[0] as Button).Visible = false;
        //        }
        //    }

        //    if (e.Cell is PivotGridRowHeaderCell)
        //    {
        //        PivotGridRowHeaderCell cell = e.Cell as PivotGridRowHeaderCell;

        //        if (cell != null && (cell.Field as PivotGridRowField).DataField == "CL_COLOR")
        //        {
        //            var cells = (cell.Parent as PivotGridRowHeaderItem).Cells;
        //            int index = cells.Cast<PivotGridRowHeaderCell>().ToList().IndexOf(cell);
        //            if (cells.Count == 3)
        //            {
        //                cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml(cell.DataItem.ToString());
        //                cells[1].BackColor = System.Drawing.ColorTranslator.FromHtml(cell.DataItem.ToString());
        //            }
        //            else
        //            {
        //                cells[0].BackColor = System.Drawing.ColorTranslator.FromHtml(cell.DataItem.ToString());
        //            }
        //        }
        //    }
    }

    //protected void pgDetallada_ItemCommand(object sender, PivotGridCommandEventArgs e)
    //{
    //    if (e.CommandName == "ExpandCollapse")
    //    {

    //        PivotGridColumnHeaderCell cell = e.Item.Cells[0] as Telerik.Web.UI.PivotGridColumnHeaderCell;
    //        //pgDetallada.CollapsedColumnIndexes

    //        if (cell != null)
    //        {
    //            if (cell.Expanded)
    //            {
    //                cell.CssClass = "Expandido";
    //            }
    //            else
    //            {
    //                cell.CssClass = "Contraido";
    //            }
    //        }

    //    }
    //}
    //}
}