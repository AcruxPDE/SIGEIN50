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


namespace SIGE.Negocio.Administracion  // reemplazar por la carpeta correspondiente
{
    public class PlantillaFormularioNegocio
    {

        public List<SPE_OBTIENE_C_PLANTILLA_FORMULARIO_Result> ObtienePlantillas(int? pIdPlantilla = null, string pNbPlantilla = null, string pDsPlantilla = null, string pClFormulario = null, bool? pFgGeneral = null, Guid? pFlPlantillaSolicitud = null)
        {
            PlantillaFormularioOperaciones oPlantilla = new PlantillaFormularioOperaciones();
            return oPlantilla.ObtenerPlantillas(pIdPlantilla, pNbPlantilla, pDsPlantilla, pClFormulario, pFgGeneral, pFlPlantillaSolicitud);
        }

        public List<SPE_OBTIENE_C_PLANTILLAS_EXTERNAS_Result> ObtenerPlantillasExternas(string pCl_Exposicion = null)
        {
            PlantillaFormularioOperaciones oPlantilla = new PlantillaFormularioOperaciones();
            return oPlantilla.ObtenerPlantillasExternas(pCl_Exposicion);
        }

        public List<SPE_OBTIENE_C_PLANTILLA_FORMULARIO_PDE_Result> ObtienePlantillasPDE(int? pIdPlantilla = null, string pNbPlantilla = null, string pDsPlantilla = null, string pClFormulario = null, bool? pFgGeneral = null)
        {
            PlantillaFormularioOperaciones oPlantilla = new PlantillaFormularioOperaciones();
            return oPlantilla.ObtenerPlantillasPDE(pIdPlantilla, pNbPlantilla, pDsPlantilla, pClFormulario, pFgGeneral);
        }

        public E_PLANTILLA ObtienePlantilla(int? pIdPlantillaFormulario, string pClFormulario)
        {
            PlantillaFormularioOperaciones oPlantilla = new PlantillaFormularioOperaciones();
            SPE_OBTIENE_PLANTILLA_FORMULARIO_Result vSPE_OBTIENE_PLANTILLA_FORMULARIO = oPlantilla.ObtenerPlantilla(pIdPlantillaFormulario, pClFormulario);

            return new E_PLANTILLA()
            {
                ID_PLANTILLA = vSPE_OBTIENE_PLANTILLA_FORMULARIO.ID_PLANTILLA_SOLICITUD,
                NB_PLANTILLA = vSPE_OBTIENE_PLANTILLA_FORMULARIO.NB_PLANTILLA_SOLICITUD,
                DS_PLANTILLA = vSPE_OBTIENE_PLANTILLA_FORMULARIO.DS_PLANTILLA_SOLICITUD,
                CL_FORMULARIO = vSPE_OBTIENE_PLANTILLA_FORMULARIO.CL_FORMULARIO,
                FG_GENERAL = vSPE_OBTIENE_PLANTILLA_FORMULARIO.FG_GENERAL,
                FL_PLANTILLA = vSPE_OBTIENE_PLANTILLA_FORMULARIO.FL_PLANTILLA,
                FG_GENERAL_CL = vSPE_OBTIENE_PLANTILLA_FORMULARIO.FG_GENERAL_CL,
                CL_EXPOSICION = vSPE_OBTIENE_PLANTILLA_FORMULARIO.CL_EXPOSICION,
                XML_PLANTILLA_FORMULARIO = vSPE_OBTIENE_PLANTILLA_FORMULARIO.XML_PLANTILLA_SOLICITUD,
                LST_CAMPOS = XElement.Parse(vSPE_OBTIENE_PLANTILLA_FORMULARIO.XML_PLANTILLA).Elements("CAMPO").Select(s => new E_CAMPO()
                {
                    ID_CAMPO = UtilXML.ValorAtributo<int?>(s.Attribute("ID_CAMPO_FORMULARIO")),
                    CL_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("CL_CAMPO_FORMULARIO")),
                    NB_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("NB_CAMPO_FORMULARIO")),
                    DS_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("NB_TOOLTIP")),
                    CL_TIPO_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("CL_TIPO_CAMPO")),
                    CL_FORMULARIO = UtilXML.ValorAtributo<string>(s.Attribute("CL_FORMULARIO")),
                    FG_ACTIVA = UtilXML.ValorAtributo<bool>(s.Attribute("FG_ACTIVO")),
                    FG_SISTEMA = UtilXML.ValorAtributo<bool>(s.Attribute("FG_SISTEMA")),
                    FG_HABILITADO = UtilXML.ValorAtributo<bool>(s.Attribute("FG_HABILITADO")),
                    FG_REQUERIDO = UtilXML.ValorAtributo<bool>(s.Attribute("FG_REQUERIDO")),
                    CL_CONTENEDOR = UtilXML.ValorAtributo<string>(s.Attribute("CL_CONTENEDOR")),
                    NO_ORDEN = (byte)UtilXML.ValorAtributo<int>(s.Attribute("NO_ORDEN")),
                    XML_CAMPO = ((s.Element("XML_CAMPO") ?? new XElement("XML_CAMPO")).Element("CAMPO") ?? new XElement("CAMPO"
                        //, new XAttribute("CL_TIPO", UtilXML.ValorAtributo<string>(s.Attribute("CL_TIPO_CAMPO")))
                    )).ToString()
                }).ToList()
            };

            //vPlantilla.LST_CAMPOS = XElement.Parse(vSPE_OBTIENE_PLANTILLA_FORMULARIO.XML_PLANTILLA).Elements("CAMPO").Select(s => new E_CAMPO()
            //{
            //    ID_CAMPO = UtilXML.ValorAtributo<int?>(s.Attribute("ID_CAMPO_FORMULARIO")),
            //    CL_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("CL_CAMPO_FORMULARIO")),
            //    NB_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("NB_CAMPO_FORMULARIO")),
            //    DS_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("NB_TOOLTIP")),
            //    CL_TIPO_CAMPO = UtilXML.ValorAtributo<string>(s.Attribute("CL_TIPO_CAMPO")),
            //    CL_FORMULARIO = UtilXML.ValorAtributo<string>(s.Attribute("CL_FORMULARIO")),
            //    FG_ACTIVA = UtilXML.ValorAtributo<bool>(s.Attribute("FG_ACTIVO")),
            //    FG_SISTEMA = UtilXML.ValorAtributo<bool>(s.Attribute("FG_SISTEMA")),
            //    FG_HABILITADO = UtilXML.ValorAtributo<bool>(s.Attribute("FG_HABILITADO")),
            //    FG_REQUERIDO = UtilXML.ValorAtributo<bool>(s.Attribute("FG_REQUERIDO")),
            //    CL_CONTENEDOR = UtilXML.ValorAtributo<string>(s.Attribute("CL_CONTENEDOR")),
            //    XML_CAMPO = ((s.Element("XML_CAMPO") ?? new XElement("XML_CAMPO")).Element("CAMPO")?? new XElement("CAMPO")).ToString()
            //}).ToList();

            //return vPlantilla;
        }
             
        public E_RESULTADO InsertaActualizaPlantillaFormulario(string pClAccion, E_PLANTILLA pPlantillaFormulario, string pClUsuario, string pNbPrograma)
        {


            XElement vLstCampos = new XElement("CAMPOS");

            int vNoOrden = 1;
            foreach (E_CAMPO campo in pPlantillaFormulario.LST_CAMPOS)
            {
                XElement vXmlCampoFormulario = new XElement("CAMPO_FORMULARIO",
                    new XAttribute("ID_CAMPO_FORMULARIO", campo.ID_CAMPO),
                    new XAttribute("CL_CONTENEDOR", campo.CL_CONTENEDOR),
                    new XAttribute("FG_HABILITADO", campo.FG_HABILITADO),
                    new XAttribute("FG_REQUERIDO", campo.FG_REQUERIDO),
                    new XAttribute("NO_ORDEN", vNoOrden++),
                    XElement.Parse(campo.XML_CAMPO));
                vLstCampos.Add(vXmlCampoFormulario);
            }
           
            PlantillaFormularioOperaciones oPlantilla = new PlantillaFormularioOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPlantilla.InsertarActualizarPlantillaFormulario(pClAccion, pPlantillaFormulario, vLstCampos, pClUsuario, pNbPrograma));
        }
              
        public E_RESULTADO EliminaPlantillaFormulario(int pIdPlantillaFormulario, string pClUsuario, string pNbPrograma)
        {
            PlantillaFormularioOperaciones oPlantilla = new PlantillaFormularioOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPlantilla.EliminarPlantillaFormulario(pIdPlantillaFormulario, pClUsuario, pNbPrograma));
        }
        
        public E_RESULTADO EstablecerPlantillaPorDefecto(int pIdPlantillaFormulario, string pClTipoFormulario, string pClUsuario, string pNbPrograma)
        {
            PlantillaFormularioOperaciones oPlantilla = new PlantillaFormularioOperaciones();
            return UtilRespuesta.EnvioRespuesta(oPlantilla.EstablecerPlantillaPorDefecto(pIdPlantillaFormulario, pClTipoFormulario, pClUsuario, pNbPrograma));
        }
    }
}
