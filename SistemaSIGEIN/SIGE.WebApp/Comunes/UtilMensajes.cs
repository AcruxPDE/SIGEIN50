using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public static class UtilMensajes
    {

        public static void MensajeResultadoDB(RadWindowManager pWindow, string pMensaje, E_TIPO_RESPUESTA_DB pTipoMensaje, int pAncho = 400, int pAlto = 150, string pCallBackFunction = "closeWindow")
        {
            string vMensaje = "";
            string vUrlImage = "/Assets/images/";
            switch (pTipoMensaje)
            {
                case E_TIPO_RESPUESTA_DB.SUCCESSFUL:
                    vUrlImage += "Exito.png";
                    vMensaje = "Aviso";
                    break;
                case E_TIPO_RESPUESTA_DB.WARNING:
                    vUrlImage += "Advertencia.png";
                    vMensaje = "Aviso";
                    pCallBackFunction = null;
                    break;
                case E_TIPO_RESPUESTA_DB.ERROR:
                    vUrlImage += "Error.png";
                    vMensaje = "Aviso";
                    pCallBackFunction = null;
                    break;
                case E_TIPO_RESPUESTA_DB.WARNING_WITH_FUNCTION:
                    vUrlImage += "Advertencia.png";
                    vMensaje = "Aviso";
                    break;
                default:
                    break;
            }

            pWindow.RadAlert(pMensaje.Replace("'", "\""), pAncho, pAlto, vMensaje, pCallBackFunction, vUrlImage);
        }

        public static void MensajeDB(RadWindowManager pWindow, string pMensaje, E_TIPO_RESPUESTA_DB pTipoMensaje, int pAncho = 400, int pAlto = 150, string pCallBackFunction = "closeWindow")
        {
            string vMensaje = "";
            string vUrlImage = "/Assets/images/";
            switch (pTipoMensaje)
            {
                case E_TIPO_RESPUESTA_DB.SUCCESSFUL:
                    vUrlImage += "Exito.png";
                    vMensaje = "Aviso";
                    break;
                case E_TIPO_RESPUESTA_DB.WARNING:
                    vUrlImage += "Advertencia.png";
                    vMensaje = "Aviso";
                    break;
                case E_TIPO_RESPUESTA_DB.ERROR:
                    vUrlImage += "Error.png";
                    vMensaje = "Aviso";
                    pCallBackFunction = null;
                    break;
                case E_TIPO_RESPUESTA_DB.WARNING_WITH_FUNCTION:
                    vUrlImage += "Advertencia.png";
                    vMensaje = "Aviso";
                    break;
                default:
                    break;
            }

            pWindow.RadConfirm(pMensaje.Replace("'", "\""), pCallBackFunction, pAncho, pAlto, null, vMensaje, vUrlImage);
        }
        public static void DisplayMessageRadNotification(int iconoamostrar, string mensaje, RadWindowManager rnMensaje, RadAjaxManager RadAjaxManager1 = null, string tipo = "radalert", int? ancho = 330, int? alto = 180, string titulo = "")
        {
            string icono = "";//
            string text = "";
            mensaje = mensaje.Replace("'", "");
            if (RadAjaxManager1 != null)
                RadAjaxManager1.EnableAJAX = false;
            switch (iconoamostrar)
            {
                case -1:
                    icono = "warning";
                    break;
                case 0:
                    icono = "delete";
                    break;
                case 1:
                    icono = "ok";
                    break;
            }
            switch (tipo)
            {
                case "radalert":
                    if (titulo == "")
                        titulo = "Alerta";
                    rnMensaje.RadAlert(mensaje, ancho, alto, titulo, null, icono);
                    //rnMensaje.RadAlert(mensaje, ancho, alto, titulo, "alertCallBackFn", icono);
                    break;
                case "radconfirm":
                    if (titulo == "")
                        titulo = "Confirmación";
                    rnMensaje.RadConfirm(mensaje, null, ancho, alto, null, titulo, icono);
                    break;
                case "radprompt":
                    if (titulo == "")
                        titulo = "Captura";
                    rnMensaje.RadPrompt(mensaje, "promptCallBackFn", ancho, alto, null, titulo, "42");
                    break;
            }
            //if (mensaje == "")
            //    rnMensaje.Text = text;
            //else
            //    rnMensaje.Text = mensaje;
            // rnMensaje.ContentIcon = icono;
            // rnMensaje.VisibleOnPageLoad = true;

            if (RadAjaxManager1 != null)
                RadAjaxManager1.EnableAJAX = true;
        }
    }
}