using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace SIGE.WebApp.Comunes
{
    public class ContextoCorreo
    {
        
        public static string asunto
        {
            get
            {
                return "Solicitud de aprobación de documento";
            }
        }

      
        public static string mje
        {
            get
            {
                return "Se solicita la aprobación de constancia de estudios para el empleado: ";
            }
        }


        /// <summary>
        /// Aprobación o rechazo de documento
        /// </summary>

        public static string aprobado
        {
            get
            {
                return "Tu documento ha sido aprobado.";
            }
        }

        public static string rechazado
        {
            get
            {
                return "Tu documento ha sido rechazado, verifica tu información o pasa a la oficina de Recursos Humanos para más información";
            }
        }

        public static string asuntoRespuesta
        {
            get
            {
                return "Respuesta a solicitud";
            }
        }

        public static string CuerpoMensaje
        {

            get
            {
                return "<html><head></head><body><div><center><div align = \"center\" ><img width = \"610\" height = \"115\" src = \"{0}\"  ></div></center><br><table width = \"490\" align =\"center\" border =\"0\" cellspacing =\"0\" cellpadding =\"0\" ><tbody><tr><td align = \"center\" style =\"color: rgb(128, 128, 128); font - family:Verdana,Geneva,sans - serif; font - size:14px\" ></td></tr><tr><td style=\"text-align: justify;font-family:Verdana, Geneva, sans-serif; font-size:11px; color:#808080;\"><div>{1}</div><br/><div>&nbsp;&nbsp;&nbsp;&nbsp;{2}</div><br/><div>{3}</div><div></div></td></tr></tbody></table><center><div align = \"center\" ><img width=\"610\" height =\"150\" src = \"{4}\"  tabindex =\"0\" ></div></center></div></body></html>";
            }
        }

        public static string encabezado
        {

            get
            {
                return "https://drive.google.com/drive/my-drive/LogotipoNombre.png";
            }
        }

        public static string pie
        {

            get
            {
                return "https://drive.google.com/drive/my-drive/LogotipoNombre.png";
            }
        }

    }
}
