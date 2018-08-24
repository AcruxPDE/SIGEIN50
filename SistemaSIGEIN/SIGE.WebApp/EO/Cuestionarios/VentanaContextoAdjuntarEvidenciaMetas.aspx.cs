using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.EO.Cuestionarios
{
    public partial class VentanaContextoAdjuntarEvidenciaMetas : System.Web.UI.Page
    {
        #region Variables
        List<E_DOCUMENTO> vLstDocumentos
        {
            get { return (List<E_DOCUMENTO>)ViewState["vs_LstDocumentos"]; }
            set { ViewState["vs_LstDocumentos"] = value; }
        }
        Guid? vIdItemFotografia
        {
            get { return (Guid?)ViewState["vs_vIdItemFotografia"]; }
            set { ViewState["vs_vIdItemFotografia"] = value; }
        }
        public int IdEvaluadoMeta
        {
            get { return (int)ViewState["vs_vidEvaluadoMeta"]; }
            set { ViewState["vs_vidEvaluadoMeta"] = value; }
        }
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        string vClRutaArchivosTemporales;
        public string vClUsuario;
        string vNbPrograma;

        SPE_OBTIENE_EVIDENCIAS_METAS_Result vXmlDocumentos;
        #endregion

        #region Funciones
        protected void AddDocumento(string pClTipoDocumento, RadAsyncUpload pFiDocumentos)
        {
            foreach (UploadedFile f in pFiDocumentos.UploadedFiles)
            {
                E_DOCUMENTO vDocumento = new E_DOCUMENTO()
                {
                    ID_ITEM = Guid.NewGuid(),
                    CL_TIPO_DOCUMENTO = pClTipoDocumento,
                    NB_DOCUMENTO = f.FileName,
                    FE_CREATED_DATE = DateTime.Now
                };

                vLstDocumentos.Add(vDocumento);

                vIdItemFotografia = vDocumento.ID_ITEM;
                f.InputStream.Close();
                f.SaveAs(String.Format(@"{0}\{1}", vClRutaArchivosTemporales, vDocumento.GetDocumentFileName()), true);
            }

            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();
        }
        protected void EliminarDocumento(string pIdItemDocumento)
        {
            E_DOCUMENTO d = vLstDocumentos.FirstOrDefault(f => f.ID_ITEM.ToString().Equals(pIdItemDocumento));

            if (d != null)
            {
                string vClRutaArchivo = Path.Combine(vClRutaArchivosTemporales, d.GetDocumentFileName());
                if (File.Exists(vClRutaArchivo))
                    File.Delete(vClRutaArchivo);
            }

            vLstDocumentos.Remove(d);
            grdDocumentos.Rebind();
        }
        protected void CargarDocumentos()
        {
            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();

            SPE_OBTIENE_EVIDENCIAS_METAS_Result vDocumento = neg.ObtieneEvidenciasMetasEvaluados(IdEvaluadoMeta);

            XElement vXmlDocumentos = vDocumento.XML_DOCUMENTOS != "" ? XElement.Parse(vDocumento.XML_DOCUMENTOS) : null;

            if (vLstDocumentos == null)
                vLstDocumentos = new List<E_DOCUMENTO>();
            if (vXmlDocumentos != null)
            {
                foreach (XElement item in (vXmlDocumentos.Elements("ITEM")))
                    vLstDocumentos.Add(new E_DOCUMENTO()
                    {
                        ID_ITEM = new Guid(UtilXML.ValorAtributo<string>(item.Attribute("ID_ITEM"))),
                        NB_DOCUMENTO = UtilXML.ValorAtributo<string>(item.Attribute("NB_DOCUMENTO")),
                        ID_DOCUMENTO = UtilXML.ValorAtributo<int>(item.Attribute("ID_DOCUMENTO")),
                        ID_ARCHIVO = UtilXML.ValorAtributo<int>(item.Attribute("ID_ARCHIVO")),
                        CL_TIPO_DOCUMENTO = UtilXML.ValorAtributo<string>(item.Attribute("CL_TIPO_DOCUMENTO")),
                        ID_PROCEDENCIA = UtilXML.ValorAtributo<int>(item.Attribute("ID_PROCEDENCIA")),
                        CL_PROCEDENCIA = UtilXML.ValorAtributo<string>(item.Attribute("CL_PROCEDENCIA"))
                    });

            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            PeriodoDesempenoNegocio neg = new PeriodoDesempenoNegocio();

            if (!IsPostBack)
            {
                if (Request.Params["pIdEvaluadoMeta"] != null)
                {
                    IdEvaluadoMeta = int.Parse(Request.QueryString["pIdEvaluadoMeta"]);

                }
                else
                {
                    IdEvaluadoMeta = 0;
                }
                vLstDocumentos = new List<E_DOCUMENTO>();
                var oMeta = neg.ObtieneMetas(pIdEvaluadoMeta: IdEvaluadoMeta).FirstOrDefault();

                if (oMeta != null)
                {
                    txtMeta.InnerText = oMeta.DS_META;
                }
                CargarDocumentos();
            }

            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";
            vClRutaArchivosTemporales = Server.MapPath(ContextoApp.ClRutaArchivosTemporales);


        }

        protected void btnAgregarDocumento_Click(object sender, EventArgs e)
        {
            AddDocumento(cmbTipoDocumento.SelectedValue, rauDocumento);
            grdDocumentos.Rebind();
        }

        protected void btnDelDocumentos_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem i in grdDocumentos.SelectedItems)
                EliminarDocumento(i.GetDataKeyValue("ID_ITEM").ToString());
        }

        protected void grdDocumentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdDocumentos.DataSource = vLstDocumentos;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            List<UDTT_ARCHIVO> vLstArchivos = new List<UDTT_ARCHIVO>();
            foreach (E_DOCUMENTO d in vLstDocumentos)
            {
                string vFilePath = Server.MapPath(Path.Combine(ContextoApp.ClRutaArchivosTemporales, d.GetDocumentFileName()));
                if (File.Exists(vFilePath))
                {
                    vLstArchivos.Add(new UDTT_ARCHIVO()
                    {
                        ID_ITEM = d.ID_ITEM,
                        ID_ARCHIVO = IdEvaluadoMeta,
                        NB_ARCHIVO = d.NB_DOCUMENTO,
                        FI_ARCHIVO = File.ReadAllBytes(vFilePath)
                    });
                }
            }
            PeriodoDesempenoNegocio nSolicitud = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = nSolicitud.InsertaActualizaEvidenciasMetas(IdEvaluadoMeta, vLstArchivos, vLstDocumentos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        }
    }

}