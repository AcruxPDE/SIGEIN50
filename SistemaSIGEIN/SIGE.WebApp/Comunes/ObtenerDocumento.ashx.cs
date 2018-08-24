using SIGE.Entidades;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SIGE.WebApp.Comunes
{
    /// <summary>
    /// Summary description for ObtenerDocumento
    /// </summary>
    public class ObtenerDocumento : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string vClRutaArchivosTemporales = context.Server.MapPath(ContextoApp.ClRutaArchivosTemporales);

            string vQsClTipoArchivo = context.Request.QueryString["TipoArchivoCl"];
            string vQsIdArchivo = context.Request.QueryString["ArchivoId"];
            string vQsNbArchivo = context.Request.QueryString["ArchivoNb"];
            string vQsNbArchivoDescarga = context.Request.QueryString["ArchivoDescargaNb"];

            int vIdArchivo = 0;

            byte[] vArchivoEnBinario = null;

            if (int.TryParse(vQsIdArchivo, out vIdArchivo))
            {
                DocumentoNegocio nDocumento = new DocumentoNegocio();
                SPE_OBTIENE_C_DOCUMENTO_Result vDocumento = nDocumento.ObtieneDocumento(pIdArchivo: vIdArchivo).FirstOrDefault();
                if (vDocumento != null)
                {
                    vArchivoEnBinario = vDocumento.FI_ARCHIVO;
                    vQsNbArchivoDescarga = vDocumento.NB_DOCUMENTO;
                }
            }
            else
            {
                using (FileStream stream = new FileStream(Path.Combine(vClRutaArchivosTemporales, vQsNbArchivo), FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        vArchivoEnBinario = reader.ReadBytes((int)stream.Length);
                    }
                }
            }

            if (vArchivoEnBinario != null)
            {
                if (String.IsNullOrWhiteSpace(vQsNbArchivoDescarga))
                    vQsNbArchivoDescarga = vQsNbArchivo;
                context.Response.ContentType = "application/octet-stream";
                context.Response.AddHeader("Content-Length", vArchivoEnBinario.Length.ToString());
                context.Response.AddHeader("Content-Disposition", String.Format("filename={0}", vQsNbArchivoDescarga));
                context.Response.BinaryWrite(vArchivoEnBinario);
                context.Response.Flush();
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}