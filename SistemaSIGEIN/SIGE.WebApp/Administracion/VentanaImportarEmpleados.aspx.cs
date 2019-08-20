using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaImportarEmpleados : System.Web.UI.Page
    {
        #region VARIABLES
        string vClUsuario = "";
        string vNbPrograma = "";
        string vClCliente = "";
        string clSistema = "SIGEIN";
        string clModulo = "NOMINA";
        string noVersion = "5.00";

        public List<E_MENSAJES> MensajesError
        {
            get { return (List<E_MENSAJES>)ViewState["vs_ip_errores"]; }
            set { ViewState["vs_ip_errores"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO.ToString();
            vNbPrograma = ContextoUsuario.nbPrograma.ToString();
            //vClCliente = ContextoUsuario.clCliente.ToString();

            if (!Page.IsPostBack)
            {
                //E_RESULTADO msjRespuesta = C verificaLicencia(Contexto.clCliente, clSistema, Contexto.clEmpresa, clModulo, noVersion, Contexto.clUsuario, Contexto.nbPrograma);
                //if (msjRespuesta.clave_retorno != "-1000")
                //{
                //    Utileria.MuestraMensajeLicencia(-1, msjRespuesta.mensaje_retorno, rnMensaje, pcallbackFunction: "closePopup");
                //}

                MensajesError = new List<E_MENSAJES>();
            }
        }

        protected void GridErrores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            GridErrores.DataSource = MensajesError;
        }
    }
}