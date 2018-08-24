using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Replicas
{
    public class Replicas
    {
        int vIdPeriodoNoEnviado;

        public E_RESULTADO enviarSolicitudesReplicas()
        {
            E_RESULTADO respuesta = new E_RESULTADO();
            var lista = obtenerListaEvaluadores();
            if (lista != null)
            {
                respuesta = envioSolicitudesReplicas(lista);
            }
            return respuesta;
        }

        public List<E_REPLICAS_ENVIAR> obtenerListaEvaluadores()
        {
            List<E_REPLICAS_ENVIAR> lEvaluadoresReplicas = new List<E_REPLICAS_ENVIAR>();
            PeriodoDesempenoNegocio pNegocio = new PeriodoDesempenoNegocio();
            var vListaEvaluadores = pNegocio.ObtenerSolicitudesEnviar();
            lEvaluadoresReplicas = (from c in vListaEvaluadores
                                    select new E_REPLICAS_ENVIAR
                                    {
                                        ID_EVALUADOR = c.ID_EVALUADOR,
                                        NB_EVALUADOR = c.NB_EVALUADOR,
                                        FL_EVALUADOR = c.FL_EVALUADOR,
                                        CL_TOKEN = c.CL_TOKEN,
                                        CL_CORREO_ELECTRONICO = c.CL_CORREO_ELECTRONICO,
                                        FE_ENVIO_SOLICITUD = c.FE_ENVIO_SOLICITUD,
                                        ID_PERIODO = c.ID_PERIODO
                                    }).ToList();
            return lEvaluadoresReplicas;
        }

        public E_RESULTADO insertarEstatusEnvio(bool pFgEnviado, int pIdPeriodo)
        {
            PeriodoDesempenoNegocio pNegocio = new PeriodoDesempenoNegocio();
            return pNegocio.InsertaEstatusEnvioSolicitudes(pIdPeriodo, pFgEnviado, "Proceso automatico", "Proceso automatico");
        }

        public E_RESULTADO envioSolicitudesReplicas(List<E_REPLICAS_ENVIAR> lEvaluadoresReplicas)
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["ClientSettingsProvider.ServiceUri"];
            string vUrl = value + "/Logon.aspx?ClProceso=DESEMPENO";
            string vDsMensaje = ContextoApp.EO.Configuracion.MensajeCapturaResultados.dsMensaje;
            var respuesta = "0";
            E_RESULTADO vResult = new E_RESULTADO();
            vIdPeriodoNoEnviado = 0;
            foreach (E_REPLICAS_ENVIAR item in lEvaluadoresReplicas)
            {
                string vMensaje = vDsMensaje;
                Mail mail = new Mail(ContextoApp.mailConfiguration);
                var EnviaMail = false;

                if (!String.IsNullOrEmpty(item.NB_EVALUADOR) && item.FL_EVALUADOR != null && !String.IsNullOrEmpty(item.CL_TOKEN))
                {
                    mail.addToAddress(item.CL_CORREO_ELECTRONICO, null);
                    EnviaMail = true;
                }
                if (EnviaMail)
                {
                    vMensaje = vMensaje.Replace("[NB_EVALUADOR]", item.NB_EVALUADOR);
                    vMensaje = vMensaje.Replace("[URL]", vUrl + "&FlProceso=" + item.FL_EVALUADOR);
                    vMensaje = vMensaje.Replace("[CONTRASENA]", item.CL_TOKEN);
                    respuesta = mail.Send("Evaluación del desempeño", vMensaje);
                    if (respuesta == "0")
                    {
                        if (item.ID_PERIODO != vIdPeriodoNoEnviado)
                        {
                            vResult = insertarEstatusEnvio(true, item.ID_PERIODO);
                            if ((vResult.CL_TIPO_ERROR.Equals("ERROR")) || (vResult.CL_TIPO_ERROR.Equals("WARNING_WITH_FUNCTION")) || (vResult.CL_TIPO_ERROR.Equals("WARNING")))
                            {
                                return vResult;
                            }
                        }
                    }
                    else
                    {
                        vIdPeriodoNoEnviado = item.ID_PERIODO;
                        vResult = insertarEstatusEnvio(false, item.ID_PERIODO);
                        if ((vResult.CL_TIPO_ERROR.Equals("ERROR")) || (vResult.CL_TIPO_ERROR.Equals("WARNING_WITH_FUNCTION")) || (vResult.CL_TIPO_ERROR.Equals("WARNING")))
                        {
                            return vResult;
                        }
                    }
                }
                else
                {
                    vIdPeriodoNoEnviado = item.ID_PERIODO;
                    vResult = insertarEstatusEnvio(false, item.ID_PERIODO);
                    if ((vResult.CL_TIPO_ERROR.Equals("ERROR")) || (vResult.CL_TIPO_ERROR.Equals("WARNING_WITH_FUNCTION")) || (vResult.CL_TIPO_ERROR.Equals("WARNING")))
                    {
                        return vResult;
                    }
                }
            }
            return vResult;
        }
    }
}