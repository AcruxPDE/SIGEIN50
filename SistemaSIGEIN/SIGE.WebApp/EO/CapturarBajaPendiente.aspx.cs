using SIGE.Entidades;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO
{
    public partial class CapturarBajaPendiente : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        private int? vIdBajaEmpleado
        {
            get { return (int?)ViewState["vs_vIdEvaluadoPeriodo"]; }
            set { ViewState["vs_vIdEvaluadoPeriodo"] = value; }
        }

        private int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        public int? vIdCausaBaja
        {
            get { return (int?)ViewState["vs_vIdCausaBaja"]; }
            set { ViewState["vs_vIdCausaBaja"] = value; }
        }

        public int? vIdCatalogoBaja
        {
            get { return (int?)ViewState["vs_vIdCatalogoBaja"]; }
            set { ViewState["vs_vIdCatalogoBaja"] = value; }
        }

        #endregion

        #region Funciones

        private XElement EditorContentToXml(string pNbNodoRaiz, string pDsContenido, string pNbTag)
        {
            return XElement.Parse(EncapsularRadEditorContent(XElement.Parse(String.Format("<{1}>{0}</{1}>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(pDsContenido)), pNbNodoRaiz)), pNbNodoRaiz));
        }

        private string EncapsularRadEditorContent(XElement nodo, string nbNodo)
        {
            if (nodo.Elements().Count() == 1)
                return EncapsularRadEditorContent((XElement)nodo.FirstNode, nbNodo);
            else
            {
                nodo.Name = nbNodo;
                return nodo.ToString();
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["pIdEmpleadoBaja"] != null)
                {
                    vIdBajaEmpleado = int.Parse(Request.Params["pIdEmpleadoBaja"].ToString());
                    vIdEmpleado = int.Parse(Request.Params["pIdEmpleado"].ToString());
                    RotacionPersonalNegocio nRotacion = new RotacionPersonalNegocio();
                    var vEmpleadoBaja = nRotacion.ObtieneBajasPendientes(vIdBajaEmpleado).FirstOrDefault();
                    txtNbEmpleado.InnerText = vEmpleadoBaja.CL_EMPLEADO + " - " + vEmpleadoBaja.NB_EMPLEADO;
                    txtNbPuesto.InnerText = vEmpleadoBaja.CL_PUESTO + " - " + vEmpleadoBaja.NB_PUESTO;
                    rdpFechaBaja.SelectedDate = vEmpleadoBaja.FE_BAJA_EFECTIVA;
                    XElement xmlComentarios = XElement.Parse(vEmpleadoBaja.DS_COMENTARIOS);
                    reComentarios.Content = xmlComentarios.Value;
                    CatalogoListaNegocio negocio = new CatalogoListaNegocio();
                    SPE_OBTIENE_C_CATALOGO_LISTA_Result vCatalogo = new SPE_OBTIENE_C_CATALOGO_LISTA_Result();
                    vCatalogo =negocio.ObtieneCatalogoLista().Where(w=>w.NB_CATALOGO_LISTA== "Causas de baja").FirstOrDefault();
                    vIdCatalogoBaja = vCatalogo.ID_CATALOGO_LISTA;
                }
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            DateTime? vFeBaja = rdpFechaBaja.SelectedDate;
            XElement nodoPrincipal = new XElement("XML_COMENTARIOS", EditorContentToXml("COMENTARIOS", reComentarios.Content.Replace("&lt;", ""), vNbFirstRadEditorTagName));
            string vDesComentarios = nodoPrincipal.ToString();

            foreach (RadListBoxItem item in lstCausaBaja.Items)
            {
                vIdCausaBaja = int.Parse(item.Value);
            }
            if (vIdCausaBaja != null)
            {
                if (rdpFechaBaja.SelectedDate != null)
                {
                    RotacionPersonalNegocio nBaja = new RotacionPersonalNegocio();
                    E_RESULTADO vResultado = nBaja.ActualizaBajaPendiente(vIdBajaEmpleado, vIdEmpleado, vIdCausaBaja, vDesComentarios, vFeBaja, vClUsuario, vNbPrograma, pTIPO_TRANSACCION: E_TIPO_OPERACION_DB.I.ToString());
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione la fecha de baja.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione la causa de la baja.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }

        }
    }
}