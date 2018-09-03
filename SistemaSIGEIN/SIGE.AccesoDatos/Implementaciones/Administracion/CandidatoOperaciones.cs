using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Administracion;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
    public class CandidatoOperaciones
    {
		private SistemaSigeinEntities context;

		public List<SPE_OBTIENE_C_CANDIDATO_Result> ObtenerCandidato(int? pIdCandidato = null, String pNbCandidato = null, String pNbApellidoPaterno = null, String pNbApellidoMaterno = null, String pClGenero = null, String pClRfc = null, String pClCurp = null, String pClEstadoCivil = null, String pNbConyuge = null, String pClNss = null, String pClTipoSanguineo = null, String pNbPais = null, String NB_ESTADO = null,String NB_MUNICIPIO = null, String pNbColonia = null, String pNbCalle = null, String pNoInterior = null, String pNoExterior = null, String pClCodigoPostal = null, String pClCorreoElectronico = null, DateTime? pFeNacimiento = null, String pDsLugarNacimiento = null, Decimal? pMnSueldo = null, String pClNacionalidad = null, String pDsNacionalidad = null, String pNbLicencia = null, String pDsVehiculo = null,String pClCartillaMilitar = null, String pClCedulaProfesional = null, String pXmlTelefonos = null, String pXmlIngresos = null, String pXmlEgresos = null, String pXmlPatrimonio = null, String pDsDisponibilidad = null, String pClDisponibilidadViaje = null, String pXmlPerfilRedSocial = null, String pDsComentario = null, bool?  pFgActivo = null, string pXmlSeleccion = null, bool pFgFoto = false)
		{
			using (context = new SistemaSigeinEntities ())
			{
                return context.SPE_OBTIENE_C_CANDIDATO(pIdCandidato, pNbCandidato, pNbApellidoPaterno, pNbApellidoMaterno, pClGenero, pClRfc, pClCurp, pClEstadoCivil, pNbConyuge, pClNss, pClTipoSanguineo, pNbPais, NB_ESTADO, NB_MUNICIPIO, pNbColonia, pNbCalle, pNoInterior, pNoExterior, pClCodigoPostal, pClCorreoElectronico, pFeNacimiento, pDsLugarNacimiento, pMnSueldo, pClNacionalidad, pDsNacionalidad, pNbLicencia, pDsVehiculo, pClCartillaMilitar, pClCedulaProfesional, pXmlTelefonos, pXmlIngresos, pXmlEgresos, pXmlPatrimonio, pDsDisponibilidad, pClDisponibilidadViaje, pXmlPerfilRedSocial, pDsComentario, pFgActivo, pXmlSeleccion).ToList();
			}
		}
        public List<SPE_OBTIENE_CANDIDATOS_BATERIA_Result> ObtenerCandidatosBateria(XElement lstCandidatos)
        {
            using (context = new SistemaSigeinEntities())
            {

                return context.SPE_OBTIENE_CANDIDATOS_BATERIA(lstCandidatos.ToString()).ToList();
            }
        }

        //public XElement InsertarActualizarCandidato(string pTipoTransaccion, E_CANDIDATO pCandidato, string pClUsuario, string pNbPrograma)
        //{
        //    using (context = new SistemaSigeinEntities ())
        //    {
        //        ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
        //        context.SPE_INSERTA_ACTUALIZA_C_CANDIDATO(pOutClRetorno,  pCandidato.ID_CANDIDATO,pCandidato.NB_CANDIDATO,pCandidato.NB_APELLIDO_PATERNO,pCandidato.NB_APELLIDO_MATERNO,pCandidato.CL_GENERO,pCandidato.CL_RFC,pCandidato.CL_CURP,pCandidato.CL_ESTADO_CIVIL,pCandidato.NB_CONYUGUE,pCandidato.CL_NSS,pCandidato.CL_TIPO_SANGUINEO,pCandidato.NB_PAIS,pCandidato.NB_ESTADO,pCandidato.NB_MUNICIPIO,pCandidato.NB_COLONIA,pCandidato.NB_CALLE,pCandidato.NO_INTERIOR,pCandidato.NO_EXTERIOR,pCandidato.CL_CODIGO_POSTAL,pCandidato.CL_CORREO_ELECTRONICO,pCandidato.FE_NACIMIENTO,pCandidato.DS_LUGAR_NACIMIENTO,pCandidato.MN_SUELDO,pCandidato.CL_NACIONALIDAD,pCandidato.DS_NACIONALIDAD,pCandidato.NB_LICENCIA,pCandidato.DS_VEHICULO,pCandidato.CL_CARTILLA_MILITAR,pCandidato.CL_CEDULA_PROFESIONAL,pCandidato.XML_TELEFONOS,pCandidato.XML_INGRESOS,pCandidato.XML_EGRESOS,pCandidato.XML_PATRIMONIO,pCandidato.DS_DISPONIBILIDAD,pCandidato.CL_DISPONIBILIDAD_VIAJE,pCandidato.XML_PERFIL_RED_SOCIAL,pCandidato.DS_COMENTARIO,pCandidato.FG_ACTIVO,pClUsuario,pClUsuario,pNbPrograma, pNbPrograma, pTipoTransaccion);
        //        return XElement.Parse(pOutClRetorno.Value.ToString());
        //    }
        //}

        //public XElement EliminarCandidato(int? pIdCandidato = null, string pClUsuario = null, string pNbPrograma = null)
        //{
        //    using (context = new SistemaSigeinEntities ())
        //    {
        //        //Declaramos el objeto de valor de retorno
        //        ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
        //        context.SPE_ELIMINA_C_CANDIDATO(pOutClRetorno, pIdCandidato, pClUsuario, pNbPrograma);
        //        return XElement.Parse(pOutClRetorno.Value.ToString());
        //    }
        //}

        //public List<SPE_OBTIENE_CANDIDATO_IDONEO_Result> ObtenerCandidatoIdoneo(int pIdrequisicion, string pXmlCandidatos, decimal pPrCompatibilidad, int pNoCandidatosMaximos, bool pFgFoto = false)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //return context.SPE_OBTIENE_CANDIDATO_IDONEO(pIdrequisicion, pXmlCandidatos, 75).ToList();
        //        return context.SPE_OBTIENE_CANDIDATO_IDONEO(pIdrequisicion, pXmlCandidatos, pPrCompatibilidad, pNoCandidatosMaximos,pFgFoto).ToList();
        //    }
        //}

        //public List<SPE_OBTIENE_CANDIDATO_IDONEO_PERFIL_Result> ObtenerCandidatoIdoneoPerfil(int pIdrequisicion, int pNoCandidatosMaximos, string xmlCandidatos,bool pFgFoto = false)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        //return context.SPE_OBTIENE_CANDIDATO_IDONEO(pIdrequisicion, pXmlCandidatos, 75).ToList();
        //        return context.SPE_OBTIENE_CANDIDATO_IDONEO_PERFIL(pIdrequisicion,pNoCandidatosMaximos, xmlCandidatos,pFgFoto).ToList();
        //    }
        //}

        public List<SPE_OBTIENE_ANALISIS_COMPETENCIAS_CANDIDATO_IDONEO_Result> ObtenerAnalisisCompetencias(int pIdRequisicion, int pIdCandidato,int pIdBateria)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_ANALISIS_COMPETENCIAS_CANDIDATO_IDONEO(pIdRequisicion, pIdCandidato, pIdBateria).ToList();
            }
        }

        public SPE_OBTIENE_PUESTO_REQUISICION_Result ObtenerPuestoRequisiscion(int pIdRequisicion)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PUESTO_REQUISICION(pIdRequisicion).FirstOrDefault();
            }
        }
        public List<SPE_OBTIENE_CANDIDATOS_CONTRATADOS_Result> ObtenerCandidatosContratados(int? pIdEmpresa = null, int? pIdRol = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_CANDIDATOS_CONTRATADOS(pIdEmpresa, pIdRol).ToList();
            }
        }


          //public List<SPE_OBTIENE_K_REQUISICION_CANDIDATO_Result> ObtenerCandidatosIdoneos(int? ID_CANDIDATO = null)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        var q = from V_C_CANDIDATO in context.SPE_OBTIENE_K_REQUISICION_CANDIDATO(ID_CANDIDATO)
        //                select V_C_CANDIDATO;
        //        return q.ToList();
        //    }
        //}

        //public XElement SPE_InsertaActualiza_C_CANDIDATO(string tipo_transaccion, SPE_OBTIENE_C_CANDIDATO_Result V_C_CANDIDATO, string usuario, string programa)
        //{
        //    using (context = new SistemaSigeinEntities())
        //    {
        //        ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
        //        context.SPE_INSERTA_ACTUALIZA_C_CANDIDATO(pOutClRetorno, V_C_CANDIDATO.ID_CANDIDATO, V_C_CANDIDATO.NB_CANDIDATO, V_C_CANDIDATO.NB_APELLIDO_PATERNO, V_C_CANDIDATO.NB_APELLIDO_MATERNO, V_C_CANDIDATO.CL_GENERO, V_C_CANDIDATO.CL_RFC, V_C_CANDIDATO.CL_CURP, V_C_CANDIDATO.CL_ESTADO_CIVIL, V_C_CANDIDATO.NB_CONYUGUE, V_C_CANDIDATO.CL_NSS, V_C_CANDIDATO.CL_TIPO_SANGUINEO, V_C_CANDIDATO.NB_PAIS, V_C_CANDIDATO.NB_ESTADO, V_C_CANDIDATO.NB_MUNICIPIO, V_C_CANDIDATO.NB_COLONIA, V_C_CANDIDATO.NB_CALLE, V_C_CANDIDATO.NO_INTERIOR, V_C_CANDIDATO.NO_EXTERIOR, V_C_CANDIDATO.CL_CODIGO_POSTAL, V_C_CANDIDATO.CL_CORREO_ELECTRONICO, V_C_CANDIDATO.FE_NACIMIENTO, V_C_CANDIDATO.DS_LUGAR_NACIMIENTO, V_C_CANDIDATO.MN_SUELDO, V_C_CANDIDATO.CL_NACIONALIDAD, V_C_CANDIDATO.DS_NACIONALIDAD, V_C_CANDIDATO.NB_LICENCIA, V_C_CANDIDATO.DS_VEHICULO, V_C_CANDIDATO.CL_CARTILLA_MILITAR, V_C_CANDIDATO.CL_CEDULA_PROFESIONAL, V_C_CANDIDATO.XML_TELEFONOS, V_C_CANDIDATO.XML_INGRESOS, V_C_CANDIDATO.XML_EGRESOS, V_C_CANDIDATO.XML_PATRIMONIO, V_C_CANDIDATO.DS_DISPONIBILIDAD, V_C_CANDIDATO.CL_DISPONIBILIDAD_VIAJE, V_C_CANDIDATO.XML_PERFIL_RED_SOCIAL, V_C_CANDIDATO.DS_COMENTARIO, V_C_CANDIDATO.FG_ACTIVO, usuario, usuario, programa, programa, tipo_transaccion);
        //        return XElement.Parse(pOutClRetorno.Value.ToString());
        //    }
        //}
	}
}