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
using System.Xml;

namespace SIGE.WebApp.EO
{
    public partial class VentanaMetasDesempeno : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdPeriodo
        {
            get { return (int)ViewState["vs_me_id_periodo"]; }
            set { ViewState["vs_me_id_periodo"] = value; }
        }

        public string oTipoMeta
        {
            get { return (string)ViewState["vs_oTipoMeta"]; }
            set { ViewState["vs_oTipoMeta"] = value; }
        }

        private int? vIdMetaEvaluado
        {
            get { return (int)ViewState["vs_me_id_meta_evaluado"]; }
            set { ViewState["vs_me_id_meta_evaluado"] = value; }
        }

        private int vNoMeta
        {
            get { return (int)ViewState["vs_me_no_meta"]; }
            set { ViewState["vs_me_no_meta"] = value; }
        }

        private int vMeta
        {
            get { return (int)ViewState["vs_me_meta"]; }
            set { ViewState["vs_me_meta"] = value; }
        }

        private int vNoTotalMetas {
            get { return (int)ViewState["vs_no_total_metas"]; }
            set { ViewState["vs_no_total_metas"] = value; }
        }

        private int vIdEvaluado
        {
            get { return (int)ViewState["vs_me_Id_Evaluado"]; }
            set { ViewState["vs_me_Id_Evaluado"] = value; }
        }

        private bool vFgActivo
        {
            get { return (bool)ViewState["vs_vFgActivo"];}
            set { ViewState["vs_vFgActivo"]=value;}
        }

        private List<E_INDICADORES_METAS> vIndicadoresMetas
        {
            get { return (List<E_INDICADORES_METAS>)ViewState["vs_oMeta"]; }
            set { ViewState["vs_oMeta"] = value; }
        }

        private List<E_OBTIENE_EVALUADOS_DESEMPENO> vEvaluados
        {
            get { return (List<E_OBTIENE_EVALUADOS_DESEMPENO>)ViewState["vs_oEvaluado"]; }
            set { ViewState["vs_oEvaluado"] = value; }
        }

        private List<E_OBTIENE_FUNCIONES_METAS> oListaFunciones
        {
            get { return (List<E_OBTIENE_FUNCIONES_METAS>)ViewState["vs_oListaFunciones"]; }
            set { ViewState["vs_oListaFunciones"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            // oListaFunciones = new List<SPE_OBTIENE_EO_FUNCIONES_METAS_Result>();
            //vEvaluados = nPeriodo.ObtieneEvaluados(pIdEvaluado: vIdEvaluado);
            var oEvaluado = vEvaluados.Where(w => w.ID_EVALUADO == vIdEvaluado).ToList().FirstOrDefault();
           
            if (oEvaluado != null)
            {
                txtEvaluado.InnerText = oEvaluado.NB_EMPLEADO_COMPLETO;
            }
            if (oListaFunciones != null)
            {
                traerFunciones();
            }
            if (oEvaluado != null)
            {
                txtEvaluado.InnerText = oEvaluado.NB_EMPLEADO_COMPLETO;
            }

            CargarMeta(0);
        }

        public void traerFunciones()
        {
            //PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            //List<SPE_OBTIENE_EO_FUNCIONES_METAS_Result> oListaFunciones = new List<SPE_OBTIENE_EO_FUNCIONES_METAS_Result>();
            //oListaFunciones = nPeriodo.ObtieneFuncionesMetas(pIdEvaluado: vIdEvaluado);
            cmbFunciones.DataSource = oListaFunciones;
            cmbFunciones.DataTextField = "DS_PUESTO_FUNCION";
            cmbFunciones.DataValueField = "DS_PUESTO_FUNCION";
            cmbFunciones.EmptyMessage = "Selecciona";
            cmbFunciones.DataBind();

        }

        public void traerIndicadores()
        {
            if (vIndicadoresMetas != null)
            {
                cmbIndicador.DataSource = null;
                cmbIndicador.ClearSelection();

                cmbIndicador.DataSource = vIndicadoresMetas;
                cmbIndicador.DataTextField = "NB_INDICADOR";
                cmbIndicador.DataValueField = "NB_INDICADOR";
                cmbIndicador.EmptyMessage = "Selecciona";
                cmbIndicador.DataBind();
            }
        }

        private void CargarMeta(int noMeta)
        {
            if (vNoMeta > 0)
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                vMeta = vMeta + noMeta;

                if (vMeta > vNoTotalMetas & vNoTotalMetas > 0)
                {
                    return;
                }

                E_META oMeta = nPeriodo.ObtieneMetas(pIdPeriodo: vIdPeriodo, pIdEvaluado: vIdEvaluado, pNoMeta: vMeta).FirstOrDefault();
                var oEvaluado = vEvaluados.Where(w => w.ID_EVALUADO == vIdEvaluado).ToList().FirstOrDefault();
                if (noMeta == 0 || (oMeta != null && oMeta.FG_EVALUAR == true))
                {
                    //SPE_OBTIENE_EO_EVALUADOS_CONFIGURACION_DESEMPENO_Result oEvaluado = nPeriodo.ObtieneEvaluados(pIdEvaluado: vIdEvaluado).FirstOrDefault();
                    vIndicadoresMetas = nPeriodo.ObtieneIndicadoresMetas(pIdPeriodo: vIdPeriodo, pIdEvaluado: vIdEvaluado, pDsFuncion: oMeta.DS_FUNCION.ToString());
                    //vIndicadoresMetas.Add(new E_INDICADORES_METAS
                    //    {
                    //        ID_EVALUADO = vIdEvaluado,
                    //        ID_PERIODO = vIdPeriodo,
                    //        DS_FUNCION="Proyecto",
                    //        NB_INDICADOR = "Proyecto"
                    //    });
                    traerIndicadores();

                    if (oMeta != null)
                    {
                        vIdMetaEvaluado = oMeta.ID_EVALUADO_META;
                        cmbFunciones.SelectedValue = oMeta.DS_FUNCION.ToString();
                        cmbIndicador.SelectedValue = oMeta.NB_INDICADOR.ToString();
                        txtMeta.Text = oMeta.DS_META;
                        txtMetaActual.Text = oMeta.NO_META;
                        txtMetaTotal.Text = oEvaluado.NO_TOTAL_METAS == 0 ? "1" : oEvaluado.NO_TOTAL_METAS.ToString();
                        vNoTotalMetas = oEvaluado.NO_TOTAL_METAS.Value;
                        vFgActivo = oMeta.FG_EVALUAR;
                        ActivarDivs(oMeta.CL_TIPO_META);

                        if (oMeta.NO_META == "1")
                        {
                                btnAnterior.Enabled = false;
                                btnMetaSiguiente.Enabled = true;
                        }
                        else if (oMeta.NO_META == oEvaluado.NO_TOTAL_METAS.ToString())
                        {
                            btnAnterior.Enabled = true;
                            btnMetaSiguiente.Enabled = false;
                        }
                        else
                        {
                            btnAnterior.Enabled = true;
                            btnMetaSiguiente.Enabled = true;
                        }

                        if (oEvaluado.NO_TOTAL_METAS_ACTIVAS == 1)
                        {
                            btnAnterior.Enabled = false;
                            btnMetaSiguiente.Enabled = false;
                        }

                        switch (oMeta.CL_TIPO_META)
                        {
                            case "Porcentual":
                                rbPorcentual.Checked = true;
                                rbMonto.Checked = false;
                                rbFecha.Checked = false;
                                rbSiNo.Checked = false;
                                txtPActual.Text = oMeta.NB_CUMPLIMIENTO_ACTUAL.ToString();
                                txtPMinimo.Text = oMeta.NB_CUMPLIMIENTO_MINIMO.ToString();
                                txtPSatisfactoria.Text = oMeta.NB_CUMPLIMIENTO_SATISFACTORIO.ToString();
                                txtPSobresaliente.Text = oMeta.NB_CUMPLIMIENTO_SOBRESALIENTE.ToString();
                                break;
                            case "Cantidad":
                                rbPorcentual.Checked = false;
                                rbMonto.Checked = true;
                                rbFecha.Checked = false;
                                rbSiNo.Checked = false;
                                txtMActual.Text = oMeta.NB_CUMPLIMIENTO_ACTUAL.ToString();
                                txtMMinima.Text = oMeta.NB_CUMPLIMIENTO_MINIMO.ToString();
                                txtMSatisfactoria.Text = oMeta.NB_CUMPLIMIENTO_SATISFACTORIO.ToString();
                                txtMSobresaliente.Text = oMeta.NB_CUMPLIMIENTO_SOBRESALIENTE.ToString();
                                break;
                            case "Fecha":
                                rbPorcentual.Checked = false;
                                rbMonto.Checked = false;
                                rbFecha.Checked = true;
                                rbSiNo.Checked = false;
                                if (oMeta.NB_CUMPLIMIENTO_ACTUAL != "")
                                    dtpActual.SelectedDate = Convert.ToDateTime(oMeta.NB_CUMPLIMIENTO_ACTUAL);
                                if (oMeta.NB_CUMPLIMIENTO_MINIMO != "")
                                    dtpMinimo.SelectedDate = Convert.ToDateTime(oMeta.NB_CUMPLIMIENTO_MINIMO);
                                if (oMeta.NB_CUMPLIMIENTO_SATISFACTORIO != "")
                                    dtpSatisfactoria.SelectedDate = Convert.ToDateTime(oMeta.FE_CUMPLIMIENTO_SATISFACTORIO);
                                if (oMeta.NB_CUMPLIMIENTO_SOBRESALIENTE != "")
                                    dtpSobresaliente.SelectedDate = Convert.ToDateTime(oMeta.FE_CUMPLIMIENTO_SOBRESALIENTE);
                                break;
                            case "Si/No":
                                rbPorcentual.Checked = false;
                                rbMonto.Checked = false;
                                rbFecha.Checked = false;
                                rbSiNo.Checked = true;
                                txtSMinimo.Text = oMeta.NB_CUMPLIMIENTO_MINIMO.ToString();
                                txtPSobresaliente.Text = oMeta.NB_CUMPLIMIENTO_SATISFACTORIO.ToString();
                                break;
                            default:
                                //PORCENTUAL
                                txtPActual.Text = "";
                                txtPMinimo.Text = "";
                                txtPSatisfactoria.Text = "";
                                txtPSobresaliente.Text = "";

                                //CANTIDAD
                                txtMActual.Text = "";
                                txtMMinima.Text = "";
                                txtMSatisfactoria.Text = "";
                                txtMSobresaliente.Text = "";

                                //FECHA
                                dtpActual.Clear();
                                dtpMinimo.Clear();
                                dtpSatisfactoria.Clear();
                                dtpSobresaliente.Clear();

                                //SI/NO
                                txtSMinimo.Text = "";
                                txtPSobresaliente.Text = "";

                                break;
                        }
                        txtPonderacion.Text = oMeta.PR_META.ToString();
                    }
                }
                else
                {
                    if (oEvaluado.NO_TOTAL_METAS.ToString() == oMeta.NO_META)
                        CargarMeta(-1);
                    else if (oMeta.NO_META == "1")
                       CargarMeta(1);
                    else
                        CargarMeta(noMeta);
                }
            }
        }

        private void GuardarDatos(bool pCerrar)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            string vTipoTransaccion = "I";
            string dsMetas = cmbFunciones.Text;
            string vCltipoMeta = "";
            string vNbActual = "";
            string vNbMinimo = "";
            string vNbSatisfactorio = "";
            string vNbSobresaliente = "";
            decimal? vPrMeta = null;

            if (txtPonderacion.Text != "")
            {
                vPrMeta = decimal.Parse(txtPonderacion.Text);
            }
            if (vIdMetaEvaluado != 0)
            {
                vTipoTransaccion = "A";
            }
            if (rbPorcentual.Checked)
            {
                vCltipoMeta = "Porcentual";

                vNbActual = txtPActual.Text;
                vNbMinimo = txtPMinimo.Text;
                vNbSatisfactorio = txtPSatisfactoria.Text;
                vNbSobresaliente = txtPSobresaliente.Text;
            }
            else if (rbMonto.Checked)
            {
                vCltipoMeta = "Cantidad";

                vNbActual = txtMActual.Text;
                vNbMinimo = txtMMinima.Text;
                vNbSatisfactorio = txtMSatisfactoria.Text;
                vNbSobresaliente = txtMSobresaliente.Text;
            }
            else if (rbFecha.Checked)
            {
                vCltipoMeta = "Fecha";

                if(dtpActual.SelectedDate != null)
                vNbActual = dtpActual.SelectedDate.Value.ToString("dd/MM/yyyy");
                if (dtpMinimo.SelectedDate != null)
                vNbMinimo = dtpMinimo.SelectedDate.Value.ToString("dd/MM/yyyy");
                if (dtpSatisfactoria.SelectedDate != null)
                vNbSatisfactorio = dtpSatisfactoria.SelectedDate.Value.ToString("dd/MM/yyyy");
                if (dtpSobresaliente.SelectedDate != null)
                vNbSobresaliente = dtpSobresaliente.SelectedDate.Value.ToString("dd/MM/yyyy");
            }
            else if (rbSiNo.Checked)
            {
                vCltipoMeta = "Si/No";
                vNbActual = "";
                vNbMinimo = txtSMinimo.Text;
                vNbSatisfactorio = "0";
                vNbSobresaliente = txtSMaximo.Text;
            }
            if ( oTipoMeta == "CERO" || oTipoMeta == "DESCRIPTIVO" && cmbIndicador.Text != "" && cmbFunciones.Text != "")
            {
                if (txtMeta.Text != "")
                {
                    if (vCltipoMeta != "")
                    {
                        if ((vPrMeta != null && vNbMinimo != "" && vNbSatisfactorio != "" && vNbSobresaliente != "") || (vCltipoMeta == "Si/No" && vPrMeta != null))
                        {
                            E_RESULTADO vResultado = nPeriodo.InsetaActualizaMetasEvaluados(vIdMetaEvaluado, vIdPeriodo, vIdEvaluado, dsMetas, vNoMeta, cmbIndicador.Text, txtMeta.Text, vCltipoMeta, null, vFgActivo, vNbActual, vNbMinimo, vNbSatisfactorio, vNbSobresaliente, vPrMeta, null, null, null, vClUsuario, vNbPrograma, vTipoTransaccion);
                            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                            {
                                if (pCerrar == true)
                                {
                                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                                }
                                else
                                {
                                    if (vIdMetaEvaluado == 0)
                                    LimpiarControles();

                                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                                }
                            }
                            else
                            {
                                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                            }
                        }
                        else
                        {
                            if (vCltipoMeta == "Si/No")
                                UtilMensajes.MensajeResultadoDB(rwmMensaje, "El campo ponderación es requerido.", E_TIPO_RESPUESTA_DB.WARNING);
                            else
                                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Los campos mínimo,  satisfactoría, sobresaliente y ponderación son requeridos.", E_TIPO_RESPUESTA_DB.WARNING);
                        }
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, "Debes insertar un tipo de meta.", E_TIPO_RESPUESTA_DB.WARNING);

                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Debes insertar una meta.", E_TIPO_RESPUESTA_DB.WARNING);
                }
            }
            else
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "La función y el indicador son requeridos.", E_TIPO_RESPUESTA_DB.WARNING);
               
                   
        }

        private void LimpiarControles()
        {
            cmbFunciones.Text = string.Empty;
            cmbFunciones.SelectedIndex = -1;
            cmbIndicador.Text = string.Empty;
            cmbIndicador.Items.Clear();
            txtMeta.Text = "";
            rbPorcentual.Checked = false;
            rbMonto.Checked = false;
            rbFecha.Checked = false;
            rbSiNo.Checked = false;
            txtMetaActual.Text = "";
            txtMMinima.Text = "";
            txtMActual.Text = "";
            txtMetaTotal.Text = "";
            txtMSatisfactoria.Text = "";
            txtMSobresaliente.Text = "";
            txtPActual.Text = "";
            txtPMinimo.Text = "";
            txtPonderacion.Text = "";
            txtPSatisfactoria.Text = "";
            txtPSobresaliente.Text = "";
            //txtSMaximo.Text = "";
            //txtSMinimo.Text = "";
            dtpActual.Clear();
            dtpMinimo.Clear();
            dtpSatisfactoria.Clear();
            dtpSobresaliente.Clear();

        }

        private void ActivarDivs(string pClTipoMeta)
        {
            switch (pClTipoMeta)
            {
                case "Porcentual":
                    divPorcentual.Style["display"] = "block";
                    divMonto.Style["display"] = "none";
                    divFecha.Style["display"] = "none";
                    divSiNo.Style["display"] = "none";
                    break;
                case "Cantidad":
                    divPorcentual.Style["display"] = "none";
                    divMonto.Style["display"] = "block";
                    divFecha.Style["display"] = "none";
                    divSiNo.Style["display"] = "none";
                    break;
                case "Fecha":
                    divPorcentual.Style["display"] = "none";
                    divMonto.Style["display"] = "none";
                    divFecha.Style["display"] = "block";
                    divSiNo.Style["display"] = "none";
                    break;
                case "Si/No":
                    divPorcentual.Style["display"] = "none";
                    divMonto.Style["display"] = "none";
                    divFecha.Style["display"] = "none";
                    divSiNo.Style["display"] = "block";
                    break;
            }
        }

        private string ObtieneResponsabilidad(string pResponsabilidades)
        {
            string vResponsabilidad = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pResponsabilidades);
            XmlNodeList responsabilidad = xml.GetElementsByTagName("NOTAS_RESPONSABILIDAD");

            if (responsabilidad.Count > 0)
            {

                XmlNodeList lista =
                ((XmlElement)responsabilidad[0]).GetElementsByTagName("NOTA");

                foreach (XmlElement nodo in lista)
                {
                    vResponsabilidad = nodo.GetAttribute("DS_NOTA");
                }
            }

            return vResponsabilidad;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
       {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            

            if (!Page.IsPostBack)
            {
                vIdEvaluado = 0;
                vNoTotalMetas = 0;

                if (Request.Params["IdEvaluado"] != null)
                {
                   // PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                    vIdEvaluado = int.Parse(Request.Params["IdEvaluado"].ToString());
                    vEvaluados = nPeriodo.ObtieneEvaluados(pIdEvaluado: vIdEvaluado);
                    var oEvaluado = vEvaluados.Where(w => w.ID_EVALUADO == vIdEvaluado).ToList().FirstOrDefault();
                    if (oEvaluado != null)
                    {
                        txtEvaluado.InnerText = oEvaluado.NB_EMPLEADO_COMPLETO;
                        txtResponsabilidad.InnerHtml = ObtieneResponsabilidad(oEvaluado.XML_RESPONSABILIDAD);
                    }
                    oListaFunciones = nPeriodo.ObtieneFuncionesMetas(pIdEvaluado: vIdEvaluado).ToList();
                    oListaFunciones.Add(new E_OBTIENE_FUNCIONES_METAS
                    {
                        ID_EVALUADO = vIdEvaluado,
                        DS_PUESTO_FUNCION = "Proyecto"
                    });
                }

                if (Request.Params["Accion"] != null && Request.Params["Accion"] == "Agregar")
                {
                    btnAnterior.Visible = false;
                    btnMetaSiguiente.Visible = false;
                    txtMetaActual.Visible = false;
                    txtMetaTotal.Visible = false;
                    lblDeM.Visible = false;
                }

                if (Request.Params["IdPeriodo"] != null)
                {
                  //  PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());
                     oTipoMeta = nPeriodo.ObtienePeriodoDesempeno(pIdPeriodo: vIdPeriodo).CL_TIPO_METAS;
                    

                    if (oTipoMeta == "CERO")
                    {
                        cmbFunciones.Visible = false;
                        lblFuncion.Visible = false;
                        lbIndicador.Visible = false;
                        cmbIndicador.Visible = false;
                    }

                    if (oTipoMeta == "DESCRIPTIVO")
                    {
                        cmbFunciones.Visible = true;
                        lblFuncion.Visible = true;
                    }
                }

                if (Request.Params["NoMeta"] != null && Request.Params["Meta"] != null)
                {
                    vNoMeta = int.Parse(Request.Params["NoMeta"].ToString());
                    vMeta = int.Parse(Request.Params["Meta"].ToString());
                    RadPane2.Visible = false;
                    CargarDatos();
                }
                else
                {
                    vNoMeta = 0;
                    vIdMetaEvaluado = 0;
                    traerFunciones();
                    vIndicadoresMetas = nPeriodo.ObtieneIndicadoresMetas(pIdPeriodo: vIdPeriodo, pIdEvaluado: vIdEvaluado, pDsFuncion: (cmbFunciones.Text == "" ? null : cmbFunciones.Text));
                    vIndicadoresMetas.Add(new E_INDICADORES_METAS
                    {
                        ID_EVALUADO = vIdEvaluado,
                        ID_PERIODO = vIdPeriodo,
                        DS_FUNCION = "Proyecto",
                        NB_INDICADOR = "Proyecto"
                    });
                    traerIndicadores();
                    vFgActivo = true;
                }

                if (Request.Params["Accion"] != null && Request.Params["Accion"] == "Editar")
                {
                    cmbFunciones.Enabled = false;
                    cmbIndicador.Enabled = false;
                }
            }           
        }

        protected void btnGuadrar_Click(object sender, EventArgs e)
        {
            GuardarDatos(false);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            GuardarDatos(true);
        }

        protected void rbPorcentual_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPorcentual.Checked)
            {
                ActivarDivs("Porcentual");
            }
        }

        protected void rbMonto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMonto.Checked)
            {
                ActivarDivs("Cantidad");
            }
        }

        protected void rbFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFecha.Checked)
            {
                ActivarDivs("Fecha");
            }
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            CargarMeta(-1);
        }

        protected void btnMetaSiguiente_Click(object sender, EventArgs e)
        {
            CargarMeta(1);
        }

        protected void rbSiNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSiNo.Checked)
            {
                ActivarDivs("Si/No");
            }
        }

        protected void cmbFunciones_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            if (cmbFunciones.Text == "Proyecto")
            {
                vIndicadoresMetas = new List<E_INDICADORES_METAS>();
                vIndicadoresMetas.Add(new E_INDICADORES_METAS
                {
                    ID_EVALUADO = vIdEvaluado,
                    ID_PERIODO = vIdPeriodo,
                    DS_FUNCION = "Proyecto",
                    NB_INDICADOR = "Proyecto"
                });
            }
            else
                vIndicadoresMetas = nPeriodo.ObtieneIndicadoresMetas(pIdPeriodo: vIdPeriodo, pIdEvaluado: vIdEvaluado, pDsFuncion: (cmbFunciones.Text == "" ? null : cmbFunciones.Text));

            traerIndicadores();
        }
    }
}