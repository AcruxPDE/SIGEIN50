using SIGE.Entidades;
using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
    public class ProcesoSeleccionOperaciones
    {
        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_PROCESO_SELECCION_Result> ObtenerProcesoSeleccion(int? pIdProcesoSeleccion = null, int? pIdCandidato = null, int? pIdRequisicion = null, int? pIdProcesoSeleccionActual = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PROCESO_SELECCION(pIdProcesoSeleccion, pIdProcesoSeleccionActual, pIdCandidato, pIdRequisicion).ToList();
            }
        }

        public XElement InsertarProcesoSeleccion(int? pIdCandidato = null, int? pIdEmpleado = null, int? pIdRequisicion = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_PROCESO_SELECCION(pOutClaveRetorno, pIdCandidato, pIdEmpleado, pIdRequisicion, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_ENTREVISTA_PROCESO_SELECCION_Result> ObtenerEntrevistasProcesoSeleccion(int? pIdProcesoSeleccion = null, int? pIdEntrevista = null, Guid? pFlEntrevista = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_ENTREVISTA_PROCESO_SELECCION(pIdProcesoSeleccion, pIdEntrevista, pFlEntrevista).ToList();
            }
        }

        public List<SPE_OBTIENE_TIPO_ENTREVISTA_Result> ObtenerTipoEntrevista(int? pIdEntrevistaTipo = null, string pClEntrevistaTipo = null, string pNbEntrevistaTipo = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_TIPO_ENTREVISTA(pIdEntrevistaTipo, pClEntrevistaTipo, pNbEntrevistaTipo).ToList();
            }
        }

        public XElement InsertarActualizarEntrevista(string pClTipoOperacion, E_ENTREVISTA pEntrevista, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_ENTREVISTA(pOutClaveRetorno, pEntrevista.ID_ENTREVISTA, pEntrevista.ID_PROCESO_SELECCION, pEntrevista.ID_ENTREVISTA_TIPO, pEntrevista.FE_ENTREVISTA, pEntrevista.ID_ENTREVISTADOR, pEntrevista.NB_ENTREVISTADOR, pEntrevista.NB_PUESTO_ENTREVISTADOR, pEntrevista.CL_CORREO_ENTREVISTADOR, pEntrevista.DS_OBSERVACIONES, pEntrevista.CL_TOKEN, pEntrevista.FL_ENTREVISTA, pClUsuario, pNbPrograma, pClTipoOperacion);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public XElement EliminarEntrevista(int pIdEntrevista)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_ENTREVISTA(pOutClaveRetorno, pIdEntrevista);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public XElement ActualizarComentarioEntrevista(int pIdEntrevista, string pDsObservaciones, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_COMENTARIO_ENTREVISTA(pOutClaveRetorno, pIdEntrevista, pDsObservaciones, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_K_EXPERIENCIA_LABORAL_Result> ObtenerExperienciaLaboral(int? pIdExperienciaLaboral = null, int? pIdCandidato = null, int? pIdEmpleado = null, String pNbEmpresa = null, String pDsDomicilio = null, String pNbGiro = null, DateTime? pFeInicio = null, DateTime? pFeFin = null, String pNbPuesto = null, String pNbFuncion = null, String pDsFunciones = null, Decimal? pMnPrimerSueldo = null, Decimal? pMnUltimoSueldo = null, String pClTipoContrato = null, String pClTipoContratoOtro = null, String pNoTelefonoContacto = null, String pClCorreoElectronico = null, String pNbContacto = null, String pNbPuestoContacto = null, bool? pClInformacionConfirmada = null, String pDsComentarios = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_K_EXPERIENCIA_LABORAL(pIdExperienciaLaboral, pIdCandidato, pIdEmpleado, pNbEmpresa, pDsDomicilio, pNbGiro, pFeInicio, pFeFin, pNbPuesto, pNbFuncion, pDsFunciones, pMnPrimerSueldo, pMnUltimoSueldo, pClTipoContrato, pClTipoContratoOtro, pNoTelefonoContacto, pClCorreoElectronico, pNbContacto, pNbPuestoContacto, pClInformacionConfirmada, pDsComentarios).ToList();
            }
        }

        public XElement ActualizarReferenciasExperienciaLaboral(string pXmlReferencias, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ACTUALIZA_REFERENCIAS_EXPERIENCIA_LABORAL(pOutClaveRetorno, pXmlReferencias, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public List<E_COMPETENCIAS_PROCESO_SELECCION> ObtenerCompetenciasProcesoSeleccion(int pIdCandidato, int? pIdPuesto)
        {
            using (context = new SistemaSigeinEntities())
            {
                var vLstOriginal = context.SPE_OBTIENE_COMPETENCIAS_PROCESO_SELECCION(pIdCandidato, pIdPuesto);

                List<E_COMPETENCIAS_PROCESO_SELECCION> vLstDestino = new List<E_COMPETENCIAS_PROCESO_SELECCION>();

                vLstDestino.AddRange(vLstOriginal.Select(t => new E_COMPETENCIAS_PROCESO_SELECCION
                {
                    ID_BATERIA = t.ID_BATERIA,
                    ID_CANDIDATO = t.ID_CANDIDATO,
                    ID_COMPETENCIA = t.ID_COMPETENCIA,
                    NO_VALOR_BAREMO = t.NO_BAREMO_FACTOR,
                    NO_VALOR_NIVEL = t.NO_VALOR_NIVEL,
                    PR_CUMPLIMIENTO_COMPETENCIA = t.PR_CUMPLIMIENTO_COMPETENCIA,
                    NB_COMPETENCIA = t.NB_COMPETENCIA,
                    DS_COMPETENCIA = t.DS_COMPETENCIA,
                    CL_COLOR = t.CL_COLOR,
                    NO_ORDEN = t.NO_ORDEN,
                    NB_CLASIFICACION_COMPETENCIA = t.NB_CLASIFICACION_COMPETENCIA,
                    DS_CLASIFICACION_COMPETENCIA = t.DS_CLASIFICACION_COMPETENCIA,
                    DS_NIVEL0 = t.DS_NIVEL0,
                    DS_NIVEL1 = t.DS_NIVEL1,
                    DS_NIVEL2 = t.DS_NIVEL2,
                    DS_NIVEL3 = t.DS_NIVEL3,
                    DS_NIVEL4 = t.DS_NIVEL4,
                    DS_NIVEL5 = t.DS_NIVEL5
                }));

                return vLstDestino;
            }
        }


        public XElement InsertarActualizarDocumentos(int pID_CANDIDATO, List<UDTT_ARCHIVO> pLstArchivosTemporales, List<E_DOCUMENTO> pLstDocumentos, string pClUsuario, string pNbPrograma)
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
                   "IDP.SPE_INSERTA_ACTUALIZA_DOCUMENTO_PROCESO " +
                   "@XML_RESULTADO OUTPUT, " +
                   "@PIN_ID_CANDIDATO, " +
                   "@PIN_XML_DOCUMENTOS, " +
                   "@PIN_ARCHIVOS, " +
                   "@PIN_CL_USUARIO, " +
                   "@PIN_NB_PROGRAMA"
                   , pXmlResultado
                   , new SqlParameter("@PIN_ID_CANDIDATO", (object)pID_CANDIDATO ?? DBNull.Value)
                   , new SqlParameter("@PIN_XML_DOCUMENTOS", SqlDbType.Xml) { Value = new SqlXml(vXmlDocumentos.CreateReader()) }
                   , pArchivos
                   , new SqlParameter("@PIN_CL_USUARIO", pClUsuario)
                   , new SqlParameter("@PIN_NB_PROGRAMA", pNbPrograma)
               );

                return XElement.Parse(pXmlResultado.Value.ToString());

            }
        }

        public SPE_OBTIENE_DOCUMENTO_PROCESO_Result ObtenerDocumentoProceso(int? pIdCandidato = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_DOCUMENTO_PROCESO(pIdCandidato).FirstOrDefault();
            }
        }


        public E_RESULTADO_MEDICO ObtenerResultadoMedico(int? pIdResultadoMedico = null, int? pIdCandidato = null, int? pIdEmpleado = null, int? pIdProcesoSeleccion = null)
        {

            using (context = new SistemaSigeinEntities())
            {
                var vResultadoMedico = context.SPE_OBTIENE_RESULTADO_MEDICO(pIdResultadoMedico, pIdCandidato, pIdEmpleado, pIdProcesoSeleccion).FirstOrDefault();

                E_RESULTADO_MEDICO vResMedico = new E_RESULTADO_MEDICO();

                if (vResultadoMedico != null)
                {
                    vResMedico.ID_RESULTADO_MEDICO = vResultadoMedico.ID_RESULTADO_MEDICO;
                    vResMedico.ID_CANDIDATO = vResultadoMedico.ID_CANDIDATO;
                    vResMedico.ID_EMPLEADO = vResultadoMedico.ID_EMPLEADO;
                    vResMedico.ID_PROCESO_SELECCION = vResultadoMedico.ID_PROCESO_SELECCION;
                    vResMedico.NO_EDAD = vResultadoMedico.NO_EDAD;
                    vResMedico.NO_TALLA = vResultadoMedico.NO_TALLA;
                    vResMedico.NO_PESO = vResultadoMedico.NO_PESO;
                    vResMedico.NO_INDICE_MASA_CORPORAL = vResultadoMedico.NO_INDICE_MASA_CORPORAL;
                    vResMedico.NO_PULSO = vResultadoMedico.NO_PULSO;
                    vResMedico.NO_PRESION_ARTERIAL = vResultadoMedico.NO_PRESION_ARTERIAL;
                    vResMedico.NO_EMBARAZOS = vResultadoMedico.NO_EMBARAZOS;
                    vResMedico.NO_HIJOS = vResultadoMedico.NO_HIJOS;
                    vResMedico.XML_ENFERMEDADES = vResultadoMedico.XML_ENFERMEDADES;
                    vResMedico.XML_MEDICAMENTOS = vResultadoMedico.XML_MEDICAMENTOS;
                    vResMedico.XML_ALERGIAS = vResultadoMedico.XML_ALERGIAS;
                    vResMedico.XML_ANTECEDENTES = vResultadoMedico.XML_ANTECEDENTES;
                    vResMedico.XML_INTERVENCIONES_QUIRURJICAS = vResultadoMedico.XML_INTERVENCIONES_QUIRURJICAS;
                    vResMedico.DS_OBSERVACIONES = vResultadoMedico.DS_OBSERVACIONES;
                    vResMedico.FG_ADECUADO = vResultadoMedico.FG_ADECUADO;
                }
                else
                {
                    vResMedico.ID_RESULTADO_MEDICO = 0;
                }

                return vResMedico;
            }
        }

        public XElement InsertarActualizaResultadoMedico(E_RESULTADO_MEDICO vResultado, string pNbPrograma, string pClUsuario, string pTipoTransaccion, int? pIdResultadoMedico = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_RESULTADO_MEDICO(pOutClaveRetorno, pIdResultadoMedico, vResultado.ID_CANDIDATO, vResultado.ID_EMPLEADO, vResultado.ID_PROCESO_SELECCION, vResultado.NO_EDAD, vResultado.NO_TALLA, vResultado.NO_PESO, vResultado.NO_INDICE_MASA_CORPORAL, vResultado.NO_PULSO, vResultado.NO_PRESION_ARTERIAL, vResultado.NO_EMBARAZOS, vResultado.NO_HIJOS, vResultado.XML_ENFERMEDADES, vResultado.XML_MEDICAMENTOS, vResultado.XML_ALERGIAS, vResultado.XML_ANTECEDENTES, vResultado.XML_INTERVENCIONES_QUIRURJICAS, vResultado.DS_OBSERVACIONES, vResultado.FG_ADECUADO, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_CONSULTA_APLICACION_PRUEBA_Result> ObtenerAplicacionPrueba(int? pID_CANDIDATO)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CONSULTA_APLICACION_PRUEBA(pID_CANDIDATO).ToList();
            }
        }

        public List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> ObtenerCatalogoValor(int? pIdCatalogoValor = null, String pClCatalogoValor = null, String pNbCatalogoValor = null, String pDsCatalogoValor = null, int? pIdCatalogoLista = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_CATALOGO_VALOR(pIdCatalogoValor, pClCatalogoValor, pNbCatalogoValor, pDsCatalogoValor, pIdCatalogoLista).ToList();
            }
        }

        public List<SPE_OBTIENE_BITACORA_SOLICITUD_Result> ObtenerBitacoraSolicitud(int? pID_CANDIDATO)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_BITACORA_SOLICITUD(pID_CANDIDATO).ToList();
            }
        }

        public XElement InsertarActualizaCopiaSocioEconomico(int? idCandidato, int idProceso, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.INSERTA_ACTUALIZA_COPIA_SOCIOECONOMICO(pOutClaveRetorno, idCandidato, idProceso, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        //DEPENDIETE
        public XElement EliminarDependiente(int pIdDependiente)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_EST_SOC_DEPENDIENTE(pOutClaveRetorno, pIdDependiente);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public XElement InsertarDependientes(int pIdEstudioSocioEconomico, string nbPariente, string clParentesco, string clGenero, DateTime fechaNacimiento, int idBitacora, string ocupacion, bool fgDependiente, bool fgActivo, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SP_INSERTA_EST_SOC_DEPENDIENTE(pOutClaveRetorno, pIdEstudioSocioEconomico, nbPariente, clParentesco, clGenero, fechaNacimiento, idBitacora, ocupacion, fgDependiente, fgActivo, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }
        public List<SPE_OBTIENE_EST_SOC_DEPENDIENTE_Result> ObtenerDependientes(int? pIdEstudioSocioEconomico = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EST_SOC_DEPENDIENTE(pIdEstudioSocioEconomico).ToList();
            }
        }

  
        public E_ESTUDIO_SOCIOECONOMICO ObtenerEstudioSocioeconomico(int? pIdEstudioSocioeconomico = null, int? pIdProcesoSeleccion = null, int? pIdEmpleado = null, int? pIdCandidato = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                E_ESTUDIO_SOCIOECONOMICO vEstudio = new E_ESTUDIO_SOCIOECONOMICO();

                var vRegistro = context.SPE_OBTIENE_ESTUDIO_SOCIOECONOMICO(pIdEstudioSocioeconomico, pIdProcesoSeleccion, pIdEmpleado, pIdCandidato).FirstOrDefault();

                if (vRegistro != null)
                {
                    vEstudio.ID_ESTUDIO_SOCIOECONOMICO = vRegistro.ID_ESTUDIO_SOCIOECONOMICO;
                    vEstudio.ID_PROCESO_SELECCION = vRegistro.ID_PROCESO_SELECCION;
                    vEstudio.FE_REALIZACION = vRegistro.FE_REALIZACION;
                    vEstudio.ID_EMPLEADO = vRegistro.ID_EMPLEADO;
                    vEstudio.ID_CANDIDATO = vRegistro.ID_CANDIDATO;
                    vEstudio.FE_NACIMIENTO = vRegistro.FE_NACIMIENTO;
                    vEstudio.NO_EDAD = vRegistro.NO_EDAD;
                    vEstudio.CL_ESTADO_CIVIL = vRegistro.CL_ESTADO_CIVIL;
                    vEstudio.CL_RFC = vRegistro.CL_RFC;
                    vEstudio.CL_CURP = vRegistro.CL_CURP;
                    vEstudio.CL_NSS = vRegistro.CL_NSS;
                    vEstudio.CL_PAIS = vRegistro.CL_PAIS;
                    vEstudio.NB_PAIS = vRegistro.NB_PAIS;
                    vEstudio.CL_ESTADO = vRegistro.CL_ESTADO;
                    vEstudio.NB_ESTADO = vRegistro.NB_ESTADO;
                    vEstudio.CL_MUNICIPIO = vRegistro.CL_MUNICIPIO;
                    vEstudio.NB_MUNICIPIO = vRegistro.NB_MUNICIPIO;
                    vEstudio.CL_COLONIA = vRegistro.CL_COLONIA;
                    vEstudio.NB_COLONIA = vRegistro.NB_COLONIA;
                    vEstudio.NB_CALLE = vRegistro.NB_CALLE;
                    vEstudio.NO_EXTERIOR = vRegistro.NO_EXTERIOR;
                    vEstudio.NO_INTERIOR = vRegistro.NO_INTERIOR;
                    vEstudio.CL_CODIGO_POSTAL = vRegistro.CL_CODIGO_POSTAL;
                    vEstudio.NO_TIEMPO_RESIDENCIA = vRegistro.NO_TIEMPO_RESIDENCIA;
                    vEstudio.CL_TIPO_SANGUINEO = vRegistro.CL_TIPO_SANGUINEO;
                    vEstudio.XML_TELEFONOS = vRegistro.XML_TELEFONOS;
                    vEstudio.CL_IDENTIFICACION_OFICIAL = vRegistro.CL_IDENTIFICACION_OFICIAL;
                    vEstudio.DS_IDENTIFICACION_OFICIAL = vRegistro.DS_IDENTIFICACION_OFICIAL;
                    vEstudio.XML_EGRESOS = vRegistro.XML_EGRESOS;
                    vEstudio.XML_INGRESOS = vRegistro.XML_INGRESOS;
                    vEstudio.CL_SERVICIOS_MEDICOS = vRegistro.CL_SERVICIO_MEDICO;
                    vEstudio.DS_SERVICIOS_MEDICOS = vRegistro.DS_SERVICIO_MEDICO;
                    
                }
                else
                {
                    vEstudio.ID_ESTUDIO_SOCIOECONOMICO = 0;
                }

                return vEstudio;
            }
        }

        public XElement InsertarActualizarEstudioSocioeconomico(E_ESTUDIO_SOCIOECONOMICO vEstudio, string pClUsuario = null, string pNbPrograma = null, string pTipoTransaccion = null, int? pIdEstudioSocioeconomico = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_ESTUDIO_SOCIOECONOMICO(pOutClaveRetorno, pIdEstudioSocioeconomico, vEstudio.ID_PROCESO_SELECCION, vEstudio.FE_REALIZACION, vEstudio.ID_EMPLEADO, vEstudio.ID_CANDIDATO, vEstudio.FE_NACIMIENTO, vEstudio.NO_EDAD, vEstudio.CL_ESTADO_CIVIL, vEstudio.CL_RFC, vEstudio.CL_CURP, vEstudio.CL_NSS, vEstudio.CL_PAIS, vEstudio.NB_PAIS, vEstudio.CL_ESTADO, vEstudio.NB_ESTADO, vEstudio.CL_MUNICIPIO, vEstudio.NB_MUNICIPIO, vEstudio.CL_COLONIA, vEstudio.NB_COLONIA, vEstudio.NB_CALLE, vEstudio.NO_EXTERIOR, vEstudio.NO_INTERIOR, vEstudio.CL_CODIGO_POSTAL, vEstudio.NO_TIEMPO_RESIDENCIA, vEstudio.CL_TIPO_SANGUINEO, vEstudio.XML_TELEFONOS, vEstudio.CL_IDENTIFICACION_OFICIAL, vEstudio.DS_IDENTIFICACION_OFICIAL, vEstudio.CL_SERVICIOS_MEDICOS, vEstudio.DS_SERVICIOS_MEDICOS, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public E_ES_DATOS_LABORALES ObtenerESDatosLaborales(int? pIdDatoLaboral = null, int? pIdEstudioSocioeconomico = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                E_ES_DATOS_LABORALES vDatosLaborales = new E_ES_DATOS_LABORALES();

                var vResgistro = context.SPE_OBTIENE_EST_SOC_DATO_LABORAL(pIdDatoLaboral, pIdEstudioSocioeconomico).FirstOrDefault();

                if (vResgistro != null)
                {
                    vDatosLaborales.ID_DATO_LABORAL = vResgistro.ID_DATO_LABORAL;
                    vDatosLaborales.ID_ESTUDIO_SOCIOECONOMICO = vResgistro.ID_ESTUDIO_SOCIOECONOMICO;
                    vDatosLaborales.NB_EMPRESA = vResgistro.NB_EMPRESA;
                    vDatosLaborales.CL_PAIS = vResgistro.CL_PAIS;
                    vDatosLaborales.NB_PAIS = vResgistro.NB_PAIS;
                    vDatosLaborales.CL_ESTADO = vResgistro.CL_ESTADO;
                    vDatosLaborales.NB_ESTADO = vResgistro.NB_ESTADO;
                    vDatosLaborales.CL_MUNICIPIO = vResgistro.CL_MUNICIPIO;
                    vDatosLaborales.NB_MUNICIPIO = vResgistro.NB_MUNICIPIO;
                    vDatosLaborales.CL_COLONIA = vResgistro.CL_COLONIA;
                    vDatosLaborales.NB_COLONIA = vResgistro.NB_COLONIA;
                    vDatosLaborales.CL_CODIGO_POSTAL = vResgistro.CL_CODIGO_POSTAL;
                    vDatosLaborales.NB_DOMICILIO = vResgistro.NB_DOMICILIO;
                    vDatosLaborales.NB_PUESTO = vResgistro.NB_PUESTO;
                    vDatosLaborales.MN_SALARIO_INICIAL = vResgistro.MN_SALARIO_INICIAL;
                    vDatosLaborales.MN_SALARIO_FINAL = vResgistro.MN_SALARIO_FINAL;
                    vDatosLaborales.CL_TIPO_EMPRESA = vResgistro.CL_TIPO_EMPRESA;
                    vDatosLaborales.DS_TIPO_EMPRESA = vResgistro.DS_TIPO_EMPRESA;
                    vDatosLaborales.CL_TIPO_CONTRATO = vResgistro.CL_TIPO_CONTRATO;
                    vDatosLaborales.NO_ANTIGUEDAD_EMPRESA = vResgistro.NO_ANTIGUEDAD_EMPRESA;
                    vDatosLaborales.CL_TIPO_SUELDO = vResgistro.CL_TIPO_SUELDO;
                    vDatosLaborales.NB_TIPO_SUELDO = vResgistro.NB_TIPO_SUELDO;

                }
                else
                {
                    vDatosLaborales.ID_DATO_LABORAL = 0;
                }

                return vDatosLaborales;
            }
        }

        public XElement InsertarActualizarESDatosLaborales(E_ES_DATOS_LABORALES vDatosLaborales, string pClUsuario, string pNbPrograma, string pTipoTransaccion, int? pIdDatoLaboral = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EST_SOC_DATO_LABORAL(pOutClaveRetorno, pIdDatoLaboral, vDatosLaborales.ID_ESTUDIO_SOCIOECONOMICO, vDatosLaborales.NB_EMPRESA, vDatosLaborales.CL_PAIS, vDatosLaborales.NB_PAIS, vDatosLaborales.CL_ESTADO, vDatosLaborales.NB_ESTADO, vDatosLaborales.CL_MUNICIPIO, vDatosLaborales.NB_MUNICIPIO, vDatosLaborales.CL_COLONIA, vDatosLaborales.NB_COLONIA, vDatosLaborales.CL_CODIGO_POSTAL, vDatosLaborales.NB_DOMICILIO, vDatosLaborales.NB_PUESTO, vDatosLaborales.MN_SALARIO_INICIAL, vDatosLaborales.MN_SALARIO_FINAL, vDatosLaborales.CL_TIPO_EMPRESA, vDatosLaborales.DS_TIPO_EMPRESA, vDatosLaborales.CL_TIPO_CONTRATO, vDatosLaborales.NO_ANTIGUEDAD_EMPRESA, vDatosLaborales.CL_TIPO_SUELDO, vDatosLaborales.NB_TIPO_SUELDO, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public E_ES_DATOS_ECONOMICOS ObtenerESDatosEconomicos(int? pIdDatoPropiedad = null, int? pIdEstudioSocioeconomico = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                E_ES_DATOS_ECONOMICOS vDatoEconomico = new E_ES_DATOS_ECONOMICOS();
                var vRegistro = context.SPE_OBTIENE_EST_SOC_DATO_PROPIEDAD(pIdDatoPropiedad, pIdEstudioSocioeconomico).FirstOrDefault();

                if (vRegistro != null)
                {
                    vDatoEconomico.ID_DATO_PROPIEDAD = vRegistro.ID_DATO_PROPIEDAD;
                    vDatoEconomico.ID_ESTUDIO_SOCIOECONOMICO = vRegistro.ID_ESTUDIO_SOCIOECONOMICO;
                    vDatoEconomico.CL_TIPO_PROPIEDAD = vRegistro.CL_TIPO_PROPIEDAD;
                    vDatoEconomico.CL_TIPO_ZONA = vRegistro.CL_TIPO_ZONA;
                    vDatoEconomico.CL_FORMA_ADQUISICION = vRegistro.CL_FORMA_ADQUISICION;
                    vDatoEconomico.DS_FORMA_ADQUISICION = vRegistro.DS_FORMA_ADQUISICION;
                }
                else
                {
                    vDatoEconomico.ID_DATO_PROPIEDAD = 0;
                }

                return vDatoEconomico;
            }
        }

        public XElement InsertarActualizarESDatosEconomicos(E_ES_DATOS_ECONOMICOS pDatoEconomico, string pClUsuario, string pNbPrograma, string pTipoTransaccion, int? pIdDatoPropiedad = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EST_SOC_DATO_PROPIEDAD(pOutClaveRetorno, pIdDatoPropiedad, pDatoEconomico.ID_ESTUDIO_SOCIOECONOMICO, pDatoEconomico.CL_TIPO_PROPIEDAD, pDatoEconomico.CL_TIPO_ZONA, pDatoEconomico.CL_FORMA_ADQUISICION, pDatoEconomico.DS_FORMA_ADQUISICION, pDatoEconomico.XML_EGRESOS, pDatoEconomico.XML_INGRESOS, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public E_ES_DATOS_VIVIENDA ObtenerESDatosVivienda(int? pIdDatoVivienda = null, int? pIdEstudioSocioeconomico = null)
        {
            using (context = new SistemaSigeinEntities())
            {

                E_ES_DATOS_VIVIENDA vDatoVivienda = new E_ES_DATOS_VIVIENDA();
                var vRegistro = context.SPE_OBTIENE_EST_SOC_DATO_VIVIENDA(pIdDatoVivienda, pIdEstudioSocioeconomico).FirstOrDefault();

                if (vRegistro != null)
                {
                    vDatoVivienda.ID_DATO_VIVIENDA = vRegistro.ID_DATO_VIVIENDA;
                    vDatoVivienda.CL_TIPO_CONSTRUCCION = vRegistro.CL_TIPO_CONSTRUCCION;
                    vDatoVivienda.ID_ESTUDIO_SOCIOECONOMICO = vRegistro.ID_ESTUDIO_SOCIOECONOMICO;
                    vDatoVivienda.CL_TIPO_VIVIENDA = vRegistro.CL_TIPO_VIVIENDA;
                    vDatoVivienda.CL_TIPO_POSESION = vRegistro.CL_TIPO_POSESION;
                    vDatoVivienda.CL_TIPO_CONSTRUCCION = vRegistro.CL_TIPO_CONSTRUCCION;
                    vDatoVivienda.DS_TIPO_CONSTRUCCION = vRegistro.DS_TIPO_CONSTRUCCION;
                    vDatoVivienda.NO_HABITACIONES = vRegistro.NO_HABITACIONES;
                    vDatoVivienda.NO_BANIOS = vRegistro.NO_BANIOS;
                    vDatoVivienda.NO_PATIOS = vRegistro.NO_PATIOS;
                    vDatoVivienda.NO_HABITANTES = vRegistro.NO_HABITANTES;
                    vDatoVivienda.XML_SERVICIOS = vRegistro.XML_SERVICIOS;
                    vDatoVivienda.XML_BIENES_MUEBLES = vRegistro.XML_BIENES_MUEBLES;
                }
                else
                {
                    vDatoVivienda.ID_DATO_VIVIENDA = 0;
                }

                return vDatoVivienda;
            }
        }

        public XElement InsertarActualizarESDatosVivienda(E_ES_DATOS_VIVIENDA pDatosVivienda, string pClUsuario, string pNbPrograma, string pTipoTransaccion, int? pIdDatoVivienda = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_EST_SOC_DATO_VIVIENDA(pOutClaveRetorno, pIdDatoVivienda, pDatosVivienda.ID_ESTUDIO_SOCIOECONOMICO, pDatosVivienda.CL_TIPO_VIVIENDA, pDatosVivienda.CL_TIPO_POSESION, pDatosVivienda.CL_TIPO_CONSTRUCCION, pDatosVivienda.DS_TIPO_CONSTRUCCION, pDatosVivienda.NO_HABITACIONES, pDatosVivienda.NO_BANIOS, pDatosVivienda.NO_PATIOS, pDatosVivienda.NO_HABITACIONES, pDatosVivienda.XML_SERVICIOS, pDatosVivienda.XML_BIENES_MUEBLES, pClUsuario, pNbPrograma, pTipoTransaccion);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public List<SPE_OBTIENE_COMENTARIOS_ENTREVISTAS_Result> ObtenerCometariosEntrevistasProcesoSeleccion(int? pIdCandidato = null, int? pIdProceso = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_COMENTARIOS_ENTREVISTAS(pIdCandidato, pIdProceso).ToList();
            }
        }

        public XElement TerminaProcesoSeleccion(int pIdProcesoSeleccion, string pDsObservacionesTermino, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_TERMINA_PROCESO_SELECCION(pOutClaveRetorno, pIdProcesoSeleccion, pDsObservacionesTermino, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }

        public XElement CopiarDatosProcesoSeleccion(int pIdProcesoSeleccionOrigen, int pIdProcesoSeleccionDestino, string pXmlConfiguracion, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_COPIA_PROCESO_SELECCION(pOutClaveRetorno, pIdProcesoSeleccionOrigen, pIdProcesoSeleccionDestino, pXmlConfiguracion, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClaveRetorno.Value.ToString());
            }
        }
    }
}
