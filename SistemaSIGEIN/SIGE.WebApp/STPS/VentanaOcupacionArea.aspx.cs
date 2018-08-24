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
    public partial class VentanaOcupacionArea : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        private int? pID
        {
            get { return (int?)ViewState["vsID"]; }
            set { ViewState["vsID"] = value; }
        }

        public E_AREA_OCUPACION vAreaOcupacion
        {
            get { return (E_AREA_OCUPACION)ViewState["vsvAreaOcupacion"]; }
            set { ViewState["vsvAreaOcupacion"] = value; }
        }
        OcupacionesNegocio negocio = new OcupacionesNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {   
                    pID = int.Parse((Request.QueryString["ID"]));

                    var vObjetoArea = negocio.Obtener_AREA_OCUPACION(PIN_ID_AREA: pID).FirstOrDefault();
                    txtClave.Text = vObjetoArea.CL_AREA;
                    txtArea.Text = vObjetoArea.NB_AREA;
                }
            }
        }

        protected void btnGuardarArea_Click(object sender, EventArgs e)
        {
            
            E_AREA_OCUPACION vAreaAgregar = new E_AREA_OCUPACION();
            string vAccion = (pID != null ? "A" : "I");

            if (vAccion == "I")
            {
                vAreaAgregar.CL_AREA = txtClave.Text;
                vAreaAgregar.NB_AREA = txtArea.Text;
                vAreaAgregar.FG_ACTIVO = true;

                E_RESULTADO resultado = negocio.InsertaActualiza_C_AREA(vAccion, vAreaAgregar, vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
            }
            else
            {
                vAreaAgregar.ID_AREA = pID;
                vAreaAgregar.CL_AREA = txtClave.Text;
                vAreaAgregar.NB_AREA = txtArea.Text;
                vAreaAgregar.FG_ACTIVO = true;

                E_RESULTADO resultado = negocio.InsertaActualiza_C_AREA(vAccion, vAreaAgregar, vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
            }
        }
    }
}