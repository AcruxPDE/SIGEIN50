using SIGE.Entidades;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaProcesoSeleccionCandidato : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private string vNbUsuario;

        public int vIdCandidato
        {
            get { return (int)ViewState["vs_vpsc_id_candidato"]; }
            set { ViewState["vs_vpsc_id_candidato"] = value; }
        }

        public int vIdBateria {
            get { return (int)ViewState["vs_vpsc_id_bateria"]; }
            set { ViewState["vs_vpsc_id_bateria"] = value; }
        }

        public string vClToken {
            get { return (string)ViewState["vs_vpsc_cl_token"]; }
            set { ViewState["vs_vpsc_cl_token"] = value; }
        }
        
        #endregion

        #region Funciones
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vNbUsuario = ContextoUsuario.oUsuario.NB_USUARIO;

            if (!Page.IsPostBack)
            {
                if (Request.Params["IdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["IdCandidato"].ToString());
                }

                if (Request.Params["IdBateria"] != null)
                {
                    vIdBateria = int.Parse(Request.Params["IdBateria"].ToString());
                }
                else
                {
                    vIdBateria = 0;
                }

                if (Request.Params["ClToken"] != null)
                {
                    vClToken = Request.Params["ClToken"].ToString();
                }
                else
                {
                    vClToken = "";
                }
            }
        }

        protected void grdProcesoSeleccion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            grdProcesoSeleccion.DataSource = nProcesoSeleccion.ObtieneProcesoSeleccion(pIdCandidato: vIdCandidato);
        }

        protected void grdProcesoSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdProcesoSeleccion.SelectedItems)
            {
                if (item.GetDataKeyValue("CL_ESTADO").ToString() != null)
                {
                    if (item.GetDataKeyValue("CL_ESTADO").ToString() == "Terminado" || item.GetDataKeyValue("CL_ESTADO").ToString() == "TERMINADO")
                    {
                        btnContinuarProceso.Enabled = false;
                        btnVerProceso.Visible = true;
                        lbMensaje.Visible = true;
                    }
                    else
                    {
                        btnContinuarProceso.Enabled = true;
                        btnVerProceso.Visible = false;
                        lbMensaje.Visible = false;
                    }
                }
                else
                {
                    btnContinuarProceso.Enabled = true;
                    btnVerProceso.Visible = false;
                    lbMensaje.Visible = false;
                }
            }
        }

        protected void btnIniciarProceso_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "script", "OpenProcesoSeleccionNuevoWindow();", true);
        }

        protected void grdProcesoSeleccion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdProcesoSeleccion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdProcesoSeleccion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdProcesoSeleccion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdProcesoSeleccion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdProcesoSeleccion.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }
}