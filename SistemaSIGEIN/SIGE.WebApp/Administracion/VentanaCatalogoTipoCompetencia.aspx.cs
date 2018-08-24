using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using SIGE.Negocio.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using Telerik.Web.UI;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Net;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCatalogoTipoCompetencia : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        public string vClTipoCompetencia
        {
            set { ViewState["vs_vClTipoCompetencia"] = value; }
            get { return (string)ViewState["vs_vClTipoCompetencia"]; }
        }

        //public string usuario = "";
        //public string programa = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;


            if (!Page.IsPostBack)
            {

                if (Request.Params["ID"] != null)
                {
                    vClTipoCompetencia = Request.Params["ID"];

                    if (vClTipoCompetencia != "" && vClTipoCompetencia != "0")
                    {
                        SPE_OBTIENE_S_TIPO_COMPETENCIA_Result catalogo = new SPE_OBTIENE_S_TIPO_COMPETENCIA_Result();

                        catalogo = getTipoCompetencia(Request.Params["ID"]);

                        txtClTipo.Text = catalogo.CL_TIPO_COMPETENCIA;
                        txtNbTipo.Text = catalogo.NB_TIPO_COMPETENCIA;
                        //txtDsTipo.Text = catalogo.DS_TIPO_COMPETENCIA;
                        chkActivo.Checked = catalogo.FG_ACTIVO;

                        txtClTipo.Enabled = false;

                    }
                }
            }
        }

        protected void btnGuardarRegistro_Click(object sender, EventArgs e)
        {
            SPE_OBTIENE_S_TIPO_COMPETENCIA_Result tipo = new SPE_OBTIENE_S_TIPO_COMPETENCIA_Result();

            string tipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();

            if (vClTipoCompetencia != null && vClTipoCompetencia != "0")
            {
                tipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
                tipo = getTipoCompetencia(vClTipoCompetencia.ToString());
            }

            tipo.CL_TIPO_COMPETENCIA = txtClTipo.Text;
            tipo.NB_TIPO_COMPETENCIA = txtNbTipo.Text;
            //tipo.DS_TIPO_COMPETENCIA = txtDsTipo.Text;
            tipo.FG_ACTIVO = chkActivo.Checked;

            TipoCompetenciaNegocio negocio = new TipoCompetenciaNegocio();

            E_RESULTADO vResultado = negocio.InsertaActualizaTipoCompetencia(pTipoTransaccion: tipoTransaccion, pClUsuario: vClUsuario, pNbPrograma: vNbPrograma, pTipoCompetencia: tipo);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
        
        }

        protected SPE_OBTIENE_S_TIPO_COMPETENCIA_Result getTipoCompetencia(string clTipoCompetencia)
        {

            TipoCompetenciaNegocio negocio = new TipoCompetenciaNegocio();

            List<SPE_OBTIENE_S_TIPO_COMPETENCIA_Result> lista = negocio.ObtieneTipoCompetencia();

            var q = from o in lista
                    where o.CL_TIPO_COMPETENCIA == clTipoCompetencia
                    select new SPE_OBTIENE_S_TIPO_COMPETENCIA_Result
                    {
                        CL_TIPO_COMPETENCIA = o.CL_TIPO_COMPETENCIA,
                        NB_TIPO_COMPETENCIA = o.NB_TIPO_COMPETENCIA,
                        DS_TIPO_COMPETENCIA = o.DS_TIPO_COMPETENCIA,
                        FG_ACTIVO = o.FG_ACTIVO,
                        NB_ACTIVO = o.NB_ACTIVO
                    };

            return q.FirstOrDefault();
        }
    }
}