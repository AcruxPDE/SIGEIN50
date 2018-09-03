using SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class DescriptivoPuestoNegocio
    {
        public E_RESULTADO ObtieneValidacionCambioDescriptivo(string id_puesto, string id_empleado, string usuario, string programa)
        {
            DescriptivoPuestoOperaciones operaciones = new DescriptivoPuestoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ObtieneValidacionCambioDescriptivo(id_puesto, id_empleado, usuario, programa));
        }

        public E_RESULTADO EliminaDescriptivoPuesto(string PIN_ID_PUESTO, string CL_USUARIO = "", string NB_PROGRAMA = "", string PIN_ID_EMPLEADO = null, int? ID_CAMBIO = null)
     {
             DescriptivoPuestoOperaciones operaciones = new DescriptivoPuestoOperaciones();
             return UtilRespuesta.EnvioRespuesta(operaciones.EliminaDescriptivoPuesto(PIN_ID_PUESTO, CL_USUARIO, NB_PROGRAMA, PIN_ID_EMPLEADO, ID_CAMBIO));
          
     }

        public E_RESULTADO InsertaActualizaCopiaDescriptivo(string PIN_ID_PUESTO, string CL_USUARIO = "", string NB_PROGRAMA = "", string PIN_ID_EMPLEADO = null)
        {
           
             DescriptivoPuestoOperaciones operaciones = new DescriptivoPuestoOperaciones();
               return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaCopiaDescriptivo(PIN_ID_PUESTO, CL_USUARIO, NB_PROGRAMA, PIN_ID_EMPLEADO));
            
        }
        public E_RESULTADO InsertaActualizaDescriptivo( string PIN_ID_PUESTO ="", string CL_USUARIO = "", string NB_PROGRAMA = "",
            int? ID_CAMBIO = null , bool FG_AUTORIZADO =true ,string DS_CAMBIO ="" , string PIN_CL_ESTADO ="")   {

            DescriptivoPuestoOperaciones operaciones = new DescriptivoPuestoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaDescriptivo(PIN_ID_PUESTO, CL_USUARIO, NB_PROGRAMA, 
                ID_CAMBIO ,FG_AUTORIZADO ,DS_CAMBIO,PIN_CL_ESTADO));
        }


        public E_DESCRIPTIVO_PDE ObtieneDescriptivo(string pIdDescriptivo, int? pIdCambio, string pIdeEmpleado)
        {
            DescriptivoPuestoOperaciones nDescriptivo = new DescriptivoPuestoOperaciones();
            SPE_OBTIENE_DESCRIPTIVO_PDE_Result vDescriptivo = nDescriptivo.ObtenerDescriptivo(pIdDescriptivo, pIdCambio, pIdeEmpleado);

            string XML_CA = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("CAMPOS_ADICIONALES").FirstOrDefault().ToString();

            List<E_ESCOLARIDADES> vEscolaridades = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("ESCOLARIDADES").Elements("ESCOLARIDAD").Select(el => new E_ESCOLARIDADES
            {
                ID_ESCOLARIDAD = UtilXML.ValorAtributo<int?>(el.Attribute("ID_ESCOLARIDAD")),
                NB_ESCOLARIDAD = el.Attribute("NB_ESCOLARIDAD").Value,
                DS_ESCOLARIDAD = el.Attribute("DS_ESCOLARIDAD").Value,
                CL_NB_NIVEL_ESCOLARIDAD = el.Attribute("CL_NB_NIVEL_ESCOLARIDAD").Value,
                FG_ACTIVO = UtilXML.ValorAtributo<bool>(el.Attribute("FG_ACTIVO")),
                ID_NIVEL_ESCOLARIDAD = UtilXML.ValorAtributo<int?>(el.Attribute("ID_NIVEL_ESCOLARIDAD")),
                CL_INSTITUCION = el.Attribute("CL_TIPO_ESCOLARIDAD").Value,
                CL_NIVEL_ESCOLARIDAD = UtilXML.ValorAtributo<int?>(el.Attribute("CL_NIVEL_ESCOLARIDAD")),
            }).ToList();

            List<E_CATALOGO_CATALOGOS> vCatalogoGenero = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("GENEROS").Elements("GENERO").Select(el => new E_CATALOGO_CATALOGOS
            {
                ID_CATALOGO_VALOR = UtilXML.ValorAtributo<int?>(el.Attribute("ID_CATALOGO_VALOR")),
                CL_CATALOGO_VALOR = el.Attribute("CL_CATALOGO_VALOR").Value,
                NB_CATALOGO_VALOR = el.Attribute("NB_CATALOGO_VALOR").Value,
                DS_CATALOGO_VALOR = el.Attribute("DS_CATALOGO_VALOR").Value,
                ID_CATALOGO_LISTA = UtilXML.ValorAtributo<int?>(el.Attribute("ID_CATALOGO_LISTA")),
                NB_CATALOGO_LISTA = el.Attribute("NB_CATALOGO_LISTA").Value,
                FG_SELECCIONADO = UtilXML.ValorAtributo<bool>(el.Attribute("FG_SELECCIONADO"))
            }).ToList();

            List<E_CATALOGO_CATALOGOS> vCatalogoEdoCivil = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("ESTADOS_CIVILES").Elements("ESTADO_CIVIL").Select(el => new E_CATALOGO_CATALOGOS
            {
                ID_CATALOGO_VALOR = UtilXML.ValorAtributo<int?>(el.Attribute("ID_CATALOGO_VALOR")),
                CL_CATALOGO_VALOR = el.Attribute("CL_CATALOGO_VALOR").Value,
                NB_CATALOGO_VALOR = el.Attribute("NB_CATALOGO_VALOR").Value,
                DS_CATALOGO_VALOR = el.Attribute("DS_CATALOGO_VALOR").Value,
                ID_CATALOGO_LISTA = UtilXML.ValorAtributo<int?>(el.Attribute("ID_CATALOGO_LISTA")),
                NB_CATALOGO_LISTA = el.Attribute("NB_CATALOGO_LISTA").Value,
                FG_SELECCIONADO = UtilXML.ValorAtributo<bool>(el.Attribute("FG_SELECCIONADO"))
            }).ToList();

            List<E_COMPETENCIAS> vCatalogoCompetencia = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("COMPETENCIAS").Elements("COMPETENCIA").Select(el => new E_COMPETENCIAS
            {
                ID_COMPETENCIA = UtilXML.ValorAtributo<int?>(el.Attribute("ID_COMPETENCIA")),
                CL_COMPETENCIA = el.Attribute("CL_COMPETENCIA").Value,
                NB_COMPETENCIA = el.Attribute("NB_COMPETENCIA").Value,
                DS_COMPETENCIA = el.Attribute("DS_COMPETENCIA").Value,
                CL_TIPO_COMPETENCIA = el.Attribute("CL_TIPO_COMPETENCIA").Value,
                CL_PUESTO_TIPO_COMPETENCIA = el.Attribute("CL_PUESTO_TIPO_COMPETENCIA").Value,
                NB_TIPO_COMPETENCIA = el.Attribute("NB_TIPO_COMPETENCIA").Value,
                CL_CLASIFICACION = el.Attribute("CL_CLASIFICACION").Value,
                NB_CLASIFICACION = el.Attribute("NB_CLASIFICACION").Value,
                DS_CLASIFICACION = el.Attribute("DS_CLASIFICACION").Value,
                CL_CLASIFICACION_COLOR = el.Attribute("CL_CLASIFICACION_COLOR").Value,
                ID_NIVEL_DESEADO = UtilXML.ValorAtributo<int>(el.Attribute("ID_NIVEL_DESEADO")),
                NO_VALOR_NIVEL = UtilXML.ValorAtributo<int>(el.Attribute("NO_VALOR_NIVEL")),
                ID_NIVEL0 = UtilXML.ValorAtributo<int>(el.Attribute("ID_NIVEL0")),
                DS_COMENTARIOS_NIVEL0 = el.Attribute("DS_COMENTARIOS_NIVEL0").Value,
                ID_NIVEL1 = UtilXML.ValorAtributo<int>(el.Attribute("ID_NIVEL1")),
                DS_COMENTARIOS_NIVEL1 = el.Attribute("DS_COMENTARIOS_NIVEL1").Value,
                ID_NIVEL2 = UtilXML.ValorAtributo<int>(el.Attribute("ID_NIVEL2")),
                DS_COMENTARIOS_NIVEL2 = el.Attribute("DS_COMENTARIOS_NIVEL2").Value,
                ID_NIVEL3 = UtilXML.ValorAtributo<int>(el.Attribute("ID_NIVEL3")),
                DS_COMENTARIOS_NIVEL3 = el.Attribute("DS_COMENTARIOS_NIVEL3").Value,
                ID_NIVEL4 = UtilXML.ValorAtributo<int>(el.Attribute("ID_NIVEL4")),
                DS_COMENTARIOS_NIVEL4 = el.Attribute("DS_COMENTARIOS_NIVEL4").Value,
                ID_NIVEL5 = UtilXML.ValorAtributo<int>(el.Attribute("ID_NIVEL5")),
                DS_COMENTARIOS_NIVEL5 = el.Attribute("DS_COMENTARIOS_NIVEL5").Value,
                ID_PUESTO_FUNCION = UtilXML.ValorAtributo<int>(el.Attribute("ID_PUESTO_FUNCION")),
                ID_PUESTO_COMPETENCIA = UtilXML.ValorAtributo<int>(el.Attribute("ID_PUESTO_COMPETENCIA"))


            }).ToList();

            List<E_COMPETENCIAS> vCatalogoCompetencias = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("CATALOGO_COMPETENCIAS").Elements("CATALOGO_COMPETENCIA").Select(el => new E_COMPETENCIAS
            {
                ID_COMPETENCIA = UtilXML.ValorAtributo<int?>(el.Attribute("ID_COMPETENCIA")),
                CL_COMPETENCIA = el.Attribute("CL_COMPETENCIA").Value,
                NB_COMPETENCIA = el.Attribute("NB_COMPETENCIA").Value,
                DS_COMPETENCIA = el.Attribute("DS_COMPETENCIA").Value,
                CL_TIPO_COMPETENCIA = el.Attribute("CL_TIPO_COMPETENCIA").Value,
                CL_CLASIFICACION = el.Attribute("CL_CLASIFICACION").Value
            }).ToList();

            List<E_AREAS_INTERES> vCatalogoAreaInteres = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("AREAS_INTERES").Elements("AREA_INTERES").Select(el => new E_AREAS_INTERES
            {
                CL_AREA_INTERES = UtilXML.ValorAtributo<int?>(el.Attribute("CL_AREA_INTERES")),
                ID_AREA_INTERES = UtilXML.ValorAtributo<int?>(el.Attribute("ID_AREA_INTERES")),
                //NB_ACTIVO = el.Attribute("NB_ACTIVO").Value,
                NB_AREA_INTERES = el.Attribute("NB_AREA_INTERES").Value
            }).ToList();

            List<E_AREAS> vCatalogoAreas = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("DEPARTAMENTOS").Elements("DEPARTAMENTO").Select(el => new E_AREAS
            {
                ID_DEPARTAMENTO = UtilXML.ValorAtributo<int?>(el.Attribute("ID_DEPARTAMENTO")),
                CL_DEPARTAMENTO = el.Attribute("CL_DEPARTAMENTO").Value,
                NB_DEPARTAMENTO = el.Attribute("NB_DEPARTAMENTO").Value,
                FG_SELECCIONADO = UtilXML.ValorAtributo<bool>(el.Attribute("FG_SELECCIONADO"))

            }).ToList();

            List<E_CENTRO_ADMVO> vCatalogoCentroADMVO = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("CENTROS_ADMVOS").Elements("CENTRO_ADMVO").Select(el => new E_CENTRO_ADMVO
            {
                ID_CENTRO_ADMVO = el.Attribute("ID_CENTRO_ADMVO").Value,
                CL_CENTRO_ADMVO = el.Attribute("CL_CENTRO_ADMVO").Value,
                NB_CENTRO_ADMVO = el.Attribute("NB_CENTRO_ADMVO").Value,
                FG_SELECCIONADO = UtilXML.ValorAtributo<bool>(el.Attribute("FG_SELECCIONADO"))
            }).ToList();

            List<E_CENTRO_OPTVO> vCatalogoCentroOPTVO = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("CENTROS_OPTVOS").Elements("CENTRO_OPTVO").Select(el => new E_CENTRO_OPTVO
            {
                ID_CENTRO_OPTVO = el.Attribute("ID_CENTRO_OPTVO").Value,
                CL_CENTRO_OPTVO = el.Attribute("CL_CENTRO_OPTVO").Value,
                NB_CENTRO_OPTVO = el.Attribute("NB_CENTRO_OPTVO").Value,
                FG_SELECCIONADO = UtilXML.ValorAtributo<bool>(el.Attribute("FG_SELECCIONADO"))
            }).ToList();

            List<E_PUESTOS> vCatalogoPuestos = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("PUESTOS").Elements("PUESTO").Select(el => new E_PUESTOS
            {
                ID_PUESTO = UtilXML.ValorAtributo<int?>(el.Attribute("ID_PUESTO")),
                CL_PUESTO = el.Attribute("CL_PUESTO").Value,
                NB_PUESTO = el.Attribute("NB_PUESTO").Value,
                ID_DEPARTAMENTO = UtilXML.ValorAtributo<int?>(el.Attribute("ID_DEPARTAMENTO")),
                CL_TIPO_RELACION = el.Attribute("CL_TIPO_RELACION").Value
            }).ToList();

            List<E_INDICADOR_DESEMPENO> vCatalogoIndicadores = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("INDICADORES").Elements("INDICADOR").Select(el => new E_INDICADOR_DESEMPENO
            {
                ID_INDICADOR = UtilXML.ValorAtributo<int>(el.Attribute("ID_INDICADOR")),
                NB_INDICADOR = el.Attribute("NB_INDICADOR").Value
            }).ToList();



            return new E_DESCRIPTIVO_PDE
            {
                ID_PUESTO = (vDescriptivo.ID_PUESTO == "=") ? null : vDescriptivo.ID_PUESTO,
                FG_ACTIVO = vDescriptivo.FG_ACTIVO,
                FE_INACTIVO = vDescriptivo.FE_INACTIVO,
                CL_PUESTO = vDescriptivo.CL_PUESTO,
                NB_PUESTO = vDescriptivo.NB_PUESTO,
                NO_EDAD_MINIMA = vDescriptivo.NO_EDAD_MINIMA,
                NO_EDAD_MAXIMA = vDescriptivo.NO_EDAD_MAXIMA,
                XML_REQUERIMIENTOS = vDescriptivo.XML_REQUERIMIENTOS,
                XML_OBSERVACIONES = vDescriptivo.XML_OBSERVACIONES,
                XML_RESPONSABILIDAD = vDescriptivo.XML_RESPONSABILIDAD,
                XML_AUTORIDAD = vDescriptivo.XML_AUTORIDAD,
                XML_CURSOS_ADICIONALES = vDescriptivo.XML_CURSOS_ADICIONALES,
                XML_MENTOR = vDescriptivo.XML_MENTOR,
                CL_TIPO_PUESTO = vDescriptivo.CL_TIPO_PUESTO,
                XML_CAMPOS_ADICIONALES = vDescriptivo.XML_CAMPOS_ADICIONALES,
                ID_BITACORA = vDescriptivo.ID_BITACORA,
                CL_POSICION_ORGANIGRAMA = vDescriptivo.CL_POSICION_ORGANIGRAMA,

                CL_DOCUMENTO = vDescriptivo.CL_DOCUMENTO,
                CL_VERSION = vDescriptivo.CL_VERSION,
                FE_ELABORACION = vDescriptivo.FE_ELABORACION,
                NB_ELABORO = vDescriptivo.NB_ELABORO,
                FE_REVISION = vDescriptivo.FE_REVISION,
                NB_REVISO = vDescriptivo.NB_REVISO,
                FE_AUTORIZACION = vDescriptivo.FE_AUTORIZACION,
                NB_AUTORIZO = vDescriptivo.NB_AUTORIZO,
                DS_CONTROL_CAMBIOS = vDescriptivo.DS_CONTROL_CAMBIOS,
                CL_TIPO_PRESTACIONES = vDescriptivo.CL_TIPO_PRESTACIONES,
                NO_PLAZAS = vDescriptivo.NO_PLAZAS,
                NO_MINIMO_PLAZAS = vDescriptivo.NO_PLAZAS_MIN,

                XML_PRESTACIONES = vDescriptivo.XML_PRESTACIONES,
                XML_CODIGO_CAMPOS_ADICIONALES = XML_CA,
                XML_PUESTO_COMPETENCIA = vDescriptivo.XML_PUESTO_COMPETENCIA,
                XML_PUESTO_ESCOLARIDAD = vDescriptivo.XML_PUESTO_ESCOLARIDAD,
                XML_PUESTO_EXPERIENCIA = vDescriptivo.XML_PUESTO_EXPERIENCIA,
                XML_PUESTO_FUNCION = vDescriptivo.XML_PUESTO_FUNCION,
                XML_PUESTO_INDICADOR = vDescriptivo.XML_PUESTO_INDICADOR,
                XML_PUESTOS_RELACIONADOS = vDescriptivo.XML_PUESTOS_RELACIONADOS,

                LST_ESCOLARIDADES = vEscolaridades,
                LST_CATALOGO_GENERO = vCatalogoGenero,
                LST_CATALOGO_EDOCIVIL = vCatalogoEdoCivil,
                LST_CATALOGO_COMPETENCIAS_ESP = vCatalogoCompetencia,
                LST_CATALOGO_COMPETENCIAS = vCatalogoCompetencias,
                LST_AREAS_INTERES = vCatalogoAreaInteres,
                LST_AREAS = vCatalogoAreas,
                LST_CENTRO_ADMVO = vCatalogoCentroADMVO,
                LST_CENTRO_OPTVO = vCatalogoCentroOPTVO,
                LST_PUESTOS = vCatalogoPuestos,
                LST_INDICADORES = vCatalogoIndicadores
                //LST_PAQUETE_PRESTACIONES = vCatalogoPaqPres
            };

        }
        public E_RESULTADO ActualizaModificacionDescriptivo(int? ID_CAMBIO = null, bool FG_AUTORIZADO = false, string DS_CAMBIO = "", string ID_EMPLEADO = null, string CL_ESTADO = "", string TIPO_TRANSACCION = "", string CL_USUARIO_APP = "", string NB_PROGRAMA = "")
        {

            DescriptivoPuestoOperaciones operaciones = new DescriptivoPuestoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.ActualizaModificacionDescriptivo(ID_CAMBIO, FG_AUTORIZADO, DS_CAMBIO, ID_EMPLEADO, CL_ESTADO, TIPO_TRANSACCION, CL_USUARIO_APP, NB_PROGRAMA));

        }

        public E_RESULTADO FinalizaModificacionDescriptivo(string PIN_ID_PUESTO, string CL_USUARIO = "", string NB_PROGRAMA = "", string PIN_ID_EMPLEADO = null)
        {

            DescriptivoPuestoOperaciones operaciones = new DescriptivoPuestoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.FinalizaModificacionDescriptivo(PIN_ID_PUESTO, CL_USUARIO, NB_PROGRAMA, PIN_ID_EMPLEADO));

        }
        public E_RESULTADO InsertaActualizaDescriptivo(string PIN_ID_PUESTO, int? ID_CAMBIO = null, string CL_USUARIO = "", string NB_PROGRAMA = "", string tipo_transaccion = "", E_PUESTO V_M_PUESTO = null)
        {
            DescriptivoPuestoOperaciones operaciones = new DescriptivoPuestoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertaActualizaDescriptivo(PIN_ID_PUESTO, ID_CAMBIO, CL_USUARIO, NB_PROGRAMA, tipo_transaccion, V_M_PUESTO));
     
           
        }
    }
}
