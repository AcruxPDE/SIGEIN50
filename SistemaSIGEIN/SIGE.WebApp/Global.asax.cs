using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

using SIGE.WebApp.QUARTZ;

namespace SIGE.WebApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            JobScheduler.IniciaEnvioCorreos();
            JobScheduler.IniciaVerificaCorreos();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)

        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }



        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)   
        {
            //Only access session state if it is available
            if (Context.Handler is IRequiresSessionState || Context.Handler is IReadOnlySessionState)
            {
                //If we are authenticated AND we dont have a session here.. redirect to login page.
                HttpCookie authenticationCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authenticationCookie != null)
                {
                    FormsAuthenticationTicket authenticationTicket = FormsAuthentication.Decrypt(authenticationCookie.Value);
                    if (!authenticationTicket.Expired)
                    {
                        //of course.. replace ANYKNOWNVALUEHERETOCHECK with "UserId" or something you set on the login that you can check here to see if its empty.
                        if (Session["__oUsuario__"] == null)
                        {
                            //This means for some reason the session expired before the authentication ticket. Force a login.
                           // Response.Write("Hola");
                            FormsAuthentication.SignOut();
                            Response.Redirect(FormsAuthentication.LoginUrl, true);
                            return;
                        }
                    }
                }
            }
        }
    }
}