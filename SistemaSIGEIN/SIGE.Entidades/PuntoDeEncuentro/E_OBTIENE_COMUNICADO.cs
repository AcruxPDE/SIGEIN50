using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{

    [Serializable]
    public class E_OBTIENE_COMUNICADO
    {
        public int ID_COMUNICADO { get; set; }
        public string ID_EMPLEADO { get; set; }
        public string NB_COMUNICADO { get; set; }
        public System.DateTime FE_COMUNICADO { get; set; }
        public string DS_COMUNICADO { get; set; }
        public System.DateTime FE_VISIBLE_DEL { get; set; }
        public System.DateTime FE_VISIBLE_AL { get; set; }
        public Nullable<int> ID_ARCHIVO_PDE { get; set; }
        public System.DateTime FE_CREACION { get; set; }
        public string FG_LEIDO { get; set; }
        public bool FG_ESTATUS { get; set; }
        public Nullable<byte> FG_PRIVADO { get; set; }
        public string NB_ARCHIVO { get; set; }
        public string TIPO_COMUNICADO { get; set; }
        public string TIPO_ACCION { get; set; }
    }
}
