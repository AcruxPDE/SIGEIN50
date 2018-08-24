using SIGE.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
    public class ConsultaPersonalOperaciones
    {
        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_CONSULTA_PERSONAL_RESUMEN_Result> obtieneConsultaPersonalResumen(int ID_BATERIA, bool vFgConsultaparcial)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from a in context.SPE_OBTIENE_CONSULTA_PERSONAL_RESUMEN(ID_BATERIA, vFgConsultaparcial)
                        select a;
                return q.ToList();
            }
        }

        public List<SPE_OBTIENE_CONSULTA_PERSONAL_DETALLADA_Result> obtieneConsultaPersonalDetallada(int ID_BATERIA)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from a in context.SPE_OBTIENE_CONSULTA_PERSONAL_DETALLADA(ID_BATERIA)
                        select a;
                return q.ToList();
            }
        }

        public List<SPE_OBTIENE_C_FACTOR_Result> obtieneFactores(int? ID_FACTOR = null, string CL_FACTOR = null, string NB_FACTOR = null, string DS_FACTOR = null, int? ID_VARIABLE = null, string NB_ABREVIATURA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from a in context.SPE_OBTIENE_C_FACTOR(ID_FACTOR, CL_FACTOR, NB_FACTOR, DS_FACTOR, ID_VARIABLE, NB_ABREVIATURA)
                        select a;

                return q.ToList();
            }
        }
        public List<SPE_OBTIENE_FACTORES_CONSULTA_Result> obtieneFactoresConsulta()
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from a in context.SPE_OBTIENE_FACTORES_CONSULTA()
                        select a;
                return q.ToList();
            }
        }
        public List<SPE_OBTIENE_COMPETENCIAS_CONSULTA_Result> obtieneCompetenciasConsulta()
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from a in context.SPE_OBTIENE_COMPETENCIAS_CONSULTA()
                        select a;
                return q.ToList();
            }
        }
        public List<SPE_OBTIENE_FACTORES_COMPETENCIAS_Result> obtieneFactoresCompetencias()
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from a in context.SPE_OBTIENE_FACTORES_COMPETENCIAS()
                        select a;
                return q.ToList();
            }
        }
    }
}
