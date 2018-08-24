using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaPlaza : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdEmpresa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int? vIdPlaza
        {
            get { return (int?)ViewState["vs_vIdPlaza"]; }
            set { ViewState["vs_vIdPlaza"] = value; }
        }

        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }

        #endregion

        #region Metodos

         protected void CargarDatos(int? vIdPlaza)
        {
            PlazaNegocio nPlaza = new PlazaNegocio();
            SPE_OBTIENE_PLAZAS_Result vPlaza = nPlaza.ObtienePlazas(vIdPlaza).FirstOrDefault() ?? new SPE_OBTIENE_PLAZAS_Result();

            txtClPlaza.Text = vPlaza.CL_PLAZA;
            txtNbPlaza.Text = vPlaza.NB_PLAZA;
            chkActivo.Checked = vPlaza.FG_ACTIVO || vClOperacion.Equals(E_TIPO_OPERACION_DB.I);

            if (vPlaza.ID_EMPLEADO != null)
            {
                lstEmpleado.Items.Clear();
                lstEmpleado.Items.Add(new RadListBoxItem(vPlaza.NB_EMPLEADO_COMPLETO, vPlaza.ID_EMPLEADO.ToString()));
            }

            if (vPlaza.ID_PUESTO != null)
            {
                lstPuesto.Items.Clear();
                lstPuesto.Items.Add(new RadListBoxItem(vPlaza.NB_PUESTO, vPlaza.ID_PUESTO.ToString()));
                lstPuesto.Enabled = false;
            }

            if (vPlaza.ID_PLAZA_SUPERIOR != null)
            {
                if (vPlaza.ID_PLAZA_SUPERIOR.Value != 0)
                {
                    lstPlazaJefe.Items.Clear();
                    lstPlazaJefe.Items.Add(new RadListBoxItem(vPlaza.NB_PLAZA_JEFE, vPlaza.ID_PLAZA_SUPERIOR.ToString()));   
                }
            }

            EmpresaNegocio nEmpresa = new EmpresaNegocio();
            List<SPE_OBTIENE_C_EMPRESA_Result> vEmpresas = nEmpresa.Obtener_C_EMPRESA(ID_EMPRESA: vIdEmpresa);
            cmbEmpresa.DataValueField = "ID_EMPRESA";
            cmbEmpresa.DataTextField = "NB_EMPRESA";
            cmbEmpresa.DataSource = vEmpresas;
            cmbEmpresa.DataBind();
            cmbEmpresa.SelectedValue = vPlaza.ID_EMPRESA.ToString();

        }


        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdEmpresa = ContextoUsuario.oUsuario.ID_EMPRESA;

            if (!Page.IsPostBack)
            {
                int vIdPlazaQS = -1;
                vClOperacion = E_TIPO_OPERACION_DB.I;
                if (int.TryParse(Request.QueryString["PlazaId"], out vIdPlazaQS))
                {
                    vIdPlaza = vIdPlazaQS;
                    vClOperacion = E_TIPO_OPERACION_DB.A;
                }

                CargarDatos(vIdPlaza ?? 0);
            }


        }
       
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            SPE_OBTIENE_PLAZAS_Result vPlaza = new SPE_OBTIENE_PLAZAS_Result()
            {
                ID_PLAZA = vIdPlaza ?? 0,
                CL_PLAZA = txtClPlaza.Text,
                NB_PLAZA = txtNbPlaza.Text,
                FG_ACTIVO = chkActivo.Checked,
                ID_EMPRESA = int.Parse(cmbEmpresa.SelectedValue ?? "0")
            };

            bool vFgGuardarPlaza = true;

            int vIdEmpleado = 0;
            foreach (RadListBoxItem item in lstEmpleado.Items)
                if (int.TryParse(item.Value, out vIdEmpleado))
                    vPlaza.ID_EMPLEADO = vIdEmpleado;

            int vIdPuesto = 0;
            foreach (RadListBoxItem item in lstPuesto.Items)
                if (int.TryParse(item.Value, out vIdPuesto))
                    vPlaza.ID_PUESTO = vIdPuesto;

            if (vPlaza.ID_PUESTO == null)
            {
                UtilMensajes.MensajeResultadoDB(rwmAlertas, "Debes selecionar un puesto.", E_TIPO_RESPUESTA_DB.WARNING);
                vFgGuardarPlaza = false;
            }

            int vIdPlazaJefe = 0;
            foreach (RadListBoxItem item in lstPlazaJefe.Items)
                if (int.TryParse(item.Value, out vIdPlazaJefe))
                    vPlaza.ID_PLAZA_SUPERIOR = vIdPlazaJefe;

            if (vFgGuardarPlaza)
            {
                PlazaNegocio nPlaza = new PlazaNegocio();

                E_RESULTADO vResultado = nPlaza.InsertaActualizaPlaza(vClOperacion, vPlaza, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
            }
        }
    }
}