using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_ESTUDIO_SOCIOECONOMICO
    {
        public int ID_ESTUDIO_SOCIOECONOMICO { get; set; }
        public int ID_PROCESO_SELECCION { get; set; }
        public System.DateTime FE_REALIZACION { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public Nullable<int> ID_CANDIDATO { get; set; }
        public Nullable<System.DateTime> FE_NACIMIENTO { get; set; }
        public Nullable<byte> NO_EDAD { get; set; }
        public string CL_ESTADO_CIVIL { get; set; }
        public string CL_RFC { get; set; }
        public string CL_CURP { get; set; }
        public string CL_NSS { get; set; }
        public string CL_PAIS { get; set; }
        public string NB_PAIS { get; set; }
        public string CL_ESTADO { get; set; }
        public string NB_ESTADO { get; set; }
        public string CL_MUNICIPIO { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string CL_COLONIA { get; set; }
        public string NB_COLONIA { get; set; }
        public string NB_CALLE { get; set; }
        public string NO_EXTERIOR { get; set; }
        public string NO_INTERIOR { get; set; }
        public string CL_CODIGO_POSTAL { get; set; }
        public string NO_TIEMPO_RESIDENCIA { get; set; }
        public string CL_TIPO_SANGUINEO { get; set; }
        public string XML_TELEFONOS { get; set; }
        public string CL_IDENTIFICACION_OFICIAL { get; set; }
        public string DS_IDENTIFICACION_OFICIAL { get; set; }
        public string XML_INGRESOS { get; set; }
        public string XML_EGRESOS { get; set; }
        public string CL_SERVICIOS_MEDICOS { get; set; }
        public string DS_SERVICIOS_MEDICOS { get; set; }
    }
}
