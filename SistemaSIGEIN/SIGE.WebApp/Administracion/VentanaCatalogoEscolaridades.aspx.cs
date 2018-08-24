using WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Net;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCatalogoEscolaridades : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private Boolean v_validad_tipo_transaccion
        {
            get { return (Boolean)ViewState["vsvalidad_tipo_transaccion"]; }
            set { ViewState["vsvalidad_tipo_transaccion"] = value; }
        }
        private int pIdEscolaridad
        {
            get { return (int)ViewState["vsIdEscolaridad"]; }
            set { ViewState["vsIdEscolaridad"] = value; }
        }

        private String pCLNivelEscolaridad
        {
            get { return (String)ViewState["vsClNivelEscolaridad"]; }
            set { ViewState["vsClNivelEscolaridad"] = value; }
        }

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }


        private E_ESCOLARIDAD vEscolaridad
        {
            get { return (E_ESCOLARIDAD)ViewState["vsEscolaridad"]; }
            set { ViewState["vsEscolaridad"] = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            EscolaridadNegocio negocio = new EscolaridadNegocio();
            pCLNivelEscolaridad = (Request.QueryString["ID_NIVEL_ESCOLARIDAD"]);

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma; 

            if (!IsPostBack)
            {
                ptipo = Request.QueryString["TIPO"];

                if (!ptipo.Equals("Agregar"))
                {
                    pIdEscolaridad = int.Parse((Request.QueryString["ID"]));

                    //  EscolaridadNegocio negocio = new EscolaridadNegocio();
                    vEscolaridad = new E_ESCOLARIDAD();
                    var x = negocio.Obtener_C_ESCOLARIDAD(ID_ESCOLARIDAD: pIdEscolaridad).FirstOrDefault();
                    vEscolaridad.ID_ESCOLARIDAD = x.ID_ESCOLARIDAD;
                    vEscolaridad.CL_ESCOLARIDAD = x.CL_ESCOLARIDAD;                    
                    vEscolaridad.NB_ESCOLARIDAD = x.NB_ESCOLARIDAD;
                    vEscolaridad.DS_ESCOLARIDAD = x.DS_ESCOLARIDAD;
                    vEscolaridad.CL_NIVEL_ESCOLARIDAD = x.CL_NIVEL_ESCOLARIDAD;
                    vEscolaridad.FG_ACTIVO = x.FG_ACTIVO;
                    vEscolaridad.NB_ACTIVO = x.NB_ACTIVO;
                    vEscolaridad.DS_FILTRO = x.DS_FILTRO;
                    vEscolaridad.CL_INSTITUCION = x.CL_INSTITUCION;
                    if (vEscolaridad != null)
                    {
                        txtNbCatalogo.Text = vEscolaridad.NB_ESCOLARIDAD;
                        txtClCatalogo.Text = vEscolaridad.CL_ESCOLARIDAD;
                        txtClCatalogo.ReadOnly = true;
                        if (vEscolaridad.FG_ACTIVO == true)
                        {
                            chkActivo.Checked = true;
                        }
                        else
                        {
                            chkActivo.Checked = false;
                        }
                    }
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
           // Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "closeWindow(0);", true);
        }

        protected void btnSave_click(object sender, EventArgs e)
        {
            EscolaridadNegocio negocio = new EscolaridadNegocio();
            E_ESCOLARIDAD vESCOLARIDAD = new E_ESCOLARIDAD();

            if (!ptipo.Equals("Agregar"))
            {
                vEscolaridad.CL_ESCOLARIDAD = txtClCatalogo.Text;
                vEscolaridad.NB_ESCOLARIDAD = txtNbCatalogo.Text;
                vEscolaridad.FG_ACTIVO = chkActivo.Checked;
                vESCOLARIDAD.CL_NIVEL_ESCOLARIDAD = pCLNivelEscolaridad;
                //vEscolaridad.DS_ESCOLARIDAD = txtDsCatalogo.Text;
                E_RESULTADO vResultado = negocio.InsertaActualiza_C_ESCOLARIDAD(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), usuario: vClUsuario, programa: vNbPrograma, V_C_ESCOLARIDAD: vEscolaridad);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
            else
            {
                vESCOLARIDAD.ID_ESCOLARIDAD = 1;
                vESCOLARIDAD.NB_ESCOLARIDAD = txtNbCatalogo.Text;
                vESCOLARIDAD.CL_ESCOLARIDAD = txtClCatalogo.Text;
                vESCOLARIDAD.FG_ACTIVO = chkActivo.Checked;
                vESCOLARIDAD.CL_NIVEL_ESCOLARIDAD = pCLNivelEscolaridad; 

                E_RESULTADO vResultado = negocio.InsertaActualiza_C_ESCOLARIDAD(tipo_transaccion: E_TIPO_OPERACION_DB.I.ToString(), usuario: vClUsuario, programa: vNbPrograma, V_C_ESCOLARIDAD: vESCOLARIDAD);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
        }

    
    }
}