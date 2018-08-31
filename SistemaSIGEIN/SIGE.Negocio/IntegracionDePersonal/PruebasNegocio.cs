using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.Administracion
{
    public class PruebasNegocio
    {
        /////////////////////////////////////////C_PRUEBA///////////////////////////////////////////////////////

        public List<SPE_OBTIENE_C_PRUEBA_Result> Obtener_C_PRUEBA(int? pIdPrueba = null, string pClPrueba = null, string pNbPrueba = null, int? pIdCuestionario = null, short? pNoTiempo = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return operaciones.Obtener_C_PRUEBA(pIdPrueba, pClPrueba, pNbPrueba, pIdCuestionario, pNoTiempo); //fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
        }

        public List<SPE_OBTIENE_CANDIDATOS_BATERIA_MASIVA_Result> ObtenerCandidatoFolio(string pNbCandidato = null, string pNbPaterno = null, string pNbMaterno = null )
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return operaciones.ObtenerCandidatoFolio(pNbCandidato, pNbPaterno, pNbMaterno);
        }


        public List<SPE_OBTIENE_PRUEBAS_CONFIGURADAS_Result> ObtenerPruebasConfiguradas(int? pID_BATERIA = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return operaciones.ObtenerPruebasConfiguradas(pID_BATERIA);

        }   

        public E_RESULTADO InsertaActualiza_C_PRUEBA(string tipo_transaccion, SPE_OBTIENE_C_PRUEBA_Result v_c_prueba, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_PRUEBA(tipo_transaccion, v_c_prueba, usuario, programa));
        }

        public E_RESULTADO Elimina_C_PRUEBA(int? pIdPrueba = null, string usuario = null, string programa = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_PRUEBA(pIdPrueba, usuario, programa));
        }


        ///////////////////////////////////K_PRUEBA////////////////////////////////////////////

        public List<SPE_OBTIENE_K_PRUEBA_Result> Obtener_K_PRUEBA(Guid? pClTokenExterno = null, int? pIdPrueba = null, int? pIdPruebaPlantilla = null, int? pIdCandidato = null, int? pIdEmpleado = null, String pClEmpleado = null, String pClEstado = null, DateTime? pFeInicio = null, DateTime? pFeTermino = null, int? pNoTiempo = null, int? pIdBateria = null, Guid? pClTokenBateria = null, bool? pFgAsignada = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return operaciones.Obtener_K_PRUEBA(pClTokenExterno, pIdPrueba, pIdPruebaPlantilla, pIdCandidato, pIdEmpleado, pClEmpleado, pClEstado, pFeInicio, pFeTermino, pNoTiempo, pIdBateria, pClTokenBateria, pFgAsignada); //fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
        }

        public List<SPE_OBTIENE_RESULTADO_PRUEBA_Result> Obtener_RESULTADO_PRUEBA(int pIdPrueba, Guid pClTokenExterno)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return operaciones.Obtener_RESULTADO_PRUEBA(pIdPrueba, pClTokenExterno);
        }

        public List<SPE_OBTIENE_BATERIA_PRUEBAS_Result> ObtieneBateria(int? pIdBateria = null, int? pFlBateria = null, String pIdCandidato = null, String pCorreoElectronico = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return operaciones.ObtenerBateria(pIdBateria, pFlBateria, pIdCandidato, pCorreoElectronico);
        }

        public List<SPE_OBTIENE_PRUEBA_POR_PUESTO_Result> Obtener_PRUEBA_POR_PUESTO(int? pIdPuesto = null, decimal? pNoValorCompetencia = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return operaciones.Obtener_PRUEBA_POR_PUESTO(pIdPuesto, pNoValorCompetencia);
        }

        public E_RESULTADO InsertaActualiza_K_PRUEBA(string tipo_transaccion, int pID_PRUEBA, SPE_OBTIENE_K_PRUEBA_Result v_k_prueba, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_K_PRUEBA(tipo_transaccion, pID_PRUEBA, v_k_prueba, usuario, programa));
        }

        public E_RESULTADO InsertaActualizaPruebasBateria(int pID_BATERIA, string pXmlPruebas, string usuario, string programa, string clTipoTransaccion)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaPruebasBateria(pID_BATERIA, pXmlPruebas, usuario, programa, clTipoTransaccion));
        }

        public E_RESULTADO CorrigePrueba(string tipo_transaccion, int pID_PRUEBA, SPE_OBTIENE_K_PRUEBA_Result v_k_prueba, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.CorrigePrueba(tipo_transaccion, pID_PRUEBA, v_k_prueba, usuario, programa));
        }

        public E_RESULTADO registra_EnvioBateria(XElement pBaterias, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.registra_EnvioBateria(pBaterias, usuario, programa));
        }

        public E_RESULTADO EliminaRespuestasPrueba(int pIdPrueba, string pClUsuario, string pNbPrograma)
        {
            PruebasOperaciones oPruebas = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPruebas.EliminarRespuestasPrueba(pIdPrueba, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminarPruebaBateria(string pXmlPruebas = null, int? pIdBateria = null, string pClUsuario = null, string pNbPrograma = null)
        {
            PruebasOperaciones oPruebas = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPruebas.EliminarPruebaBateria(pXmlPruebas, pIdBateria, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaBateriaPruebas(string pXmlBaterias = null, string pNbUsuario = null, string pNbPrograma = null)
        {
            PruebasOperaciones oPruebas = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPruebas.EliminaBateriaPruebas(pXmlBaterias, pNbUsuario, pNbPrograma));
        }

        public E_RESULTADO EliminaRespuestasBaterias(int pIdBateria, string pClUsuario, string pNbPrograma)
        {
            PruebasOperaciones oPruebas = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPruebas.EliminarRespuestasBateria(pIdBateria, pClUsuario, pNbPrograma));
        }

        ///////////////////////////////////CREACION DE PRUEBAS PARA LOS CANDIDATOS O EMPLEADOS////////////////////////////////////////////
        
        public E_RESULTADO generarPruebasEmpleado(XElement vEmpleados, XElement vPruebas, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.generarPruebasEmpleado(vEmpleados, vPruebas, usuario, programa));
        }

        public E_RESULTADO ActualizarPruebasEmpleado(XElement vPruebas, int? pIdBateria, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarPruebasEmpleado(vPruebas, pIdBateria, usuario, programa));
        }
                
        public E_RESULTADO Elimina_K_PRUEBA(int? pIdPruebaPlantilla = null, string usuario = null, string programa = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_K_PRUEBA(pIdPruebaPlantilla, usuario, programa));
        }
                
        public E_RESULTADO INICIAR_K_PRUEBA(int pIdPrueba, DateTime pFeInicio, Guid pClTokenExterno, string usuario, string programa, int? pIdCandidato = null, int? pIdEmpleado = null, int? pIdPruebaPlantilla = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Inicia_K_PRUEBA(pIdPrueba, pFeInicio, pClTokenExterno, usuario, programa, pIdCandidato, pIdEmpleado, pIdPruebaPlantilla));
        }        

        ///////////////////////////////////C_PRUEBA_SECCION////////////////////////////////////////////
        
        public List<SPE_OBTIENE_C_PRUEBA_SECCION_Result> Obtener_C_PRUEBA_SECCION(int? pIdPruebaTiempo = null, int? pIdPrueba = null, string pClPrueba = null, string pNbPrueba = null, short? pNoTiempo = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return operaciones.Obtener_C_PRUEBA_SECCION(pIdPruebaTiempo, pIdPrueba, pClPrueba, pNbPrueba, pNoTiempo); //fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
        }
        
        public E_RESULTADO InsertaActualiza_C_PRUEBA_SECCION(string tipo_transaccion, E_PRUEBA_TIEMPO v_c_prueba, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_C_PRUEBA_SECCION(tipo_transaccion, v_c_prueba, usuario, programa));
        }
        
        public E_RESULTADO Elimina_C_PRUEBA_SECCION(int? pIdPrueba = null, string usuario = null, string programa = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_C_PRUEBA_SECCION(pIdPrueba, usuario, programa));
        }
        
        public E_RESULTADO INICIAR_K_PRUEBA_SECCION(int? pIdPrueba, DateTime pFeInicio, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Inicia_K_PRUEBA_SECCION(pIdPrueba, pFeInicio, usuario, programa));
        }
        
        public E_RESULTADO ActualizaTiempoPruebaSeccion(string pXmlTiempo, string pTipoTransaccion, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizaTiempoPruebaSeccion(pXmlTiempo, pTipoTransaccion, usuario, programa));
        }
        
        ///////////////////////////////////K_PRUEBA_SECCION////////////////////////////////////////////
        
        public List<SPE_OBTIENE_K_PRUEBA_SECCION_Result> Obtener_K_PRUEBA_SECCION(int? pIdPruebaSeccion = null, int? pIdPrueba = null, String pCLPruebaSeccion = null, String pNbPruebaSeccion = null, int? pNoTiempo = null, String pClEstado = null, DateTime? pFeInicio = null, DateTime? pFeTermino = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return operaciones.Obtener_K_PRUEBA_SECCION(pIdPruebaSeccion, pIdPrueba, pCLPruebaSeccion, pNbPruebaSeccion, pNoTiempo, pClEstado, pFeInicio, pFeTermino);
        }
        
        public E_RESULTADO InsertaActualiza_K_PRUEBA_SECCION(string tipo_transaccion, E_PRUEBA_TIEMPO v_k_prueba, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_K_PRUEBA_SECCION(tipo_transaccion, v_k_prueba, usuario, programa));
        }

        public E_RESULTADO Elimina_K_PRUEBA_SECCION(int? pIdPruebaSeccion = null, string usuario = null, string programa = null)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_K_PRUEBA_SECCION(pIdPruebaSeccion, usuario, programa));
        }
        
        ///////////////////////////////////OBTENER BATERIAS PRUEBAS////////////////////////////////////////////
        
        //public List<SPE_OBTIENE_BATERIA_PRUEBAS_Result> Obtener_BATERIA_PRUEBAS(int? pIdBateria = null, int? pFlBateria = null, String pIdCandidato = null, String pCorreoElectronico = null)
        //{
        //    PruebasOperaciones operaciones = new PruebasOperaciones();
        //    return operaciones.Obtener_BATERIAS_PRUEBAS(pIdBateria, pFlBateria, pIdCandidato, pCorreoElectronico); //fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
        //}
        
        public List<SPE_OBTIENE_VARIABLES_BAREMOS_Result> obtenerVariableBaremos(int ID_BATERIA)
        {
            PruebasOperaciones op = new PruebasOperaciones();
            return op.obtenerVariablesBaremos(ID_BATERIA);
        }

        public List<SPE_OBTIENE_VARIABLES_BAREMOS_SUSTITUCION_Result> obtenerBaremosSustitucion(int ID_BATERIA)
        {
            PruebasOperaciones op = new PruebasOperaciones();
            return op.obtenerBaremosSustitucion(ID_BATERIA);
        }

        public E_RESULTADO insertaVariablesBaremos(int ID_BATERIA, string XML_BAREMOS, string CL_USUARIO, string NB_PROGRAMA)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.insertaVariablesBaremos(ID_BATERIA, XML_BAREMOS, CL_USUARIO, NB_PROGRAMA));
        }

        public E_RESULTADO generaVariablesBaremos(int ID_BATERIA, string CL_USUARIO, string NB_PROGRAMA)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.generaVariablesBaremos(ID_BATERIA, CL_USUARIO, NB_PROGRAMA));
        }

        ///////////////////////////RELACION PRUEBAS FACTORES////////////////////////////////////////////////

        public List<SPE_OBTIENE_PRUEBAS_FACTORES_COMPETENCIAS_Result> ObtienePruebasFactores(int? pID_SELECCION = null, string pCL_SELECCION = null)
        {
            PruebasOperaciones op = new PruebasOperaciones();
            return op.ObtenerPruebasFactores(pID_SELECCION, pCL_SELECCION);
        }

        public E_RESULTADO InsertaCompetenciaFactor(string pXML_COMPETENCIAS, int pID_FACTOR, int pID_PRUEBA, string pCL_USUARIO, string pNB_PROGRAMA)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaCompetenciaFactor(pXML_COMPETENCIAS, pID_FACTOR, pID_PRUEBA, pCL_USUARIO, pNB_PROGRAMA));
        }

        public E_RESULTADO EliminaCompetenciaFactor(int pID_FACTOR, string pXmlCompetencias, string usuario, string programa)
        {
            PruebasOperaciones operaciones = new PruebasOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.EliminarCompetenciaFactor(pID_FACTOR, pXmlCompetencias, usuario, programa));
        }

        public SPE_OBTIENE_BATERIA_PRUEBAS_CANDIDATO_Result ObtenienePruebasResultadosCandidatos(int? pIdBateria = null)
        {
            PruebasOperaciones op = new PruebasOperaciones();
            return op.ObtenerPruebasResultadosCandidatos(pIdBateria);
        }

        public SPE_OBTIENE_FACTOR_POR_PRUEBA_Result ObtenienePruebasFactor(string pClPrueba = null)
        {
            PruebasOperaciones op = new PruebasOperaciones();
            return op.ObtenerPruebasFactor(pClPrueba);
        }


    }
}
