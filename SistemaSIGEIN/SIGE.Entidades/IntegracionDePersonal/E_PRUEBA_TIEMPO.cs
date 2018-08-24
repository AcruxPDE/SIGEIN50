using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.IntegracionDePersonal
{
    [Serializable]
     public class E_PRUEBA_TIEMPO
     {
         public int ID_PRUEBA_SECCION { get; set; }
         public int ID_PRUEBA { get; set; }
         public string CL_PRUEBA_SECCION { get; set; }
         public string NB_PRUEBA_SECCION { get; set; }
         public Nullable<int> NO_TIEMPO { get; set; }
         public string CL_ESTADO { get; set; }
         public Nullable<System.DateTime> FE_INICIO { get; set; }
         public Nullable<System.DateTime> FE_TERMINO { get; set; }
    }
}

