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

namespace SIGE.WebApp.EO
{
    public partial class VentanaCumplimientoGlobal : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

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
                vSerie.SeriesItems.Add(vNoCumplido, System.Drawing.Color.Red, "No Cumplido");
                vSerie.SeriesItems.Add(vPrDesempenoGlobal.CUMPLIDO, System.Drawing.Color.Green, "Cumplido");
                vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
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


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
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
            lDesempenoGlobal = nDesempenoGlobal.ObtieneCumplimientoGlobal(vIdPeriodo);
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

    }
}