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
    
    public partial class SPE_OBTIENE_C_AUTORIZACION_DCTO_EMPLEADO_Result
    {
        public int ID_AUTORIZACION { get; set; }
        public System.Guid FL_AUTORIZACION { get; set; }
        public string CL_TOKEN { get; set; }
        public int ID_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public int ID_DOCUMENTO { get; set; }
        public string CL_ESTADO { get; set; }
        public string DS_OBSERVACIONES { get; set; }
        public Nullable<System.DateTime> FE_AUTORIZACION { get; set; }
        public string CL_EMPLEADO { get; set; }
        public string NB_EMPLEADO_PUESTO { get; set; }
        public int ID_PUESTO { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_DOCUMENTO { get; set; }
        public string NB_DOCUMENTO { get; set; }
        public string CL_TIPO_DOCUMENTO { get; set; }
        public string DS_NOTAS { get; set; }
        public string VERSION { get; set; }
        public Nullable<System.DateTime> FE_ELABORACION { get; set; }
        public Nullable<System.DateTime> FE_REVISION { get; set; }
        public string NB_EMPLEADO_ELABORA { get; set; }
        public Nullable<int> ID_PROGRAMA { get; set; }
        public string CL_PROGRAMA { get; set; }
        public string NB_PROGRAMA { get; set; }
        public Nullable<int> ID_PERIODO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
    }
}