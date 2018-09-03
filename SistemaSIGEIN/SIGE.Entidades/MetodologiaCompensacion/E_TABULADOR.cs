using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
    [Serializable]
     public class E_TABULADOR
    {
        public int? ID_TABULADOR { set; get; }
      public string  CL_TABULADOR { set; get;}
      public string  NB_TABULADOR {set; get;}
      public string  DS_TABULADOR {set; get;}
      public Nullable<System.DateTime> FE_VIGENCIA { set; get; }
      public string  CL_TIPO_PUESTO {set; get;}
      public string  CL_ESTADO {set; get;}
      public byte  NO_NIVELES {set;get;}
      public byte  NO_CATEGORIAS {set; get;}
      public decimal PR_PROGRESION {set; get;}
      public decimal PR_INFLACION {set; get;}
      public int  ID_CUARTIL_INFLACIONAL {set;get;}
      public int  ID_CUARTIL_INCREMENTO {set; get;}
      public int ID_CUARTIL_MERCADO { set; get; }
      public string  CL_SUELDO_COMPARACION {set; get;}
      public string  XML_VARIACION  {set; get;}
      public string CL_ORIGEN_NIVELES { set; get; }
      public Nullable<System.DateTime> FE_CREACION { set; get;}
      //public string XML_CUARTILES { set; get; }

    }
}
