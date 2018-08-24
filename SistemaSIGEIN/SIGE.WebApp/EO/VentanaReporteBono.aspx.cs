using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.EvaluacionOrganizacional;
using Newtonsoft.Json;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.WebApp.FYD;
using Telerik.Web.UI;
using SIGE.WebApp.IDP;
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class ReporteBono : System.Web.UI.Page
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

                    ContextoPeriodos.oLstPeriodosBonos.Add(new E_SELECCION_PERIODOS_DESEMPENO
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

        private void CargarDatosContexto()
        {
            PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
            var oPeriodo = periodo.ObtienePeriodoDesempenoContexto(vIdPeriodo, null);
            if (oPeriodo != null)
            {
                txtClPeriodo.InnerText = oPeriodo.CL_PERIODO;
               // txtNbPeriodo.InnerText = oPeriodo.NB_PERIODO;
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
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["PeriodoId"]);
                    PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                    var vPeriodo = nPeriodo.ObtienePeriodoDesempeno(pIdPeriodo: vIdPeriodo);

                    if (vPeriodo != null)
                    {
                        CargarDatosContexto();
                    }
                    //txtNbPeriodo.InnerText = vPeriodo.NB_PERIODO;
                    //txtFechaDel.InnerText = vPeriodo.FE_INICIO.ToString("d") + " a " + vPeriodo.FE_TERMINO.Value.ToShortDateString();

                    //if (vPeriodo.DS_NOTAS != null)
                    //{
                    //    XElement vNotas = XElement.Parse(vPeriodo.DS_NOTAS);
                    //    if (vNotas != null)
                    //    {
                    //        txtNotas.InnerHtml = validarDsNotas(vNotas.ToString());
                    //    }
                    //}

                    //if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "PREDEFINIDO")
                    //    txtTipoPeriodo.InnerText = "Predefinido";
                    //if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "REPLICA")
                    //    txtTipoPeriodo.InnerText = "Replica";
                    //if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "COPIA")
                    //    txtTipoPeriodo.InnerText = "Copia";

                    //if (vPeriodo.CL_TIPO_BONO == "INDEPENDIENTE")
                    //    txtTipoBono.InnerText = "Independiente";
                    //if (vPeriodo.CL_TIPO_BONO == "DEPENDIENTE")
                    //    txtTipoBono.InnerText = "Dependiente";
                    //if (vPeriodo.CL_TIPO_BONO == "GRUPAL")
                    //    txtTipoBono.InnerText = "Grupal";

                    //if (vPeriodo.CL_TIPO_CAPTURISTA == "COORDINADOR_EVAL")
                    //    txtCapturista.InnerText = "Coordinador de evaluación";
                    //if (vPeriodo.CL_TIPO_CAPTURISTA == "OCUPANTE_PUESTO")
                    //    txtCapturista.InnerText = "Ocupante del puesto";
                    //if (vPeriodo.CL_TIPO_CAPTURISTA == "JEFE_INMEDIATO")
                    //    txtCapturista.InnerText = "Jefe inmediato";
                    //if (vPeriodo.CL_TIPO_CAPTURISTA == "OTRO")
                    //    txtCapturista.InnerText = "Otro";

                    ContextoPeriodos.oLstPeriodosBonos = new List<E_SELECCION_PERIODOS_DESEMPENO>();
                    ContextoPeriodos.oLstPeriodosBonos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = vIdPeriodo,
                        clPeriodo = vPeriodo.CL_PERIODO,
                        nbPeriodo = vPeriodo.NB_PERIODO,
                        dsPeriodo = vPeriodo.DS_PERIODO,
                        dsNotas = vPeriodo.DS_NOTAS,
                        feInicio = vPeriodo.FE_INICIO.ToString(),
                        feTermino = vPeriodo.FE_TERMINO.ToString()
                    });

                    oLstPeriodos = new List<E_SELECCION_PERIODOS_DESEMPENO>();
                    string vOrigenPeriodo;
                    if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "COPIA")
                        vOrigenPeriodo = vPeriodo.CL_ORIGEN_CUESTIONARIO + " " + vPeriodo.CL_TIPO_COPIA;
                    else
                        vOrigenPeriodo = vPeriodo.CL_ORIGEN_CUESTIONARIO;



                    oLstPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
                    {
                        idPeriodo = vIdPeriodo,
                        clPeriodo = vPeriodo.CL_PERIODO,
                        nbPeriodo = vPeriodo.NB_PERIODO,
                        dsPeriodo = vPeriodo.DS_PERIODO,
                        clOrigen = vOrigenPeriodo.ToLower()
                    });

                }
                else
                {
                    vIdPeriodo = 0;
                }

            }
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            //PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            //E_RESULTADO vResultado = neg.InsertaActualizaBono(vIdPeriodo, vClUsuario, vNbPrograma);
            //string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        }

        protected void grdEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            grdEvaluados.DataSource = nPeriodo.ObtieneEvaluados(vIdPeriodo);
        }

        protected void rgComparativos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgComparativos.DataSource = oLstPeriodos;
        }

        protected void ramPeriodos_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "PERIODODESEMPENO")
                AgregarPeriodos(vSeleccion.oSeleccion.ToString());
        }

        protected void grdEvaluados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
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
                ContextoPeriodos.oLstPeriodosBonos.RemoveAll(w => w.idPeriodo == vIdPeriodo);
                rgComparativos.Rebind();
            }
        }

    }
}