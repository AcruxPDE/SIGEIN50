using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.PuntoDeEncuentro;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace SIGE.WebApp.PDE
{
    public partial class VisorDeComunicadosSoloLectura : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        VisorComunicadoNegocio negocio = new VisorComunicadoNegocio();
        public string archivo
        {
            set { ViewState["vs_vrn_Archivo"] = value; }
            get { return (string)ViewState["vs_vrn_Archivo"]; }

        }

        public int? idArchivo
        {
            set { ViewState["vs_idArchivo"] = value; }
            get { return (int?)ViewState["vs_idArchivo"]; }

        }


        private int vIdComunicado
        {
            get { return (int)ViewState["vs_vObtieneId"]; }
            set { ViewState["vs_vObtieneId"] = value; }
        }
        private List<E_OBTIENE_COMENTARIOS_COMUNICADO> ListaComentarios
        {
            get { return (List<E_OBTIENE_COMENTARIOS_COMUNICADO>)ViewState["vs_vObtieneComentario"]; }
            set { ViewState["vs_vObtieneComentario"] = value; }
        }

        private List<E_OBTIENE_COMUNICADO> ListaComunicados
        {
            get { return (List<E_OBTIENE_COMUNICADO>)ViewState["vs_vObtieneComunicado"]; }
            set { ViewState["vs_vObtieneComunicado"] = value; }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {

                //vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
                //vNbPrograma = ContextoUsuario.nbPrograma;
                if (Request.Params["IdComunicado"] != null)
                {
                    vIdComunicado = int.Parse(Request.Params["IdComunicado"]);
                    ListaComunicados = negocio.ObtenerComunicados(null, ContextoUsuario.oUsuario.ID_EMPLEADO_PDE);

                    var vComunicado = ListaComunicados.Where(w => w.ID_COMUNICADO == vIdComunicado).ToList().FirstOrDefault();

                    rtbTitulo.Text = vComunicado.NB_COMUNICADO;
                    content.InnerHtml = vComunicado.DS_COMUNICADO;
                    //rcbleido.Checked = (vComunicado.FG_LEIDO == "Sí") ? true : false;
                    rlvComentarios.DataSource = negocio.ObtenerComentarios_Comunicado(null, vIdComunicado);
                    grdComunicados.DataSource = ListaComunicados;
                    grdComunicados.Rebind();
                    grdComunicados.MasterTableView.FindItemByKeyValue("ID_COMUNICADO", vIdComunicado).Selected = true;
                    GridDataItem item = (GridDataItem)grdComunicados.SelectedItems[0];
                    var cont = grdComunicados.SelectedItems.Count;
                    if (cont > 0)
                    {
                        //rcbleido.Enabled = true;
                        int dataKey = int.Parse(item.GetDataKeyValue("ID_COMUNICADO").ToString());
                        var vComunicado2 = ListaComunicados.Where(w => w.ID_COMUNICADO == dataKey).ToList().FirstOrDefault();

                        rtbTitulo.Text = vComunicado2.NB_COMUNICADO;
                        content.InnerHtml = vComunicado2.DS_COMUNICADO;
                        //rcbleido.Checked = (vComunicado2.FG_LEIDO == "Sí") ? true : false;
                    }
                    if (vComunicado.FG_PRIVADO != 1)
                    {
                        //rbnComentario.Enabled = true;
                        rlvComentarios.Rebind();
                        //rlMensajePrivado.Visible = false;
                    }
                    else
                    {
                        //rbnComentario.Enabled = false;
                        //rlMensajePrivado.Visible = true;
                    }

                    archivo = vComunicado.NB_ARCHIVO;

                    archivo = vComunicado.NB_ARCHIVO;
                    if (vComunicado.ID_ARCHIVO_PDE != null)
                    {
                        if (archivo.Contains(".jpg"))
                        {
                            ArchivosAdjuntos.Visible = true;
                        }
                        if (archivo.Contains(".png"))
                        {

                            ArchivosAdjuntos.Visible = true;
                        }
                        if (archivo.Contains(".jpeg"))
                        {

                            ArchivosAdjuntos.Visible = true;
                        }
                        if (archivo.Contains(".pdf"))
                        {

                            ArchivosAdjuntos.Visible = true;
                        }
                        if (archivo.Contains(".mp4"))
                        {
                            VideoAdjunto.Visible = true;
                        }
                        else if (archivo.Contains(".mp3"))
                        {
                            VideoAdjunto.Visible = true;

                        }
                        else if (archivo.Contains(".avi"))
                        {
                            VideoAdjunto.Visible = true;

                        }
                        else if (archivo.Contains(".mov"))
                        {
                            VideoAdjunto.Visible = true;

                        }
                        else
                        {
                            //VideoAdjunto.Visible = false;
                            //ArchivosAdjuntos.Visible = false;

                        }
                        idArchivo = vComunicado.ID_ARCHIVO_PDE;
                    }
                    else
                    {
                        VideoAdjunto.Visible = false;

                        ArchivosAdjuntos.Visible = false;
                    }
                    if (vComunicado.TIPO_COMUNICADO == "I" || vComunicado.TIPO_COMUNICADO == "D")
                    {
                        Informacion.Visible = true;
                    }
                    else
                    {
                        Informacion.Visible = false;
                    }

                }

            }

        }


        protected void grdComunicados_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridDataItem item = (GridDataItem)grdComunicados.SelectedItems[0];
            var cont = grdComunicados.SelectedItems.Count;
            if (cont > 0)
            {
                //rcbleido.Enabled = true;
                int dataKey = int.Parse(item.GetDataKeyValue("ID_COMUNICADO").ToString());
                var vComunicado = ListaComunicados.Where(w => w.ID_COMUNICADO == dataKey).ToList().FirstOrDefault();

                rtbTitulo.Text = vComunicado.NB_COMUNICADO;
                content.InnerHtml = vComunicado.DS_COMUNICADO;
                //rcbleido.Checked = (vComunicado.FG_LEIDO == "Sí") ? true : false;

                if (vComunicado.FG_PRIVADO != 1)
                {
                    //rbnComentario.Enabled = true;
                    //rlMensajePrivado.Visible = false;
                    rlvComentarios.Rebind();
                }
                else
                {
                    //rlMensajePrivado.Visible = true;
                    ////rbnComentario.Enabled = false;
                    rlvComentarios.Rebind();
                }

                archivo = vComunicado.NB_ARCHIVO;
                if (vComunicado.ID_ARCHIVO_PDE != null)
                {
                    if (archivo.Contains(".jpg"))
                    {
                        ArchivosAdjuntos.Visible = true;
                    }
                    if (archivo.Contains(".png"))
                    {

                        ArchivosAdjuntos.Visible = true;
                    }
                    if (archivo.Contains(".jpeg"))
                    {

                        ArchivosAdjuntos.Visible = true;
                    }
                    if (archivo.Contains(".pdf"))
                    {

                        ArchivosAdjuntos.Visible = true;
                    }
                    if (archivo.Contains(".mp4"))
                    {
                        VideoAdjunto.Visible = true;
                    }
                    else if (archivo.Contains(".mp3"))
                    {
                        VideoAdjunto.Visible = true;

                    }
                    else if (archivo.Contains(".avi"))
                    {
                        VideoAdjunto.Visible = true;

                    }
                    else if (archivo.Contains(".mov"))
                    {
                        VideoAdjunto.Visible = true;

                    }
                    else
                    {
                        //VideoAdjunto.Visible = false;
                        //ArchivosAdjuntos.Visible = false;
                        //ClientScript.RegisterStartupScript(GetType(), "jsVisor", "AbrirVentanaArchivo();", true);
                        //Response.Redirect("VisorDeArchivos.aspx?IdArchivo=" + (vComunicado.ID_ARCHIVO_PDE));

                    }
                    idArchivo = vComunicado.ID_ARCHIVO_PDE;
                    string ventana = "window.radopen('VisorDeArchivos.aspx?IdArchivo=" + idArchivo + "'";
                    string AbrirVentana = ventana + ",'rwVerArchivo', document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), AbrirVentana, true);

                }
                else
                {
                    VideoAdjunto.Visible = false;
                    ArchivosAdjuntos.Visible = false;

                }
                if (vComunicado.TIPO_COMUNICADO == "I" || vComunicado.TIPO_COMUNICADO == "D")
                {
                    Informacion.Visible = true;
                }
                else
                {
                    Informacion.Visible = false;
                }


            }
        }

        protected void grdComunicados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ListaComunicados = negocio.ObtenerComunicados(null, ContextoUsuario.oUsuario.ID_EMPLEADO_PDE);
            grdComunicados.DataSource = ListaComunicados;

        }

        protected void rcbleido_CheckedChanged(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;


            GridDataItem item = (GridDataItem)grdComunicados.SelectedItems[0];
            int datakey = int.Parse(item.GetDataKeyValue("ID_COMUNICADO").ToString());
            var vComentario = ListaComunicados.Where(a => a.ID_COMUNICADO == (int)datakey).ToList().FirstOrDefault();
            if (vComentario.FG_LEIDO == "No")
            {
                E_RESULTADO res = negocio.ActualizarComunicadoNoLeido(datakey, (ContextoUsuario.oUsuario.ID_EMPLEADO_PDE), vClUsuario, vNbPrograma);
                string vMensaje = res.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                if (vMensaje == "ERROR" || vMensaje == "WARNING")
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, res.CL_TIPO_ERROR, pCallBackFunction: "");
                }

            }
            else
            {
                E_RESULTADO res = negocio.ActualizarComunicadoLeido(datakey, (ContextoUsuario.oUsuario.ID_EMPLEADO_PDE), vClUsuario, vNbPrograma);

                string vMensaje = res.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                if (vMensaje == "ERROR" || vMensaje == "WARNING")
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, res.CL_TIPO_ERROR, pCallBackFunction: "");
                }
            }
            grdComunicados.Rebind();
            item.Selected = true;

        }


        protected void rlvComentarios_NeedDataSource(object sender, RadListViewNeedDataSourceEventArgs e)
        {
            var cont = grdComunicados.SelectedItems.Count;
            if (cont > 0)
            {
                GridDataItem item = (GridDataItem)grdComunicados.SelectedItems[0];
                int dataKey = int.Parse(item.GetDataKeyValue("ID_COMUNICADO").ToString());
                rlvComentarios.DataSource = negocio.ObtenerComentarios_Comunicado(null, dataKey);

            }
        }
    }
}