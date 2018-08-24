using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; // reemplazar por la carpeta correspondiente
using SIGE.Entidades.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;


namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class CuestionarioPreguntaNegocio
    {
        public List<SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_Result> Obtener_K_CUESTIONARIO_PREGUNTA(int? ID_CUESTIONARIO_PREGUNTA = null, int? ID_CUESTIONARIO = null, int? ID_PREGUNTA = null, String NB_PREGUNTA = null, String NB_RESPUESTA = null, Decimal? NO_VALOR_RESPUESTA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
        {
            CuestionarioPreguntaOperaciones operaciones = new CuestionarioPreguntaOperaciones();
            return operaciones.Obtener_K_CUESTIONARIO_PREGUNTA(ID_CUESTIONARIO_PREGUNTA, ID_CUESTIONARIO, ID_PREGUNTA, NB_PREGUNTA, NB_RESPUESTA, NO_VALOR_RESPUESTA, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
        }

        public E_RESULTADO InsertaActualiza_K_CUESTIONARIO_PREGUNTA(string tipo_transaccion, int? pIdEvaluado, int? pIdEvaluador, int? pIdCuestionarioPregunta, int? pIdCuestionario, string XML_CUESTIONARIO, string pNbPrueba, string usuario, string programa)
        {
            CuestionarioPreguntaOperaciones operaciones = new CuestionarioPreguntaOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion, pIdEvaluado, pIdEvaluador, pIdCuestionarioPregunta, pIdCuestionario, XML_CUESTIONARIO, pNbPrueba, usuario, programa));
        }

        public List<SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_PPRUEBA_Result> Obtener_K_CUESTIONARIO_PREGUNTA_PRUEBA(int? ID_PRUEBA = null, Guid? CL_TOKEN = null)
        {
            CuestionarioPreguntaOperaciones operaciones = new CuestionarioPreguntaOperaciones();
            return operaciones.Obtener_K_CUESTIONARIO_PREGUNTA_PRUEBA(ID_PRUEBA, CL_TOKEN);
        }

        //#region ELIMINA DATOS  K_CUESTIONARIO_PREGUNTA
        //public int Elimina_K_CUESTIONARIO_PREGUNTA(int? ID_CUESTIONARIO_PREGUNTA = null, string usuario = null, string programa = null)
        //{
        //    CuestionarioPreguntaOperaciones operaciones = new CuestionarioPreguntaOperaciones();
        //    return operaciones.Elimina_K_CUESTIONARIO_PREGUNTA(ID_CUESTIONARIO_PREGUNTA, usuario, programa);
        //}
        //#endregion

        

        
    }
}
