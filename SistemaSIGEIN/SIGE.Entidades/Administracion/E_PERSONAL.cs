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
        public int ID_EMPLEADO_NOMINA_DO { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public Nullable<System.Guid> ID_PLANTILLA { get; set; }
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
        public string XML_TELEFONOS { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public string NB_ACTIVO { get; set; }
        public Nullable<System.DateTime> FE_NACIMIENTO { get; set; }
        public string DS_LUGAR_NACIMIENTO { get; set; }
        public Nullable<System.DateTime> FE_ALTA { get; set; }
        public Nullable<System.DateTime> FE_BAJA { get; set; }
        public Nullable<decimal> MN_SUELDO { get; set; }
        public Nullable<decimal> MN_SUELDO_VARIABLE { get; set; }
        public string DS_SUELDO_COMPOSICION { get; set; }
        public Nullable<int> ID_CANDIDATO { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public string CL_USUARIO_APP_MODIFICA { get; set; }
        public Nullable<System.DateTime> FE_MODIFICA { get; set; }
        public Nullable<int> ID_PUESTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<int> ID_PLAZA { get; set; }
        public string CL_PLAZA { get; set; }
        public string NB_PLAZA { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }
        public string CL_EMPRESA { get; set; }
        public string NB_EMPRESA { get; set; }
        public string NB_RAZON_SOCIAL { get; set; }
        public Nullable<int> ID_DEPARTAMENTO { get; set; }
        public string CL_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public string DEPARTAMENTO_XML_CAMPOS_ADICIONALES { get; set; }
        public byte[] FI_FOTOGRAFIA { get; set; }
        public Nullable<bool> FG_DO { get; set; }
        public Nullable<bool> FG_NOMINA { get; set; }
        public Nullable<bool> FG_NOMINA_DO { get; set; }
        public string NB_NOMINA_DO { get; set; }
        public Nullable<bool> FG_SUELDO_NOMINA_DO { get; set; }
    }
}
