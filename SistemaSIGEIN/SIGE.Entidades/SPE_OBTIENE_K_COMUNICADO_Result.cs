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
    
    public partial class SPE_OBTIENE_K_COMUNICADO_Result
    {
        public int ID_COMUNICADO { get; set; }
        public string ID_EMPLEADO { get; set; }
        public string NB_COMUNICADO { get; set; }
        public System.DateTime FE_COMUNICADO { get; set; }
        public string DS_COMUNICADO { get; set; }
        public System.DateTime FE_VISIBLE_DEL { get; set; }
        public System.DateTime FE_VISIBLE_AL { get; set; }
        public Nullable<int> ID_ARCHIVO_PDE { get; set; }
        public System.DateTime FE_CREACION { get; set; }
        public bool FG_LEIDO { get; set; }
        public bool FG_ESTATUS { get; set; }
        public Nullable<byte> FG_PRIVADO { get; set; }
        public string NB_ARCHIVO { get; set; }
        public string TIPO_COMUNICADO { get; set; }
        public string TIPO_ACCION { get; set; }
    }
}
