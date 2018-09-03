using WebApp.Comunes;
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
using Telerik.Web.UI;
using SIGE.Entidades;
using System.Net;


namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCatalogoNivelEscolaridad : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        
        private int pID_NIVEL_ESCOLARIDAD
        {
            get { return (int)ViewState["vsID_NIVEL_ESCOLARIDAD"]; }
            set { ViewState["vsID_NIVEL_ESCOLARIDAD"] = value; }
        }

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }
        private string vCL_TIPO_ESCOLARIDAD
        {
            get { return (string)ViewState["vsCL_TIPO_ESCOLARIDAD"]; }
            set { ViewState["vsCL_TIPO_ESCOLARIDAD"] = value; }
        }


        private E_NIVEL_ESCOLARIDAD vNivelEscolaridad
        {
            get { return (E_NIVEL_ESCOLARIDAD)ViewState["vsNivelEscolaridad"]; }
            set { ViewState["vsNivelEscolaridad"] = value; }
        }


        private List<E_TIPO_ESCOLARIDAD> vNIVELES_ESCOLARIDAD
        {
            get { return (List<E_TIPO_ESCOLARIDAD>)ViewState["vsNIVELES_ESCOLARIDAD"]; }
            set { ViewState["vsNIVELES_ESCOLARIDAD"] = value; }
        }


        List<String> vNivelesEscolaridad;
        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

           
            if (!IsPostBack)
            
            {

                //vNivelesEscolaridad = new List<string>();
                //vNivelesEscolaridad.Add(E_CL_TIPO_ESCOLARIDAD.BACHILLERATO.ToString());
                //vNivelesEscolaridad.Add(E_CL_TIPO_ESCOLARIDAD.POSGRADO.ToString());
                //vNivelesEscolaridad.Add(E_CL_TIPO_ESCOLARIDAD.PRIMARIA.ToString());
                //vNivelesEscolaridad.Add(E_CL_TIPO_ESCOLARIDAD.PROFESIONAL.ToString());
                //vNivelesEscolaridad.Add(E_CL_TIPO_ESCOLARIDAD.SECUNDARIA.ToString());

                NivelEscolaridadNegocio negocio = new NivelEscolaridadNegocio();
                //foreach(String vnivel in vNivelesEscolaridad)
                //{
                //RadComboBoxItem item= new RadComboBoxItem();
                //    item.Text=vnivel;
                //    item.Value=vnivel;
                //    cmbNivelEscolaridades.Items.Add(item);
                //}
                vNIVELES_ESCOLARIDAD = negocio.Obtener_VW_TIPO_ESCOLARIDAD();
                cmbNivelEscolaridades.DataSource = vNIVELES_ESCOLARIDAD;//LLENAMOS DE DATOS EL GRID
                cmbNivelEscolaridades.DataTextField = "CL_TIPO_ESCOLARIDAD";
                cmbNivelEscolaridades.DataValueField = "CL_TIPO_ESCOLARIDAD";
                cmbNivelEscolaridades.DataBind();

                
                ptipo = Request.QueryString["TIPO"];
                vNivelEscolaridad = new E_NIVEL_ESCOLARIDAD();
                if (Request.Params["ID"] != null)
                {

                    pID_NIVEL_ESCOLARIDAD = int.Parse((Request.QueryString["ID"]));
                    var vObjetoNivelEsc = negocio.Obtener_C_NIVEL_ESCOLARIDAD(ID_NIVEL_ESCOLARIDAD: pID_NIVEL_ESCOLARIDAD).FirstOrDefault();
                    vNivelEscolaridad.CL_NIVEL_ESCOLARIDAD = vObjetoNivelEsc.CL_NIVEL_ESCOLARIDAD;

                    vNivelEscolaridad.FG_ACTIVO = vObjetoNivelEsc.FG_ACTIVO;
                    vNivelEscolaridad.ID_NIVEL_ESCOLARIDAD = vObjetoNivelEsc.ID_NIVEL_ESCOLARIDAD;
                    vNivelEscolaridad.CL_TIPO_ESCOLARIDAD = vObjetoNivelEsc.CL_TIPO_ESCOLARIDAD;
                    vNivelEscolaridad.DS_NIVEL_ESCOLARIDAD = vObjetoNivelEsc.DS_NIVEL_ESCOLARIDAD;

                    if (vNivelEscolaridad != null)
                    {
                         txtClCatalogo.Text = vNivelEscolaridad.CL_NIVEL_ESCOLARIDAD;
                         txtNbCatalogo.Text = vNivelEscolaridad.DS_NIVEL_ESCOLARIDAD;
                         cmbNivelEscolaridades.SelectedValue = vNivelEscolaridad.CL_TIPO_ESCOLARIDAD.ToString();
                         txtClCatalogo.ReadOnly = true;
                        if (vNivelEscolaridad.FG_ACTIVO == true)
                        {
                            chkActivo.Checked = true;
                        }
                        else
                        {
                            chkActivo.Checked = false;
                        }
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
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "closeWindow(0);", true);
        }

        protected void btnSave_click(object sender, EventArgs e)
        {

            NivelEscolaridadNegocio negocio = new NivelEscolaridadNegocio();
            E_NIVEL_ESCOLARIDAD vAreaAgregar = new E_NIVEL_ESCOLARIDAD();


            if (Request.Params["ID"] != null)
            {
                vNivelEscolaridad.CL_NIVEL_ESCOLARIDAD = txtClCatalogo.Text;
                vNivelEscolaridad.DS_NIVEL_ESCOLARIDAD = txtNbCatalogo.Text;
                vNivelEscolaridad.FG_ACTIVO = chkActivo.Checked;
                vNivelEscolaridad.CL_TIPO_ESCOLARIDAD = cmbNivelEscolaridades.SelectedValue;


                E_RESULTADO vResultado = negocio.InsertaActualiza_C_NIVEL_ESCOLARIDAD(tipo_transaccion: E_TIPO_OPERACION_DB.A.ToString(), usuario: vClUsuario, programa: vNbPrograma, V_C_NIVEL_ESCOLARIDAD: vNivelEscolaridad);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
            else
            {
                vAreaAgregar.ID_NIVEL_ESCOLARIDAD = 1;
                vAreaAgregar.DS_NIVEL_ESCOLARIDAD = txtNbCatalogo.Text;
                vAreaAgregar.CL_NIVEL_ESCOLARIDAD = txtClCatalogo.Text;
                vAreaAgregar.FG_ACTIVO = chkActivo.Checked;
                vAreaAgregar.CL_TIPO_ESCOLARIDAD = cmbNivelEscolaridades.SelectedValue;

                E_RESULTADO vResultado = negocio.InsertaActualiza_C_NIVEL_ESCOLARIDAD(tipo_transaccion: E_TIPO_OPERACION_DB.I.ToString(), usuario: vClUsuario, programa: vNbPrograma, V_C_NIVEL_ESCOLARIDAD: vAreaAgregar);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
        }


    }
}