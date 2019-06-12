using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Linq;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaAsignarRequisicion : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private string vNbUsuario;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int? vIdCandidato
        {
            get { return (int?)ViewState["vs_vpsc_id_candidato"]; }
            set { ViewState["vs_vpsc_id_candidato"] = value; }
        }

        private int? vIdSolicitud
        {
            get { return (int?)ViewState["vs_vcc_id_solicitud"]; }
            set { ViewState["vs_vcc_id_solicitud"] = value; }
        }

        public int? vIdRequisicion
        {
            get { return (int?)ViewState["vs_vIdRequisicion"]; }
            set { ViewState["vs_vIdRequisicion"] = value; }
        }

        public int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vNbUsuario = ContextoUsuario.oUsuario.NB_USUARIO;

            if (!IsPostBack)
            {
                if (Request.Params["IdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["IdCandidato"].ToString());
                }

                if (Request.Params["SolicitudId"] != null)
                {
                    vIdSolicitud = int.Parse(Request.Params["SolicitudId"].ToString());
                }

                if (Request.Params["IdEmpleado"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["IdEmpleado"].ToString());
                }

                SolicitudNegocio nSoilcitud = new SolicitudNegocio();
                if (vIdSolicitud != null)
                {
                    var oSolicitud = nSoilcitud.ObtieneSolicitudes(ID_SOLICITUD: vIdSolicitud).FirstOrDefault();

                    if (oSolicitud != null)
                    {
                        txtCandidato.InnerText = oSolicitud.NB_CANDIDATO_COMPLETO;
                        txtClaveSolicitud.InnerText = oSolicitud.CL_SOLICITUD;
                    }
                }
                else if (vIdEmpleado != null)
                {
                    SolicitudNegocio nSolicitud = new SolicitudNegocio();
                    E_RESULTADO vResultado = nSolicitud.InsertaCandidatoEmpleado(vIdEmpleado, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    var idCandidato = 0;
                    bool esNumero;

                    if (vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_CANDIDATO").FirstOrDefault() != null)
                    {
                        esNumero = int.TryParse(vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_CANDIDATO").FirstOrDefault().DS_MENSAJE, out idCandidato);
                        vIdCandidato = idCandidato;

                        var oSolicitud = nSoilcitud.ObtieneSolicitudes(ID_CANDIDATO: vIdCandidato).FirstOrDefault();

                        if (oSolicitud != null)
                        {
                            txtCandidato.InnerText = oSolicitud.NB_CANDIDATO_COMPLETO;
                            txtClaveSolicitud.InnerText = oSolicitud.CL_SOLICITUD;
                        }
                    }


                }

                if (Request.Params["IdRequisicion"] != null)
                {
                    vIdRequisicion = int.Parse(Request.Params["IdRequisicion"].ToString());

                    RequisicionNegocio nRequisicion = new RequisicionNegocio();
                    var vRequicion = nRequisicion.ObtieneRequisicion(pIdRequisicion: vIdRequisicion);
                    if (vRequicion != null)
                    {
                        rlbRequicion.DataSource = vRequicion;
                        rlbRequicion.DataValueField = "ID_REQUISICION";
                        rlbRequicion.DataTextField = "NO_REQUISICION";
                        rlbRequicion.DataBind();

                        rlbRequicion.SelectedValue = vIdRequisicion.ToString();
                    }
                }
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            int vIdRequisicion;

            if (int.TryParse(rlbRequicion.SelectedValue, out vIdRequisicion))
            {
                E_RESULTADO vResultado = new RequisicionNegocio().InsertarCandidatoRequisicion(vIdCandidato ?? 0, vIdEmpleado ?? 0, vIdRequisicion, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "onCloseWindows");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No se ha seleccionado la requisición.", Entidades.Externas.E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
        }
    }
}