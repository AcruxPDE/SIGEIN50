using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_EMPLEADO
    {
        public string CL_CORREO_ELECTRONICO { get; set; }
        public string CL_CURP { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string CL_ESTADO_EMPLEADO { get; set; }
        public string CL_GENERO { get; set; }
        public string CL_NSS { get; set; }
        public string CL_RFC { get; set; }
        public string DS_SUELDO_COMPOSICION { get; set; }
        public Nullable<System.DateTime> FE_ALTA { get; set; }
        public Nullable<System.DateTime> FE_BAJA { get; set; }
        public bool FG_ACTIVO { get; set; }
        public int? ID_BITACORA { get; set; }
        public int? ID_DEPARTAMENTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public int? ID_EMPLEADO { get; set; }
        public int? ID_EMPLEADO_JEFE { get; set; }
        public int? ID_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public decimal? MN_SUELDO { get; set; }
        public decimal? MN_SUELDO_VARIABLE { get; set; }
        public string NB_APELLIDO_MATERNO { get; set; }
        public string NB_APELLIDO_PATERNO { get; set; }
        public string NB_CALLE { get; set; }
        public string NB_COLONIA { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string NB_ESTADO { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string NB_PAIS { get; set; }
        public string NO_EXTERIOR { get; set; }
        public string NO_INTERIOR { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public string XML_TELEFONOS { get; set; } 
    }
}
