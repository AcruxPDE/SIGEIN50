using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Utilerias;
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

namespace SIGE.WebApp.EO
{
    public partial class VentanaControlAvanceDesempeno : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int? vIdRol;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vcd_id_periodo"]; }
            set { ViewState["vs_vcd_id_periodo"] = value; }
        }

        #endregion

        #region Funciones

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);

            if (foundEl != null)
            {
                return true;
            }
            return false;
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
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {
                if (Request.Params["IdPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());

                    PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
                    var vPeriodoDesempeno = nDesempeno.ObtienePeriodoDesempenoContexto(vIdPeriodo, null);

                    if (vPeriodoDesempeno != null)
                    {
                        CargarDatosContexto();
                    }
                }
            }
        }

        protected void grdControlAvance_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nDesempeno = new PeriodoDesempenoNegocio();
            grdControlAvance.DataSource = nDesempeno.ObtieneControlAvanceDesempeno(vIdPeriodo, pIdRol: vIdRol);
        }

        protected void btnGuardarPonderacion_Click(object sender, EventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();

            XElement vXmlEvaluadores = new XElement("EVALUADOS");
            List<E_PONDERACION_META> vListaPonderacion = new List<E_PONDERACION_META>();
            foreach (GridDataItem item in grdControlAvance.MasterTableView.Items)
            {
                
                int vIdEvaluado = Convert.ToInt32(item.GetDataKeyValue("ID_EVALUADO").ToString());                
                RadNumericTextBox txtPonderacion = (RadNumericTextBox)item.FindControl("txtPorcentajeEvaluado");
                decimal vPrPonderacion = txtPonderacion.Text != "" ? Convert.ToDecimal(txtPonderacion.Value) : 0;

                E_PONDERACION_META ponderacionM = new E_PONDERACION_META { ID_EVALUADO = vIdEvaluado, PR_PONDERACION = vPrPonderacion };

                vListaPonderacion.Add(ponderacionM);

                var vXelements = vListaPonderacion.Select(x =>
                                      new XElement("EVALUADO",
                                      new XAttribute("ID_EVALUADO", x.ID_EVALUADO),
                                      new XAttribute("PR_EVALUADO", x.PR_PONDERACION)));

                vXmlEvaluadores.Add(vXelements);
            }

            decimal sumaPonderacion = 0;
            foreach (E_PONDERACION_META item in vListaPonderacion)
            {
                sumaPonderacion = sumaPonderacion + Convert.ToDecimal(item.PR_PONDERACION);
            }

            decimal totalPonderacion = sumaPonderacion;
            if (totalPonderacion > 100 || totalPonderacion < 100)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "La suma de la ponderación debe ser igual a 100", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            var evaluadosEvaluador = nPeriodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo);
            foreach (var evaluador in evaluadosEvaluador)
            {
                int? ev = evaluador.NO_EVALUADOR;
                string nb = evaluador.NB_EMPLEADO_COMPLETO;
                if (ev == 0)
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "El evaluado " + nb + " no tiene evaluador", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                    return;
                }
            }

            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = neg.ActualizaPonderacionEvaluados(vIdPeriodo, vXmlEvaluadores.ToString(), "ACTUALIZA", vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            grdControlAvance.Rebind();
        }

        protected void grdControlAvance_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdControlAvance.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdControlAvance.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdControlAvance.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdControlAvance.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdControlAvance.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}