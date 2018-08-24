using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.FormacionDesarrollo
{
    public class E_REPORTE_FYD
    {
        public Guid ID_REPORTE_FYD { get; set; }
        public DateTime FE_INICIO { get; set; }
        public DateTime FE_FINAL { get; set; }
        public string CL_TIPO_CURSO { get; set; }
        public List<int> LISTA_CURSOS { get; set; }
        public List<int> LISTA_INSTRUCTORES { get; set; }
        public List<int> LISTA_COMPETENCIAS { get; set; }
        public List<int> LISTA_PARTICIPANTES { get; set; }
        public List<int> LISTA_EVENTOS { get; set; }

        public E_REPORTE_FYD()
        {
            LISTA_CURSOS = new List<int>();
            LISTA_INSTRUCTORES = new List<int>();
            LISTA_COMPETENCIAS = new List<int>();
            LISTA_PARTICIPANTES = new List<int>();
            LISTA_EVENTOS = new List<int>();
        }
    }
}
