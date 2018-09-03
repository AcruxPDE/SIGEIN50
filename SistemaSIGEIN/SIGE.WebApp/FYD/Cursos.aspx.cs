using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;
using SIGE.Entidades.FormacionDesarrollo;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.FYD
{
    public partial class Cursos : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        protected void SeguridadProcesos()
        {
            btnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.C.A");
            btnEditar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.C.B");
            btnEliminar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.C.C");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
                SeguridadProcesos();
        }

        protected void grdCursos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CursoNegocio nCurso = new CursoNegocio();
            grdCursos.DataSource = nCurso.ObtieneCursos(null);
        }

        protected void grdCursos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ExpandCollapse")
            {
                GridDataItem i = (GridDataItem)e.Item;
                int pCursoId = int.Parse(i.GetDataKeyValue("ID_CURSO").ToString());

                if (!i.Expanded)
                {
                    GridNestedViewItem nestedItem = (GridNestedViewItem)i.ChildItem;
                    RadGrid grdCursosInstructor = (RadGrid)nestedItem.FindControl("grdCursosInstructor");
                    RadGrid grdCursosCompetencia = (RadGrid)nestedItem.FindControl("grdCursosCompetencia");
                    RadGrid grdCursosTema = (RadGrid)nestedItem.FindControl("grdCursosTema");


                    CursoNegocio nCursoInstructor = new CursoNegocio();
                    SPE_OBTIENE_C_CURSO_Result lista = nCursoInstructor.ObtieneCursos(pCursoId).FirstOrDefault();

                    List<E_CURSO_INSTRUCTOR> vLstCurso = new List<E_CURSO_INSTRUCTOR>();
                    if (lista.XML_INSTRUCTOR != null & lista.XML_INSTRUCTOR != "")
                    {
                        vLstCurso = XElement.Parse(lista.XML_INSTRUCTOR).Elements("INSTRUCTOR").Select(el => new E_CURSO_INSTRUCTOR
                        {
                            ID_INSTRUCTOR_CURSO = UtilXML.ValorAtributo<int>(el.Attribute("ID_INSTRUCTOR_CURSO")),
                            ID_INSTRUCTOR = UtilXML.ValorAtributo<int>(el.Attribute("ID_INSTRUCTOR")),
                            CL_INSTRUCTOR = UtilXML.ValorAtributo<string>(el.Attribute("CL_INSTRUCTOR")),
                            NB_INSTRUCTOR = UtilXML.ValorAtributo<string>(el.Attribute("NB_INSTRUCTOR")),
                        }).ToList();
                    }

                    grdCursosInstructor.DataSource = vLstCurso;
                    grdCursosInstructor.Rebind();

                    List<E_CURSO_COMPETENCIA> vLstCompetencia = new List<E_CURSO_COMPETENCIA>();
                    if (lista.XML_COMPETENCIAS != null & lista.XML_COMPETENCIAS != "")
                    {
                        vLstCompetencia = XElement.Parse(lista.XML_COMPETENCIAS).Elements("COMPETENCIA").Select(el => new E_CURSO_COMPETENCIA
                        {
                            ID_CURSO_COMPETENCIA = UtilXML.ValorAtributo<int>(el.Attribute("ID_CURSO_COMPETENCIA")),
                            ID_COMPETENCIA = UtilXML.ValorAtributo<int>(el.Attribute("ID_COMPETENCIA")),
                            CL_TIPO_COMPETENCIA = UtilXML.ValorAtributo<string>(el.Attribute("CL_TIPO_COMPETENCIA")),
                            NB_COMPETENCIA = UtilXML.ValorAtributo<string>(el.Attribute("NB_COMPETENCIA")),
                        }).ToList();
                    }

                    grdCursosCompetencia.DataSource = vLstCompetencia;
                    grdCursosCompetencia.Rebind();

                    List<E_TEMA> vLstTema = new List<E_TEMA>();
                    if (lista.XML_TEMAS != null & lista.XML_TEMAS != "")
                        vLstTema = XElement.Parse(lista.XML_TEMAS).Elements("TEMA").Select(el => new E_TEMA
                        {
                            ID_TEMA = UtilXML.ValorAtributo<int>(el.Attribute("ID_TEMA")),
                            CL_TEMA = UtilXML.ValorAtributo<string>(el.Attribute("CL_TEMA")),
                            NB_TEMA = UtilXML.ValorAtributo<string>(el.Attribute("NB_TEMA")),
                            NO_DURACION = UtilXML.ValorAtributo<string>(el.Attribute("NO_DURACION"))

                        }).ToList();

                    grdCursosTema.DataSource = vLstTema;
                    grdCursosTema.Rebind();
                }

                foreach (GridItem item in grdCursos.MasterTableView.Items)
                {
                    item.Expanded = false;
                }
                e.Item.Expanded = i.Expanded;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            CursoNegocio nCurso = new CursoNegocio();

            foreach (GridDataItem item in grdCursos.SelectedItems)
            {
                E_RESULTADO vResultado = nCurso.EliminaCurso(int.Parse(item.GetDataKeyValue("ID_CURSO").ToString()), item.GetDataKeyValue("CL_CURSO").ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(RadWindowManager1, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void grdCursos_ItemDataBound(object sender, GridItemEventArgs e)
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