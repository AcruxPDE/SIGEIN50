using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal; 
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System.Xml.Linq;
using SIGE.Entidades.IntegracionDePersonal;


namespace SIGE.Negocio.Administracion
{
    public class RequisicionNegocio
    {

        public List<SPE_OBTIENE_K_REQUISICION_Result> ObtieneRequisicion(int? pIdRequisicion = null, String pNoRequisicion = null, DateTime? pFeSolicitud = null, DateTime? pFeRequisicion = null, int? pIdPuesto = null, String pClEstado = null, String pClCausa = null, String pDsCausa = null, int? pIdNotificacion = null, int? pIdSolicitante = null, int? pIdAutoriza = null, int? pIdVistoBueno = null, int? pIdEmpresa = null, Guid? flRequisicion=null, Guid? flNotificacion = null, int? pIdCandidato = null)
		{
			RequisicionOperaciones operaciones = new RequisicionOperaciones();
            return operaciones.ObtenerRequisicion(pIdRequisicion, pNoRequisicion, pFeSolicitud, pFeRequisicion, pIdPuesto, pClEstado, pClCausa, pDsCausa, pIdNotificacion, pIdSolicitante, pIdAutoriza, pIdVistoBueno, pIdEmpresa,flRequisicion, flNotificacion, pIdCandidato);
		}     

        public List<SPE_OBTIENE_K_AUTORIZA_REQUISICION_Result> ObtenerAutorizarRequisicion(int? ID_REQUISICION = null, string NO_REQUISICION = null, DateTime? FE_SOLICITUD = null, DateTime? FE_REQUERIMIENTO = null, int? ID_PUESTO = null, string CL_ESTADO = null, string CL_CAUSA = null, string DS_CAUSA = null, int? ID_NOTIFICACION = null, int? ID_SOLICITANTE = null, int? ID_AUTORIZA = null, int? ID_VISTO_BUENO = null, int? ID_EMPRESA = null, string NB_EMPRESA = null, string NB_PUESTO = null, string SOLICITANTE = null, Guid? FL_REQUISICION = null, string CL_TOKEN = null)
        {
            RequisicionOperaciones operaciones = new RequisicionOperaciones();
            return operaciones.ObtenerAutorizarRequisicion(ID_REQUISICION, NO_REQUISICION, FE_SOLICITUD, FE_REQUERIMIENTO, ID_PUESTO, CL_ESTADO, CL_CAUSA, DS_CAUSA, ID_NOTIFICACION, ID_SOLICITANTE, ID_AUTORIZA, ID_VISTO_BUENO, ID_EMPRESA, NB_EMPRESA, NB_PUESTO, SOLICITANTE, FL_REQUISICION, CL_TOKEN);
        }

        public E_RESULTADO InsertaActualizaRequisicion(string pTipoTransaccion, E_REQUISICION pRequisicion, string pClUsuario, string pNbPrograma)
		{
			RequisicionOperaciones oRequisicion = new RequisicionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oRequisicion.InsertarActualizarRequisicion(pTipoTransaccion, pRequisicion, pClUsuario, pNbPrograma));
		}

        public E_RESULTADO ActualizaEstatusRequisicion(bool pFgActualizaRequisicion, bool pFgActualizaPuesto, int pIdRequisicion, int pIdPuesto, string pDsComentariosPuesto, string pDsComentariosRequisicion, string pEstatusPuesto, string pEstatusRequisicion, string pNbPrograma, string pClUsuario)
        {
            RequisicionOperaciones oRequisicion = new RequisicionOperaciones();
            return UtilRespuesta.EnvioRespuesta(oRequisicion.ActualizaEstatusRequisicion(pFgActualizaRequisicion, pFgActualizaPuesto, pIdRequisicion, pIdPuesto, pDsComentariosPuesto, pDsComentariosRequisicion, pEstatusPuesto, pEstatusRequisicion,pNbPrograma, pClUsuario));
        }

        public E_RESULTADO Elimina_K_REQUISICION(int? ID_REQUISICION = null, string usuario = null, string programa = null)
		{
			RequisicionOperaciones operaciones = new RequisicionOperaciones();
			return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_K_REQUISICION(ID_REQUISICION, usuario, programa));
		}

        public E_RESULTADO ActualizaEstatusRequisicion(int? ID_REQUISICION = null, string usuario = null, string programa = null)
		{
			RequisicionOperaciones operaciones = new RequisicionOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizaEstatusRequisicion(ID_REQUISICION, usuario, programa));
		}

        

        public List<SPE_OBTIENE_CANDIDATOS_POR_REQUISICION_Result> ObtenerCandidatosPorRequisicion(int? pIdRequisicion = null, int? pIdProcesoSeleccion = null, int? pIdCandidato = null, int? pIdSolicitud = null)
        {
            RequisicionOperaciones oReq = new RequisicionOperaciones();
            return oReq.ObtenerCandidatosPorRequisicion(pIdRequisicion, pIdProcesoSeleccion, pIdCandidato, pIdSolicitud);
        }

        //public C_NOTIFICAR_REQUISICION ObtienetablasNotificar()
        //{
        //    RequisicionOperaciones oRequisicion = new RequisicionOperaciones();
        //    SPE_OBTIENE_C_NOTIFICAR_REQUISICION_Result tablas = oRequisicion.Obtener_Tablas_Notificar().FirstOrDefault();

        //    List<E_DEPARTAMENTO> vDepartamentos = XElement.Parse(tablas.XML_DEPARTAMENTOS).Elements("DEPARTAMENTO").Select(el => new E_DEPARTAMENTO
        //    {
        //        ID_DEPARTAMENTO = (int?)UtilXML.ValorAtributo(el.Attribute("ID_DEPARTAMENTO"), E_TIPO_DATO.INT),
        //        NB_DEPARTAMENTO = el.Attribute("NB_DEPARTAMENTO").Value,
        //        CL_DEPARTAMENTO = el.Attribute("CL_DEPARTAMENTO").Value,
        //         FG_ACTIVO = (bool)UtilXML.ValorAtributo(el.Attribute("FG_ACTIVO"), E_TIPO_DATO.BOOLEAN)
                
        //    }).ToList();

        //    List<E_ESCOLARIDAD> vEscolaridades = XElement.Parse(tablas.XML_C_ESCOLARIDAD).Elements("ESCOLARIDAD").Select(el => new E_ESCOLARIDAD
        //    {
        //        ID_ESCOLARIDAD = (int)UtilXML.ValorAtributo(el.Attribute("ID_ESCOLARIDAD"), E_TIPO_DATO.INT),
        //        NB_ESCOLARIDAD = el.Attribute("NB_ESCOLARIDAD").Value,
        //        DS_ESCOLARIDAD = el.Attribute("DS_ESCOLARIDAD").Value,
        //        CL_NIVEL_ESCOLARIDAD = el.Attribute("CL_NIVEL_ESCOLARIDAD").Value
        //    }).ToList();

        //    List<E_NIVEL_ESCOLARIDAD> vNivelEscolaridades = XElement.Parse(tablas.XML_C_NIVEL_ESCOLARIDADES).Elements("NIVEL_ESCOLARIDAD").Select(el => new E_NIVEL_ESCOLARIDAD
        //    {
        //        ID_NIVEL_ESCOLARIDAD = (int?)UtilXML.ValorAtributo(el.Attribute("ID_NIVEL_ESCOLARIDAD"), E_TIPO_DATO.INT),
        //        CL_NIVEL_ESCOLARIDAD = el.Attribute("CL_NIVEL_ESCOLARIDAD").Value,
        //        DS_NIVEL_ESCOLARIDAD = el.Attribute("DS_NIVEL_ESCOLARIDAD").Value,
        //        CL_TIPO_ESCOLARIDAD = el.Attribute("CL_TIPO_ESCOLARIDAD").Value
        //     }).ToList();


        
        //    List<E_CATALOGO_LISTA> vCatalogoLista = XElement.Parse(tablas.XML_CATALOGO_LISTA).Elements("CATALOGO_LISTA").Select(el => new E_CATALOGO_LISTA
        //    {
        //        ID_CATALOGO_LISTA = (int)UtilXML.ValorAtributo(el.Attribute("ID_CATALOGO_LISTA"), E_TIPO_DATO.INT),
        //        NB_CATALOGO_LISTA = el.Attribute("NB_CATALOGO_LISTA").Value,
        //        DS_CATALOGO_LISTA = el.Attribute("DS_CATALOGO_LISTA").Value,
        //        ID_CATALOGO_TIPO = (int)UtilXML.ValorAtributo(el.Attribute("ID_CATALOGO_TIPO"), E_TIPO_DATO.INT)
        //    }).ToList();

          
        //    List<E_CATALOGO_CATALOGOS> vCatalogoValor = XElement.Parse(tablas.XML_CATALOGO_VALOR).Elements("CATALOGO_VALOR").Select(el => new E_CATALOGO_CATALOGOS
        //    {
        //        ID_CATALOGO_VALOR = (int)UtilXML.ValorAtributo(el.Attribute("ID_CATALOGO_VALOR"), E_TIPO_DATO.INT),
        //        CL_CATALOGO_VALOR = el.Attribute("CL_CATALOGO_VALOR").Value,
        //        NB_CATALOGO_VALOR = el.Attribute("NB_CATALOGO_VALOR").Value
        //    }).ToList();

        //    vCatalogoValor.Add(new E_CATALOGO_CATALOGOS { ID_CATALOGO_VALOR = 0, CL_CATALOGO_VALOR = "", NB_CATALOGO_VALOR = "Indistinto" });

        //    return new C_NOTIFICAR_REQUISICION
        //    {
        //        DEPARTAMENTOS = vDepartamentos,
        //        ESCOLARIDADES = vEscolaridades,
        //        C_NIVEL_ESCOLARIDADES = vNivelEscolaridades,
        //        CATALOGO_LISTA = vCatalogoLista,
        //        CATALOGO_VALOR = vCatalogoValor
        //    };
        //}

        //public SPE_OBTIENE_SUELDO_PROMEDIO_PUESTO_Result Obtener_Sueldo_Promedio_Puesto( int? ID_PUESTO = null)
        //{
        //    RequisicionOperaciones operaciones = new RequisicionOperaciones();
        //    return operaciones.Obtiene_Sueldo_Promedio_Puesto(ID_PUESTO).FirstOrDefault();
        //}

        public string ObtieneNumeroRequisicion()
        {
            RequisicionOperaciones operaciones = new RequisicionOperaciones();
            return operaciones.ObtenerNumeroRequisicion();
        }

        public List<E_CANDIDATO_IDONEO> BuscaCandidatoRequisicion(int pIdRequisicion, string pXmlParametrosBusqueda)
        {
            RequisicionOperaciones oRequisicion = new RequisicionOperaciones();
            return oRequisicion.BuscarCandidatoRequisicion(pIdRequisicion, pXmlParametrosBusqueda);
        }

        public List<SPE_OBTIENE_COMPARACION_CANDIDATO_PUESTO_REQUISICION_Result> ObtenerComparacionCandidatoPuesto(int pIdRequisicion, int pIdCandidato, string pXmlParametrosBusqueda)
        {
            RequisicionOperaciones oRequisicion = new RequisicionOperaciones();
            return oRequisicion.ObtenerComparacionCandidatoPuesto(pIdRequisicion, pIdCandidato, pXmlParametrosBusqueda);
        }

    }

    //[Serializable]
    //public class C_NOTIFICAR_REQUISICION 
    //{
    //    public List<E_DEPARTAMENTO> DEPARTAMENTOS { get; set; }
    //    public List<E_ESCOLARIDAD> ESCOLARIDADES { get; set; }
    //    public List<E_NIVEL_ESCOLARIDAD> C_NIVEL_ESCOLARIDADES { get; set; }
    //    public List<E_CATALOGO_LISTA> CATALOGO_LISTA { get; set; }
    //    public List<E_CATALOGO_CATALOGOS> CATALOGO_VALOR { get; set; }
    //}
}