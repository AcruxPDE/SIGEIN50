using SIGE.Entidades.PuntoDeEncuentro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.Data;
using System.Configuration;
using SIGE.Negocio.AdministracionSitio;

namespace SIGE.WebApp.PDE
{
    public partial class MenuPDE1 : System.Web.UI.Page
    {
        private string vClUsuario;
        private string xmlPuestoEmpleado;
        private XElement xmlEmpleado;
        private string vIdEmpleado;
        private string vNbPrograma;
        bool vEsDeRRHH;
      


        E_OBTIENE_PUESTO_EMPLEADOS vNotificacionAdministrar
        {
            get { return (E_OBTIENE_PUESTO_EMPLEADOS)ViewState["vs_vConfiguracionNotificacionAdministrar"]; }
            set { ViewState["vs_vConfiguracionNotificacionAdministrar"] = value; }
        }

        public int? vCantidadPendientes
        {
            set { ViewState["vs_vip_Cantidad_Pendientes"] = value; }
            get { return (int?)ViewState["vs_vip_Cantidad_Pendientes"]; }
        }


        public string url
        {
            set { ViewState["vs_url"] = value; }
            get { return (string)ViewState["vs_url"]; }
        }


        public string vTipoTransaccion
        {
            set { ViewState["vs_vTipoTransaccion"] = value; }
            get { return (string)ViewState["vs_vTipoTransaccion"]; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            AdscripcionesNegocio negocioa = new AdscripcionesNegocio();
            string adscripcionVisible = negocioa.SeleccionaAdscripcion().ToString();
            if (adscripcionVisible != "No")
            {
                vTipoTransaccion = "49";

            }
            else
            {
                vTipoTransaccion = "50";
            }

            // vServidor = HttpContext.Current.Request.Url.Authority;
          //  UtilMensajes.MensajeResultadoDB(rnMensaje, "direccion: " + url, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            url = Request.Url.GetLeftPart(UriPartial.Authority) + Page.ResolveUrl("VisorComunicados.aspx");
           // urlrel = "~/VisorComunicados.aspx";
            //vServidor = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
            vNotificacionAdministrar = new E_OBTIENE_PUESTO_EMPLEADOS();
            vIdEmpleado = ContextoUsuario.oUsuario.CL_USUARIO;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            ConfiguracionNotificacionNegocio negocio = new ConfiguracionNotificacionNegocio();
            Nombre.InnerText= " " + ContextoUsuario.oUsuario.NB_USUARIO;
            xmlPuestoEmpleado = negocio.ObtienePuestoEmpleado(null, vClUsuario);
            XElement root = null;
            XElement vXmlUsuario = null;
            if(xmlPuestoEmpleado!=null)
                root = XElement.Parse(xmlPuestoEmpleado);
            if(root != null)
                vXmlUsuario = root.Elements("EMPLEADO").Where(t => t.Attribute("CL_USUARIO").Value == (vClUsuario != null ? vClUsuario.ToString() : "")).FirstOrDefault();
            if (vXmlUsuario != null)
            {
                vEsDeRRHH = true;
            }
            else { vEsDeRRHH = false ;
            }
            if (!IsPostBack)
            {

            if (vEsDeRRHH == true)
            {
            ModificacionesNotificaciones.Visible = true;
            pendientes.Visible = true;
            }
            else { 
                
                ModificacionesNotificaciones.Visible = false;
                 pendientes.Visible = false ;
            }
            }


        }

        protected void rgdNoLeidos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
           
            List<SPE_OBTIENE_K_COMUNICADO_EMPLEADO_Result> ListaComunicadosNoLeidos = new List<SPE_OBTIENE_K_COMUNICADO_EMPLEADO_Result>();
            MenuNegocio negocio = new MenuNegocio();
            //usuarios
            ListaComunicadosNoLeidos = negocio.ObtenerComunicadoEmpleado(ContextoUsuario.oUsuario.CL_USUARIO.ToString(), false);
            rgdNoLeidos.DataSource = ListaComunicadosNoLeidos;
        }

        protected void rgdComunicadosLeidos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            List<SPE_OBTIENE_K_COMUNICADO_EMPLEADO_Result> ListaComunicadosLeidos = new List<SPE_OBTIENE_K_COMUNICADO_EMPLEADO_Result>();
            MenuNegocio negocio = new MenuNegocio();

            ListaComunicadosLeidos = negocio.ObtenerComunicadoEmpleado(ContextoUsuario.oUsuario.CL_USUARIO.ToString(), true);
            rgdComunicadosLeidos.DataSource = ListaComunicadosLeidos;

        }


        protected void grdNotificaciones_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_NOTIFICACIONES_A_RRHH_Result> ListaNotificacionesRRHH = new List<SPE_OBTIENE_NOTIFICACIONES_A_RRHH_Result>();
            MenuNegocio negocio = new MenuNegocio();

            ListaNotificacionesRRHH = negocio.ObtenerNotificacionesRRHH(ContextoUsuario.oUsuario.CL_USUARIO, null);
            grdNotificacionesRRHH.DataSource = ListaNotificacionesRRHH;


        }
      
        protected void grdModificacionesInformacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            List<SPE_OBTIENE_MODIFICACIONES_INFORMACION_EMPLEADO_Result> ListaModificacionesInformacion = new List<SPE_OBTIENE_MODIFICACIONES_INFORMACION_EMPLEADO_Result>();
            MenuNegocio negocio = new MenuNegocio();

            ListaModificacionesInformacion = negocio.ObtenerComunicadoModificacionesEmpleado(ContextoUsuario.oUsuario.CL_USUARIO);

            grdmodificacionesinformacion.DataSource = ListaModificacionesInformacion;


        }
       
        protected void grdNotificacionesdPendientes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_NOTIFICACIONES_PENDIENTES_Result> ListaNotificacionesPendientes = new List<SPE_OBTIENE_NOTIFICACIONES_PENDIENTES_Result>();
            MenuNegocio negocio = new MenuNegocio();

            ListaNotificacionesPendientes = negocio.ObtenerNotificacionesPendientes(ContextoUsuario.oUsuario.CL_USUARIO, null, "Pendiente");
            grdNotificacionesdPendientes.DataSource = ListaNotificacionesPendientes;
            vCantidadPendientes = ListaNotificacionesPendientes.Count();
            if (ListaNotificacionesPendientes.Count() > 0)
            {

                divNotificacionesPendientes.Visible = true;
                if (vCantidadPendientes <= 9)
                {
                    lblPendientesN.Text = "0" + vCantidadPendientes.ToString();
                }
                else
                {
                    lblPendientesN.Text = vCantidadPendientes.ToString();
                }

            }
            else
            {
                divNotificacionesPendientes.Visible = false;
            }

        }
        
        protected void rgModificacionesPendientes_NeedDataSource1(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            
            List<SPE_OBTIENE_MODIFICACIONES_PENDIENTES_Result> ListaModificacionesPendientes = new List<SPE_OBTIENE_MODIFICACIONES_PENDIENTES_Result>();
            MenuNegocio negocio = new MenuNegocio();

            ListaModificacionesPendientes = negocio.ObtenerModificacionePendientes(vIdEmpleado, null, "Pendiente", null);
            grdModificacionesPendientes.DataSource = ListaModificacionesPendientes;

            vCantidadPendientes = ListaModificacionesPendientes.Count();
            if (ListaModificacionesPendientes.Count() > 0)
            {
                divModificacionesPendientes.Visible = true;
                if (vCantidadPendientes <= 9)
                {
                    lblPendientesM.Text = "0" + vCantidadPendientes.ToString();
                }
                else
                {
                    lblPendientesM.Text = vCantidadPendientes.ToString();
                }

            }
            else
            {
                divModificacionesPendientes.Visible = false;
            }

        }

        protected void rgdNoLeidos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                string fechaComunicado = DateTime.Parse(item.GetDataKeyValue("FE_REGISTRO").ToString()).ToString("dd/MM/yyyy");
                string fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                Control target = e.Item.FindControl("divNuevo");
                Control target2 = e.Item.FindControl("divNoNuevo");

                if (fechaComunicado == fechaActual)
                {
                    target.Visible = true;
                    target2.Visible = false;
                }
                else
                {
                    target.Visible = false;
                    target2.Visible = true;

                }



            }

        }

        protected void grdNotificacionesRRHH_ItemDataBound(object sender, GridItemEventArgs e)
        {


            grdModificacionesPendientes.Rebind();
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                string estatusNotificacion = item.GetDataKeyValue("CL_ESTADO").ToString();
                Control target = e.Item.FindControl("divAtendida");
                if (estatusNotificacion == "Atendida")
                {
                    target.Visible = true;
                }
                else { target.Visible = false; }
            }
        }

        protected void raComunicados_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            rgdNoLeidos.Rebind();
            rgdComunicadosLeidos.Rebind();
            grdNotificacionesRRHH.Rebind();
            grdmodificacionesinformacion.Rebind();
            grdNotificacionesdPendientes.Rebind();
            grdModificacionesPendientes.Rebind();
        }

    }
}