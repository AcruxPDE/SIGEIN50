using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    [Serializable]
    public class E_CURSO
    {
        public Guid ID_ITEM { get; set; }
        public int? ID_CURSO { get; set; }
        public string CL_CURSO { get; set; }
        public string NB_CURSO { get; set; }
        public string CL_TIPO_CURSO { get; set; }
        public int? ID_PUESTO_OBJETIVO { get; set; }
        public decimal NO_DURACION_CURSO { get; set; }
        public string DS_NOTAS { get; set; }

        public List<E_CURSO_COMPETENCIA> LS_COMPETENCIAS { get; set; }
        public List<E_TEMA> LS_TEMAS { get; set; }
        public List<E_CURSO_INSTRUCTOR> LS_INSTRUCTORES { get; set; }


        //Se agregan las áreas temáticas
        public E_CURSO_AREA_TEMATICA LS_AREAS_TEMATICAS { get; set; }

        public int ID_TEMA { get; set; }
        public string CL_TEMA { get; set; }
        public string NB_TEMA { get; set; }
        public string NO_DURACION { get; set; }
        public string DS_DESCRIPCION { get; set; }
        public string XML_DOCUMENTOS { get; set; }
        public string XML_CAMPOS_ADICIONALES { get; set; }

        public E_CURSO()
        {
            ID_ITEM = Guid.NewGuid();
            LS_COMPETENCIAS = new List<E_CURSO_COMPETENCIA>();
            LS_TEMAS = new List<E_TEMA>();
            LS_INSTRUCTORES = new List<E_CURSO_INSTRUCTOR>();
            LS_AREAS_TEMATICAS = new E_CURSO_AREA_TEMATICA();
        }
    }
}
