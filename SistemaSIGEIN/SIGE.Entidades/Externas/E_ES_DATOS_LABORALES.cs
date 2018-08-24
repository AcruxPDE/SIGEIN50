using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_ES_DATOS_LABORALES
    {
        public int ID_DATO_LABORAL { get; set; }
        public int ID_ESTUDIO_SOCIOECONOMICO { get; set; }
        public string NB_EMPRESA { get; set; }
        public string CL_PAIS { get; set; }
        public string NB_PAIS { get; set; }
        public string CL_ESTADO { get; set; }
        public string NB_ESTADO { get; set; }
        public string CL_MUNICIPIO { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string CL_COLONIA { get; set; }
        public string NB_COLONIA { get; set; }
        public string CL_CODIGO_POSTAL { get; set; }
        public string NB_DOMICILIO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<decimal> MN_SALARIO_INICIAL { get; set; }
        public Nullable<decimal> MN_SALARIO_FINAL { get; set; }
        public string CL_TIPO_EMPRESA { get; set; }
        public string DS_TIPO_EMPRESA { get; set; }
        public string CL_TIPO_CONTRATO { get; set; }
        public Nullable<decimal> NO_ANTIGUEDAD_EMPRESA { get; set; }
        public string CL_TIPO_SUELDO { get; set; }
        public string NB_TIPO_SUELDO { get; set; }

    }
}
