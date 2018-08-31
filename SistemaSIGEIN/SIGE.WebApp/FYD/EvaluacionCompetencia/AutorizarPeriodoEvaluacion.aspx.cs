using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD.EvaluacionCompetencia
{
    public partial class AutorizarPeriodoEvaluacion : System.Web.UI.Page
    {
        #region MyRegion
        
        #endregion

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_ape_id_periodo"]; }
            set { ViewState["vs_ape_id_periodo"] = value; }
        }

        private Guid? vFlAutorizacion
        {
            get { return (Guid?)ViewState["vs_ape_FlAutorizacion"]; }
            set { ViewState["vs_ape_FlAutorizacion"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null) ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO";
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (Request.Params["IdPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"]);

                    vFlAutorizacion = Guid.Parse((Request.QueryString["TOKEN"]));
                    PeriodoNegocio nPeriodo = new PeriodoNegocio();

                    var oPeriodo = nPeriodo.ObtienePeriodoEvaluacion(vIdPeriodo);

                    if (oPeriodo != null)
                    {
                        txtClavePeriodo.Text = oPeriodo.CL_PERIODO;
                        txtNombrePeriodo.Text = oPeriodo.DS_PERIODO;
                        btnAutorizado.Checked = true;
                    }
                }
            }
        }

        protected void grdCuestionarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            List<SPE_OBTIENE_FYD_EVALUADOS_AUTORIZACION_Result> vLstEvaluadosCuestionarios = nPeriodo.ObtenerEvaluadosEvaluadores(vIdPeriodo);
            grdCuestionarios.DataSource = vLstEvaluadosCuestionarios;
        }

        protected void grdCuestionarios_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "gtvEvaluadores":
                    int vIdEvaluado = 0;
                    if (int.TryParse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString(), out vIdEvaluado))
                    {
                        PeriodoNegocio nPeriodo = new PeriodoNegocio();
                        e.DetailTableView.DataSource = nPeriodo.ObtenerEvaluadoresAutorizacion(vIdEvaluado);
                    }
                    break;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            string autoriza = (btnAutorizado.Checked) ? "Autorizado" : "No autorizado";
            XElement vXelementNotas = null;

            var vXelementNota =
            new XElement("NOTA",
            new XAttribute("FE_NOTA", DateTime.Now.ToString()),
            new XAttribute("DS_NOTA", radObservaciones.Content.ToString())
                 );
            vXelementNotas = new XElement("NOTAS", vXelementNota);

            E_RESULTADO vResultado = nPeriodo.ActualizaEstatusDocumentoAutorizacion(vFlAutorizacion, autoriza, vXelementNotas.ToString(), null, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, "");
            string myUrl = ResolveUrl("~/Logon.aspx");
            Response.Redirect(ContextoUsuario.nbHost + myUrl);
        }

        protected void grdCuestionarios_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCuestionarios.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}