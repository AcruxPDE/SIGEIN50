using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
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
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.EO.Cuestionarios
{
    public partial class VentanaCapturaMasiva : System.Web.UI.Page
    {

        #region Metodos

        private bool ValidaConfiguracionMeta(string vCumplimientoMin, string vCumplimientoSatisfactorio, string vCumplimientoSobresaliente)
        {
            bool vFgMetaConfigurada = false;

            if (vCumplimientoMin != "" && vCumplimientoSatisfactorio != "" && vCumplimientoSobresaliente != "")
            {
                vFgMetaConfigurada = true;
            }

            return vFgMetaConfigurada;
        }

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

        private void CalculaResultado()
        {
            string cl_tipo_meta;
            string vCumplimientoMin;
            string vCumplimientoSatisfactorio;
            string vCumplimientoSobresaliente;

            metas = new List<E_RESULTADO_META>();
            foreach (GridDataItem item in grdMetas.MasterTableView.Items)
            {
                cl_tipo_meta = (item.GetDataKeyValue("CL_TIPO_META").ToString());            
                vCumplimientoMin = (item.GetDataKeyValue("NB_CUMPLIMIENTO_MINIMO").ToString());
                vCumplimientoSatisfactorio = (item.GetDataKeyValue("NB_CUMPLIMIENTO_SATISFACTORIO").ToString());
                vCumplimientoSobresaliente = (item.GetDataKeyValue("NB_CUMPLIMIENTO_SOBRESALIENTE").ToString());


                //VALORES
                decimal ponderacion = Convert.ToDecimal(item.GetDataKeyValue("PR_EVALUADO").ToString());
                int id_evaluado_meta = Convert.ToInt32(item.GetDataKeyValue("ID_META_EVALUADO").ToString());
                E_RESULTADO_META meta = new
                E_RESULTADO_META { ID_EVALUADO_META = id_evaluado_meta, CL_META = 0, PONDERACION = ponderacion, MINIMO = vCumplimientoMin, SATISFACTORIO = vCumplimientoSatisfactorio, SOBRESALIENTE = vCumplimientoSobresaliente, CUMPLIMIENTO = 0, RANGOVALOR = 0, RESULTADO = String.Empty };

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
              
            }  
            decimal sumaCumplimiento = 0;
            foreach (E_RESULTADO_META item in metas)
            {
                sumaCumplimiento = sumaCumplimiento + Convert.ToDecimal(item.CUMPLIMIENTO);
            }

           txtTotal.Text = sumaCumplimiento.ToString() + "%";
        }

        #endregion

        #region Variables

        private string vClUsuario;
        public string cssModulo = String.Empty;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        List<E_RESULTADO_META> metas
        {
            get { return (List<E_RESULTADO_META>)ViewState["vsmetas"]; }
            set { ViewState["vsmetas"] = value; }
        }

        public int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }

        public Guid clToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }

        public int vIdEvaluador
        {
            get { return (int)ViewState["vsIdEvaluador"]; }
            set { ViewState["vsIdEvaluador"] = value; }
        }

        private XElement RESULTADOS { get; set; }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";
            vNbPrograma = ContextoUsuario.nbPrograma;

            string vClModulo = "EVALUACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            if (!IsPostBack)
            {
                if (Request.Params["ID_PERIODO"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["ID_PERIODO"]);
                }
                else
                {
                    vIdPeriodo = 0;
                }
                PeriodoDesempenoNegocio periodo = new PeriodoDesempenoNegocio();
                var oPeriodo = periodo.ObtienePeriodoDesempeno(pIdPeriodo: vIdPeriodo);

                if (oPeriodo != null)
                {
                    txtNoPeriodo.InnerText = oPeriodo.CL_PERIODO;
                    txtNbPeriodo.InnerText = oPeriodo.DS_PERIODO;
                }

                if (Request.Params["IdEvaluador"] != null && Request.Params["TOKEN"] != null)
                {
                    vIdEvaluador = int.Parse(Request.QueryString["IdEvaluador"]);
                    clToken = Guid.Parse(Request.QueryString["TOKEN"]);
                    var evaluador = periodo.ObtenerPeriodoEvaluadorDesempeno(vIdEvaluador, clToken);

                    lblEvaluador.InnerText = evaluador.NB_EVALUADOR;
                }
            }
        }

        protected void btnTerminar_Click(object sender, EventArgs e)
        {
            var myUrl = ResolveUrl("~/Logon.aspx");
            Response.Redirect(myUrl);
        }

        protected void grdMetas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_EO_METAS_CAPTURA_MASIVA_Result> vMetas = new List<SPE_OBTIENE_EO_METAS_CAPTURA_MASIVA_Result>();
            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            vMetas = neg.ObtieneMetasCapturaMasiva(pIdPeriodo: vIdPeriodo, idEvaluador: vIdEvaluador, pFlEvaluador:clToken);
            grdMetas.DataSource = vMetas;
        }

        protected void grdMetas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            string cl_tipo_meta;
            string nb_resultado;
            string vCumplimientoMin;
            string vCumplimientoSatisfactorio;
            string vCumplimientoSobresaliente;
            bool vHabilitaMeta;

            foreach (GridDataItem item in grdMetas.MasterTableView.Items)
            {
                cl_tipo_meta = (item.GetDataKeyValue("CL_TIPO_META").ToString());
                nb_resultado = ((item.GetDataKeyValue("NB_RESULTADO")) != null) ? (item.GetDataKeyValue("NB_RESULTADO").ToString()) : "";
                vCumplimientoMin = (item.GetDataKeyValue("NB_CUMPLIMIENTO_MINIMO").ToString());
                vCumplimientoSatisfactorio = (item.GetDataKeyValue("NB_CUMPLIMIENTO_SATISFACTORIO").ToString());
                vCumplimientoSobresaliente = (item.GetDataKeyValue("NB_CUMPLIMIENTO_SOBRESALIENTE").ToString());

                vHabilitaMeta = ValidaConfiguracionMeta(vCumplimientoMin, vCumplimientoSatisfactorio, vCumplimientoSobresaliente);
                item.Enabled = vHabilitaMeta;


                if (cl_tipo_meta == "Porcentual")
                {
                    RadNumericTextBox txtResultadoPorcentual = (RadNumericTextBox)item.FindControl("txtResultadoPorcentual");
                    txtResultadoPorcentual.Visible = true;
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
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdMetas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdMetas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdMetas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdMetas.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdMetas.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }

            CalculaResultado();
        }

        protected void grdEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            grdEvaluados.DataSource = nPeriodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo, pIdEvaluador: vIdEvaluador);
        }

        protected void btnAplicarTodos_Click(object sender, EventArgs e)
        {
            CalculaResultado();
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


            decimal sumaCumplimiento = 0;
            foreach (E_RESULTADO_META item in metas)
            {
                sumaCumplimiento = sumaCumplimiento + Convert.ToDecimal(item.CUMPLIMIENTO);
            }


            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();
            if (RESULTADOS != null)
            {
                E_RESULTADO vResultado = neg.ActualizaResultadosMetasMasiva(vIdPeriodo, vIdEvaluador, RESULTADOS, vClUsuario, vNbPrograma, sumaCumplimiento);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onRebindEvaluados");             
            }
        }

        protected void txtResultadoPorcentual_TextChanged(object sender, EventArgs e)
        {
            CalculaResultado();
        }

        protected void cmbrResultadoSiNo_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            CalculaResultado();
        }

        protected void dtpResultaFecha_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            CalculaResultado();
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
    }
}