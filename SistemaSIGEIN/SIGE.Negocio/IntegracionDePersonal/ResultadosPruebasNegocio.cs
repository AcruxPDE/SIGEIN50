using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades;
using SIGE.Entidades.IntegracionDePersonal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.Administracion
{
   public class ResultadosPruebasNegocio
    {
       #region obtiene datos resultados pruebas  k_Resultadp
       public String Obtener_ResultadosBaterias(int? pIdBateria = null, Guid? pClTokenBateria = null)
       {
           ResultadosPruebasOperaciones operaciones = new ResultadosPruebasOperaciones();
           return operaciones.Obtiene_Resultados_pruebas(pIdBateria, pClTokenBateria); //fe_creacion, fe_modificacion, cl_usuario_app_crea, cl_usuario_app_modifica, nb_programa_crea, nb_programa_modifica);
       }
       #endregion

        #region OBTIENE RESULTADOS PRUEBAS/BAREMOS

       public List<SPE_OBTIENE_FACTORES_PRUEBAS_Result> ObtieneFactoresPruebas(int? pIdVariable = null, int? pDsFactor = null)
       {
           ResultadosPruebasOperaciones operaciones = new ResultadosPruebasOperaciones();
           return operaciones.ObtieneFactoresPruebas(pIdVariable, pDsFactor); 
       }

       public List<E_RESULTADOS_PRUEBAS_REPORTE> ObtieneResultadosReporte(int? pIdPeriodo = null)
       {
           ResultadosPruebasOperaciones operaciones = new ResultadosPruebasOperaciones();
           var vResultados = operaciones.ObtieneResultadosReporte(pIdPeriodo);
           return (from c in vResultados
                   select new E_RESULTADOS_PRUEBAS_REPORTE
                   {
                            ID_BATERIA = c.ID_BATERIA,
                            CL_PRUEBA = c.CL_PRUEBA,
                            NB_PRUEBA = c.NB_PRUEBA,
                            NO_VALOR = (decimal)c.NO_VALOR,
                            ID_CUESTIONARIO = c.ID_CUESTIONARIO,
                            CL_VARIABLE = c.CL_VARIABLE

                   }).ToList();
       }

       public List<E_BAREMOS_PRUEBAS_RESPORTE> ObtieneResultadosBaremosReporte(int? pIdPeriodo = null)
       {
           ResultadosPruebasOperaciones operaciones = new ResultadosPruebasOperaciones();
           var vResultados = operaciones.ObtieneResultadosBaremosReporte(pIdPeriodo);
           return (from c in vResultados
                   select new E_BAREMOS_PRUEBAS_RESPORTE
                   {
                            ID_FACTOR = c.ID_FACTOR,
                            CL_FACTOR = c.CL_FACTOR,
                            NB_FACTOR = c.NB_FACTOR,
                            DS_FACTOR = c.DS_FACTOR,
                            ID_VARIABLE = c.ID_VARIABLE,
                            CL_VARIABLE = c.CL_VARIABLE,
                            NB_PRUEBA = c.NB_PRUEBA,
                            CL_TIPO_VARIABLE = c.CL_TIPO_VARIABLE,
                            CL_TIPO_RESULTADO = c.CL_TIPO_RESULTADO,
                            NO_VALOR = c.NO_VALOR,
                            ID_CUESTIONARIO = c.ID_CUESTIONARIO,
                            ID_BATERIA = c.ID_BATERIA
                   }).ToList();
           
       }

        #endregion

    }
}
