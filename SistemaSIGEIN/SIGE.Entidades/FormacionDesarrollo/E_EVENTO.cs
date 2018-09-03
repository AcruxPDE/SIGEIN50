using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_EVENTO
    {
        public int ID_EVENTO { get; set; }
        public string CL_EVENTO { get; set; }
        public string NB_EVENTO { get; set; }
        public string DS_EVENTO { get; set; }
        public Nullable<int> ID_PROGRAMA { get; set; }
        public string NB_PROPGRAMA { get; set; }
        public System.DateTime FE_INICIO { get; set; }
        public System.DateTime FE_TERMINO { get; set; }

        public DateTime FE_TERMINO_COMPLETO
        {
            get
            {
                return FE_TERMINO.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
        }

        public Nullable<int> ID_CURSO { get; set; }
        public string NB_CURSO { get; set; }
        public Nullable<int> ID_INSTRUCTOR { get; set; }
        public string NB_INSTRUCTOR { get; set; }
        public string CL_TIPO_CURSO { get; set; }
        public string CL_ESTADO { get; set; }
        public string NB_ESTADO { get; set; }
        public Nullable<int> ID_EMPLEADO_EVALUADOR { get; set; }
        public string CL_EVALUADOR { get; set; }
        public string NB_EVALUADOR { get; set; }
        public string CL_CORREO_EVALUADOR { get; set; }
        public Nullable<System.DateTime> FE_EVALUACION { get; set; }
        public string DS_LUGAR { get; set; }
        public string DS_REFRIGERIO { get; set; }
        public decimal MN_COSTO_DIRECTO { get; set; }
        public decimal MN_COSTO_INDIRECTO { get; set; }
        public bool FG_INCLUIR_EN_PLANTILLA { get; set; }
        public string CL_CURSO { get; set; }
        public decimal NO_DURACION_CURSO { get; set; }
        public string XML_PARTICIPANTES { get; set; }
        public string XML_CALENDARIO { get; set; }
        public Nullable<System.Guid> FL_EVENTO { get; set; }
        public string CL_TOKEN { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public string CL_USUARIO_APP_MODIFICA { get; set; }
        public string FE_MODIFICA { get; set; }
    }
}
