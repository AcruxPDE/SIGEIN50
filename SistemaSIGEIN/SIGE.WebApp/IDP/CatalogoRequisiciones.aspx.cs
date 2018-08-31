using WebApp.Comunes;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using SIGE.Negocio.Utilerias;
using System.Web.Security;
using System.Text;

namespace SIGE.WebApp.Administracion
{
    public partial class CatalogoRequisiciones : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario = string.Empty;
        private string vNbPrograma = string.Empty;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        //StringBuilder builder = new StringBuilder();
        //string Email { set; get; }

        private int vIdRequisicion
        {
            get { return (int)ViewState["vsID_REQUISICION"]; }
            set { ViewState["vsID_REQUISICION"] = value; }
        }

        //private List<SPE_OBTIENE_K_REQUISICION_Result> Requisiciones;

        private string vEstatus
        {
            get { return (string)ViewState["vEstatus"]; }
            set { ViewState["vEstatus"] = value; }
        }

        #endregion

        protected void SeguridadProcesos()
        {
            btnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.A");
            btnEditar.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.B");
            btnCancelar.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.C");
            btnEliminar.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.D");
            btnBuscarCandidato.Enabled = ContextoUsuario.oUsuario.TienePermiso("J.A.A.E");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
                SeguridadProcesos();

        }

        protected void grdRequisicion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RequisicionNegocio nRequisicion = new RequisicionNegocio();
            grdRequisicion.DataSource = nRequisicion.ObtieneRequisicion(pIdEmpresa: ContextoUsuario.oUsuario.ID_EMPRESA);

        }

        protected void btnEliminar_click(object sender, EventArgs e)
        {
            RequisicionNegocio negocio = new RequisicionNegocio();
            foreach (GridDataItem item in grdRequisicion.SelectedItems)
            {
                //vEstatus = item.GetDataKeyValue("CL_ESTATUS_REQUISICION").ToString();
                //if (vEstatus == "CREADA")
                //{
                vIdRequisicion = (int.Parse(item.GetDataKeyValue("ID_REQUISICION").ToString()));
                //  var vObtenerKrequisicion = negocio.ObtieneRequisicion(pIdRequisicion: vIdRequisicion).FirstOrDefault();
                E_RESULTADO vResultado = negocio.Elimina_K_REQUISICION(ID_REQUISICION: vIdRequisicion, programa: vNbPrograma, usuario: vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
                //}
                //else
                //{
                //    UtilMensajes.MensajeResultadoDB(rnMensaje, "Esta requisición no se puede eliminar por que está: " + vEstatus, E_TIPO_RESPUESTA_DB.WARNING, 400, 200);

                //}
            }
        }

        protected void grdRequisicion_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            RequisicionNegocio nReq = new RequisicionNegocio();
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

            int vIdRequisicion = int.Parse(dataItem.GetDataKeyValue("ID_REQUISICION").ToString());

            e.DetailTableView.DataSource = nReq.ObtenerCandidatosPorRequisicion(pIdRequisicion: vIdRequisicion);
        }

        protected void grdRequisicion_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                if (e.Item.OwnerTableView.Name == "CandidatosAsociados")
                {
                    GridDataItem dataItem = e.Item as GridDataItem;
                    if (dataItem["CL_ESTATUS_CANDIDATO_REQUISICION"].Text == "Contratado")
                        item.BackColor = System.Drawing.ColorTranslator.FromHtml("#C6DB95");
                }
            }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", grdRequisicion.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            RequisicionNegocio negocio = new RequisicionNegocio();
            foreach (GridDataItem item in grdRequisicion.SelectedItems)
            {
                vIdRequisicion = (int.Parse(item.GetDataKeyValue("ID_REQUISICION").ToString()));
                E_RESULTADO vResultado = negocio.ActualizaEstatusRequisicion(ID_REQUISICION: vIdRequisicion, programa: vNbPrograma, usuario: vClUsuario);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        //private string clToken
        //{
        //    get { return (string)ViewState["clToken"]; }
        //    set { ViewState["clToken"] = value; }
        //}

        //private Guid flRequisicion
        //{
        //    get { return (Guid)ViewState["flRequisicion"]; }
        //    set { ViewState["flRequisicion"] = value; }
        //}

        //private int vIdAutoriza
        //{
        //    get { return (int)ViewState["vIdAutoriza"]; }
        //    set { ViewState["vIdAutoriza"] = value; }
        //}

        //private string vNbPuesto
        //{
        //    get { return (string)ViewState["vNbPuesto"]; }
        //    set { ViewState["vNbPuesto"] = value; }
        //}

        //private string  vEstatusPuesto
        //{
        //    get { return (string )ViewState["vEstatusPuesto"]; }
        //    set { ViewState["vEstatusPuesto"] = value; }
        //}
        //private string vCausa
        //{
        //    get { return (string)ViewState["vCausa"]; }
        //    set { ViewState["vCausa"] = value; }
        //}


        //protected void btnNotificar_Click(object sender, EventArgs e)
        //{
        //    //vIdAutoriza = 0;
        //    //Mail mail = new Mail(ContextoApp.mailConfiguration);    
        //    //EmpleadoNegocio nEmpleado = new EmpleadoNegocio();

        //    //foreach (GridDataItem item in grdRequisicion.SelectedItems)
        //    //{

        //    //    if (item.GetDataKeyValue("ID_AUTORIZA") != null)
        //    //    {
        //    //        vIdAutoriza = (int)item.GetDataKeyValue("ID_AUTORIZA");

        //    //        vEstatus = item.GetDataKeyValue("CL_ESTADO").ToString();
        //    //        vNbPuesto = item.GetDataKeyValue("NB_PUESTO").ToString();
        //    //        flRequisicion = (Guid)item.GetDataKeyValue("FL_REQUISICION");
        //    //        clToken = item.GetDataKeyValue("CL_TOKEN").ToString();
        //    //        vID_RQUISICION = (int)item.GetDataKeyValue("ID_REQUISICION");
        //    //        vEstatusPuesto = item.GetDataKeyValue("CL_ESTATUS").ToString();
        //    //        vCausa =item.GetDataKeyValue("CL_CAUSA").ToString();

        //    //        var vUsuarioInfo = nEmpleado.Obtener_M_EMPLEADO(ID_EMPLEADO: vIdAutoriza).FirstOrDefault();
        //    //        if (vUsuarioInfo != null)
        //    //        {
        //    //            string Asunto = "Notificación de requisición.";

        //    //            if (vEstatus == "CREADA" )
        //    //            {
        //    //                if (vEstatusPuesto != "AUTORIZADO")
        //    //                {
        //    //                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Este puesto aún no ha sido autorizado", E_TIPO_RESPUESTA_DB.WARNING);

        //    //                }
        //    //                else
        //    //                {
        //    //                    try
        //    //                    {
        //    //                        string vUrl = ContextoUsuario.nbHost + "/Logon.aspx?FlProceso=" + flRequisicion.ToString() + "&ClProceso=" + "AUTORIZAREQUISICION";
        //    //                        string vMensajeCorreo = ContextoApp.IDP.NotificacionRrhh.dsAutorizadorRequisicion.dsMensaje;
        //    //                        vMensajeCorreo = vMensajeCorreo.Replace("[NB_NOTIFICAR]", vUsuarioInfo.NB_EMPLEADO_COMPLETO);
        //    //                        vMensajeCorreo = vMensajeCorreo.Replace("[NB_CREA_REQUISICION]", ContextoUsuario.oUsuario.NB_USUARIO);
        //    //                        vMensajeCorreo = vMensajeCorreo.Replace("[NB_PUESTO]", vNbPuesto);
        //    //                        vMensajeCorreo = vMensajeCorreo.Replace("[URL]", vUrl);
        //    //                        vMensajeCorreo = vMensajeCorreo.Replace("[CONTRASENA]", clToken);
        //    //                        builder.Append(vUsuarioInfo.CL_CORREO_ELECTRONICO + ";");
        //    //                        EnvioCorreo(builder.ToString(), vMensajeCorreo, Asunto);
        //    //                        RequisicionNegocio Rnegocio = new RequisicionNegocio();
        //    //                        E_RESULTADO vResultado = Rnegocio.ActualizaEstatusRequisicion(vID_RQUISICION, vClUsuario, vNbPrograma, "");
        //    //                        UtilMensajes.MensajeResultadoDB(rnMensaje, "Envío procesado", E_TIPO_RESPUESTA_DB.SUCCESSFUL);
        //    //                    }
        //    //                    catch (Exception)
        //    //                    {
        //    //                        UtilMensajes.MensajeResultadoDB(rnMensaje, "Envío no procesado", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "onCloseWindow");
        //    //                    }
        //    //                }
        //    //            }
        //    //            else
        //    //                if (vEstatus == "AUTORIZADO")
        //    //                {
        //    //                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Esta requisición ya está autorizada.", E_TIPO_RESPUESTA_DB.WARNING);
        //    //                }
        //    //                else if (vEstatus == "RECHAZADO")
        //    //                {
        //    //                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Esta requisición ya está rechazada.", E_TIPO_RESPUESTA_DB.WARNING);
        //    //                }
        //    //                else if (vEstatus == "POR AUTORIZAR")
        //    //                {
        //    //                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Esta requisición está por autorizarse.", E_TIPO_RESPUESTA_DB.WARNING);
        //    //                }
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        UtilMensajes.MensajeResultadoDB(rnMensaje, "Esta requisición no cuenta con una persona asignada para autorizar, puedes editar la requisisción para agregarla.", E_TIPO_RESPUESTA_DB.ERROR, 400, 180);
        //    //    }
        //    //}
        //}
        //public void EnvioCorreo(string Email, string Mensaje, string Asunto)
        //{
        //    Mail mail = new Mail(ContextoApp.mailConfiguration);
        //    mail.addToAddress(Email, Mensaje);
        //    RadProgressContext progress = RadProgressContext.Current;
        //    mail.Send(Asunto, Mensaje);
        //}
    }
}