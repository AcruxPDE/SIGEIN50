//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIGE.Entidades
{
    using System;
    
    public partial class SPE_OBTIENE_RESULTADO_PRUEBA_Result
    {
        public int ID_PRUEBA { get; set; }
        public Nullable<int> ID_PREGUNTA { get; set; }
        public int ID_VARIABLE { get; set; }
        public string CL_PREGUNTA { get; set; }
        public int CL_TIPO_VARIABLE { get; set; }
        public string NB_PREGUNTA { get; set; }
        public string NB_RESPUESTA { get; set; }
        public Nullable<decimal> NO_VALOR_RESPUESTA { get; set; }
        public string NB_RESULTADO { get; set; }
    }
}