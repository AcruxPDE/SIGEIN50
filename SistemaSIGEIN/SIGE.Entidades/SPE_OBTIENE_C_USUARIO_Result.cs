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
    
    public partial class SPE_OBTIENE_C_USUARIO_Result
    {
        public string CL_USUARIO { get; set; }
        public string NB_USUARIO { get; set; }
        public string NB_CORREO_ELECTRONICO { get; set; }
        public string NB_PASSWORD { get; set; }
        public bool FG_CAMBIAR_PASSWORD { get; set; }
        public string XML_PERSONALIZACION { get; set; }
        public int ID_ROL { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public Nullable<System.DateTime> FE_INACTIVO { get; set; }
        public string DS_FILTRO { get; set; }
    }
}