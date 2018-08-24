using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class SustitucionBaremos : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario = string.Empty;
        private string vNbPrograma = string.Empty;

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdBateria
        {
            get { return (int)ViewState["vs_vIdBateria"]; }
            set { ViewState["vs_vIdBateria"] = value; }
        }

        #endregion

        #region Metodos

        private void cargarDatos()
        {
            PruebasNegocio neg = new PruebasNegocio();
            List<SPE_OBTIENE_VARIABLES_BAREMOS_SUSTITUCION_Result> oDatos = new List<SPE_OBTIENE_VARIABLES_BAREMOS_SUSTITUCION_Result>();

            oDatos = neg.obtenerBaremosSustitucion(vIdBateria);
            dgvBaremos.DataSource = oDatos;            
        }

        private string generarXml()
        {
            XElement vVariablesBaremos = new XElement("BAREMOS");

            foreach (GridDataItem item in dgvBaremos.Items)
            {
                int NO_VALOR;
                int ID_VARIABLE;

                NO_VALOR = (int.Parse((item.FindControl("rsNivel1") as RadSlider).Value.ToString())) - 1;
                ID_VARIABLE = int.Parse(item.GetDataKeyValue("ID_VARIABLE").ToString());

                if (NO_VALOR != -1)
                {
                    vVariablesBaremos.Add(new XElement("VARIABLE",
                        new XAttribute("ID_VARIABLE", ID_VARIABLE),
                        new XAttribute("NO_VALOR", NO_VALOR)));
                }
            }

            return vVariablesBaremos.ToString();
        }

        private void guardarBaremos()
        {
            string XML_BAREMOS = generarXml();
            PruebasNegocio neg = new PruebasNegocio();

            E_RESULTADO vResultado = neg.insertaVariablesBaremos(vIdBateria, XML_BAREMOS, vClUsuario, vNbPrograma);

            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                //UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
        }

        private void reiniciarBaremos()
        {            
            PruebasNegocio neg = new PruebasNegocio();

            E_RESULTADO vResultado = neg.generaVariablesBaremos(vIdBateria, vClUsuario, vNbPrograma);

            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                dgvBaremos.Rebind();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["pIdBateria"] != null)
                {
                    vIdBateria = int.Parse(Request.QueryString["pIdBateria"]);
                }
            }
        }

        protected void dgvBaremos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            cargarDatos();            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            guardarBaremos();
        }

        protected void btnReinicio_Click(object sender, EventArgs e)
        {
            reiniciarBaremos();
        }
    }
}