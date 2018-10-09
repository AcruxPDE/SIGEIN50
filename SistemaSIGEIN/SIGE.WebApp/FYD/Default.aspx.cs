using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.FYD
{
    public partial class Default : System.Web.UI.Page
    {
        /* Pendientes listos que afectan varias pantallas*/
        //TODO: Abrir los window con el codigo que no haga mas pequeñas las pantallas.

        /* Pendientes por revisar*/
        //TODO:- Crear automaticamente los cuestionarios (Revisar con julio)
        //TODO:- Revisar eliminacion de cuestionarios. (Revisar con Julio)
        //TODO: 7.	Copiar información para solicitudes de empleo

        /*Pendientes Nivel Dificil*/


        /* Pendientes Nivel medio */

        #region Variables

        public string vNbUsuario
        {
            get { return (string)ViewState["vs_vNbUsuario"]; }
            set { ViewState["vs_vNbUsuario"] = value; }
        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            vNbUsuario = ContextoUsuario.oUsuario.NB_USUARIO;
            if (ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo != null)
                rbiLogoOrganizacion1.DataValue = ContextoApp.InfoEmpresa.FiLogotipo.FiArchivo;
            else
                dvLogo.Visible = false;
        }
    }
}