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

namespace SIGE.WebApp.EO
{
    public partial class VentanaEnvioSolicitudesReplica : System.Web.UI.Page
    {
        #region Variables

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement FECHASENVIOSOLICITUDES { get; set; }
        private string vClUsuario;
        private string vNbPrograma;

        public int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_IdPeriodo"]; }
            set { ViewState["vs_IdPeriodo"] = value; }
        }

        public List<E_FECHA_ENVIO_SOLICITUDES> lFechasEnvio
        {
            get { return (List<E_FECHA_ENVIO_SOLICITUDES>)ViewState["vs_lFechasEnvio"]; }
            set { ViewState["vs_lFechasEnvio"] = value; }
        }

        public List<E_OBTIENE_PERIODO_REPLICAS> lPerReplica
        {
            get { return (List<E_OBTIENE_PERIODO_REPLICAS>)ViewState["vs_lPerReplica"]; }
            set { ViewState["vs_lPerReplica"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                lPerReplica = new List<E_OBTIENE_PERIODO_REPLICAS>();
                List<SPE_OBTIENE_PERIODO_REPLICAS_Result> lPeriodos = new List<SPE_OBTIENE_PERIODO_REPLICAS_Result>();
                List<SPE_OBTIENE_CONFIGURACION_PERIODO_REPLICAS_Result> lPeriodosNoConfigurables = new List<SPE_OBTIENE_CONFIGURACION_PERIODO_REPLICAS_Result>();
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoId"].ToString());
                    PeriodoDesempenoNegocio pNegocio = new PeriodoDesempenoNegocio();
                    lPeriodosNoConfigurables = pNegocio.ObtieneConfiguracionEnvio(vIdPeriodo);
                    if (lPeriodosNoConfigurables.Count == 0)
                    {
                        lbEnvioSolReplicas.Attributes.Add("style", "Color:Red;");
                        btnAceptar.Enabled = false;
                        btnCancelar.Enabled = false;
                        lbEnvioSolReplicas.InnerText = "Para este periodo y sus réplicas ya han sido configuradas las fechas de envío de solicitudes.";
                    }
                    else
                    {
                        var vBaja = lPeriodosNoConfigurables.Where(t => t.CL_ESTADO_EMPLEADO == "BAJA").FirstOrDefault();
                        if (vBaja != null)
                        {
                            lbEnvioSolReplicas.Attributes.Add("style", "Color:Red;");
                            btnAceptar.Enabled = false;
                            btnCancelar.Enabled = false;
                            lbEnvioSolReplicas.InnerText = "Este periodo cuenta con evaluados dados de baja y no es posible enviar solicitudes.";
                        }
                        else
                        {
                            lPeriodos = (pNegocio.ObtenerPeriodos(vIdPeriodo));
                            foreach (var item in lPeriodos)
                            {
                                lPerReplica.Add(new E_OBTIENE_PERIODO_REPLICAS
                                {
                                    ID_PERIODO = item.ID_PERIODO,
                                    CL_PERIODO = item.CL_PERIODO,
                                    NB_PERIODO = item.NB_PERIODO,
                                    DS_PERIODO = item.DS_PERIODO,
                                    FE_INICIO = item.FE_INICIO,
                                    FE_TERMINO = (DateTime)item.FE_TERMINO,
                                    CL_ESTADO_PERIODO = item.CL_ESTADO_PERIODO,
                                    DS_NOTAS = item.DS_NOTAS,
                                    CL_TIPO_PERIODO = item.CL_ESTADO_PERIODO,
                                    ID_PERIODO_DESEMPENO = item.ID_PERIODO_DESEMPENO,
                                    FG_BONO = item.FG_BONO,
                                    PR_BONO = item.PR_BONO,
                                    MN_BONO = item.MN_BONO,
                                    CL_TIPO_BONO = item.CL_TIPO_BONO,
                                    CL_TIPO_CAPTURISTA = item.CL_TIPO_CAPTURISTA,
                                    CL_TIPO_METAS = item.CL_TIPO_METAS,
                                    CL_ORIGEN_CUESTIONARIO = item.CL_ORIGEN_CUESTIONARIO,
                                    ID_PERIODO_REPLICA = item.ID_PERIODO_REPLICA
                                });
                            }
                            var vIdReplica = lPerReplica.Where(t => t.ID_PERIODO == vIdPeriodo).FirstOrDefault();
                            if (vIdReplica.ID_PERIODO_REPLICA == null)
                            {
                                int vReplicas = (pNegocio.ObtenerPeriodos(vIdPeriodo).Count - 1);
                                lbEnvioSolReplicas.InnerText = "Este periodo tiene " + vReplicas.ToString() + " réplicas, ¿Cuándo quieres enviar solicitud para capturar los resultados del periodo original y sus réplicas?. A continuación se muestran las opciones que podrás elegir para el envío.";
                            }
                            else
                            {
                                lbEnvioSolReplicas.InnerText = "Este periodo es una réplica, ¿Cuándo quieres enviar solicitud para capturar los resultados del periodo original y sus réplicas?. A continuación se muestran las opciones que podrás elegir para el envío.";
                            }
                        }
                    }
                }
            }
            if (btnDiasPrevios.Checked)
                txtDiasPrevios.Visible = true;
            else
                txtDiasPrevios.Visible = false;

            if (btnDiasPosteriores.Checked)
                txtDiasPosteriores.Visible = true;
            else
                txtDiasPosteriores.Visible = false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            PeriodoDesempenoNegocio pNegocio = new PeriodoDesempenoNegocio();
            lFechasEnvio = new List<E_FECHA_ENVIO_SOLICITUDES>();

            if (btnDiasPrevios.Checked)
            {
                int vDiasPrev = txtDiasPrevios.Text == "" ? 0 : int.Parse(txtDiasPrevios.Text);
                if (vDiasPrev != 0)
                {
                    lFechasEnvio = new List<E_FECHA_ENVIO_SOLICITUDES>();
                    foreach (E_OBTIENE_PERIODO_REPLICAS item in lPerReplica)
                    {

                        DateTime vFeTermino = item.FE_TERMINO;
                        var vIdPeriodoXml = item.ID_PERIODO.ToString();
                        lFechasEnvio.Add(new E_FECHA_ENVIO_SOLICITUDES
                        {
                            ID_PERIODO = vIdPeriodoXml,
                            FE_ENVIO_SOLICITUD = vFeTermino.AddDays(-vDiasPrev).ToString("MM/dd/yyyy")
                        });

                        var vXelements = lFechasEnvio.Select(x =>
                                               new XElement("FECHAS_ENVIO",
                                               new XAttribute("ID_PERIODO", x.ID_PERIODO),
                                               new XAttribute("FE_ENVIO", x.FE_ENVIO_SOLICITUD))
                                    );
                        FECHASENVIOSOLICITUDES =
                        new XElement("ASIGNACION", vXelements
                        );
                    }

                    if (FECHASENVIOSOLICITUDES != null)
                    {
                        E_RESULTADO vResultado = pNegocio.InsertaFeEnvioSolicitud(FECHASENVIOSOLICITUDES.ToString(), vClUsuario, vNbPrograma, "I");
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Introduzca la cantidad de dias previos.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }

            }
            if (btnDiasPosteriores.Checked)
            {
                int vDiasPost = txtDiasPosteriores.Text == "" ? 0 : int.Parse(txtDiasPosteriores.Text);
                if (vDiasPost != 0)
                {
                    foreach (E_OBTIENE_PERIODO_REPLICAS item in lPerReplica)
                    {
                        DateTime vFeTermino = (DateTime)item.FE_TERMINO;
                        var vIdPeriodoXml = item.ID_PERIODO.ToString();
                        lFechasEnvio.Add(new E_FECHA_ENVIO_SOLICITUDES
                        {
                            ID_PERIODO = vIdPeriodoXml,
                            FE_ENVIO_SOLICITUD = vFeTermino.AddDays(vDiasPost).ToString("MM/dd/yyyy")
                        }
                            );

                        var vXelements = lFechasEnvio.Select(x =>
                                         new XElement("FECHAS_ENVIO",
                                             new XAttribute("ID_PERIODO", x.ID_PERIODO),
                                             new XAttribute("FE_ENVIO", x.FE_ENVIO_SOLICITUD))
                                             );

                        FECHASENVIOSOLICITUDES = (new XElement("ASIGNACION", vXelements));

                        if (FECHASENVIOSOLICITUDES != null)
                        {
                            E_RESULTADO vResultado = pNegocio.InsertaFeEnvioSolicitud(FECHASENVIOSOLICITUDES.ToString(), vClUsuario, vNbPrograma, "I");
                            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
                        }
                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Introduzca la cantidad de dias posteriores", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }
            }
            if (btnCierrePeriodo.Checked)
            {
                foreach (E_OBTIENE_PERIODO_REPLICAS item in lPerReplica)
                {
                    var vIdPeriodoXml = item.ID_PERIODO.ToString();
                    DateTime vFeCierre = (DateTime)item.FE_TERMINO;

                    lFechasEnvio.Add(new E_FECHA_ENVIO_SOLICITUDES
                        {
                            ID_PERIODO = vIdPeriodoXml,
                            FE_ENVIO_SOLICITUD = vFeCierre.ToString("MM/dd/yyyy")
                        }
                        );

                    var vXelements = lFechasEnvio.Select(x => new XElement("FECHAS_ENVIO",
                                                                  new XAttribute("ID_PERIODO", x.ID_PERIODO),
                                                                  new XAttribute("FE_ENVIO", x.FE_ENVIO_SOLICITUD))
                        );

                    FECHASENVIOSOLICITUDES = (new XElement("ASIGNACION", vXelements));

                    if (FECHASENVIOSOLICITUDES != null)
                    {
                        E_RESULTADO vResultado = pNegocio.InsertaFeEnvioSolicitud(FECHASENVIOSOLICITUDES.ToString(), vClUsuario, vNbPrograma, "I");
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
                    }
                }
            }
            if (btnCreaPeriodo.Checked)
            {
                foreach (E_OBTIENE_PERIODO_REPLICAS item in lPerReplica)
                {
                    var vIdPeriodoXml = item.ID_PERIODO.ToString();
                    DateTime vFeCreacion = (DateTime)item.FE_INICIO;

                    lFechasEnvio.Add(new E_FECHA_ENVIO_SOLICITUDES
                    {
                        ID_PERIODO = vIdPeriodoXml,
                        FE_ENVIO_SOLICITUD = vFeCreacion.ToString("MM/dd/yyyy")
                    }
                        );

                    var vXelements = lFechasEnvio.Select(x => new XElement("FECHAS_ENVIO",
                                                                  new XAttribute("ID_PERIODO", x.ID_PERIODO),
                                                                  new XAttribute("FE_ENVIO", x.FE_ENVIO_SOLICITUD))
                                                                  );

                    FECHASENVIOSOLICITUDES = (new XElement("ASIGNACION", vXelements));

                    if (FECHASENVIOSOLICITUDES != null)
                    {
                        E_RESULTADO vResultado = pNegocio.InsertaFeEnvioSolicitud(FECHASENVIOSOLICITUDES.ToString(), vClUsuario, vNbPrograma, "I");
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
                    }
                }

            }
            if (btnFechaEnvio.Checked)
            {
                ClientScript.RegisterStartupScript(GetType(), "script", "OpenSendMessage(" + vIdPeriodo + ");", true);
            }
        }
    }
}