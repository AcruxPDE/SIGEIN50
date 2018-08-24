using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Externas
{
    [Serializable]
    public class E_COMPETENCIAS_PROCESO_SELECCION
    {
        private Nullable<decimal> vNoValorRespuesta;

        public int ID_BATERIA { get; set; }
        public int ID_CANDIDATO { get; set; }
        public int ID_COMPETENCIA { get; set; }

        public string CL_COLOR { get; set; }
        public string CL_CLASIFICACION { get; set; }
        public string NB_CLASIFICACION_COMPETENCIA { get; set; }
        public string DS_CLASIFICACION_COMPETENCIA { get; set; }
        public byte NO_ORDEN { get; set; }
        public string NB_COMPETENCIA { get; set; }
        public string DS_COMPETENCIA { get; set; }
        public string NB_RESPUESTA { get; set; }

        public Nullable<decimal> NO_VALOR_NIVEL { get; set; }
        public Nullable<decimal> PR_CUMPLIMIENTO_COMPETENCIA { get; set; }

        public Nullable<decimal> NO_VALOR_BAREMO {
            get { return vNoValorRespuesta; }
            set { this.vNoValorRespuesta = value; AsignarValor(); }
        }

        public bool FG_VALOR0 { get; set; }
        public bool FG_VALOR1 { get; set; }
        public bool FG_VALOR2 { get; set; }
        public bool FG_VALOR3 { get; set; }
        public bool FG_VALOR4 { get; set; }
        public bool FG_VALOR5 { get; set; }

        public string DS_NIVEL0 { get; set; }
        public string DS_NIVEL1 { get; set; }
        public string DS_NIVEL2 { get; set; }
        public string DS_NIVEL3 { get; set; }
        public string DS_NIVEL4 { get; set; }
        public string DS_NIVEL5 { get; set; }

        public E_COMPETENCIAS_PROCESO_SELECCION()
        {
            FG_VALOR0 = false;
            FG_VALOR1 = false;
            FG_VALOR2 = false;
            FG_VALOR3 = false;
            FG_VALOR4 = false;
            FG_VALOR5 = false;
        }

        public void AsignarValor()
        {
            if (NO_VALOR_BAREMO == 0)
            {
                FG_VALOR0 = true;
            }
            else if (NO_VALOR_BAREMO == 1)
            {
                FG_VALOR1 = true;
            }
            else if (NO_VALOR_BAREMO == 2)
            {
                FG_VALOR2 = true;
            }
            else if (NO_VALOR_BAREMO == 3)
            {
                FG_VALOR3 = true;
            }
            else if (NO_VALOR_BAREMO == 4)
            {
                FG_VALOR4 = true;
            }
            else if (NO_VALOR_BAREMO == 5)
            {
                FG_VALOR5 = true;
            }

        }
    }
}
