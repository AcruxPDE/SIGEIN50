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
    
    public partial class SPE_OBTIENE_C_PROGRAMA_Result
    {
        public int ID_PROGRAMA { get; set; }
        public Nullable<int> ID_PERIODO { get; set; }
        public string CL_PROGRAMA { get; set; }
        public string NB_PROGRAMA { get; set; }
        public string CL_TIPO_PROGRAMA { get; set; }
        public string CL_ESTADO { get; set; }
        public string CL_AUTORIZACION { get; set; }
        public string DS_NOTAS { get; set; }
        public Nullable<int> ID_DOCUMENTO_AUTORIZACION { get; set; }
        public string CL_DOCUMENTO { get; set; }
        public string VERSION { get; set; }
        public Nullable<int> NO_COMPETENCIAS { get; set; }
        public Nullable<int> NO_PARTICIPANTES { get; set; }
        public System.DateTime FE_CREACION { get; set; }
        public string CL_EVALUACION { get; set; }
        public string CL_USUARIO_APP_MODIFICA { get; set; }
        public Nullable<System.DateTime> FE_MODIFICA { get; set; }
        public string TIPO_EVALUACION { get; set; }
    }
}