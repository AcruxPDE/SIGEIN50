using OfficeOpenXml;
using SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.Entidades.EvaluacionOrganizacional;
using System.Web.Security;

namespace SIGE.Negocio.EvaluacionOrganizacional
{
   public class RotacionPersonalNegocio
    {
        public E_RESULTADO InsertaBajaEmpleado(E_BAJA_EMPLEADO pBaja, string pCL_USUARIO, string pNB_PROGRAMA, string pTIPO_TRANSACCION)
        {
            RotacionPersonalOperaciones operaciones = new RotacionPersonalOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarBajaEmpleado(pBaja, pCL_USUARIO, pNB_PROGRAMA, pTIPO_TRANSACCION));
        }

        public E_RESULTADO InsertaBajaManualEmpleado(E_BAJA_EMPLEADO pBaja, string pCL_USUARIO, string pNB_PROGRAMA, string pTIPO_TRANSACCION)
        {
            RotacionPersonalOperaciones operaciones = new RotacionPersonalOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaBajaManualEmpleado(pBaja, pCL_USUARIO, pNB_PROGRAMA, pTIPO_TRANSACCION));
        }

        public List<SPE_OBTIENE_HISTORIAL_BAJAS_Result> ObtenerHistorialBajas(int? pID_EMPLEADO = null, int? pID_CAUSA_ROTACION = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            RotacionPersonalOperaciones oPeriodo = new RotacionPersonalOperaciones();
            return oPeriodo.ObtenerHistorialBajas(pID_EMPLEADO, pID_CAUSA_ROTACION, pID_EMPRESA, pID_ROL);
        }

        public List<SPE_OBTIENE_EO_GRAFICA_INDICE_ROTACION_Result> ObtieneGraficaIndiceRotacion(DateTime? pFECHA_INICIO = null, DateTime? pFECHA_FINAL = null, string pTIPO_REPORTE = null, XElement pXML_FILTROS = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            RotacionPersonalOperaciones oPeriodo = new RotacionPersonalOperaciones();
            return oPeriodo.ObtenerGraficaIndiceRotacion(pFECHA_INICIO, pFECHA_FINAL, pTIPO_REPORTE, pXML_FILTROS, pID_EMPRESA, pID_ROL);
        }

        public List<SPE_OBTIENE_EO_GRAFICA_CAUSA_ROTACION_Result> ObtieneGraficaCausaRotacion(DateTime? pFECHA_INICIO = null, DateTime? pFECHA_FINAL = null, XElement pXML_FILTROS = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            RotacionPersonalOperaciones oPeriodo = new RotacionPersonalOperaciones();
            return oPeriodo.ObtenerGraficaCausaRotacion(pFECHA_INICIO, pFECHA_FINAL, pXML_FILTROS, pID_EMPRESA, pID_ROL);
        }

        public List<SPE_OBTIENE_CLAVE_EMPLEADO_INDICE_ROTACION_Result> ObtieneEmpleadosIndiceRotacion(DateTime? pFECHA_INICIO = null, DateTime? pFECHA_FINAL = null, string pTIPO_REPORTE = null, XElement pXML_FILTROS = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            RotacionPersonalOperaciones oPeriodo = new RotacionPersonalOperaciones();
            return oPeriodo.ObtenerEmpleadosIndiceRotacion(pFECHA_INICIO, pFECHA_FINAL, pTIPO_REPORTE, pXML_FILTROS, pID_EMPRESA, pID_ROL);
        }

        public List<SPE_OBTIENE_EO_PLAZAS_EMPLEADO_Result> ObtienePlazasEmpleado( int ID_EMPLEADO)
        {
            RotacionPersonalOperaciones oPeriodo = new RotacionPersonalOperaciones();
            return oPeriodo.ObtenerPlazasEmpleado(ID_EMPLEADO);
        }

        public List<SPE_OBTIENE_ADSCRIPCIONES_Result> ObtieneCatalogoAdscripciones(int? pIdCatalogoLista = null)
             {
                 RotacionPersonalOperaciones operaciones = new RotacionPersonalOperaciones();
                 return operaciones.ObtieneCatalogoAdscripciones(pIdCatalogoLista);

             }

        public List<E_BAJAS_PENDIENTES> ObtieneBajasPendientes(int? pIdBajaEmpleado = null, int? pIdEmpresa = null,int? pIdRol = null)
        {
            RotacionPersonalOperaciones oRotacion = new RotacionPersonalOperaciones();
            var vBajasPendientes = oRotacion.ObtieneBajasPendientes(pIdBajaEmpleado, pIdEmpresa, pIdRol).ToList();
            return (from x in vBajasPendientes
                    select new E_BAJAS_PENDIENTES
                    {
                        ID_BAJA_EMPLEADO = x.ID_BAJA_EMPLEADO,
                        ID_EMPLEADO = x.ID_EMPLEADO,
                        CL_EMPLEADO = x.CL_EMPLEADO,
                        NB_EMPLEADO = x.NB_EMPLEADO,
                        NB_PUESTO = x.NB_PUESTO,
                        CL_PUESTO = x.CL_PUESTO,
                        ID_PUESTO = x.ID_PUESTO,
                        ID_CAUSA_ROTACION = x.ID_CAUSA_ROTACION,
                        DS_COMENTARIOS = x.DS_COMENTARIOS,
                        FE_BAJA_EFECTIVA = x.FE_BAJA_EFECTIVA,
                        FE_CREACION = x.FE_CREACION,
                        CL_USUARIO_APP_CREA = x.CL_USUARIO_APP_CREA,
                        ID_PLAZA = x.ID_PLAZA
                    }).ToList();
        }

        public E_RESULTADO ActualizaBajaPendiente(int? pID_BAJA_EMPLEADO = null, int? pID_EMPLEADO = null, int? pID_CAUSA_ROTACION = null, string pDS_COMENTARIO = null, DateTime? pFE_BAJA_EFECTIVA = null, string pCL_USUARIO = null, string pNB_PROGRAMA = null, string pTIPO_TRANSACCION = null)
        {
            RotacionPersonalOperaciones operaciones = new RotacionPersonalOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizaBajaPendiente(pID_BAJA_EMPLEADO, pID_EMPLEADO, pID_CAUSA_ROTACION, pDS_COMENTARIO, pFE_BAJA_EFECTIVA, pCL_USUARIO, pNB_PROGRAMA, pTIPO_TRANSACCION));
        }

    }
}
