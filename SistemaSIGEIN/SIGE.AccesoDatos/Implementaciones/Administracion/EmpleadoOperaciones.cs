using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class EmpleadoOperaciones
    {

        private SistemaSigeinEntities context;

        List<SPE_OBTIENE_M_EMPLEADO_Result> x = new List<SPE_OBTIENE_M_EMPLEADO_Result>();


        public List<SPE_OBTIENE_EMPLEADOS_CAMPOS_EXTRA_Result> ObtenerEmpleadosCamposExtra(XElement pXmlSeleccion = null, bool? pFgFoto = false, string pClUsuario = null, bool? pFgActivo = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlSeleccion != null)
                    vXmlFiltro = pXmlSeleccion.ToString();
                return context.SPE_OBTIENE_EMPLEADOS_CAMPOS_EXTRA(vXmlFiltro,pClUsuario,pFgActivo, pFgFoto, pIdEmpresa).ToList();
            }
        }

        public List<SPE_OBTIENE_M_EMPLEADO_Result> ObtieneEmpleado(int? ID_EMPLEADO = null, String CL_EMPLEADO = null, String NB_EMPLEADO = null, String NB_APELLIDO_PATERNO = null, String NB_APELLIDO_MATERNO = null, String CL_ESTADO_EMPLEADO = null, String CL_GENERO = null, String CL_ESTADO_CIVIL = null, String NB_CONYUGUE = null, String CL_RFC = null, String CL_CURP = null, String CL_NSS = null, String CL_TIPO_SANGUINEO = null, String CL_NACIONALIDAD = null, String NB_PAIS = null, String NB_ESTADO = null, String NB_MUNICIPIO = null, String NB_COLONIA = null, String NB_CALLE = null, String NO_INTERIOR = null, String NO_EXTERIOR = null, String CL_CODIGO_POSTAL = null, String CL_CORREO_ELECTRONICO = null, bool? FG_ACTIVO = null, System.DateTime? FE_NACIMIENTO = null, String DS_LUGAR_NACIMIENTO = null, System.DateTime? FE_ALTA = null, System.DateTime? FE_BAJA = null, int? ID_PUESTO = null, Decimal? MN_SUELDO = null, Decimal? MN_SUELDO_VARIABLE = null, String DS_SUELDO_COMPOSICION = null, int? ID_CANDIDATO = null, int? ID_EMPRESA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_M_EMPLEADO(ID_EMPLEADO, CL_EMPLEADO, NB_EMPLEADO, NB_APELLIDO_PATERNO, NB_APELLIDO_MATERNO, CL_ESTADO_EMPLEADO, CL_GENERO, CL_ESTADO_CIVIL, NB_CONYUGUE, CL_RFC, CL_CURP, CL_NSS, CL_TIPO_SANGUINEO, CL_NACIONALIDAD, NB_PAIS, NB_ESTADO, NB_MUNICIPIO, NB_COLONIA, NB_CALLE, NO_INTERIOR, NO_EXTERIOR, CL_CODIGO_POSTAL, CL_CORREO_ELECTRONICO, FG_ACTIVO, FE_NACIMIENTO, DS_LUGAR_NACIMIENTO, FE_ALTA, FE_BAJA, ID_PUESTO, MN_SUELDO, MN_SUELDO_VARIABLE, DS_SUELDO_COMPOSICION, ID_CANDIDATO, ID_EMPRESA).ToList();  //(ID_EMPLEADO,CL_EMPLEADO,NB_EMPLEADO,NB_APELLIDO_PATERNO,NB_APELLIDO_MATERNO,CL_ESTADO_EMPLEADO,CL_GENERO,CL_ESTADO_CIVIL,NB_CONYUGUE,CL_RFC,CL_CURP,CL_NSS,CL_TIPO_SANGUINEO,CL_NACIONALIDAD,NB_PAIS,NB_ESTADO,NB_MUNICIPIO,NB_COLONIA,NB_CALLE,NO_INTERIOR,NO_EXTERIOR,CL_CODIGO_POSTAL,XML_TELEFONOS,CL_CORREO_ELECTRONICO,FG_ACTIVO,FE_NACIMIENTO,DS_LUGAR_NACIMIENTO,FE_ALTA,FE_BAJA,ID_PUESTO,MN_SUELDO,MN_SUELDO_VARIABLE,DS_SUELDO_COMPOSICION,ID_CANDIDATO,ID_EMPRESA,XML_CAMPOS_ADICIONALES)
            }
        }

        public List<SPE_OBTIENE_EMPLEADOS_Result> ObtenerEmpleados(XElement pXmlSeleccion = null, bool? pFgFoto = false, string pClUsuario = null, bool? pFgActivo = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlSeleccion != null)
                    vXmlFiltro = pXmlSeleccion.ToString();
                return context.SPE_OBTIENE_EMPLEADOS(vXmlFiltro, pClUsuario, pFgActivo, pFgFoto, pIdEmpresa).ToList();
            }
        }

        public List<SPE_OBTIENE_EVALUACION_COMPETENCIAS_PLAN_SUCESION_Result> ObtieneEvalCompetenciasSucesion(int? vIdEmpleadoSuceder = null, int? vIdEmpleadoSucesor = null, int? vIdPuestoSuceder = null, int? vIdPuestoSucesor = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EVALUACION_COMPETENCIAS_PLAN_SUCESION(vIdEmpleadoSuceder, vIdEmpleadoSucesor, vIdPuestoSuceder, vIdPuestoSucesor).ToList();
            }
        }

        public List<SPE_OBTIENE_EMPLEADOS_SELECTOR_Result> ObtenerEmpleadosSelector(XElement pXmlSeleccion = null, bool? pFgFoto = false, string pClUsuario = null, bool? pFgActivo = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlSeleccion != null)
                    vXmlFiltro = pXmlSeleccion.ToString();
                return context.SPE_OBTIENE_EMPLEADOS_SELECTOR(vXmlFiltro, pClUsuario, pFgActivo, pFgFoto, pIdEmpresa).ToList();
            }
        }
     
        public SPE_OBTIENE_EMPLEADO_PLANTILLA_Result ObtenerPlantilla(int? pIdPlantilla, int? pIdEmpleado, int? pidEmpresa)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EMPLEADO_PLANTILLA(pIdPlantilla, pIdEmpleado, pidEmpresa).FirstOrDefault();
            }
        }

        public SPE_OBTIENE_EMPLEADO_PLANTILLA_PDE_Result ObtenerPlantillaPDE(int? pIdPlantilla, string pIdEmpleado, string pClFormulario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EMPLEADO_PLANTILLA_PDE(pIdPlantilla, pIdEmpleado,pClFormulario).FirstOrDefault();
            }
        }

        public SPE_OBTIENE_EMPLEADO_PLANTILLA_CAMBIO_PDE_Result ObtenerPlantillaCambioPDE(int? pIdPlantilla, string pIdEmpleado, string pClFormulario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EMPLEADO_PLANTILLA_CAMBIO_PDE(pIdPlantilla, pIdEmpleado, pClFormulario).FirstOrDefault();
            }
        }

        public ObjectParameter GetObjectParameter<T>(string pNbParametro, T pValor)
        {
            var pParameter = pValor != null ?
                new ObjectParameter(pNbParametro, pValor) :
                new ObjectParameter(pNbParametro, typeof(T));

            return pParameter;
        }

        public XElement InsertarActualizarEmpleado(XElement pXmlEmpleado, int? pIdEmpleado, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma, string vTipoTransaccion)
        {
            using (context = new SistemaSigeinEntities())
            {
                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                XElement vXmlDocumentos = new XElement("DOCUMENTOS");
                pLstDocumentos.ForEach(s => vXmlDocumentos.Add(new XElement("DOCUMENTO",
                    new XAttribute("ID_ITEM", s.ID_ITEM),
                    new XAttribute("ID_DOCUMENTO", s.ID_DOCUMENTO ?? 0),
                    new XAttribute("ID_ARCHIVO", s.ID_ARCHIVO ?? 0),
                    new XAttribute("NB_DOCUMENTO", s.NB_DOCUMENTO),
                    new XAttribute("CL_TIPO_DOCUMENTO", s.CL_TIPO_DOCUMENTO))
                ));

                List<UDTT_ARCHIVO> vLstArchivos = pLstArchivosTemporales;

                DataTable dtArchivos = new DataTable();
                dtArchivos.Columns.Add(new DataColumn("ID_ITEM", typeof(SqlGuid)));
                dtArchivos.Columns.Add(new DataColumn("ID_ARCHIVO", typeof(int)));
                dtArchivos.Columns.Add(new DataColumn("NB_ARCHIVO"));
                dtArchivos.Columns.Add(new DataColumn("FI_ARCHIVO", typeof(SqlBinary)));

                vLstArchivos.ForEach(f => dtArchivos.Rows.Add(f.ID_ITEM, f.ID_ARCHIVO ?? 0, f.NB_ARCHIVO, f.FI_ARCHIVO));

                var pArchivos = new SqlParameter("@PIN_ARCHIVOS", SqlDbType.Structured);
                pArchivos.Value = dtArchivos;
                pArchivos.TypeName = "ADM.UDTT_ARCHIVO";

                context.Database.ExecuteSqlCommand("EXEC " +
                    "ADM.SPE_INSERTA_ACTUALIZA_EMPLEADO " +
                    "@XML_RESULTADO OUTPUT, " +
                    "@PIN_XML_PLANTILLA, " +
                    "@PIN_ID_EMPLEADO, " +
                    "@PIN_XML_DOCUMENTOS, " +
                    "@PIN_ARCHIVOS, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA," +
                    "@PIN_TIPO_TRANSACCION"
                    , pXmlResultado
                    , new SqlParameter("@PIN_XML_PLANTILLA", SqlDbType.Xml) { Value = new SqlXml(pXmlEmpleado.CreateReader()) }
                    , new SqlParameter("@PIN_ID_EMPLEADO", (object)pIdEmpleado ?? DBNull.Value)
                    , new SqlParameter("@PIN_XML_DOCUMENTOS", SqlDbType.Xml) { Value = new SqlXml(vXmlDocumentos.CreateReader()) }
                    , pArchivos
                    , new SqlParameter("@PIN_CL_USUARIO", pClUsuario)
                    , new SqlParameter("@PIN_NB_PROGRAMA", pNbPrograma)
                    , new SqlParameter("@PIN_TIPO_TRANSACCION", vTipoTransaccion)
                );
                return XElement.Parse(pXmlResultado.Value.ToString()); 
            }
        }

        public XElement InsertarActualizarEmpleadoPDE(XElement pXmlEmpleado, string pIdEmpleado, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma, XElement xmlNuevaPlantilla)
        {
            using (context = new SistemaSigeinEntities())
            {
                var pXmlResultado = new SqlParameter("@XML_RESULTADO", SqlDbType.Xml)
                {
                    Direction = ParameterDirection.Output
                };

                XElement vXmlDocumentos = new XElement("DOCUMENTOS");
                pLstDocumentos.ForEach(s => vXmlDocumentos.Add(new XElement("DOCUMENTO",
                    new XAttribute("ID_ITEM", s.ID_ITEM),
                    new XAttribute("ID_DOCUMENTO", s.ID_DOCUMENTO ?? 0),
                    new XAttribute("ID_ARCHIVO", s.ID_ARCHIVO ?? 0),
                    new XAttribute("NB_DOCUMENTO", s.NB_DOCUMENTO),
                    new XAttribute("CL_TIPO_DOCUMENTO", s.CL_TIPO_DOCUMENTO))
                
                ));

                List<UDTT_ARCHIVO> vLstArchivos = pLstArchivosTemporales;

                DataTable dtArchivos = new DataTable();
                dtArchivos.Columns.Add(new DataColumn("ID_ITEM", typeof(SqlGuid)));
                dtArchivos.Columns.Add(new DataColumn("ID_ARCHIVO", typeof(int)));
                dtArchivos.Columns.Add(new DataColumn("NB_ARCHIVO"));
                dtArchivos.Columns.Add(new DataColumn("FI_ARCHIVO", typeof(SqlBinary)));

                vLstArchivos.ForEach(f => dtArchivos.Rows.Add(f.ID_ITEM, f.ID_ARCHIVO ?? 0, f.NB_ARCHIVO, f.FI_ARCHIVO));

                var pArchivos = new SqlParameter("@PIN_ARCHIVOS", SqlDbType.Structured);
                pArchivos.Value = dtArchivos;
                pArchivos.TypeName = "ADM.UDTT_ARCHIVO";

                context.Database.ExecuteSqlCommand("EXEC " +
                    "PDE.SPE_INSERTA_ACTUALIZA_EMPLEADO_PDE " +
                    "@XML_RESULTADO OUTPUT, " +
                    "@PIN_XML_PLANTILLA, " +
                    "@PIN_ID_EMPLEADO, " +
                    "@PIN_XML_DOCUMENTOS, " +
                    "@PIN_ARCHIVOS, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA," +
                    "@PIN_XML_NUEVA_PLANTILLA"
                    , pXmlResultado
                    , new SqlParameter("@PIN_XML_PLANTILLA", SqlDbType.Xml) { Value = new SqlXml(pXmlEmpleado.CreateReader()) }
                    , new SqlParameter("@PIN_ID_EMPLEADO", (object)pIdEmpleado ?? DBNull.Value)
                    , new SqlParameter("@PIN_XML_DOCUMENTOS", SqlDbType.Xml) { Value = new SqlXml(vXmlDocumentos.CreateReader()) }
                    , pArchivos
                    , new SqlParameter("@PIN_CL_USUARIO", pClUsuario)
                    , new SqlParameter("@PIN_NB_PROGRAMA", pNbPrograma)
                    , new SqlParameter("@PIN_XML_NUEVA_PLANTILLA", SqlDbType.Xml) { Value = new SqlXml(xmlNuevaPlantilla.CreateReader()) }
                );
                return XElement.Parse(pXmlResultado.Value.ToString());
            }
        }

        public XElement Elimina_M_EMPLEADO(int? ID_EMPLEADO = null, String CL_EMPLEADO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                //  pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_M_EMPLEADO(pout_clave_retorno, ID_EMPLEADO, CL_EMPLEADO, usuario, programa);
                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
 
        public List<SPE_OBTIENE_SUELDO_EMPLEADOS_Result> ObtenerSueldoEmpleados()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_SUELDO_EMPLEADOS().ToList();
            }
        }
     
        public List<SPE_OBTIENE_PERFIL_EMPLEADOS_Result> ObtenerPerfilEmpleados()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PERFIL_EMPLEADOS().ToList();
            }
        }
     
        public List<SPE_OBTIENE_CAPACITACIONES_EMPLEADO_Result> ObtenerCapacitacionEmpleados()
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CAPACITACIONES_EMPLEADO().ToList();
            }
        }

        public XElement ActualizarBajaEmpleados()
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                
                context.SPE_ACTUALIZA_BAJA_DEFINITIVA_EMPLEADO(pout_clave_retorno, null);

                //regresamos el valor de retorno de sql				
                return XElement.Parse(pout_clave_retorno.Value.ToString());   
            }
        }

        public XElement CancelarBajaEmpleado(int ID_EMPLEADO, string CL_USUARIO, string NB_PROGRAMA)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_EO_BAJA_EMPLEADO(pOutClRetorno, ID_EMPLEADO, CL_USUARIO, NB_PROGRAMA);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public string ReporteEmpleadoPorModulo(int pIdEmpleado)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EMPLEADO_REPORTE_POR_MODULO(pIdEmpleado).FirstOrDefault();
            }
        }

        public XElement ActualizaReingresoEmpleado(string pXmlDatosEmpleado, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_EMPLEADO_REINGRESO(pOutClRetorno, pXmlDatosEmpleado, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

    }
}
