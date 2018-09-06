using SIGE.Entidades;
using SIGE.Entidades.Externas;
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
    public partial class VentanaCreaDescriptivo : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int? vIdPuesto 
        {
            get { return(int?)ViewState["vs_vIdPuesto"];}
            set { ViewState["vs_vIdPuesto"] = value; }
        }

        #endregion

        #region Metodos

        private bool ValidarControles()
        {
            if (txtClave.Text != "" && txtClave.Text != null)
                if (txtNombre.Text != "" && txtNombre.Text != null)
                        return true;

            return false;
        }

        private void CargarDatos()
        {
            CamposNominaNegocio cNegocio = new CamposNominaNegocio();
            SPE_OBTIENE_PUESTOS_NOMINA_DO_Result vPuestoNominaDo = cNegocio.ObtienePuestosNominaDo(pID_PUESTO_NOMINA_DO: vIdPuesto).FirstOrDefault();

            txtClave.Text = vPuestoNominaDo.CL_PUESTO;
            txtNombre.Text = vPuestoNominaDo.NB_PUESTO;
            btnDOTrue.Checked = vPuestoNominaDo.FG_DO;
            btnDOFalse.Checked = !vPuestoNominaDo.FG_DO;
            btnNOTrue.Checked = vPuestoNominaDo.FG_NOMINA;
            btnNOFalse.Checked = !vPuestoNominaDo.FG_NOMINA;

            if (vPuestoNominaDo.FG_DO == false)
                btnMasDatos.Enabled = false;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnNOFalse.Checked = true;
                btnDOTrue.Checked = true;

                int vIdPuestoRq = 0;
                if (int.TryParse(Request.Params["pIdPuesto"], out vIdPuestoRq))
                {
                    vIdPuesto = vIdPuestoRq;
                    CargarDatos();
                }

                if (ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo != "1")
                {
                    btnNOFalse.Enabled = false;
                    btnNOTrue.Enabled = false;
                }

                //if (ContextoApp.IDP.LicenciaIntegracion.MsgActivo != "1" && ContextoApp.FYD.LicenciaFormacion.MsgActivo != "1" && ContextoApp.EO.LicenciaCL.MsgActivo != "1" && ContextoApp.EO.LicenciaED.MsgActivo != "1"
                // && ContextoApp.EO.LicenciaRDP.MsgActivo != "1" && ContextoApp.MPC.LicenciaMetodologia.MsgActivo != "1" && ContextoApp.RP.LicenciaReportes.MsgActivo != "1" && ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo != "1"
                //   && ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo != "1")
                //{
                //    btnDOFalse.Enabled = false;
                //    btnDOTrue.Enabled = false;
                //    btnDOTrue.Checked = false;
                //    btnDOFalse.Checked = true;
                //    btnMasDatos.Enabled = false;
                //}

            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                if (btnDOTrue.Checked != true && btnNOTrue.Checked != true)
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "El puesto debe de estar disponible en Nómina o/y DO.", E_TIPO_RESPUESTA_DB.ERROR, 400,150, pCallBackFunction: "");
                    return;
                }
                else
                {
                    string vClPuesto = txtClave.Text;
                    string vNbPuesto = txtNombre.Text;
                    bool vFgNomina = btnNOTrue.Checked ? true : false;
                    bool vFgDO = btnDOTrue.Checked ? true : false;
                    string vClTipoTransaccion = vIdPuesto != null ? "A" : "I";

                    CamposNominaNegocio nNomina = new CamposNominaNegocio();
                    E_RESULTADO vResultado = nNomina.InsertaActualizaPuesto(pID_PUESTO: vIdPuesto, pCL_PUESTO: vClPuesto, pNB_PUESTO: vNbPuesto, pFG_NOMINA: vFgNomina, pFG_DO: vFgDO, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pClTipoTransaccion: vClTipoTransaccion);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "OnCloseWindows");
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "La clave y el nombre del puesto son requeridos.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
            }
        }

        protected void btnMasDatos_Click(object sender, EventArgs e)
        {
            if (ValidarControles())
            {
                if (btnDOTrue.Checked != true && btnNOTrue.Checked != true)
                {
                    UtilMensajes.MensajeResultadoDB(rwmAlertas, "El puesto debe de estar disponible en Nómina o/y DO.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, pCallBackFunction: "");
                    return;
                }
                else
                {
                    string vClPuesto = txtClave.Text;
                    string vNbPuesto = txtNombre.Text;
                    bool vFgNomina = btnNOTrue.Checked ? true : false;
                    bool vFgDO = btnDOTrue.Checked ? true : false;
                    string vClTipoTransaccion = vIdPuesto != null ? "A" : "I";

                    CamposNominaNegocio nNomina = new CamposNominaNegocio();
                    E_RESULTADO vResultado = nNomina.InsertaActualizaPuesto(pID_PUESTO: vIdPuesto, pCL_PUESTO: vClPuesto, pNB_PUESTO: vNbPuesto, pFG_NOMINA: vFgNomina, pFG_DO: vFgDO, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pClTipoTransaccion: vClTipoTransaccion);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL && vFgDO == true)
                    {
                        var idPuesto = 0;
                        var esNumero = int.TryParse(vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_PUESTO").FirstOrDefault().DS_MENSAJE, out idPuesto);
                        vIdPuesto = idPuesto;

                        ClientScript.RegisterStartupScript(GetType(), "script", "AbrirDescriptivo(" + vIdPuesto + ");", true);
                    }
                    else
                    {

                        UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "OnCloseWindows");
                    }
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "La clave y el nombre del puesto son requeridos.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
            }
        }
    }
}