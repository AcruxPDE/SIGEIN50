using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
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
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int? vIdCandidato
        {
            get { return (int?)ViewState["vs_vpsc_id_candidato"]; }
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

        private int? vIdSolicitud
        {
            get { return (int?)ViewState["vs_vcc_id_solicitud"]; }
            set { ViewState["vs_vcc_id_solicitud"] = value; }
        }

        public string vClTipoConsulta
        {
            get { return (string)ViewState["vs_vClTipoConsulta"]; }
            set { ViewState["vs_vClTipoConsulta"] = value; }
        }

        public int? vIdRequisicion
        {
            get { return (int?)ViewState["vs_vIdRequisicion"]; }
            set { ViewState["vs_vIdRequisicion"] = value; }
        }

        public int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
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

                if (Request.Params["SolicitudId"] != null)
                {
                    vIdSolicitud = int.Parse(Request.Params["SolicitudId"].ToString());
                }
                if (Request.Params["IdEmpleado"] != null)
                {
                    vIdEmpleado = int.Parse(Request.Params["IdEmpleado"].ToString());
                }

                if (vIdSolicitud != null)
                {
                    SolicitudNegocio nSoilcitud = new SolicitudNegocio();
                    var oSolicitud = nSoilcitud.ObtieneSolicitudes(ID_SOLICITUD: vIdSolicitud).FirstOrDefault();

                    if (oSolicitud != null)
                    {
                        txtCandidato.InnerText = oSolicitud.NB_CANDIDATO_COMPLETO;
                        txtClaveSolicitud.InnerText = oSolicitud.CL_SOLICITUD;
                    }
                }
                else if (vIdEmpleado != null)
                {
                    SolicitudNegocio nSolicitud = new SolicitudNegocio();
                    E_RESULTADO vResultado = nSolicitud.InsertaCandidatoEmpleado(vIdEmpleado, vClUsuario, vNbPrograma);
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                    var idCandidato = 0;
                    bool esNumero;

                    if (vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_CANDIDATO").FirstOrDefault() != null)
                    {
                        esNumero = int.TryParse(vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_CANDIDATO").FirstOrDefault().DS_MENSAJE, out idCandidato);
                        vIdCandidato = idCandidato;

                        var oSolicitud = nSolicitud.ObtieneSolicitudes(ID_CANDIDATO: vIdCandidato).FirstOrDefault();

                        if (oSolicitud != null)
                        {
                            txtCandidato.InnerText = oSolicitud.NB_CANDIDATO_COMPLETO;
                            txtClaveSolicitud.InnerText = oSolicitud.CL_SOLICITUD;
                            vIdSolicitud = oSolicitud.ID_SOLICITUD;
                        }
                    }
                }

                if (Request.Params["IdRequisicion"] != null)
                    vIdRequisicion = int.Parse(Request.Params["IdRequisicion"].ToString());

                if (Request.Params["pClTipoConsulta"] != null)
                    if (Request.Params["pClTipoConsulta"].ToString() == "CONSULTAR")
                    {
                        vClTipoConsulta = "CONSULTAR";
                        btnIniciarProceso.Visible = false;
                        btnContinuarProceso.Visible = false;
                        btnVerProceso.Visible = true;
                    }
            }
        }

        protected void grdProcesoSeleccion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdProcesoSeleccion.DataSource = new ProcesoSeleccionNegocio().ObtieneProcesoSeleccion(pIdCandidato: vIdCandidato);
        }

        protected void grdProcesoSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (vClTipoConsulta != "CONSULTAR")
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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            foreach (GridDataItem item in grdProcesoSeleccion.SelectedItems)
            {
                int vIdProcesoSeleccion = int.Parse(item.GetDataKeyValue("ID_PROCESO_SELECCION").ToString());
                E_RESULTADO vResultado = nProcesoSeleccion.EliminaProcesoSeleccion(pIdProcesoSeleccion: vIdProcesoSeleccion);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
                grdProcesoSeleccion.Rebind();
            }
        }
    }
}