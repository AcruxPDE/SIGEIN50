using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.IntegracionDePersonal;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.IDP
{
    public partial class AplicacionPruebas : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int? vIdRol;
        private XElement BATERIASSELECCIONADAS { get; set; }

        private string vIdBateriaSeleccionada
        {
            get { return (string)ViewState["vs_ap_id_bateria_seleccionada"]; }
            set { ViewState["vs_ap_id_bateria_seleccionada"] = value; }
        }

        public Guid? vIdGeneraBaterias
        {
            get { return (Guid?)ViewState["vs_vIdGeneraBaterias"]; }
            set { ViewState["vs_vIdGeneraBaterias"] = value; }
        }

        private int? vIdCandidato
        {
            get { return (int?)ViewState["vs_idCandidato"]; }
            set { ViewState["vs_idCandidato"] = value; }
        }

        private int? vIdEmpleado
        {
            get { return (int?)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        public int? vIdRequisicion
        {
            get { return (int?)ViewState["vs_vIdRequisicion"]; }
            set { ViewState["vs_vIdRequisicion"] = value; }
        }

        public bool vbtnCrearBateria;
        public bool vbtnAplicarPruebas;
        public bool vbtnAgrgarPruebas;
        public bool vbtnCapturaManual;
        public bool vbtnVizualizarPruebas;
        public bool vbtnProceso;
        public bool vbtnAsigRequisicion;
        public bool vbtnContratar;

        #endregion

        #region Funciones

        private void SeleccionarEmpleado(List<SPE_OBTIENE_EMPLEADOS_PROCESOS_EVALUACION_Result> pLstEmpleado)
        {
            if (pLstEmpleado != null && vIdEmpleado != null)
            {
                int vNumeroLista = 0;
                var vEmpleadoSeleccionado = pLstEmpleado.Where(w => w.M_EMPLEADO_ID_EMPLEADO == vIdEmpleado).FirstOrDefault();
                int vIndex = pLstEmpleado.IndexOf(vEmpleadoSeleccionado);
                if (vEmpleadoSeleccionado != null)
                    vNumeroLista = vIndex;

                decimal vNumeroItem = Math.Round(((decimal)vNumeroLista / 10), 1);
                int vNumeroPag = (vNumeroLista / 10);
                if (vNumeroItem > vNumeroPag)
                    rgPruebasEmpleados.CurrentPageIndex = (vNumeroPag - 1) + 1;
                else
                    rgPruebasEmpleados.CurrentPageIndex = (vNumeroPag - 1);

            }
        }

        private void SeleccionarCandidato(List<SPE_OBTIENE_SOLICITUDES_PROCESOS_EVALUACION_Result> pLstCandidatos)
        {
            if (pLstCandidatos != null && vIdCandidato != null && vIdEmpleado == null)
            {
                int vNumeroLista = 0;
                var vCandidatoSeleccionado = pLstCandidatos.Where(w => w.ID_CANDIDATO == vIdCandidato).FirstOrDefault();
                if (vCandidatoSeleccionado != null)
                    vNumeroLista = int.Parse(vCandidatoSeleccionado.RENGLON.ToString());

                decimal vNumeroItem = Math.Round(((decimal)vNumeroLista / 10), 1);
                int vNumeroPag = (vNumeroLista / 10);
                if (vNumeroItem > vNumeroPag)
                    grdSolicitudes.CurrentPageIndex = (vNumeroPag - 1) + 1;
                else
                    grdSolicitudes.CurrentPageIndex = (vNumeroPag - 1);

            }
        }

        private int GenerarIdCandidato(int pIdEmpleado)
        {
            int vIdCandidato = 0;

            SolicitudNegocio nSolicitud = new SolicitudNegocio();
            E_RESULTADO vResultado = nSolicitud.InsertaCandidatoEmpleado(pIdEmpleado, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            var idCandidato = 0;
            bool esNumero;

            if (vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_CANDIDATO").FirstOrDefault() != null)
            {
                esNumero = int.TryParse(vResultado.MENSAJE.Where(x => x.CL_IDIOMA == "ID_CANDIDATO").FirstOrDefault().DS_MENSAJE, out idCandidato);
                vIdCandidato = idCandidato;
            }

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                return vIdCandidato;
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                return 0;
            }
        }

        private void SeguridadProcesos()
        {
            btnCrearBateria.Enabled = vbtnCrearBateria = ContextoUsuario.oUsuario.TienePermiso("J.A.B.A");
            btnCrearBateriaEmp.Enabled = vbtnCrearBateria = ContextoUsuario.oUsuario.TienePermiso("J.A.B.A");

            btnAplicarPruebas.Enabled = vbtnAplicarPruebas = ContextoUsuario.oUsuario.TienePermiso("J.A.B.B");
            btnAplicarPruebaEmp.Enabled = vbtnAplicarPruebas = ContextoUsuario.oUsuario.TienePermiso("J.A.B.B");

            btnAgrgarPruebas.Enabled = vbtnAgrgarPruebas = ContextoUsuario.oUsuario.TienePermiso("J.A.B.C");
            btnAgregarPruebaEmp.Enabled = vbtnAgrgarPruebas = ContextoUsuario.oUsuario.TienePermiso("J.A.B.C");


            btnCapturaManual.Enabled = vbtnCapturaManual = ContextoUsuario.oUsuario.TienePermiso("J.A.B.D");
            btnManualEmp.Enabled = vbtnCapturaManual = ContextoUsuario.oUsuario.TienePermiso("J.A.B.D");

            btnVizualizarPruebas.Enabled = vbtnVizualizarPruebas = ContextoUsuario.oUsuario.TienePermiso("J.A.B.E");
            btnVizualizaEmp.Enabled = vbtnVizualizarPruebas = ContextoUsuario.oUsuario.TienePermiso("J.A.B.E");

            btnProceso.Enabled = vbtnProceso = ContextoUsuario.oUsuario.TienePermiso("J.A.B.F");
            btnProcesoEvalEmp.Enabled = vbtnProceso = ContextoUsuario.oUsuario.TienePermiso("J.A.B.F");

            btnAsigRequisicion.Enabled = vbtnAsigRequisicion = ContextoUsuario.oUsuario.TienePermiso("J.A.B.G");
            btAsignaReqEmp.Enabled = vbtnAsigRequisicion = ContextoUsuario.oUsuario.TienePermiso("J.A.B.G");

            btnContratar.Enabled = vbtnContratar = ContextoUsuario.oUsuario.TienePermiso("J.A.B.H");

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = (ContextoUsuario.oUsuario != null ? ContextoUsuario.oUsuario.CL_USUARIO : "INVITADO");
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {
                if (Request.Params["pIdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["pIdCandidato"].ToString());
                }

                if (Request.Params["pIdEmpleado"] != null)
                    vIdEmpleado = int.Parse(Request.Params["pIdEmpleado"].ToString());

                if (Request.Params["pIdRequisicion"] != null)
                    vIdRequisicion = int.Parse(Request.Params["pIdRequisicion"].ToString());

                SeguridadProcesos();
            }
        }

        protected void rgPruebasEmpleados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SolicitudNegocio nSolicitudes = new SolicitudNegocio();
            List<SPE_OBTIENE_EMPLEADOS_PROCESOS_EVALUACION_Result> vLstEmpleados = nSolicitudes.ObtieneEmpleadosEvaluacion(ContextoUsuario.oUsuario.ID_EMPRESA);
            rgPruebasEmpleados.DataSource = nSolicitudes.ObtieneEmpleadosEvaluacion(ContextoUsuario.oUsuario.ID_EMPRESA, vIdRol);

            if (!Page.IsPostBack)
                SeleccionarEmpleado(vLstEmpleados);
        }

        protected void rgPruebasEmpleados_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (!Page.IsPostBack)
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    if (vIdEmpleado != null && item.GetDataKeyValue("M_EMPLEADO_ID_EMPLEADO").ToString() == vIdEmpleado.ToString())
                    {
                        item.Selected = true;
                        rtsAplicacionPruebas.Tabs[1].Selected = true;
                        rpvPruebasEmpleados.Selected = true;
                    }
                }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgPruebasEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgPruebasEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgPruebasEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgPruebasEmpleados.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgPruebasEmpleados.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void grdSolicitudes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            SolicitudNegocio nSolicitudes = new SolicitudNegocio();
            List<SPE_OBTIENE_SOLICITUDES_PROCESOS_EVALUACION_Result> vLstCandidatos = nSolicitudes.ObtieneSolicitudesEvaluacion();
            grdSolicitudes.DataSource = vLstCandidatos;

            if (!Page.IsPostBack)
                SeleccionarCandidato(vLstCandidatos);

        }

        protected void grdSolicitudes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (!Page.IsPostBack)
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    if (vIdCandidato != null && item.GetDataKeyValue("ID_CANDIDATO").ToString() == vIdCandidato.ToString() && vIdEmpleado == null)
                        item.Selected = true;
                }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdSolicitudes.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnAplicarPruebas_Click(object sender, EventArgs e)
        {
            if (grdSolicitudes.SelectedItems.Count > 0)
            {
                vIdGeneraBaterias = Guid.NewGuid();
                ContextoCandidatosBateria.oCandidatosBateria = new List<E_CANDIDATOS_BATERIA>();

                ContextoCandidatosBateria.oCandidatosBateria.Add(new E_CANDIDATOS_BATERIA
                {
                    vIdGeneraBaterias = (Guid)vIdGeneraBaterias
                });

                foreach (GridDataItem item in grdSolicitudes.SelectedItems)
                {
                    if (item.GetDataKeyValue("ESTATUS").ToString() != "NO CREADA")
                        ContextoCandidatosBateria.oCandidatosBateria.Where(t => t.vIdGeneraBaterias == vIdGeneraBaterias).FirstOrDefault().vListaCandidatos.Add(int.Parse(item.GetDataKeyValue("ID_CANDIDATO").ToString()));
                    else
                    {
                        if (item.GetDataKeyValue("CL_SOLICITUD") != null)
                            UtilMensajes.MensajeResultadoDB(rnMensaje, "La solicitud " + item.GetDataKeyValue("CL_SOLICITUD").ToString() + " no cuenta con una batería creada. Crea la batería o deselecciónala para este proceso.", E_TIPO_RESPUESTA_DB.ERROR, 450, 200, pCallBackFunction: "");
                        else
                            UtilMensajes.MensajeResultadoDB(rnMensaje, "Una de las solicitudes seleccionada no cuenta con una batería creada. Crea la batería o deselecciónala para este proceso.", E_TIPO_RESPUESTA_DB.ERROR, 450, 200, pCallBackFunction: "");
                        return;
                    }
                }

                ClientScript.RegisterStartupScript(GetType(), "script", "OpenAplicarPruebas();", true);
            }
            else
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona una solicitud.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");

        }

        protected void btnCrearBateria_Click(object sender, EventArgs e)
        {
            if (grdSolicitudes.SelectedItems.Count > 0)
            {
                vIdGeneraBaterias = Guid.NewGuid();
                ContextoCandidatosBateria.oCandidatosBateria = new List<E_CANDIDATOS_BATERIA>();

                ContextoCandidatosBateria.oCandidatosBateria.Add(new E_CANDIDATOS_BATERIA
                {
                    vIdGeneraBaterias = (Guid)vIdGeneraBaterias
                });

                foreach (GridDataItem item in grdSolicitudes.SelectedItems)
                {
                    if (item.GetDataKeyValue("ESTATUS").ToString() == "NO CREADA")
                        ContextoCandidatosBateria.oCandidatosBateria.Where(t => t.vIdGeneraBaterias == vIdGeneraBaterias).FirstOrDefault().vListaCandidatos.Add(int.Parse(item.GetDataKeyValue("ID_CANDIDATO").ToString()));
                    else
                    {
                        if (item.GetDataKeyValue("CL_SOLICITUD") != null)
                            UtilMensajes.MensajeResultadoDB(rnMensaje, "La solicitud " + item.GetDataKeyValue("CL_SOLICITUD").ToString() + " ya cuenta con una batería creada. Elimina sus respuestas para volverla a aplicar.", E_TIPO_RESPUESTA_DB.ERROR, 450, 200, pCallBackFunction: "");
                        else
                            UtilMensajes.MensajeResultadoDB(rnMensaje, "Una de las solicitudes seleccionada ya cuenta con una batería creada. Elimina sus respuestas para volverla a aplicar.", E_TIPO_RESPUESTA_DB.ERROR, 450, 200, pCallBackFunction: "");
                        return;
                    }
                }

                ClientScript.RegisterStartupScript(GetType(), "script", "OpenCrearBaterias();", true);
            }
            else
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona una solicitud.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
        }

        protected void btnAplicarPruebaEmp_Click(object sender, EventArgs e)
        {
            if (rgPruebasEmpleados.SelectedItems.Count > 0)
            {
                vIdGeneraBaterias = Guid.NewGuid();
                ContextoCandidatosBateria.oCandidatosBateria = new List<E_CANDIDATOS_BATERIA>();

                ContextoCandidatosBateria.oCandidatosBateria.Add(new E_CANDIDATOS_BATERIA
                {
                    vIdGeneraBaterias = (Guid)vIdGeneraBaterias
                });

                foreach (GridDataItem item in rgPruebasEmpleados.SelectedItems)
                {
                    if (item.GetDataKeyValue("ESTATUS").ToString() != "NO CREADA")
                        ContextoCandidatosBateria.oCandidatosBateria.Where(t => t.vIdGeneraBaterias == vIdGeneraBaterias).FirstOrDefault().vListaCandidatos.Add(int.Parse(item.GetDataKeyValue("ID_CANDIDATO").ToString()));
                    else
                    {
                        if (item.GetDataKeyValue("M_EMPLEADO_CL_EMPLEADO") != null)
                            UtilMensajes.MensajeResultadoDB(rnMensaje, "El empleado " + item.GetDataKeyValue("M_EMPLEADO_CL_EMPLEADO").ToString() + " no cuenta con una batería creada. Crea la batería o deselecciónalo para este proceso.", E_TIPO_RESPUESTA_DB.ERROR, 450, 200, pCallBackFunction: "");
                        else
                            UtilMensajes.MensajeResultadoDB(rnMensaje, "Un de los empleados seleccionados no cuenta con una batería creada. Crea la batería o deselecciónalo para este proceso.", E_TIPO_RESPUESTA_DB.ERROR, 450, 200, pCallBackFunction: "");
                        return;
                    }
                }

                ClientScript.RegisterStartupScript(GetType(), "script", "OpenAplicarPruebasEmp();", true);
            }
            else
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona un empleado.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");

        }

        protected void btnCrearBateriaEmp_Click(object sender, EventArgs e)
        {
            if (rgPruebasEmpleados.SelectedItems.Count > 0)
            {
                vIdGeneraBaterias = Guid.NewGuid();
                ContextoCandidatosBateria.oCandidatosBateria = new List<E_CANDIDATOS_BATERIA>();
                

                ContextoCandidatosBateria.oCandidatosBateria.Add(new E_CANDIDATOS_BATERIA
                {
                    vIdGeneraBaterias = (Guid)vIdGeneraBaterias
                });

                foreach (GridDataItem item in rgPruebasEmpleados.SelectedItems)
                {
                    if (item.GetDataKeyValue("ESTATUS").ToString() == "NO CREADA")
                    {
                        if (item.GetDataKeyValue("ID_CANDIDATO") == null)
                        {
                            int vIdCandidato = GenerarIdCandidato(int.Parse(item.GetDataKeyValue("M_EMPLEADO_ID_EMPLEADO").ToString()));
                            if (vIdCandidato != 0)
                            ContextoCandidatosBateria.oCandidatosBateria.Where(t => t.vIdGeneraBaterias == vIdGeneraBaterias).FirstOrDefault().vListaCandidatos.Add(vIdCandidato);
                            else
                                UtilMensajes.MensajeResultadoDB(rnMensaje, "Ocurrio un error.", E_TIPO_RESPUESTA_DB.ERROR, 450, 200, pCallBackFunction: "");
                        }
                        else
                            ContextoCandidatosBateria.oCandidatosBateria.Where(t => t.vIdGeneraBaterias == vIdGeneraBaterias).FirstOrDefault().vListaCandidatos.Add(int.Parse(item.GetDataKeyValue("ID_CANDIDATO").ToString()));
                    }
                    else
                    {
                        if (item.GetDataKeyValue("M_EMPLEADO_CL_EMPLEADO") != null)
                            UtilMensajes.MensajeResultadoDB(rnMensaje, "El empleado " + item.GetDataKeyValue("M_EMPLEADO_CL_EMPLEADO").ToString() + " ya cuenta con una batería creada. Elimina sus respuestas para volverla a aplicar.", E_TIPO_RESPUESTA_DB.ERROR, 450, 200, pCallBackFunction: "");
                        else
                            UtilMensajes.MensajeResultadoDB(rnMensaje, "Uno de los empleados seleccionados ya cuenta con una batería creada. Elimina sus respuestas para volverla a aplicar.", E_TIPO_RESPUESTA_DB.ERROR, 450, 200, pCallBackFunction: "");
                        return;
                    }
                }

                ClientScript.RegisterStartupScript(GetType(), "script", "OpenCrearBateriasEmpleado();", true);
            }
            else
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona un empleado.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
        }

    }
}