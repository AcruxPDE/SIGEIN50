using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.IDP.Pruebas
{
    public partial class PruebaBienvenida : System.Web.UI.Page
    {

        #region Variables


        public Guid vClTokenExterno
        {
            get { return (Guid)ViewState["vs_pb_cl_token"]; }
            set { ViewState["vs_pb_cl_token"] = value; }
        }

        public int vFlBateria {
            get { return (int)ViewState["vs_pb_fl_bateria"]; }
            set { ViewState["vs_pb_fl_bateria"] = value; }
        }

        public int vIdCandidato {
            get { return (int)ViewState["vs_pb_id_candidato"]; }
            set { ViewState["vs_pb_id_candidato"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarCandidato()
        {
            SolicitudNegocio nSolicitud = new SolicitudNegocio();

            var vSolicitud = nSolicitud.ObtieneSolicitudes(ID_CANDIDATO: vIdCandidato).FirstOrDefault();

            if (vSolicitud != null)
            {
                txtFolioSolicitud.InnerHtml = vSolicitud.CL_SOLICITUD;
                txtNombreCandidato.InnerHtml = vSolicitud.NB_CANDIDATO_COMPLETO;
            }
        }

        private void CargarBateria()
        {
            PruebasNegocio nPruebas = new PruebasNegocio();

            var vBateria = nPruebas.ObtieneBateria(pIdBateria: vFlBateria).FirstOrDefault();

            if (vBateria != null)
            {
                if (vBateria.ESTATUS.Equals("TERMINADA"))
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Todas las pruebas en la secuencia para este folio de solicitud ya están completadas y no pueden volverse a ingresar", Entidades.Externas.E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    btnIniciarPrueba.Enabled = false;
                }
            }

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                txtMensajeBienvenida.InnerHtml = ContextoApp.IDP.MensajeBienvenidaPrueba.dsMensaje;
                

                if (Request.Params["T"] != null)
                {
                    vClTokenExterno = Guid.Parse(Request.Params["T"].ToString());
                    //ContextoUsuario.clTokenPruebas = vClTokenExterno;
                }

                if (Request.Params["idCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["idCandidato"].ToString());
                    CargarCandidato();
                }

                if (Request.Params["ID"] != null)
                {
                    vFlBateria = int.Parse(Request.Params["ID"].ToString());
                    //ContextoUsuario.idBateriaPruebas = vFlBateria;
                    CargarBateria();
                }
            }
        }
    }
}