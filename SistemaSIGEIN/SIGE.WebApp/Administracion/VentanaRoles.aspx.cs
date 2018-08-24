using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaRoles : System.Web.UI.Page
    {
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

            SPE_OBTIENE_C_ROL_Result vRol = new SPE_OBTIENE_C_ROL_Result
            {
                CL_ROL = txtClRol.Text,
                FG_ACTIVO = chkActivo.Checked,
                NB_ROL = txtNbRol.Text,
                XML_AUTORIZACION = vFunciones.ToString()
            };

            if (vClOperacion.Equals(E_TIPO_OPERACION_DB.A) && vIdRol != null)
                vRol.ID_ROL = (int)vIdRol;

            RolNegocio nRol = new RolNegocio();

            E_RESULTADO vResultado = nRol.InsertaActualizaRoles(vClOperacion, vRol, vFunciones, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
        }
    }
}