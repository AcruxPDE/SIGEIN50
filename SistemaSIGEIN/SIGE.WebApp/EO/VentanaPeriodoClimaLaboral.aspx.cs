using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.Externas;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;

namespace SIGE.WebApp.EO
{
    public partial class VentanaNuevoPeriodoClimaLaboral : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";
        private string vTipoTransaccion = "";

        private int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        private int? vIdPeriodoCopia
        {
            get { return (int?)ViewState["vs_vIdPeriodoCopia"]; }
            set { ViewState["vs_vIdPeriodoCopia"] = value; }
        }

        RadListBoxItem vNoSeleccionado = new RadListBoxItem("No seleccionado", String.Empty);

        #endregion

        #region Funciones

        private XElement EditorContentToXml(string pNbNodoRaiz, string pDsContenido, string pNbTag)
        {
            return XElement.Parse(EncapsularRadEditorContent(XElement.Parse(String.Format("<{1}>{0}</{1}>", HttpUtility.HtmlDecode(HttpUtility.UrlDecode(pDsContenido)), pNbNodoRaiz)), pNbNodoRaiz));
        }

        private string EncapsularRadEditorContent(XElement nodo, string nbNodo)
        {
            if (nodo.Elements().Count() == 1)
                return EncapsularRadEditorContent((XElement)nodo.FirstNode, nbNodo);
            else
            {
                nodo.Name = nbNodo;
                return nodo.ToString();
            }
        }

        private void SeguridadProcesos()
        {
            btnAceptar.Enabled = ContextoUsuario.oUsuario.TienePermiso("L.A.A.B");
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                txtDsEstado.Text = "Abierto";

                if (Request.QueryString["PeriodoId"] != null)
                {
                    vIdPeriodoCopia = Request.QueryString["Tipo"] == "COPIA" ? int.Parse((Request.QueryString["PeriodoId"])) : 0;
                    vIdPeriodo = int.Parse((Request.QueryString["PeriodoId"]));
                    ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
                    var vPeriodo = nPeriodo.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                    txtDsDescripcion.Text = vPeriodo.DS_PERIODO;
                    txtDsPeriodo.Text = vPeriodo.CL_PERIODO;
                    txtDsEstado.Text = vPeriodo.CL_ESTADO_PERIODO;

                    if (vPeriodo.CL_TIPO_CONFIGURACION == "PARAMETROS")
                        rbParametros.Checked = true;
                    else
                        rbSeleccion.Checked = true;

                    SeguridadProcesos();

                    if (vPeriodo.CL_ESTADO_PERIODO == "CERRADO")
                        btnAceptar.Enabled = false;

                    string vNbPeriodoOrigen = vPeriodo.NB_PERIODO_ORIGEN;

                    if (vPeriodo.DS_NOTAS != null)
                    {
                        if (vPeriodo.DS_NOTAS.Contains("DS_NOTA"))
                        {
                            txtDsNotas.Content = Utileria.MostrarNotas(vPeriodo.DS_NOTAS);
                        }
                        else
                        {
                            XElement vRequerimientos = XElement.Parse(vPeriodo.DS_NOTAS);
                            if (vRequerimientos != null)
                            {
                                vRequerimientos.Name = vNbFirstRadEditorTagName;
                                txtDsNotas.Content = vRequerimientos.ToString();
                            }
                        }
                    }

                    if (vIdPeriodoCopia != null && vIdPeriodoCopia != 0)
                    {
                        rbCuestionarioPredefinido.Enabled = false;
                        rbCopiarCuestionario.Enabled = false;
                        rbCuestionarioEnBlanco.Enabled = false;
                        rbCopiarCuestionario.Checked = true;
                        copiarPeriodo.Style.Add("display", "block");
                        btnBuscarPeriodo.Enabled = false;
                        btnEliminarPeriodo.Enabled = false;
                        lstPeriodo.Items.Add(new RadListBoxItem(vPeriodo.CL_PERIODO, vIdPeriodoCopia.ToString()));
                        lstPeriodo.Items.FirstOrDefault().Selected = true;
                    }
                    else
                    {
                        lstPeriodo.Items.Add((vNbPeriodoOrigen != null) ? new RadListBoxItem(vPeriodo.NB_PERIODO_ORIGEN, vPeriodo.ID_PERIODO_ORIGEN.ToString()) : vNoSeleccionado);
                        lstPeriodo.Items.FirstOrDefault().Selected = true;


                        if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "PREDEFINIDO")
                            rbCuestionarioPredefinido.Checked = true;
                        if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "COPIA")
                            rbCopiarCuestionario.Checked = true;
                        if (vPeriodo.CL_ORIGEN_CUESTIONARIO == "VACIO")
                            rbCuestionarioEnBlanco.Checked = true;

                        rbCuestionarioPredefinido.Enabled = false;
                        rbCopiarCuestionario.Enabled = false;
                        rbCuestionarioEnBlanco.Enabled = false;
                        rbParametros.Enabled = false;
                        rbSeleccion.Enabled = false;
                    }
                }
                else
                {
                    lstPeriodo.Items.Add(vNoSeleccionado);
                    rbSeleccion.Checked = true;
                }

            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["TIPO"] == "COPIA" && vIdPeriodoCopia != 0)
            {
                E_PERIODO_CLIMA_COPIA vPeriodo = new E_PERIODO_CLIMA_COPIA();

                vPeriodo.ID_PERIODO = (int)vIdPeriodoCopia;
                vPeriodo.CL_ESTADO_PERIODO = txtDsEstado.Text;
                vPeriodo.NB_PERIODO = txtDsPeriodo.Text;
                vPeriodo.CL_PERIODO = txtDsPeriodo.Text;
                vPeriodo.DS_PERIODO = txtDsDescripcion.Text;
                vPeriodo.DS_NOTAS = Utileria.GuardarNotas(txtDsNotas.Content, "XML_NOTAS").ToString();
                vPeriodo.CL_TIPO_CONFIGURACION = rbParametros.Checked? "PARAMETROS":"EVALUADORES";

                ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();

                E_RESULTADO vResultado = nPeriodo.InsertaPeriodoClimaCopia(pPeriodo: vPeriodo, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
            }
            else
            {
                E_PERIODO_CLIMA vPeriodo = new E_PERIODO_CLIMA();
 
                if (Request.QueryString["PeriodoId"] != null && Request.QueryString["clAccion"] == null)
                {
                    ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
                    var vDsPeriodo = nPeriodo.ObtienePeriodosClima(pIdPerido: vIdPeriodo).FirstOrDefault();
                    if (vDsPeriodo != null)
                    vPeriodo.DS_MENSAJE_ENVIO = vDsPeriodo.DS_MENSAJE_CORREO;

                    vTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
                }
                else
                {
                    vTipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();
                }

              
                if (vIdPeriodo == null)
                    vIdPeriodo = 0;

                vPeriodo.ID_PERIODO = (int)vIdPeriodo;
                vPeriodo.CL_ESTADO_PERIODO = txtDsEstado.Text;
                vPeriodo.NB_PERIODO = txtDsPeriodo.Text;
                vPeriodo.CL_PERIODO = txtDsPeriodo.Text;
                vPeriodo.DS_PERIODO = txtDsDescripcion.Text;
                vPeriodo.DS_NOTAS = Utileria.GuardarNotas(txtDsNotas.Content, "XML_NOTAS").ToString();
                vPeriodo.CL_TIPO_CONFIGURACION = rbParametros.Checked ? "PARAMETROS" : "EVALUADORES";


                if (rbCuestionarioPredefinido.Checked)
                    vPeriodo.CL_ORIGEN_CUESTIONARIO = "PREDEFINIDO";
                if (rbCopiarCuestionario.Checked)
                    vPeriodo.CL_ORIGEN_CUESTIONARIO = "COPIA";
                if (rbCuestionarioEnBlanco.Checked)
                    vPeriodo.CL_ORIGEN_CUESTIONARIO = "VACIO";

                if (rbCopiarCuestionario.Checked == true)
                {
                    if (lstPeriodo.SelectedItem != null)
                    {
                        foreach (RadListBoxItem item in lstPeriodo.Items)
                        {
                            int vIdPeriodoOrigen = int.Parse(item.Value);
                            string vNbPeriodoOrigen = item.Text;
                            vPeriodo.ID_PERIODO_ORIGEN = vIdPeriodoOrigen;
                        }
                    }
                }
                if (vPeriodo.CL_ORIGEN_CUESTIONARIO != "COPIA")
                {
                    ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
                    E_RESULTADO vResultado = nPeriodo.InsertaActualizaPeriodoClima(pPeriodo: vPeriodo, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTIPO_TRANSACCION: vTipoTransaccion.ToString());
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    if (vTipoTransaccion.ToString() == E_TIPO_OPERACION_DB.I.ToString())
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
                    else
                       UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindowEdit");
                }
                else
                {
                    if (vPeriodo.ID_PERIODO_ORIGEN != null)
                    {
                        ClimaLaboralNegocio nPeriodo = new ClimaLaboralNegocio();
                        E_RESULTADO vResultado = nPeriodo.InsertaActualizaPeriodoClima(pPeriodo: vPeriodo, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTIPO_TRANSACCION: vTipoTransaccion.ToString());
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, "Seleccione el período para copiar cuestionario", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    }
                }
            }
        }
        
    }
}