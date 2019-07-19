using SIGE.Entidades;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIGE.Entidades.EvaluacionOrganizacional;
using System.Data.SqlClient;

namespace SIGE.AccesoDatos.Implementaciones.EvaluacionOrganizacional
{
    public class RotacionPersonalOperaciones
    {
        public XElement InsertarBajaEmpleado(E_BAJA_EMPLEADO pBaja, string pCL_USUARIO, string pNB_PROGRAMA, string pTIPO_TRANSACCION)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_BAJA_EMPLEADO(poutClaveRetorno,pBaja.ID_EMPLEADO, pBaja.CL_EMPLEADO, pBaja.NB_EMPLEADO, pBaja.ID_CAUSA_ROTACION, pBaja.DS_COMENTARIOS, pBaja.FE_BAJA_EFECTIVA, pCL_USUARIO, pNB_PROGRAMA, pTIPO_TRANSACCION);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertaBajaManualEmpleado(E_BAJA_EMPLEADO pBaja, string pCL_USUARIO, string pNB_PROGRAMA, string pTIPO_TRANSACCION)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_BAJA_MANUAL_EMPLEADO(poutClaveRetorno, pBaja.ID_EMPLEADO, pBaja.CL_EMPLEADO, pBaja.NB_EMPLEADO, pBaja.ID_CAUSA_ROTACION, pBaja.DS_COMENTARIOS, pBaja.FE_BAJA_EFECTIVA, pCL_USUARIO, pNB_PROGRAMA, pTIPO_TRANSACCION);
                return XElement.Parse(poutClaveRetorno.Value.ToString()); 
            }
        }

        public List<SPE_OBTIENE_HISTORIAL_BAJAS_Result> ObtenerHistorialBajas(int? pID_EMPLEADO = null,int? pID_CAUSA_ROTACION = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_HISTORIAL_BAJAS(pID_EMPLEADO, pID_CAUSA_ROTACION, pID_EMPRESA, pID_ROL).ToList(); 
            }
        }

        public List<SPE_OBTIENE_EO_GRAFICA_INDICE_ROTACION_Result> ObtenerGraficaIndiceRotacion(DateTime? pFECHA_INICIO = null, DateTime? pFECHA_FINAL = null, string pTIPO_REPORTE = null, XElement pXML_FILTROS = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                string vXML_FILTROS = null;
                if (pXML_FILTROS != null)
                    vXML_FILTROS = pXML_FILTROS.ToString();
                return context.SPE_OBTIENE_EO_GRAFICA_INDICE_ROTACION(pFECHA_INICIO, pFECHA_FINAL, pTIPO_REPORTE, vXML_FILTROS, pID_EMPRESA, pID_ROL).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_GRAFICA_CAUSA_ROTACION_Result> ObtenerGraficaCausaRotacion(DateTime? pFECHA_INICIO = null, DateTime? pFECHA_FINAL = null, XElement pXML_FILTROS = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                string vXML_FILTROS = null;
                if (pXML_FILTROS != null)
                    vXML_FILTROS = pXML_FILTROS.ToString();
                return context.SPE_OBTIENE_EO_GRAFICA_CAUSA_ROTACION(pFECHA_INICIO, pFECHA_FINAL, vXML_FILTROS, pID_EMPRESA, pID_ROL).ToList();
            }
        }

        public List<SPE_OBTIENE_CLAVE_EMPLEADO_INDICE_ROTACION_Result> ObtenerEmpleadosIndiceRotacion(DateTime? pFECHA_INICIO = null, DateTime? pFECHA_FINAL = null, string pTIPO_REPORTE = null, XElement pXML_FILTROS = null, int? pID_EMPRESA = null, int? pID_ROL = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                string vXML_FILTROS = null;
                if (pXML_FILTROS != null)
                    vXML_FILTROS = pXML_FILTROS.ToString();
                return context.SPE_OBTIENE_CLAVE_EMPLEADO_INDICE_ROTACION(pFECHA_INICIO, pFECHA_FINAL, pTIPO_REPORTE,vXML_FILTROS, pID_EMPRESA, pID_ROL).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_PLAZAS_EMPLEADO_Result> ObtenerPlazasEmpleado(int ID_EMPLEADO)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_PLAZAS_EMPLEADO(ID_EMPLEADO).ToList();
            }
        }

        public List<SPE_OBTIENE_ADSCRIPCIONES_Result> ObtieneCatalogoAdscripciones(int? pIdCatalogoLista = null)
        {
            using (EvaluacionOrganizacionalEntities context = new EvaluacionOrganizacionalEntities())
            {
                return context.SPE_OBTIENE_ADSCRIPCIONES(pIdCatalogoLista).ToList();
            }
        }

        public List<SPE_OBTIENE_EO_BAJAS_PENDIENTES_Result> ObtieneBajasPendientes( int? pIdBajaEmpleado = null, int? pIdEmpresa = null, int? pIdRol = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EO_BAJAS_PENDIENTES(pIdBajaEmpleado, pIdEmpresa, pIdRol).ToList();
            }
        }

        public XElement ActualizaBajaPendiente(int? pID_BAJA_EMPLEADO = null, int? pID_EMPLEADO = null, int? pID_CAUSA_ROTACION = null, string pDS_COMENTARIO = null, DateTime? pFE_BAJA_EFECTIVA= null, string pCL_USUARIO = null, string pNB_PROGRAMA = null, string pTIPO_TRANSACCION = null)
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_BAJA_PENDIENTE_EMPLEADO(poutClaveRetorno, pID_BAJA_EMPLEADO, pID_EMPLEADO, pID_CAUSA_ROTACION, pDS_COMENTARIO, pFE_BAJA_EFECTIVA, pCL_USUARIO, pNB_PROGRAMA, pTIPO_TRANSACCION);
                return XElement.Parse(poutClaveRetorno.Value.ToString());
            }
        }

        public List<E_BAJA_IMPORTANTE_EO> ObtenerEmpleadoImportante()
        {
            using (SistemaSigeinEntities context = new SistemaSigeinEntities())
            {
                return context.Database.SqlQuery<E_BAJA_IMPORTANTE_EO>("EXEC " +
                "EO.SPE_OBTIENE_CONFIGURACION_EO_BAJA_IMPORTANTE"
            ).ToList();
            }
        }
    }
}
