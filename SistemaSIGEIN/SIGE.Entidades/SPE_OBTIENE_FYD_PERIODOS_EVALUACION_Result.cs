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
    
    public partial class SPE_OBTIENE_FYD_PERIODOS_EVALUACION_Result
    {
        public int ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public string DS_PERIODO { get; set; }
        public System.DateTime FE_INICIO { get; set; }
        public Nullable<System.DateTime> FE_TERMINO { get; set; }
        public string CL_ESTADO_PERIODO { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }
        public string DS_NOTAS { get; set; }
        public bool FG_AUTOEVALUACION { get; set; }
        public bool FG_SUPERVISOR { get; set; }
        public bool FG_SUBORDINADOS { get; set; }
        public bool FG_INTERRELACIONADOS { get; set; }
        public bool FG_OTROS_EVALUADORES { get; set; }
        public string CL_USUARIO_APP_MODIFICA { get; set; }
        public Nullable<System.DateTime> FE_MODIFICA { get; set; }
        public Nullable<bool> FG_TIENE_EVALUADORES { get; set; }
    }
}