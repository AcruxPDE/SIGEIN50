using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_CANDIDATO_IDONEO
    {
        public Nullable<int> ID_CANDIDATO { get; set; }
        public Nullable<int> ID_SOLICITUD { get; set; }
        public string CL_SOLICITUD { get; set; }
        public string NB_CANDIDATO { get; set; }
        public Nullable<int> NO_EDAD { get; set; }
        public string DS_POSTGRADOS { get; set; }
        public string DS_PROFESIONAL { get; set; }
        public string DS_TECNICA { get; set; }
        public string DS_AREAS_INTERES { get; set; }
        public string DS_COMPETENCIAS_LABORALES { get; set; }
        public string PR_CUMPLIMIENTO { get; set; }
        public Nullable<int> ID_BATERIA { get; set; }
        public Nullable<System.Guid> CL_TOKEN { get; set; }
        public byte[] FI_FOTOGRAFIA { get; set; }
        public string vOrigen { get; set; }
        public string CL_ESTADO_PROCESO { get; set; }

        public Nullable<int> ID_EMPLEADO { get; set; }
	    public string CL_EMPLEADO { get; set; }
	    public string NB_PUESTO { get; set; }
	    public string CL_ORIGEN { get; set; }


        public Nullable<int> ID_REQUISICION { get; set; }
        public Nullable<int> ID_PROCESO_SELECCION { get; set; }

        public Nullable<decimal> PR_COMPATIBILIDAD_PERFIL { get; set; }
        public Nullable<decimal> PR_COMPATIBILIDAD_COMPETENCIAS { get; set; }
        public string CL_SOLICITUD_ESTATUS { get; set; }
        public string FG_OTRO_PROCESO_SELECCION { get; set; }


        public string CL_PAIS { get; set; }
        public string CL_ESTADO { get; set; }
        public string CL_MUNICIPIO { get; set; }
        public string CL_COLONIA { get; set; }
        public Nullable<System.DateTime> FE_NACIMIENTO { get; set; }
        public string DS_LUGAR_NACIMIENTO { get; set; }
        public string CL_NACIONALIDAD { get; set; }
        public string CL_DISPONIBILIDAD_VIAJE { get; set; }
        public string DS_DISPONIBILIDAD { get; set; }

    }
}
