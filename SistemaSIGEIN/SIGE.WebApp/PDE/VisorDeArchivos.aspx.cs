using SIGE.Entidades;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class VisorDeArchivos : System.Web.UI.Page
    {
        public string vIdEmpleado;
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
                if (Request.Params["IdArchivo"] != null || Request.Params["IdArchivo"] != "")
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
                    string tipo;
                    if (vNombreArchivo.Contains(".pdf"))
                    {

                        ArchivoAdjunto.Visible = false;
                        tipo = Response.ContentType = "application/pdf";
                        Response.ClearHeaders();
                        Response.ClearContent();
                        Response.Charset = "";
                        Response.AddHeader("Content-Type", tipo);
                        memoryStream.Write(archivo, 0, archivo.Length);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.Close();
                        Response.End();
                    }
                    else if (vNombreArchivo.Contains(".jpg"))
                    {
                        ArchivoAdjunto.Visible = true;
                        Response.ContentType = "image/jpg";
                        memoryStream.Write(archivo, 0, archivo.Length);
                        Bitmap bit = new Bitmap(memoryStream);
                        bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                    }
                    else if (vNombreArchivo.Contains(".png"))
                    {
                        ArchivoAdjunto.Visible = true;
                        Response.ContentType = "image/png";
                        memoryStream.Write(archivo, 0, archivo.Length);
                        Bitmap bit = new Bitmap(memoryStream);
                        bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                    }
                    else if (vNombreArchivo.Contains(".jpeg"))
                    {
                        ArchivoAdjunto.Visible = true;
                        Response.ContentType = "image/jpeg"; memoryStream.Write(archivo, 0, archivo.Length);
                        Bitmap bit = new Bitmap(memoryStream);
                        bit.Save(Response.OutputStream, ImageFormat.Jpeg);
                    }
                    else
                    {
                        
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Length", archivo.Length.ToString());
                        Response.AddHeader("Content-Disposition", String.Format("filename={0}", vNombreArchivo));
                        Response.BinaryWrite(archivo);
                        Response.Flush();
                        Response.End();

                    }
                }


            }

        }
    }
}

