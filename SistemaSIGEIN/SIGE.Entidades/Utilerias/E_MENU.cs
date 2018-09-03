using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades
{
    public class E_MENU
    {
        public List<E_MENU> listaSubMenu {get; set;}
        public List<E_ATRIBUTOS> listaAtributos { get; set; }
        public string nombreMenu { get; set; }
        public bool tieneSubmenu { get; set; }
        public string icono { get; set; }

        public E_MENU() { }
        
    }


    public class E_ATRIBUTOS
    {
        public string nombreAtributo {get; set;}
        public string valorAtributo { get; set; }
    }


}
