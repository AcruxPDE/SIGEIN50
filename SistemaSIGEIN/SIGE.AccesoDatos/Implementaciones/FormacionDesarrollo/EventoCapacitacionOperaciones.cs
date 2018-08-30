using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class EventoCapacitacionOperaciones
    {
        SistemaSigeinEntities contexto;

        public List<E_EVENTO> ObtenerEventos(int? ID_EVENTO =null, int? ID_PROGRAMA = null, int? ID_CURSO = null, int? ID_INSTRUCTOR =null, int? ID_EMPLEADO_EVALUADOR = null,
                                                                string CL_EVENTO = null, string NB_EVENTO = null, string DS_EVENTO = null, DateTime? FE_INICIO = null, DateTime? FE_TERMINO = null, string NB_CURSO = null,
                                                                string NB_INSTRUCTOR = null, string CL_TIPO_CURSO = null, string CL_ESTADO = null, DateTime? FE_EVALUACION = null, string DS_LUGAR = null, decimal? MN_COSTO_DIRECTO = null, decimal? MN_COSTO_INDIRECTO = null,
                                                                Guid? FL_EVENTO = null, string CL_TOKEN = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var lista = contexto.SPE_OBTIENE_C_EVENTO(ID_EVENTO, ID_PROGRAMA, ID_CURSO, ID_INSTRUCTOR, ID_EMPLEADO_EVALUADOR, CL_EVENTO, NB_EVENTO, DS_EVENTO, FE_INICIO, FE_TERMINO, NB_CURSO, NB_INSTRUCTOR, CL_TIPO_CURSO, CL_ESTADO, FE_EVALUACION, DS_LUGAR, MN_COSTO_DIRECTO, MN_COSTO_INDIRECTO, FL_EVENTO, CL_TOKEN).ToList();

                var source = (from a in lista
                             select new E_EVENTO
                             {
                                 ID_EVENTO = a.ID_EVENTO,
                                 CL_EVENTO = a.CL_EVENTO,
                                 NB_EVENTO = a.NB_EVENTO,
                                 DS_EVENTO = a.DS_EVENTO,
                                 ID_PROGRAMA = a.ID_PROGRAMA,
                                 NB_PROPGRAMA = a.NB_PROGRAMA,
                                 FE_INICIO = a.FE_INICIO,
                                 FE_TERMINO = a.FE_TERMINO,
                                 ID_CURSO = a.ID_CURSO,
                                 CL_CURSO = a.CL_CURSO,
                                 NB_CURSO = a.NB_CURSO,
                                 ID_INSTRUCTOR = a.ID_INSTRUCTOR,
                                 NB_INSTRUCTOR = a.NB_INSTRUCTOR,
                                 CL_TIPO_CURSO = a.CL_TIPO_CURSO,
                                 CL_ESTADO = a.CL_ESTADO,
                                 NB_ESTADO = a.NB_ESTADO,
                                 ID_EMPLEADO_EVALUADOR = a.ID_EMPLEADO_EVALUADOR,
                                 CL_EVALUADOR = a.CL_EVALUADOR,
                                 NB_EVALUADOR = a.NB_EVALUADOR,
                                 CL_CORREO_EVALUADOR = a.CL_CORREO_EVALUADOR,
                                 FE_EVALUACION = a.FE_EVALUACION,
                                 DS_LUGAR = a.DS_LUGAR,
                                 DS_REFRIGERIO = a.DS_REFRIGERIO,
                                 MN_COSTO_DIRECTO = a.MN_COSTO_DIRECTO,
                                 MN_COSTO_INDIRECTO = a.MN_COSTO_INDIRECTO,
                                 FG_INCLUIR_EN_PLANTILLA = a.FG_INCLUIR_EN_PLANTILLA,
                                 NO_DURACION_CURSO = a.NO_DURACION_CURSO == null ? 0 : (decimal)a.NO_DURACION_CURSO,
                                 XML_PARTICIPANTES = a.XML_PARTICIPANTES,
                                 FL_EVENTO = a.FL_EVENTO,
                                 CL_TOKEN = a.CL_TOKEN,
                                 XML_CAMPOS_ADICIONALES = a.XML_CAMPOS_ADICIONALES,
                                 CL_USUARIO_APP_MODIFICA = a.CL_USUARIO_APP_MODIFICA,
                                 FE_MODIFICA = String.Format("{0:dd/MM/yyyy}", a.FE_MODIFICA)
                             }).ToList();

                return source;
            }
        }

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null, bool? pFgFoto = null, string pClUsuario =null, bool? pFgActivo = null, int? pIdEmpresa = null, int? pIdRol = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                string sXmlSeleccion = null;

                if (pXmlSeleccion != null)
                {
                    sXmlSeleccion = pXmlSeleccion.ToString();
                }
                else
                {
                    sXmlSeleccion = (new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "TODAS")))).ToString();
                }

                return contexto.SPE_OBTIENE_EMPLEADOS(sXmlSeleccion, pClUsuario, pFgActivo, pFgFoto, pIdEmpresa, pIdRol).ToList();
            }
        }

        public XElement InsertaActualizaEvento(string tipo_transaccion, E_EVENTO evento, string usuario, string programa)
        {
            using (contexto = new SistemaSigeinEntities())
            {                
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                contexto.SPE_INSERTA_ACTUALIZA_EVENTO(pout_clave_retorno, evento.ID_EVENTO, evento.CL_EVENTO, evento.NB_EVENTO, evento.DS_EVENTO, evento.ID_PROGRAMA, evento.FE_INICIO, evento.FE_TERMINO, evento.ID_CURSO, evento.NB_CURSO, evento.ID_INSTRUCTOR, evento.NB_INSTRUCTOR, evento.CL_TIPO_CURSO, evento.CL_ESTADO, evento.ID_EMPLEADO_EVALUADOR, evento.FE_EVALUACION, evento.DS_LUGAR, evento.DS_REFRIGERIO, evento.MN_COSTO_DIRECTO, evento.MN_COSTO_INDIRECTO, evento.FG_INCLUIR_EN_PLANTILLA, usuario, programa, evento.XML_PARTICIPANTES, evento.XML_CALENDARIO, evento.FL_EVENTO, evento.CL_TOKEN, evento.XML_CAMPOS_ADICIONALES, tipo_transaccion);

                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }

        public XElement EliminaEvento(int pIdEvento)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                contexto.SPE_ELIMINA_EVENTO(pout_clave_retorno, pIdEvento);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminaEmpleadoPrograma(int pIdPrograma, int pIdEmpleado)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_EMPLEADO_CAPACITACION(pout_clave_retorno, pIdPrograma, pIdEmpleado);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public List<E_EVENTO_PARTICIPANTE> ObtenerParticipanteEvento( int? ID_EVENTO_PARTICIPANTE = null, int? ID_EVENTO = null, int? ID_EMPLEADO = null, string CL_PARTICIPANTE = null, string NB_PARTICIPANTE = null, string NB_PUESTO = null, string NB_DEPARTAMENTO = null, int? NO_TIEMPO = null, decimal? PR_CUMPLIMIENTO = null, int? ID_ROL = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var lista = contexto.SPE_OBTIENE_EVENTO_PARTICIPANTE(ID_EVENTO_PARTICIPANTE, ID_EVENTO, ID_EMPLEADO, CL_PARTICIPANTE, NB_PARTICIPANTE, NB_PUESTO, NB_DEPARTAMENTO, NO_TIEMPO, PR_CUMPLIMIENTO, ID_ROL).ToList();

                var source = (from a in lista select new E_EVENTO_PARTICIPANTE {
                    ID_EVENTO_PARTICIPANTE = a.ID_EVENTO_PARTICIPANTE,
                    ID_EVENTO = a.ID_EVENTO,
                    ID_EMPLEADO = a.ID_EMPLEADO, 
                    CL_PARTICIPANTE = a.CL_PARTICIPANTE,
                    NB_PARTICIPANTE = a.NB_PARTICIPANTE,
                    NB_PUESTO = a.NB_PUESTO,
                    NB_DEPARTAMENTO = a.NB_DEPARTAMENTO,
                    NO_TIEMPO = a.NO_TIEMPO,
                    PR_CUMPLIMIENTO = a.PR_CUMPLIMIENTO,
                    NO_DURACION = a.NO_DURACION,
                    CL_CORREO_ELECTRONICO = a.CL_CORREO_ELECTRONICO
                }).ToList();


                return source;
            }
        }

        public List<E_EVENTO_CALENDARIO> ObtenerEventoCalendario(int? pIdEventoCalendario = null, int? pIdEvento = null, DateTime? pFeInicial = null, DateTime? pFeFinal = null, int? pNoHoras = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var lista = contexto.SPE_OBTIENE_EVENTO_CALENDARIO(pIdEventoCalendario, pIdEvento, pFeInicial, pFeFinal, pNoHoras).ToList();

                var source = (from a in lista
                              select new E_EVENTO_CALENDARIO
                              {
                                  ID_EVENTO_CALENDARIO = a.ID_EVENTO_CALENDARIO,
                                  ID_EVENTO = a.ID_EVENTO,
                                  FE_INICIAL = a.FE_INICIAL,
                                  FE_FINAL = a.FE_FINAL,
                                  NO_HORAS = a.NO_HORAS
                              }).ToList();


                return source;
            }
        }

        public XElement ActualizarEventoCalendario( string pXmlParticipantes, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                contexto.SPE_ACTUALIZA_EVENTO_PARTICIPANTE(pout_clave_retorno, pXmlParticipantes, pClUsuario, pNbPrograma);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_EVENTO_PARTICIPANTE_COMPETENCIA_Result> ObtenerEventoParticipanteCompetencia(int? ID_EVENTO_PARTICIPANTE_COMPETENCIA = null, int? ID_EVENTO = null, int? ID_PARTICIPANTE = null, int? ID_COMPETENCIA = null, byte? NO_EVALUACION = null, string NB_COMPETENCIA = null, int? ID_EMPRESA = null, int? ID_ROL = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EVENTO_PARTICIPANTE_COMPETENCIA(ID_EVENTO_PARTICIPANTE_COMPETENCIA, ID_EVENTO, ID_PARTICIPANTE, ID_COMPETENCIA, NO_EVALUACION, NB_COMPETENCIA, ID_EMPRESA, ID_ROL).ToList();
            }
        }

        public XElement ActualizarEvaluacionCompetencias(string pXmlEvaluacion, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                contexto.SPE_ACTUALIZA_EVENTO_PARTICIPANTE_COMPETENCIA(pout_clave_retorno, pXmlEvaluacion, pClUsuario, pNbPrograma);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminarEventoParticipante(int pIdEventoParticipante)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                contexto.SPE_ELIMINA_EVENTO_PARTICIPANTE(pout_clave_retorno, pIdEventoParticipante);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminarEventoCalendario(int pIdEventoCalendario)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                contexto.SPE_ELIMINA_EVENTO_CALENDARIO(pout_clave_retorno, pIdEventoCalendario);

                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public SPE_OBTIENE_EVENTO_LISTA_ASISTENCIA_Result ObtenerDatosListaAsistencia(int pIdEvento, int? vIdRol)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_EVENTO_LISTA_ASISTENCIA(pIdEvento, vIdRol).FirstOrDefault();
            }
        }

        public List<SPE_OBTIENE_FYD_REPORTE_RESULTADOS_EVENTO_Result> ObtenerReporteResultadosEvento(int pIdEvento, int? vIdRol)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_FYD_REPORTE_RESULTADOS_EVENTO(pIdEvento, vIdRol).ToList();
            }
        }

        public List<E_EVENTO_PARTICIPANTE_COMPETENCIA> ObtenerReporteResultadosEventoDetalle(int pIdEvento)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var vLstCompetenciasDetalle = contexto.SPE_OBTIENE_FYD_REPORTE_RESULTADOS_EVENTO_DETALLE(pIdEvento).ToList();


                var vLstCompetencias = vLstCompetenciasDetalle.Select(t =>
                    new E_EVENTO_PARTICIPANTE_COMPETENCIA
                    {
                        CL_COMPETENCIA = t.CL_COMPETENCIA,
                        ID_COMPETENCIA = t.ID_COMPETENCIA,
                        ID_EVENTO = t.ID_EVENTO,
                        ID_PARTICIPANTE = t.ID_PARTICIPANTE,
                        NB_COMPETENCIA = t.NB_COMPETENCIA,
                        PR_EVALUACION_COMPETENCIA = t.PR_EVALUACION_COMPETENCIA.Value
                    }).ToList();

                return vLstCompetencias;

            }
        }

        public string ObtenerCamposAdicionalesXml(String CL_TABLA_REFERENCIA = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CAMPO_ADICIONAL_XML(CL_TABLA_REFERENCIA).FirstOrDefault().ToString();
            }
        }
    }
}
