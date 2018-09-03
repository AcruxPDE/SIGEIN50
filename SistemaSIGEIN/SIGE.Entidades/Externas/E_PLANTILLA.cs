using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_PLANTILLA
    {
        public int? ID_PLANTILLA { get; set; }
        public string NB_PLANTILLA { get; set; }
        public string DS_PLANTILLA { get; set; }
        public string CL_FORMULARIO { get; set; }
        public string CL_EXPOSICION { get; set; }
        public bool FG_GENERAL { get; set; }
        public string FG_GENERAL_CL { get; set; }
        public Guid? FL_PLANTILLA { get; set; }
        public string XML_PLANTILLA_FORMULARIO { get; set; }
        public string XML_PLANTILLA { get; set; }
        public List<E_CAMPO> LST_CAMPOS { get; set; }
        public string XML_CAMPOS { set; get; }
    }

    [Serializable]
    public class E_CAMPO
    {
        public int? ID_CAMPO { get; set; }
        public string CL_CAMPO { get; set; }
        public string NB_CAMPO { get; set; }
        public string DS_CAMPO { get; set; }
        public string CL_TIPO_CAMPO { get; set; }
        public string CL_FORMULARIO { get; set; }
        public bool FG_ACTIVA { get; set; }
        public bool FG_SISTEMA { get; set; }
        public bool FG_HABILITADO { get; set; }
        public bool FG_REQUERIDO { get; set; }
        public string CL_CONTENEDOR { get; set; }
        public byte NO_ORDEN { get; set; }
        public string XML_CAMPO { get; set; }
    }
}
