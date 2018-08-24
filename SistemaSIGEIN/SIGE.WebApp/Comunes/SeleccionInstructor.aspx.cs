using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
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
    public partial class SeleccionInstructor : System.Web.UI.Page
    {

        private int? vIdEmpresa;

        private string vCompetencias
        {
            get { return (string)ViewState["vs_xml_competencias"]; }
            set { ViewState["vs_xml_competencias"] = value; }

        }
        //******************************************************************+
        private int vIdCurso
        {
            get { return (int)ViewState["vs_vIdCurso"]; }
            set { ViewState["vs_vIdCurso"] = value; }
        }

        private void GenerarXmlCompetencias(Guid vIdCurso)
        {
            XElement vXmlCompetencias = new XElement("COMPETENCIAS");

            if (ContextoCurso.oCursos != null)
            {
                List<E_CURSO_COMPETENCIA> oListaCompCurso = ContextoCurso.oCursos.Where(t => t.ID_ITEM == vIdCurso).FirstOrDefault().LS_COMPETENCIAS;

                if (oListaCompCurso.Count > 0)
                {
                    vXmlCompetencias.Add(oListaCompCurso.Select(t => new XElement("COMPETENCIA", new XAttribute("ID_COMPETENCIA", t.ID_COMPETENCIA))));
                    vCompetencias = vXmlCompetencias.ToString();
                }


            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!Page.IsPostBack)
            {
                vCompetencias = null;

                //Eventos
                if (Request.Params["IdCursoInstructor"] != null && Request.Params["IdCursoInstructor"] != "")
                {

                    vIdCurso = Convert.ToInt32(Request.Params["IdCursoInstructor"]);
                }
                else
                {
                    vIdCurso = 0;
                }


                //Cursos
                vCompetencias = null;

                if (Request.Params["IdCurso"] != null)
                {
                    GenerarXmlCompetencias(Guid.Parse(Request.Params["IdCurso"].ToString()));
                }
            }

        }

        protected void grdInstructores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            InstructorNegocio nInstructor = new InstructorNegocio();

            if (vIdCurso != 0)
            {
                grdInstructores.DataSource = nInstructor.ObtieneInstructores(pXmlCompetencias: vCompetencias, pIdCurso: vIdCurso, pIdEmpresa: vIdEmpresa);
            }
            else
                grdInstructores.DataSource = nInstructor.ObtieneInstructores(pXmlCompetencias: vCompetencias, pIdEmpresa: vIdEmpresa);


        }

        protected void grdInstructores_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdInstructores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdInstructores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdInstructores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdInstructores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdInstructores.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}