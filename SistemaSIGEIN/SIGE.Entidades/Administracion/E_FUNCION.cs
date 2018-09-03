using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public partial class E_FUNCION
    {
        public int? ID_FUNCION { get; set; }
        public string CL_FUNCION { get; set; }
        public string CL_TIPO_FUNCION { get; set; }
        public string NB_FUNCION { get; set; }
        public string DS_FUNCION { get; set; }
        public string NB_URL { get; set; }
        public string XML_CONFIGURACION { get; set; }
        public int? ID_FUNCION_PADRE { get; set; }
        public bool FG_SELECCIONADO { get; set; }
        public string NB_IMAGEN { get; set; }
        public int NO_ORDEN { get; set; }
    }
}
