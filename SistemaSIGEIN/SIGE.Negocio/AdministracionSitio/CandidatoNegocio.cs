using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using SIGE.Negocio.AdministracionSitio;


namespace SIGE.Negocio.Administracion 
{
    public class CandidatoNegocio
    {

        public List<SPE_OBTIENE_C_CANDIDATO_Result> ObtieneCandidato(int? pIdCandidato = null, String pNbCandidato = null, String pNbApellidoPaterno = null, String pNbApellidoMaterno = null, String pClGenero = null, String pClRfc = null, String pClCurp = null, String pClEstadoCivil = null, String pNbConyuge = null, String pClNss = null, String pClTipoSanguineo = null, String pNbPais = null, String NB_ESTADO = null, String NB_MUNICIPIO = null, String pNbColonia = null, String pNbCalle = null, String pNoInterior = null, String pNoExterior = null, String pClCodigoPostal = null, String pClCorreoElectronico = null, DateTime? pFeNacimiento = null, String pDsLugarNacimiento = null, Decimal? pMnSueldo = null, String pClNacionalidad = null, String pDsNacionalidad = null, String pNbLicencia = null, String pDsVehiculo = null, String pClCartillaMilitar = null, String pClCedulaProfesional = null, String pXmlTelefonos = null, String pXmlIngresos = null, String pXmlEgresos = null, String pXmlPatrimonio = null, String pDsDisponibilidad = null, String pClDisponibilidadViaje = null, String pXmlPerfilRedSocial = null, String pDsComentario = null, bool? pFgActivo = null, string pXmlSeleccion = null)
        {
            CandidatoOperaciones oCandidato = new CandidatoOperaciones();
            return oCandidato.ObtenerCandidato(pIdCandidato, pNbCandidato, pNbApellidoPaterno, pNbApellidoMaterno, pClGenero, pClRfc, pClCurp, pClEstadoCivil, pNbConyuge, pClNss, pClTipoSanguineo, pNbPais, NB_ESTADO, NB_MUNICIPIO, pNbColonia, pNbCalle, pNoInterior, pNoExterior, pClCodigoPostal, pClCorreoElectronico, pFeNacimiento, pDsLugarNacimiento, pMnSueldo, pClNacionalidad, pDsNacionalidad, pNbLicencia, pDsVehiculo, pClCartillaMilitar, pClCedulaProfesional, pXmlTelefonos, pXmlIngresos, pXmlEgresos, pXmlPatrimonio, pDsDisponibilidad, pClDisponibilidadViaje, pXmlPerfilRedSocial, pDsComentario, pFgActivo, pXmlSeleccion);
        }

        public List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result> ObtieneCandidatosBateria(XElement lstCandidatos)
        {
            CandidatoOperaciones oCandidato = new CandidatoOperaciones();
            return oCandidato.ObtenerCandidatosBateria(lstCandidatos);
        }

        //public E_RESULTADO InsertaActualizaCandidato(string pTipoTransaccion, E_CANDIDATO pCandidato, string pClUsuario, string pNbPrograma)
        //{
        //    CandidatoOperaciones oCandidato = new CandidatoOperaciones();
        //    return UtilRespuesta.EnvioRespuesta(oCandidato.InsertarActualizarCandidato(pTipoTransaccion, pCandidato, pClUsuario, pNbPrograma));
        //}
        
        //public E_RESULTADO EliminaCandidato(int? pIdCandidato = null, string pClUsuario = null, string pNbPrograma = null)
        //{
        //    CandidatoOperaciones oCandidato = new CandidatoOperaciones();
        //    return UtilRespuesta.EnvioRespuesta(oCandidato.EliminarCandidato(pIdCandidato, pClUsuario, pNbPrograma));
        //}

        //public List<SPE_OBTIENE_CANDIDATO_IDONEO_Result> ObtieneCandidatoIdoneo(int pIdRequisicion, string pXmlCandidatos, decimal pPrCompatibilidad, int pNoCandidatosMaximos, bool pFgfotografia = false)
        //{
        //    CandidatoOperaciones oCandidato = new CandidatoOperaciones();
        //    return oCandidato.ObtenerCandidatoIdoneo(pIdRequisicion, pXmlCandidatos, pPrCompatibilidad, pNoCandidatosMaximos, pFgfotografia);
        //}

        //public List<SPE_OBTIENE_CANDIDATO_IDONEO_PERFIL_Result> ObtieneCandidatoIdoneoPerfil(int pIdRequisicion, int pNoCandidatosMaximos,string xmlCandidatos ,bool pFgfotografia = false)
        //{
        //    CandidatoOperaciones oCandidato = new CandidatoOperaciones();
        //    return oCandidato.ObtenerCandidatoIdoneoPerfil(pIdRequisicion, pNoCandidatosMaximos, xmlCandidatos ,pFgfotografia);
        //}

        public List<SPE_OBTIENE_ANALISIS_COMPETENCIAS_CANDIDATO_IDONEO_Result> ObtieneAnalisisCompetencias(int pIdRequisicion, int pIdCandidato, int pIdBateria)
        {
            CandidatoOperaciones oCandidato = new CandidatoOperaciones();
            return oCandidato.ObtenerAnalisisCompetencias(pIdRequisicion, pIdCandidato, pIdBateria);
        }

        public SPE_OBTIENE_PUESTO_REQUISICION_Result ObtienePuestoRequisicion(int pIdRequisicion)
        {
            CandidatoOperaciones oCandidato = new CandidatoOperaciones();
            return oCandidato.ObtenerPuestoRequisiscion(pIdRequisicion);
        }











        //public C_REQUISICION_CANDIDATO ObtieneCandidatoIdoneo(int? ID_CANDIDATO = null)
        //{
        //    CandidatoOperaciones oCandidato = new CandidatoOperaciones();
        //    SPE_OBTIENE_K_REQUISICION_CANDIDATO_Result VobjetoCandidato = oCandidato.ObtenerCandidatosIdoneos(ID_CANDIDATO).FirstOrDefault();

        //    List<E_CANDIDATO_EMPLEADOS> vCandidatos = new List<E_CANDIDATO_EMPLEADOS>();

        //    if (VobjetoCandidato.XML_CANDIDATOS != null)
        //    {
        //        vCandidatos = XElement.Parse(VobjetoCandidato.XML_CANDIDATOS).Elements("CANDIDATO").Select(el => new E_CANDIDATO_EMPLEADOS
        //        {
        //            ID_CANDIDATO = (int)UtilXML.ValorAtributo(el.Attribute("ID_CANDIDATO"), E_TIPO_DATO.INT),
        //            NB_CANDIDATO = el.Attribute("NB_CANDIDATO").Value,
        //            NO_EDAD = (int)UtilXML.ValorAtributo(el.Attribute("NO_EDAD"), E_TIPO_DATO.INT),
        //            FG_ACTIVO = (bool)UtilXML.ValorAtributo(el.Attribute("FG_ACTIVO"), E_TIPO_DATO.BOOLEAN),

        //            CL_CORREO_ELECTRONICO = el.Attribute("CL_CORREO_ELECTRONICO").Value,
        //            CL_EMPLEADO = el.Attribute("CL_EMPLEADO").Value,
        //            ID_EMPLEADO = (int)UtilXML.ValorAtributo(el.Attribute("ID_EMPLEADO"), E_TIPO_DATO.INT),
        //            NB_EMPLEADO = el.Attribute("NB_EMPLEADO").Value
        //        }).ToList();
        //    }

        //    List<E_EMPLEADO_COMPETENCIA> vEmpleadosCompetencia = new List<E_EMPLEADO_COMPETENCIA>();

        //    if (VobjetoCandidato.XML_EMPLEADOS_COMPETENCIAS != null)
        //    {
        //        vEmpleadosCompetencia = XElement.Parse(VobjetoCandidato.XML_EMPLEADOS_COMPETENCIAS).Elements("EMPLEADO_COMPETENCIA").Select(el => new E_EMPLEADO_COMPETENCIA
        //    {
        //        ID_EMPLEADO_COMPETENCIA = (int)UtilXML.ValorAtributo(el.Attribute("ID_EMPLEADO_COMPETENCIA"), E_TIPO_DATO.INT),
        //        ID_EMPLEADO = (int)UtilXML.ValorAtributo(el.Attribute("ID_EMPLEADO"), E_TIPO_DATO.INT),
        //        ID_COMPETENCIA = (int)UtilXML.ValorAtributo(el.Attribute("ID_COMPETENCIA"), E_TIPO_DATO.INT),
        //        NO_CALIFICACION = decimal.Parse(el.Attribute("NO_CALIFICACION").Value),

        //        CL_COMPETENCIA = el.Attribute("CL_COMPETENCIA").Value,
        //        NB_COMPETENCIA = el.Attribute("NB_COMPETENCIA").Value,
        //        DS_COMPETENCIA = el.Attribute("DS_COMPETENCIA").Value,
        //        CL_TIPO_COMPETENCIA = el.Attribute("CL_TIPO_COMPETENCIA").Value,
        //        CL_CLASIFICACION = el.Attribute("CL_CLASIFICACION").Value
        //    }).ToList();
        //    }

        //    List<E_CANDIDATO_AREAS_INTERES> vCandidatosAreasInteres = new List<E_CANDIDATO_AREAS_INTERES>();

        //    if (VobjetoCandidato.XML_K_AREA_INTERES != null)
        //    {
        //        vCandidatosAreasInteres = XElement.Parse(VobjetoCandidato.XML_K_AREA_INTERES).Elements("AREA_INTERES").Select(el => new E_CANDIDATO_AREAS_INTERES
        //        {
        //            ID_CANDIDATO_AREA_INTERES = (int)UtilXML.ValorAtributo(el.Attribute("ID_CANDIDATO_AREA_INTERES"), E_TIPO_DATO.INT),
        //            ID_CANDIDATO = (int)UtilXML.ValorAtributo(el.Attribute("ID_CANDIDATO"), E_TIPO_DATO.INT),
        //            ID_AREA_INTERES = (int)UtilXML.ValorAtributo(el.Attribute("ID_AREA_INTERES"), E_TIPO_DATO.INT),

        //            CL_AREA_INTERES = el.Attribute("CL_AREA_INTERES").Value,
        //            NB_AREA_INTERES = el.Attribute("NB_AREA_INTERES").Value
        //        }).ToList();
        //    }

        //    List<E_EMPLEADO_ESCOLARIDAD> vEmpleadosEscolaridad = new List<E_EMPLEADO_ESCOLARIDAD>();

        //    if (VobjetoCandidato.XML_EMPLEADOS_ESCOLARIDADES != null)
        //    {
        //        vEmpleadosEscolaridad  = XElement.Parse(VobjetoCandidato.XML_EMPLEADOS_ESCOLARIDADES).Elements("EMPLEADO_ESCOLARIDAD").Select(el => new E_EMPLEADO_ESCOLARIDAD
        //        {
        //            ID_EMPLEADO_ESCOLARIDAD = (int?)UtilXML.ValorAtributo(el.Attribute("ID_EMPLEADO_ESCOLARIDAD"), E_TIPO_DATO.INT),
        //            ID_EMPLEADO = (int?)UtilXML.ValorAtributo(el.Attribute("ID_EMPLEADO"), E_TIPO_DATO.INT),
        //            ID_CANDIDATO = (int?)UtilXML.ValorAtributo(el.Attribute("ID_CANDIDATO"), E_TIPO_DATO.INT),
        //            //  CL_INSTITUCION = (int)UtilXML.ValorAtributo(el.Attribute("CL_INSTITUCION"),E_TIPO_DATO.INT),
        //            NB_INSTITUCION = el.Attribute("NB_INSTITUCION").Value,
        //            FE_PERIODO_INICIO = el.Attribute("FE_PERIODO_INICIO").Value,
        //            FE_PERIODO_FIN = el.Attribute("FE_PERIODO_FIN").Value,
        //            CL_ESTADO_ESCOLARIDAD = el.Attribute("CL_ESTADO_ESCOLARIDAD").Value,

        //            NB_ESCOLARIDAD = el.Attribute("NB_ESCOLARIDAD").Value,
        //            DS_ESCOLARIDAD = el.Attribute("DS_ESCOLARIDAD").Value,
        //            ID_NIVEL_ESCOLARIDAD = (int?)UtilXML.ValorAtributo(el.Attribute("ID_NIVEL_ESCOLARIDAD"), E_TIPO_DATO.INT)

        //        }).ToList();
        //    }

           

        //    return new C_REQUISICION_CANDIDATO
        //    {
        //        ID_CANDIDATO = VobjetoCandidato.ID_CANDIDATO,
        //        XML_CANDIDATOS = vCandidatos,
        //        XML_K_AREA_INTERES = vCandidatosAreasInteres,
        //        XML_EMPLEADOS_COMPETENCIAS = vEmpleadosCompetencia,
        //        XML_EMPLEADOS_ESCOLARIDADES = vEmpleadosEscolaridad

        //    };
        //}

        public List<SPE_OBTIENE_CANDIDATOS_CONTRATADOS_Result> ObtieneCandidatosContratados(int? pIdEmpresa = null, int? pIdRol = null)
        {
            CandidatoOperaciones oCandidato = new CandidatoOperaciones();
            return oCandidato.ObtenerCandidatosContratados(pIdEmpresa, pIdRol);
        }
    }
}