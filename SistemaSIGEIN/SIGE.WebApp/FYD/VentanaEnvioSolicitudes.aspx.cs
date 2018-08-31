using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
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

namespace SIGE.WebApp.FYD
{
    public partial class VentanaEnvioSolicitudes : System.Web.UI.Page
    {
        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";
        private int? vIdRol;

        public int vIdPeriodo {
            get { return (int)ViewState["vs_ves_id_periodo"]; }
            set { ViewState["vs_ves_id_periodo"] = value; }
        }

        public string vDsMensaje {
            get { return (string)ViewState["vs_ves_ds_mensaje"]; }
            set { ViewState["vs_ves_ds_mensaje"] = value; }
        }

        private bool vFgRevisaCuestionarios {
            get { return (bool)ViewState["vs_ves_fg_revisa_cuestionario"]; }
            set { ViewState["vs_ves_fg_revisa_cuestionario"] = value; }
        }

        #endregion

        #region Funciones

        private void CargarPeriodo()
        {
            PeriodoNegocio neg = new PeriodoNegocio();

            var oPeriodo = neg.ObtienePeriodoEvaluacion(vIdPeriodo);

            if (oPeriodo != null)
            {
                //txtNoPeriodo.InnerText = oPeriodo.CL_PERIODO;
                //txtNbPeriodo.InnerText = oPeriodo.DS_PERIODO;

                txtClPeriodo.InnerText = oPeriodo.NB_PERIODO;
                txtDsPeriodo.InnerText = oPeriodo.DS_PERIODO;

                txtEstatus.InnerText = oPeriodo.CL_ESTADO_PERIODO;
                string vTiposEvaluacion = "";

                if (oPeriodo.FG_AUTOEVALUACION)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Autoevaluación" : String.Join(", ", vTiposEvaluacion, "Autoevaluacion");
                }

                if (oPeriodo.FG_SUPERVISOR)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Superior" : String.Join(", ", vTiposEvaluacion, "Superior");
                }

                if (oPeriodo.FG_SUBORDINADOS)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Subordinado" : String.Join(", ", vTiposEvaluacion, "Subordinado");
                }

                if (oPeriodo.FG_INTERRELACIONADOS)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Interrelacionado" : String.Join(", ", vTiposEvaluacion, "Interrelacionado");
                }

                if (oPeriodo.FG_OTROS_EVALUADORES)
                {
                    vTiposEvaluacion = string.IsNullOrEmpty(vTiposEvaluacion) ? "Otros" : String.Join(", ", vTiposEvaluacion, "Otros");
                }

                txtTipoEvaluacion.InnerText = vTiposEvaluacion;

                if (oPeriodo.DS_NOTAS != null)
                {
                    if (oPeriodo.DS_NOTAS.Contains("DS_NOTA"))
                    {
                        txtNotas.InnerHtml = Utileria.MostrarNotas(oPeriodo.DS_NOTAS);
                    }
                    else
                    {
                        XElement vNotas = XElement.Parse(oPeriodo.DS_NOTAS);
                        if (vNotas != null)
                        {
                            vNotas.Name = vNbFirstRadEditorTagName;
                            txtNotas.InnerHtml = vNotas.ToString();
                        }
                    }
                }
            }
        }

        private void ActualizaCorreoEvaluador(string pXmlEvaluados)
        {            
            PeriodoNegocio oPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = oPeriodo.ActualizarCorreoEvaluador(pXmlEvaluados, vClUsuario, vNbPrograma);

            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR || vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.WARNING )
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }            
        }
        
        private void EnviarCorreo(bool pFgEnviarTodos)
        {
            ProcesoExterno pe = new ProcesoExterno();
            
            int vNoCorreosEnviados = 0;
            int vNoTotalCorreos = 0;
            int vIdEvaluador;
            string vClCorreo;
            string vNbEvaluador;
            string myUrl = ResolveUrl("~/Logon.aspx?ClProceso=CUESTIONARIOS");
            string vUrl = ContextoUsuario.nbHost + myUrl;
            GridItemCollection oListaEvaluadores = new GridItemCollection();
            XElement vXmlEvaluados = new XElement("EVALUADORES");

            if (pFgEnviarTodos)
                oListaEvaluadores = rgEvaluadores.Items;
            else
               oListaEvaluadores = rgEvaluadores.SelectedItems;
                        
            vNoTotalCorreos = oListaEvaluadores.Count;

            foreach (GridDataItem item in oListaEvaluadores)
            {
                string vMensaje = vDsMensaje;
                vClCorreo = (item.FindControl("txtCorreo") as RadTextBox).Text;
                vNbEvaluador = item["NB_EVALUADOR"].Text;
                vIdEvaluador = int.Parse(item.GetDataKeyValue("ID_EVALUADOR").ToString());

                if (Utileria.ComprobarFormatoEmail(vClCorreo))
                {
                    if (item.GetDataKeyValue("FL_EVALUADOR") != null)
                    {
                        vMensaje = vMensaje.Replace("[nombre]", vNbEvaluador);
                        vMensaje = vMensaje.Replace("[URL]", vUrl + "&FlProceso=" + item.GetDataKeyValue("FL_EVALUADOR").ToString());
                        vMensaje = vMensaje.Replace("[contraseña]", item.GetDataKeyValue("CL_TOKEN").ToString());

                        //bool vEstatusCorreo = pe.EnvioCorreo("gabriel.vazquez@acrux.mx", item.NB_EVALUADOR, "Cuestionarios para evaluación", vMensaje);

                        bool vEstatusCorreo = pe.EnvioCorreo(vClCorreo, vNbEvaluador, "Cuestionarios para evaluación", vMensaje);

                        if (vEstatusCorreo)
                        {
                            vXmlEvaluados.Add(new XElement("EVALUADOR", new XAttribute("ID_EVALUADOR", vIdEvaluador), new XAttribute("CL_CORREO_ELECTRONICO", vClCorreo)));
                            vNoCorreosEnviados++;

                            (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.White;
                            (item.FindControl("txtCorreo") as RadTextBox).HoveredStyle.BackColor = System.Drawing.Color.White;
                        }
                        else
                        {
                            (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                        }
                    }
                    else
                    {
                        (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                    }
                }
                else
                {
                    (item.FindControl("txtCorreo") as RadTextBox).EnabledStyle.BackColor = System.Drawing.Color.Gold;
                }
            }

            if (vXmlEvaluados.Elements("EVALUADOR").Count() > 0)
            {
                ActualizaCorreoEvaluador(vXmlEvaluados.ToString());
            }


            if (vNoTotalCorreos == vNoCorreosEnviados)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Los correos han sido enviados con éxito.", E_TIPO_RESPUESTA_DB.SUCCESSFUL);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Se enviaron " + vNoCorreosEnviados.ToString() + " correos de " + vNoTotalCorreos.ToString() + " en total.", E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "");
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

            if (!Page.IsPostBack)
            {
                if (Request.Params["IdPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());

                    if (Request.Params["FgRevisaSeleccion"] != null)
                    {
                        vFgRevisaCuestionarios = bool.Parse(Request.Params["FgRevisaSeleccion"].ToString());
                    }
                    else
                    {
                        vFgRevisaCuestionarios = false;
                    }


                    CargarPeriodo();
                }

                 vDsMensaje = ContextoApp.FYD.MensajeCorreoSolicitudes.dsMensaje;
                 lMensaje.InnerHtml = vDsMensaje.Replace("<a href=\"[URL]\">","").Replace("</a>","");
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            if (rgEvaluadores.SelectedItems.Count == 0)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No ha seleccionado ningun evaluador.", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }
            else
            {
                EnviarCorreo(false);
            }

            

            //FormacionYDesarrollo fyd = new FormacionYDesarrollo();

            //fyd.MensajeCorreoParticipantes.dsMensaje = "<p>Estimado(a) <label style=\"color: blue;\">[Participante]</label></p><br /> " +
            //    "<p>Te invitamos a participar en el siguiente evento:<br />" +
            //    "Evento de capacitación: <label style=\"color: blue;\">[NB_EVENTO]</label><br />" +
            //    "Lugar: <label style=\"color: blue;\">[DS_LUGAR]</label><br />" +
            //    "Fecha de inicio: <label style=\"color: blue;\">[FE_INICIO]</label><br />" +
            //    "Fecha de término: <label style=\"color: blue;\">[FE_TERMINO]</label><br /></p>" +
            //    "<p>[TABLA_CALENDARIO]</p>";

            //fyd.MensajeCorreoEvaluadores.dsMensaje = "<p>Estimado(a) [Evaluador]:</p><br />" +
            //                                         "<p>Tus evaluaciones de resultados a completar por medio del sistema para el <br />" +
            //                                         "Evento [NB_EVENTO] - '[Evaluador]' están listas. Para <br />" +
            //                                         "contestarlos, por favor accede a la siguiente dirección:</p><br /><p>[URL]</p><br />" +
            //                                         "<p>Tu contraseña de acceso es [contraseña] y la fecha de evaluación es a <br />partir de [FE_EVALUACION]. ¡Gracias por tu apoyo!";

            //fyd.MensajeCorreoSolicitudes.dsMensaje = "Estimado(a) [nombre]:<p>Tus cuestionarios a completar por medio del sistema para el periodo de evaluación de competencias están listos. Recuerda que la información que nos proporciones será confidencial. Para contestarlos, por favor haz clic en la siguiente liga:</p>" +
            //                                           "<p>[URL]</p>" +
            //                                           "<p>Tu contraseña de acceso es <b> [contraseña] </b> ¡Gracias por tu apoyo!</p>";


            //ContextoApp.FYD = fyd;
            //ContextoApp.SaveConfiguration("ADMIN", "VentanaEnvioSolicitudes.aspx");
        }

        protected void rgEvaluadores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            CuestionarioNegocio oCuestionario = new CuestionarioNegocio();
            rgEvaluadores.DataSource = oCuestionario.ObtieneEvaluadores(pIdPeriodo: vIdPeriodo, pID_EMPRESA: vIdEmpresa, pID_ROL: vIdRol);
        }

        protected void btnEnviarTodos_Click(object sender, EventArgs e)
        {
            EnviarCorreo(true);
        }

        protected void rgEvaluadores_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (vFgRevisaCuestionarios)
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem vFila = e.Item as GridDataItem;

                    int vContestados;
                    int vTotales;

                    if (vFila.GetDataKeyValue("NO_CUESTIONARIOS") != null)
                    {
                        vContestados = int.Parse(vFila.GetDataKeyValue("NO_CUESTIONARIOS_CONTESTADOS").ToString());
                        vTotales = int.Parse(vFila.GetDataKeyValue("NO_CUESTIONARIOS").ToString());

                        if (vContestados != vTotales)
                        {
                            e.Item.Selected = true;
                        }
                    }
                }
            }

            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgEvaluadores.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }

        }
    }
}