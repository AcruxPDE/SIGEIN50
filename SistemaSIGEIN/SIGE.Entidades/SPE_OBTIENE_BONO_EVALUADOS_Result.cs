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
    
    public partial class SPE_OBTIENE_BONO_EVALUADOS_Result
    {
        public int ID_BONO_EVALUADO { get; set; }
        public int ID_PERIODO { get; set; }
        public int ID_EVALUADO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string CL_EVALUADO { get; set; }
        public string NB_EVALUADO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public string NB_PUESTO { get; set; }
        public decimal MN_SUELDO { get; set; }
        public decimal MN_TOPE_BONO { get; set; }
        public decimal NO_MONTO_BONO { get; set; }
    }
}