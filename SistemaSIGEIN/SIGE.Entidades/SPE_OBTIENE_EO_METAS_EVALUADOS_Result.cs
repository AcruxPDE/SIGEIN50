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
    
    public partial class SPE_OBTIENE_EO_METAS_EVALUADOS_Result
    {
        public int ID_EVALUADO_META { get; set; }
        public int ID_PERIODO { get; set; }
        public int ID_EVALUADO { get; set; }
        public Nullable<int> NO_META { get; set; }
        public string DS_FUNCION { get; set; }
        public string NB_INDICADOR { get; set; }
        public string DS_META { get; set; }
        public string CL_TIPO_META { get; set; }
        public bool FG_VALIDA_CUMPLIMIENTO { get; set; }
        public bool FG_EVALUAR { get; set; }
        public string NB_CUMPLIMIENTO_ACTUAL { get; set; }
        public string NB_CUMPLIMIENTO_MINIMO { get; set; }
        public string NB_CUMPLIMIENTO_SATISFACTORIO { get; set; }
        public string NB_CUMPLIMIENTO_SOBRESALIENTE { get; set; }
        public Nullable<decimal> PR_META { get; set; }
        public string NB_RESULTADO { get; set; }
        public Nullable<decimal> PR_RESULTADO { get; set; }
        public Nullable<int> CL_NIVEL { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO_META { get; set; }
        public Nullable<bool> FG_EVIDENCIA { get; set; }
        public Nullable<decimal> PR_EVALUADO { get; set; }
        public string NIVEL_ALZANZADO { get; set; }
        public string COLOR_NIVEL { get; set; }
    }
}