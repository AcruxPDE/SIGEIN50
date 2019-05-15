using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.Externas;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;

namespace SIGE.WebApp.EO
{
    public partial class VentanaCuestionario : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vTipoTransaccion = "";

        private int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        private int vIdPregunta
        {
            get { return (int)ViewState["vs_vIdPregunta"]; }
            set { ViewState["vs_vIdPregunta"] = value; }
        }

        private int? vIdPreguntaReferencia
        {
            get { return (int?)ViewState["vs_vIdPreguntaReferencia"]; }
            set { ViewState["vs_vIdPreguntaReferencia"] = value; }
        }

        private List<E_PREGUNTAS_PERIODO_CLIMA> vPreguntasPeriodo
        {
            get { return (List<E_PREGUNTAS_PERIODO_CLIMA>)ViewState["vs_vPreguntasPeriodo"]; }
            set { ViewState["vs_vPreguntasPeriodo"] = value; }
        }

        private List<E_PREGUNTAS_PERIODO_CLIMA> vlstPreguntasPeriodo
        {
            get { return (List<E_PREGUNTAS_PERIODO_CLIMA>)ViewState["vs_vlstPreguntasPeriodo"]; }
            set { ViewState["vs_vlstPreguntasPeriodo"] = value; }
        }

        private Guid vIdRelacionPregunta
        {
            get { return (Guid)ViewState["vs_vc_id_relacion_pregunta"]; }
            set { ViewState["vs_vc_id_relacion_pregunta"] = value; }
        }

        #endregion

        #region Funciones

        protected void MostrarPreguntaSecundaria(bool pVerificar)
        {
            if (pVerificar == true)
            {
                foreach (var item in vPreguntasPeriodo)
                {
                    if (vIdPregunta != item.ID_PREGUNTA)
                    {
                        vIdPreguntaReferencia = item.ID_PREGUNTA;
                        txtPreguntaVerificacion.Text = item.NB_PREGUNTA;
                        txnSecuenciaVerificacion.Text = item.NO_SECUENCIA.ToString();
                    }
                }
                txtPreguntaVerificacion.Enabled = true;
                txnSecuenciaVerificacion.Enabled = true;
            }
            else
            {
                txtPreguntaVerificacion.Text = "";
                txnSecuenciaVerificacion.Text = "";
                txtPreguntaVerificacion.Enabled = false;
                txnSecuenciaVerificacion.Enabled = false;
            }
        }

        public void CargarCatalogos()
        {
            ClimaLaboralNegocio nClimaLaboral = new ClimaLaboralNegocio();
            cmbDimension.DataSource = nClimaLaboral.ObtieneCatalogoDimensiones();
            cmbDimension.DataValueField = "CL_DIMENSION";
            cmbDimension.DataTextField = "NB_DIMENSION";
            cmbDimension.DataBind();

            cmbTema.DataSource = nClimaLaboral.ObtenerCatalogoTemas();
            cmbTema.DataValueField = "CL_TEMA";
            cmbTema.DataTextField = "NB_TEMA";
            cmbTema.DataBind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                vIdPeriodo = int.Parse((Request.QueryString["ID_PERIODO"]));
                CargarCatalogos();
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                var vClima = nClima.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                if (vClima.CL_ORIGEN_CUESTIONARIO == "PREDEFINIDO" && Request.QueryString["ID_PREGUNTA"] != null)
                {
                    btnVerificacionTrue.Enabled = false;
                    btnVerificacionFalse.Enabled = false;
                    //rbVerificacion.Enabled = false;
                }

                if (Request.QueryString["ID_PREGUNTA"] != null)
                {
                    vIdPregunta = int.Parse((Request.QueryString["ID_PREGUNTA"]));
                    vPreguntasPeriodo = nClima.ObtienePreguntaPeriodo(pID_PREGUNTA: vIdPregunta, pID_PERIODO: vIdPeriodo).Select(s => new E_PREGUNTAS_PERIODO_CLIMA
                    {
                        ID_PREGUNTA = s.ID_PREGUNTA,
                        NB_DIMENSION = s.NB_DIMENSION,
                        NB_PREGUNTA = s.NB_PREGUNTA,
                        NB_TEMA = s.NB_TEMA,
                        NO_SECUENCIA = s.NO_SECUENCIA,
                        CL_TIPO = s.CL_TIPO,
                        FG_HABILITA_VERIFICACION = (bool)s.FG_INVALIDEZ,
                        ID_RELACION_PREGUNTA = s.ID_RELACION_PREGUNTA
                    }).ToList();

                 
                    foreach (var item in vPreguntasPeriodo)
                    {
                        if (item.ID_PREGUNTA == vIdPregunta)
                        {
                            cmbDimension.Text = item.NB_DIMENSION;
                            cmbTema.Text = item.NB_TEMA;
                            txtPregunta.Text = item.NB_PREGUNTA;
                            txnSecuencia.Text = item.NO_SECUENCIA.ToString();
                           // rbVerificacion.Checked = item.FG_HABILITA_VERIFICACION;
                            btnVerificacionTrue.Checked = item.FG_HABILITA_VERIFICACION;
                            btnVerificacionFalse.Checked = !item.FG_HABILITA_VERIFICACION;
                            vIdRelacionPregunta = item.ID_RELACION_PREGUNTA;

                            if (item.FG_HABILITA_VERIFICACION == false)
                            {
                                txtPreguntaVerificacion.Enabled = false;
                                txnSecuenciaVerificacion.Enabled = false;
                            }

                        }
                        else
                        {
                            vIdPreguntaReferencia = item.ID_PREGUNTA;
                            if (item.FG_HABILITA_VERIFICACION == true)
                            {
                                txtPreguntaVerificacion.Text = item.NB_PREGUNTA;
                                txnSecuenciaVerificacion.Text = item.NO_SECUENCIA.ToString();
                                txtPreguntaVerificacion.Enabled = true;
                                txnSecuenciaVerificacion.Enabled = true;
                            }
                            else
                            {
                                txtPreguntaVerificacion.Enabled = false;
                                txnSecuenciaVerificacion.Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    btnVerificacionTrue.Checked = false;
                    btnVerificacionFalse.Checked = true;
                    txtPreguntaVerificacion.Enabled = false;
                    txnSecuenciaVerificacion.Enabled = false;
                    vIdPregunta = 0;
                    vIdPreguntaReferencia = 0;
                    vIdRelacionPregunta = Guid.NewGuid();
                }
            }
        }

        protected void grdEjemplo_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<E_INSTRUCCIONES> vlstInstrucciones = new List<E_INSTRUCCIONES>();
            vlstInstrucciones.Add(new E_INSTRUCCIONES { TEMA = "Pregunta 1", LIDERAZGO = "Mi jefe inmediato respeta y valora mi trabajo.", ORDEN = 1 });
            vlstInstrucciones.Add(new E_INSTRUCCIONES { TEMA = "Pregunta 2", LIDERAZGO = "Mi jefe inmediato me brinda apoyo en mi trabajo cuando es necesario.", ORDEN = 17 });

            grdEjemplo.DataSource = vlstInstrucciones;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
            E_PERIODO_CLIMA vPeriodo = new E_PERIODO_CLIMA();
            E_PREGUNTAS_PERIODO_CLIMA vPreguntas = new E_PREGUNTAS_PERIODO_CLIMA();
             vPreguntas.ID_PREGUNTA = vIdPregunta;
            vPreguntas.ID_RELACION_PREGUNTA = vIdRelacionPregunta;
            //vPreguntas.FG_HABILITA_VERIFICACION = rbVerificacion.Checked;
            vPreguntas.FG_HABILITA_VERIFICACION = btnVerificacionTrue.Checked;
            vPreguntas.ID_PREGUNTA_VERIFICACION = vIdPreguntaReferencia;

            if (Request.QueryString["ID_PREGUNTA"] != null)
                vTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
            else
                vTipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();

            if (cmbDimension.Text != "")
                vPreguntas.NB_DIMENSION = cmbDimension.Text;
            //else if (txtAgDimension.Text != "")
            //    vPreguntas.NB_DIMENSION = txtAgDimension.Text;
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione o ingrese una dimensión.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }

            if (cmbTema.Text != "")
                vPreguntas.NB_TEMA = cmbTema.Text;
            //else if (txtAgTema.Text != "")
            //    vPreguntas.NB_TEMA = txtAgTema.Text;
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione o ingrese un tema.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }

            if (txtPregunta.Text != "")
                vPreguntas.NB_PREGUNTA = txtPregunta.Text;
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese la pregunta.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }

            if (txnSecuencia.Value != null)
                vPreguntas.NO_SECUENCIA = (int)txnSecuencia.Value;
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese el número de secuencia.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return;
            }

            //if (rbVerificacion.Checked == true)
            if (btnVerificacionTrue.Checked == true)
            {
                if (txtPreguntaVerificacion.Text != "")
                    vPreguntas.NB_PREGUNTA_VERIFICACION = txtPreguntaVerificacion.Text;
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese la pregunta de verificación.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }
                if (txnSecuenciaVerificacion.Text != "")
                {
                    vPreguntas.NO_SECUENCIA_VERIFICACION = int.Parse(txnSecuenciaVerificacion.Text);

                    if (vPreguntas.NO_SECUENCIA_VERIFICACION == vPreguntas.NO_SECUENCIA)
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, "La pregunta de verificación no puede tener el mismo número de secuencia.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                        return;
                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese el numero de secuencia de la pregunta de verificación.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return;
                }
            }

            E_RESULTADO vResultado = nPeriodo.InsertaActualizaPreguntasPeriodo(vIdPeriodo, vPreguntas, vClUsuario, vNbPrograma, vTipoTransaccion);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
            }
        }

        //protected void rbVerificacion_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (vPreguntasPeriodo != null)
        //    {
        //        if (rbVerificacion.Checked == true)
        //            MostrarPreguntaSecundaria(true);
        //        else
        //            MostrarPreguntaSecundaria(false);
        //    }
        //}

        protected void cmbDimension_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ClimaLaboralNegocio nClimaLaboral = new ClimaLaboralNegocio();
            if (e.Value != "")
            {
                var vDimension = nClimaLaboral.ObtieneCatalogoDimensiones(null, (e.Value.ToString())).FirstOrDefault();
                cmbTema.DataSource = nClimaLaboral.ObtenerCatalogoTemas(null, null, null, vDimension.ID_DIMENSION);
                cmbTema.DataValueField = "CL_TEMA";
                cmbTema.DataTextField = "NB_TEMA";
                cmbTema.DataBind();
                //txtAgDimension.Enabled = false;
            }
            else
            {
                //txtAgDimension.Enabled = true;
            }
        }

        protected void cmbTema_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (e.Value != "")
            //    txtAgTema.Enabled = false;
            //else
            //    txtAgTema.Enabled = true;
        }

        protected void btnVerificacionTrue_CheckedChanged(object sender, EventArgs e)
        {
            if (vPreguntasPeriodo != null)
            {
                if (btnVerificacionTrue.Checked == true)
                    MostrarPreguntaSecundaria(true);
                else
                    MostrarPreguntaSecundaria(false);
            }
            else
            {
                if (btnVerificacionTrue.Checked == true)
                {
                    txtPreguntaVerificacion.Enabled = true;
                    txnSecuenciaVerificacion.Enabled = true;
                }
                else
                {
                    txtPreguntaVerificacion.Enabled = false;
                    txnSecuenciaVerificacion.Enabled = false;
                }
            }
        }

    }
}