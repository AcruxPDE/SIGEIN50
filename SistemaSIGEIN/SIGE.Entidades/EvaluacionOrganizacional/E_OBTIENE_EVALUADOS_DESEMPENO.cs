using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.EvaluacionOrganizacional
{
    [Serializable]
    public class E_OBTIENE_EVALUADOS_DESEMPENO
    {
        public int ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO_COMPLETO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string NB_APELLIDO_PATERNO { get; set; }
        public string NB_APELLIDO_MATERNO { get; set; }
        public string CL_ESTADO_EMPLEADO { get; set; }
        public string CL_GENERO { get; set; }
        public string CL_ESTADO_CIVIL { get; set; }
        public string NB_CONYUGUE { get; set; }
        public string CL_RFC { get; set; }
        public string CL_CURP { get; set; }
        public string CL_NSS { get; set; }
        public string CL_TIPO_SANGUINEO { get; set; }
        public string CL_NACIONALIDAD { get; set; }
        public string NB_PAIS { get; set; }
        public string NB_ESTADO { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string NB_COLONIA { get; set; }
        public string NB_CALLE { get; set; }
        public string NO_INTERIOR { get; set; }
        public string NO_EXTERIOR { get; set; }
        public string CL_CODIGO_POSTAL { get; set; }
        public string M_EMPLEADO_CL_CORREO_ELECTRONICO { get; set; }
        public System.DateTime FE_INICIO { get; set; }
        public System.DateTime FE_TERMINO { get; set; }
        public string M_EMPLEADO_ACTIVO { get; set; }
        public Nullable<System.DateTime> FE_NACIMIENTO { get; set; }
        public string DS_LUGAR_NACIMIENTO { get; set; }
        public System.DateTime FE_ALTA { get; set; }
        public Nullable<System.DateTime> FE_BAJA { get; set; }
        public Nullable<decimal> MN_SUELDO { get; set; }
        public Nullable<decimal> MN_SUELDO_VARIABLE { get; set; }
        public string DS_SUELDO_COMPOSICION { get; set; }
        public string XML_RESPONSABILIDAD { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_EMPRESA { get; set; }
        public string NB_EMPRESA { get; set; }
        public string NB_RAZON_SOCIAL { get; set; }
        public string CL_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public int ID_EVALUADO { get; set; }
        public int ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public Nullable<int> NO_TOTAL_METAS { get; set; }
        public Nullable<int> NO_TOTAL_METAS_ACTIVAS { get; set; }
        public Nullable<decimal> PR_EVALUADO { get; set; }
        public Nullable<int> NO_EVALUADOR { get; set; }
        public Nullable<System.DateTime> FE_CAPTURA_METAS { get; set; }
        public Nullable<decimal> MN_TOPE_BONO { get; set; }
        public Nullable<decimal> NO_MONTO_BONO { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO_EVALUADO { get; set; }
        public Nullable<decimal> MN_BONO_TOTAL { get; set; }
        public bool FG_VISIBLE_BONO { get; set; }
        public string ESTATUS { get; set; }
        public decimal PR_BONO { get; set; }
        public decimal MN_BONO { get; set; }
    }
}
