using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaEdicionDeTramites : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_FORMATO_TRAMITE> vTramite
        {
            get { return (List<E_FORMATO_TRAMITE>)ViewState["vs_vTramite"]; }
            set { ViewState["vs_vTramite"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
         

        }

        protected void grdTramites_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result> ListaTramites = new List<SPE_OBTIENE_FORMATOS_Y_TRAMITES_Result>();
            FormatosYTramitesNegocio negocio = new FormatosYTramitesNegocio();
            ListaTramites = negocio.OBTENER_FORMATOS_Y_TRAMITES(null, "Trámite", true);
            rgTramites.DataSource = ListaTramites;
        }

        public void DeserializarDocumentoAutorizar(XElement tablas)
        {
            if (ValidarRamaXml(tablas, "FORMATO_XML"))
            {
                vTramite = tablas.Element("FORMATO_XML").Elements("DOCUMENTO").Select(el => new E_FORMATO_TRAMITE
                {
                    XML_FORMATO_TRAMITE = el.Attribute("XML_FORMATO_TRAMITE").Value
                }).ToList();
            }
        }


        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);

            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            FormatosYTramitesNegocio nConfiguracion = new FormatosYTramitesNegocio();
            GridDataItem item = rgTramites.SelectedItems[0] as GridDataItem;
            int idFormato = int.Parse(item.GetDataKeyValue("ID_FORMATO_TRAMITE").ToString());
            E_RESULTADO vResultado = nConfiguracion.ELIMINA_FORMATOS_Y_TRAMITES(idFormato, null, false, vClUsuario, vNbPrograma, "A");
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");
            rgTramites.Rebind();
        }
    }
}