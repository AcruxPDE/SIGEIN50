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
    
    public partial class SPE_OBTIENE_EO_CUMPLIMIENTO_GLOBAL_DESEMPENO_Result
    {
        public int ID_PERIODO { get; set; }
        public string CL_PUESTO_PERIODO { get; set; }
        public string NB_PUESTO_PERIODO { get; set; }
        public string CL_PUESTO_ACTUAL { get; set; }
        public string NB_PUESTO_ACTUAL { get; set; }
        public string NB_EVALUADO { get; set; }
        public Nullable<decimal> PR_EVALUADO { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO_EVALUADO { get; set; }
        public Nullable<decimal> C_GENERAL { get; set; }
        public int ID_EVALUADO { get; set; }
        public int ID_BONO_EVALUADO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public Nullable<decimal> CUMPLIDO { get; set; }
    }
}