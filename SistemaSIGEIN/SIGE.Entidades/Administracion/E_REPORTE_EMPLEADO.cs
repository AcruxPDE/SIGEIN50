using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.Administracion
{
    [Serializable]
    public class E_REPORTE_EMPLEADO
    {
        public E_DATOS_IDP DatosIdp;
        public E_DATOS_FYD DatosFyd;
        public E_DATOS_EO DatosEo;
        public E_DATOS_MC DatosMc;

        public E_REPORTE_EMPLEADO()
        {
            DatosIdp = new E_DATOS_IDP();
            DatosFyd = new E_DATOS_FYD();
            DatosEo = new E_DATOS_EO();
            DatosMc = new E_DATOS_MC();
        }
    }

    #region IDP

    [Serializable]
    public class E_DATOS_IDP
    {
        public int ID_BATERIA { get; set; }
        public string FL_BATERIA { get; set; }
        public string CL_TOKEN { get; set; }
        public int ID_SOLICITUD { get; set; }
        public string CL_SOLICITUD { get; set; }
        public int ID_CANDIDATO { get; set; }

        public E_DATOS_IDP()
        {
            ID_BATERIA = 0;
            FL_BATERIA = "";
            CL_TOKEN = "";
            ID_SOLICITUD = 0;
            CL_SOLICITUD = "";
            ID_CANDIDATO = 0;
        }
    }

    #endregion

    #region FYD

    [Serializable]
    public class E_DATOS_FYD
    {
        public List<E_PROGRAMAS> vLstProgramas { get; set; }
        public List<E_EVENTOS> vLstEventos { get; set; }

        public E_DATOS_FYD()
        {
            vLstEventos = new List<E_EVENTOS>();
            vLstProgramas = new List<E_PROGRAMAS>();
        }
    }

    [Serializable]
    public class E_PROGRAMAS
    {
        public int ID_PROGRAMA { get; set; }
        public string CL_PROGRAMA { get; set; }
        public string NB_PROGRAMA { get; set; }
        public string CL_USUARIO_APP_CREA { get; set; }
        public DateTime FE_CREACION { get; set; }
    }

    [Serializable]
    public class E_EVENTOS
    {
        public int ID_EVENTO { get; set; }
        public string CL_EVENTO { get; set; }
        public string NB_EVENTO { get; set; }
        public string CL_CURSO { get; set; }
        public string NB_CURSO { get; set; }
        public decimal MN_COSTO_DIRECTO { get; set; }
        public DateTime FE_INICIO { get; set; }
        public DateTime FE_TERMINO { get; set; }
        public decimal? PR_CUMPLIMIENTO { get; set; }
    }

    #endregion

    #region EO

    [Serializable]
    public class E_DATOS_EO
    {
        public List<E_DESEMPENO> vLstDesempeno { get; set; }
        public int ID_PERIODO_CLIMA { get; set; }
        public int ID_EVALUADO_CLIMA { get; set; }
        public List<E_ROTACION> vLstRotacion { get; set; }

        public E_DATOS_EO()
        {
            vLstDesempeno = new List<E_DESEMPENO>();
            ID_PERIODO_CLIMA = 0;
            ID_EVALUADO_CLIMA = 0;
            vLstRotacion = new List<E_ROTACION>();
        }
    }

    [Serializable]
    public class E_DESEMPENO
    {
        public int ID_PERIODO { get; set; }
        public int ID_EVALUADO { get; set; }
        public string CL_PERIODO { get; set; }
        public string NB_PERIODO { get; set; }
        public decimal PR_CUMPLIMIENTO_EVALUADO { get; set; }

        public E_DESEMPENO()
        {
            ID_PERIODO = 0;
            ID_EVALUADO = 0;
            CL_PERIODO = "";
            NB_PERIODO = "";
            PR_CUMPLIMIENTO_EVALUADO = 0;
        }
    }

    [Serializable]
    public class E_ROTACION
    {
        public int ID_BAJA_EMPLEADO { get; set; }
        public DateTime FE_BAJA_EFECTIVA { get; set; }
        public string NB_MOTIVO { get; set; }
        public string DS_MOTIVO { get; set; }

        public E_ROTACION()
        {
            ID_BAJA_EMPLEADO = 0;
            FE_BAJA_EFECTIVA = DateTime.Now;
            NB_MOTIVO = "";
            DS_MOTIVO = "";
        }
    }

    #endregion

    #region MC

    [Serializable]
    public class E_DATOS_MC
    {
        public List<E_BITACORA_SUELDO> vLstBitacoraSueldos;

        public E_DATOS_MC()
        {
            vLstBitacoraSueldos = new List<E_BITACORA_SUELDO>();
        }
    }

    [Serializable]
    public class E_BITACORA_SUELDO
    {
        public int ID_BITACORA_SUELDO { get; set; }
        public DateTime FE_CAMBIO { get; set; }
        public string NB_PROCESO { get; set; }
        public string DS_PROCESO { get; set; }
        public string NB_ANTERIOR { get; set; }
        public string NB_ACTUAL { get; set; }

        public E_BITACORA_SUELDO()
        {
            ID_BITACORA_SUELDO = 0;
            FE_CAMBIO = DateTime.Now;
            NB_PROCESO = "";
            DS_PROCESO = "";
            NB_ANTERIOR = "";
            NB_ACTUAL = "";
        }

    }

    #endregion
}
