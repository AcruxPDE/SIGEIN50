using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
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
using SIGE.Negocio.Utilerias;
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class CuestionarioClimaLaboral : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        public string cssModulo = String.Empty;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        public int vIdEvaluado
        {
            get { return (int)ViewState["vs_vIdEvaluado"]; }
            set { ViewState["vs_vIdEvaluado"] = value; }
        }


        public string xmlRespuestasCuestionario
        {
            get { return (string)ViewState["vs_xmlRespuestasCuestionario"]; }
            set { ViewState["vs_xmlRespuestasCuestionario"] = value; }
        }

        public List<E_RESPUESTA> vlstRespuestas
        {
            get { return (List<E_RESPUESTA>)ViewState["vs_vlstRespuestas"]; }
            set { ViewState["vs_vlstRespuestas"] = value; }
        }

        public string xmlPreguntasAbiertas
        {
            get { return (string)ViewState["vs_xmlPreguntasAbiertas"]; }
            set { ViewState["vs_xmlPreguntasAbiertas"] = value; }
        }

        public List<E_RESPUESTA_PREGUNTAS_ABIERTAS> vlstPreguntasAbiertas
        {
            get { return (List<E_RESPUESTA_PREGUNTAS_ABIERTAS>)ViewState["vs_vlstPreguntasAbiertas"]; }
            set { ViewState["vs_vlstPreguntasAbiertas"] = value; }
        }

        public List<E_PREGUNTAS_CUESTIONARIO_CLIMA> vlstCuestionarios
        {
            get { return (List<E_PREGUNTAS_CUESTIONARIO_CLIMA>)ViewState["vs_vlstCuestionarios"]; }
            set { ViewState["vs_vlstCuestionarios"] = value; }
        }

        public bool vFgHabilitado
        {
            get { return (bool)ViewState["vs_fg_habilitado"]; }
            set { ViewState["vs_fg_habilitado"] = value; }
        }

        #endregion

        #region Metodos

        protected void ValorRespuestas()
        {
            int vIdPregunta;
            decimal vNoRespuesta = 0;
            vlstRespuestas = new List<E_RESPUESTA>();
            vlstPreguntasAbiertas = new List<E_RESPUESTA_PREGUNTAS_ABIERTAS>();
            bool vFgPrimer = false;

            foreach (GridDataItem item in rgCuestionario.MasterTableView.Items)
            {
                vIdPregunta = int.Parse(item.GetDataKeyValue("ID_CUESTIONARIO_PREGUNTA").ToString());
                RadButton rbTotalmenteAcuerdo = (RadButton)item.FindControl("rbTotalmenteAcuerdo");
                RadButton rbCasiAcuerdo = (RadButton)item.FindControl("rbCasiAcuerdo");
                RadButton rbCasiDesacuerdo = (RadButton)item.FindControl("rbCasiDesacuerdo");
                RadButton rbTotalmenteDesacuerdo = (RadButton)item.FindControl("rbTotalmenteDesacuerdo");

                if (rbTotalmenteAcuerdo.Checked == true)
                    vNoRespuesta = 4;
                else if (rbCasiAcuerdo.Checked == true)
                    vNoRespuesta = 3;
                else if (rbCasiDesacuerdo.Checked == true)
                    vNoRespuesta = 2;
                else if (rbTotalmenteDesacuerdo.Checked == true)
                    vNoRespuesta = 1;
                else { vNoRespuesta = 0; }

                if (vNoRespuesta != 0)
                {
                    vlstRespuestas.Add(new E_RESPUESTA { ID_PREGUNTA_RESPUESTA = vIdPregunta, NO_VALOR = vNoRespuesta });
                    item.BackColor = System.Drawing.Color.White;
                }
                else
                {
                    item.BackColor = System.Drawing.Color.Gold;
                    if (vFgPrimer == false)
                    {
                        rbTotalmenteAcuerdo.Focus();
                        rbTotalmenteAcuerdo.BorderWidth = 2;
                        vFgPrimer = true;
                    }
                }
            }

            var vXelements = vlstRespuestas.Select(x =>
                                         new XElement("RESPUESTA",
                                         new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_PREGUNTA_RESPUESTA),
                                         new XAttribute("NO_VALOR", x.NO_VALOR)
                              ));
            XElement vXmlRespuestas = new XElement("RESPUESTAS", vXelements);
            xmlRespuestasCuestionario = vXmlRespuestas.ToString();

            foreach (GridDataItem item in rgPreguntasAbiertas.MasterTableView.Items)
            {
                vIdPregunta = int.Parse(item.GetDataKeyValue("ID_CUESTIONARIO_PREGUNTA").ToString());
                RadTextBox txtRespuesta = (RadTextBox)item.FindControl("txtRespuesta");
                if (txtRespuesta != null)
                    vlstPreguntasAbiertas.Add(new E_RESPUESTA_PREGUNTAS_ABIERTAS { ID_PREGUNTA_RESPUESTA = vIdPregunta, NB_RESPUESTA = txtRespuesta.Text });
            }

            var vXelementos = vlstPreguntasAbiertas.Select(x =>
                                                           new XElement("RESPUESTA",
                                                               new XAttribute("ID_CUESTIONARIO_PREGUNTA", x.ID_PREGUNTA_RESPUESTA),
                                                               new XAttribute("RESPUESTA", x.NB_RESPUESTA)));
            XElement vXmlPreguntasAbiertas = new XElement("RESPUESTAS", vXelementos);

            xmlPreguntasAbiertas = vXmlPreguntasAbiertas.ToString();
        }

        protected void SeguridadProcesos()
        {
            btnFinalizar.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.J.A");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO; ;
            vNbPrograma = ContextoUsuario.nbPrograma;

            string vClModulo = "EVALUACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);


            if (!IsPostBack)
            {
                if (Request.QueryString["ID_PERIODO"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["ID_PERIODO"]);
                }
                if (Request.QueryString["ID_EVALUADOR"] != null)
                {
                    vIdEvaluado = int.Parse(Request.QueryString["ID_EVALUADOR"]);
                }

                //if (Request.Params["FG_HABILITADO"] != null)
                //{
                //    vFgHabilitado = bool.Parse(Request.Params["FG_HABILITADO"].ToString());
                //}
                //else
                //{
                    vFgHabilitado = true;
                //}

                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                var vPeriodoClima = nClima.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                txtNoPeriodo.InnerText = vPeriodoClima.NB_PERIODO.ToString() + " - " + vPeriodoClima.DS_PERIODO.ToString();


                SeguridadProcesos();

                //rgCuestionario.Enabled = vFgHabilitado;
                //rgPreguntasAbiertas.Enabled = vFgHabilitado;
                //btnFinalizar.Enabled = vFgHabilitado;


            }
        }

        protected void rgCuestionario_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            vlstCuestionarios = nClima.ObtieneCuestionario(pID_EVALUADOR: vIdEvaluado, pID_PERIODO: vIdPeriodo).Select(s => new E_PREGUNTAS_CUESTIONARIO_CLIMA
            {
                ID_CUESTIONARIO = s.ID_CUESTIONARIO,
                ID_CUESTIONARIO_PREGUNTA = s.ID_CUESTIONARIO_PREGUNTA,
                ID_EVALUADOR = s.ID_EVALUADOR,
                NB_PREGUNTA = s.NB_PREGUNTA,
                NO_SECUENCIA = s.NO_SECUENCIA,
                NO_VALOR_RESPUESTA = s.NO_VALOR_RESPUESTA,
                FG_VALOR1 = s.NO_VALOR_RESPUESTA == 4 ? true : false,
                FG_VALOR2 = s.NO_VALOR_RESPUESTA == 3 ? true : false,
                FG_VALOR3 = s.NO_VALOR_RESPUESTA == 2 ? true : false,
                FG_VALOR4 = s.NO_VALOR_RESPUESTA == 1 ? true : false,
            }).ToList();
            rgCuestionario.DataSource = vlstCuestionarios;
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            ValorRespuestas();
            string vMensaje;
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();

            if (vlstCuestionarios.Count == vlstRespuestas.Count)
            {
                var vResultado = nClima.ActualizaPreguntasCuestionario(xmlRespuestasCuestionario, xmlPreguntasAbiertas, true, vIdEvaluado, vClUsuario, vNbPrograma);
                vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
            else
            {
                vMensaje = "No se puede guardar el cuestionario por que hay preguntas sin responder.";
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ValorRespuestas();
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            var vResultado = nClima.ActualizaPreguntasCuestionario(xmlRespuestasCuestionario, xmlPreguntasAbiertas, false, vIdEvaluado, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vClUsuario.Equals("INVITADO"))
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                var myUrl = ResolveUrl("~/Logon.aspx");
                Response.Redirect(myUrl);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
        }

        protected void rgPreguntasAbiertas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            int vCount = nClima.ObtenerCuestionarioPreAbiertas(pID_EVALUADOR: vIdEvaluado, pID_PERIODO: vIdPeriodo).Count;
            if (vCount > 0)
                rgPreguntasAbiertas.DataSource = nClima.ObtenerCuestionarioPreAbiertas(pID_EVALUADOR: vIdEvaluado, pID_PERIODO: vIdPeriodo).ToList();
            else
                rgPreguntasAbiertas.Visible = false;
        }
    }

    [Serializable]
    public class E_RESPUESTA
    {
        public int? ID_PREGUNTA_RESPUESTA { set; get; }
        public decimal? NO_VALOR { set; get; }
    }

}