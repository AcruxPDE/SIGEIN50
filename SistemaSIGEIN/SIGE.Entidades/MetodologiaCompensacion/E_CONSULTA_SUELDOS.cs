using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.MetodologiaCompensacion
{
    [Serializable]
    public class E_CONSULTA_SUELDOS
    {
        public int? NUM_ITEM { set; get; }
        public int? ID_TABULADOR_EMPLEADO { set; get; }
        public string NB_TABULADOR_NIVEL { set; get; }
        public int? NO_CATEGORIA { set; get; }
        public int? NO_NIVEL { set; get; }
        public string CL_PUESTO { set; get; }
        public string NB_PUESTO { set; get; }
        public string CL_EMPLEADO { set; get; }
        public string CL_DEPARTAMENTO { set; get; }
        public string NB_DEPARTAMENTO { set; get; }
        public string NB_EMPLEADO { set; get; }
        public decimal? NO_CUARTIL_INCREMENTO { set; get; }
        public decimal? MN_MINIMO_CUARTIL { set; get; }
        public decimal? MN_MAXIMO_CUARTIL { set; get; }
        public decimal? MN_MAXIMO_MINIMO { set; get; }
        public decimal? MN_MINIMO_MINIMO { set; get; }
        public decimal? MN_MAXIMO_PRIMER_CUARTIL { set; get; }
        public decimal? MN_MINIMO_PRIMER_CUARTIL { set; get; }
        public decimal? MN_MAXIMO_MEDIO { set; get; }
        public decimal? MN_MINIMO_MEDIO { set; get; }
        public decimal? MN_MINIMO_SEGUNDO_CUARTIL { set; get; }
        public decimal? MN_MAXIMO_SEGUNDO_CUARTIL { set; get; }
        public decimal? MN_MINIMO_MAXIMO { set; get; }
        public decimal? MN_MAXIMO_MAXIMO { set; get; }
        public decimal? MN_SUELDO_ORIGINAL { set; get; }
        public decimal? DIFERENCIA { set; get; }
        public decimal? PR_DIFERENCIA { set; get; }
        public int? FG_DIFERENCIA { set; get; }
        public decimal? MN_SUELDO_NUEVO { set; get; }
        //public decimal? MN_SUELDO_NUEVO_INICIAL { set; get; }
        public decimal? DIFERENCIA_NUEVO { set; get; }
        public decimal? PR_DIFERENCIA_NUEVO { set; get; }
        public DateTime? FE_CAMBIO_SUELDO { set; get; }
        public string ICONO { set; get; }
        public string ICONO_NUEVO { set; get; }
        public string COLOR_DIFERENCIA { set; get; }
        public string COLOR_DIFERENCIA_NUEVO { set; get; }
        public decimal? INCREMENTO { set; get; }
        public decimal? PR_INCREMENTO { set; get; }
        public decimal? MN_MINIMO { set; get; }
        public decimal? MN_MAXIMO { set; get; }
        public decimal? DIFERENCIA_MERCADO { set; get; }
        public decimal? PR_DIFERENCIA_MERCADO { set; get; }
        public string COLOR_DIFERENCIA_MERCADO { set; get; }
        public string ICONO_MERCADO { set; get; }
        public string XML_CATEGORIAS { set; get; }
        public int CUARTIL_SELECCIONADO { set; get; }
        public int? NO_VALUACION { set; get; }
        public List<E_CATEGORIA> lstCategorias { set; get; }
        public Nullable<bool> FG_SUELDO_VISIBLE_TABULADOR { set; get; }
    }
}
