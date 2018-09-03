using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Externas;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{

    public class PlantillaFormularioOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE
        public List<SPE_OBTIENE_C_PLANTILLA_FORMULARIO_Result> ObtenerPlantillas(int? pIdPlantillaSolicitud = null, string pNbPlantillaSolicitud = null, string pDsPlantillaSolicitud = null, string pClFormulario = null, bool? pFgGeneral = null, Guid? pFlPlantillaSolicitud = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_PLANTILLA_FORMULARIO(pIdPlantillaSolicitud, pNbPlantillaSolicitud, pDsPlantillaSolicitud, pClFormulario, pFgGeneral, pFlPlantillaSolicitud).ToList();
            }
        }


        public List<SPE_OBTIENE_C_PLANTILLAS_EXTERNAS_Result> ObtenerPlantillasExternas(string pCl_Exposicion = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_PLANTILLAS_EXTERNAS(pCl_Exposicion).ToList();
            }
        }

        public List<SPE_OBTIENE_C_PLANTILLA_FORMULARIO_PDE_Result> ObtenerPlantillasPDE(int? pIdPlantillaSolicitud = null, string pNbPlantillaSolicitud = null, string pDsPlantillaSolicitud = null, string pClFormulario = null, bool? pFgGeneral = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_C_PLANTILLA_FORMULARIO_PDE(pIdPlantillaSolicitud, pNbPlantillaSolicitud, pDsPlantillaSolicitud, pClFormulario, pFgGeneral).ToList();
            }
        }

        public SPE_OBTIENE_PLANTILLA_FORMULARIO_Result ObtenerPlantilla(int? pIdPlantillaFormulario, string pClFormulario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PLANTILLA_FORMULARIO(pIdPlantillaFormulario, pClFormulario).ToList().FirstOrDefault();
            }
        }
        #endregion

        #region INSERTA ACTUALIZA DATOS  C_PLANTILLA_FORMULARIO
        public XElement InsertarActualizarPlantillaFormulario(string pClAccion, E_PLANTILLA pPlantillaFormulario, XElement pXmlCamposFormulario, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_C_PLANTILLA_FORMULARIO(pOutClRetorno, pPlantillaFormulario.ID_PLANTILLA, pPlantillaFormulario.NB_PLANTILLA, pPlantillaFormulario.DS_PLANTILLA, pPlantillaFormulario.CL_FORMULARIO, pXmlCamposFormulario.ToString(), pPlantillaFormulario.XML_PLANTILLA_FORMULARIO,  pPlantillaFormulario.XML_CAMPOS, pPlantillaFormulario.CL_EXPOSICION, pClUsuario, pNbPrograma, pClAccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString()); 
            }
        }
        #endregion

        #region ELIMINA DATOS  C_PLANTILLA_FORMULARIO
        public XElement EliminarPlantillaFormulario(int pIdPlantillaFormulario, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_C_PLANTILLA_FORMULARIO(pOutClRetorno, pIdPlantillaFormulario, pClUsuario, pNbPrograma);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
        #endregion

        public XElement EstablecerPlantillaPorDefecto(int pIdPlantillaFormulario, string pClTipoFormulario, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ESTABLECE_PLANTILLA_FORMULARIO_GENERAL(pOutClRetorno, pIdPlantillaFormulario, pClTipoFormulario, pClUsuario, pNbPrograma);
                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
    }
}