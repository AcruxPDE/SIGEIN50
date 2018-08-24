using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
  public  class E_SOC_DEPENDIENTE
    {
      public int  ID_DATO_DEPENDIENTE  { get; set; }
      public int ID_ESTUDIO_SOCIOECONOMICO  { get; set; }
      public string  NB_PARIENTE  { get; set; }
      public string CL_PARENTESCO { get; set; }
      public string CL_GENERO { get; set; }
      public DateTime  FE_NACIMIENTO  { get; set; }
      public int  ID_BITACORA  { get; set; }
      public string CL_OCUPACION { get; set; }
      public bool   FG_DEPENDIENTE  { get; set; }
      public string   FG_ACTIVO { get; set; }
    }
}
