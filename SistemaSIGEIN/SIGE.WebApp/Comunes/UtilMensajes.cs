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
    }
}