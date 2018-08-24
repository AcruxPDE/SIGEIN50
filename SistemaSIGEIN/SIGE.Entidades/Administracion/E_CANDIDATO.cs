using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_CANDIDATO
    {
        public int ID_CANDIDATO { get; set; }
        public string NB_CANDIDATO { get; set; }
        public string NB_APELLIDO_PATERNO { get; set; }
        public string NB_APELLIDO_MATERNO { get; set; }
        public string CL_GENERO { get; set; }
        public string CL_RFC { get; set; }
        public string CL_CURP { get; set; }
        public string CL_ESTADO_CIVIL { get; set; }
        public string NB_CONYUGUE { get; set; }
        public string CL_NSS { get; set; }
        public string CL_TIPO_SANGUINEO { get; set; }
        public string NB_PAIS { get; set; }
        public string NB_ESTADO { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string NB_COLONIA { get; set; }
        public string NB_CALLE { get; set; }
        public string NO_INTERIOR { get; set; }
        public string NO_EXTERIOR { get; set; }
        public string CL_CODIGO_POSTAL { get; set; }
        public string CL_CORREO_ELECTRONICO { get; set; }
        public System.DateTime? FE_NACIMIENTO { get; set; }
        public string DS_LUGAR_NACIMIENTO { get; set; }
        public Nullable<decimal> MN_SUELDO { get; set; }
        public string CL_NACIONALIDAD { get; set; }
        public string DS_NACIONALIDAD { get; set; }
        public string NB_LICENCIA { get; set; }
        public string DS_VEHICULO { get; set; }
        public string CL_CARTILLA_MILITAR { get; set; }
        public string CL_CEDULA_PROFESIONAL { get; set; }
        public string XML_TELEFONOS { get; set; }
        public string XML_INGRESOS { get; set; }
        public string XML_EGRESOS { get; set; }
        public string XML_PATRIMONIO { get; set; }
        public string DS_DISPONIBILIDAD { get; set; }
        public string CL_DISPONIBILIDAD_VIAJE { get; set; }
        public string XML_PERFIL_RED_SOCIAL { get; set; }
        public string DS_COMENTARIO { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string DS_FILTRO { get; set; }
        public int NO_EDAD { get; set; }
        public string CL_SOLICITUD { get; set; }
        public int ID_BATERIA { get; set; }
        public string FL_BATERIA { get; set; }
    }
}
