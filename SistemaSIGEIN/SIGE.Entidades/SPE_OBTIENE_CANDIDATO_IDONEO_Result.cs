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
    
    public partial class SPE_OBTIENE_CANDIDATO_IDONEO_Result
    {
        public int ID_CANDIDATO { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string CL_SOLICITUD { get; set; }
        public string NB_CANDIDATO { get; set; }
        public Nullable<int> NO_EDAD { get; set; }
        public string DS_POSTGRADOS { get; set; }
        public string DS_PROFESIONAL { get; set; }
        public string DS_TECNICA { get; set; }
        public string DS_AREAS_INTERES { get; set; }
        public string DS_COMPETENCIAS_LABORALES { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO { get; set; }
        public Nullable<int> ID_BATERIA { get; set; }
        public Nullable<System.Guid> CL_TOKEN { get; set; }
        public byte[] FI_FOTOGRAFIA { get; set; }
    }
}