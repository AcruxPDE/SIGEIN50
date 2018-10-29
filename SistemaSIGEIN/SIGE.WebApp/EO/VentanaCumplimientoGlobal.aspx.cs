using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using SIGE.Negocio.Utilerias;
using Newtonsoft.Json;
using WebApp.Comunes;
using SIGE.WebApp.Comunes;
using SIGE.Entidades.MetodologiaCompensacion;

namespace SIGE.WebApp.EO
{
    public partial class VentanaCumplimientoGlobal : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int? vIdRol;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }

        public List<E_SELECCION_PERIODOS_DESEMPENO> oLstPeriodos
        {
            get { return (List<E_SELECCION_PERIODOS_DESEMPENO>)ViewState["vs_lista_periodos"]; }
            set { ViewState["vs_lista_periodos"] = value; }
        }

        #endregion

        #region Funciones

        protected void GraficaDesempenoGlobal()
        {
            PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
            var vPrDesempenoGlobal = nDesempeno.ObtieneCumplimientoGlobal(vIdPeriodo).FirstOrDefault();
            PieSeries vSerie = new PieSeries();
            if (vPrDesempenoGlobal != null)
            {
                decimal? vNoCumplido = 100 - vPrDesempenoGlobal.CUMPLIDO;
                
                vSerie.SeriesItems.Add(vNoCumplido, System.Drawing.Color.Red, "No cumplido");
                vSerie.SeriesItems.Add(vPrDesempenoGlobal.CUMPLIDO, System.Drawing.Color.Green, "Cumplido");
                vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
                vSerie.LabelsAppearance.TextStyle.FontSize = 14;
                rhcGraficaGlobal.Legend.Appearance.TextStyle.FontSize = 15;
                rhcGraficaGlobal.PlotArea.Series.Add(vSerie);
            }
            else
            {
                rhcGraficaGlobal.Visible = false;
            }
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

                if (pNota.DS_NOTA != null)
                {
                    return pNota.DS_NOTA.ToString();
                }
                else return "";
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

        private void AgregarPeriodos(string pPeriodos)
        {
            List<E_SELECCION_PERIODOS_DESEMPENO> vLstPeriodos = JsonConvert.DeserializeObject<List<E_SELECCION_PERIODOS_DESEMPENO>>(pPeriodos);
            string vOrigen;

            foreach (E_SELECCION_PERIODOS_DESEMPENO item in vLstPeriodos)
            {
                if (item.clOrigen == "COPIA")
                    vOrigen = item.clOrigen + " " + item.clTipoCopia;
                if (item.clOrigen == "PREDEFINIDO")
                    vOrigen = "Original";
                else if (item.clOrigen == "REPLICA")
                    vOrigen = "réplica";
                else
                    vOrigen = item.clOrigen;

                if (oLstPeriodos.Where(t => t.idPeriodo == item.idPeriodo).Count() == 0)
                {
                    oLstPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = item.idPeriodo,
                        clPeriodo = item.clPeriodo,
                        nbPeriodo = item.nbPeriodo,
                        dsPeriodo = item.dsPeriodo,
                        clOrigen = vOrigen.ToLower()
                    });

                    ContextoPeriodos.oLstPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = item.idPeriodo,
                        clPeriodo = item.clPeriodo,
                        nbPeriodo = item.nbPeriodo,
                        dsPeriodo = item.dsPeriodo,
                        dsNotas = item.dsNotas,
                        feInicio = item.feInicio,
                        feTermino = item.feTermino
                    });

                }
            }
            rgComparativos.Rebind();
        }

        private Color ObtieneColorCumplimiento(decimal? pPrCumplimiento)
        {
            Color vColor = System.Drawing.ColorTranslator.FromHtml("#F2F2F2");

            if (pPrCumplimiento > 0 && pPrCumplimiento < 60)
                vColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            else if (pPrCumplimiento > 59 && pPrCumplimiento < 76)
                vColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");
            else if (pPrCumplimiento > 75 && pPrCumplimiento < 101)
                vColor = System.Drawing.ColorTranslator.FromHtml("#00B050");

            return vColor;
        }

        private List<E_CODIGO_COLORES> CodigosColores()
        {
            List<E_CODIGO_COLORES> vCodigoColores = new List<E_CODIGO_COLORES>();
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "#F2F2F2", DESCRIPCION = "No calificada" });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "#FF0000", DESCRIPCION = "No alcanzada" });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "#FFFF00", DESCRIPCION = "Mínimo" });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "#0070C0", DESCRIPCION = "Satisfactorio" });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "#00B050", DESCRIPCION = "Sobresaliente" });
            return vCodigoColores;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            if (!IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoId"]);
                    PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();

                    var oPeriodo = periodo.ObtienePeriodoDesempenoContexto(vIdPeriodo, null);

                    if (oPeriodo != null)
                    {
                        txtClPeriodo.InnerText = oPeriodo.CL_PERIODO;
                        //txtNbPeriodo.InnerText = oPeriodo.NB_PERIODO;
                        txtPeriodos.InnerText = oPeriodo.DS_PERIODO;
                        txtFechas.InnerText = oPeriodo.FE_INICIO.ToString("d") + " a " + oPeriodo.FE_TERMINO.Value.ToShortDateString();
                        txtTipoMetas.InnerText = oPeriodo.CL_TIPO_PERIODO;
                        txtTipoCapturista.InnerText = Utileria.LetrasCapitales(oPeriodo.CL_TIPO_CAPTURISTA);

                        if (oPeriodo.FG_BONO == true && oPeriodo.FG_MONTO == true)
                            txtTipoBono.InnerText = oPeriodo.CL_TIPO_BONO + " (monto)";
                        else if (oPeriodo.FG_BONO == true && oPeriodo.FG_PORCENTUAL == true)
                            txtTipoBono.InnerText = oPeriodo.CL_TIPO_BONO + " (porcentual)";
                        else
                            txtTipoBono.InnerText = oPeriodo.CL_TIPO_BONO;

                        txtTipoPeriodo.InnerText = oPeriodo.CL_ORIGEN_CUESTIONARIO;

                        if (oPeriodo.DS_NOTAS != null)
                        {
                            XElement vNotas = XElement.Parse(oPeriodo.DS_NOTAS);
                            if (vNotas != null)
                            {
                                txtNotas.InnerHtml = validarDsNotas(vNotas.ToString());
                            }
                        }
                    }

                    ContextoPeriodos.oLstPeriodos = new List<E_SELECCION_PERIODOS_DESEMPENO>();

                    ContextoPeriodos.oLstPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = vIdPeriodo,
                        clPeriodo = oPeriodo.CL_PERIODO,
                        nbPeriodo = oPeriodo.NB_PERIODO,
                        dsPeriodo = oPeriodo.DS_PERIODO,
                        dsNotas = oPeriodo.DS_NOTAS,
                        feInicio = oPeriodo.FE_INICIO.ToString(),
                        feTermino = oPeriodo.FE_TERMINO.ToString()
                    });

                    oLstPeriodos = new List<E_SELECCION_PERIODOS_DESEMPENO>();
                    string vOrigenPeriodo;
                    if (oPeriodo.CL_ORIGEN_CUESTIONARIO == "Copia")
                        vOrigenPeriodo = oPeriodo.CL_ORIGEN_CUESTIONARIO + " "+ oPeriodo.CL_TIPO_COPIA;
                    if (oPeriodo.CL_ORIGEN_CUESTIONARIO == "PREDEFINIDO")
                        vOrigenPeriodo = "Original";
                    else if (oPeriodo.CL_ORIGEN_CUESTIONARIO == "REPLICA")
                        vOrigenPeriodo = "Réplica";
                    else
                        vOrigenPeriodo = oPeriodo.CL_ORIGEN_CUESTIONARIO;


                    oLstPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = vIdPeriodo,
                        clPeriodo = oPeriodo.CL_PERIODO,
                        nbPeriodo = oPeriodo.NB_PERIODO,
                        dsPeriodo = oPeriodo.DS_PERIODO,
                        clOrigen = vOrigenPeriodo.ToLower()
                    });
                }

                GraficaDesempenoGlobal();
            }
        }

        protected void rgEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nDesempenoGlobal = new PeriodoDesempenoNegocio();
            List<E_OBTIENE_CUMPLIMIENTO_GLOBAL> lDesempenoGlobal = new List<E_OBTIENE_CUMPLIMIENTO_GLOBAL>();
            lDesempenoGlobal = nDesempenoGlobal.ObtieneCumplimientoGlobal(vIdPeriodo, vIdRol);
            rgEvaluados.DataSource = lDesempenoGlobal;
        }

        protected void rgComparativos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgComparativos.DataSource = oLstPeriodos;
        }

        protected void ramReportes_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "PERIODODESEMPENO")
                AgregarPeriodos(vSeleccion.oSeleccion.ToString());
        }

        protected void rgComparativos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgComparativos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void rgComparativos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                int? vIdPeriodo = int.Parse(item.GetDataKeyValue("idPeriodo").ToString());
                oLstPeriodos.RemoveAll(w => w.idPeriodo == vIdPeriodo);
                ContextoPeriodos.oLstPeriodos.RemoveAll(w => w.idPeriodo == vIdPeriodo);
                rgComparativos.Rebind();

            }
        }

        protected void rgEvaluados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFooterItem)
            {
                
                GridFooterItem footerItem = (GridFooterItem)e.Item;
                PeriodoDesempenoNegocio nDesempenoGlobal = new PeriodoDesempenoNegocio();
                var vDesempenoGlobal = nDesempenoGlobal.ObtieneCumplimientoGlobal(vIdPeriodo, vIdRol).Sum(s => s.C_GENERAL);
                footerItem["C_GENERAL"].BackColor = ObtieneColorCumplimiento(decimal.Parse(vDesempenoGlobal.ToString()));
                         
            }

        }

        protected void grdCodigoColores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdCodigoColores.DataSource = CodigosColores();
        }

        protected void rgColores2_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgColores2.DataSource = CodigosColores();
        }

    }
}