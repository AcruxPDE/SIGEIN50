using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;

namespace SIGE.WebApp.EO
{
    public partial class VentanaDarBajaEmpleado : System.Web.UI.Page
    {
        #region Variables

          private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private string vNbFirstRadEditorTagName = "p";

        public int vIdEmpleado
        {
            get { return (int)ViewState["vs_vIdEmpleado"]; }
            set { ViewState["vs_vIdEmpleado"] = value; }
        }

        private bool vFgProgramarFecha
        {
            get { return (bool)ViewState["vs_vFgProgramarFecha"];}
            set { ViewState["vs_vFgProgramarFecha"] = value; }
        }

        public int? vIdCausaBaja
        {
            get { return (int?)ViewState["vs_vIdCausaBaja"]; }
            set { ViewState["vs_vIdCausaBaja"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
                    vIdEmpleado = int.Parse(Request.QueryString["ID"]);
                    var vEmpleado = nEmpleado.ObtenerEmpleado(ID_EMPLEADO: vIdEmpleado).FirstOrDefault();
                    txtClEmpleado.InnerText = vEmpleado.CL_EMPLEADO;
                    txtClPuesto.InnerText = vEmpleado.CL_PUESTO;
                    txtNombre.InnerText = vEmpleado.NB_EMPLEADO_COMPLETO;
                    txtPuesto.InnerText = vEmpleado.NB_PUESTO;
                    DateTime vFecha = DateTime.Now;
                    rdpFechaBaja.SelectedDate = vFecha;
                    rdpFechaBaja.MaxDate = vFecha;
                    lbFechaBaja.InnerText = "Fecha de baja:";
                    //btnProgramarTrue.Checked = false;
                    //btnProgramarFalse.Checked = true;
                    vFgProgramarFecha = false;
                }
            }
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }
       
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            E_BAJA_EMPLEADO vBaja = new E_BAJA_EMPLEADO();

            vBaja.CL_EMPLEADO = txtClEmpleado.InnerText;
            vBaja.NB_EMPLEADO = txtNombre.InnerText;
            vBaja.FE_BAJA_EFECTIVA = rdpFechaBaja.SelectedDate;
            vBaja.ID_EMPLEADO = vIdEmpleado;
            XElement nodoPrincipal = new XElement("XML_COMENTARIOS", EditorContentToXml("COMENTARIOS", reComentarios.Content.Replace("&lt;",""), vNbFirstRadEditorTagName));
            vBaja.DS_COMENTARIOS = nodoPrincipal.ToString();

            foreach (RadListBoxItem item in lstCausaBaja.Items)
            {
                vIdCausaBaja = int.Parse(item.Value);
                vBaja.ID_CAUSA_ROTACION = (int)vIdCausaBaja;
            }
            //if (vIdCausaBaja != null)
            //{
                if (vFgProgramarFecha)
                {
                    if (rdpFechaBaja.SelectedDate != null)
                    {
                        RotacionPersonalNegocio nBaja = new RotacionPersonalNegocio();
                        E_RESULTADO vResultado = nBaja.InsertaBajaEmpleado(pBaja: vBaja, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTIPO_TRANSACCION: E_TIPO_OPERACION_DB.I.ToString());
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                    }
                }
                else
                {
                    RotacionPersonalNegocio nBaja = new RotacionPersonalNegocio();
                    E_RESULTADO vResultado = nBaja.InsertaBajaManualEmpleado(pBaja: vBaja, pCL_USUARIO: vClUsuario, pNB_PROGRAMA: vNbPrograma, pTIPO_TRANSACCION: E_TIPO_OPERACION_DB.I.ToString());
                    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                }
            //}
            //else
            //{
            //    UtilMensajes.MensajeResultadoDB(rwmMensaje,"Seleccione la causa de la baja.",E_TIPO_RESPUESTA_DB.WARNING,pCallBackFunction:"");
            //}
        }

        //protected void btnProgramarTrue_Click(object sender, EventArgs e)
        //{
        //    lbFechaBaja.InnerText = "Fecha efectiva de la baja:";
        //    rdpFechaBaja.Enabled = true;
        //    rdpFechaBaja.MaxDate = DateTime.Today.AddYears(100);
        //    rdpFechaBaja.MinDate = DateTime.Today.AddDays(1);
        //    vFgProgramarFecha = true;
            
        //}

        //protected void btnProgramarFalse_Click(object sender, EventArgs e)
        //{
        //    lbFechaBaja.InnerText = "Programar fecha efectiva de baja:";
        //    rdpFechaBaja.MinDate = DateTime.Today.AddYears(-100);
        //    rdpFechaBaja.MaxDate = DateTime.Now;
        //    vFgProgramarFecha = false;
        //}
    }
}