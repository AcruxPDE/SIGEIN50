using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    public class E_DESCRIPTIVO
    {
        //public int? ID_DESCRIPTIVO { get; set; }
        public int? ID_PUESTO { get; set; }
        public string ID_PUESTO_PDE { get; set; }
        public bool? FG_ACTIVO { get; set; }
        public DateTime? FE_INACTIVO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public byte? NO_EDAD_MINIMA { get; set; }
        public byte? NO_EDAD_MAXIMA { get; set; }
        public string XML_REQUERIMIENTOS { get; set; }
        public string XML_OBSERVACIONES { get; set; }
        public string XML_RESPONSABILIDAD { get; set; }
        public string XML_AUTORIDAD { get; set; }
        public string XML_CURSOS_ADICIONALES { get; set; }
        public string XML_MENTOR { get; set; }
        public string CL_TIPO_PUESTO { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public int? ID_BITACORA { get; set; }
        public string CL_POSICION_ORGANIGRAMA { get; set; }
        public string XML_PUESTO_ESCOLARIDAD { get; set; }
        public string XML_PUESTO_EXPERIENCIA { get; set; }
        public string XML_PUESTOS_RELACIONADOS { get; set; }
        public string XML_PUESTO_FUNCION { get; set; }
        public string XML_PUESTO_COMPETENCIA { get; set; }
        public string XML_PUESTO_INDICADOR { get; set; }
        public string XML_CODIGO_CAMPOS_ADICIONALES { get; set; }
        public string CL_DOCUMENTO { get; set; }
		public string CL_VERSION { get; set; }
		public DateTime? FE_ELABORACION { get; set; }
		public string NB_ELABORO { get; set; }
		public DateTime? FE_REVISION { get; set; }
		public string NB_REVISO { get; set; }
		public DateTime? FE_AUTORIZACION { get; set; }
		public string NB_AUTORIZO { get; set; }
        public string DS_CONTROL_CAMBIOS { get; set; }
        public string CL_TIPO_PRESTACIONES { get; set; }
        public string XML_PRESTACIONES { get; set; }
        public int? NO_PLAZAS { get; set; }
        public int? NO_MINIMO_PLAZAS { get; set; }
        public string DS_COMPETENCIAS_REQUERIDAS { get; set; }
        public string ESTATUS { get; set; }
        //public string XML_ESCOLARIDADES { get; set; }
        //public string XML_COMPETENCIAS { get; set; }

        public List<E_ESCOLARIDADES> LST_ESCOLARIDADES { get; set; }
        public List<E_CATALOGO_CATALOGOS> LST_CATALOGO_GENERO { get; set; }
        public List<E_CATALOGO_CATALOGOS> LST_CATALOGO_EDOCIVIL { get; set; }
        public List<E_COMPETENCIAS> LST_CATALOGO_COMPETENCIAS_ESP { get; set; }
        public List<E_COMPETENCIAS> LST_CATALOGO_COMPETENCIAS { get; set; }
        public List<E_AREAS_INTERES> LST_AREAS_INTERES { get; set; }
        public List<E_AREAS> LST_AREAS { get; set; }
        public List<E_CENTRO_ADMVO> LST_CENTRO_ADMVO { get; set; }
        public List<E_CENTRO_OPTVO> LST_CENTRO_OPTVO { get; set; }
        public List<E_PUESTOS> LST_PUESTOS { get; set; }
        public List<E_INDICADOR_DESEMPENO> LST_INDICADORES { get; set; }
        
        //Ocupaciones
        public E_OCUPACION_PUESTO LST_OCUPACION_PUESTO { get; set; }

    }
}
