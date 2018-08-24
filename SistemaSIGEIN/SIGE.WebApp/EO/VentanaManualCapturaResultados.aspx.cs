using SIGE.Entidades;
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

namespace SIGE.WebApp.EO
{
    public partial class VentanaManualCapturaResultados : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private decimal vMontoMnInd;
        private decimal vMontoMnDep;
        private decimal vMontoGrup;
        private XElement RESULTADOS { get; set; }
        private XElement RESULTADOSBONOS { get; set; }

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();

        private List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result> vResultados
        {
            get { return (List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result>)ViewState["vs_vResultados"]; }
            set { ViewState["vs_vResultados"] = value; }
        }
        public int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }
        public int vsIdEvaluado
        {
            get { return (int)ViewState["vsIdEvaluado"]; }
            set { ViewState["vsIdEvaluado"] = value; }
        }
        #endregion

        #region Funciones

        private decimal ValidarRangoDeMetaPorcentual(decimal valorMeta, E_RESULTADO_META pMeta)
        {
            decimal resultado;
            pMeta.RESULTADO = valorMeta.ToString();
            if (Convert.ToDecimal(pMeta.MINIMO) < Convert.ToDecimal(pMeta.SOBRESALIENTE))
            {
                if (valorMeta < Convert.ToDecimal(pMeta.MINIMO))
                {
                    pMeta.CL_META = 1;
                    pMeta.RANGOVALOR = 0;
                }
                else if (valorMeta >= Convert.ToDecimal(pMeta.MINIMO) && valorMeta < Convert.ToDecimal(pMeta.SATISFACTORIO))
                {
                    pMeta.CL_META = 2;
                    pMeta.RANGOVALOR = 50;
                }
                else if (valorMeta >= Convert.ToDecimal(pMeta.SATISFACTORIO) && valorMeta < Convert.ToDecimal(pMeta.SOBRESALIENTE))
                {
                    pMeta.CL_META = 3;
                    pMeta.RANGOVALOR = 75;
                }
                else if (valorMeta >= Convert.ToDecimal(pMeta.SOBRESALIENTE))
                {
                    pMeta.CL_META = 4;
                    pMeta.RANGOVALOR = 100;
                }
            }
            else if (Convert.ToDecimal(pMeta.MINIMO) > Convert.ToDecimal(pMeta.SOBRESALIENTE))
            {
                if (valorMeta > Convert.ToDecimal(pMeta.MINIMO))
                {
                    pMeta.CL_META = 1;
                    pMeta.RANGOVALOR = 0;
                }
                else if (valorMeta <= Convert.ToDecimal(pMeta.MINIMO) && valorMeta > Convert.ToDecimal(pMeta.SATISFACTORIO))
                {
                    pMeta.CL_META = 2;
                    pMeta.RANGOVALOR = 50;
                }
                else if (valorMeta <= Convert.ToDecimal(pMeta.SATISFACTORIO) && valorMeta > Convert.ToDecimal(pMeta.SOBRESALIENTE))
                {
                    pMeta.CL_META = 3;
                    pMeta.RANGOVALOR = 75;
                }
                else if (valorMeta <= Convert.ToDecimal(pMeta.SOBRESALIENTE))
                {
                    pMeta.CL_META = 4;
                    pMeta.RANGOVALOR = 100;
                }
            }

            if (Convert.ToDecimal(pMeta.MINIMO) == Convert.ToDecimal(pMeta.SOBRESALIENTE))
            {
                if (valorMeta < Convert.ToDecimal(pMeta.MINIMO))
                {
                    pMeta.CL_META = 1;
                    pMeta.RANGOVALOR = 0;
                }

                if (valorMeta >= Convert.ToDecimal(pMeta.SOBRESALIENTE))
                {
                    pMeta.CL_META = 4;
                    pMeta.RANGOVALOR = 100;
                }
            }


            resultado = CalcularCumplimiento(pMeta);
            return resultado;
        }

        private decimal ValidarRangoDeMetaFecha(DateTime valorMeta, E_RESULTADO_META pMeta)
        {
            decimal resultado;
            pMeta.RESULTADO = valorMeta.ToShortDateString();
            if (Convert.ToDateTime(pMeta.MINIMO) < Convert.ToDateTime(pMeta.SATISFACTORIO))
            {
                if (valorMeta < Convert.ToDateTime(pMeta.MINIMO))
                {
                    pMeta.CL_META = 1;
                    pMeta.RANGOVALOR = 0;
                }
                else if (valorMeta >= Convert.ToDateTime(pMeta.MINIMO) && valorMeta < Convert.ToDateTime(pMeta.SATISFACTORIO))
                {
                    pMeta.CL_META = 2;
                    pMeta.RANGOVALOR = 50;
                }
                else if (valorMeta >= Convert.ToDateTime(pMeta.SATISFACTORIO) && valorMeta < Convert.ToDateTime(pMeta.SOBRESALIENTE))
                {
                    pMeta.CL_META = 3;
                    pMeta.RANGOVALOR = 75;
                }
                else if (valorMeta >= Convert.ToDateTime(pMeta.SOBRESALIENTE))
                {
                    pMeta.CL_META = 4;
                    pMeta.RANGOVALOR = 100;
                }
            }
            else if (Convert.ToDateTime(pMeta.MINIMO) > Convert.ToDateTime(pMeta.SATISFACTORIO))
            {
                if (valorMeta > Convert.ToDateTime(pMeta.MINIMO))
                {
                    pMeta.CL_META = 1;
                    pMeta.RANGOVALOR = 0;
                }
                else if (valorMeta <= Convert.ToDateTime(pMeta.MINIMO) && valorMeta > Convert.ToDateTime(pMeta.SATISFACTORIO))
                {
                    pMeta.CL_META = 2;
                    pMeta.RANGOVALOR = 50;
                }
                else if (valorMeta <= Convert.ToDateTime(pMeta.SATISFACTORIO) && valorMeta > Convert.ToDateTime(pMeta.SOBRESALIENTE))
                {
                    pMeta.CL_META = 3;
                    pMeta.RANGOVALOR = 75;
                }
                else if (valorMeta <= Convert.ToDateTime(pMeta.SOBRESALIENTE))
                {
                    pMeta.CL_META = 4;
                    pMeta.RANGOVALOR = 100;
                }
            }

            resultado = CalcularCumplimiento(pMeta);
            return resultado;
        }

        private decimal CalcularCumplimiento(E_RESULTADO_META pMeta)
        {
            decimal resultado = 0;
            resultado = pMeta.PONDERACION * pMeta.RANGOVALOR / 100;

            return resultado;
        }

        private decimal CalcularCumplimientoPuesto(decimal sumaCumplimiento)
        {
            decimal resultado = 0;
            decimal resul = 0;
            resul = Convert.ToDecimal(txtPonderacion.Text) * sumaCumplimiento / 100;

            if (resul != 0)
            {
                resultado = (100 / Convert.ToDecimal(txtPonderacion.Text)) * resul;    
            }
            
            return resultado;
        }

        private bool ValidaConfiguracionMeta( string vCumplimientoMin, string vCumplimientoSatisfactorio, string vCumplimientoSobresaliente)
        {
            bool vFgMetaConfigurada = false;

            if (vCumplimientoMin != "" && vCumplimientoSatisfactorio != "" && vCumplimientoSobresaliente != "")
                {
                    vFgMetaConfigurada = true;
                }

            return vFgMetaConfigurada;
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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.Params["idPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["idPeriodo"]);

                }
                else
                {
                    vIdPeriodo = 0;
                }
                if (Request.Params["idEvaluado"] != null)
                {
                    vsIdEvaluado = int.Parse(Request.QueryString["idEvaluado"]);

                }
                else
                {
                    vsIdEvaluado = 0;
                }

                var oPeriodo = periodo.ObtienePeriodoDesempenoContexto(pIdPeriodo: vIdPeriodo, idEvaluado: vsIdEvaluado);

                if (oPeriodo != null)
                {

                    txtNbPeriodo.InnerText = oPeriodo.DS_PERIODO;
                    txtFechaDel.InnerText = oPeriodo.FE_INICIO.ToString("d") + " a " + oPeriodo.FE_TERMINO.Value.ToShortDateString();
                    if (oPeriodo.DS_NOTAS != null)
                    {
                        XElement vNotas = XElement.Parse(oPeriodo.DS_NOTAS);
                        if (vNotas != null)
                        {
                            txtNotas.InnerHtml = validarDsNotas(vNotas.ToString());
                        }
                    }

                    txtTipoPeriodo.InnerText = oPeriodo.CL_TIPO_PERIODO;
                    txtTipoBono.InnerText = oPeriodo.CL_TIPO_BONO;
                    txtResponsable.InnerText = oPeriodo.CL_TIPO_CAPTURISTA;
                    txtPonderacion.Text = oPeriodo.PR_EVALUADO.ToString();
                }
                var oEvaluado = periodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo, pIdEvaluado: vsIdEvaluado).FirstOrDefault();
                if (oEvaluado != null)
                {
                    txtEmpleado.InnerText = oEvaluado.ID_EVALUADO + " " + oEvaluado.NB_EMPLEADO_COMPLETO;
                }
            }
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";
            vNbPrograma = ContextoUsuario.nbPrograma;
            vMontoMnInd = ContextoApp.EO.Configuracion.BonoMinimoIndividualIndependiente;
            vMontoMnDep = ContextoApp.EO.Configuracion.BonoMinimoIndividualDependiente;
            vMontoGrup = ContextoApp.EO.Configuracion.BonoMinimoGrupal;

            var oEstatus = periodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo, pIdEvaluado: vsIdEvaluado).FirstOrDefault();
            if (vClUsuario == "INVITADO" && oEstatus.ESTATUS == "CALIFICADO")
                btnGuardar.Enabled = false;
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            decimal ponderacion = decimal.Parse(txtPonderacion.Text);
            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = neg.ActualizaPonderacionPuesto(ponderacion, vIdPeriodo, vsIdEvaluado, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, 400, 200, pCallBackFunction: "");

            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            List<E_RESULTADO_META> metas = new List<E_RESULTADO_META>();
            foreach (GridDataItem item in grdResultados.MasterTableView.Items)
            {
                int id_evaluado_meta = Convert.ToInt32(item.GetDataKeyValue("ID_EVALUADO_META").ToString());
                string cl_tipo_meta = (item.GetDataKeyValue("CL_TIPO_META").ToString());
                decimal ponderacion = Convert.ToDecimal(item.GetDataKeyValue("PR_EVALUADO").ToString());
                string minimo = ((item.GetDataKeyValue("NB_CUMPLIMIENTO_MINIMO")) != null) ? (item.GetDataKeyValue("NB_CUMPLIMIENTO_MINIMO").ToString()) : "";
                string satisfactorio = ((item.GetDataKeyValue("NB_CUMPLIMIENTO_SATISFACTORIO")) != null) ? (item.GetDataKeyValue("NB_CUMPLIMIENTO_SATISFACTORIO").ToString()) : "";
                string sobresaliente = ((item.GetDataKeyValue("NB_CUMPLIMIENTO_SOBRESALIENTE")) != null) ? (item.GetDataKeyValue("NB_CUMPLIMIENTO_SOBRESALIENTE").ToString()) : "";

                E_RESULTADO_META meta = new
                E_RESULTADO_META { ID_EVALUADO_META = id_evaluado_meta, CL_META = 0, PONDERACION = ponderacion, MINIMO = minimo, SATISFACTORIO = satisfactorio, SOBRESALIENTE = sobresaliente, CUMPLIMIENTO = 0, RANGOVALOR = 0, RESULTADO = String.Empty };

                if (cl_tipo_meta == "Porcentual")
                {
                    RadNumericTextBox txtResultadoPorcentual = (RadNumericTextBox)item.FindControl("txtResultadoPorcentual");
                    if (txtResultadoPorcentual.Text == String.Empty)
                    {
                        meta.CUMPLIMIENTO = 0;
                    }
                    else
                    {
                        meta.CUMPLIMIENTO = ValidarRangoDeMetaPorcentual(Convert.ToDecimal(txtResultadoPorcentual.Text), meta);
                    }

                }
                else if (cl_tipo_meta == "Cantidad")
                {
                    RadNumericTextBox txtResultadoMonto = (RadNumericTextBox)item.FindControl("txtResultadoMonto");
                    if (txtResultadoMonto.Text == String.Empty)
                    {
                        meta.CUMPLIMIENTO = 0;
                    }
                    else
                    {
                        meta.CUMPLIMIENTO = ValidarRangoDeMetaPorcentual(Convert.ToDecimal(txtResultadoMonto.Text), meta);
                    }
                }
                else if (cl_tipo_meta == "Fecha")
                {
                    RadDatePicker dtpResultaFecha = (RadDatePicker)item.FindControl("dtpResultaFecha");
                    if (dtpResultaFecha.SelectedDate.ToString() == String.Empty)
                    {
                        meta.CUMPLIMIENTO = 0;
                    }
                    else
                    {
                        DateTime fecha = Convert.ToDateTime(dtpResultaFecha.SelectedDate);
                        meta.CUMPLIMIENTO = ValidarRangoDeMetaFecha(Convert.ToDateTime(fecha), meta);
                    }


                }
                else if (cl_tipo_meta == "Si/No")
                {
                    RadComboBox cmbrResultadoSiNo = (RadComboBox)item.FindControl("cmbrResultadoSiNo");
                    if (cmbrResultadoSiNo.SelectedIndex == 0)
                    {
                        meta.RANGOVALOR = 0;
                        meta.CUMPLIMIENTO = 0;
                    }
                    else if (cmbrResultadoSiNo.SelectedIndex == 1)
                    {
                        meta.RANGOVALOR = 100;
                        meta.CUMPLIMIENTO = CalcularCumplimiento(meta);

                    }
                    else if (cmbrResultadoSiNo.SelectedIndex == 2)
                    {
                        meta.RANGOVALOR = 0;
                        meta.CUMPLIMIENTO = CalcularCumplimiento(meta);

                    }
                    meta.CL_META = Convert.ToInt32(cmbrResultadoSiNo.SelectedValue);
                    meta.RESULTADO = cmbrResultadoSiNo.SelectedItem.Text;

                }

                metas.Add(meta);
                var vXelements = metas.Select(x =>
                                      new XElement("META",
                                      new XAttribute("ID_EVALUADO_META", x.ID_EVALUADO_META),
                                      new XAttribute("CL_NIVEL", x.CL_META),
                                      new XAttribute("NB_RESULTADO", x.RESULTADO),
                                      new XAttribute("PR_CUMPLIMIENTO_META", x.CUMPLIMIENTO)

                          ));
                RESULTADOS =
                new XElement("CUMPLIMIENTO", vXelements
                );

            }

            decimal sumaCumplimiento = 0;
            foreach (E_RESULTADO_META item in metas)
            {
                sumaCumplimiento = sumaCumplimiento + Convert.ToDecimal(item.CUMPLIMIENTO);
            }

            //decimal cumplimientoTotal = CalcularCumplimientoPuesto(sumaCumplimiento);

            //var vXelementsBono = (new XElement("BONO",
            //                        new XAttribute("MIN_INDEPENDIENTE", vMontoMnInd),
            //                        new XAttribute("MIN_DEPENDIENTE", vMontoMnDep),
            //                        new XAttribute("MIN_GRUPAL", vMontoGrup)
            //            ));
            //RESULTADOSBONOS =
            //new XElement("BONOS", vXelementsBono
            //);

            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            if (RESULTADOS != null)
            {
                E_RESULTADO vResultado = neg.ActualizaResultadosMetas(vIdPeriodo, vsIdEvaluado, RESULTADOS, vClUsuario, vNbPrograma, sumaCumplimiento);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
            else
            {

            }
        }

        protected void grdResultados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result> vResultados = new List<SPE_OBTIENE_EO_METAS_EVALUADOS_Result>();
            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            vResultados = neg.ObtieneMetasEvaluados(idEvaluadoMeta: null, pIdPeriodo: vIdPeriodo, idEvaluado: vsIdEvaluado, no_Meta: null, cl_nivel: null, FgEvaluar: true);
            grdResultados.DataSource = vResultados;
        }

        protected void grdResultados_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            string cl_tipo_meta;
            string nb_resultado;
            string vCumplimientoMin;
            string vCumplimientoSatisfactorio;
            string vCumplimientoSobresaliente;
            bool vHabilitaMeta;

            foreach (GridDataItem item in grdResultados.MasterTableView.Items)
            {
                cl_tipo_meta = (item.GetDataKeyValue("CL_TIPO_META").ToString());
                nb_resultado = ((item.GetDataKeyValue("NB_RESULTADO")) != null) ? (item.GetDataKeyValue("NB_RESULTADO").ToString()) : "";
                vCumplimientoMin = (item.GetDataKeyValue("NB_CUMPLIMIENTO_MINIMO").ToString());
                vCumplimientoSatisfactorio = (item.GetDataKeyValue("NB_CUMPLIMIENTO_SATISFACTORIO").ToString());
                vCumplimientoSobresaliente = (item.GetDataKeyValue("NB_CUMPLIMIENTO_SOBRESALIENTE").ToString());

                 vHabilitaMeta  = ValidaConfiguracionMeta(vCumplimientoMin, vCumplimientoSatisfactorio, vCumplimientoSobresaliente);
                 if (!vHabilitaMeta)
                     lbMetasConfiguradas.Visible = true;

                 item.Enabled = vHabilitaMeta;

                if (cl_tipo_meta == "Porcentual")
                {
                    RadNumericTextBox txtResultadoPorcentual = (RadNumericTextBox)item.FindControl("txtResultadoPorcentual");
                    txtResultadoPorcentual.Visible = true;
                   // txtResultadoPorcentual.MaxValue = 100;
                    txtResultadoPorcentual.Text = nb_resultado;
                }
                else if (cl_tipo_meta == "Cantidad")
                {
                    RadNumericTextBox txtResultadoMonto = (RadNumericTextBox)item.FindControl("txtResultadoMonto");
                    txtResultadoMonto.Visible = true;
                    txtResultadoMonto.Text = nb_resultado;
                }
                else if (cl_tipo_meta == "Fecha")
                {
                    RadDatePicker dtpResultaFecha = (RadDatePicker)item.FindControl("dtpResultaFecha");
                    dtpResultaFecha.Visible = true;
                    if (nb_resultado != "")
                    {
                        dtpResultaFecha.SelectedDate = Convert.ToDateTime(nb_resultado);
                    }
                }
                else if (cl_tipo_meta == "Si/No")
                {
                    RadComboBox cmbrResultadoSiNo = (RadComboBox)item.FindControl("cmbrResultadoSiNo");
                    cmbrResultadoSiNo.Visible = true;
                    int indice;
                    if (nb_resultado == "Sí") { indice = 1; } else if (nb_resultado == "No") { indice = 2; } else { indice = 0; }
                    cmbrResultadoSiNo.SelectedIndex = indice;
                }

            }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdResultados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdResultados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdResultados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdResultados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdResultados.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }

        }

    }
}