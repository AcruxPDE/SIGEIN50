using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
    public class ResultadosPruebasOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE DATOS K_RESULTADO POR BATERIA
        public String Obtiene_Resultados_pruebas
            (int? pIdBateria = null, Guid? pClTokenBateria = null)
        {
            List<E_RESULTADOS_BATERIA> vResultadosBaterias = new List<E_RESULTADOS_BATERIA>();
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter xml = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                xml.Value = "";
                context.SPE_OBTIENE_RESULTADOS_BATERIA(xml, pIdBateria, pClTokenBateria);
                return xml.Value.ToString();

            }
        }
        #endregion

        #region OBTIENE RESULTADOS PRUEBAS/BAREMOS

        public List<SPE_OBTIENE_FACTORES_PRUEBAS_Result> ObtieneFactoresPruebas(int? pIdVariable = null, int? pDsFactor = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_FACTORES_PRUEBAS(pIdVariable, pDsFactor).ToList();
            }
        }

        public List<SPE_OBTIENE_RESULTADOS_PRUEBAS_REPORTE_Result> ObtieneResultadosReporte(int? pIdPeriodo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_RESULTADOS_PRUEBAS_REPORTE(pIdPeriodo).ToList();
            }
        }

        public List<SPE_OBTIENE_RESULTADOS_BAREMOS_PRUEBAS_Result> ObtieneResultadosBaremosReporte(int? pIdPeriodo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_RESULTADOS_BAREMOS_PRUEBAS(pIdPeriodo).ToList();
            }
        }

        #endregion



    }
}
