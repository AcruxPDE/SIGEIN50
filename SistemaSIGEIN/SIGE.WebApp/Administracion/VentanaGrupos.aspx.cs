using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.AdministracionSitio;
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
    public partial class VentanaGrupos : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_PLAZA_GRUPO> vLstPlazasGrupo
        {
            get { return (List<E_PLAZA_GRUPO>)ViewState["vs_vLstPlazasGrupo"]; }
            set { ViewState["vs_vLstPlazasGrupo"] = value; }
        }

        private int? vIdGrupo
        {
            get { return (int?)ViewState["vs_vIdGrupo"]; }
            set { ViewState["vs_vIdGrupo"] = value; }
        }

        #endregion

        #region Metodos

        private void AgregarPlaza(string pListaSeleccion)
        {
            List<E_PLAZA_GRUPO> vLstSeleccionPlazas = JsonConvert.DeserializeObject<List<E_SELECCION_PLAZA>>(pListaSeleccion).Select(s => new E_PLAZA_GRUPO { ID_PLAZA = s.idPlaza, CL_PLAZA = s.clPlaza, NB_PUESTO = s.nbPuesto, NB_EMPLEADO = s.nbEmpleado }).ToList();
            foreach (E_PLAZA_GRUPO item in vLstSeleccionPlazas)
            {
                if (!vLstPlazasGrupo.Exists(e => e.ID_PLAZA == item.ID_PLAZA))
                {
                    vLstPlazasGrupo.Add(new E_PLAZA_GRUPO
                    {
                        ID_PLAZA = item.ID_PLAZA,
                        CL_PLAZA = item.CL_PLAZA,
                        NB_PUESTO = item.NB_PUESTO,
                        NB_EMPLEADO = item.NB_EMPLEADO
                    }
                        );
                }
            }

            rgGrupos.Rebind();
        }

        private bool ValidarDatos()
        {
            if (txtClaveGrupo.Text != "" && txtClaveGrupo.Text != null)
                if (txtNombreGrupo.Text != "" && txtNombreGrupo.Text != null)
                    return true;

            return false;
        }

        private string GenerarXmlPlazasGrupo()
        {
            XElement vXmlPlazasGrupos = null;

            var vPlazasLista = vLstPlazasGrupo.Select(x => new XElement("PLAZA",
                                                           new XAttribute("ID_PLAZA", x.ID_PLAZA.ToString())
                                                        ));

            vXmlPlazasGrupos = new XElement("PLAZAS", vPlazasLista);
            return vXmlPlazasGrupos.ToString();
        }

        private void CargarDatos()
        {
            GruposNegocio oNegocio = new GruposNegocio();
            SPE_OBTIENE_GRUPOS_Result vGrupo = oNegocio.ObtieneGrupos(pID_GRUPO: vIdGrupo).FirstOrDefault();
            if (vGrupo != null)
            {
                txtClaveGrupo.Text = vGrupo.CL_GRUPO;
                txtNombreGrupo.Text = vGrupo.NB_GRUPO;

                if (vGrupo.ID_GRUPO == 1 && vGrupo.CL_GRUPO == "TODOS")
                {
                    txtClaveGrupo.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnSeleccionar.Enabled = false;
                }

                if (vGrupo.XML_GRUPO_PLAZAS != null)
                {
                    //foreach (XElement item in XElement.Parse(vGrupo.XML_GRUPO_PLAZAS).Elements("PLAZAS"))
                    //{
                    //    vLstPlazasGrupo.Add(new E_PLAZA_GRUPO
                    //    {
                    //        ID_PLAZA = int.Parse(item.Attribute("ID_PLAZA").Value),
                    //        CL_PLAZA = item.Attribute("CL_PLAZA").Value.ToString(),
                    //        NB_PUESTO = item.Attribute("NB_PUESTO").Value.ToString(),
                    //        NB_EMPLEADO = item.Attribute("NB_EMPLEADO").Value.ToString()
                    //    });

                    //}

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int vIdGrupoRequest = 0;
                if (int.TryParse(Request.Params["pIdGrupo"], out vIdGrupoRequest))
                    vIdGrupo = vIdGrupoRequest;

                vLstPlazasGrupo = new List<E_PLAZA_GRUPO>();

                if (vIdGrupo != null)
                {
                    CargarDatos();
                }


            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
        }

        protected void rgGrupos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgGrupos.DataSource = vLstPlazasGrupo;
        }

        protected void ramGrupos_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vLstSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vLstSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

                if (vLstSeleccion.clTipo == "PLAZA")
                {
                    AgregarPlaza(vLstSeleccion.oSeleccion.ToString());
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            foreach (GridDataItem item in rgGrupos.SelectedItems)
            {
                int vIdPlaza = int.Parse(item.GetDataKeyValue("ID_PLAZA").ToString());
                E_PLAZA_GRUPO vValorItem = vLstPlazasGrupo.Where(w => w.ID_PLAZA == vIdPlaza).FirstOrDefault();

                if (vValorItem != null)
                {
                    vLstPlazasGrupo.Remove(vValorItem);
                }
            }

            rgGrupos.Rebind();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                string vClGrupo = txtClaveGrupo.Text;
                string vNbGrupo = txtNombreGrupo.Text;
                string vXmlPlazasGrupo = GenerarXmlPlazasGrupo();
                string vTipoTransaccion = vIdGrupo == null ? "I" : "A";

                GruposNegocio oNeg = new GruposNegocio();
                E_RESULTADO vResultado = oNeg.InsertaActualizaGrupo(pID_GRUPO: vIdGrupo, pCL_GRUPO: vClGrupo, pNB_GRUPO: vNbGrupo, pXML_PLAZAS: vXmlPlazasGrupo, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pCL_TIPO_TRANSACCION: vTipoTransaccion);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindows");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwMensaje, "La clave y nombre del grupo son requeridos.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150);
            }
        }

        protected void rgGrupos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgGrupos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}