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
    
    public partial class SPE_OBTIENE_TABULADOR_DESCRIPTIVO_PUESTO_Result
    {
        public Nullable<int> ID_TABULADOR_PUESTO { get; set; }
        public int ID_TABULADOR { get; set; }
        public int ID_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public Nullable<decimal> MN_MINIMO { get; set; }
        public Nullable<decimal> MN_MAXIMO { get; set; }
        public string CL_ORIGEN { get; set; }
        public Nullable<int> NO_NIVEL { get; set; }
    }
}
