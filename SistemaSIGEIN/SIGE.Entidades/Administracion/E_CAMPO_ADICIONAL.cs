using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_CAMPO_ADICIONAL
    {
        public int ID_CAMPO { get; set; }
        public string CL_CAMPO { get; set; }
        public string NB_CAMPO { get; set; }
        public string DS_CAMPO { get; set; }
        public bool FG_REQUERIDO { get; set; }
        public string NO_VALOR_DEFECTO { get; set; }
        public string CL_TIPO_DATO { get; set; }
        public string CL_DIMENSION { get; set; }
        public string CL_TABLA_REFERENCIA { get; set; }
        public string CL_ESQUEMA_REFERENCIA { get; set; }
        
    }
}
