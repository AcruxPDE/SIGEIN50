using SIGE.Entidades;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;
using System.Xml.Linq;
using SIGE.Entidades.Externas;

namespace SIGE.WebApp.FYD
{
    public partial class Instructores : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        protected void SeguridadProcesos()
        {
            btnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.B.A");
            btnEditar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.B.B");
            btnEliminar.Enabled = ContextoUsuario.oUsuario.TienePermiso("K.B.C");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!Page.IsPostBack)
            {
                InstructorNegocio nCurso = new InstructorNegocio();
                List<SPE_OBTIENE_C_CURSO_Result> cursos = new List<SPE_OBTIENE_C_CURSO_Result>();
                cursos.Add(new SPE_OBTIENE_C_CURSO_Result()
                {
                    ID_CURSO = 0,
                    NB_CURSO = "Todos"
                });

                cursos.AddRange(nCurso.ObtieneCursos(null));
                cmbCurso.DataSource = cursos;
                cmbCurso.DataTextField = "NB_CURSO";
                cmbCurso.DataValueField = "ID_CURSO";
                cmbCurso.DataBind();

                InstructorNegocio nCompetencia = new InstructorNegocio();
                List<SPE_OBTIENE_C_COMPETENCIA_Result> competencias = new List<SPE_OBTIENE_C_COMPETENCIA_Result>();
                competencias.Add(new SPE_OBTIENE_C_COMPETENCIA_Result()
                {
                    ID_COMPETENCIA = 0,
                    NB_COMPETENCIA = "Todos"
                });

                competencias.AddRange(nCompetencia.ObtieneCompetencias(null));
                cmbCompetencia.DataSource = competencias;
                cmbCompetencia.DataTextField = "NB_COMPETENCIA";
                cmbCompetencia.DataValueField = "ID_COMPETENCIA";
                cmbCompetencia.DataBind();

                SeguridadProcesos();

            }
        }

        #region OBTIENE DATOS  C_INSTRUCTORES
        protected void grdInstructores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            InstructorNegocio nInstructor = new InstructorNegocio();
            grdInstructores.DataSource = nInstructor.ObtieneInstructores(null, null, null, null, vIdEmpresa);
        }
        #endregion

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            var idCompetencia = Convert.ToInt32(cmbCompetencia.SelectedValue);
            var idCurso = Convert.ToInt32(cmbCurso.SelectedValue);

            InstructorNegocio nInstructor = new InstructorNegocio();
            if (idCompetencia == 0)
            {
                if (idCurso == 0)
                {
                    grdInstructores.DataSource = nInstructor.ObtieneInstructores(null, null, null);
                }
                else
                {
                    grdInstructores.DataSource = nInstructor.ObtieneInstructores(null, null, idCurso);
                }

            }
            else
            {
                if (idCurso == 0)
                {
                    grdInstructores.DataSource = nInstructor.ObtieneInstructores(null, idCompetencia, null);
                }
                else
                {
                    grdInstructores.DataSource = nInstructor.ObtieneInstructores(null, idCompetencia, idCurso);
                }
            }

            grdInstructores.Rebind();
        }

        protected void grdInstructores_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ExpandCollapse")
            {
                GridDataItem i = (GridDataItem)e.Item;
                int pInstructorId = int.Parse(i.GetDataKeyValue("ID_INSTRUCTOR").ToString());

                if (!i.Expanded)
                {
                    GridNestedViewItem nestedItem = (GridNestedViewItem)i.ChildItem;
                    RadGrid grdCursosInstructor = (RadGrid)nestedItem.FindControl("grdCursos");
                    RadGrid grdCompetenciasInstructor = (RadGrid)nestedItem.FindControl("grdCompetencia");
                    RadGrid grdTelefono = (RadGrid)nestedItem.FindControl("grdTelefono");
                    //RadTextBox txtEmail = (RadTextBox)nestedItem.FindControl("txtEmail");
                    System.Web.UI.HtmlControls.HtmlGenericControl txtTblEmail = (System.Web.UI.HtmlControls.HtmlGenericControl)nestedItem.FindControl("txtTblEmail");

                    InstructorNegocio nInstructorCurso = new InstructorNegocio();
                    E_INSTRUCTORES lista = nInstructorCurso.ObtieneInstructor(pInstructorId).FirstOrDefault();

                    List<E_INSTRUCTOR_CURSO> vLstCurso = new List<E_INSTRUCTOR_CURSO>();
                    if (lista.XML_CURSOS != null & lista.XML_CURSOS != "")
                    {
                        vLstCurso = XElement.Parse(lista.XML_CURSOS).Elements("CURSO").Select(el => new E_INSTRUCTOR_CURSO
                        {
                            ID_INSTRUCTOR_CURSO = UtilXML.ValorAtributo<int>(el.Attribute("ID_INSTRUCTOR_CURSO")),
                            ID_CURSO = UtilXML.ValorAtributo<int>(el.Attribute("ID_CURSO")),
                            CL_CURSO = UtilXML.ValorAtributo<string>(el.Attribute("CL_CURSO")),
                            NB_CURSO = UtilXML.ValorAtributo<string>(el.Attribute("NB_CURSO")),
                        }).ToList();
                    }

                    grdCursosInstructor.DataSource = vLstCurso;
                    grdCursosInstructor.Rebind();

                    List<E_INSTRUCTOR_COMPETENCIA> vLstCompetencia = new List<E_INSTRUCTOR_COMPETENCIA>();
                    if (lista.XML_COMPETENCIAS != null & lista.XML_COMPETENCIAS != "")
                    {
                        vLstCompetencia = XElement.Parse(lista.XML_COMPETENCIAS).Elements("COMPETENCIA").Select(el => new E_INSTRUCTOR_COMPETENCIA
                        {
                            ID_INSTRUCTOR_COMPETENCIA = UtilXML.ValorAtributo<int>(el.Attribute("ID_INSTRUCTOR_COMPETENCIA")),
                            ID_COMPETENCIA = UtilXML.ValorAtributo<int>(el.Attribute("ID_COMPETENCIA")),
                            CL_COMPETENCIA = UtilXML.ValorAtributo<string>(el.Attribute("CL_COMPETENCIA")),
                            NB_COMPETENCIA = UtilXML.ValorAtributo<string>(el.Attribute("NB_COMPETENCIA")),
                        }).ToList();
                    }

                    grdCompetenciasInstructor.DataSource = vLstCompetencia;
                    grdCompetenciasInstructor.Rebind();

                    txtTblEmail.InnerText = lista.CL_CORREO_ELECTRONICO;

                    List<E_TIPO_TELEFONO> vLstTipoTelefono = new List<E_TIPO_TELEFONO>();
                    vLstTipoTelefono = XElement.Parse(lista.XML_NO_TELEFONO_TIPOS).Elements("ITEM").Select(el => new E_TIPO_TELEFONO
                    {
                        NB_TEXTO = UtilXML.ValorAtributo<String>(el.Attribute("NB_TEXTO")),
                        NB_VALOR = UtilXML.ValorAtributo<String>(el.Attribute("NB_VALOR"))
                    }).ToList();

                    List<E_TELEFONO> vLstTelefono = new List<E_TELEFONO>();
                    if (lista.XML_TELEFONOS != null & lista.XML_TELEFONOS != "")
                        vLstTelefono = XElement.Parse(lista.XML_TELEFONOS).Elements("TELEFONO").Select(el => new E_TELEFONO
                        {
                            NB_TELEFONO = UtilXML.ValorAtributo<string>(el.Attribute("NO_TELEFONO")),
                            CL_TIPO = UtilXML.ValorAtributo<string>(el.Attribute("CL_TIPO")),
                            NB_TIPO = (vLstTipoTelefono.FirstOrDefault(f => f.NB_VALOR.Equals(UtilXML.ValorAtributo<string>(el.Attribute("CL_TIPO")))) ?? new E_TIPO_TELEFONO()).NB_TEXTO
                        }).ToList();

                    grdTelefono.DataSource = vLstTelefono;
                    grdTelefono.Rebind();

                }

                foreach (GridItem item in grdInstructores.MasterTableView.Items)
                {
                    item.Expanded = false;
                }
                e.Item.Expanded = i.Expanded;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            InstructorNegocio nInstructor = new InstructorNegocio();

            foreach (GridDataItem item in grdInstructores.SelectedItems)
            {
                E_RESULTADO vResultado = nInstructor.EliminaInstructor(int.Parse(item.GetDataKeyValue("ID_INSTRUCTOR").ToString()), item.GetDataKeyValue("CL_INTRUCTOR").ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(RadWindowManager1, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void grdInstructores_ItemDataBound(object sender, GridItemEventArgs e)
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