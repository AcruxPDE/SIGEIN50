using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.IDP.Pruebas
{
    public partial class Default : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario = string.Empty;
        private string vNbPrograma = string.Empty;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdBateria
        {
            get { return (int)ViewState["vsIdBateria"]; }
            set { ViewState["vsIdBateria"] = value; }
        }

        private Guid vClToken
        {
            get { return (Guid)ViewState["vsClToken"]; }
            set { ViewState["vsClToken"] = value; }
        }

        private string vClEstadoPrueba
        {
            get { return (string)ViewState["vsClEstadoPrueba"]; }
            set { ViewState["vsClEstadoPrueba"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (ContextoUsuario.idBateriaPruebas == 0 && ContextoUsuario.clTokenPruebas == Guid.Empty)
            {
                if (Request.QueryString["ID"] != null && Request.QueryString["T"] != null)
                {
                    FormsAuthentication.SignOut();
                    vIdBateria = int.Parse(Request.QueryString["ID"]);
                    vClToken = Guid.Parse(Request.QueryString["T"].ToString());
                    ContextoUsuario.idBateriaPruebas = vIdBateria;
                    ContextoUsuario.clTokenPruebas = vClToken;

                    vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
                    vNbPrograma = ContextoUsuario.nbPrograma;
                    //txtMensajeDespedida.InnerHtml = ContextoApp.IDP.MensajeDespedidaPrueba.dsMensaje;
                }
            }

            if (Request.QueryString["ty"] != null)
            {
                vClEstadoPrueba = Request.QueryString["ty"].ToString();
                ContextoUsuario.clEstadoPruebas = vClEstadoPrueba;
            }

            if (Request.QueryString["F"] != null)
            {
                vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
                vNbPrograma = ContextoUsuario.nbPrograma;
                vIdBateria = ContextoUsuario.idBateriaPruebas;
                generarBaremos();

                //Observacion: Solucion al bug al terminar una bateria y querer empezar otra, no deja iniciarla
 
                ContextoUsuario.idBateriaPruebas = 0;
                ContextoUsuario.clTokenPruebas = Guid.Empty;
                ContextoUsuario.clEstadoPruebas =string.Empty;
            }

            if (!Page.IsPostBack)
            {
                txtMensajeDespedida.InnerHtml = ContextoApp.IDP.MensajeDespedidaPrueba.dsMensaje;
            }
            
        }

        private void generarBaremos()
        {
            PruebasNegocio neg = new PruebasNegocio();

            E_RESULTADO vResultado = neg.generaVariablesBaremos(vIdBateria, vClUsuario, vNbPrograma);

            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR)
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);                
            }            
        }

    }
}