using Org.BouncyCastle.Asn1.Ocsp;
using SIGE.Entidades.Administracion;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace SIGE.WebApp.PDE
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class HandlerPDE : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private const int TICKET_VERSION = 1;


        public void ProcessRequest(HttpContext context)
        {

            HttpCookie usuario = HttpContext.Current.Request.Cookies["loginInfo"];
            if (usuario != null)
            {
            ContextoPDENegocio negocio = new ContextoPDENegocio();
            E_USUARIO vUsuario = negocio.AutenticaUsuario(usuario.Values[1].ToString());//usuario.Values[1].ToString()

            if (vUsuario.FG_ACTIVO == true)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(TICKET_VERSION, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddMinutes(FormsAuthentication.Timeout.TotalMinutes), false, string.Empty, FormsAuthentication.FormsCookiePath);
                string cookie = FormsAuthentication.Encrypt(ticket);
                context.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, cookie));
                context.Session["UniqueUserId"] = Guid.NewGuid();
                ContextoUsuario.oUsuario = vUsuario;
                
                    context.Response.Redirect("VentanaInicioPDE.aspx");
                
                
            }
            else
            {
                context.Response.Redirect(ConfigurationManager.AppSettings["siteSIGEIN"].ToString());
            }
           }
            else
            {
                context.Response.Redirect(ConfigurationManager.AppSettings["siteSIGEIN"].ToString());
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}