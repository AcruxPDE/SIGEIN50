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
    
    public partial class SPE_OBTIENE_PERIODO_TABLERO_CONTROL_Result
    {
        public int ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public string DS_PERIODO { get; set; }
        public System.DateTime FE_INICIO { get; set; }
        public Nullable<System.DateTime> FE_TERMINO { get; set; }
        public string CL_ESTADO_PERIODO { get; set; }
        public string CL_TIPO_PERIODO { get; set; }
        public string DS_NOTAS { get; set; }
        public int ID_PERIODO_TABLERO { get; set; }
        public Nullable<bool> FG_EVALUACION_IDP { get; set; }
        public Nullable<bool> FG_EVALUACION_FYD { get; set; }
        public Nullable<bool> FG_EVALUACION_DESEMPEÑO { get; set; }
        public Nullable<bool> FG_EVALUACION_CLIMA_LABORAL { get; set; }
        public Nullable<bool> FG_SITUACION_SALARIAL { get; set; }
        public Nullable<decimal> PR_IDP { get; set; }
        public Nullable<decimal> PR_FYD { get; set; }
        public Nullable<decimal> PR_DESEMPENO { get; set; }
        public Nullable<decimal> PR_CLIMA_LABORAL { get; set; }
        public Nullable<bool> FG_GENERADO { get; set; }
        public string CL_USUARIO_APP_MODIFICA { get; set; }
        public Nullable<System.DateTime> FE_MODIFICA { get; set; }
    }
}
