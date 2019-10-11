using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_PLANTILLA_NOMINA
    {
        public System.Guid ID_PLANTILLA { get; set; }
        public string CL_CLIENTE { get; set; }
        public string NB_PATERNO { get; set; }
        public string NB_MATERNO { get; set; }
        public string NB_NOMBRES { get; set; }
        public string NB_TRABAJADOR { get; set; }
        public string CL_TRABAJADOR { get; set; }
        public string CL_TIPO_NOMINA { get; set; }
        public System.DateTime? FE_NACIMIENTO { get; set; }
        public string CL_SEXO { get; set; }
        public string DS_SEXO { get; set; }
        public string CL_ESTADO_CIVIL { get; set; }
        public string DS_ESTADO_CIVIL { get; set; }
        public string DS_NACIONALIDAD { get; set; }
        public string CL_RFC { get; set; }
        public string CL_CURP { get; set; }
        public string NB_CALLE { get; set; }
        public string NB_PUESTO { get; set; }
        public string NO_EXTERIOR { get; set; }
        public string NO_INTERIOR { get; set; }
        public string NB_COLONIA { get; set; }
        public string NB_MUNICIPIO { get; set; }
        public string CL_ESTADO { get; set; }
        public string CL_CP { get; set; }
        public string NO_TELEFONO_FIJO { get; set; }
        public string NO_TELEFONO_CELULAR { get; set; }
        public string DS_EMAIL { get; set; }
        public string DS_ACCIDENTE { get; set; }
        public string DS_LUGAR_NACIMIENTO { get; set; }
        public string CL_ESTADO_NACIMIENTO { get; set; }
        public string NB_PADRE { get; set; }
        public string NB_MADRE { get; set; }
        public string DS_GRUPO_SANGUINEO { get; set; }
        public string CL_DEPARTAMENTO { get; set; }
        public string CL_PUESTO { get; set; }
        public string NO_IMSS { get; set; }
        public System.DateTime? FE_REINGRESO { get; set; }
        public Nullable<System.DateTime> FE_PLANTA { get; set; }
        public System.DateTime? FE_ANTIGUEDAD { get; set; }
        public Nullable<System.DateTime> FE_BAJA { get; set; }
        public Nullable<System.DateTime> FE_ULTIMA_PV { get; set; }
        public Nullable<System.DateTime> FE_ULTIMO_AG { get; set; }
        public decimal? MN_SNOMINAL_MENSUAL { get; set; }
        public decimal? MN_SNOMINAL { get; set; }
        public decimal? MN_SBC_FIJO { get; set; }
        public decimal? MN_SBC_VARIABLE { get; set; }
        public decimal? MN_SBC_DETERMINADO { get; set; }
        public decimal? MN_SBC_MAXIMO { get; set; }
        public decimal? MN_SBC { get; set; }
        public bool? FG_COTIZA_IMSS { get; set; }
        public string NO_UMF { get; set; }
        public string CL_FORMA_PAGO { get; set; }
        public string CL_BANCO_SAT { get; set; }
        public string NO_CUENTA_PAGO { get; set; }
        public string NO_CLABE_PAGO { get; set; }
        public string NO_CUENTA_DESPENSA { get; set; }
        public string CL_CENTRO_OPERATIVO { get; set; }
        public string CL_CENTRO_ADMVO { get; set; }
        public string FILLER01 { get; set; }
        public string FILLER02 { get; set; }
        public string FILLER03 { get; set; }
        public string FILLER04 { get; set; }
        public string FILLER05 { get; set; }
        public System.Guid? ID_RAZON_SOCIAL { get; set; }
        public string CL_RAZON_SOCIAL { get; set; }
        public string NB_RAZON_SOCIAL { get; set; }
        public string ID_EMPLEADO { get; set; }
        public string CL_REGISTRO_PATRONAL { get; set; }
        public string CL_PAQUETE { get; set; }
        public string CL_FORMATO_DISPERSION { get; set; }
        public string CL_FORMATO_VALES_G { get; set; }
        public string CL_FORMATO_VALES_D { get; set; }
        public string DS_DESCRIPCION_GRAL { get; set; }
        public string CL_GRAL { get; set; }
        public System.Guid? ID_PAQUETE_PRESTACIONES { get; set; }
        public Nullable<decimal> NO_FACTOR_SBC { get; set; }
        public string CL_TIPO_TRAB_SUA { get; set; }
        public string DS_TIPO_TRAB_SUA { get; set; }
        public string CL_JORNADA_SUA { get; set; }
        public string DS_JORNADA_SUA { get; set; }
        public string CL_UBICACION_SUA { get; set; }
        public Nullable<decimal> CL_TIPO_SALARIO_SUA { get; set; }
        public string DS_TIPO_SALARIO_SUA { get; set; }
        public string DS_LEYENDA_RECIBO { get; set; }
        public string NB_DEPARTAMENTO { get; set; }
        public byte[] IM_FOTOGRAFIA { get; set; }
        public string IM_FOTOGRAFIA_BASE64 { get; set; }
        public string CL_HORARIO { get; set; }
        public string CL_HORARIO_SEMANA { get; set; }
        public string CL_TIPO_CONTRATO { get; set; }
        public string DS_TIPO_CONTRATO { get; set; }
        public string CL_TIPO_JORNADA { get; set; }
        public string DS_TIPO_JORNADA { get; set; }
        public string NO_REGISTRO_PATRONAL { get; set; }
        public bool? CL_SUBCONTRATADO { get; set; }
        public string CL_TIPO_RIESGO_TRABAJO { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string CL_REGIMEN_CONTRATACION { get; set; }
        public string XML_TELEFONOS { get; set; }
        public string CL_PENSIONADO { get; set; }
        public string PENSIONADO { get; set; }
    }
}
