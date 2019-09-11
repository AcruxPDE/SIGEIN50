using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using System.Net;
using System.Xml.Linq;


namespace SIGE.WebApp.PDE
{
    public partial class CambiarPassword : System.Web.UI.Page
    {
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement SELECCIONUSUARIOS { get; set; }
        #region Variables

        public string vNbUsuario
        {
            get { return (string)ViewState["vs_vNbUsuario"]; }
            set { ViewState["vs_vNbUsuario"] = value; }
        }

        public string vClUsuario
        {
            get { return (string)ViewState["vs_vClUsuario"]; }
            set { ViewState["vs_vClUsuario"] = value; }
        }

        public string vNbCorreo
        {
            get { return (string)ViewState["vs_vNbCorreo"]; }
            set { ViewState["vs_vNbCorreo"] = value; }
        }

        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            vNbUsuario = ContextoUsuario.oUsuario.NB_USUARIO;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbCorreo = ContextoUsuario.oUsuario.NB_CORREO_ELECTRONICO;
            vClOperacion = E_TIPO_OPERACION_DB.A;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo != null)
                rbiLogoOrganizacion1.DataValue = ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo;
            else
                dvLogo.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool vFgCoincidenPasswords = txtNbPassword.Text == txtNbPasswordConfirm.Text;
            bool vFgPasswordVacio = String.IsNullOrEmpty(txtNbPassword.Text);


            E_USUARIO vUsuario = new E_USUARIO();
            vUsuario.CL_USUARIO = vClUsuario;
            vUsuario.FG_CAMBIAR_PASSWORD = true;//vClOperacion.Equals(E_TIPO_OPERACION_DB.A) && chkPasswordChange.Checked;
            vUsuario.NB_PASSWORD = txtNbPassword.Text;
            vUsuario.NB_CORREO_ELECTRONICO = vNbCorreo;
            vUsuario.FG_ACTIVO = true;
            vUsuario.CONTRASENA = txtNbPassword.Text;


            
            UsuarioNegocio nUsuario = new UsuarioNegocio();

            E_RESULTADO vResultado = nUsuario.ActualizaUsuarioPDE(vClOperacion, vUsuario, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 180);


            //AGREGAR INSERCION A CORREO_PDE
            string correo = vUsuario.NB_CORREO_ELECTRONICO;
            string vUrls = WebUtility.HtmlEncode("Estimado(a): Colaborador." +
                                "El usuario y contraseña para ingresar aL sistema SIGEIN son los siguientes:" +
                                "  Usuario: " + vUsuario.CL_USUARIO + " Contraseña: " + vUsuario.CONTRASENA +
                               "Saludos.");

            bool vEstatusCorreo = EnvioCorreo(correo, vUrls, "Usuario y contraseña para acceder a Punto de encuentro");
            E_RESULTADO vResultadoCorreo;
            string vMensajeCorreo;
            bool fgEnviado;

            if (vEstatusCorreo)
            {
                fgEnviado = true;
            }
            else
            {

                fgEnviado = false;
            }

            var vXelements =
                   new XElement("USUARIO",
                    new XAttribute("CL_USUARIO", vUsuario.CL_USUARIO),
                    new XAttribute("FE_ENVIO", DateTime.Now),
                    new XAttribute("FG_ENVIO", fgEnviado)
                            );

            SELECCIONUSUARIOS =
                                 new XElement("SELECCION", vXelements
                                 );
            vResultadoCorreo = nUsuario.InsertarUsuarioCorreo(SELECCIONUSUARIOS.ToString(), vClUsuario, vNbPrograma, vClOperacion.ToString());
            vMensajeCorreo = vResultadoCorreo.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensajeCorreo, vResultadoCorreo.CL_TIPO_ERROR);

            redireccionar();
        }

        public bool EnvioCorreo(string Email, string Mensaje, string Asunto)
        {
            Mail mail = new Mail(ContextoApp.mailConfiguration);
            bool resultado;

            try
            {
                mail.addToAddress(Email, Mensaje);
                mail.Send(Asunto, Mensaje);
                resultado = true;
            }
            catch (Exception)
            {
                resultado = false;
            }

            return resultado;


        }

        public void redireccionar()
        {

            Response.Redirect("~/PDE/Default.aspx");
        }
    }
}