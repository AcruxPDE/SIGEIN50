using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{
    public class E_OBTIENE_COMENTARIOS_COMUNICADO
    {
        public int ID_COMENTARIO_COMUNICADO { get; set; }
        public int ID_COMUNICADO { get; set; }
        public string ID_EMPLEADO { get; set; }
        public string NOMBRE { get; set; }
        public System.DateTime FE_COMENTARIO { get; set; }
        public string DS_COMENTARIO { get; set; }
        public Nullable<byte> FG_PRIVADO { get; set; }
    }
}
