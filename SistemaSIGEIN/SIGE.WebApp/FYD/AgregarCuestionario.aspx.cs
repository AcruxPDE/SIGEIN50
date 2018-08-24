using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class AgregarCuestionario : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int? vIdPeriodo
        {
            get { return (int?)ViewState["vs_vIdPeriodo"]; }
            set { ViewState["vs_vIdPeriodo"] = value; }
        }

        public int? vIdEvaluado
        {
            get { return (int?)ViewState["vs_vIdEvaluado"]; }
            set { ViewState["vs_vIdEvaluado"] = value; }
        }

        public int? vIdEvaluadoEvaluador
        {
            get { return (int?)ViewState["vs_vIdEvaluadoEvaluador"]; }
            set { ViewState["vs_vIdEvaluadoEvaluador"] = value; }
        }

        public string vClParent
        {
            get { return (string)ViewState["vs_vClParent"]; }
            set { ViewState["vs_vClParent"] = value; }
        }

        public string vClAccionCerrar
        {
            get { return (string)ViewState["vs_vClAccionCerrar"]; }
            set { ViewState["vs_vClAccionCerrar"] = value; }
        }

        public string vClRol
        {
            get { return (string)ViewState["vs_vClRol"]; }
            set { ViewState["vs_vClRol"] = value; }
        }

        public bool vFgCrearCuestinario
        {
            get { return (bool)ViewState["vs_fg_mostrar_empleado"]; }
            set { ViewState["vs_fg_mostrar_empleado"] = value; }
        }

        #endregion

        #region Funciones

        protected void CargarRolesEvaluador(int pIdPeriodo)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            rcbRolEvaluador.DataSource = nPeriodo.ObtieneRolesEvaluador(pIdPeriodo);
            rcbRolEvaluador.DataValueField = "CL_TIPO_EVALUADOR";
            rcbRolEvaluador.DataTextField = "NB_TIPO_EVALUADOR";
            rcbRolEvaluador.DataBind();

            if (vClRol != null)
            {
                rcbRolEvaluador.SelectedValue = vClRol;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                int vIdPeriodoQS = 0;
                int vIdEvaluadoQS = 0;
                int vIdEvaluadoEvaluadorQS = 0;
                vClRol = null;

                if (Request.Params["FgCrearCuestionarios"] != null)
                {
                    vFgCrearCuestinario = bool.Parse(Request.Params["FgCrearCuestionarios"].ToString());
                }
                else
                {
                    vFgCrearCuestinario = true;
                }

                if (Request.Params["ClRol"] != null)
                {
                    vClRol = Request.Params["Clrol"].ToString();
                }

                if (int.TryParse(Request.QueryString["PeriodoId"], out vIdPeriodoQS))
                    vIdPeriodo = vIdPeriodoQS;

                if (int.TryParse(Request.QueryString["EvaluadoId"], out vIdEvaluadoQS))
                    vIdEvaluado = vIdEvaluadoQS;

                if (int.TryParse(Request.QueryString["EvaluadoEvaluadorId"], out vIdEvaluadoEvaluadorQS))
                    vIdEvaluadoEvaluador = vIdEvaluadoEvaluadorQS;

                vClParent = Request.QueryString["ParentCl"];
                if (String.IsNullOrWhiteSpace(vClParent))
                    vClParent = "CONFIGURACION";

                vClAccionCerrar = Request.QueryString["AccionCerrarCl"];
                if (String.IsNullOrWhiteSpace(vClAccionCerrar))
                    vClAccionCerrar = "REBIND";

                if (vIdPeriodo != null)
                {
                    CargarRolesEvaluador(vIdPeriodo ?? 0);

                    PeriodoNegocio nPeriodo = new PeriodoNegocio();
                    SPE_OBTIENE_FYD_CUESTIONARIO_EVALUADO_EVALUADOR_Result vEvaluadoEvaluador = nPeriodo.ObtieneCuestionarioEvaluadoEvaludor(vIdPeriodo ?? 0, vIdEvaluado, vIdEvaluadoEvaluador);

                    if (vEvaluadoEvaluador != null)
                    {
                        foreach (RadComboBoxItem vItem in ((RadComboBoxItemCollection)rcbRolEvaluador.Items).Where(w => w.Value == vEvaluadoEvaluador.CL_ROL_EVALUADOR))
                            vItem.Selected = true;

                        rlbEvaluado.Items.Clear();
                        rlbEvaluado.Items.Add(new RadListBoxItem(vEvaluadoEvaluador.NB_EVALUADO, vEvaluadoEvaluador.ID_EVALUADO.ToString()));
                        btnBuscarEvaluado.Enabled = false;
                        btnEliminarEvaluado.Enabled = false;

                        if (vEvaluadoEvaluador.ID_EVALUADOR != null)
                        {
                            rlbEvaluador.Items.Clear();
                            rlbEvaluador.Items.Add(new RadListBoxItem(vEvaluadoEvaluador.NB_EVALUADOR, vEvaluadoEvaluador.ID_EVALUADOR.ToString()));
                            btnBuscarEvaluador.Enabled = false;
                            btnEliminarEvaluador.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void btnGuardarConfiguracion_Click(object sender, EventArgs e)
        {
            string vClRolEvaluador = rcbRolEvaluador.SelectedValue;
            string vDsMensaje = String.Empty;

            if (((RadListBoxItemCollection)rlbEvaluado.Items).Any(a => a.Value.Equals("0") || String.IsNullOrEmpty(a.Value)))
                vDsMensaje += "Selecciona a un evaluado.<br />";

            if (((RadListBoxItemCollection)rlbEvaluador.Items).Any(a => a.Value.Equals("0") || String.IsNullOrEmpty(a.Value)))
                vDsMensaje += "Selecciona a un evaluador.<br />";

            if (String.IsNullOrWhiteSpace(vDsMensaje))
            {
                XElement vXmlEvaluados = new XElement("EVALUADOS", rlbEvaluado.Items.Select(s => new XElement("EVALUADO", new XAttribute("ID_EVALUADO", s.Value))));

                XElement vXmlEvaluadores = new XElement("EVALUADORES");
                if (vIdEvaluadoEvaluador != null)
                    vXmlEvaluadores.Add(new XElement("EVALUADOR", new XAttribute("ID_EVALUADO_EVALUADOR", vIdEvaluadoEvaluador)));
                else
                {
                    string vClAtributoEvaluador = (rcbRolEvaluador.SelectedValue == E_CL_TIPO_EVALUADOR.OTRO.ToString()) ? "ID_EVALUADOR" : "ID_EMPLEADO";
                    vXmlEvaluadores.Add(rlbEvaluador.Items.Select(s => new XElement("EVALUADOR", new XAttribute(vClAtributoEvaluador, s.Value))));
                }

                PeriodoNegocio nPeriodo = new PeriodoNegocio();
                E_RESULTADO vResultado = nPeriodo.InsertaActualizaCuestionariosAdicionales(vIdPeriodo ?? 0, vXmlEvaluados, vXmlEvaluadores, vClRolEvaluador, vFgCrearCuestinario, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                string vCallBackFunction = vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL) ? "generateDataForParent" : null;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vDsMensaje, E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: null);
            }
        }
    }
}