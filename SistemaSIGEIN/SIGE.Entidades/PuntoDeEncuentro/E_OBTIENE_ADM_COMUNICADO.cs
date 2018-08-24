using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{

    [Serializable]
   public class E_OBTIENE_ADM_COMUNICADO
    {
        public int ID_COMUNICADO { get; set; }
        public string NB_COMUNICADO { get; set; }
        public System.DateTime FE_COMUNICADO { get; set; }
        public string DS_COMUNICADO { get; set; }
        public System.DateTime FE_VISIBLE_DEL { get; set; }
        public System.DateTime FE_VISIBLE_AL { get; set; }
        public Nullable<int> ID_ARCHIVO_PDE { get; set; }
        public string NB_ARCHIVO { get; set; }
        public string FG_PRIVADO { get; set; }
        public bool FG_ESTATUS { get; set; }
        public int TOTAL { get; set; }
        public int LEIDOS { get; set; }
        public Nullable<int> COMENTARIOS { get; set; }
        public string TIPO_COMUNICADO { get; set; }
        public string TIPO_ACCION { set; get; }
    }
}
