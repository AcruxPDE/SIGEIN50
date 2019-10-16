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
using System.Text.RegularExpressions;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaUsuarios : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private string vClCliente;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement SELECCIONEMPLEADOS { get; set; }

        private XElement SELECCIONEMPLEADOS2 { get; set; }
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

        private string vPassword
        {
            get { return (string)ViewState["vPassword"]; }
            set { ViewState["vPassword"] = value; }
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

        private List<E_EMPLEADOS_GENERA_CONTRASENA> vListaEmpleados
        {
            get { return (List<E_EMPLEADOS_GENERA_CONTRASENA>)ViewState["vs_vListaEmpleados"]; }
            set { ViewState["vs_vListaEmpleados"] = value; }
        }

        private List<E_OBTIENE_EMPLEADOS_GENERA_CONTRASENA> vListaEUsuarios
        {
            get { return (List<E_OBTIENE_EMPLEADOS_GENERA_CONTRASENA>)ViewState["vs_vListaEUsuarios"]; }
            set { ViewState["vs_vListaEUsuarios"] = value; }
        }

        private string contrasenaAleatoria
        {
            get { return (ViewState["vs_contrasenaAleatoria"] != null) ? ViewState["vs_contrasenaAleatoria"].ToString() : String.Empty; }
            set { ViewState["vs_contrasenaAleatoria"] = value; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClCliente = ContextoApp.Licencia.clCliente;

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

            }
           
        }

        protected void CargarDatos(string pIdUsuario)
        {
            UsuarioNegocio nUsuario = new UsuarioNegocio();
            E_USUARIO vUsuario = nUsuario.ObtieneUsuario(pIdUsuario);

            cmbRol.Items.AddRange(vUsuario.XML_CATALOGOS.Element("ROLES").Elements("ROL").Select(s => new RadComboBoxItem(s.Attribute("NB_ROL").Value, s.Attribute("ID_ROL").Value)
            {
                Selected = UtilXML.ValorAtributo<bool>(s.Attribute("FG_SELECCIONADO"))
            }));

            RadListBoxItem vItmEmpleado = new RadListBoxItem("No seleccionado", String.Empty);
            lstEmpleado.Items.Add(vItmEmpleado);
        }

        protected void btnGuardarInformacionGeneral_Click(object sender, EventArgs e)
        {
            int vIdRol = 0;
            int vIdEmpleado = 0;
            E_USUARIO vUsuario = new E_USUARIO();

            vUsuario.CL_USUARIO = txtClUsuario.Text;
            vUsuario.NB_USUARIO = txtNbUsuario.Text;
            vUsuario.FG_CAMBIAR_PASSWORD = true;
            vUsuario.NB_CORREO_ELECTRONICO = txtNbCorreoElectronico.Text;
            vUsuario.FG_ACTIVO = chkActivo.Checked;
            vUsuario.NB_PASSWORD = GenerarContrasena();
            vUsuario.CL_TIPO_MULTIEMPRESA = "Corporativo ";

            foreach (RadListBoxItem item in lstEmpleado.Items)
                if (int.TryParse(item.Value, out vIdEmpleado))
                    vUsuario.ID_EMPLEADO = vIdEmpleado;

            if (int.TryParse(cmbRol.SelectedValue, out vIdRol))
                vUsuario.ID_ROL = vIdRol;
            else
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Debes selecionar un rol.", E_TIPO_RESPUESTA_DB.WARNING);

            UsuarioNegocio nUsuario = new UsuarioNegocio();

            E_RESULTADO vResultado = nUsuario.InsertaActualizaUsuario(vClOperacion, vUsuario, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if(vResultado.CL_TIPO_ERROR.ToString() == "SUCCESSFUL")
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Proceso exitoso", E_TIPO_RESPUESTA_DB.SUCCESSFUL);
            else
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);


        }

        public string GenerarContrasena()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 10;
            contrasenaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contrasenaAleatoria += letra.ToString();
            }

            return contrasenaAleatoria;
        }
    }
}