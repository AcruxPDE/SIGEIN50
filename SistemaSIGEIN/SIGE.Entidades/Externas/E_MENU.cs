using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    public class E_MENU
    {
        public int? ID_MENU { get; set; }
        public string CL_MENU { get; set; }
        public string CL_TIPO_MENU { get; set; }
        public string NB_MENU { get; set; }
        public string NB_URL { get; set; }
        public string XML_CONFIGURACION { get; set; }
        public int? ID_MENU_PADRE { get; set; }
        public bool FG_SELECCIONADO { get; set; }
        public string NB_IMAGEN { get; set; }
        public string NB_TOOLTIP { get; set; }
    }
}
