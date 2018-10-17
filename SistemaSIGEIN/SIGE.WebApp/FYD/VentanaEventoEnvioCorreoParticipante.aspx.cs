using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
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
    public partial class VentanaEventoEnvioCorreo : System.Web.UI.Page
    {
        #region Variables

        private int vIdEvento
        {
            get { return (int)ViewState["vs_veec_id_evento"]; }
            set { ViewState["vs_veec_id_evento"] = value; }
        }

        private E_EVENTO oEvento
        {
            get { return (E_EVENTO)ViewState["vs_veec_evento"]; }
            set { ViewState["vs_veec_evento"] = value; }
        }

        private List<E_EVENTO_PARTICIPANTE> oListaParticipante {
            get { return (List<E_EVENTO_PARTICIPANTE>)ViewState["vs_veec_lista_participantes"]; }
            set { ViewState["vs_veec_lista_participantes"] = value; }
        }

        private List<E_EVENTO_CALENDARIO> oCalendario
        {
            get { return (List<E_EVENTO_CALENDARIO>)ViewState["vs_veec_lista_calendario"]; }
            set { ViewState["vs_veec_lista_calendario"] = value; }
        }

        private string vMensajeCorreo {
            get { return (string)ViewState["vs_veec_mensaje_correo"]; }
            set { ViewState["vs_veec_mensaje_correo"] = value; }
        }

        private int? vIdRol;

        #endregion

        #region Funciones

        private void cargarEvento()
        {
            EventoCapacitacionNegocio neg = new EventoCapacitacionNegocio();
            oEvento = neg.ObtieneEventos(ID_EVENTO: vIdEvento).FirstOrDefault();
          
            if (oEvento != null)
            {
                txtNombreEvento.InnerText = oEvento.NB_EVENTO;
                txtLugar.InnerText = oEvento.DS_LUGAR;
                txtFechaInicial.InnerText = oEvento.FE_INICIO.ToString("dd/MM/yyyy");
                txtFechaTermino.InnerText = oEvento.FE_TERMINO.ToString("dd/MM/yyyy");

                oListaParticipante = neg.ObtieneParticipanteEvento(ID_EVENTO: vIdEvento, pID_ROL: vIdRol);
                oCalendario = neg.ObtieneEventoCalendario(ID_EVENTO: vIdEvento);

                vMensajeCorreo = ContextoApp.FYD.MensajeCorreoParticipantes.dsMensaje;

                vMensajeCorreo = vMensajeCorreo.Replace("[NB_EVENTO]", oEvento.NB_EVENTO);
                vMensajeCorreo = vMensajeCorreo.Replace("[DS_LUGAR]", oEvento.DS_LUGAR);
                vMensajeCorreo = vMensajeCorreo.Replace("[FE_INICIO]", oEvento.FE_INICIO.ToString("dd/MM/yyyy"));
                vMensajeCorreo = vMensajeCorreo.Replace("[FE_TERMINO]", oEvento.FE_TERMINO.ToString("dd/MM/yyyy"));
                vMensajeCorreo = vMensajeCorreo.Replace("[TABLA_CALENDARIO]", ConvertToHTMLTable());

                lMensaje.InnerHtml = vMensajeCorreo;

                if (DateTime.Now.Date > oEvento.FE_TERMINO)
                {
                    btnAceptar.Enabled = false;
                    lblCaducidad.Visible = true;
                }

            }
        }

        private string ConvertToHTMLTable()
        {
            string Table = "<Table border=\"1\">";
            string aux = "";
            int i = 1;
            bool alternateColor = false;

            if (oCalendario.Count() >= 1)
            {
                
                Table = Table + "<tr style=\"padding: 5px;\">" +
                                    "<th style=\"padding: 5px;\">#</th>" +
                                    "<th style=\"padding: 5px;\">Fecha</th>" +
                                    "<th style=\"padding: 5px;\">Hora inicio</th>" +
                                    "<th style=\"padding: 5px;\">Hora final</th>" +
                                "</tr>";

                foreach (E_EVENTO_CALENDARIO item in oCalendario)
                {

                    if (alternateColor)
                    {
                        aux = "<tr style=\"padding: 5px; background-color:#E6E6FA\">";
                    }
                    else
                    {
                        aux = "<tr style=\"padding: 5px;\">";
                    }

                    aux = aux + "<td style=\"padding: 5px;\">" + i.ToString() + "</td>";
                    aux = aux + "<td style=\"padding: 5px;\">" + item.FE_INICIAL.ToString("dd/MM/yyyy") + "</td>";
                    aux = aux + "<td style=\"padding: 5px;\">" + item.FE_INICIAL.ToShortTimeString() + "</td>";
                    aux = aux + "<td style=\"padding: 5px;\">" + item.FE_FINAL.ToShortTimeString() + "</td>";                    

                    alternateColor = !alternateColor;
                    aux = aux + "</tr>";
                    Table = Table + aux;
                    i++;
                }

                Table = Table + "</Table>";
            }
            else
            {
                Table = "<b>(No hay datos)</b>";
            }

            return Table;
        }

        private void EnviarCorreos()
        {
            string mensaje = vMensajeCorreo;
            int vEnviados = 0;
            int vNoEnviados = 0;

            foreach (E_EVENTO_PARTICIPANTE item in oListaParticipante)
            {
                try
                {
                    Mail mail = new Mail(ContextoApp.mailConfiguration);
                    mensaje = vMensajeCorreo;
                    mensaje = mensaje.Replace("[Participante]", item.NB_PARTICIPANTE);
                    mail.addToAddress(item.CL_CORREO_ELECTRONICO, item.NB_PARTICIPANTE);
                    mail.Send("Evento", mensaje);

                    vEnviados++;
                }
                catch (Exception)
                {
                    vNoEnviados++;                    
                }                
            }

            UtilMensajes.MensajeResultadoDB(rwmMensaje, "Termino el proceso de envio. " + vEnviados.ToString() + " correo(s) enviado(s), " + vNoEnviados + " correo(s) no enviado(s).", E_TIPO_RESPUESTA_DB.SUCCESSFUL);

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {
                if (Request.Params["IdEvento"] != null)
                {
                    vIdEvento = int.Parse(Request.Params["IdEvento"]);
                    cargarEvento();
                }
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            //FormacionYDesarrollo fyd = new FormacionYDesarrollo();
          
            ////Te invitamos a participar en el siguiente evento:</p><br /><p>Evento de capacitación: <label style=\"color: blue;\">[NB_EVENTO]</label></p><p>Lugar: <label style=\"color: blue;\">[DS_LUGAR]</label></p><p>Fecha de inicio: <label style=\"color: blue;\">[FE_INICIO]</label></p><p>Fecha de término: <label style=\"color: blue;\">[FE_TERMINO]</label></p><p>[TABLA_CALENDARIO]</p>";
            //ContextoApp.FYD = fyd;
            //ContextoApp.SaveConfiguration("ADMIN", "VentanaEventoEnvioCorreo.aspx");
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            EnviarCorreos();
        }
    }
}