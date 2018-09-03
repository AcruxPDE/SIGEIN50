using WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Net;

namespace SIGE.WebApp.Administracion
{
    public partial class PopupCatExperProf : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma ;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int pID
        {
            get { return (int)ViewState["vsID"]; }
            set { ViewState["vsID"] = value; }
        }

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }


        private E_AREA_INTERES vExperienciaProfesional
        {
            get { return (E_AREA_INTERES)ViewState["vsvExperienciaProfesional"]; }
            set { ViewState["vsvExperienciaProfesional"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                ptipo = Request.QueryString["TIPO"];
                vExperienciaProfesional = new E_AREA_INTERES();
                if (!ptipo.Equals("Agregar"))
                {
                    AreaInteresNegocio negocio = new AreaInteresNegocio();

                    pID = int.Parse((Request.QueryString["ID"]));

                    // AreaInteresNegocio negocio = new AreaInteresNegocio();
                    var vObjetoAreaInteres = negocio.Obtener_C_AREA_INTERES(id_area_interes: pID).FirstOrDefault();
                    vExperienciaProfesional.ID_AREA_INTERES = vObjetoAreaInteres.ID_AREA_INTERES;
                    vExperienciaProfesional.CL_AREA_INTERES = vObjetoAreaInteres.CL_AREA_INTERES;
                    vExperienciaProfesional.NB_AREA_INTERES = vObjetoAreaInteres.NB_AREA_INTERES;
                    vExperienciaProfesional.FG_ACTIVO = vObjetoAreaInteres.FG_ACTIVO;
                    vExperienciaProfesional.NB_ACTIVO = vObjetoAreaInteres.NB_ACTIVO;
                    vExperienciaProfesional.DS_FILTRO = vObjetoAreaInteres.DS_FILTRO;

                    if (vExperienciaProfesional != null)
                    {
                        txtNbCatalogo.Text = vExperienciaProfesional.NB_AREA_INTERES;
                        txtClCatalogo.Text = vExperienciaProfesional.CL_AREA_INTERES;
                        txtClCatalogo.ReadOnly = true;
                        chkActivo.Checked = vExperienciaProfesional.FG_ACTIVO ?? false;
                    }
                }
                else
                {
                    chkActivo.Checked = false;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "closeWindow(0);", true);
        }

        protected void btnSave_click(object sender, EventArgs e)
        {
            AreaInteresNegocio negocio = new AreaInteresNegocio();
            E_AREA_INTERES vExperienciaAgregar = new E_AREA_INTERES();

            if (!ptipo.Equals("Agregar"))
            {
                vExperienciaProfesional.CL_AREA_INTERES = txtClCatalogo.Text;
                vExperienciaProfesional.NB_AREA_INTERES = txtNbCatalogo.Text;
                vExperienciaProfesional.FG_ACTIVO = chkActivo.Checked;

                E_RESULTADO vResultado = negocio.InsertaActualiza_C_AREA_INTERES(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), usuario: vClUsuario, programa: vNbPrograma, v_c_area_interes: vExperienciaProfesional);
                // = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
            else
            {
                vExperienciaAgregar.ID_AREA_INTERES = 1;
                vExperienciaAgregar.NB_AREA_INTERES = txtNbCatalogo.Text;
                vExperienciaAgregar.CL_AREA_INTERES = txtClCatalogo.Text;
                vExperienciaAgregar.FG_ACTIVO = chkActivo.Checked;

                E_RESULTADO vResultado = negocio.InsertaActualiza_C_AREA_INTERES(tipo_transaccion: E_TIPO_OPERACION_DB.I.ToString(), usuario: vClUsuario, programa: vNbPrograma, v_c_area_interes: vExperienciaAgregar);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
        }
    }
}