using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.SecretariaTrabajoPrevisionSocial;
using SIGE.Negocio.SecretariaTrabajoPrevisionSocial;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.STPS
{
    public partial class VentanaOcupacionModulo : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int? pID
        {
            get { return (int?)ViewState["vsID"]; }
            set { ViewState["vsID"] = value; }
        }

        public E_MODULO_OCUPACION vModuloOcupacion
        {
            get { return (E_MODULO_OCUPACION)ViewState["vsvModuloOcupacion"]; }
            set { ViewState["vsvModuloOcupacion"] = value; }
        }

        OcupacionesNegocio negocio = new OcupacionesNegocio();

        public List<SPE_OBTIENE_AREA_OCUPACION_Result> listaAreas;
        public List<SPE_OBTIENE_SUBAREA_OCUPACION_Result> listaSubareas;


        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                traerAreas();

                if (Request.QueryString["ID"] != null)
                {
                    pID = int.Parse((Request.QueryString["ID"]));
                    var vObjetoModulo = negocio.Obtener_MODULO_OCUPACION(PIN_ID_MODULO: pID).FirstOrDefault();
                    txtClave.Text = vObjetoModulo.CL_MODULO;
                    txtModulo.Text = vObjetoModulo.NB_MODULO;
                    cmbArea.SelectedValue = vObjetoModulo.CL_AREA.ToString();
                    List<SPE_OBTIENE_SUBAREA_OCUPACION_Result> vSubarea = negocio.Obtener_SUBAREA_OCUPACION(PIN_CL_AREA: vObjetoModulo.CL_AREA.ToString());
                    cmbSubarea.DataValueField = "CL_SUBAREA";
                    cmbSubarea.DataTextField = "NB_SUBAREA";
                    cmbSubarea.DataSource = vSubarea;
                    cmbSubarea.DataBind();
                    cmbSubarea.SelectedValue = vObjetoModulo.CL_SUBAREA.ToString();
                }
            }
        }

        public void traerAreas()
        {
            listaAreas = negocio.Obtener_AREA_OCUPACION();
            cmbArea.DataSource = listaAreas;
            cmbArea.DataTextField = "NB_AREA";
            cmbArea.DataValueField = "CL_AREA";
            cmbArea.DataBind();
        }

        public void traerSubAreas(string clArea)
        {
            cmbSubarea.Text = string.Empty;
            listaSubareas = negocio.Obtener_SUBAREA_OCUPACION(PIN_CL_AREA: clArea);
            cmbSubarea.DataSource = listaSubareas;
            cmbSubarea.DataTextField = "NB_SUBAREA";
            cmbSubarea.DataValueField = "CL_SUBAREA";
            cmbSubarea.DataBind();
        }

        protected void cmbArea_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbSubarea.DataSource = new string[] { };
            cmbSubarea.DataBind();
            traerSubAreas(e.Value);
        }


        protected void cmbSubarea_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void btnGuardarModulo_Click(object sender, EventArgs e)
        {

            if (cmbArea.SelectedValue != "" && cmbSubarea.SelectedValue != "")
            {
                string vClArea = cmbArea.SelectedValue;
                string vClSubarea = cmbSubarea.SelectedValue;

                E_MODULO_OCUPACION vModuloAgregar = new E_MODULO_OCUPACION();
                string vAccion = (pID != null ? "A" : "I");

                if (vAccion == "I")
                {
                    vModuloAgregar.CL_MODULO = txtClave.Text;
                    vModuloAgregar.NB_MODULO = txtModulo.Text;
                    vModuloAgregar.FG_ACTIVO = true;
                    vModuloAgregar.CL_AREA = vClArea;
                    vModuloAgregar.CL_SUBAREA = vClSubarea;

                    E_RESULTADO resultado = negocio.InsertaActualiza_C_MODULO(vAccion, vModuloAgregar, vClUsuario, vNbPrograma);
                    UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
                }
                else
                {
                    vModuloAgregar.ID_MODULO = pID;
                    vModuloAgregar.CL_MODULO = txtClave.Text;
                    vModuloAgregar.NB_MODULO = txtModulo.Text;
                    vModuloAgregar.FG_ACTIVO = true;
                    vModuloAgregar.CL_AREA = vClArea;
                    vModuloAgregar.CL_SUBAREA = vClSubarea;

                    E_RESULTADO resultado = negocio.InsertaActualiza_C_MODULO(vAccion, vModuloAgregar, vClUsuario, vNbPrograma);
                    UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
                }
            }
            else {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Debes llenar todos los campos", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
            }
        }
    }
}