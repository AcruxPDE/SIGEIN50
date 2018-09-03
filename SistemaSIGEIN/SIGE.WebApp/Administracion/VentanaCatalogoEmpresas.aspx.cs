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
    
    public partial class VentanaCatalogoEmpresas : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        private Boolean v_validatipo_transaccion
        {
            get { return (Boolean)ViewState["vsvalidatipo_transaccion"]; }
            set { ViewState["vsvalidatipo_transaccion"] = value; }
        }
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


        private E_EMPRESAS vExperienciaProfesional
        {
            get { return (E_EMPRESAS)ViewState["vsvExperienciaProfesional"]; }
            set { ViewState["vsvExperienciaProfesional"] = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
           EmpresaNegocio negocio = new EmpresaNegocio();
           vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
           vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {

                ptipo = Request.QueryString["TIPO"];
                vExperienciaProfesional = new E_EMPRESAS();
                if (!ptipo.Equals("Agregar"))
                {

                    pID = int.Parse((Request.QueryString["ID"]));

                    // AreaInteresNegocio negocio = new AreaInteresNegocio();
                    var vObjetoAreaInteres = negocio.Obtener_C_EMPRESA(ID_EMPRESA: pID).FirstOrDefault();
                    vExperienciaProfesional.ID_EMPRESA =vObjetoAreaInteres.ID_EMPRESA;
                    vExperienciaProfesional.CL_EMPRESA =vObjetoAreaInteres.CL_EMPRESA;
                    vExperienciaProfesional.NB_EMPRESA =vObjetoAreaInteres.NB_EMPRESA;
                    vExperienciaProfesional.NB_RAZON_SOCIAL =vObjetoAreaInteres.NB_RAZON_SOCIAL;
                    vExperienciaProfesional.DS_FILTRO = vObjetoAreaInteres.DS_FILTRO;



                    if (vExperienciaProfesional != null)
                    {
                        txtNbCatalogo.Text = vExperienciaProfesional.NB_EMPRESA;
                        txtClCatalogo.Text = vExperienciaProfesional.CL_EMPRESA;
                      //  txtNBRazon.Text = vExperienciaProfesional.NB_RAZON_SOCIAL;
                        txtClCatalogo.ReadOnly = true;
                    }
                }
                else
                {
                    
                }

            }

            else
            {
     
            }

        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "closeWindow(0);", true);
        }

        protected void btnSave_click(object sender, EventArgs e)
        {
            EmpresaNegocio negocio = new EmpresaNegocio();
            E_EMPRESAS vExperienciaAgregar = new E_EMPRESAS();


            if (!ptipo.Equals("Agregar"))
            {
                vExperienciaProfesional.CL_EMPRESA = txtClCatalogo.Text;
                vExperienciaProfesional.NB_EMPRESA = txtNbCatalogo.Text;
               // vExperienciaProfesional.NB_RAZON_SOCIAL = txtNBRazon.Text;
               E_RESULTADO vResultado= negocio.InsertaActualiza_C_EMPRESA(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), usuario: vClUsuario, programa: vNbPrograma, V_C_EMPRESA: vExperienciaProfesional);
               //  = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                

            }
            else
            {
                vExperienciaAgregar.ID_EMPRESA = 1;
                vExperienciaAgregar.NB_EMPRESA = txtNbCatalogo.Text;
                vExperienciaAgregar.CL_EMPRESA = txtClCatalogo.Text;
                //vExperienciaAgregar.NB_RAZON_SOCIAL = txtNBRazon.Text;


               
                   E_RESULTADO vResultado = negocio.InsertaActualiza_C_EMPRESA(tipo_transaccion: E_TIPO_OPERACION_DB.I.ToString(), usuario:vClUsuario ,programa: vNbPrograma, V_C_EMPRESA: vExperienciaAgregar);

                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;


                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                    

               
            }
        }



    }
}