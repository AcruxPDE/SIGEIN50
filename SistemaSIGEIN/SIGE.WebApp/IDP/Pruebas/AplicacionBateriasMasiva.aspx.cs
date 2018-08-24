using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.IDP.Pruebas
{
    public partial class AplicacionBateriasMasiva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnIniciarPrueba_Click(object sender, EventArgs e)
        {
            if (txtFolio.Text != "")
            {
                SolicitudNegocio nSoilcitud = new SolicitudNegocio();
                var oSolicitud = nSoilcitud.ObtieneSolicitudes(CL_SOLICITUD: txtFolio.Text).FirstOrDefault();
                if (oSolicitud != null)
                {
                    PruebasNegocio nPruebas = new PruebasNegocio();
                    var vBateria = nPruebas.ObtieneBateria(pIdCandidato: oSolicitud.ID_CANDIDATO.ToString()).FirstOrDefault();
                    if (vBateria != null)
                    {
                        if (vBateria.ESTATUS != "TERMINADA")
                        {
                            if (vBateria.CL_TOKEN != null && vBateria.ID_BATERIA != null)
                            {
                                var myUrl = ResolveUrl("~/IDP/Pruebas/PruebaBienvenida.aspx");
                                Response.Redirect(myUrl + "?ID=" + vBateria.ID_BATERIA.ToString() + "&T=" + vBateria.CL_TOKEN.ToString() + "&idCandidato=" + oSolicitud.ID_CANDIDATO.ToString());
                            }
                            else
                                UtilMensajes.MensajeResultadoDB(rnMensaje, "Ocurrio un error con la batería.", E_TIPO_RESPUESTA_DB.ERROR, 420, 170, "");
                        }
                        else
                            UtilMensajes.MensajeResultadoDB(rnMensaje, "Todas las pruebas en la secuencia para este folio de solicitud ya están completadas y no pueden volverse a ingresar.", E_TIPO_RESPUESTA_DB.ERROR, 420, 170, "");
                    }
                    else
                        UtilMensajes.MensajeResultadoDB(rnMensaje, "El candidato no cuenta con una batería de pruebas creada.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
                }
                else
                UtilMensajes.MensajeResultadoDB(rnMensaje, "El folio de solicitud ingresado no existe.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
            }
            else
                UtilMensajes.MensajeResultadoDB(rnMensaje,"Ingresa el folio de solicitud para continuar.", E_TIPO_RESPUESTA_DB.ERROR,400,150,"");
        }
    }
}