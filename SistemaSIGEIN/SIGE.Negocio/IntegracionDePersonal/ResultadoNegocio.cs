using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.Administracion
{
    public class ResultadoNegocio
    {
        public E_RESULTADO insertaResultadosLaboral2(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones operaciones = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.insertaResultadosLaboral2(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }

        public E_RESULTADO insertaResultadosPensamiento(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosPensamiento(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }

        public E_RESULTADO insertaResultadosTiva(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosTiva(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }

        public E_RESULTADO insertaResultadosInteres(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosInteres(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }

        public E_RESULTADO insertaResultadosAdaptacionMedio(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosAdaptacionMedio(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }

        public E_RESULTADO insertaResultadosAptitud1(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosAptitud1(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }

        public E_RESULTADO insertaResultadosLaboral1(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosLaboral1(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }

        public E_RESULTADO insertaResultadosTecnicaPc(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosTecnicaPc(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }


        public E_RESULTADO insertaResultadosAptitud2(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones operaciones = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.insertaResultadosAptitud2(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }

        
        public E_RESULTADO insertaResultadosIngles(string pXmlResultados, int? pIdCuestionario, int pIdPrueba,string pnbPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosIngles(pXmlResultados, pIdCuestionario, pIdPrueba, pnbPrueba, usuario, programa));
        }

        public E_RESULTADO insertaResultadosOrtografias(string pXmlResultados, int? pIdCuestionario, int pIdPrueba,string pnbPruebas, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosOrtografias(pXmlResultados, pIdCuestionario, pIdPrueba, pnbPruebas, usuario, programa));
        }

        public E_RESULTADO insertaResultadosRedaccion(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosRedaccion(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }

        public E_RESULTADO insertaResultadosEntrevista(string pXmlResultados, int? pIdCuestionario, int pIdPrueba, string usuario, string programa)
        {
            ResultadoOperaciones op = new ResultadoOperaciones();
            return UtilRespuesta.EnvioRespuesta(op.insertaResultadosEntrevista(pXmlResultados, pIdCuestionario, pIdPrueba, usuario, programa));
        }
    }
}
