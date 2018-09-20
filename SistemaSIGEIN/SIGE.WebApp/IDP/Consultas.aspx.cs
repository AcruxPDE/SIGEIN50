using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
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
    public partial class Consultas : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        //public Guid? vIdPuestoVsCandidatos
        //{
        //    get { return (Guid?)ViewState["vs_vIdPuestoVsCandidatos"]; }
        //    set { ViewState["vs_vIdPuestoVsCandidatos"] = value; }
        //}

        #endregion

        public bool vbtnResultados;
        public bool vbtnPersonalResumida;
        public bool vbtnPersonalDetallada;
        public bool vbtnPuestoPersonas;
        public bool vbtnPersonaPuestos;
        public bool vbtnConsultaGlobal;
        public bool vbtnIntegral;


        protected void SeguridadProcesos()
        {
            btnResultados.Enabled = vbtnResultados = ContextoUsuario.oUsuario.TienePermiso("J.A.C.A");
            btnPersonalResumida.Enabled = vbtnPersonalResumida = ContextoUsuario.oUsuario.TienePermiso("J.A.C.B");
            btnPersonalDetallada.Enabled = vbtnPersonalDetallada = ContextoUsuario.oUsuario.TienePermiso("J.A.C.C");
            btnPuestoPersonas.Enabled = vbtnPuestoPersonas = ContextoUsuario.oUsuario.TienePermiso("J.A.C.D");
            btnPersonaPuestos.Enabled = vbtnPersonaPuestos = ContextoUsuario.oUsuario.TienePermiso("J.A.C.E");
            btnConsultaGlobal.Enabled = vbtnConsultaGlobal = ContextoUsuario.oUsuario.TienePermiso("J.A.C.F");
            btnIntegral.Enabled = vbtnIntegral = ContextoUsuario.oUsuario.TienePermiso("J.A.C.G");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SeguridadProcesos();
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void grdCandidatos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            SolicitudNegocio nSolicitudes = new SolicitudNegocio();
            grdCandidatos.DataSource = nSolicitudes.ObtieneCandidatosBaterias();
        }

        protected void grdCandidatos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCandidatos.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        //protected void btnPuestoPersonas_Click(object sender, EventArgs e)
        //{
        //    if (grdCandidatos.SelectedItems.Count > 0)
        //    {
        //        vIdPuestoVsCandidatos = Guid.NewGuid();
        //        ContextoConsultasComparativas.oPuestoVsCandidatos = new List<E_PUESTO_VS_CANDIDATOS>();

        //        ContextoConsultasComparativas.oPuestoVsCandidatos.Add(new E_PUESTO_VS_CANDIDATOS { vIdPuestoVsCandidatos = (Guid)vIdPuestoVsCandidatos });

        //        foreach (GridDataItem item in grdCandidatos.SelectedItems)
        //        {
        //            ContextoConsultasComparativas.oPuestoVsCandidatos.Where(t => t.vIdPuestoVsCandidatos == (Guid)vIdPuestoVsCandidatos).FirstOrDefault().vListaCandidatos.Add(int.Parse(item.GetDataKeyValue("ID_CANDIDATO").ToString()));
        //        }

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "OpenConsultaPuestoPersonas();", true);
        //    }
        //    else
        //        UtilMensajes.MensajeResultadoDB(rwMensaje, "Selecciona un candidato.", E_TIPO_RESPUESTA_DB.ERROR, 400, 150, "");
        //}

    }
}