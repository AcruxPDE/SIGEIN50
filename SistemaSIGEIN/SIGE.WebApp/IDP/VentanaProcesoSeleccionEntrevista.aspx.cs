using SIGE.Entidades.Externas;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaProcesoSeleccionEntrevista : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdEntrevista
        {
            get { return (int)ViewState["vs_vpse_id_entrevista"]; }
            set { ViewState["vs_vpse_id_entrevista"] = value; }
        }

        private int vIdProcesoSeleccion
        {
            get { return (int)ViewState["vs_vpse_id_proceso_seleccion"]; }
            set { ViewState["vs_vpse_id_proceso_seleccion"] = value; }
        }

        private DateTime vFeEntrevista
        {
            get { return (DateTime)ViewState["vs_vpse_fe_entrevista"]; }
            set { ViewState["vs_vpse_fe_entrevista"] = value; }
        }

        private string vCltocken
        {
            get { return (string)ViewState["vs_vpse_cl_token"]; }
            set { ViewState["vs_vpse_cl_token"] = value; }
        }

        private Guid vFlEntrevista
        {
            get { return (Guid)ViewState["vs_vpse_fl_entrevista"]; }
            set { ViewState["vs_vpse_fl_entrevista"] = value; }
        }

        public int? vIdEntrevistador
        {
            get { return (int?)ViewState["vIdEntrevistador"]; }
            set { ViewState["vIdEntrevistador"] = value; }
        }


        #endregion

        #region Funciones

        private void CargarCatalogos()
        {
            ProcesoSeleccionNegocio nProceso = new ProcesoSeleccionNegocio();

            var vLstTipoentrevista = nProceso.ObtieneTipoEntrevista();

            cmbTipoEntrevista.DataSource = vLstTipoentrevista;
            cmbTipoEntrevista.DataBind();

        }

        private void CargarDatos()
        {
            ProcesoSeleccionNegocio nProceso = new ProcesoSeleccionNegocio();

            var vEntrevista = nProceso.ObtieneEntrevistaProcesoSeleccion(pIdEntrevista: vIdEntrevista).FirstOrDefault();

            if (vEntrevista != null)
            {
                cmbTipoEntrevista.SelectedValue = vEntrevista.ID_ENTREVISTA_TIPO.ToString();
                vFeEntrevista = vEntrevista.FE_ENTREVISTA;
                RadListBoxItem vItem = new RadListBoxItem();
                vItem.Text = vEntrevista.NB_ENTREVISTADOR;
                vItem.Value = vEntrevista.ID_ENTREVISTADOR.ToString();
                txtEntrevistador.Text = vItem.Text;
                vIdEntrevistador  = vEntrevista.ID_ENTREVISTADOR;
                //lstEntrevistador.Items.Add(vItem);
                txtCorreoEntrevistador.Text = vEntrevista.CL_CORREO_ENTREVISTADOR;
                txtPuesto.Text = vEntrevista.NB_PUESTO_ENTREVISTADOR;
                txtDsNotas.Content = vEntrevista.DS_OBSERVACIONES;

                vCltocken = vEntrevista.CL_TOKEN;
                vFlEntrevista = vEntrevista.FL_ENTREVISTA.Value;

            }
            if (vIdEntrevistador == null)
            {
                txtEntrevistador.Enabled = true;
                txtPuesto.Enabled = true;
            }
            else
            {
                txtEntrevistador.Enabled = false;
                txtPuesto.Enabled = false;
            }
        }

        private void GuardarDatos()
        {
            E_ENTREVISTA vEntrevista = new E_ENTREVISTA();
            ProcesoSeleccionNegocio nProceso = new ProcesoSeleccionNegocio();

            var vEntrevistas = vIdEntrevista != null ? nProceso.ObtieneEntrevistaProcesoSeleccion(pIdEntrevista: vIdEntrevista).FirstOrDefault() : null;


            string vClTipoSeleccion = "";

            vEntrevista.CL_CORREO_ENTREVISTADOR = txtCorreoEntrevistador.Text;
            vEntrevista.DS_OBSERVACIONES = txtDsNotas.Content;
            vEntrevista.ID_ENTREVISTA_TIPO = int.Parse(cmbTipoEntrevista.SelectedValue);
            vEntrevista.ID_PROCESO_SELECCION = vIdProcesoSeleccion;

            //if (lstEntrevistador.SelectedItem != null && lstEntrevistador.Items[0].Value != "")
            //{
            //    vEntrevista.ID_ENTREVISTADOR = int.Parse(lstEntrevistador.Items[0].Value);
            //    vEntrevista.NB_ENTREVISTADOR = lstEntrevistador.Items[0].Text;
            //    vEntrevista.NB_PUESTO_ENTREVISTADOR = txtPuesto.Text;
            //}
            //else if (vEntrevistas != null && lstEntrevistador.Items[0].Value != "")
            //{
            //    vEntrevista.ID_ENTREVISTADOR = int.Parse(lstEntrevistador.Items[0].Value);
            //    vEntrevista.NB_ENTREVISTADOR = lstEntrevistador.Items[0].Text;
            //    vEntrevista.NB_PUESTO_ENTREVISTADOR = txtPuesto.Text;
            //}
            if (txtEntrevistador.Text != "")
            {
                int idEntrevistador = (vIdEntrevistador != null) ? Convert.ToInt32(vIdEntrevistador) : 0;
                vEntrevista.ID_ENTREVISTADOR = idEntrevistador;
                vEntrevista.NB_ENTREVISTADOR = txtEntrevistador.Text;
                vEntrevista.NB_PUESTO_ENTREVISTADOR = txtPuesto.Text;
            }
            else if (vEntrevistas != null && txtEntrevistador.Text != "")
            {
                int idEntrevistador = (vIdEntrevistador != null) ? Convert.ToInt32(vIdEntrevistador ) : 0;
                vEntrevista.ID_ENTREVISTADOR = idEntrevistador;
                vEntrevista.NB_ENTREVISTADOR = txtEntrevistador.Text;
                vEntrevista.NB_PUESTO_ENTREVISTADOR = txtPuesto.Text;
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No se ha seleccionado un entrevistador", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
                return;
            }

            if (vIdEntrevista != 0)
            {
                vClTipoSeleccion = E_TIPO_OPERACION_DB.A.ToString();
                vEntrevista.ID_ENTREVISTA = vIdEntrevista;
                vEntrevista.FE_ENTREVISTA = vFeEntrevista;
                vEntrevista.CL_TOKEN = vCltocken;
                vEntrevista.FL_ENTREVISTA = vFlEntrevista;
            }
            else
            {
                vClTipoSeleccion = E_TIPO_OPERACION_DB.I.ToString();
                vEntrevista.FE_ENTREVISTA = DateTime.Now;
                vEntrevista.FL_ENTREVISTA = Guid.NewGuid();
                vEntrevista.CL_TOKEN = Membership.GeneratePassword(12, 1);

            }

            E_RESULTADO vRespuesta = nProceso.InsertaActualizaEntrevista(vClTipoSeleccion, vEntrevista, vClUsuario, vNbPrograma);
            string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vRespuesta.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "generateDataForParent");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }

        private bool ValidarDatos()
        {

            //string vMensaje = "";

            //if (lstEntrevistador.Items.Count == 0)
            //{
            //    UtilMensajes.MensajeResultadoDB(rnMensaje, "No se ha seleccionado un entrevistador", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            //    return false;
            //}
            if (txtEntrevistador.Text == "")
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No se ha seleccionado un entrevistador", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtCorreoEntrevistador.Text))
            {
                if (Utileria.ComprobarFormatoEmail(txtCorreoEntrevistador.Text))
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "El correo no esta en el formato correcto.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    return false;
                }
            }


            return true;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        
            if (!Page.IsPostBack)
            {
                vIdEntrevista = 0;
                vCltocken = "";
                vFlEntrevista = Guid.Empty;

                CargarCatalogos();

                if (Request.Params["IdProcesoSeleccion"] != null)
                {
                    vIdProcesoSeleccion = int.Parse(Request.Params["IdProcesoSeleccion"].ToString());
                }

                if (Request.Params["IdEntrevista"] != null)
                {
                    vIdEntrevista = int.Parse(Request.Params["IdEntrevista"].ToString());
                    CargarDatos();
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos();
        }

        protected void ramEntrevista_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vIdEntrevistador  = int.Parse(pParameter);
                txtEntrevistador.Enabled = false;
                txtPuesto.Enabled = false ;
            }
        }

        protected void btnEliminarEntrevistador_Click(object sender, EventArgs e)
        {
            txtEntrevistador.Enabled = true ;
            txtPuesto.Enabled = true;
        }
    }
}