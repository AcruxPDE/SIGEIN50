using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_PERIODO_EVALUACION
    {
        public int? ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public string CL_TIPO_PERIODO { get; set; }
        public DateTime FE_PERIODO { get; set; }
        public string CL_ESTADO { get; set; }
        public string NB_PERIODO { get; set; }
        public string DS_PERIODO { get; set; }
        public bool FG_AUTOEVALUACION { get; set; }
        public bool FG_SUPERIOR { get; set; }
        public bool FG_SUBORDINADOS { get; set; }
        public bool FG_INTERRELACIONADOS { get; set; }
        public bool FG_OTROS_EVALUADORES { get; set; }
        public decimal PR_AUTOEVALUACION { get; set; }
        public decimal PR_SUPERIOR { get; set; }
        public decimal PR_SUBORDINADOS { get; set; }
        public decimal PR_INTERRELACIONADOS { get; set; }
        public decimal PR_OTROS_EVALUADORES { get; set; }
        public string CL_TIPO_EVALUACION { get; set; }
        public string XML_MENSAJE_INICIAL { get; set; }
        public string XML_INSTRUCCIONES_DE_LLENADO { get; set; }
        public int ID_DOCUMENTO_AUTORIZACION { get; set; }

        public bool FG_PUESTO_ACTUAL { get; set; }
        public bool FG_OTROS_PUESTOS { get; set; }
        public bool FG_RUTA_VERTICAL { get; set; }
        public bool FG_RUTA_VERTICAL_ALTERNATIVA { get; set; }
        public bool FG_RUTA_HORIZONTAL { get; set; }

        public bool FG_CREADO_POR_PVC { get; set; }

        public bool FG_COMPETENCIAS_GENERICAS { get; set; }
        public bool FG_COMPETENCIAS_ESPECIFICAS { get; set; }
        public bool FG_COMPETENCIAS_INSTITUCIONALES { get; set; }
        public decimal PR_COMPETENCIAS_GENERICAS { get; set; }
        public decimal PR_COMPETENCIAS_ESPECIFICAS { get; set; }
        public decimal PR_COMPETENCIAS_INSTITUCIONALES { get; set; }

        public List<E_COMPETENCIAS> LS_OTRAS_COMPETENCIAS { get; set; }
        public List<E_CAMPO> LS_CAMPOS_COMUNES { get; set; }
        public List<E_CAMPO> LS_PREGUNTAS_ADICIONALES { get; set; }

        public bool FG_PONDERACION_EVALUADORES { get; set; }
        public bool FG_PONDERACION_COMPETENCIAS { get; set; }

    }
}
