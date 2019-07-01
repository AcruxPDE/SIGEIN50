using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_INSTRUCTOR
    {
        public Guid ID_ITEM { get; set; }

        public int? ID_INSTRUCTOR { get; set; }
        public string CL_TIPO_INSTRUCTOR { get; set; }
        public string CL_INTRUCTOR { get; set; }
        public string NB_INSTRUCTOR { get; set; }
        public string NB_APELLIDO_PATERNO { get; set; }
        public string NB_APELLIDO_MATERNO { get; set; }
        public string NB_VALIDADOR { get; set; }
        public string CL_RFC { get; set; }
        public string CL_CURP { get; set; }
        public string CL_STPS { get; set; }
        public string CL_PAIS { get; set; }
        public string NB_PAIS { get; set; }
        public string CL_ESTADO { get; set; }
        public string NB_ESTADO { get; set; }
        public string CL_MUNICIPIO { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string CL_COLONIA { get; set; }
        public string NB_COLONIA { get; set; }
        public string NB_CALLE { get; set; }
        public string NO_INTERIOR { get; set; }
        public string NO_EXTERIOR { get; set; }
        public string CL_CODIGO_POSTAL { get; set; }
        public string DS_ESCOLARIDAD { get; set; }
        public Nullable<System.DateTime> FE_NACIMIENTO { get; set; }
        public string XML_TELEFONOS { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public Nullable<decimal> MN_COSTO_HORA { get; set; }
        public Nullable<decimal> MN_COSTO_PARTICIPANTE { get; set; }
        public string DS_EVIDENCIA_COMPETENCIA { get; set; }
        public string XML_CURSOS { get; set; }

        public string XML_COMPETENCIAS { get; set; }

        public List<E_INSTRUCTOR_COMPETENCIA> LstCompetencias { get; set; }

        public E_INSTRUCTOR()
        {
            LstCompetencias = new List<E_INSTRUCTOR_COMPETENCIA>();
        }
    }
}
