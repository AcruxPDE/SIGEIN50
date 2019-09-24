using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
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
using WebApp.Comunes;

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
            if (vResultado.CL_TIPO_ERROR.ToString() == "SUCCESSFUL")
            {

                List<E_BAJA_IMPORTANTE_EO> LstEmpleadoBajaImportante = new List<E_BAJA_IMPORTANTE_EO>();
                RotacionPersonalNegocio nBaja = new RotacionPersonalNegocio();
                E_BAJA_IMPORTANTE_EO bBajaNotificado = new E_BAJA_IMPORTANTE_EO();
                LstEmpleadoBajaImportante = nBaja.ObtieneEmpleadoImportante().ToList();
                bBajaNotificado = LstEmpleadoBajaImportante.Where(w => w.CL_TIPO_NOTIFICACION == "BAJANOTIFICADO").FirstOrDefault();
                if (bBajaNotificado != null)
                    EnviarCorreoImportate(bBajaNotificado.CL_CORREO_ELECTRONICO, bBajaNotificado.NB_EMPLEADO_COMPLETO);


            }


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

            ActualizarLicencia();

        }

        protected void ActualizarLicencia()
        {
            UtilLicencias nLicencia = new UtilLicencias();
            E_RESULTADO vResultado = nLicencia.generaClaves(ContextoApp.Licencia.clCliente, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.FirstOrDefault().DS_MENSAJE;

            SPE_OBTIENE_S_CONFIGURACION_Result vConfiguracionLicencia = new SPE_OBTIENE_S_CONFIGURACION_Result();
            ConfiguracionNegocio oConfiguracion = new ConfiguracionNegocio();

            vConfiguracionLicencia = oConfiguracion.obtieneConfiguracionGeneral();

            if (vConfiguracionLicencia.CL_LICENCIAMIENTO == null)
            {
                nLicencia.generaXmlLicencias(vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.CL_PASS_WS, "Web Service", "Web Service");
                nLicencia.insertaXmlIdentificacion(vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.CL_PASS_WS, "Web Service", "Web Service");

                vConfiguracionLicencia = oConfiguracion.obtieneConfiguracionGeneral();
            }

            ContextoApp.Licencia.clCliente = vConfiguracionLicencia.CL_CLIENTE;
            ContextoApp.Licencia.clEmpresa = vConfiguracionLicencia.CL_EMPRESA;
            ContextoApp.Licencia.clLicencia = vConfiguracionLicencia.CL_LICENCIAMIENTO;
            ContextoApp.Licencia.clPassWs = vConfiguracionLicencia.CL_PASS_WS;
            ContextoApp.Licencia.feCreacion = vConfiguracionLicencia.FE_CREACION;
            ContextoApp.Licencia.objAdicional = vConfiguracionLicencia.OBJ_ADICIONAL;

            Crypto desencripta = new Crypto();
            string keyPassword = vConfiguracionLicencia.CL_PASS_WS.Substring(0, 16);
            string cadenaDesencriptadaLic = desencripta.descifrarTextoAES(vConfiguracionLicencia.CL_LICENCIAMIENTO, vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.FE_CREACION, "SHA1", 22, keyPassword, 256);
            string cadenaDesencriptadaObj = desencripta.descifrarTextoAES(vConfiguracionLicencia.OBJ_ADICIONAL, vConfiguracionLicencia.CL_CLIENTE, vConfiguracionLicencia.FE_CREACION, "SHA1", 22, keyPassword, 256);
            XElement XmlConfiguracionLicencia = XElement.Parse(cadenaDesencriptadaLic);
            XElement XmlConfiguracionCliente = XElement.Parse(cadenaDesencriptadaObj);

            List<E_LICENCIA> lstLicencia = XmlConfiguracionCliente.Descendants("LICENCIA").Select(x => new E_LICENCIA
            {
                CL_CLIENTE = UtilXML.ValorAtributo<string>(x.Attribute("CL_CLIENTE")),
                CL_SISTEMA = UtilXML.ValorAtributo<string>(x.Attribute("CL_SISTEMA")),
                CL_EMPRESA = UtilXML.ValorAtributo<string>(x.Attribute("CL_EMPRESA")),
                CL_MODULO = UtilXML.ValorAtributo<string>(x.Attribute("CL_MODULO")),
                NO_RELEASE = UtilXML.ValorAtributo<string>(x.Attribute("NO_RELEASE")),
                FE_INICIO = UtilXML.ValorAtributo<string>(x.Attribute("FE_INICIO")),
                FE_FIN = UtilXML.ValorAtributo<string>(x.Attribute("FE_FIN")),
                NO_VOLUMEN = UtilXML.ValorAtributo<string>(x.Attribute("NO_VOLUMEN"))
            }).ToList();

            ContextoApp.IDP.LicenciaIntegracion.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "IDP", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.FYD.LicenciaFormacion.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "FYD", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.EO.LicenciaED.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "ED", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.EO.LicenciaCL.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "CL", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.EO.LicenciaRDP.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "RDP", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.MPC.LicenciaMetodologia.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "MPC", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.RP.LicenciaReportes.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "RP", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "CI", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "PDE", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);
            ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo = (nLicencia.validaLicencia(vConfiguracionLicencia.CL_CLIENTE, "SIGEIN", vConfiguracionLicencia.CL_EMPRESA, "NOMINA", "5.00", "Sistema", "Web Service", XmlConfiguracionLicencia, XmlConfiguracionCliente).MENSAJE.FirstOrDefault().DS_MENSAJE);

            if (lstLicencia.Count > 0)
            {
                ContextoApp.InfoEmpresa.Volumen = int.Parse(lstLicencia.FirstOrDefault().NO_VOLUMEN);

                if (ContextoApp.IDP.LicenciaIntegracion.MsgActivo == "1" || ContextoApp.FYD.LicenciaFormacion.MsgActivo == "1" || ContextoApp.EO.LicenciaED.MsgActivo == "1"
         || ContextoApp.EO.LicenciaCL.MsgActivo == "1" || ContextoApp.EO.LicenciaRDP.MsgActivo == "1"
           || ContextoApp.MPC.LicenciaMetodologia.MsgActivo == "1" || ContextoApp.RP.LicenciaReportes.MsgActivo == "1" || ContextoApp.CI.LicenciaConsultasInteligentes.MsgActivo == "1"
           || ContextoApp.PDE.LicenciaPuntoEncuentro.MsgActivo == "1" || ContextoApp.ANOM.LicenciaAccesoModulo.MsgActivo == "1")
                {
                    ContextoApp.InfoEmpresa.MsgSistema = "1";
                }
                else
                {
                    ContextoApp.InfoEmpresa.MsgSistema = "El cliente actual no cuenta con licencias.";
                }
            }

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

        private void EnviarCorreoImportate(string correo, string NombreBajaNotificado)
        {
            ProcesoExterno pe = new ProcesoExterno();
            string vClCorreo;
            string vNbBajaNotificado;
            string vMensaje;
            vClCorreo = correo;
            vNbBajaNotificado = NombreBajaNotificado;
            vMensaje = "Estimado " + NombreBajaNotificado + " ,la persona configurada para recibir las notificaciones de los periodos de desempeño, ha sido dado de baja";

            if (Utileria.ComprobarFormatoEmail(vClCorreo) && (vClCorreo != null || vClCorreo == ""))
            {
                //Envío de correo
                bool vEstatusCorreo = pe.EnvioCorreo(vClCorreo, vNbBajaNotificado, "Configuración Desempeño ", vMensaje);

            }
        }

    }
}