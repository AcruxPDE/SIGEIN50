using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class PlanVidaCarreraNegocio
    {
        public List<SPE_OBTIENE_PLAN_VIDA_CARRERA_Result> obtieneDatosPlanVidaCarrera(int ID_PUESTO, int? ID_EMPRESA)
        {
            PlanVidaCarreraOperaciones op = new PlanVidaCarreraOperaciones();
            return op.obtenerDatosPlanVidaCarrera(ID_PUESTO, ID_EMPRESA);
        }

        //public List<E_COMPARACION_COMPETENCIA> obtieneComparacionCompetenciasPlanVidaCarrera(int ID_EMPLEADO, int ID_PERIODO, string XML_PUESTOS)
        //{
        //    PlanVidaCarreraOperaciones op = new PlanVidaCarreraOperaciones();
        //    return op.obtenerComparacionCompetenciasPlanVidaCarrera(ID_EMPLEADO, ID_PERIODO, XML_PUESTOS);
        //}

        public DataTable obtieneComparacionCompetenciasPlanVidaCarrera(int ID_EMPLEADO, int ID_PERIODO, string XML_PUESTOS, ref List<E_COMPARACION_COMPETENCIA> vLstPlanVidaCarrera, ref List<E_PROMEDIO_PLAN_VIDA_CARRERA> vLstPromedios)
        {
            //double vTotalElementos = 0;
            //double vSumaElementos = 0;
            //double vPromedio = 0;

            string vNbPorcentaje = "";
            string vClaseColor = "";
            string vDivClasificacion = "<div style=\"height: 80%; width: 20px; border-radius: 5px; border:1px solid black;  background:{0} ;\" ><br><br></div>";
            string vDivsCeldasPo = "<table class=\"tablaColor\"> " +
    "<tr> " +
    "<td class=\"porcentaje\"> " +
    "<div class=\"divPorcentaje\">{0}</div> " +
    "</td> " +
    "<td class=\"color\"> " +
    "<div class=\"{1}\">&nbsp;</div> " +
    "</td> </tr> </table>";

            PlanVidaCarreraOperaciones op = new PlanVidaCarreraOperaciones();
            vLstPlanVidaCarrera = op.obtenerComparacionCompetenciasPlanVidaCarrera(ID_EMPLEADO, ID_PERIODO, XML_PUESTOS);

            DataTable vDtPivot = new DataTable();

            vDtPivot.Columns.Add("ID_COMPETENCIA", typeof(int));
            //vDtPivot.Columns.Add("NO_ORDEN", typeof(int));
            vDtPivot.Columns.Add("CL_COLOR", typeof(string));
            vDtPivot.Columns.Add("NB_COMPETENCIA", typeof(string));
            vDtPivot.Columns.Add("DS_COMPETENCIA", typeof(string));

            var vLstPuestos = (from a in vLstPlanVidaCarrera select new { a.ID_PUESTO, a.NB_PUESTO }).Distinct();
            var vLstCompetencias = (from a in vLstPlanVidaCarrera select new { a.ID_COMPETENCIA, a.CL_COLOR, a.NB_COMPETENCIA, a.DS_COMPETENCIA, a.NO_ORDEN }).Distinct().OrderBy(t => t.NO_ORDEN);

            foreach (var item in vLstPuestos)
            {
                vLstPromedios.Add(new E_PROMEDIO_PLAN_VIDA_CARRERA { NB_PUESTO = item.ID_PUESTO.ToString() + "E" });
                vDtPivot.Columns.Add(item.ID_PUESTO.ToString() + "E");
            }

            foreach (var vComp in vLstCompetencias)
            {
                DataRow vDr = vDtPivot.NewRow();

                vDr["ID_COMPETENCIA"] = vComp.ID_COMPETENCIA;
                ///vDr["NO_ORDEN"] = vComp.NO_ORDEN;
                vClaseColor = string.Format(vComp.CL_COLOR);
                vDr["CL_COLOR"] = string.Format(vDivClasificacion, vClaseColor);
                vDr["NB_COMPETENCIA"] = vComp.NB_COMPETENCIA;
                vDr["DS_COMPETENCIA"] = vComp.DS_COMPETENCIA;

                foreach (var vPuesto in vLstPuestos)
                {
                    var vResultado = vLstPlanVidaCarrera.Where(t => t.ID_COMPETENCIA == vComp.ID_COMPETENCIA & t.ID_PUESTO == vPuesto.ID_PUESTO).FirstOrDefault();

                    if (vResultado != null)
                    {
                        if (vResultado.PR_COMPATIBILIDAD.Equals("-1.00"))
                        {
                            vDr[vPuesto.ID_PUESTO.ToString() + "E"] = string.Format(vDivsCeldasPo, "N/C", vResultado.CL_COLOR_COMPATIBILIDAD);
                        }
                        else
                        {
                            vLstPromedios.Where(t => t.NB_PUESTO == vPuesto.ID_PUESTO.ToString() + "E").FirstOrDefault().SumarElemento(vResultado.PR_NO_COMPATIBILIDAD.Value);
                            vNbPorcentaje = string.Format("{0:N2}%", vResultado.PR_COMPATIBILIDAD);
                            vDr[vPuesto.ID_PUESTO.ToString() + "E"] = string.Format(vDivsCeldasPo, vNbPorcentaje, vResultado.CL_COLOR_COMPATIBILIDAD);
                        }                        
                    }
                    else
                    {
                        vDr[vPuesto.ID_PUESTO.ToString() + "E"] = string.Format(vDivsCeldasPo, "N/A", "divGris");
                    }
                }


                vDtPivot.Rows.Add(vDr);
            }

            return vDtPivot;
        }

        public DataTable obtenerComparacionPuestos(string XML_PUESTOS = null, int? ID_EMPLEADO = null)
        {
            PlanVidaCarreraOperaciones op = new PlanVidaCarreraOperaciones();

            List<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA_Result> lista = new List<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA_Result>();


            lista = op.obtenerComparacionPuestos(XML_PUESTOS, ID_EMPLEADO);

            Utilerias.Utilerias aux = new Utilerias.Utilerias();

            return aux.ConvertToDataTable<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA_Result>(lista, true);

        }

        public List<string> obtenerPuestos(string XML_PUESTOS, int ID_EMPLEADO)
        {
            PlanVidaCarreraOperaciones op = new PlanVidaCarreraOperaciones();
            List<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA_Result> lista = new List<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_VIDA_CARRERA_Result>();

            List<string> puestos = new List<string>();

            lista = op.obtenerComparacionPuestos(XML_PUESTOS, ID_EMPLEADO);

            for (int i = 2; i <= lista.Count - 1; i++)
            {
                puestos.Add(lista[i].NB_EMPLEADO);
            }

            return puestos;
        }


        public List<SPE_OBTIENE_M_PUESTO_Result> ObtienePuestos(string XML_PUESTOS)
        {
            PlanVidaCarreraOperaciones operaciones = new PlanVidaCarreraOperaciones();
            return operaciones.ObtenerPuestos(xml_puestos: XML_PUESTOS);
        }

    }
}
