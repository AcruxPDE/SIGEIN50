using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaUsuarios : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private string vClCliente;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement SELECCIONEMPLEADOS { get; set; }
        private XElement SELECCIONUSUARIOS { get; set; }
        StringBuilder builder = new StringBuilder();
        string Email { set; get; }

        private string vClUser
        {
            get { return (ViewState["vs_vClUser"] != null) ? ViewState["vs_vClUser"].ToString() : String.Empty; }
            set { ViewState["vs_vClUser"] = value; }
        }
        private string vActivo
        {
            get { return (ViewState["vs_vActivo"] != null) ? ViewState["vs_vActivo"].ToString() : String.Empty; }
            set { ViewState["vs_vActivo"] = value; }
        }
        private string vClaveUsuario
        {
            get { return (ViewState["vs_vClaveUsuario"] != null) ? ViewState["vs_vClaveUsuario"].ToString() : String.Empty; }
            set { ViewState["vs_vClaveUsuario"] = value; }
        }

        private string vEditar
        {
            get { return (ViewState["vs_vEditar"] != null) ? ViewState["vs_vEditar"].ToString() : String.Empty; }
            set { ViewState["vs_vEditar"] = value; }
        }
        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }

        private string vIdEmpleado
        {
            get { return (ViewState["vs_vIdEmpleado"] != null) ? ViewState["vs_vIdEmpleado"].ToString() : String.Empty; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        private List<SPE_OBTIENE_EMPLEADOS_GENERA_CONTRASENA_Result> vListaEmpleados
        {
            get { return (List<SPE_OBTIENE_EMPLEADOS_GENERA_CONTRASENA_Result>)ViewState["vs_vListaEmpleados"]; }
            set { ViewState["vs_vListaEmpleados"] = value; }
        }

        private List<E_OBTIENE_EMPLEADOS_GENERA_CONTRASENA> vListaEUsuarios
        {
            get { return (List<E_OBTIENE_EMPLEADOS_GENERA_CONTRASENA>)ViewState["vs_vListaEUsuarios"]; }
            set { ViewState["vs_vListaEUsuarios"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                vClOperacion = E_TIPO_OPERACION_DB.I;
                if (!String.IsNullOrEmpty(Request.QueryString["UsuarioId"]))
                {
                    vClUser = Request.QueryString["UsuarioId"];
                    vActivo = Request.QueryString["Activo"];
                    vEditar = Request.QueryString["Editar"];
                    vClOperacion = E_TIPO_OPERACION_DB.A;
                }
                else
                {
                    vEditar = Request.QueryString["Crear"];
                }
                CargarDatos(vClUser);
                ObtenerUsuarios();
                mpUsuarios.SelectedIndex = 0;

            }

            if (vEditar == "true")
            {
                cmbUsuarios.Visible = false;
                cmbUsuariosNl.Visible = false;
                txtClUsuario.Visible = true;
                rbLigado.Visible = false;
                rbNoLigado.Visible = false;

            }
            else
            {
                if (rbLigado.Checked == true)
                {
                    cmbUsuarios.Visible = true;
                    cmbUsuariosNl.Visible = false;
                }
                else
                {
                    cmbUsuariosNl.Visible = true;
                    cmbUsuarios.Visible = false;
                }

                txtClUsuario.Visible = false;
                rbLigado.Visible = true;
                rbNoLigado.Visible = true;
            }

            if (vActivo == "No")
            {
                chkActivo.Enabled = false;
                btnGuardar.Enabled = false;

            }
            else
            {
                chkActivo.Enabled = true;
                btnGuardar.Enabled = true;
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClCliente = ContextoApp.Licencia.clCliente;
        }
        public void ObtenerUsuarios()
        {
            bool vLigado;
            if (rbLigado.Checked == true)
            {
                vLigado = true;
            }
            else { vLigado = false; }
            UsuarioNegocio nUsuarios = new UsuarioNegocio();
            List<SPE_OBTIENE_USUARIOS_EMPLEADOS_Result> vListaUsuarios = new List<SPE_OBTIENE_USUARIOS_EMPLEADOS_Result>();
            vListaUsuarios = nUsuarios.ObtieneUsuariosActivos(null, vLigado);
            if (rbLigado.Checked == true)
            {
                cmbUsuarios.DataSource = vListaUsuarios.ToList();
                cmbUsuarios.DataTextField = "CL_USUARIO";
                cmbUsuarios.DataValueField = "CL_USUARIO";
                cmbUsuarios.DataBind();
            }
            else
            {
                cmbUsuariosNl.DataSource = vListaUsuarios.ToList();
                cmbUsuariosNl.DataTextField = "CL_USUARIO";
                cmbUsuariosNl.DataValueField = "CL_USUARIO";
                cmbUsuariosNl.DataBind();
            }
        }

        protected void CargarDatos(string pIdUsuario)
        {
            UsuarioNegocio nUsuario = new UsuarioNegocio();
            //  E_USUARIO vUsuario = nUsuario.ObtieneUsuario(pIdUsuario);
            E_USUARIO vUsuario = nUsuario.ObtieneUsuarioPde(pIdUsuario);
            bool vEsInsercion = vClOperacion == E_TIPO_OPERACION_DB.I;

            if (vEsInsercion)
            {
            }
            if (vEditar == "true")
            {
                txtClUsuario.ReadOnly = !vEsInsercion;
                txtNbUsuario.ReadOnly = !vEsInsercion;
                txtClUsuario.Text = vUsuario.CL_USUARIO;
                txtContrasena.Text = vUsuario.CONTRASENA;
            }

            vIdEmpleado = vUsuario.ID_EMPLEADO_PDE;
            txtNbUsuario.Text = vUsuario.NB_USUARIO;
            txtNbCorreoElectronico.Text = vUsuario.NB_CORREO_ELECTRONICO;
            chkActivo.Checked = vUsuario.FG_ACTIVO;

            cmbRol.Items.AddRange(vUsuario.XML_CATALOGOS.Element("ROLES").Elements("ROL").Select(s => new RadComboBoxItem(s.Attribute("NB_ROL").Value, s.Attribute("ID_ROL").Value)
            {
                Selected = UtilXML.ValorAtributo<bool>(s.Attribute("FG_SELECCIONADO"))
            }));

        }
        protected void CargarDatosNuevo(string pIdUsuario)
        {

            UsuarioNegocio nUsuario = new UsuarioNegocio();
            E_USUARIO vUsuario = nUsuario.ObtieneUsuarioNuevo(pIdUsuario);
            vIdEmpleado = vUsuario.ID_EMPLEADO_PDE;
            bool vEsInsercion = vClOperacion == E_TIPO_OPERACION_DB.I;

            if (vEsInsercion)
            {
            }
            if (vEditar == "true")
            {

                txtClUsuario.ReadOnly = !vEsInsercion;
                txtClUsuario.Text = vUsuario.CL_USUARIO;
                txtContrasena.Text = vUsuario.CONTRASENA;
            }

            txtNbUsuario.Text = vUsuario.NB_USUARIO;
            txtNbCorreoElectronico.Text = vUsuario.NB_CORREO_ELECTRONICO;
            txtContrasena.Text = vUsuario.CONTRASENA;


        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int vIdRol = 0;


            E_USUARIO vUsuario = new E_USUARIO();
            bool vEsInsercion = vClOperacion == E_TIPO_OPERACION_DB.I;

            if (vEditar == "false")
            {
                vClaveUsuario = cmbUsuarios.SelectedValue.ToString();
                vUsuario.CL_USUARIO = vClaveUsuario;
                vClaveUsuario = cmbUsuariosNl.SelectedValue.ToString();
                vUsuario.CL_USUARIO = vClaveUsuario;
            }
            else
            {
                vUsuario.NB_PASSWORD = txtContrasena.Text;
                vUsuario.CL_USUARIO = txtClUsuario.Text;
            }
            if (cmbUsuarios.Visible == true && cmbUsuarios.SelectedValue != "") 
          
            {
                vUsuario.CL_USUARIO = cmbUsuarios.SelectedItem.Text;
            }
            if 
               (cmbUsuariosNl.Visible == true && cmbUsuariosNl.SelectedValue != "")
            {
                vUsuario.CL_USUARIO = cmbUsuariosNl.SelectedItem.Text;
            }
            vUsuario.NB_USUARIO = txtNbUsuario.Text;
            vUsuario.NB_CORREO_ELECTRONICO = txtNbCorreoElectronico.Text;
            vUsuario.FG_ACTIVO = chkActivo.Checked;
            vUsuario.ID_EMPLEADO_PDE = vIdEmpleado;
            vUsuario.CONTRASENA = txtContrasena.Text;
            string correo = txtNbCorreoElectronico.Text;
            string vUrl = ContextoUsuario.nbHost + "/InicioApp.aspx";

            if (int.TryParse(cmbRol.SelectedValue, out vIdRol))
                vUsuario.ID_ROL = vIdRol;
            else
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Debes seleccionar un rol.", E_TIPO_RESPUESTA_DB.WARNING);

            UsuarioNegocio nUsuario = new UsuarioNegocio();
            if (txtNbUsuario.Text != "")
            {
                if (txtNbCorreoElectronico.Text != "")
                {
                    E_RESULTADO vResultado = nUsuario.InsertaActualizaUsuario_pde(vClOperacion, vUsuario, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    if (correo != "" && correo != null)
                    {
                        if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                        {
                            if (vUsuario.FG_ACTIVO == true)
                            {
                                UsuarioNegocio negocio = new UsuarioNegocio();
                                SPE_OBTIENE_PDE_CONFIGURACION_URL_Result vConfiguracion;
                                vConfiguracion = negocio.ObtenerConfiguracionPDE().FirstOrDefault();
                                string url = vConfiguracion.URL != null ?  vConfiguracion.URL : "";
                                string bd = vConfiguracion.BASEDATOS != null ? vConfiguracion.BASEDATOS : "";
                                string vUrls = WebUtility.HtmlEncode("Estimado(a):    " +
                                "El usuario y contraseña para ingresar a Punto de Encuentro (PDE) son los siguientes:  " +
                                "*  Usuario: " + vUsuario.CL_USUARIO + " *  Contraseña: " + vUsuario.CONTRASENA +
                                "*  Base de datos: " + bd + ". Liga de acceso: " + url + "    Gracias por tu apoyo!");
                             
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
                                string vTransaccion = "";
                                vTransaccion = vEditar == "true" ? "A" : "I";
                                vResultadoCorreo = nUsuario.InsertarUsuarioCorreo(SELECCIONUSUARIOS.ToString(), vClUsuario, vNbPrograma, vTransaccion);
                                vMensajeCorreo = vResultadoCorreo.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensajeCorreo, vResultadoCorreo.CL_TIPO_ERROR);

                            }
                            else
                            {
                                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
                            }


                        }

                    }

                }

                else
                {

                    var VLigado = "";
                    if (rbNoLigado.Checked == true)
                    {
                        VLigado = "Ingresa un correo electrónico.";
                    }
                    else
                    {
                        VLigado = "El usuario no  cuenta con correo electrónico, agrégalo en inventario de personal.";
                    }
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, VLigado , E_TIPO_RESPUESTA_DB.WARNING);

                }
            }
            else
            {
                var VLigado = "";
                if (rbNoLigado.Checked == true)
                {
                    VLigado = "Ingresa el nombre completo del usuario";
                }
                else
                {
                    VLigado = "Debes seleccionar a un usuario";
                }
                UtilMensajes.MensajeResultadoDB(rwmAlertas, VLigado , E_TIPO_RESPUESTA_DB.WARNING);

            }

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

        protected void cmbUusarios_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            vClUser = e.Text;
            CargarDatosNuevo(vClUser);

        }

        protected void grdEmpleados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ContextoPDENegocio negocio = new ContextoPDENegocio();
            List<SPE_OBTIENE_EMPLEADOS_GENERA_CONTRASENA_Result> vListaEmpleados = new List<SPE_OBTIENE_EMPLEADOS_GENERA_CONTRASENA_Result>();
            vListaEmpleados = negocio.ObtenerEmpleados_Pde();
            grdEmpleados.DataSource = vListaEmpleados;
        }

        protected void btnGuardarUsuarios_Click(object sender, EventArgs e)
        {

            vListaEUsuarios = new List<E_OBTIENE_EMPLEADOS_GENERA_CONTRASENA>();
            foreach (GridDataItem item in grdEmpleados.SelectedItems)
            {
                string vNbEmpleado = item.GetDataKeyValue("NB_EMPLEADO").ToString();
                string vNbPaterno = item.GetDataKeyValue("NB_PATERNO").ToString();
                string vNbCompleto = vNbEmpleado + " " + vNbPaterno;
                string vUsuario = item.GetDataKeyValue("ID_USUARIO").ToString();
                string vContrasena = item.GetDataKeyValue("CONTRASENA").ToString();
                string vCorreo = item.GetDataKeyValue("CORREO_ELECTRONICO").ToString();
                string vIdGrupo = item.GetDataKeyValue("ID_Grupo").ToString();
                string vIdEmpleado = item.GetDataKeyValue("ID_EMPLEADO").ToString();
                int vIdRol = (int)item.GetDataKeyValue("ID_ROL");

                vListaEUsuarios.Add(new E_OBTIENE_EMPLEADOS_GENERA_CONTRASENA
                {
                    NB_EMPLEADO = vNbEmpleado,
                    NB_PATERNO = vNbPaterno,
                    NB_COMPLETO = vNbCompleto,
                    ID_USUARIO = vUsuario,
                    CONTRASENA = vContrasena,
                    CORREO_ELECTRONICO = vCorreo,
                    ID_Grupo = vIdGrupo,
                    ID_EMPLEADO = vIdEmpleado,
                    ID_ROL = vIdRol,
                });

                var vXelements = vListaEUsuarios.Select(x =>
                                         new XElement("EMPLEADO",
                                         new XAttribute("DESCRIPCION", x.NB_COMPLETO),
                                          new XAttribute("NOMBRE", x.NB_EMPLEADO),
                                           new XAttribute("APELLIDO", x.NB_PATERNO),
                                            new XAttribute("USUARIO", x.ID_USUARIO),
                                             new XAttribute("CONTRASENA", x.CONTRASENA),
                                              new XAttribute("CORREO", x.CORREO_ELECTRONICO),
                                              new XAttribute("ID_GRUPO", x.ID_Grupo),
                                              new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO),
                                         new XAttribute("ID_ROL", x.ID_ROL)
                              ));

                SELECCIONEMPLEADOS =
                new XElement("SELECCION", vXelements
                );

            }

            if (SELECCIONEMPLEADOS != null)
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                E_RESULTADO vResultado = negocio.InsertarUsuariosPdeMasivo(SELECCIONEMPLEADOS.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                var correos = "";
                var Usuario = "";
                var Contrasena = "";
                var Nombre = "";
                E_RESULTADO vResultadoCorreo;
                string vMensajeCorreo;
                List<E_USUARIO_CORREO> listaEnvioCorreos = new List<E_USUARIO_CORREO>();
                XElement root = XElement.Parse(SELECCIONEMPLEADOS.ToString());
                foreach (XElement name in root.Elements("EMPLEADO"))
                {
                    correos = name.Attribute("CORREO").Value;
                    Usuario = name.Attribute("USUARIO").Value;
                    Contrasena = name.Attribute("CONTRASENA").Value;
                    Nombre = name.Attribute("DESCRIPCION").Value;
                    if (correos != "" && correos != null)
                    {
                        if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
                        {
                            E_USUARIO_CORREO usuarioCorreo = new E_USUARIO_CORREO();
                            SPE_OBTIENE_PDE_CONFIGURACION_URL_Result vConfiguracion ;
                            vConfiguracion = negocio.ObtenerConfiguracionPDE().FirstOrDefault();
                            string url = vConfiguracion.URL != null ? vConfiguracion.URL : "";
                            string bd = vConfiguracion.BASEDATOS != null ? vConfiguracion.BASEDATOS : "";
                               
                            string vUrls = WebUtility.HtmlEncode("Estimado(a):    " +
                              "El usuario y contraseña para ingresar a Punto de Encuentro (PDE) son los siguientes:  " +
                              "*  Usuario: " + Usuario + " *  Contraseña: " + Contrasena +
                              "*  Base de datos: " + bd + ". Liga de acceso: " + url + "    Gracias por tu apoyo!");
                            bool vEstatusCorreo = EnvioCorreo(correos, vUrls, "Usuario y contraseña para acceder a  Punto de encuentro");

                            usuarioCorreo.CL_USUARIO = Usuario;
                            usuarioCorreo.FE_ENVIO = DateTime.Now;
                            if (vEstatusCorreo)
                            {
                                usuarioCorreo.FG_ENVIADO = true;

                            }
                            else
                            {
                                usuarioCorreo.FG_ENVIADO = false;

                            }
                            listaEnvioCorreos.Add(usuarioCorreo);

                        }
                    }


                }
                if (listaEnvioCorreos.Count > 0 && listaEnvioCorreos != null)
                {

                    var vXelements = listaEnvioCorreos.Select(x =>
                        new XElement("USUARIO",
                         new XAttribute("CL_USUARIO", x.CL_USUARIO),
                            new XAttribute("FE_ENVIO", x.FE_ENVIO),
                            new XAttribute("FG_ENVIO", x.FG_ENVIADO)
                                ));

                    SELECCIONUSUARIOS =
                    new XElement("SELECCION", vXelements
                    );


                    vResultadoCorreo = negocio.InsertarUsuarioCorreo(SELECCIONUSUARIOS.ToString(), vClUsuario, vNbPrograma, "I");
                    vMensajeCorreo = vResultadoCorreo.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensajeCorreo, vResultadoCorreo.CL_TIPO_ERROR);


                }
                //UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);

            }
            else
            {

                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Selecciona al menos un empleado para crear los usuarios", E_TIPO_RESPUESTA_DB.WARNING);

            }
        }

        protected void cmbUsuariosNl_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            vClUser = e.Text;
            CargarDatosNuevo(vClUser);
        }
        protected void Tipo_Click(object sender, EventArgs e)
        {
            ObtenerUsuarios();
            cmbUsuarios.Text = "Selecciona un usuario";
            txtNbUsuario.ReadOnly = true;
            txtNbCorreoElectronico.ReadOnly = true;
            txtNbUsuario.Text = "";
            txtNbCorreoElectronico.Text = "";
            txtContrasena.Text = "";
        }
        protected void TipoNl_Click(object sender, EventArgs e)
        {
            ObtenerUsuarios();
            cmbUsuariosNl.Text = "Selecciona un usuario";
            txtNbUsuario.ReadOnly = false;
            txtNbCorreoElectronico.ReadOnly = false;
            txtNbUsuario.Text = "";
            txtNbCorreoElectronico.Text = "";
            txtContrasena.Text = "";
        }

    }
}