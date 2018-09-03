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
    public partial class VentanaOcupacionSubarea : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int? pID
        {
            get { return (int?)ViewState["vsID"]; }
            set { ViewState["vsID"] = value; }
        }

        public E_SUBAREA_OCUPACION vSubOcupacion
        {
            get { return (E_SUBAREA_OCUPACION)ViewState["vsvSubOcupacion"]; }
            set { ViewState["vsvSubOcupacion"] = value; }
        }

        OcupacionesNegocio negocio = new OcupacionesNegocio();
        public List<SPE_OBTIENE_AREA_OCUPACION_Result> listaAreas;

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
                    var vObjetoArea = negocio.Obtener_SUBAREA_OCUPACION(PIN_ID_SUBAREA: pID).FirstOrDefault();
                    txtClave.Text = vObjetoArea.CL_SUBAREA;
                    txtSubarea.Text = vObjetoArea.NB_SUBAREA;
                    cmbArea.SelectedValue = vObjetoArea.CL_AREA.ToString();
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

        protected void cmbArea_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }
        
        protected void btnGuardarSubarea_Click(object sender, EventArgs e)
        {
            if (cmbArea.SelectedValue != "")
            {
                string vClArea = cmbArea.SelectedValue;

                E_SUBAREA_OCUPACION vSubAgregar = new E_SUBAREA_OCUPACION();
                string vAccion = (pID != null ? "A" : "I");

                if (vAccion == "I")
                {
                    vSubAgregar.CL_SUBAREA = txtClave.Text;
                    vSubAgregar.NB_SUBAREA = txtSubarea.Text;
                    vSubAgregar.FG_ACTIVO = true;
                    vSubAgregar.CL_AREA = vClArea;

                    E_RESULTADO resultado = negocio.InsertaActualiza_C_SUBAREA(vAccion, vSubAgregar, vClUsuario, vNbPrograma);
                    UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
                }
                else
                {
                    vSubAgregar.ID_SUBAREA = pID;
                    vSubAgregar.CL_SUBAREA = txtClave.Text;
                    vSubAgregar.NB_SUBAREA = txtSubarea.Text;
                    vSubAgregar.FG_ACTIVO = true;
                    vSubAgregar.CL_AREA = vClArea;

                    E_RESULTADO resultado = negocio.InsertaActualiza_C_SUBAREA(vAccion, vSubAgregar, vClUsuario, vNbPrograma);
                    UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
                }
            }
            else {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Debes seleccionar un área", E_TIPO_RESPUESTA_DB.WARNING, 400, 150,null);
            }
        }
    }
}