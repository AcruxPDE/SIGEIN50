using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class AgregarEvaluadores : System.Web.UI.Page
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

        public bool vFgMostrarEmpleado
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

        }
    }
}