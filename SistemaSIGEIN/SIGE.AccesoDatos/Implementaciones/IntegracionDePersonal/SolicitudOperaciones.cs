﻿using System;
using System.Collections.Generic;
using System.Linq;
using SIGE.Entidades;
using System.Data.Objects;
using SIGE.Entidades.IntegracionDePersonal;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{

    public class SolicitudOperaciones
    {
        private SistemaSigeinEntities contexto;

        public List<SPE_OBTIENE_K_SOLICITUD_Result> ObtenerSolicitudes(int? ID_SOLICITUD = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, int? ID_DESCRIPTIVO = null, int? ID_REQUISICION = null, String CL_SOLICITUD = null, String CL_ACCESO_EVALUACION = null, int? ID_PLANTILLA_SOLICITUD = null, String DS_COMPETENCIAS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, String CL_SOLICITUD_ESTATUS = null, DateTime? FE_SOLICITUD = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_K_SOLICITUD(ID_SOLICITUD, ID_CANDIDATO, ID_EMPLEADO, ID_DESCRIPTIVO, ID_REQUISICION, CL_SOLICITUD, CL_ACCESO_EVALUACION, ID_PLANTILLA_SOLICITUD, DS_COMPETENCIAS_ADICIONALES).ToList();//, CL_SOLICITUD_ESTATUS, FE_SOLICITUD).ToList();
            }
        }

        public XElement InsertarActualizarSolicitud(XElement pXmlSolicitud, int? pIdSolicitud, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma, string pTipoTransaccion)
        {
            using (contexto = new SistemaSigeinEntities())
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
                    new XAttribute("CL_TIPO_DOCUMENTO", s.CL_TIPO_DOCUMENTO),
                    new XAttribute("CL_PROCEDENCIA", "META"))
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

                contexto.Database.ExecuteSqlCommand("EXEC " +
                    "IDP.SPE_INSERTA_ACTUALIZA_SOLICITUD " +
                    "@XML_RESULTADO OUTPUT, " +
                    "@PIN_XML_SOLICITUD_PLANTILLA, " +
                    "@PIN_ID_SOLICITUD, " +
                    "@PIN_XML_DOCUMENTOS, " +
                    "@PIN_ARCHIVOS, " +
                    "@PIN_CL_USUARIO, " +
                    "@PIN_NB_PROGRAMA, " +
                    "@PIN_TIPO_TRANSACCION"
                    , pXmlResultado
                    , new SqlParameter("@PIN_XML_SOLICITUD_PLANTILLA", SqlDbType.Xml) { Value = new SqlXml(pXmlSolicitud.CreateReader()) }
                    , new SqlParameter("@PIN_ID_SOLICITUD", (object)pIdSolicitud ?? DBNull.Value)
                    , new SqlParameter("@PIN_XML_DOCUMENTOS", SqlDbType.Xml) { Value = new SqlXml(vXmlDocumentos.CreateReader()) }
                    , pArchivos
                    , new SqlParameter("@PIN_CL_USUARIO", pClUsuario)
                    , new SqlParameter("@PIN_NB_PROGRAMA", pNbPrograma)
                    , new SqlParameter("@PIN_TIPO_TRANSACCION", pTipoTransaccion)
                );

                return XElement.Parse(pXmlResultado.Value.ToString());

            }
        }

        public XElement Elimina_K_SOLICITUD(int? ID_SOLICITUD = null, string usuario = null, string programa = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_K_SOLICITUD(pOutClRetorno, ID_SOLICITUD, usuario, programa);

                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public XElement ActualizaDatosSolicitudCorreo(int? ID_SOLICITUD = null, Guid? CL_ACCESO_CARTERA = null, string CL_CONTRASENA_CARTERA = null, string usuario = null, string programa = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ACTUALIZA_CARTERA_K_SOLICITUD(pOutClRetorno, ID_SOLICITUD, CL_ACCESO_CARTERA, CL_CONTRASENA_CARTERA, usuario, programa);

                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }


        public XElement Elimina_K_SOLICITUDES(XElement listaSolicitudes, string usuario = null, string programa = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_ELIMINA_SOLICITUDES(pOutClRetorno, listaSolicitudes.ToString(), usuario, programa);

                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_SOLICITUDES_Result> ObtenerCatalogoSolicitudes(string pXmlSeleccion = null)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                if (pXmlSeleccion == null)
                    pXmlSeleccion = (new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "TODAS")))).ToString();

                return contexto.SPE_OBTIENE_SOLICITUDES().ToList();
            }
        }

        public List<SPE_OBTIENE_CARTERA_Result> Obtener_SOLICITUDES_CARTERA()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CARTERA().ToList();
            }
        }

        public List<SPE_OBTIENE_CARTERA_A_ELIMINAR_Result> Obtener_SOLICITUDES_CARTERA_A_ELIMINAR()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_CARTERA_A_ELIMINAR().ToList();
            }
        }

        public SPE_OBTIENE_SOLICITUD_PLANTILLA_Result ObtenerSolicitudPlantilla(int? pIdPlantilla, int? pIdSolicitud, Guid? pFlPlantillaSolicitud)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_SOLICITUD_PLANTILLA(pIdPlantilla, pIdSolicitud, pFlPlantillaSolicitud).FirstOrDefault();
            }
        }

        public XElement InsertarCandidatoContratado(string pXmlDatosCandidato, string pClUsuario, string pNbPrograma)
        {
            using (contexto = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                contexto.SPE_INSERTA_EMPLEADO_CONTRATADO(pOutClRetorno, pXmlDatosCandidato, pClUsuario, pNbPrograma);

                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_SOLICITUDES_EMPLEO_Result> ObtenerSolicitudesEmpleo()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_SOLICITUDES_EMPLEO().ToList();
            }
        }

        public List<SPE_OBTIENE_DATOS_EMPLEADOS_Result> ObtenerDatosEmpleados()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_DATOS_EMPLEADOS().ToList();
            }
        }

        public List<SPE_OBTIENE_ENTREVISTAS_SELECCIONADOS_Result> ObtenerEntrevistasSeleccionados()
        {
            using (contexto = new SistemaSigeinEntities())
            {
                return contexto.SPE_OBTIENE_ENTREVISTAS_SELECCIONADOS().ToList();
            }
        }        
    }
}