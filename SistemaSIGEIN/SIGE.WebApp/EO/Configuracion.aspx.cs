using Newtonsoft.Json;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.EvaluacionOrganizacional;
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

namespace SIGE.WebApp.EO
{
    public partial class Configuracion : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private List<E_EMPLEADOS> oListaEmpleados
        {
            get { return (List<E_EMPLEADOS>)ViewState["vs_config_lista_empleados"]; }
            set { ViewState["vs_config_lista_empleados"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarDatos()
        {

            txtMensajeCaptura.Content = ContextoApp.EO.Configuracion.MensajeCapturaResultados.dsMensaje;
            txtEmpleadoMensaje.Content = ContextoApp.EO.Configuracion.MensajeImportantes.dsMensaje;
            // txtMensajeBajaCapturista.Content = ContextoApp.EO.Configuracion.MensajeBajaEmpleado.dsMensaje;
            txtMensajeBajaNotificado.Content = ContextoApp.EO.Configuracion.MensajeBajaNotificador.dsMensaje;
            txtBajaReplica.Content = ContextoApp.EO.Configuracion.MensajeBajaReplica.dsMensaje;

            txtNivelMinimoII.Text = ContextoApp.EO.Configuracion.NivelMinimoIndividualIndependiente.ToString();
            txtBonoMinimoII.Text = ContextoApp.EO.Configuracion.BonoMinimoIndividualIndependiente.ToString();

            txtNivelMinimoID.Text = ContextoApp.EO.Configuracion.NivelMinimoIndividualDependiente.ToString();
            txtBonoMinimoID.Text = ContextoApp.EO.Configuracion.BonoMinimoIndividualDependiente.ToString();

            txtNivelMinimoG.Text = ContextoApp.EO.Configuracion.NivelMinimoGrupal.ToString();
            txtBonoMinimoG.Text = ContextoApp.EO.Configuracion.BonoMinimoGrupal.ToString();

            rbSueldo.Checked = ContextoApp.EO.Configuracion.SueldoAsignacion == "1" ? true : false;
            rbExtra.Checked = ContextoApp.EO.Configuracion.CampoExtra == "1" ? true : false;

            if (rbExtra.Checked)
            {
                string vValor = ContextoApp.EO.Configuracion.IdCampoExtraSueldoAsignacion;
                string vTexto = ContextoApp.EO.Configuracion.NbCampoExtraSueldoAsignacion;
                lstBusqueda.Items.Clear();
                lstBusqueda.Items.Add(new RadListBoxItem(vTexto, vValor));
            }


        }

        private void agregarEmpleados(List<E_SELECTOR_EMPLEADO> pListaSelector, string pCltipoNotificacion)
        {
            ConfiguracionNegocio nConfiguracion = new ConfiguracionNegocio();
            XElement vXmlEmpleados = new XElement("EMPLEADOS");

            foreach (E_SELECTOR_EMPLEADO str in pListaSelector)
            {
                vXmlEmpleados.Add(new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", str.idEmpleado)));
            }

            var vResultado = nConfiguracion.InsertaConfiguracionNotificado(vXmlEmpleados.ToString(), pCltipoNotificacion, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");

        }

        private void EliminarEmpleados(RadGrid pGrid)
        {
            XElement vXmlEmpleado = new XElement("EMPLEADOS");
            ConfiguracionNegocio nConfiguracion = new ConfiguracionNegocio();
            int vIdConfiguracionNotificado;

            foreach (GridDataItem item in pGrid.SelectedItems)
            {
                vIdConfiguracionNotificado = int.Parse(item.GetDataKeyValue("ID_CONFIGURACION_NOTIFICADO").ToString());
                vXmlEmpleado.Add(new XElement("EMPLEADO", new XAttribute("ID_CONFIGURACION_NOTIFICADO", vIdConfiguracionNotificado)));
            }

            E_RESULTADO vResultado = nConfiguracion.EliminaConfiguracionNotificado(vXmlEmpleado.ToString());
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            pGrid.Rebind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                //rbSueldo.Checked = true;
                //rdSeleccionar.Enabled = false;
                //rdBorrar.Enabled = false;
                CargarDatos();
            }
        }

        //protected void grdCapturaResultados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    ConfiguracionNegocio nConfiguracion = new ConfiguracionNegocio();
        //    grdCapturaResultados.DataSource = nConfiguracion.ObteneConfiguracionEvaluacionOrganizacional("CAPTURISTA");
        //}

        protected void grdRecepcionMensajes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ConfiguracionNegocio nConfiguracion = new ConfiguracionNegocio();
            grdRecepcionMensajes.DataSource = nConfiguracion.ObteneConfiguracionEvaluacionOrganizacional("IMPORTANTE");

            if (nConfiguracion.ObteneConfiguracionEvaluacionOrganizacional("IMPORTANTE").Count() > 0)
                btnAgregarImportante.Enabled = false;
            else 
                btnAgregarImportante.Enabled = true;
        }

        //protected void grdBajaCapturista_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //    ConfiguracionNegocio nConfiguracion = new ConfiguracionNegocio();
        //    // grdBajaCapturista.DataSource = nConfiguracion.ObteneConfiguracionEvaluacionOrganizacional("BAJACAPTURISTA");
        //}

        protected void grdBajaNotificado_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ConfiguracionNegocio nConfiguracion = new ConfiguracionNegocio();
            grdBajaNotificado.DataSource = nConfiguracion.ObteneConfiguracionEvaluacionOrganizacional("BAJANOTIFICADO");

            if (nConfiguracion.ObteneConfiguracionEvaluacionOrganizacional("BAJANOTIFICADO").Count() > 0)
                btnAltaEmpleadoBajaNotificado.Enabled = false;
            else
                btnAltaEmpleadoBajaNotificado.Enabled = true;
        }

        protected void ramConfiguracion_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;
            string vClTipo;

            if (pParameter != null)
            {
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);
                vClTipo = vSeleccion.clTipo;

                List<E_SELECTOR_EMPLEADO> listaEmpleados = new List<E_SELECTOR_EMPLEADO>();
                listaEmpleados = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(vSeleccion.oSeleccion.ToString());
                agregarEmpleados(listaEmpleados, vClTipo);

                //if (vSeleccion.clTipo == "CAPTURISTA")
                //    grdCapturaResultados.Rebind();


                if (vSeleccion.clTipo == "IMPORTANTE")
                    grdRecepcionMensajes.Rebind();


                //if (vSeleccion.clTipo == "BAJACAPTURISTA")
                //    grdBajaCapturista.Rebind();


                if (vSeleccion.clTipo == "BAJANOTIFICADO")
                    grdBajaNotificado.Rebind();

                if (vSeleccion.clTipo == "BAJAREPLICA")
                    rgBajaReplica.Rebind();

            }
        }

        //protected void btnEliminaCapturistas_Click(object sender, EventArgs e)
        //{
        //    EliminarEmpleados(grdCapturaResultados);
        //}

        protected void btnEliminarImportante_Click(object sender, EventArgs e)
        {
            EliminarEmpleados(grdRecepcionMensajes);
        }

        protected void btnBajaMensajeBajaCapturista_Click(object sender, EventArgs e)
        {
            //EliminarEmpleados(grdBajaCapturista);         
        }

        protected void btnBajaEmpleadoBajaNotificado_Click(object sender, EventArgs e)
        {
            EliminarEmpleados(grdBajaNotificado);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            EvaluacionOrganizacional EO = new EvaluacionOrganizacional();
            EO.MensajeCorreoEvaluador.dsMensaje = "<p>Estimado(a):</p><br/>" +
                                                     "<p>Tus cuestionarios a completar por medio del sistema para el Clima Laboral están listos.<br/>" +
                                                     "El objetivo de esta encuesta es detectar áreas de oportunidad que nos ayuden a mejorar el ambiente laboral de la empresa.<br/>" +
                                                     "Agradecemos el tiempo que te tomes en contestar la siguiente encuesta, la información se maneja de forma anónima, sientete con la confianza de responder de manera libre<br/>" +
                                                     "y contesta lo que refleje tu punto de vista.</p></p>" +
                                                     "<p>Para contestarlos, por favor haz clic en la siguiente liga:<br/>" +
                                                     " [URL] </p><br/><br/>" +
                                                     " <p>Tu contraseña de acceso es [contraseña] <br/> <br/> Gracias por tu apoyo!</p>";

            EO.Configuracion.BonoMinimoGrupal = decimal.Parse(txtBonoMinimoG.Text);
            EO.Configuracion.BonoMinimoIndividualDependiente = decimal.Parse(txtBonoMinimoID.Text);
            EO.Configuracion.BonoMinimoIndividualIndependiente = decimal.Parse(txtBonoMinimoII.Text);

            EO.Configuracion.NivelMinimoIndividualDependiente = decimal.Parse(txtNivelMinimoID.Text);
            EO.Configuracion.NivelMinimoIndividualIndependiente = decimal.Parse(txtNivelMinimoII.Text);
            EO.Configuracion.NivelMinimoGrupal = decimal.Parse(txtNivelMinimoG.Text);

            // EO.Configuracion.MensajeBajaEmpleado.dsMensaje = txtMensajeBajaCapturista.Content != "" ? txtMensajeBajaCapturista.Content.Replace("&lt;","") : "N/A";
            EO.Configuracion.MensajeBajaNotificador.dsMensaje = txtMensajeBajaNotificado.Content != "" ? txtMensajeBajaNotificado.Content.Replace("&lt;", "") : "N/A";
            EO.Configuracion.MensajeCapturaResultados.dsMensaje = txtMensajeCaptura.Content != "" ? txtMensajeCaptura.Content.Replace("&lt;", "") : "N/A";
            EO.Configuracion.MensajeImportantes.dsMensaje = txtEmpleadoMensaje.Content != "" ? txtEmpleadoMensaje.Content.Replace("&lt;", "") : "N/A";
            EO.Configuracion.MensajeBajaReplica.dsMensaje = txtBajaReplica.Content != "" ? txtBajaReplica.Content.Replace("&lt;", "") : "N/A";


            EO.Configuracion.SueldoAsignacion = rbSueldo.Checked ? "1" : "0";
            EO.Configuracion.CampoExtra = rbExtra.Checked ? "1" : "0";
            EO.Configuracion.IdCampoExtraSueldoAsignacion = rbExtra.Checked ? lstBusqueda.Items[0].Value : "N/A";
            EO.Configuracion.NbCampoExtraSueldoAsignacion = rbExtra.Checked ? lstBusqueda.Items[0].Text : "N/A";

            ContextoApp.EO = EO;
            ContextoApp.SaveConfiguration(vClUsuario, "Configuracion.aspx");
            UtilMensajes.MensajeResultadoDB(rwmMensaje, "Proceso exitoso.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");
        }

        //protected void grdCapturaResultados_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridPagerItem)
        //    {
        //        RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

        //        PageSizeCombo.Items.Clear();
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
        //        PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdCapturaResultados.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
        //        PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdCapturaResultados.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
        //        PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdCapturaResultados.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
        //        PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdCapturaResultados.MasterTableView.ClientID);
        //        PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
        //        PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdCapturaResultados.MasterTableView.ClientID);
        //        PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
        //    }
        //}

        protected void grdRecepcionMensajes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdRecepcionMensajes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdRecepcionMensajes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdRecepcionMensajes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdRecepcionMensajes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdRecepcionMensajes.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdBajaNotificado_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdBajaNotificado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdBajaNotificado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdBajaNotificado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdBajaNotificado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdBajaNotificado.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void rgBajaReplica_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ConfiguracionNegocio nConfiguracion = new ConfiguracionNegocio();
            rgBajaReplica.DataSource = nConfiguracion.ObteneConfiguracionEvaluacionOrganizacional("BAJAREPLICA");

            if (nConfiguracion.ObteneConfiguracionEvaluacionOrganizacional("BAJAREPLICA").Count() > 0)
                btnAgregarEncargado.Enabled = false;
            else
                btnAgregarEncargado.Enabled = true;

        }

        protected void rgBajaReplica_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgBajaReplica.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgBajaReplica.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgBajaReplica.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgBajaReplica.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgBajaReplica.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnEliminarEncargado_Click(object sender, EventArgs e)
        {
            EliminarEmpleados(rgBajaReplica);
        }

    }
}