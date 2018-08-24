using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class EnvioDeSolicitudes : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        public string vDsMensaje
        {
            get { return (string)ViewState["vs_vDsMensaje"]; }
            set { ViewState["vs_vDsMensaje"] = value; }
        }

        private int? vCuentaCuestionarios
        {
            get { return (int?)ViewState["vs_vCuentaCuestionarios"]; }
            set { ViewState["vs_vCuentaCuestionarios"] = value; }
        }

        private int? vCuentaContestados
        {
            get { return (int?)ViewState["vs_vCuentaContestados"]; }
            set { ViewState["vs_vCuentaContestados"] = value; }
        }


        #endregion

        #region Funciones

        private void EnviarCorreo(bool pFgEnviarTodos)
        {
            ProcesoExterno pe = new ProcesoExterno();

            int vNoCorreosEnviados = 0;
            int vNoTotalCorreos = 0;
            int vIdEvaluador;
            string vClCorreo;
            string vNbEvaluador;
            string vUrl = ContextoUsuario.nbHost + "/Logon.aspx?ClProceso=CLIMALABORAL";
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

                        vMensaje = vMensaje.Replace("[EVALUADOR]", vNbEvaluador);
                        vMensaje = vMensaje.Replace("[URL]", vUrl + "&FlProceso=" + item.GetDataKeyValue("FL_EVALUADOR").ToString());
                        vMensaje = vMensaje.Replace("[CONTRASENA]", item.GetDataKeyValue("CL_TOKEN").ToString());

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

        private void ActualizaCorreoEvaluador(string pXmlEvaluados)
        {
            ClimaLaboralNegocio nPeriodoClima = new ClimaLaboralNegocio();
            E_RESULTADO vResultado = nPeriodoClima.ActualizaCorreosEvaluadores(pXmlEvaluados, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.ERROR || vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.WARNING)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
        }

        private static Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);
            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        protected string ObtieneDepartamentos(string pXmlDepartamentos)
        {
            string vDepartamentos = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlDepartamentos);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vDepartamentos = vDepartamentos + nodo.GetAttribute("NB_DEPARTAMENTO") + ".\n";

            }


            return vDepartamentos;
        }

        protected string ObtieneAdicionales(string pXmlAdicionales)
        {
            string vAdicionales = "";
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(pXmlAdicionales);
            XmlNodeList departamentos = xml.GetElementsByTagName("ITEMS");

            XmlNodeList lista =
            ((XmlElement)departamentos[0]).GetElementsByTagName("ITEM");

            foreach (XmlElement nodo in lista)
            {

                vAdicionales = vAdicionales + nodo.GetAttribute("NB_CAMPO") + ".\n";

            }


            return vAdicionales;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
                if (Request.Params["PeriodoID"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoID"]);
                    var vPeriodoClima = nClima.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                    txtClPeriodo.InnerText = vPeriodoClima.CL_PERIODO;
                    txtDsPeriodo.InnerText = vPeriodoClima.DS_PERIODO;
                    txtEstatus.InnerText = vPeriodoClima.CL_ESTADO_PERIODO;

                    if (vPeriodoClima.CL_TIPO_CONFIGURACION == "PARAMETROS")
                        txtTipo.InnerText = "Sin asignación de evaluadores";
                    else
                        txtTipo.InnerText = "Evaluadores asignados";

                    if (vPeriodoClima.DS_NOTAS != null)
                    {
                        if (vPeriodoClima.DS_NOTAS.Contains("DS_NOTA"))
                        {
                            txtNotas.InnerHtml = Utileria.MostrarNotas(vPeriodoClima.DS_NOTAS);
                        }
                        else
                        {
                            XElement vRequerimientos = XElement.Parse(vPeriodoClima.DS_NOTAS);
                            if (vRequerimientos != null)
                            {
                                vRequerimientos.Name = vNbFirstRadEditorTagName;
                                txtNotas.InnerHtml = vRequerimientos.ToString();
                            }
                        }
                    }
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "PREDEFINIDO")
                        lbCuestionario.InnerText = "Predefinido de SIGEIN";
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "COPIA")
                       lbCuestionario.InnerText = "Copia de otro periodo";
                    if (vPeriodoClima.CL_ORIGEN_CUESTIONARIO == "VACIO")
                        lbCuestionario.InnerText = "Creado desde cero";

                    //int countFiltros = nClima.ObtenerFiltrosEvaluadores(vIdPeriodo).Count;
                    //if (countFiltros > 0)
                    //{
                    //    var vFiltros = nClima.ObtenerParametrosFiltros(vIdPeriodo).FirstOrDefault();
                    //    if (vFiltros != null)
                    //    {
                    //        // LbFiltros.Visible = true;
                    //        if (vFiltros.EDAD_INICIO != null)
                    //        {
                    //            lbedad.Visible = true;
                    //            txtEdad.Visible = true;
                    //            txtEdad.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            txtEdad.InnerText = vFiltros.EDAD_INICIO + " a " + vFiltros.EDAD_FINAL + " años";
                    //        }
                    //        if (vFiltros.ANTIGUEDAD_INICIO != null)
                    //        {
                    //            lbAntiguedad.Visible = true;
                    //            txtAntiguedad.Visible = true;
                    //            txtAntiguedad.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            txtAntiguedad.InnerText = vFiltros.ANTIGUEDAD_INICIO + " a " + vFiltros.ANTIGUEDAD_FINAL + " años";
                    //        }
                    //        if (vFiltros.CL_GENERO != null)
                    //        {
                    //            lbGenero.Visible = true;
                    //            txtGenero.Visible = true;
                    //            txtGenero.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            if (vFiltros.CL_GENERO == "M")
                    //                txtGenero.InnerText = "Masculino";
                    //            else
                    //                txtGenero.InnerText = "Femenino";
                    //        }

                    //        if (vFiltros.XML_DEPARTAMENTOS != null)
                    //        {
                    //            lbDepartamento.Visible = true;
                    //            rlDepartamento.Visible = true;
                    //            rlDepartamento.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            rlDepartamento.Text = ObtieneDepartamentos(vFiltros.XML_DEPARTAMENTOS);
                    //        }

                    //        if (vFiltros.XML_CAMPOS_ADICIONALES != null)
                    //        {
                    //            lbAdscripciones.Visible = true;
                    //            rlAdicionales.Visible = true;
                    //            rlAdicionales.Attributes.Add("class", "ctrlTableDataBorderContext");
                    //            rlAdicionales.Text = ObtieneAdicionales(vFiltros.XML_CAMPOS_ADICIONALES);
                    //        }
                    //    }
                    //}
                }
                vDsMensaje = ContextoApp.EO.MensajeCorreoEvaluador.dsMensaje;
                lMensaje.InnerHtml = vDsMensaje;

                vCuentaCuestionarios = nClima.ObtieneEvaluadoresCuestionario(pID_PERIODO: vIdPeriodo).Count;
                vCuentaContestados = 0;
            }
        }

        protected void btnEnviarTodos_Click(object sender, EventArgs e)
        {
            EnviarCorreo(true);
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

            //EvaluacionOrganizacional EO = new EvaluacionOrganizacional();
            //EO.MensajeCorreoEvaluador.dsMensaje = "<p>Estimado(a):</p><br />" +
            //                                         "<p>Tus cuestionarios a completar por medio del sistema para el Clima Laboral están listos.<br />" +
            //                                         "El objetivo de esta encuesta es detectar áreas de oportunidad que nos ayuden a mejorar el ambiente laboral de la empresa.<br />" +
            //                                         "Agradecemos el tiempo que te tomes en contestar la siguiente encuesta, la información se maneja de forma anónima, sientete con la confianza de responder de manera libre<br />" +
            //                                         "y contesta lo que refleje tu punto de vista.</p></p>"+
            //                                         "<p>Para contestarlos, por favor haz clic en la siguiente liga:</br>"+
            //                                         " [URL] </p><br/><br/>"+
            //                                         " <p>Tu contraseña de acceso es [contraseña] <br/><br/> Gracias por tu apoyo!</p>";

            //ContextoApp.EO = EO;
            //ContextoApp.SaveConfiguration("ADMIN", "EnvioDeSolicitudesClima.aspx");

        }

        protected void rgEvaluadores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            ClimaLaboralNegocio nClima = new ClimaLaboralNegocio();
            rgEvaluadores.DataSource = nClima.ObtieneEvaluadoresCuestionario(pID_PERIODO: vIdPeriodo);
        }

        protected void rgEvaluadores_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                bool vFgContestado = bool.Parse(item.GetDataKeyValue("FG_CONTESTADO").ToString());
                if (vFgContestado == true)
                {

                    item.Enabled = false;
                    item.SelectableMode = GridItemSelectableMode.None;
                    vCuentaContestados = +vCuentaContestados + 1;
                }

            }

            if (vCuentaCuestionarios == vCuentaContestados)
            {
                btnEnviarTodos.Enabled = false;
                btnEnviar.Enabled = false;
                lbMensaje.Visible = true;
            }
            else if (vCuentaContestados != 0)
            {
                btnEnviarTodos.Enabled = false;
                lbMensaje.Visible = false;
            }
            else
            {
                btnEnviarTodos.Enabled = true;
                btnEnviar.Enabled = true;
                lbMensaje.Visible = false;
            }

        }

    }
}