using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.IntegracionDePersonal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
   public class PruebasOperaciones
    {
        private SistemaSigeinEntities context;

        ///////////////////////////////////////C_PRUEBA////////////////////////////////////////////

        public List<SPE_OBTIENE_C_PRUEBA_Result> Obtener_C_PRUEBA(int? pIdPrueba = null, string pClPrueba = null, string pNbPrueba = null, int? pIdCuestionario = null, short? pNoTiempo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from V_C_PRUEBA in context.SPE_OBTIENE_C_PRUEBA(pIdPrueba, pClPrueba,pNbPrueba,pIdCuestionario,pNoTiempo)
                        select V_C_PRUEBA;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }
        }

        public List<SPE_OBTIENE_CANDIDATOS_BATERIA_MASIVA_Result> ObtenerCandidatoFolio(string pNbCandidato = null, string pNbPaterno = null, string pNbMaterno = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from V_C_CANDIDATOS in context.SPE_OBTIENE_CANDIDATOS_BATERIA_MASIVA(pNbCandidato, pNbPaterno, pNbMaterno)
                        select V_C_CANDIDATOS;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }
        }

        public List<SPE_OBTIENE_PRUEBAS_CONFIGURADAS_Result> ObtenerPruebasConfiguradas(int? pID_BATERIA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                List<SPE_OBTIENE_PRUEBAS_CONFIGURADAS_Result> retorno = new List<SPE_OBTIENE_PRUEBAS_CONFIGURADAS_Result>();
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                retorno = context.SPE_OBTIENE_PRUEBAS_CONFIGURADAS(pID_BATERIA).ToList();
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return retorno;
            }
        }
               
        public XElement InsertaActualiza_C_PRUEBA(string tipo_transaccion, SPE_OBTIENE_C_PRUEBA_Result V_C_PRUEBA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_INSERTA_ACTUALIZA_C_PRUEBA(pout_clave_retorno, V_C_PRUEBA.ID_PRUEBA,V_C_PRUEBA.CL_PRUEBA,V_C_PRUEBA.NB_PRUEBA,V_C_PRUEBA.ID_CUESTIONARIO,V_C_PRUEBA.NO_TIEMPO_PRUEBA,usuario,usuario,programa,programa, tipo_transaccion);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
       
        public XElement generarPruebasEmpleado(XElement vEmpleados, XElement vPruebas, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_GENERA_PRUEBAS_CANDIDATOS(pout_clave_retorno, vEmpleados.ToString(), vPruebas.ToString(), usuario,  programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement ActualizarPruebasEmpleado( XElement vPruebas, int? pIdBateria, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ACTUALIZA_PRUEBAS_CANDIDATOS(pout_clave_retorno, vPruebas.ToString(), pIdBateria, usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
       
        public XElement Elimina_C_PRUEBA(int? ID_PRUEBA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ELIMINA_C_PRUEBA(pout_clave_retorno, ID_PRUEBA, usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminaBateriaPruebas(string pXmlBaterias = null, string pNbUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ELIMINA_K_BATERIA_PRUEBA(pout_clave_retorno, pXmlBaterias, pNbUsuario, pNbPrograma);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }     

       ///////////////////////////////////////K_PRUEBA////////////////////////////////////////////

        public List<SPE_OBTIENE_K_PRUEBA_Result> Obtener_K_PRUEBA(Guid? pClTokenExterno = null, int? pIdPrueba = null, int? pIdPruebaPlantilla = null, int? pIdCandidato = null, int? pIdEmpleado = null, String pClEmpleado = null, String pClEstado = null, DateTime? pFeInicio = null, DateTime? pFeTermino = null, int? pNoTiempo = null, int? pIdBateria = null, Guid? pClTokenBateria = null, bool? pFgAsignada = null)     
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from V_K_PRUEBA in context.SPE_OBTIENE_K_PRUEBA(pIdPrueba, pIdPruebaPlantilla, pIdCandidato, pIdEmpleado, pClEmpleado, pClEstado, pFeInicio, pFeTermino, pNoTiempo, pClTokenExterno, pClTokenBateria, pIdBateria, pFgAsignada)
                        select V_K_PRUEBA;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }
        }
        
        public List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> Obtener_RESULTADO_PRUEBA(int pIdPrueba, Guid pClTokenExterno)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from V_K_PRUEBA in context.SPE_OBTIENE_RESULTADO_PRUEBA(pIdPrueba, pClTokenExterno)
                        select V_K_PRUEBA;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }        
        }

        public List<SPE_OBTIENE_BATERIA_PRUEBAS_Result> ObtenerBateria(int? pIdBateria = null, int? pFlBateria = null, String pIdCandidato = null, String pCorreoElectronico = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                List<SPE_OBTIENE_BATERIA_PRUEBAS_Result> retorno = new List<SPE_OBTIENE_BATERIA_PRUEBAS_Result>();
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                retorno = context.SPE_OBTIENE_BATERIA_PRUEBAS(pIdBateria, pFlBateria, pIdCandidato, pCorreoElectronico).ToList();
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return retorno;
            }
        }              
       
        public List<SPE_OBTIENE_PRUEBA_POR_PUESTO_Result> Obtener_PRUEBA_POR_PUESTO(int? pIdPuesto = null, decimal? pNoValorCompetencia = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from V_K_PRUEBA in context.SPE_OBTIENE_PRUEBA_POR_PUESTO(pIdPuesto, pNoValorCompetencia)
                        select V_K_PRUEBA;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }
        }
                                    
        public XElement InsertaActualiza_K_PRUEBA(string tipo_transaccion,int pID_PRUEBA ,SPE_OBTIENE_K_PRUEBA_Result V_K_PRUEBA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_INSERTA_ACTUALIZA_K_PRUEBA(pout_clave_retorno,pID_PRUEBA ,V_K_PRUEBA.ID_PRUEBA_PLANTILLA, V_K_PRUEBA.ID_CANDIDATO, V_K_PRUEBA.ID_EMPLEADO, V_K_PRUEBA.CL_EMPLEADO, V_K_PRUEBA.CL_ESTADO,V_K_PRUEBA.FE_INICIO,V_K_PRUEBA.FE_TERMINO,V_K_PRUEBA.NO_TIEMPO, usuario, usuario, programa, programa, tipo_transaccion,V_K_PRUEBA.NB_TIPO_PRUEBA);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement InsertaActualizaPruebasBateria(int pID_BATERIA, string pXmlPruebas, string usuario, string programa, string clTipoTransaccion)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ACTUALIZA_PRUEBAS_BATERIA(pout_clave_retorno, pID_BATERIA, pXmlPruebas, usuario, programa, clTipoTransaccion);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement CorrigePrueba(string tipo_transaccion, int pID_PRUEBA, SPE_OBTIENE_K_PRUEBA_Result V_K_PRUEBA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ACTUALIZA_K_PRUEBA_CORREGIDA(pout_clave_retorno, pID_PRUEBA, V_K_PRUEBA.ID_PRUEBA_PLANTILLA, V_K_PRUEBA.ID_CANDIDATO, V_K_PRUEBA.ID_EMPLEADO, V_K_PRUEBA.CL_EMPLEADO, V_K_PRUEBA.CL_ESTADO, V_K_PRUEBA.FE_INICIO, V_K_PRUEBA.FE_TERMINO, V_K_PRUEBA.NO_TIEMPO, usuario, usuario, programa, programa, tipo_transaccion, V_K_PRUEBA.NB_TIPO_PRUEBA);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement Elimina_K_PRUEBA(int? ID_PRUEBA_PLANTILLA = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ELIMINA_K_PRUEBA(pout_clave_retorno, ID_PRUEBA_PLANTILLA, usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        
        public XElement Inicia_K_PRUEBA(int pIdPrueba, DateTime pFeInicio, Guid pClTokenExterno, string usuario, string programa, int? pIdCandidato = null, int? pIdEmpleado = null, int? pIdPruebaPlantilla = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_INICIA_CUESTIONARIO_PRUEBA(pout_clave_retorno, pIdEmpleado, pIdCandidato, pIdPrueba, pIdPruebaPlantilla, pFeInicio,pClTokenExterno, usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminarRespuestasPrueba(int pIdPrueba, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ELIMINA_RESPUESTAS_PRUEBA(pout_clave_retorno, pIdPrueba, pClUsuario, pNbPrograma);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
            }
        }

        public XElement EliminarPruebaBateria(string pXmlPruebas = null, int? pIdBateria = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ELIMINA_PRUEBA_RESPUESTAS(pout_clave_retorno, pXmlPruebas, pIdBateria, pClUsuario, pNbPrograma);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement EliminarRespuestasBateria(int pIdBateria, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ELIMINA_RESPUESTAS_BATERIA(pout_clave_retorno, pIdBateria, pClUsuario, pNbPrograma);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

       ////////////////////////////////////////C_PRUEBA_SECCION////////////////////////////////////////////        
       
        public List<SPE_OBTIENE_C_PRUEBA_SECCION_Result> Obtener_C_PRUEBA_SECCION(int? pIdPruebaTiempo = null, int? pIdPrueba = null, string pClPruebaTiempo = null, string pNbPruebaTiempo = null, short? pNoTiempo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from V_C_PRUEBA_TIEMPO in context.SPE_OBTIENE_C_PRUEBA_SECCION(pIdPruebaTiempo,pIdPrueba, pClPruebaTiempo, pNbPruebaTiempo, pNoTiempo)
                        select V_C_PRUEBA_TIEMPO;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }
        }
               
        public XElement InsertaActualiza_C_PRUEBA_SECCION(string tipo_transaccion, E_PRUEBA_TIEMPO  V_C_PRUEBA_TIEMPO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_INSERTA_ACTUALIZA_K_PRUEBA_SECCION(pout_clave_retorno,V_C_PRUEBA_TIEMPO.ID_PRUEBA_SECCION ,V_C_PRUEBA_TIEMPO.ID_PRUEBA, V_C_PRUEBA_TIEMPO.CL_PRUEBA_SECCION, V_C_PRUEBA_TIEMPO.NB_PRUEBA_SECCION, V_C_PRUEBA_TIEMPO.NO_TIEMPO,V_C_PRUEBA_TIEMPO.CL_ESTADO,V_C_PRUEBA_TIEMPO.FE_INICIO,V_C_PRUEBA_TIEMPO.FE_TERMINO,usuario, usuario, programa, programa, tipo_transaccion);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
                
        public XElement Elimina_C_PRUEBA_SECCION(int? ID_PRUEBA_TIEMPO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ELIMINA_C_PRUEBA_SECCION(pout_clave_retorno, ID_PRUEBA_TIEMPO, usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
                
        public XElement Inicia_K_PRUEBA_SECCION(int? pIdPrueba, DateTime pFeInicio, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_INICIA_CUESTIONARIO_PRUEBA_SECCION(pout_clave_retorno, pIdPrueba, pFeInicio, usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
                
        public XElement ActualizaTiempoPruebaSeccion(String pXmlTiempo, string pTipoTransaccion, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ACTUALIZA_TIEMPO_PRUEBA_SECCION(pout_clave_retorno, pXmlTiempo, pTipoTransaccion, usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }        

        ///////////////////////////////////////K_PRUEBA_SECCION////////////////////////////////////////////
        
        public List<SPE_OBTIENE_K_PRUEBA_SECCION_Result> Obtener_K_PRUEBA_SECCION(int? pIdPruebaSeccion = null, int? pIdPrueba = null, String pCLPruebaSeccion = null, String pNbPruebaSeccion = null, int? pNoTiempo = null, String pClEstado = null, DateTime? pFeInicio = null, DateTime? pFeTermino = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from V_K_PRUEBA in context.SPE_OBTIENE_K_PRUEBA_SECCION(pIdPruebaSeccion, pIdPrueba, pCLPruebaSeccion, pNbPruebaSeccion, pNoTiempo, pClEstado, pFeInicio, pFeTermino)
                        select V_K_PRUEBA;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }
        }
        
        public XElement InsertaActualiza_K_PRUEBA_SECCION(string tipo_transaccion, E_PRUEBA_TIEMPO V_K_PRUEBA, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_INSERTA_ACTUALIZA_K_PRUEBA_SECCION(pout_clave_retorno, V_K_PRUEBA.ID_PRUEBA_SECCION, V_K_PRUEBA.ID_PRUEBA, V_K_PRUEBA.CL_PRUEBA_SECCION, V_K_PRUEBA.NB_PRUEBA_SECCION, V_K_PRUEBA.NO_TIEMPO,V_K_PRUEBA.CL_ESTADO, V_K_PRUEBA.FE_INICIO, V_K_PRUEBA.FE_TERMINO, usuario, usuario, programa, programa, tipo_transaccion);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        
        public XElement Elimina_K_PRUEBA_SECCION(int? ID_PRUEBA_SECCION = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ELIMINA_K_PRUEBA_SECCION(pout_clave_retorno, ID_PRUEBA_SECCION, usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        
        ///////////////////////////////////////OBTENER BATERIAS PRUEBAS///////////////////////////////////////////
        
        //public List<SPE_OBTIENE_BATERIA_PRUEBAS_Result> Obtener_BATERIAS_PRUEBAS(int? pIdBateria = null, int? pFlBateria = null, String pIdCandidato = null, String pCorreoElectronico = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        var q = from V_C_PRUEBA in context.SPE_OBTIENE_BATERIA_PRUEBAS(pIdBateria, pFlBateria, pIdCandidato, pCorreoElectronico)
        //                select V_C_PRUEBA;
        //        return q.ToList();
        //    }
        //}

        public XElement registra_EnvioBateria(XElement pBaterias, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ACTUALIZA_BATERIAS_ENVIADAS(pout_clave_retorno, pBaterias.ToString(), usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_VARIABLES_BAREMOS_Result> obtenerVariablesBaremos(int ID_BATERIA)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from a in context.SPE_OBTIENE_VARIABLES_BAREMOS(ID_BATERIA)
                        select a;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }
        }

        public List<SPE_OBTIENE_VARIABLES_BAREMOS_SUSTITUCION_Result> obtenerBaremosSustitucion(int ID_BATERIA)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from a in context.SPE_OBTIENE_VARIABLES_BAREMOS_SUSTITUCION(ID_BATERIA)
                        select a;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }
        }

        public XElement insertaVariablesBaremos(int ID_BATERIA, string XML_BAREMOS, string CL_USUARIO, string NB_PROGRAMA)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_INSERTA_VARIABLES_BAREMOS(pout_clave_retorno, ID_BATERIA, XML_BAREMOS, CL_USUARIO, NB_PROGRAMA);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

        public XElement generaVariablesBaremos(int ID_BATERIA, string CL_USUARIO, string NB_PROGRAMA)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_GENERA_VARIABLES_BAREMOS(pout_clave_retorno, ID_BATERIA, CL_USUARIO, NB_PROGRAMA);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }

       ///////////////////////////RELACION PRUEBAS FACTORES////////////////////////////////////////////////

        public List<SPE_OBTIENE_PRUEBAS_FACTORES_COMPETENCIAS_Result> ObtenerPruebasFactores(int? pID_SELECCION = null, string pCL_SELECCION = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from a in context.SPE_OBTIENE_PRUEBAS_FACTORES_COMPETENCIAS(pID_SELECCION,pCL_SELECCION)
                        select a;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.ToList();
            }
        }
       
        public XElement InsertaCompetenciaFactor( string  pXML_COMPETENCIAS = null, int? pID_FACTOR = null, int? pID_PRUEBA = null, string pCL_USUARIO = null, string pNB_PROGRAMA = null )
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_INSERTA_ACTUALIZA_COMPETENCIA_FACTOR(poutClaveRetorno,pXML_COMPETENCIAS,pID_FACTOR,pID_PRUEBA,pCL_USUARIO,pNB_PROGRAMA);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }
       
        public XElement EliminarCompetenciaFactor(int pID_FACTOR,string pXML_COMPETENCIAS,string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                context.SPE_ELIMINA_COMPETENCIA_FACTOR(poutClaveRetorno, pID_FACTOR, pXML_COMPETENCIAS, usuario, programa);
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result ObtenerPruebasResultadosCandidatos(int? pIdBateria = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from a in context.SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO(pIdBateria)
                        select a;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.FirstOrDefault();
            }
        }

        public SPE_OBTIENE_FACTOR_POR_PRUEBA_Result ObtenerPruebasFactor(string pClPrueba = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 6000;
                var q = from a in context.SPE_OBTIENE_FACTOR_POR_PRUEBA(pClPrueba)
                        select a;
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 30;
                return q.FirstOrDefault();
            }
        }
       
    }
}
