using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    public class E_ORGANIGRAMA_PUESTO
    {
        public List<E_ORGANIGRAMA_GRUPO_PUESTO> lstGrupo { get; set; }
        public List<E_ORGANIGRAMA_NODO_PUESTO> lstNodoDescendencia { get; set; }
        public List<E_ORGANIGRAMA_NODO_PUESTO> lstNodoAscendencia { get; set; }
    }

    public class E_ORGANIGRAMA_NODO_PUESTO
    {
        public int idNodo { get; set; }
        public int? idNodoSuperior { get; set; }
        public int idNodoP { get; set; }
        public int? idNodoSuperiorP { get; set; }
        public string clNodo { get; set; }
        public string nbNodo { get; set; }
        public string clTipoNodo { get; set; }
        public string clTipoGenealogia { get; set; }
        public int noNivel { get; set; }
        public int noNivelPuesto { get; set; }
        public string cssNodo { get; set; }
    }

    public class E_ORGANIGRAMA_GRUPO_PUESTO
    {
        public int? idNodo { get; set; }
        public int? idItem { get; set; }
        public string clItem { get; set; }
        public string nbItem { get; set; }
        public string cssItem { get; set; }
    }

    public class E_ORGANIGRAMA_AGRUPA_PUESTO
    {
        public int ID_PLAZA { get; set; }
        public int CANTIDAD { get; set; }
        public int ID_PUESTO { get; set; }
        public Nullable<int> ID_PUESTO_SUPERIOR { get; set; }
        public bool FG_ACTIVO { get; set; }
        public string CL_ACTIVO { get; set; }
        public Nullable<System.DateTime> FE_INACTIVO { get; set; }
        public string CL_PLAZA { get; set; }
        public string NB_PLAZA { get; set; }
        public string CL_PUESTO { get; set; }
        public string NB_PUESTO { get; set; }
        public string CL_TIPO_PUESTO { get; set; }
        public string CL_POSICION_ORGANIGRAMA { get; set; }
        public Nullable<int> ID_PLAZA_SUPERIOR { get; set; }
        public Nullable<int> NO_NIVEL { get; set; }
        public string CL_TIPO_GENEALOGIA { get; set; }
        public Nullable<int> NO_NIVEL_ORGANIGRAMA { get; set; }
    }
}