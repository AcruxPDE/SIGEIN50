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
    
    public partial class SPE_OBTIENE_K_SOLICITUD_Result
    {
        public int ID_SOLICITUD { get; set; }
        public Nullable<int> ID_CANDIDATO { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public Nullable<int> ID_DESCRIPTIVO { get; set; }
        public Nullable<int> ID_REQUISICION { get; set; }
        public string CL_SOLICITUD { get; set; }
        public string CL_ACCESO_EVALUACION { get; set; }
        public Nullable<int> ID_PLANTILLA_SOLICITUD { get; set; }
        public string DS_COMPETENCIAS_ADICIONALES { get; set; }
        public string NB_EMPLEADO_COMPLETO { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public string CL_EMPLEADO { get; set; }
        public Nullable<decimal> MN_SUELDO { get; set; }
        public string NO_REQUISICION { get; set; }
        public string CL_CAUSA { get; set; }
        public string CL_ESTADO { get; set; }
        public Nullable<System.DateTime> FE_SOLICITUD { get; set; }
        public Nullable<System.DateTime> FE_MODIFICACION { get; set; }
        public string CL_USUARIO_MODIFICA { get; set; }
        public string NB_EMPRESA { get; set; }
        public string CL_EMPRESA { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }
        public string NB_CANDIDATO_COMPLETO { get; set; }
        public string XML_EGRESOS { get; set; }
        public string XML_INGRESOS { get; set; }
        public string XML_PATRIMONIO { get; set; }
        public string XML_PERFIL_RED_SOCIAL { get; set; }
        public string XML_TELEFONOS { get; set; }
        public Nullable<int> ID_BATERIA { get; set; }
        public Nullable<System.Guid> CL_TOKEN { get; set; }
    }
}