using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Entidades.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;
using System.Xml.Linq;

namespace SIGE.Negocio.Administracion
{
    public class PuestoNegocio
    {
        public List<SPE_OBTIENE_M_PUESTO_Result> ObtienePuestos(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, String pClPuesto = null, String pNbPuesto = null, int? pIdPuestoJefe = null, int? pIdDepartamento = null, String pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, String pClGenero = null, String pClEstadoCivil = null, String pXmlRequerimientos = null, String pXmlObservaciones = null, String pXmlResponsabilidad = null, String XML_AUTORIDAD = null, String XML_CURSOS_ADICIONALES = null, String XML_MENTOR = null, String CL_TIPO_PUESTO = null, Guid? ID_CENTRO_ADMINISTRATIVO = null, Guid? ID_CENTRO_OPERATIVO = null, int? ID_PAQUETE_PRESTACIONES = null, String NB_DEPARTAMENTO = null, String CL_DEPARTAMENTO = null, string xml_puestos = null, XElement XML_PUESTOS_SELECCIONADOS = null, int? ID_EMPRESA = null, int? ID_ROL = null)
        {
            PuestoOperaciones operaciones = new PuestoOperaciones();
            return operaciones.ObtenerPuestos(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidad, XML_AUTORIDAD, XML_CURSOS_ADICIONALES, XML_MENTOR, CL_TIPO_PUESTO, ID_CENTRO_ADMINISTRATIVO, ID_CENTRO_OPERATIVO, ID_PAQUETE_PRESTACIONES, NB_DEPARTAMENTO, CL_DEPARTAMENTO, xml_puestos, XML_PUESTOS_SELECCIONADOS, ID_EMPRESA, ID_ROL);
        }

        public List<SPE_OBTIENE_M_PUESTOS_GENERAL_Result> ObtienePuestosGeneral(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, String pClPuesto = null, String pNbPuesto = null, int? pIdPuestoJefe = null, int? pIdDepartamento = null, String pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, String pClGenero = null, String pClEstadoCivil = null, String pXmlRequerimientos = null, String pXmlObservaciones = null, String pXmlResponsabilidad = null, String XML_AUTORIDAD = null, String XML_CURSOS_ADICIONALES = null, String XML_MENTOR = null, String CL_TIPO_PUESTO = null, Guid? ID_CENTRO_ADMINISTRATIVO = null, Guid? ID_CENTRO_OPERATIVO = null, int? ID_PAQUETE_PRESTACIONES = null, String NB_DEPARTAMENTO = null, String CL_DEPARTAMENTO = null, string xml_puestos = null, XElement XML_PUESTOS_SELECCIONADOS = null, int? ID_EMPRESA = null)
        {
            PuestoOperaciones operaciones = new PuestoOperaciones();
            return operaciones.ObtenerPuestosGeneral(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidad, XML_AUTORIDAD, XML_CURSOS_ADICIONALES, XML_MENTOR, CL_TIPO_PUESTO, ID_CENTRO_ADMINISTRATIVO, ID_CENTRO_OPERATIVO, ID_PAQUETE_PRESTACIONES, NB_DEPARTAMENTO, CL_DEPARTAMENTO, xml_puestos, XML_PUESTOS_SELECCIONADOS, ID_EMPRESA);
        }

        public List<SPE_VERIFICA_PUESTO_FACTOR_Result> ValidarConfiguracionPuestos(string vXmlPuestos = null, int? pIdPuesto = null)
        {
            PuestoOperaciones operaciones = new PuestoOperaciones();
            return operaciones.ValidarConfiguracionPuestos(vXmlPuestos, pIdPuesto);
        }

        public List<SPE_OBTIENE_M_PUESTO_REQUISICION_Result> ObtienePuestosRequisicion(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, String pClPuesto = null, String pNbPuesto = null, int? pIdPuestoJefe = null, int? pIdDepartamento = null, String pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, String pClGenero = null, String pClEstadoCivil = null, String pXmlRequerimientos = null, String pXmlObservaciones = null, String pXmlResponsabilidad = null, String XML_AUTORIDAD = null, String XML_CURSOS_ADICIONALES = null, String XML_MENTOR = null, String CL_TIPO_PUESTO = null, Guid? ID_CENTRO_ADMINISTRATIVO = null, Guid? ID_CENTRO_OPERATIVO = null, int? ID_PAQUETE_PRESTACIONES = null, String NB_DEPARTAMENTO = null, String CL_DEPARTAMENTO = null, string xml_puestos = null, XElement XML_PUESTOS_SELECCIONADOS = null, int? ID_EMPRESA = null)
        {
            PuestoOperaciones operaciones = new PuestoOperaciones();
            return operaciones.ObtenerPuestosRequisicion(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidad, XML_AUTORIDAD, XML_CURSOS_ADICIONALES, XML_MENTOR, CL_TIPO_PUESTO, ID_CENTRO_ADMINISTRATIVO, ID_CENTRO_OPERATIVO, ID_PAQUETE_PRESTACIONES, NB_DEPARTAMENTO, CL_DEPARTAMENTO, xml_puestos, XML_PUESTOS_SELECCIONADOS, ID_EMPRESA);
        }
        public E_RESULTADO InsertaActualizaPuesto(string tipo_transaccion, E_PUESTO V_M_PUESTO, List<UDTT_ARCHIVO> pLstArchivoTemporales, List<E_DOCUMENTO> pLstDocumentos, string usuario, string programa,string pXmlOcupacion)
        {
            PuestoOperaciones operaciones = new PuestoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarPuesto(tipo_transaccion, V_M_PUESTO, pLstArchivoTemporales,pLstDocumentos,  usuario, programa,pXmlOcupacion));
        }
        public E_RESULTADO InsertaActualizaPuestoRequsicion(string pTipoTransaccion = null, int? pIdPuesto = null, string pXmlPuesto = null, string pXmlPuestoOcupacion = null, int? pIdRequisicion = null, string pDsComentarios = null, string pClAutorizaRechaza = null, string pClAutorizaPuesto = null, string pClUsuario = null, string pNbPrograma = null)
        {
            PuestoOperaciones oPuesto = new PuestoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPuesto.InsertarActualizarPuestoRequisicion(pTipoTransaccion, pIdPuesto, pXmlPuesto,pXmlPuestoOcupacion, pIdRequisicion, pDsComentarios, pClAutorizaRechaza, pClAutorizaPuesto, pClUsuario, pNbPrograma));
        }
        public E_RESULTADO EliminaPuesto(int? pIdPuesto = null, string pClPuesto = null, string pClUsuario = null, string pNbPrograma = null)
        {
            PuestoOperaciones oPuesto = new PuestoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPuesto.EliminarPuesto(pIdPuesto, pClPuesto, pClUsuario, pNbPrograma));
        }
    }
}
