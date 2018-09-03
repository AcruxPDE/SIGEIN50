using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
    public class E_PARAMETROS_BUSQUEDA_CANDIDATO
    {
        public Guid vIdParametroBusqueda { get; set; }

        public bool vFgBusquedaPerfil { get; set; }
        public bool vFgBusquedaCompetencias { get; set; }

        public bool vFgEdad { get; set; }
        public bool vFgGenero { get; set; }
        public bool vFgEdoCivil { get; set; }
        public bool vFgEscolaridad { get; set; }
        public bool vFgComEsp { get; set; }
        public bool vFgAreaInteres { get; set; }
    }
}
