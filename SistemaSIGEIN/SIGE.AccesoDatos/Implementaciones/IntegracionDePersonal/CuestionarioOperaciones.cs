
using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
   public class CuestionariosOperaciones
    {
       private SistemaSigeinEntities context;

        #region OBTIENE DATOS  C_CUESTIONARIO
        public List<SPE_OBTIENE_C_CUESTIONARIO_Result> Obtiene_C_CUESTIONARIO
            (int? pIdCuestionario = null, string pClCuestionario = null, string pNbCuestionario = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_CUESTIONARIO in context.SPE_OBTIENE_C_CUESTIONARIO(pIdCuestionario, pClCuestionario, pNbCuestionario)
                        select V_C_CUESTIONARIO;
                return q.ToList();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_CUESTIONARIO
        public XElement InsertaActualiza_C_CUESTIONARIO(string tipo_transaccion, SPE_OBTIENE_C_CUESTIONARIO_Result V_C_CUESTIONARIO, string usuario, string programa)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_C_CUESTIONARIO(pout_clave_retorno, V_C_CUESTIONARIO.ID_CUESTIONARIO, V_C_CUESTIONARIO.CL_CUESTIONARIO, V_C_CUESTIONARIO.NB_CUESTIONARIO, usuario, usuario, programa, programa, tipo_transaccion);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  C_CUESTIONARIO
        public XElement Elimina_C_CUESTIONARIO(int? pIdCuestionario = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_CUESTIONARIO(pout_clave_retorno, pIdCuestionario, usuario, programa);
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
        #endregion

        #region OBTENER DATOS K_PREGUNTA

        public List<SPE_OBTIENE_K_PREGUNTA_Result> Obtiene_K_PREGUNTA
        (int? pIdPregunta = null, string pClPregunta = null, string pNbPregunta = null, string pDsPregunta = null, string pClTipoPregunta = null, decimal? pNoValor = null, bool? pFgRequerido = null, bool? pFgActivo = null, int? pIdCompetencia = null, int? pIdBitacora = null, int? pIdPrueba = null,int? pIdCuestionario=null,Guid? pClTokenExterno =null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from V_C_PREGUNTAS in context.SPE_OBTIENE_K_PREGUNTA(pIdPregunta, pClPregunta, pNbPregunta,pDsPregunta,pClTipoPregunta,pNoValor,pFgRequerido,pFgActivo,pIdCompetencia,pIdBitacora,pIdPrueba,pIdCuestionario,pClTokenExterno)
                        select V_C_PREGUNTAS;
                return q.ToList();
            }
        }
        #endregion

    }
}
