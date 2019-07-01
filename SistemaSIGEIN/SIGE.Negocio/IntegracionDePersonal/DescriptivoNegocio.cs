using SIGE.Entidades.Externas;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Xml.Linq;
using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;
using SIGE.Entidades.Administracion;

namespace SIGE.Negocio.Administracion
{
    public class DescriptivoNegocio
    {
        public E_DESCRIPTIVO ObtieneDescriptivo(int? pIdDescriptivo)
        {
            DescriptivoOperaciones nDescriptivo = new DescriptivoOperaciones();
            SPE_OBTIENE_DESCRIPTIVO_Result vDescriptivo = nDescriptivo.ObtenerDescriptivo(pIdDescriptivo);

             E_DESCRIPTIVO eDescriptivo=new E_DESCRIPTIVO();

            string XML_CA = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("CAMPOS_ADICIONALES").FirstOrDefault().ToString();

            
            if (vDescriptivo.XML_PUESTO_OCUPACION != null)
                eDescriptivo.LST_OCUPACION_PUESTO = XElement.Parse(vDescriptivo.XML_PUESTO_OCUPACION).Elements("PUESTOOCUPACION").Select(el => new E_OCUPACION_PUESTO
                {
                    ID_OCUPACION_PUESTO = UtilXML.ValorAtributo<int>(el.Attribute("ID_OCUPACION_PUESTO")),
                    ID_OCUPACION = UtilXML.ValorAtributo<int>(el.Attribute("ID_OCUPACION")),
                    NB_OCUPACION = UtilXML.ValorAtributo<string>(el.Attribute("NB_OCUPACION")),
                    CL_OCUPACION = UtilXML.ValorAtributo<string>(el.Attribute("CL_OCUPACION")),
                    NB_MODULO = UtilXML.ValorAtributo<string>(el.Attribute("NB_MODULO")),
                    CL_MODULO = UtilXML.ValorAtributo<string>(el.Attribute("CL_MODULO")),
                    NB_SUBAREA = UtilXML.ValorAtributo<string>(el.Attribute("NB_SUBAREA")),
                    CL_SUBAREA = UtilXML.ValorAtributo<string>(el.Attribute("CL_SUBAREA")),
                    NB_AREA = UtilXML.ValorAtributo<string>(el.Attribute("NB_AREA")),
                    CL_AREA = UtilXML.ValorAtributo<string>(el.Attribute("CL_AREA"))
                }).FirstOrDefault();
            

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

            return new E_DESCRIPTIVO
            {
                ID_PUESTO = (vDescriptivo.ID_PUESTO < 0) ? (int?)null : vDescriptivo.ID_PUESTO,
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
                NO_NIVEL_ORGANIGRAMA = vDescriptivo.NO_NIVEL_ORGANIGRAMA,

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
                DS_COMPETENCIAS_REQUERIDAS = vDescriptivo.DS_COMPETENCIAS_REQUERIDAS,

                XML_PRESTACIONES = vDescriptivo.XML_PRESTACIONES,
                XML_CODIGO_CAMPOS_ADICIONALES = XML_CA,
                XML_PUESTO_COMPETENCIA = vDescriptivo.XML_PUESTO_COMPETENCIA,
                XML_PUESTO_ESCOLARIDAD = vDescriptivo.XML_PUESTO_ESCOLARIDAD,
                XML_PUESTO_EXPERIENCIA = vDescriptivo.XML_PUESTO_EXPERIENCIA,
                XML_PUESTO_FUNCION = vDescriptivo.XML_PUESTO_FUNCION,
                XML_PUESTO_INDICADOR = vDescriptivo.XML_PUESTO_INDICADOR,
                XML_PUESTOS_RELACIONADOS = vDescriptivo.XML_PUESTOS_RELACIONADOS,
                XML_DOCUMENTOS = vDescriptivo.XML_DOCUMENTOS_PUESTO,

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
                LST_INDICADORES = vCatalogoIndicadores,
                //LST_PAQUETE_PRESTACIONES = vCatalogoPaqPres
                LST_OCUPACION_PUESTO=eDescriptivo.LST_OCUPACION_PUESTO
            };

        }
        public E_DESCRIPTIVO ObtieneDescriptivoRequisicion(int? pIdDescriptivo)
        {
            DescriptivoOperaciones nDescriptivo = new DescriptivoOperaciones();
            SPE_OBTIENE_DESCRIPTIVO_REQUISICION_Result vDescriptivo = nDescriptivo.ObtenerDescriptivoRequisicion(pIdDescriptivo);

            E_DESCRIPTIVO eDescriptivo = new E_DESCRIPTIVO();

            string XML_CA = XElement.Parse(vDescriptivo.XML_CATALOGOS).Elements("CAMPOS_ADICIONALES").FirstOrDefault().ToString();


            if (vDescriptivo.XML_PUESTO_OCUPACION != null)
                eDescriptivo.LST_OCUPACION_PUESTO = XElement.Parse(vDescriptivo.XML_PUESTO_OCUPACION).Elements("PUESTOOCUPACION").Select(el => new E_OCUPACION_PUESTO
                {
                    ID_OCUPACION_PUESTO = UtilXML.ValorAtributo<int>(el.Attribute("ID_OCUPACION_PUESTO")),
                    ID_OCUPACION = UtilXML.ValorAtributo<int>(el.Attribute("ID_OCUPACION")),
                    NB_OCUPACION = UtilXML.ValorAtributo<string>(el.Attribute("NB_OCUPACION")),
                    CL_OCUPACION = UtilXML.ValorAtributo<string>(el.Attribute("CL_OCUPACION")),
                    NB_MODULO = UtilXML.ValorAtributo<string>(el.Attribute("NB_MODULO")),
                    CL_MODULO = UtilXML.ValorAtributo<string>(el.Attribute("CL_MODULO")),
                    NB_SUBAREA = UtilXML.ValorAtributo<string>(el.Attribute("NB_SUBAREA")),
                    CL_SUBAREA = UtilXML.ValorAtributo<string>(el.Attribute("CL_SUBAREA")),
                    NB_AREA = UtilXML.ValorAtributo<string>(el.Attribute("NB_AREA")),
                    CL_AREA = UtilXML.ValorAtributo<string>(el.Attribute("CL_AREA"))
                }).FirstOrDefault();


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

            return new E_DESCRIPTIVO
            {
                ID_PUESTO = (vDescriptivo.ID_PUESTO < 0) ? (int?)null : vDescriptivo.ID_PUESTO,
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
                DS_COMPETENCIAS_REQUERIDAS = vDescriptivo.DS_COMPETENCIAS_REQUERIDAS,
                ESTATUS = vDescriptivo.ESTATUS ,
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
                LST_INDICADORES = vCatalogoIndicadores,
                //LST_PAQUETE_PRESTACIONES = vCatalogoPaqPres
                LST_OCUPACION_PUESTO = eDescriptivo.LST_OCUPACION_PUESTO
            };

        }
        public E_DESCRIPTIVO ObtieneDescriptivo_pde(string pIdDescriptivo)
        {
            DescriptivoOperaciones nDescriptivo = new DescriptivoOperaciones();
            SPE_OBTIENE_PDE_DESCRIPTIVO_Result vDescriptivo = nDescriptivo.ObtenerDescriptivo_pde(pIdDescriptivo);

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



            return new E_DESCRIPTIVO
            {
                ID_PUESTO_PDE = vDescriptivo.ID_PUESTO,
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

        public List<SPE_OBTIENE_PUESTO_FACTOR_Result> ObtieneFactoresPuestos(int pIdPuesto)
        {
            DescriptivoOperaciones oDescriptivo = new DescriptivoOperaciones();
            return oDescriptivo.ObtenerFactoresPuestos(pIdPuesto);
        }

        public E_RESULTADO ActualizarPuestoFactor(string pXmlPuestos, string pXmlFactores, string pClUsuario, string pNbPrograma)
        {
            DescriptivoOperaciones oDescriptivo = new DescriptivoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oDescriptivo.ActualizarPuestoFactor(pXmlPuestos, pXmlFactores, pClUsuario, pNbPrograma));
        }

        public E_RESULTADO InsertaPuestoFactor(int pIdPuesto, string pClUsuario, string pNbPrograma)
        {
            DescriptivoOperaciones oDescriptivo = new DescriptivoOperaciones();
            return UtilRespuesta.EnvioRespuesta(oDescriptivo.InsertarPuestoFactor(pIdPuesto, pClUsuario, pNbPrograma));
        }
        
      
        public List<SPE_OBTIENE_DESCRIPTIVOS_PUESTOS_Result> ObtieneDescriptivosPuestos()
        {
            DescriptivoOperaciones oDescriptivo = new DescriptivoOperaciones();
            return oDescriptivo.ObtenerDescriptivosPuestos();
        }


        public List<SPE_OBTIENE_JEFES_DESCRIPTIVO_Result> ObtenerJefesDescriptivo(int? pIdPuesto)
        {
            DescriptivoOperaciones oDescriptivo = new DescriptivoOperaciones();
            return oDescriptivo.ObtenerJefesDescriptivo(pIdPuesto);
        }

        public List<SPE_OBTIENE_SUBORDINADOS_DESCRIPTIVO_Result> ObtenerSubordinadosDescriptivo(int? pIdPuesto)
        {
            DescriptivoOperaciones oDescriptivo = new DescriptivoOperaciones();
            return oDescriptivo.ObtenerSubordinadosDescriptivo(pIdPuesto);
        }

        public List<E_PUESTO_INTERRELACIONADO> ObtenerPuestosInterrelacionadas(int? pIdPuesto)
        {
            DescriptivoOperaciones oDescriptivo = new DescriptivoOperaciones();
            return oDescriptivo.ObtenerPuestosInterrelacionadas(pIdPuesto);
        }

    }
}
