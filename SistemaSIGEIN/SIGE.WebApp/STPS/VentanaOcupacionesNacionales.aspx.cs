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
    public partial class VentanaOcupacionesNacionales : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int? pID
        {
            get { return (int?)ViewState["vsID"]; }
            set { ViewState["vsID"] = value; }
        }

        public E_OCUPACION vOcupacion
        {
            get { return (E_OCUPACION)ViewState["vsvOcupacion"]; }
            set { ViewState["vsvOcupacion"] = value; }
        }

        OcupacionesNegocio negocio = new OcupacionesNegocio();

        public List<SPE_OBTIENE_AREA_OCUPACION_Result> listaAreas;
        public List<SPE_OBTIENE_SUBAREA_OCUPACION_Result> listaSubareas;
        public List<SPE_OBTIENE_MODULO_OCUPACION_Result> listaModulos;

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
                    var vObjetoOcupacion = negocio.Obtener_OCUPACIONES(PIN_ID_OCUPACION: pID).FirstOrDefault();
                    txtClave.Text = vObjetoOcupacion.CL_OCUPACION;
                    txtOcupacion.Text = vObjetoOcupacion.NB_OCUPACION;
                    cmbArea.SelectedValue = vObjetoOcupacion.CL_AREA.ToString();
                    List<SPE_OBTIENE_SUBAREA_OCUPACION_Result> vSubarea = negocio.Obtener_SUBAREA_OCUPACION(PIN_CL_AREA: vObjetoOcupacion.CL_AREA.ToString());
                    cmbSubarea.DataValueField = "CL_SUBAREA";
                    cmbSubarea.DataTextField = "NB_SUBAREA";
                    cmbSubarea.DataSource = vSubarea;
                    cmbSubarea.DataBind();
                    cmbSubarea.SelectedValue = vObjetoOcupacion.CL_SUBAREA.ToString();
                    List<SPE_OBTIENE_MODULO_OCUPACION_Result> vModulo = negocio.Obtener_MODULO_OCUPACION(PIN_CL_AREA:vObjetoOcupacion.CL_AREA.ToString(),PIN_CL_SUBAREA: vObjetoOcupacion.CL_SUBAREA.ToString());
                    cmbModulo.DataValueField = "CL_MODULO";
                    cmbModulo.DataTextField = "NB_MODULO";
                    cmbModulo.DataSource = vModulo;
                    cmbModulo.DataBind();
                    cmbModulo.SelectedValue = vObjetoOcupacion.CL_MODULO.ToString();
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

        public void traerModulos(string clSubarea)
        {
            cmbModulo.Text = string.Empty;
            listaModulos = negocio.Obtener_MODULO_OCUPACION(PIN_CL_SUBAREA:clSubarea);
            cmbModulo.DataSource = listaModulos;
            cmbModulo.DataTextField = "NB_MODULO";
            cmbModulo.DataValueField = "CL_MODULO";
            cmbModulo.DataBind();
        }

        protected void cmbArea_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbSubarea.DataSource = new string[] { };
            cmbSubarea.DataBind();
            traerSubAreas(e.Value);
        }

        protected void cmbSubarea_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            cmbModulo.DataSource = new string[] { };
            cmbModulo.DataBind();
            traerModulos(e.Value);
        }

        protected void cmbModulo_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        protected void btnGuardarOcupacion_Click(object sender, EventArgs e)
        {
            if (cmbArea.SelectedValue != "" && cmbSubarea.SelectedValue != "" && cmbModulo.SelectedValue != "")
            {

                string vClArea = cmbArea.SelectedValue;
                string vClSubarea = cmbSubarea.SelectedValue;
                string vClModulo = cmbModulo.SelectedValue;

                E_OCUPACION vOcupacionAgregar = new E_OCUPACION();
                string vAccion = (pID != null ? "A" : "I");

                if (vAccion == "I")
                {
                    vOcupacionAgregar.CL_OCUPACION = txtClave.Text;
                    vOcupacionAgregar.NB_OCUPACION = txtOcupacion.Text;
                    vOcupacionAgregar.FG_ACTIVO = true;
                    vOcupacionAgregar.CL_AREA = vClArea;
                    vOcupacionAgregar.CL_SUBAREA = vClSubarea;
                    vOcupacionAgregar.CL_MODULO = vClModulo;

                    E_RESULTADO resultado = negocio.InsertaActualiza_C_OCUPACION(vAccion, vOcupacionAgregar, vClUsuario, vNbPrograma);
                    UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
                }
                else
                {
                    vOcupacionAgregar.ID_OCUPACION = pID;
                    vOcupacionAgregar.CL_OCUPACION = txtClave.Text;
                    vOcupacionAgregar.NB_OCUPACION = txtOcupacion.Text;
                    vOcupacionAgregar.FG_ACTIVO = true;
                    vOcupacionAgregar.CL_AREA = vClArea;
                    vOcupacionAgregar.CL_SUBAREA = vClSubarea;
                    vOcupacionAgregar.CL_MODULO = vClModulo;

                    E_RESULTADO resultado = negocio.InsertaActualiza_C_OCUPACION(vAccion, vOcupacionAgregar, vClUsuario, vNbPrograma);
                    UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
                }
            }
            else {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Debes llenar todos los campos",E_TIPO_RESPUESTA_DB.WARNING, 400, 150,null);
            }
        }
    }
}