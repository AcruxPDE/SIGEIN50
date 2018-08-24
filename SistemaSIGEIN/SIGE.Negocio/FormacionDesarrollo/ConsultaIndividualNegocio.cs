using SIGE.AccesoDatos.Implementaciones.FormacionDesarrollo;
using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SIGE.Negocio.FormacionDesarrollo
{
    public class ConsultaIndividualNegocio
    {
        public List<SPE_OBTIENE_FYD_EVALUADOS_CONFIGURACION_Result> ObtenerEvaluados(int pIdPeriodo, int? pID_EMPRESA = null)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
            return op.ObtenerEvaluados(pIdPeriodo, pID_EMPRESA);
        }

        public SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result ObtenerPeriodoEvaluacion(int pIdPeriodo)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
            return op.ObtenerPeriodoEvaluacion(pIdPeriodo);
        }

        public List<E_PERIODO_EVALUACION> ObtenerPeriodosEvaluacion(int? pIdPeriodo = null)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
            return op.ObtenerPeriodosEvaluacion(pIdPeriodo);
        }

        //public List<E_REPORTE_GENERAL_INDIVIDUAL> ObtenerDatosReporteGeneralIndividual(int ID_PERIODO, int ID_EVALUADO)
        //{
        //    ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
        //    return op.ObtenerDatosReporteGeneralIndividual(ID_PERIODO, ID_EVALUADO);
        //}

        public List<E_REPORTE_360> ObtenerDatosReporte360(int ID_PERIODO, int ID_EVALUADO)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
           return op.ObtenerDatosReporte360(ID_PERIODO, ID_EVALUADO);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_COMPARATIVO_INDIVIDUAL_Result> ObtenerDatosReporteComparativoIndividual(int ID_EMPLEADO, string XML_PERIODOS)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
            return op.ObtenerDatosReporteComparativoIndividual(ID_EMPLEADO, XML_PERIODOS);
        }

        public List<SPE_OBTIENE_FYD_PUESTOS_PERIODO_Result> ObtenerPuestosPeriodo(int ID_EMPLEADO, string XML_PERIODOS)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
            return op.ObtenerPuestosPeriodo(ID_EMPLEADO, XML_PERIODOS);
        }

        

        public List<SPE_OBTIENE_FYD_CUMPLIMIENTO_PUESTO_PERIODO_Result> ObtenerCumplimientoPuestoPeriodo(int pID_PERIODO, int? pID_EVALUADO, int? pID_PUESTO, int? pID_EMPLEADO)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
            return op.ObtenerCumplimientoPuestoPeriodo(pID_PERIODO, pID_EVALUADO, pID_PUESTO, pID_EMPLEADO);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_INDIVIAL_EVALUADO_Result> ObtenerCumplimientoGeneralIndividual(int? pID_PERIODO, int? pID_EMPLEADO, int? pID_COMPETENCIA)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
            return op.ObtenerCumplimientoGeneralIndividual(pID_PERIODO, pID_EMPLEADO, pID_COMPETENCIA);
        }

        public List<SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL_Result> ObtenerReporteIndividualGeneral(int ID_PERIODO, int? ID_EVALUADO, int? ID_EMPLEADO)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();
            return op.ObtenerDatosReporteGeneralIndividual(ID_PERIODO, ID_EVALUADO, ID_EMPLEADO);
        }

        public DataTable ObtenerDatosReporteGeneralIndividual(int ID_PERIODO, int? ID_EVALUADO,ref List<E_REPORTE_GENERAL_INDIVIDUAL> vLstIndividual)
        {
            ConsultaIndividualOperaciones op = new ConsultaIndividualOperaciones();

            List<SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL_Result> vListaIndividual = new List<SPE_OBTIENE_FYD_REPORTE_GENERAL_INDIVIDUAL_Result>();
            //string vDivsCeldasChk = "<div class=\"divCheckbox\"> <input type=\"checkbox\" runat=\"server\" class=\"{4}\" id=\"{2}\" value=\"{2}\" {3}> </div>  <div class=\"divPorcentaje\">{0:N2}</div><div class=\"{1}\">&nbsp;</div>";
            string vNbPorcentaje = "";
            string vDivsCeldasPo = "<table class=\"tablaColor\"> " +
                "<tr> " +
                "<td class=\"porcentaje\"> " +
                "<div class=\"divPorcentaje\">{0}</div> " +
                "</td> " +
                "<td class=\"color\"> " +
                "<div class=\"{1}\">&nbsp;</div> " +
                "</td> </tr> </table>";
            string vClaseDivs = "";
            string vClaseColor = "";

            DataTable vDtPivot = new DataTable();

            vListaIndividual = op.ObtenerDatosReporteGeneralIndividual(ID_PERIODO,ID_EVALUADO, null);
            vLstIndividual = vListaIndividual.Select(s=> new E_REPORTE_GENERAL_INDIVIDUAL
             {
                    CL_COLOR = s.CL_COLOR,
                    CL_COLOR_COMPATIBILIDAD = s.CL_COLOR_COMPATIBILIDAD,
                    CL_EVALUADO = s.CL_EVALUADO,
                    CL_PUESTO = s.CL_PUESTO,
                    CL_TIPO_COMPETENCIA = s.CL_TIPO_COMPETENCIA,
                    DS_COMPETENCIA = s.DS_COMPETENCIA,
                    ID_COMPETENCIA = s.ID_COMPETENCIA,
                    ID_EMPLEADO = s.ID_EMPLEADO,
                    ID_EVALUADO = s.ID_EVALUADO,
                    ID_PERIODO = s.ID_PERIODO,
                    ID_PUESTO = s.ID_PUESTO,
                    NB_COMPETENCIA = s.NB_COMPETENCIA,
                    NB_EVALUADO = s.NB_EVALUADO,
                    NB_PUESTO = s.NB_PUESTO,
                    NO_TOTAL_TIPO_EVALUACION = s.NO_TOTAL_TIPO_EVALUACION,
                    NO_ORDEN = s.NO_ORDEN
                }).ToList();

            vDtPivot.Columns.Add("ID_COMPETENCIA", typeof(int));
            vDtPivot.Columns.Add("CL_COLOR",typeof(string));
            vDtPivot.Columns.Add("NB_COMPETENCIA", typeof(string));
            vDtPivot.Columns.Add("DS_COMPETENCIA", typeof(string));

            var vLstEmpleados = (from a in vListaIndividual select new { a.ID_PUESTO , a.NO_ORDEN}).Distinct().OrderBy(t => t.NO_ORDEN);
            var vLstCompetencias = (from a in vListaIndividual select new { a.ID_COMPETENCIA, 
                a.CL_COLOR, 
                a.NB_COMPETENCIA, a.DS_COMPETENCIA }).Distinct().OrderBy(t => t.ID_COMPETENCIA);

            foreach (var item in vLstEmpleados)
            {
                vDtPivot.Columns.Add(item.ID_PUESTO.ToString() + "E");
            }

            foreach (var vCom in vLstCompetencias)
            {
                DataRow vDr = vDtPivot.NewRow();

                vDr["ID_COMPETENCIA"] = vCom.ID_COMPETENCIA;
                vClaseColor = string.Format(vCom.CL_COLOR);
                vDr["CL_COLOR"] = "<div style=\"height: 80%; width: 20px;border-radius: 5px;  background:"+ vClaseColor +" ;\" ><br><br></div>";
                vDr["NB_COMPETENCIA"] = vCom.NB_COMPETENCIA;
                vDr["DS_COMPETENCIA"] = vCom.DS_COMPETENCIA;

                foreach (var vEmp in vLstEmpleados)
                {
                    var vResultado = vListaIndividual.Where(t => t.ID_COMPETENCIA == vCom.ID_COMPETENCIA & t.ID_PUESTO == vEmp.ID_PUESTO).FirstOrDefault();
                    if (vResultado != null)
                    {

                        switch (vResultado.CL_COLOR_COMPATIBILIDAD)
                        {
                            case "Green":
                                vClaseDivs = "divVerde";
                                break;

                            case "Gold":
                                vClaseDivs = "divAmarillo";
                                break;

                            case "Red":
                                vClaseDivs = "divRojo";
                                break;
                        }

                        vNbPorcentaje = string.Format("{0:N2}", vResultado.NO_TOTAL_TIPO_EVALUACION) + "%";
                        vDr[vEmp.ID_PUESTO.ToString() + "E"] = String.Format(vDivsCeldasPo, vNbPorcentaje, vClaseDivs);
                    }
                    else
                    {
                        vDr[vEmp.ID_PUESTO.ToString() + "E"] = String.Format(vDivsCeldasPo, "NA", "divNa");
                    }
                }
                vDtPivot.Rows.Add(vDr);
            }
            return vDtPivot;
        }
    }
}
