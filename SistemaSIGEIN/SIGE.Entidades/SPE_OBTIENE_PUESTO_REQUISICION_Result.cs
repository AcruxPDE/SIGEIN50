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
    
    public partial class SPE_OBTIENE_PUESTO_REQUISICION_Result
    {
        public int ID_REQUISICION { get; set; }
        public int ID_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public Nullable<byte> NO_EDAD_MINIMA { get; set; }
        public Nullable<byte> NO_EDAD_MAXIMA { get; set; }
        public string CL_GENERO { get; set; }
        public string CL_ESTADO_CIVIL { get; set; }
        public string DS_POSTGRADO { get; set; }
        public string DS_PROFESIONAL { get; set; }
        public string DS_TECNICA { get; set; }
        public Nullable<int> ID_EMPLEADO_SUPLENTE { get; set; }
        public string NB_EMPLEADO_SUPLENTE { get; set; }
        public Nullable<decimal> MN_SUELDO { get; set; }
        public Nullable<decimal> MN_SUELDO_SUGERIDO { get; set; }
    }
}