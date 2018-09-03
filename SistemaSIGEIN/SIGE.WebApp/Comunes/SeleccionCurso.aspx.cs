using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.FYD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Comunes
{
    public partial class SeleccionCurso : System.Web.UI.Page
    {

        #region Variables

        private Guid vIdInstructor
        {
            get { return (Guid)ViewState["vs_sc_id_instructor"]; }
            set { ViewState["vs_sc_id_instructor"] = value; }
        }
        private int vIdPrograma;
        //{
        //    get { return (int)ViewState["vs_sc_vIdPrograma"]; }
        //    set { ViewState["vs_sc_vIdPrograma"] = value; }
        //}
        private string vFiltroCompetencias
        {
            get { return (string)ViewState["vs_sc_filtro_competencia"]; }
            set { ViewState["vs_sc_filtro_competencia"] = value; }
        }
        private string vVinculo
        {
            get { return (string)ViewState["vs_sc_vVinculo"]; }
            set { ViewState["vs_sc_vVinculo"] = value; }
        }

        #endregion

        #region Funciones

        private void GenerarXml()
        {
            XElement vXmlCompetencias = new XElement("COMPETENCIAS");

            List<E_INSTRUCTOR_COMPETENCIA> vLstCompetencias = ContextoInstructor.oInstructores.Where(t => t.ID_ITEM == vIdInstructor).FirstOrDefault().LstCompetencias;

            if (vLstCompetencias.Count > 0)
            {
                vXmlCompetencias.Add(vLstCompetencias.Select(t => new XElement("COMPETENCIA", new XAttribute("ID_COMPETENCIA", t.ID_COMPETENCIA.ToString()))));
                vFiltroCompetencias = vXmlCompetencias.ToString();
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                grdCursos.AllowMultiRowSelection = true;
                if (!String.IsNullOrEmpty(Request.QueryString["mulSel"]))
                {
                    grdCursos.AllowMultiRowSelection = (Request.QueryString["mulSel"] == "1");
                    btnAgregar.Visible = (Request.QueryString["mulSel"] == "1");
                }



                vFiltroCompetencias = null;

                if (Request.Params["IdInstructor"] != null)
                {
                    vIdInstructor = Guid.Parse(Request.Params["IdInstructor"].ToString());
                    GenerarXml();
                }
                if (Request.Params["Idprograma"] != null)
                {
                    if (Request.Params["Idprograma"] == "")
                    {

                        vIdPrograma = 0;
                    }
                    else
                    {
                        vIdPrograma = Convert.ToInt32(Request.Params["Idprograma"]);
                    }
                }
                if (Request.Params["pVinculado"] != null)
                {
                    vVinculo = Request.Params["pVinculado"];
                }


            }
        }

        protected void grdCursos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (vVinculo == "No")
            {
                vIdPrograma = 0;
            }
            CursoNegocio nCurso = new CursoNegocio();
            if (vIdPrograma > 0 && vVinculo == "Si")
            {
                grdCursos.DataSource = nCurso.ObtieneCursos(pXmlCompetencias: vFiltroCompetencias, pId_Programa: vIdPrograma);
            }

            else
            {
                grdCursos.DataSource = nCurso.ObtieneCursos(pXmlCompetencias: vFiltroCompetencias);

            }
        }

        protected void grdCursos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCursos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCursos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCursos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCursos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCursos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}