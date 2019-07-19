using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaRoles : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int? vIdRol
        {
            get { return (int?)ViewState["vs_vIdRol"]; }
            set { ViewState["vs_vIdRol"] = value; }
        }

        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }

        private List<E_FUNCION> vLstFunciones
        {
            get { return (List<E_FUNCION>)ViewState["vs_vLstFunciones"]; }
            set { ViewState["vs_vLstFunciones"] = value; }
        }

        private List<E_PLAZA_GRUPO> vLstPlazasGrupo
        {
            get { return (List<E_PLAZA_GRUPO>)ViewState["vs_vLstPlazasGrupo"]; }
            set { ViewState["vs_vLstPlazasGrupo"] = value; }
        }

        private List<E_GRUPOS> vLstGruposPlaza
        {
            get { return (List<E_GRUPOS>)ViewState["vs_vLstGruposPlaza"]; }
            set { ViewState["vs_vLstGruposPlaza"] = value; }
        }

        int? vIdPlantilla;

        #endregion

        protected string ObtenerXmlGrupos()
        {
            XElement vXmlGrupos = null;

            var vGrupos = vLstGruposPlaza.Select(s => new XElement("GRUPO",
                new XAttribute("ID_GRUPO", s.ID_GRUPO.ToString())
                ));

            vXmlGrupos = new XElement("GRUPOS", vGrupos);

            return vXmlGrupos.ToString();
        }

        protected void AgregarGrupos(string pGruposSeleccion)
        {
            List<E_GRUPOS> vGruposSeleccionados = JsonConvert.DeserializeObject<List<E_GRUPOS>>(pGruposSeleccion);
            foreach (E_GRUPOS item in vGruposSeleccionados)
            {
                if (!vLstGruposPlaza.Exists(e => e.ID_GRUPO == item.ID_GRUPO))
                {
                    vLstGruposPlaza.Add(new E_GRUPOS
                    {
                        ID_GRUPO = item.ID_GRUPO,
                        CL_GRUPO = item.CL_GRUPO,
                        NB_GRUPO = item.NB_GRUPO
                    });
                }
            }

            rgGrupos.Rebind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int vIdRolQS = -1;
                vClOperacion = E_TIPO_OPERACION_DB.I;
                if (int.TryParse(Request.QueryString["RolId"], out vIdRolQS))
                {
                    vIdRol = vIdRolQS;
                    vClOperacion = E_TIPO_OPERACION_DB.A;
                }
                vLstPlazasGrupo = new List<E_PLAZA_GRUPO>();
                vLstGruposPlaza = new List<E_GRUPOS>();

                CargarDatos(vIdRol);
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void CargarDatos(int? pIdRol)
        {
            RolNegocio nRol = new RolNegocio();

            E_ROL vRol = nRol.ObtieneFuncionesRol(pIdRol);

            txtClRol.Text = vRol.CL_ROL;
            txtNbRol.Text = vRol.NB_ROL;
            chkActivo.Checked = vRol.FG_ACTIVO;
            vLstFunciones = vRol.LST_FUNCIONES;

            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
            List<SPE_OBTIENE_C_PLANTILLA_FORMULARIO_Result> vLstPlantillas = nPlantilla.ObtienePlantillas();

            rcbPlantilla.DataSource = vLstPlantillas.Where(w => w.CL_FORMULARIO == "INVENTARIO").ToList();
            rcbPlantilla.DataTextField = "NB_PLANTILLA_SOLICITUD";
            rcbPlantilla.DataValueField = "ID_PLANTILLA_SOLICITUD";
            rcbPlantilla.DataBind();

            if (vRol.ID_PLANTILLA != null)
            {
                rcbPlantilla.ClearSelection();
                rcbPlantilla.SelectedValue = vRol.ID_PLANTILLA.ToString();

            }


            if (vRol.XML_GRUPOS != null)
            {

                vLstGruposPlaza = (XElement.Parse(vRol.XML_GRUPOS).Elements("GRUPOS")).Select(s => new E_GRUPOS
                {
                    ID_GRUPO = int.Parse(s.Attribute("ID_GRUPO").Value),
                    CL_GRUPO = s.Attribute("CL_GRUPO").Value,
                    NB_GRUPO = s.Attribute("NB_GRUPO").Value
                }).ToList();

            }
            else
            {

                vLstGruposPlaza.Add(new E_GRUPOS() { ID_GRUPO = 1, CL_GRUPO = "TODOS", NB_GRUPO = "Todos" });
            }
        }

        protected void grdMenuModulos_NeedDataSource(object sender, TreeListNeedDataSourceEventArgs e)
        {
            grdMenuModulos.DataSource = vLstFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUWEB.ToString()) || w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.CONTROLWEB.ToString()));
        }

        protected void grdMenuGeneral_NeedDataSource(object sender, TreeListNeedDataSourceEventArgs e)
        {
            grdMenuGeneral.DataSource = vLstFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUGRAL.ToString()) || w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.CONTROLWEB.ToString()));
        }

        protected void grdMenuAdicionales_NeedDataSource(object sender, TreeListNeedDataSourceEventArgs e)
        {
            grdMenuAdicionales.DataSource = vLstFunciones.Where(w => w.CL_TIPO_FUNCION.Equals(E_TIPO_FUNCION.MENUADICIONAL.ToString()));
        }

        protected void grdMenuGeneral_ItemDataBound(object sender, TreeListItemDataBoundEventArgs e)
        {
            SeleccionarRegistro(e);
        }

        protected void grdMenuModulos_ItemDataBound(object sender, TreeListItemDataBoundEventArgs e)
        {
            SeleccionarRegistro(e);
        }

        protected void grdMenuAdicionales_ItemDataBound(object sender, TreeListItemDataBoundEventArgs e)
        {
            SeleccionarRegistro(e);
        }

        protected void SeleccionarRegistro(TreeListItemDataBoundEventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (e.Item is TreeListDataItem)
                {
                    TreeListDataItem item = e.Item as TreeListDataItem;
                    CheckBox chk = item["FG_SELECCIONADO"].Controls[0] as CheckBox;
                    item.Selected = chk.Checked;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            XElement vFunciones = new XElement("FUNCIONES");

            grdMenuGeneral.ExpandAllItems();    //SE EXPANDEN LOS NODOS PARA OBTENER LA SELECCIÓN DE TODOS LOS NODOS QUE VISUALMENTE ESTÁN OCULTOS
            vFunciones.Add(grdMenuGeneral.SelectedItems.Select(i => new XElement("FUNCION", new XAttribute("ID_FUNCION", ((TreeListDataItem)i).GetDataKeyValue("ID_FUNCION").ToString()))));

            grdMenuModulos.ExpandAllItems();    //SE EXPANDEN LOS NODOS PARA OBTENER LA SELECCIÓN DE TODOS LOS NODOS QUE VISUALMENTE ESTÁN OCULTOS
            vFunciones.Add(grdMenuModulos.SelectedItems.Select(i => new XElement("FUNCION", new XAttribute("ID_FUNCION", ((TreeListDataItem)i).GetDataKeyValue("ID_FUNCION").ToString()))));

            grdMenuAdicionales.ExpandAllItems();    //SE EXPANDEN LOS NODOS PARA OBTENER LA SELECCIÓN DE TODOS LOS NODOS QUE VISUALMENTE ESTÁN OCULTOS
            vFunciones.Add(grdMenuAdicionales.SelectedItems.Select(i => new XElement("FUNCION", new XAttribute("ID_FUNCION", ((TreeListDataItem)i).GetDataKeyValue("ID_FUNCION").ToString()))));


            if (rcbPlantilla.SelectedValue != "")
                vIdPlantilla = int.Parse(rcbPlantilla.SelectedValue);


            SPE_OBTIENE_C_ROL_Result vRol = new SPE_OBTIENE_C_ROL_Result
            {
                CL_ROL = txtClRol.Text,
                ID_PLANTILLA = vIdPlantilla,
                FG_ACTIVO = chkActivo.Checked,
                NB_ROL = txtNbRol.Text,
                XML_AUTORIZACION = vFunciones.ToString()
            };

            if (vClOperacion.Equals(E_TIPO_OPERACION_DB.A) && vIdRol != null)
                vRol.ID_ROL = (int)vIdRol;

            vRol.XML_GRUPOS = ObtenerXmlGrupos();

            RolNegocio nRol = new RolNegocio();

            E_RESULTADO vResultado = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
        }

        protected void rgGrupos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgGrupos.DataSource = vLstGruposPlaza;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgGrupos.SelectedItems)
            {
                int vIdGrupo = int.Parse(item.GetDataKeyValue("ID_GRUPO").ToString());

                E_GRUPOS vItemGrupo = vLstGruposPlaza.Where(w => w.ID_GRUPO == vIdGrupo).FirstOrDefault();
                if (vItemGrupo != null)
                {
                    vLstGruposPlaza.Remove(vItemGrupo);
                }
            }

            vLstPlazasGrupo = new List<E_PLAZA_GRUPO>();
            rgPlazasGrupo.Rebind();
            rgGrupos.Rebind();
        }

        protected void rgPlazasGrupo_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgPlazasGrupo.DataSource = vLstPlazasGrupo;
        }

        protected void rgPlazasGrupo_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgPlazasGrupo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgPlazasGrupo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgPlazasGrupo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgPlazasGrupo.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgPlazasGrupo.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vLstDatos = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vLstDatos = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

                if (vLstDatos.clTipo == "GRUPO")
                {
                    AgregarGrupos(vLstDatos.oSeleccion.ToString());
                }
            }
        }

        protected void rgGrupos_ItemCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem item = e.Item as GridEditableItem;
            if (item != null)
            {
                GruposNegocio oNegocio = new GruposNegocio();
                SPE_OBTIENE_GRUPOS_Result vGrupo = oNegocio.ObtieneGrupos(pID_GRUPO: int.Parse(item.GetDataKeyValue("ID_GRUPO").ToString())).FirstOrDefault();
                if (vGrupo != null)
                {
                    if (vGrupo.XML_GRUPO_PLAZAS != null)
                    {
                        vLstPlazasGrupo = (XElement.Parse(vGrupo.XML_GRUPO_PLAZAS).Elements("PLAZAS")).Select(s => new E_PLAZA_GRUPO
                        {
                            ID_PLAZA = int.Parse(s.Attribute("ID_PLAZA").Value),
                            CL_PLAZA = s.Attribute("CL_PLAZA").Value.ToString(),
                            NB_PUESTO = s.Attribute("NB_PUESTO").Value.ToString(),
                            NB_EMPLEADO = s.Attribute("NB_EMPLEADO").Value.ToString()

                        }).ToList();
                    }
                }

            }
            else
            {
                vLstPlazasGrupo = new List<E_PLAZA_GRUPO>();
            }
            rgPlazasGrupo.Rebind();
        }
    }
}