using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.Administracion;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using Telerik.Web.UI;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Net;
using System.Drawing;
using System.Globalization;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCatalogoClasificacionComp : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        public int vIdClasificacion
        {
            set { ViewState["vs_vIdClasificacion"] = value; }
            get { return (int)ViewState["vs_vIdClasificacion"]; }
        }

        //public string usuario = "";
        //public string programa = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;


            if (!Page.IsPostBack)
            {
                ObtenerTiposCompetencia();

                if (Request.Params["ID"] != null)
                {
                    vIdClasificacion = int.Parse(Request.Params["ID"]);

                    if (vIdClasificacion != 0) {

                        SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result clasificacion = new SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result();

                        clasificacion = getClasificacion(vIdClasificacion);

                        txtClClasificacion.Text = clasificacion.CL_CLASIFICACION;
                        cmbIdTipoCompetencia.SelectedValue = clasificacion.CL_TIPO_COMPETENCIA;
                        txtNbCategoria.Text = clasificacion.NB_CLASIFICACION_COMPETENCIA;
                        txtDescripcion.Text = clasificacion.DS_CLASIFICACION_COMPETENCIA;
                        txtNotasCategoria.Text = clasificacion.DS_NOTAS_CLASIFICACION;
                        chkActivo.Checked = clasificacion.FG_ACTIVO;
                        rdcClasificacionCompetencia.SelectedColor = System.Drawing.ColorTranslator.FromHtml(clasificacion.CL_COLOR.ToString()) ;
                    }

                }
            }
        }

        protected void ObtenerTiposCompetencia() {

            TipoCompetenciaNegocio negocio = new TipoCompetenciaNegocio();

            cmbIdTipoCompetencia.DataSource = negocio.ObtieneTipoCompetencia();
            cmbIdTipoCompetencia.DataTextField = "NB_TIPO_COMPETENCIA";
            cmbIdTipoCompetencia.DataValueField = "CL_TIPO_COMPETENCIA";
            cmbIdTipoCompetencia.DataBind();
        }

        protected SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result getClasificacion(int idClasificacion) {

            ClasificacionCompetenciaNegocio negocio = new ClasificacionCompetenciaNegocio();

            List<SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result> lista = negocio.ObtieneClasificacionCompetencia();

            var q = from a in lista
                    where a.ID_CLASIFICACION_COMPETENCIA == idClasificacion
                    select new SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result
                    {
                        ID_CLASIFICACION_COMPETENCIA = a.ID_CLASIFICACION_COMPETENCIA,
                        CL_CLASIFICACION = a.CL_CLASIFICACION,
                        CL_TIPO_COMPETENCIA = a.CL_TIPO_COMPETENCIA,
                        NB_CLASIFICACION_COMPETENCIA = a.NB_CLASIFICACION_COMPETENCIA,
                        DS_CLASIFICACION_COMPETENCIA = a.DS_CLASIFICACION_COMPETENCIA,
                        DS_NOTAS_CLASIFICACION= a.DS_NOTAS_CLASIFICACION,
                        DS_TIPO_COMPETENCIA = a.DS_TIPO_COMPETENCIA,
                        FG_ACTIVO = a.FG_ACTIVO,
                        NB_TIPO_COMPETENCIA = a.NB_TIPO_COMPETENCIA,
                        CL_COLOR= a.CL_COLOR
                    };

            return q.FirstOrDefault();
        }

        protected void btnGuardarRegistro_Click(object sender, EventArgs e)
        {
            SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result catalogo = new SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result();


            string tipoTransaccion = E_TIPO_OPERACION_DB.I.ToString(); 

            if (vIdClasificacion != 0)
            {
             tipoTransaccion=   E_TIPO_OPERACION_DB.A.ToString();
                catalogo = getClasificacion(int.Parse(vIdClasificacion.ToString()));
            }

            catalogo.CL_CLASIFICACION = txtClClasificacion.Text;
            catalogo.NB_CLASIFICACION_COMPETENCIA = txtNbCategoria.Text;
            catalogo.DS_CLASIFICACION_COMPETENCIA = txtDescripcion.Text;
            catalogo.DS_NOTAS_CLASIFICACION = txtNotasCategoria.Text;
            catalogo.CL_TIPO_COMPETENCIA = cmbIdTipoCompetencia.SelectedValue;
            catalogo.FG_ACTIVO = chkActivo.Checked;
            catalogo.CL_COLOR = HexConverter(rdcClasificacionCompetencia.SelectedColor);

            ClasificacionCompetenciaNegocio nClasificacionComp = new ClasificacionCompetenciaNegocio();

              E_RESULTADO vResultado= nClasificacionComp.InsertaActualizaClasificacionCompetencia(tipoTransaccion, catalogo,vClUsuario, vNbPrograma);

            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
         }


        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private static String RGBConverter(System.Drawing.Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }
    }
}