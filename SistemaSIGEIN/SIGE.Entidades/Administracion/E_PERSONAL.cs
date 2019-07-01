using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public partial class E_PERSONAL
    {
        public int M_EMPLEADO_ID_EMPLEADO { get; set; }
        public Nullable<int> ID_EMPLEADO_DO { get; set; }
        public Nullable<System.Guid> ID_EMPLEADO_NOMINA { get; set; }
        public string M_EMPLEADO_ID_EMPLEADO_PDE { get; set; }
        public string M_EMPLEADO_CL_EMPLEADO { get; set; }
        public string M_EMPLEADO_NB_EMPLEADO_COMPLETO { get; set; }
        public string M_EMPLEADO_NB_EMPLEADO { get; set; }
        public string M_EMPLEADO_NB_APELLIDO_PATERNO { get; set; }
        public string M_EMPLEADO_NB_APELLIDO_MATERNO { get; set; }
        public string M_EMPLEADO_CL_ESTADO_EMPLEADO { get; set; }
        public string M_EMPLEADO_CL_GENERO { get; set; }
        public string M_EMPLEADO_CL_ESTADO_CIVIL { get; set; }
        public string M_EMPLEADO_NB_CONYUGUE { get; set; }
        public string M_EMPLEADO_CL_RFC { get; set; }
        public string M_EMPLEADO_CL_CURP { get; set; }
        public string M_EMPLEADO_CL_NSS { get; set; }
        public string M_EMPLEADO_CL_TIPO_SANGUINEO { get; set; }
        public string M_EMPLEADO_CL_NACIONALIDAD { get; set; }
        public string M_EMPLEADO_NB_PAIS { get; set; }
        public string M_EMPLEADO_NB_ESTADO { get; set; }
        public string M_EMPLEADO_NB_MUNICIPIO { get; set; }
        public string M_EMPLEADO_NB_COLONIA { get; set; }
        public string M_EMPLEADO_NB_CALLE { get; set; }
        public string M_EMPLEADO_NO_INTERIOR { get; set; }
        public string M_EMPLEADO_NO_EXTERIOR { get; set; }
        public string M_EMPLEADO_CL_CODIGO_POSTAL { get; set; }
        public string M_EMPLEADO_XML_TELEFONOS { get; set; }
        public string M_EMPLEADO_CL_CORREO_ELECTRONICO { get; set; }
        public string M_EMPLEADO_ACTIVO { get; set; }
        public Nullable<System.DateTime> M_EMPLEADO_FE_NACIMIENTO { get; set; }
        public string M_EMPLEADO_DS_LUGAR_NACIMIENTO { get; set; }
        public Nullable<System.DateTime> M_EMPLEADO_FE_ALTA { get; set; }
        public Nullable<System.DateTime> M_EMPLEADO_FE_BAJA { get; set; }
        public Nullable<decimal> M_EMPLEADO_MN_SUELDO { get; set; }
        public Nullable<decimal> M_EMPLEADO_MN_SUELDO_VARIABLE { get; set; }
        public string M_EMPLEADO_DS_SUELDO_COMPOSICION { get; set; }
        public Nullable<int> M_EMPLEADO_ID_CANDIDATO { get; set; }
        public string M_EMPLEADO_XML_CAMPOS_ADICIONALES { get; set; }
        public string M_EMPLEADO_CL_USUARIO_APP_MODIFICA { get; set; }
        public Nullable<System.DateTime> M_FE_MODIFICA { get; set; }
        public Nullable<int> M_PUESTO_ID_PUESTO { get; set; }
        public string M_PUESTO_ID_PUESTO_PDE { get; set; }
        public string M_PUESTO_CL_PUESTO { get; set; }
        public string M_PUESTO_NB_PUESTO { get; set; }
        public string C_EMPRESA_CL_EMPRESA { get; set; }
        public string C_EMPRESA_NB_EMPRESA { get; set; }
        public string C_EMPRESA_NB_RAZON_SOCIAL { get; set; }
        public Nullable<int> C_EMPRESA_ID_EMPRESA { get; set; }
        public string M_DEPARTAMENTO_NB_DEPARTAMENTO { get; set; }
        public byte[] FI_FOTOGRAFIA { get; set; }
        public string CL_EMPLEADO_NOMINA { get; set; }
        public Nullable<bool> FG_ACTIVO_DO { get; set; }
        public Nullable<bool> FG_ACTIVO_NOMINA { get; set; }
        public string NB_ACTIVO_NOMINA { get; set; }
        public Nullable<bool> FG_PUESTO_IGUAL_NOMINA_DO { get; set; }
        public bool FG_SUELDO_VISIBLE_BONO { get; set; }
        public bool FG_SUELDO_VISIBLE_INVENTARIO { get; set; }
        public bool FG_SUELDO_VISIBLE_TABULADOR { get; set; }
        public Nullable<int> ID_PUESTO_NOMINA { get; set; }
        public Nullable<System.Guid> ID_RAZON_SOCIAL { get; set; }
        public Nullable<decimal> SUELDO_DIARIO { get; set; }
        public Nullable<decimal> SUELDO_MENSUAL { get; set; }
        public Nullable<decimal> SUELDO_DO { get; set; }
        public Nullable<decimal> BASE_COTIZACION { get; set; }
    }
}
