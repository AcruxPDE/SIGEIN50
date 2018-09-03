using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;
using System.Data;


namespace SIGE.WebApp.EO
{
    public partial class ConsultasRotacionPersonal : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int? vIdRol;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateTime vFecha = DateTime.Now;
                rdpFechaFin.SelectedDate = vFecha;
                //rdpFechaFinCausa.SelectedDate = vFecha;
                DateTime vFechaPrimeroMes = GetLastDay(vFecha);
                rdpFechaInicio.SelectedDate = vFechaPrimeroMes;
                //rdpFechaInicioCausa.SelectedDate = new DateTime(vFecha.Year, vFecha.Month, 1);

              
                int vIdTab = 0;
                if (int.TryParse(Request.Params["p"].ToString(), out vIdTab))
                {
                    tbReportes.Tabs[vIdTab].Selected = true;
                    mpgReportes.PageViews[vIdTab].Selected = true;
                }

                    FiltrosIndiceRotacion();

            }
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
        }

        DateTime GetLastDay(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        protected void grdHistorialBaja_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            RotacionPersonalNegocio nRotacion = new RotacionPersonalNegocio();
            var vRotacion = nRotacion.ObtenerHistorialBajas(pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol).Select(s => new E_HISTORIAL_BAJA
            {
                CL_EMPLEADO = s.CL_EMPLEADO,
                CL_ESTADO = s.CL_ACTIVO,
                DS_COMENTARIO = s.DS_COMENTARIOS,
                FECHA_INGRESO = s.FE_ALTA,
                FECHA_BAJA = s.FE_BAJA_EFECTIVA,
                FECHA_REINGRESO = s.FE_REINGRESO,
                NB_EMPLEADO = s.NB_EMPLEADO,
                NB_CAUSA_BAJA = s.NB_CAUSA_BAJA,
            });
            grdHistorialBaja.DataSource = vRotacion;
        }

        public void FiltrosIndiceRotacion()
        {
            List<E_SELECCIONADOS> vDepartamentos = new List<E_SELECCIONADOS>();
            List<E_ADICIONALES_SELECCIONADOS> vAdicionales = new List<E_ADICIONALES_SELECCIONADOS>();
            XElement vXlmFiltros = new XElement("FILTROS");
            XElement vXlmDepartamentos = new XElement("DEPARTAMENTOS");
            XElement vXlmGeneros = new XElement("GENEROS");
            XElement vXlmEdad = new XElement("EDAD");
            XElement vXlmAntiguedad = new XElement("ANTIGUEDAD");
            XElement vXlmCamposAdicional = new XElement("CAMPOS_ADICIONALES");

            foreach (RadListBoxItem item in lstDepartamentosIndice.Items)
            {
                int vIdDepartamento = int.Parse(item.Value);
                vDepartamentos.Add(new E_SELECCIONADOS { ID_SELECCIONADO = vIdDepartamento });
            }
            var vXelements = vDepartamentos.Select(x => new XElement("DEPARTAMENTO", new XAttribute("ID_DEPARTAMENTO", x.ID_SELECCIONADO)));
            vXlmDepartamentos = new XElement("DEPARTAMENTOS", vXelements);
            vXlmFiltros.Add(vXlmDepartamentos);

            foreach (RadListBoxItem item in rlbAdicionales.Items)
            {
                string vClAdicional = item.Value;
                // string vValorAdicional = item.Text;
                vAdicionales.Add(new E_ADICIONALES_SELECCIONADOS { CL_CAMPO = vClAdicional });
            }
            var vXelementsAdicionales = vAdicionales.Select(x => new XElement("ADICIONAL", new XAttribute("CL_CAMPO", x.CL_CAMPO)));
            vXlmCamposAdicional = new XElement("SELECCIONADOS", vXelementsAdicionales);
            vXlmFiltros.Add(vXlmCamposAdicional);

            foreach (RadListBoxItem item in lstGeneroIndice.Items)
            {
                vXlmGeneros = new XElement("GENERO", new XAttribute("NB_GENERO", item.Value));
            }
            vXlmFiltros.Add(vXlmGeneros);

            if (rbEdadIndice.Checked == true)
            {

                vXlmEdad = new XElement("EDAD", new XAttribute("EDAD_INICIAL", rnEdadInicial.Text), new XAttribute("EDAD_FINAL", rnEdadFinal.Text));
                vXlmFiltros.Add(vXlmEdad);
            }
            if (rbAntiguedadIndice.Checked == true)
            {
         
                vXlmAntiguedad = new XElement("ANTIGUEDAD", new XAttribute("ANTIGUEDAD_INICIAL", rnAntiguedadInicial.Text), new XAttribute("ANTIGUEDAD_FINAL", rnAtiguedadFinal.Text));
                vXlmFiltros.Add(vXlmAntiguedad);
            }
            GraficaIndiceRotacion(vXlmFiltros);
            GraficaCausaRotacion(vXlmFiltros);
        }

        protected void GraficaIndiceRotacion(XElement pFiltros)
        {
            DateTime? vFechaInicio = rdpFechaInicio.SelectedDate;
            DateTime? vFechaFinal = rdpFechaFin.SelectedDate;
            string vTipoReporte = cmbTipoReporte.Text == "" ? "Diario" : cmbTipoReporte.Text;


            RotacionPersonalNegocio nRotacion = new RotacionPersonalNegocio();
            List<E_GRAFICA_ROTACION> vGraficaRotacion = nRotacion.ObtieneGraficaIndiceRotacion(pFECHA_INICIO: vFechaInicio, pFECHA_FINAL: vFechaFinal, pTIPO_REPORTE: vTipoReporte, pXML_FILTROS: pFiltros, pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol).Select(s => new E_GRAFICA_ROTACION { FECHA = s.FECHA, NO_CANTIDAD = s.CANTIDAD_BAJA, NO_EMPLEADOS_ALTA = s.NO_EMPLEADOS_ALTA, NO_EMPLEADOS_BAJA = s.NO_EMPLEADOS_BAJA, PR_TOTAL_BAJA = s.PR_TOTAL_EMPLEADOS }).ToList();
            PintaGraficaIndiceRotacion(vGraficaRotacion);

            var vEmpleados = nRotacion.ObtieneEmpleadosIndiceRotacion(pFECHA_INICIO: vFechaInicio, pFECHA_FINAL: vFechaFinal, pTIPO_REPORTE: vTipoReporte, pXML_FILTROS: pFiltros, pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol);
            rgEmpleadosGrafica.DataSource = vEmpleados;
            rgEmpleadosGrafica.DataBind();
        }

        protected void PintaGraficaIndiceRotacion(List<E_GRAFICA_ROTACION> pGraficaIndice)
        {

            rhlIndiceRotacion.PlotArea.Series.Clear();
            rhlIndiceRotacion.PlotArea.XAxis.Items.Clear();
            LineSeries vSeries = new LineSeries();

            foreach (var item in pGraficaIndice)
            {
                vSeries.SeriesItems.Add(item.NO_CANTIDAD);
                vSeries.LabelsAppearance.Visible = false;
                vSeries.MarkersAppearance.Visible = true;
                rhlIndiceRotacion.PlotArea.XAxis.Items.Add(item.FECHA.Value.ToShortDateString().ToString());
                //rhlIndiceRotacion.PlotArea.XAxis.LabelsAppearance.ClientTemplate = item.FECHA.ToString();
                rhlIndiceRotacion.PlotArea.XAxis.LabelsAppearance.RotationAngle = 270;
               // rhlIndiceRotacion.PlotArea.XAxis.LabelsAppearance.DataFormatString = "dd/MM/yyyy";
                //rhlIndiceRotacion.PlotArea.XAxis.DataLabelsField = item.FECHA.ToString();
                // rhlIndiceRotacion.PlotArea.XAxis.Items.Add(item.FECHA.ToString());
            }
            rhlIndiceRotacion.PlotArea.Series.Add(vSeries);

            List<E_PORCENTAJE_EMPLEADOS> vlstPorcentanjes = new List<E_PORCENTAJE_EMPLEADOS>();
            var vPorcentajes = pGraficaIndice.FirstOrDefault();

            vlstPorcentanjes.Add(new E_PORCENTAJE_EMPLEADOS { NB_CANTIDAD = "Bajas en el período", PR_CANTIDAD = vPorcentajes.NO_EMPLEADOS_BAJA.ToString() });
            vlstPorcentanjes.Add(new E_PORCENTAJE_EMPLEADOS { NB_CANTIDAD = "Personal en inventario", PR_CANTIDAD = vPorcentajes.NO_EMPLEADOS_ALTA.ToString() });
            vlstPorcentanjes.Add(new E_PORCENTAJE_EMPLEADOS { NB_CANTIDAD = "Índice de rotación", PR_CANTIDAD = vPorcentajes.PR_TOTAL_BAJA + "%" });

            rgGraficaIndiceRotacion.DataSource = vlstPorcentanjes;
            rgGraficaIndiceRotacion.DataBind();
            GenararItemDataBound();

            //rtIndice.SelectedIndex = 2;
            //rpvGraficaIndiceRotacion.Selected = true;
        }

        protected void GenararItemDataBound()
        {
            int strId = 0;

            foreach (GridDataItem dataItem in rgGraficaIndiceRotacion.Items)
           {
                strId = int.Parse(dataItem.ItemIndex.ToString());

                if (strId % 2 == 0)
                    dataItem.CssClass = "RadGrid2Class";
                else dataItem.CssClass = "RadGrid1Class";
            }
        }

        protected void GenararItemDataBoundCausas()
        {
            int strId = 0;

            foreach (GridDataItem dataItem in rgGraficaCausaRotacion.Items)
            {
                strId = int.Parse(dataItem.ItemIndex.ToString());

                if (strId % 2 == 0)
                    dataItem.CssClass = "RadGrid2Class";
                else dataItem.CssClass = "RadGrid1Class";
            }
        }

        protected void btnGraficaIndiceRotacion_Click(object sender, EventArgs e)
        {
            FiltrosIndiceRotacion();

        }

        //public void FiltrosCausaRotacion()
        //{
        //    List<E_SELECCIONADOS> vDepartamentos = new List<E_SELECCIONADOS>();
        //    XElement vXlmFiltros = new XElement("FILTROS");
        //    XElement vXlmDepartamentos = new XElement("DEPARTAMENTOS");
        //    XElement vXlmGeneros = new XElement("GENEROS");
        //    XElement vXlmEdad = new XElement("EDAD");
        //    XElement vXlmAntiguedad = new XElement("ANTIGUEDAD");
        //    foreach (RadListBoxItem item in rlbDepartamentoCausa.Items)
        //    {
        //        int vIdDepartamento = int.Parse(item.Value);
        //        vDepartamentos.Add(new E_SELECCIONADOS { ID_SELECCIONADO = vIdDepartamento });
        //    }
        //    var vXelements = vDepartamentos.Select(x => new XElement("DEPARTAMENTO", new XAttribute("ID_DEPARTAMENTO", x.ID_SELECCIONADO)));
        //    vXlmDepartamentos = new XElement("DEPARTAMENTOS", vXelements);
        //    vXlmFiltros.Add(vXlmDepartamentos);

        //    foreach (RadListBoxItem item in rlbGeneroCausa.Items)
        //    {
        //        vXlmGeneros = new XElement("GENERO", new XAttribute("NB_GENERO", item.Value));
        //    }
        //    vXlmFiltros.Add(vXlmGeneros);

        //    if (rbFiltroEdadCausa.Checked == true)
        //    {
        //        vXlmEdad = new XElement("EDAD", new XAttribute("EDAD_INICIAL", rntEdadInicialCausa.Text), new XAttribute("EDAD_FINAL", rntEdadFinalCausa.Text));
        //        vXlmFiltros.Add(vXlmEdad);
        //    }
        //    if (rbFiltroAntiguedadCausa.Checked == true)
        //    {
        //        vXlmAntiguedad = new XElement("ANTIGUEDAD", new XAttribute("ANTIGUEDAD_INICIAL", rnAntiguedadInicialCausa.Text), new XAttribute("ANTIGUEDAD_FINAL", rnAntiguedadFinalCausa.Text));
        //        vXlmFiltros.Add(vXlmAntiguedad);
        //    }
        //    GraficaCausaRotacion(vXlmFiltros);
        //}

        protected void GraficaCausaRotacion(XElement pFiltros)
        {
            DateTime? vFechaInicio = rdpFechaInicio.SelectedDate;
            DateTime? vFechaFinal = rdpFechaFin.SelectedDate;

            RotacionPersonalNegocio nRotacion = new RotacionPersonalNegocio();
            List<E_GRAFICA_ROTACION> vGraficaCausaRotacion = nRotacion.ObtieneGraficaCausaRotacion(pFECHA_INICIO: vFechaInicio, pFECHA_FINAL: vFechaFinal, pXML_FILTROS: pFiltros, pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol).Select(s => new E_GRAFICA_ROTACION { NB_CAUSA = s.NB_CATALOGO_VALOR, NO_CANTIDAD = s.NO_CANTIDAD, PR_CANTIDAD = s.PR_CANTIDAD, NO_EMPLEADOS_ALTA = s.NO_EMPLEADOS_ALTA, NO_EMPLEADOS_BAJA = s.NO_EMPLEADOS_BAJA, PR_TOTAL_BAJA = s.PR_TOTAL_BAJA }).ToList();
            PintaGraficaCausaRotacion(vGraficaCausaRotacion);
        }

        protected void PintaGraficaCausaRotacion(List<E_GRAFICA_ROTACION> plstPorcentajes)
        {
            PieSeries vSerie = new PieSeries();
            foreach (var item in plstPorcentajes)
            {
                System.Drawing.Color vColor = new System.Drawing.Color();
                vSerie.SeriesItems.Add(item.PR_CANTIDAD, vColor, item.NB_CAUSA, false, true);
                vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
                vSerie.TooltipsAppearance.Visible = false;
                vSerie.Name = item.NB_CAUSA;
            }
            rhcGraficaCausasRotacion.PlotArea.Series.Add(vSerie);

            List<E_PORCENTAJE_EMPLEADOS> vlstPorcentanjes = new List<E_PORCENTAJE_EMPLEADOS>();
            var vPorcentajes = plstPorcentajes.FirstOrDefault();

            //0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
            vlstPorcentanjes.Add(new E_PORCENTAJE_EMPLEADOS { NB_CANTIDAD = "Bajas en el período", PR_CANTIDAD = vPorcentajes != null ? vPorcentajes.NO_EMPLEADOS_BAJA.ToString() : "0" });
            vlstPorcentanjes.Add(new E_PORCENTAJE_EMPLEADOS { NB_CANTIDAD = "Personal en inventario", PR_CANTIDAD = vPorcentajes != null ? vPorcentajes.NO_EMPLEADOS_ALTA.ToString() : "0" });
            vlstPorcentanjes.Add(new E_PORCENTAJE_EMPLEADOS { NB_CANTIDAD = "Indice de rotación", PR_CANTIDAD = vPorcentajes != null ? vPorcentajes.PR_TOTAL_BAJA.ToString() + "%" : "0.00%" });

            if (vlstPorcentanjes != null)
            {
                rgGraficaCausaRotacion.DataSource = vlstPorcentanjes;
                rgGraficaCausaRotacion.DataBind();
                GenararItemDataBoundCausas();
            }

        }

        //protected void rgBajasPendientes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        //{
        //    RotacionPersonalNegocio nRotacion = new RotacionPersonalNegocio();
        //    rgBajasPendientes.DataSource = nRotacion.ObtieneBajasPendientes();
        //}

        //protected void btnCancelarBaja_Click(object sender, EventArgs e)
        //{
        //    EmpleadoNegocio nEmpleados = new EmpleadoNegocio();
        //    GridDataItem itemId = (GridDataItem)rgBajasPendientes.SelectedItems[0];
        //    int vIdEmpleado = (int.Parse(itemId.GetDataKeyValue("ID_EMPLEADO").ToString()));
        //    E_RESULTADO vResultado = nEmpleados.CancelaBajaEmpleado(vIdEmpleado, vClUsuario, vNbPrograma);
        //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, null);
        //    rgBajasPendientes.Rebind();
        //}

        protected void grdHistorialBaja_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        //protected void rgBajasPendientes_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridPagerItem)
        //    {
        //        RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

        //        PageSizeCombo.Items.Clear();
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
        //        PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgBajasPendientes.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
        //        PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgBajasPendientes.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
        //        PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgBajasPendientes.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
        //        PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgBajasPendientes.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
        //        PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgBajasPendientes.MasterTableView.ClientID);
        //        PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
        //    }
        //}

        protected void grdHistorialBaja_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdHistorialBaja.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void rgEmpleadosGrafica_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int strId = 0;


            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                strId = int.Parse(dataItem.ItemIndex.ToString());

                if (strId % 2 == 0)
                    dataItem.CssClass = "RadGrid2Class";
                else dataItem.CssClass = "RadGrid1Class";
            }
        }

       
    }
}