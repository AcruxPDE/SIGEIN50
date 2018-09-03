using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_PUESTO
    {
        public Nullable<int> ID_PUESTO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string NB_ACTIVO { get; set; }
        public Nullable<System.DateTime> FE_INACTIVO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<int> ID_PUESTO_JEFE { get; set; }
        public int ID_DEPARTAMENTO { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public Nullable<int> ID_BITACORA { get; set; }
        public Nullable<byte> NO_EDAD_MINIMA { get; set; }
        public Nullable<byte> NO_EDAD_MAXIMA { get; set; }
        public string CL_GENERO { get; set; }
        public string CL_ESTADO_CIVIL { get; set; }
        public string XML_REQUERIMIENTOS { get; set; }
        public string XML_OBSERVACIONES { get; set; }
        public string XML_RESPONSABILIDAD { get; set; }
        public string XML_AUTORIDAD { get; set; }
        public string XML_CURSOS_ADICIONALES { get; set; }
        public string XML_MENTOR { get; set; }
        public string CL_TIPO_PUESTO { get; set; }
        public Nullable<int> ID_CENTRO_ADMINISTRATIVO { get; set; }
        public Nullable<int> ID_CENTRO_OPERATIVO { get; set; }
        public Nullable<int> ID_PAQUETE_PRESTACIONES { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public string CL_DEPARTAMENTO { get; set; }
        public string DS_FILTRO { get; set; }
        public string XML_PUESTO { get; set; }
    }
}
