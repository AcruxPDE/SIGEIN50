using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
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
using System.Net;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaUsuarios : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement SELECCIONUSUARIOS { get; set; }

        private string vClUser
        {
            get { return (ViewState["vs_vClUser"] != null) ? ViewState["vs_vClUser"].ToString() : String.Empty; }
            set { ViewState["vs_vClUser"] = value; }
        }

        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClOperacion = E_TIPO_OPERACION_DB.I;
                if (!String.IsNullOrEmpty(Request.QueryString["UsuarioId"]))
                {
                    vClUser = Request.QueryString["UsuarioId"];
                    vClOperacion = E_TIPO_OPERACION_DB.A;
                }

                CargarDatos(vClUser);
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void CargarDatos(string pIdUsuario)
        {
            UsuarioNegocio nUsuario = new UsuarioNegocio();
            E_USUARIO vUsuario = nUsuario.ObtieneUsuario(pIdUsuario);

            string vChangePasswordStyle = "block";
            string vPasswordStyle = "none";

            bool vEsInsercion = vClOperacion == E_TIPO_OPERACION_DB.I;

            if (vEsInsercion)
            {
                vChangePasswordStyle = "none";
                vPasswordStyle = "block";
            }

            txtClUsuario.ReadOnly = !vEsInsercion;
            chkPasswordChange.Checked = vEsInsercion;
            ctrlPasswordChange.Style.Value = String.Format("display:{0};", vChangePasswordStyle);
            ctrlPassword.Style.Value = String.Format("display:{0};", vPasswordStyle);

            txtClUsuario.Text = vUsuario.CL_USUARIO;
            txtNbUsuario.Text = vUsuario.NB_USUARIO;
            txtNbCorreoElectronico.Text = vUsuario.NB_CORREO_ELECTRONICO;
            chkActivo.Checked = vUsuario.FG_ACTIVO;

            cmbRol.Items.AddRange(vUsuario.XML_CATALOGOS.Element("ROLES").Elements("ROL").Select(s => new RadComboBoxItem(s.Attribute("NB_ROL").Value, s.Attribute("ID_ROL").Value)
            {
                Selected = UtilXML.ValorAtributo<bool>(s.Attribute("FG_SELECCIONADO"))
            }));

            cmbTipoMultiempresa.Items.AddRange(vUsuario.XML_CATALOGOS.Element("MULTIEMPRESAS").Elements("MULTIEMPRESA").Select(s => new RadComboBoxItem(s.Attribute("NB_TIPO_MULTIEMPRESAS").Value, s.Attribute("CL_TIPO_MULTIEMPRESAS").Value)
            {
                Selected = UtilXML.ValorAtributo<bool>(s.Attribute("FG_SELECCIONADO"))
            }));



            RadListBoxItem vItmEmpleado = new RadListBoxItem("No seleccionado", String.Empty);
            XElement vEmpleados = vUsuario.XML_CATALOGOS.Element("EMPLEADOS");
            if (vEmpleados != null)
            {
                XElement vEmpleado = vEmpleados.Element("EMPLEADO");
                if (vEmpleado != null)
                    vItmEmpleado = new RadListBoxItem(vEmpleado.Attribute("NB_EMPLEADO").Value, vEmpleado.Attribute("ID_EMPLEADO").Value);
            }
            lstEmpleado.Items.Add(vItmEmpleado);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool vFgCoincidenPasswords = txtNbPassword.Text == txtNbPasswordConfirm.Text;
            bool vFgPasswordVacio = String.IsNullOrEmpty(txtNbPassword.Text);
            int vIdRol = 0;
            int vIdEmpleado = 0;

            E_USUARIO vUsuario = new E_USUARIO();
            vUsuario.CL_USUARIO = txtClUsuario.Text;
            vUsuario.NB_USUARIO = txtNbUsuario.Text;
            vUsuario.FG_CAMBIAR_PASSWORD = vClOperacion.Equals(E_TIPO_OPERACION_DB.A) && chkPasswordChange.Checked;
            vUsuario.NB_PASSWORD = txtNbPassword.Text;
            vUsuario.NB_CORREO_ELECTRONICO = txtNbCorreoElectronico.Text;
            vUsuario.FG_ACTIVO = chkActivo.Checked;
            vUsuario.CL_TIPO_MULTIEMPRESA = cmbTipoMultiempresa.SelectedValue;

            foreach (RadListBoxItem item in lstEmpleado.Items)
                if (int.TryParse(item.Value, out vIdEmpleado))
                    vUsuario.ID_EMPLEADO = vIdEmpleado;

            if (int.TryParse(cmbRol.SelectedValue, out vIdRol))
                vUsuario.ID_ROL = vIdRol;
            else
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Debes selecionar un rol.", E_TIPO_RESPUESTA_DB.WARNING);

            if (!vFgCoincidenPasswords)
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Las contraseñas no coinciden.", E_TIPO_RESPUESTA_DB.WARNING);

            if (vFgPasswordVacio)
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "La contraseña es requerida.", E_TIPO_RESPUESTA_DB.WARNING);

            UsuarioNegocio nUsuario = new UsuarioNegocio();

            E_RESULTADO vResultado = nUsuario.InsertaActualizaUsuario(vClOperacion, vUsuario, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 180);


            //AGREGAR INSERCION A CORREO_PDE
            string correo = vUsuario.NB_CORREO_ELECTRONICO;
            string vUrls = WebUtility.HtmlEncode("<p>Estimado(a): Colaborador." +
                                "El usuario y contraseña para ingresar aL sistema SIGEIN son los siguientes:" +
                                "  Usuario: " + vUsuario.CL_USUARIO + " Contraseña: " + vUsuario.CONTRASENA+ 
                               "Saludos.<p>");

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
                                vResultadoCorreo = nUsuario.InsertarUsuarioCorreo(SELECCIONUSUARIOS.ToString(), vClUsuario, vNbPrograma,vClOperacion.ToString());
                                vMensajeCorreo = vResultadoCorreo.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensajeCorreo, vResultadoCorreo.CL_TIPO_ERROR);

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
    }
}