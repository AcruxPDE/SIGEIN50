using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{
    [Serializable]
    public class E_COMPROMISO
    {
        public Guid ID_COMPROMISO { set; get; }
        public string CL_COMPROMISO { get; set; }
        public string NB_COMPROMISO { set; get; }
        public int ID_TIPO_COMPROMISO { set; get; }
        public Guid ID_PRIORIDAD { set; get; }
        public Guid ID_ESTATUS_COMPROMISO { set; get; }
        public Guid ID_CALIFICACION { set; get; }
        public System.DateTime FE_ENTREGA { set; get; }
        public System.DateTime FE_NEGOCIABLE { set; get; }
        public bool FG_ACTIVO { set; get; }
        public string CL_USUARIO_ASIGNADO { set; get; }









    }
}
