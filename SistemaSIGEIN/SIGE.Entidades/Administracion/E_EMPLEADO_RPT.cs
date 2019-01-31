using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_EMPLEADO_RPT
    {
        public Nullable<long> RENGLON { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string APELLIDOS { get; set; }
        public string CL_ESTADO_EMPLEADO { get; set; }
        public Nullable<System.DateTime> FE_NACIMIENTO { get; set; }
        public string EDAD { get; set; }
        public string CL_RFC { get; set; }
        public string CL_CURP { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public string NB_CENTRO_ADMVO { get; set; }
        public string NB_CENTRO_OPTVO { get; set; }
        public System.DateTime FE_ALTA { get; set; }
        public string CL_SOLICITUD { get; set; }
        public string CL_NSS { get; set; }
        public string DIRECCION { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string NB_ESTADO { get; set; }
        public string CL_CODIGO_POSTAL { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public string TELEFONO_MOVIL { get; set; }
        public string TELEFONO_CASA { get; set; }
        public string DS_COMENTARIO { get; set; }
        public string SEXO { get; set; }
        public string TIENE_DEPENDIENTES { get; set; }
        public string NUMERO_DEPENDIENTES { get; set; }
        public string NIVEL_ESCOLARIDAD { get; set; }
        public string NB_ESCOLARIDAD { get; set; }
        public string CL_EMPRESA { get; set; }
        public string NB_EMPRESA { get; set; }
    }
}
