using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.Modulos_de_apoyo;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaConsultaInteligente : System.Web.UI.Page
    {
        #region Variables

        private int vIdCubo
        {
            get { return (int)ViewState["vs_vIdCubo"]; }
            set { ViewState["vs_vIdCubo"] = value; }
        }

        private int vIdArchivo
        {
            get { return (ViewState["vs_vIdArchivo"] == null ? 0 : (int)ViewState["vs_vIdArchivo"]); }
            set { ViewState["vs_vIdArchivo"] = value; }
        }

        private string vClUsuario
        {
            get { return (string)ViewState["vs_vClUsuario"]; }
            set { ViewState["vs_vClUsuario"] = value; }
        }

        private string vNbPrograma
        {
            get { return (string)ViewState["vs_vNbPrograma"]; }
            set { ViewState["vs_vNbPrograma"] = value; }
        }

        private List<E_OBTIENE_C_CONSULTA_INTELIGENTE> vLstConsultaInteligente
        {
            get { return (List<E_OBTIENE_C_CONSULTA_INTELIGENTE>)ViewState["vs_vLstConsultaInteligente"]; }
            set { ViewState["vs_vLstConsultaInteligente"] = value; }
        }

        #endregion

        #region Eventos Load

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Page.Request.QueryString["idCubo"] != null)
                {
                    vIdCubo = int.Parse(Page.Request.QueryString["idCubo"].ToString());
                    CargarDatos();
                }
            }

        }

        #endregion

        #region Eventos

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        #endregion

        #region Metodos y Funciones

        private void Guardar()
        {
            string Mensajes = string.Empty;
            Mensajes = ValidarCampos();
            if (Mensajes.Length > 0)
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, Mensajes, E_TIPO_RESPUESTA_DB.WARNING, pAlto: (120 + (2 * 16)));
                return;
            }
            int idArchivo = vIdArchivo;
            string nbCatalogo = "", nbArchivo = "", usuario = "", programa = "";
            byte[] fiArhivo = null;
            Guid idItem;
            CatalogoVistaInteligenteNegocio negocio = new CatalogoVistaInteligenteNegocio();
            nbCatalogo = txtNombre.Text;
            usuario = vClUsuario;
            programa = vNbPrograma;
            string rutaTemp, rutaArchivoTemp;
            rutaTemp = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);
            if (rauSubirArchivo.UploadedFiles.Count > 0)
            {
               // nbCatalogo = txtNombre.Text;         
                nbArchivo = rauSubirArchivo.UploadedFiles[0].GetName();
                idItem = Guid.NewGuid();
                //usuario = vClUsuario;
                //programa = vNbPrograma;
                //fiArhivo= rauSubirArchivo
                //string rutaTemp, rutaArchivoTemp;
               // rutaTemp = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);
                rutaArchivoTemp = Server.MapPath(Path.Combine(ContextoApp.ClRutaArchivosTemporales, rauSubirArchivo.UploadedFiles[0].GetName()));
                GuardarArchivoRutaAPP(rutaTemp);
                fiArhivo = File.ReadAllBytes(rutaArchivoTemp);
                negocio.InsertaConsultaInteligente(idArchivo, nbArchivo, fiArhivo, nbCatalogo, idItem, usuario, programa, 0);
                EliminarDocumentoTemporal(rutaArchivoTemp);
            }
            else if (vIdArchivo != 0)
            {
                nbArchivo = vLstConsultaInteligente[0].NB_ARCHIVO;
                idItem = (Guid)vLstConsultaInteligente[0].ID_ITEM;
                rutaArchivoTemp = Server.MapPath(Path.Combine(ContextoApp.ClRutaArchivosTemporales, vLstConsultaInteligente[0].NB_ARCHIVO));
                GuardarArchivoRutaAPP(rutaTemp);
                fiArhivo = File.ReadAllBytes(rutaArchivoTemp);
                negocio.InsertaConsultaInteligente(idArchivo, nbArchivo, fiArhivo, nbCatalogo, idItem, usuario, programa, 0);
                EliminarDocumentoTemporal(rutaArchivoTemp);
            }
            string script = "GetRadWindow().close();";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, true);

        }

        private string ValidarCampos()
        {
            string Mensajes = string.Empty;
            if (txtNombre.Text == string.Empty)
                Mensajes = "Falta nombre vista inteligente";
            if (rauSubirArchivo.UploadedFiles.Count == 0)
            {
                if (vIdArchivo == 0)
                {
                    Mensajes = Mensajes + @"\n Falta seleccionar un archivo ó el archivo no contiene el formato correcto (.twbx)";
                }
            }

            return Mensajes;
        }

        private void EliminarDocumentoTemporal(string ruta)
        {
            if (File.Exists(ruta))
                File.Delete(ruta);
        }

        private void GuardarArchivoRutaAPP(string rutaTemp)
        {
            if (rauSubirArchivo.UploadedFiles.Count > 0)
            {
                foreach (UploadedFile f in rauSubirArchivo.UploadedFiles)
                {
                    f.InputStream.Close();
                    f.SaveAs(String.Format(@"{0}\{1}", rutaTemp, f.FileName), true);
                }
            }
            else
            {
                var urlfile = Path.Combine(rutaTemp, vLstConsultaInteligente[0].NB_ARCHIVO);
                MemoryStream ms = new MemoryStream(vLstConsultaInteligente[0].FI_ARCHIVO);
                FileStream file = new FileStream(urlfile, FileMode.Create, FileAccess.Write);
                ms.WriteTo(file);
                file.Close();
                ms.Close();
            }
        }

        private void CargarDatos()
        {
            CatalogoVistaInteligenteNegocio negocio = new CatalogoVistaInteligenteNegocio();
            vLstConsultaInteligente = negocio.ObtieneConsultaIntligente(vIdCubo, null, null, null, null);
            txtNombre.Text = vLstConsultaInteligente[0].NB_CATALOGO;
            vIdArchivo = vLstConsultaInteligente[0].ID_ARCHIVO;
            rbArchivo.Text = vLstConsultaInteligente[0].NB_ARCHIVO;
        }

        #endregion
    }
}