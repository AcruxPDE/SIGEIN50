using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    public partial class E_EXPERIENCIA_LABORAL
    {
        public int? ID_EXPERIENCIA_LABORAL { get; set; }
        public Nullable<int> ID_CANDIDATO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string NB_EMPRESA { get; set; }
        public string DS_DOMICILIO { get; set; }
        public string NB_GIRO { get; set; }
        public string FE_INICIO_ANIO { get; set; }
        public string FE_INICIO_MES { get; set; }
        public string FE_FIN_ANIO { get; set; }
        public string FE_FIN_MES { get; set; }
        public string NB_PUESTO { get; set; }
        public string NB_FUNCION { get; set; }
        public string DS_FUNCIONES { get; set; }
        public Nullable<decimal> MN_PRIMER_SUELDO { get; set; }
        public Nullable<decimal> MN_ULTIMO_SUELDO { get; set; }
        public string CL_TIPO_CONTRATO { get; set; }
        public string CL_TIPO_CONTRATO_OTRO { get; set; }
        public string NO_TELEFONO_CONTACTO { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public string NB_CONTACTO { get; set; }
        public string NB_PUESTO_CONTACTO { get; set; }
        public bool CL_INFORMACION_CONFIRMADA { get; set; }
        public string DS_COMENTARIOS { get; set; }
        public string DS_FILTRO { get; set; }
    }
}
