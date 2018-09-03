using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.Modulos_de_apoyo;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class CatalogoConsultaInteligente : System.Web.UI.Page
    {
        #region Variables

        private List<E_OBTIENE_C_CONSULTA_INTELIGENTE> lstConsultaInteligente
        {
            get { return (List<E_OBTIENE_C_CONSULTA_INTELIGENTE>)ViewState["vs_vLstConsultaInteligente"]; }
            set { ViewState["vs_vLstConsultaInteligente"] = value; }
        }

        public bool vAgregar;
        public bool vEditar;
        public bool vEliminar;

        #endregion

        #region Eventos Load

        protected void Page_Load(object sender, EventArgs e)
        {
              if (ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo == "1")
            {
                SeguridadProcesos();
            }
              else
              {
                  var myUrl = ResolveUrl("~/Logon.aspx");
                  UtilMensajes.MensajeResultadoDB(rwmAlertas, ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo, E_TIPO_RESPUESTA_DB.WARNING);
                  Response.Redirect(ContextoUsuario.nbHost + myUrl);
              }

        }

        #endregion

        #region Eventos

        protected void SeguridadProcesos()
        {
            btnAgregar.Enabled = vAgregar = ContextoUsuario.oUsuario.TienePermiso("P.A");
            btnEditar.Enabled = vEditar = ContextoUsuario.oUsuario.TienePermiso("P.B");
            btnEliminar.Enabled = vEliminar = ContextoUsuario.oUsuario.TienePermiso("P.C");
        }

        protected void rdgConsultasInteligentes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ObtenerCatalogoVistaInteligente();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarArchivo();
            UtilMensajes.MensajeResultadoDB(rwmAlertas, "Se ha eliminado correctamente la vista inteligente", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pAlto: (120 + ( 1 * 16)));
            rdgConsultasInteligentes.Rebind();
        }

        protected void imgBtnArchivo_Click(object sender, Telerik.Web.UI.ImageButtonClickEventArgs e)
        {
            RadScriptManager.GetCurrent(Page);
            RadAjaxManager.GetCurrent(Page);
            int idArchivo =int.Parse(hdnId.Value.ToString());
           // List<SPE_OBTIENE_C_CONSULTA_INTELIGENTE_Result> lstConsultaInteligente = new List<SPE_OBTIENE_C_CONSULTA_INTELIGENTE_Result>();
            CatalogoVistaInteligenteNegocio negocio = new CatalogoVistaInteligenteNegocio();
            lstConsultaInteligente= negocio.ObtieneConsultaIntligente(null,idArchivo,null,null,null);    
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + lstConsultaInteligente[0].NB_ARCHIVO);
            Response.BinaryWrite(lstConsultaInteligente[0].FI_ARCHIVO);
            Response.Flush();
            Response.End();
            
        }        

        #endregion

        #region Metodos o Funciones

        private void ObtenerCatalogoVistaInteligente()
        {
            CatalogoVistaInteligenteNegocio negocio = new CatalogoVistaInteligenteNegocio();
            rdgConsultasInteligentes.DataSource= negocio.ObtieneConsultaIntligente(null,null,null,null,null);
        }

        private void EliminarArchivo()
        {
            CatalogoVistaInteligenteNegocio negocio = new CatalogoVistaInteligenteNegocio();            
            int idArchivo =int.Parse(hdnId.Value.ToString());
            negocio.InsertaConsultaInteligente(idArchivo,null,null,null,null,null,null,1);            
        }

        #endregion
    }
}