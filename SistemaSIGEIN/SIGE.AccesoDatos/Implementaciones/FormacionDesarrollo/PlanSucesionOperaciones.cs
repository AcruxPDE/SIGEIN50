using SIGE.Entidades;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo
{
    public class PlanSucesionOperaciones
    {
        SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_PLAN_SUCESION_Result> obtienePlanSucesion(int ID_EMPLEADO,int? ID_EMPRESA, string XML_PRIORIDADES, string XML_EMPLEADOS = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_PLAN_SUCESION(ID_EMPLEADO, XML_PRIORIDADES, XML_EMPLEADOS, ID_EMPRESA).ToList();
            }    

        }

        public List<E_COMPARACION_COMPETENCIA> obtieneComparacionCompetenciasPlanSucesion(string XML_EMPLEADOS, int? ID_PUESTO, int? ID_ROL)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                var listaCompetencias = contexto.SPE_OBTIENE_COMPARACION_COMPETENCIAS_PLAN_SUCESION(XML_EMPLEADOS, ID_PUESTO).ToList();
                var listaPuestos = contexto.SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_SUCESION(ID_PUESTO, XML_EMPLEADOS,ID_ROL).ToList();
                int maximo;

                if (listaCompetencias.Count() > 0)
                {
                    maximo = listaCompetencias.Max(t => t.NO_ORDEN) + 1;
                }
                else {
                    maximo = 1;
                }




                var source = (from a in listaCompetencias
                              select new E_COMPARACION_COMPETENCIA
                              {
                                  ID_PUESTO = a.ID_PUESTO,
                                  CL_PUESTO = a.CL_PUESTO,
                                  NB_PUESTO = a.NB_PUESTO,
                                  ID_EMPLEADO = a.ID_EMPLEADO,
                                  CL_EMPLEADO = a.CL_EVALUADO,
                                  NB_EMPLEADO = a.NB_EVALUADO,
                                  ID_COMPETENCIA = a.ID_COMPETENCIA,
                                  NB_COMPETENCIA = a.NB_COMPETENCIA,
                                  DS_COMPETENCIA = a.DS_COMPETENCIA,
                                  //NO_VALOR_NIVEL = a.NO_VALOR_NIVEL,
                                  //NO_RESULTADO_PROMEDIO = a.NO_RESULTADO_PROMEDIO,
                                  PR_COMPATIBILIDAD = a.PR_COMPATIBILIDAD.ToString(),
                                  PR_NO_COMPATIBILIDAD = a.PR_COMPATIBILIDAD,
                                  CL_COLOR = a.CL_COLOR,
                                  NO_ORDEN = a.NO_ORDEN,
                                  NO_ACIERTO = a.NO_CONTADOR,
                                  CL_TIPO_REGISTRO = "C"
                              }).ToList();

                foreach (SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_SUCESION_Result item in listaPuestos)
                {
                    E_COMPARACION_COMPETENCIA e = new E_COMPARACION_COMPETENCIA();

                    int vContador = listaCompetencias.Where(w => w.CL_EVALUADO == item.CL_EMPLEADO).Sum(s => s.NO_CONTADOR);

                    e.CL_EMPLEADO = item.CL_EMPLEADO;
                    e.NB_COMPETENCIA = item.CL_ITEM;
                    e.DS_COMPETENCIA = string.IsNullOrEmpty(item.DS_ITEM) ? "" : item.DS_ITEM;
                    e.NO_ORDEN = maximo;
                    e.CL_COLOR = "#F5F5F5";
                    e.CL_TIPO_REGISTRO = "P";

                    switch (item.ID_ITEM)
                    {
                        case 1:
                            e.PR_NO_COMPATIBILIDAD = item.FG_CUMPLE_EDAD;
                            e.NO_ACIERTO = item.FG_CUMPLE_EDAD;
                            e.PR_COMPATIBILIDAD = item.NO_EDAD;
                            break;

                        case 2:
                            e.PR_NO_COMPATIBILIDAD = item.FG_GENERO;
                            e.NO_ACIERTO = item.FG_GENERO;
                            e.PR_COMPATIBILIDAD = item.NB_GENERO;
                            break;

                        case 3:
                            e.PR_NO_COMPATIBILIDAD = item.FG_ESTADO_CIVIL;
                            e.NO_ACIERTO = item.FG_ESTADO_CIVIL;
                            e.PR_COMPATIBILIDAD = item.CL_ESTADO_CIVIL;
                            break;

                        case 4:
                            e.PR_NO_COMPATIBILIDAD = item.FG_PROFESIONAL;
                            e.NO_ACIERTO = item.FG_PROFESIONAL;
                            e.PR_COMPATIBILIDAD = item.NB_PROFESIONAL;
                            break;

                        case 5:
                            e.PR_NO_COMPATIBILIDAD = item.FG_POSTGRADO;
                            e.NO_ACIERTO = item.FG_POSTGRADO;
                            e.PR_COMPATIBILIDAD = item.NB_POSTGRADO;
                            break;

                        case 6:
                            e.PR_NO_COMPATIBILIDAD = item.FG_SUELDO;
                            e.NO_ACIERTO = item.FG_SUELDO;
                            e.PR_COMPATIBILIDAD = item.MN_SUELDO.ToString();
                            break;
                        case 7:
                            e.PR_NO_COMPATIBILIDAD = item.TOTAL + vContador;
                            e.NO_ACIERTO = 0;
                            e.PR_COMPATIBILIDAD = item.TOTAL.ToString();
                            break;

                        default:
                            break;
                    }

                    source.Add(e);
                }

                return source;

            }
        }

        public List<SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_SUCESION_Result> obtieneComparacionPuestosPlanSucesion(int ID_PUESTO, string XML_EMPLEADOS,int? ID_ROL)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_COMPARACION_PUESTOS_PLAN_SUCESION(ID_PUESTO, XML_EMPLEADOS,ID_ROL).ToList();
            }
        }

    }
}
