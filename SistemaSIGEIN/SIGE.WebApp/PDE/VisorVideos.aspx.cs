using SIGE.Entidades;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.PDE
{
    public partial class VisorVideos : System.Web.UI.Page
    {

        public string  vIdEmpleado;
        public string vNbPrograma;
        public string vClUsuario;

        public int vIdArchivos
        {
            set { ViewState["vs_vrn_Archivos"] = value; }
            get { return (int)ViewState["vs_vrn_Archivos"]; }
        }

        public Byte[] archivo
        {
            set { ViewState["vs_vrn_Archivo"] = value; }
            get { return (Byte[])ViewState["vs_vrn_Archivo"]; }

        }
        public string vNombreArchivo
        {
            set { ViewState["vs_vip_NombreArchivo"] = value; }
            get { return (string)ViewState["vs_vip_NombreArchivo"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            vIdEmpleado = ContextoUsuario.oUsuario.ID_EMPLEADO_PDE.ToString();
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;

            if (!IsPostBack)
            {
                ConfiguracionNotificacionNegocio negocio = new ConfiguracionNotificacionNegocio();
                if (Request.Params["IdArchivo"] != null)
                {
                    vIdArchivos = int.Parse(Request.Params["IdArchivo"]);
                    List<SPE_OBTIENE_ARCHIVOS_PDE_Result> ListaArchivos = new List<SPE_OBTIENE_ARCHIVOS_PDE_Result>();
                    MenuNegocio nego = new MenuNegocio();
                    ListaArchivos = nego.ObtenerArchivos(vIdArchivos);
                    foreach (SPE_OBTIENE_ARCHIVOS_PDE_Result item in ListaArchivos)
                    {
                        vNombreArchivo = item.NB_ARCHIVO;
                        archivo = item.FI_ARCHIVO;
                      
                    }


                    MemoryStream memoryStream = new MemoryStream();

                 
                    if (vNombreArchivo.Contains(".mp3"))
                    {
                        rmpVideo.Visible = true;
                        var url = HttpContext.Current.Server.MapPath("~") + "\\App_Data\\RadUploadTemp\\";
                        var filename = vNombreArchivo;
                        var urlfile = Path.Combine(url, filename);
                        MemoryStream ms = new MemoryStream(archivo);
                        FileStream file = new FileStream(urlfile, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(file);
                        file.Close();
                        ms.Close();
                        rmpVideo.Source = "../App_Data/RadUploadTemp/" + filename;
                    }
                    else if (vNombreArchivo.Contains(".mp4"))
                    {
                        //RadMediaPlayer player = new RadMediaPlayer();
                        rmpVideo.Visible = true;
                        // ArchivoAdjunto.Visible = false;
                        // var url = HttpContext.Current.Request.Url.Authority;
                        var url = HttpContext.Current.Server.MapPath("~") + "\\App_Data\\RadUploadTemp\\";
                        // string ruta = url + "\\App_Data\\" + vNombreArchivo + DateTime.Now;
                        //var result = Path.GetTempPath();
                        // var url = HttpContext.Current.Request.Url.Authority;
                        //var filename = vNombreArchivo;
                        //var urlfile = Path.Combine(result, filename);
                        var filename = vNombreArchivo;
                        var urlfile = Path.Combine(url, filename);
                        MemoryStream ms = new MemoryStream(archivo);
                        FileStream file = new FileStream(urlfile, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(file);
                        file.Close();
                        ms.Close();
                        rmpVideo.Source = "../App_Data/RadUploadTemp/" + filename;
                        //MediaPlayerSource mpSource = new MediaPlayerSource();
                        //mpSource.Path = urlfile;
                        //player.Sources.Add(mpSource);
                        //player.StartVolume = 90;
                        //player.OnClientReady = "playerReady";
                        ////player.FullScreen = true;
                        //player.HDActive = true;
                        //player.VolumeButtonToolTip = "Zvuk";
                        //player.FullScreenButtonToolTip = "Veći ekran";
                        //player.Skin = "Metro";
                        //FileStream fileStream = File.Create("C:\\", (int)archivo.Length);
                        //// Initialize the bytes array with the stream length and then fill it with data
                        // byte[] bytesInStream = new byte[archivo.Length];
                        //archivo.Read(bytesInStream, 0, bytesInStream.Length);
                        //// Use write method to write to the file specified above
                        //fileStream.Write(bytesInStream, 0, bytesInStream.Length);

                    }
                    else if (vNombreArchivo.Contains(".avi"))
                    {
                        rmpVideo.Visible = true;
                        var url = HttpContext.Current.Server.MapPath("~") + "\\App_Data\\RadUploadTemp\\";
                        var filename = vNombreArchivo;
                        var urlfile = Path.Combine(url, filename);
                        MemoryStream ms = new MemoryStream(archivo);
                        FileStream file = new FileStream(urlfile, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(file);
                        file.Close();
                        ms.Close();
                        rmpVideo.Source = "../App_Data/RadUploadTemp/" + filename;
                    }
                    else if (vNombreArchivo.Contains(".mov"))
                    {
                        rmpVideo.Visible = true;
                        var url = HttpContext.Current.Server.MapPath("~") + "\\App_Data\\RadUploadTemp\\";
                        var filename = vNombreArchivo;
                        var urlfile = Path.Combine(url, filename);
                        MemoryStream ms = new MemoryStream(archivo);
                        FileStream file = new FileStream(urlfile, FileMode.Create, FileAccess.Write);
                        ms.WriteTo(file);
                        file.Close();
                        ms.Close();
                        rmpVideo.Source = "../App_Data/RadUploadTemp/" + filename;
                    }

                        //memoryStream.Write(archivo, 0, archivo.Length);
                        //Bitmap bit = new Bitmap(memoryStream);
                        //bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                    }
                 
                }
              
            }

        }
    }
