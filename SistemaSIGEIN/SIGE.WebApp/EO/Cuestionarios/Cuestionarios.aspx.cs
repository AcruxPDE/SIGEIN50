using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Web.UI;
using SIGE.Entidades;
using SIGE.Negocio.EvaluacionOrganizacional;
using System.Xml.Linq;
using System.Linq;
using Telerik.Web.UI;
using SIGE.WebApp.Comunes;
using WebApp.Comunes;
using System.Collections;

namespace SIGE.WebApp.EO.Cuestionarios
{
    public partial class Cuestionarios : System.Web.UI.Page
    {
        public string cssModulo = String.Empty;
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int pIdEvaluador
        {
            get { return (int)ViewState["vs_pIdEvaluado"]; }
            set { ViewState["vs_pIdEvaluado"] = value; }
        }

        public string vEstadoPeriodo
        {
            get { return (string)ViewState["vs_vEstadoPeriodo"]; }
            set { ViewState["vs_vEstadoPeriodo"] = value; }
        }

        ClimaLaboralNegocio negocio = new ClimaLaboralNegocio();
        PeriodoDesempenoNegocio negocioEval = new PeriodoDesempenoNegocio();
        string vNbFirstRadEditorTagName = "p";

        protected void Page_Load(object sender, EventArgs e)
        {

            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";

            string vClModulo = "FORMACION";
            string vModulo = Request.QueryString["m"];
            if (vModulo != null)
                vClModulo = vModulo;

            cssModulo = Utileria.ObtenerCssModulo(vClModulo);

            if (!Page.IsPostBack)
            {

                if (vClUsuario == "INVITADO")
                {
                    btnSalir.Visible = true;
                }

                if (Request.Params["ID_EVALUADOR"] != null)
                {
                    SPE_OBTIENE_EO_PERIODO_EVALUADOR_Result periodo = new SPE_OBTIENE_EO_PERIODO_EVALUADOR_Result();

                    pIdEvaluador = int.Parse((Request.QueryString["ID_EVALUADOR"]));
                    periodo = negocio.ObtenerPeriodoEvaluado(pIdEvaluador);
                    vEstadoPeriodo = Request.QueryString["ESTADO_PERIODO"];                    
                    if (periodo != null)
                    {
                        txtNoPeriodo.InnerText = periodo.ID_PERIODO.ToString();
                        txtNbPeriodo.InnerText = periodo.NB_PERIODO;

                        //if (!String.IsNullOrEmpty(periodo.XML_MENSAJE_INICIAL))
                        //{
                        //    XElement vMensajeInicial = XElement.Parse(periodo.XML_MENSAJE_INICIAL);
                        //    if (vMensajeInicial != null)
                        //    {
                        //        vMensajeInicial.Name = vNbFirstRadEditorTagName;
                        //        mensajeInicial.InnerHtml = vMensajeInicial.ToString().Replace("{PERSONA_QUE_EVALUA}", periodo.NB_EVALUADOR);
                        //    }
                        //}

                    }
                }

            }
        }

        protected void grdEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_EO_EVALUADOS_Result> evaluados = negocioEval.ObtenerEvaluadosEvaluacionOrganizacional(pIdEvaluador);
            total.Value = evaluados.Count.ToString();
            evalu.Value = evaluados.Where(w => w.FG_EVALUADO.Equals("Sí")).Count().ToString();
            grdEvaluados.DataSource = evaluados;
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            grdEvaluados.Rebind();
        }

        protected void btnEvaluar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdEvaluados.SelectedItems)
            {
                string vClEvaluado = item.GetDataKeyValue("FG_EVALUADO").ToString();

                if (vClEvaluado.Equals("Sí") & vClUsuario == "INVITADO")
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, "El cuestionario ya fue contestado.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
                    return;
                }

                int vID_EVALUADO_EVALUADOR = (int.Parse(item.GetDataKeyValue("ID_EVALUADO_EVALUADOR").ToString()));

                Response.Redirect("Evaluar.aspx?ID_EVALUADOR=" + pIdEvaluador + "&ID_EVALUADO_EVALUADOR=" + vID_EVALUADO_EVALUADOR);
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Logout.aspx");
        }


    }
}