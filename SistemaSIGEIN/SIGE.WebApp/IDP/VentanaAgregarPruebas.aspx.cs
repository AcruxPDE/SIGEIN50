using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaAgregarPruebas1 : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdBateria
        {
            get { return (int)ViewState["vs_vIdBateria"];}
            set { ViewState["vs_vIdBateria"]=value;}
        }

        #endregion

        #region Metodos

        protected void AgregarPruebas(string pPruebas)
        {
            List<E_PRUEBAS> vPruebas = JsonConvert.DeserializeObject<List<E_PRUEBAS>>(pPruebas);

            if (vPruebas.Count > 0)
                InsertarPruebas(new XElement("PRUEBAS", vPruebas.Select(s => new XElement("PRUEBA", new XAttribute("ID_PRUEBA", s.idPrueba )))));
        }

        protected void InsertarPruebas(XElement pXmlElementos)
        {
            PruebasNegocio nPruebas = new PruebasNegocio();
            E_RESULTADO vResultado = nPruebas.InsertaActualizaPruebasBateria(vIdBateria, pXmlElementos.ToString(), vClUsuario, vNbPrograma, "");
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                grdPruebas.Rebind();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["pIdBateria"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["pIdBateria"].ToString());
                }
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void grdPruebas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PruebasNegocio pruebas = new PruebasNegocio();
            grdPruebas.DataSource = pruebas.Obtener_K_PRUEBA(pIdBateria: vIdBateria, pFgAsignada: true);
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "PRUEBAS")
                AgregarPruebas(vSeleccion.oSeleccion.ToString());
        }



    }
}