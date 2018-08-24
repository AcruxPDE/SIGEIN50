using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Web.UI;
using SIGE.Entidades;
using SIGE.Negocio.FormacionDesarrollo;
using System.Xml.Linq;
using System.Linq;
using Telerik.Web.UI;
using SIGE.WebApp.Comunes;
using WebApp.Comunes;
using System.Collections;

namespace SIGE.WebApp.FYD.EvaluacionCompetencia
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

        PeriodoNegocio negocio = new PeriodoNegocio();
        CuestionarioNegocio negocioEval = new CuestionarioNegocio();
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
                    SPE_OBTIENE_FYD_PERIODO_EVALUADOR_Result periodo = new SPE_OBTIENE_FYD_PERIODO_EVALUADOR_Result();

                    pIdEvaluador = int.Parse((Request.QueryString["ID_EVALUADOR"]));
                    periodo = negocio.ObtienePeriodoEvaluador(pIdEvaluador);
                    vEstadoPeriodo = Request.QueryString["ESTADO_PERIODO"];
                    if (periodo != null)
                    {
                        txtNoPeriodo.InnerText = periodo.CL_PERIODO;
                        txtNbPeriodo.InnerText = periodo.DS_PERIODO;

                        if (!String.IsNullOrEmpty(periodo.XML_MENSAJE_INICIAL))
                        {
                            XElement vMensajeInicial = XElement.Parse(periodo.XML_MENSAJE_INICIAL);
                            if (vMensajeInicial != null)
                            {
                                vMensajeInicial.Name = vNbFirstRadEditorTagName;
                                mensajeInicial.InnerHtml = vMensajeInicial.ToString().Replace("{PERSONA_QUE_EVALUA}", periodo.NB_EVALUADOR);
                            }
                        }

                        //if (!String.IsNullOrEmpty(periodo.XML_INSTRUCCIONES_LLENADO))
                        //{
                        //    XElement vInstrucciones = XElement.Parse(periodo.XML_INSTRUCCIONES_LLENADO);
                        //    if (vInstrucciones != null)
                        //    {
                        //        vInstrucciones.Name = vNbFirstRadEditorTagName;
                        //        instrucciones.InnerHtml = vInstrucciones.ToString();
                        //    }
                        //}
                    }
                }

            }
        }

        protected void grdEvaluados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<SPE_OBTIENE_FYD_EVALUADOS_Result> evaluados = negocioEval.ObtieneEvaluados(pIdEvaluador);
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

        protected void grdEvaluados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdEvaluados.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}