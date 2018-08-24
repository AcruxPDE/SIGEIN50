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
    public partial class VentanaCatalogoAreasTematicas : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;


        private int? pID
        {
            get { return (int?)ViewState["vsID"]; }
            set { ViewState["vsID"] = value; }
        }

        private string ptipo
        {
            get { return (string)ViewState["vstipo"]; }
            set { ViewState["vstipo"] = value; }
        }


        public E_AREA_TEMATICA vAreaTematica
        {
            get { return (E_AREA_TEMATICA)ViewState["vsvAreaTematica"]; }
            set { ViewState["vsvAreaTematica"] = value; }
        }
        AreaTematicaNegocio negocio = new AreaTematicaNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    pID = int.Parse((Request.QueryString["ID"]));

                    var vObjetoAreaTematica = negocio.Obtener_C_AREA_TEMATICA(ID_AREA_TEMATICA: pID).FirstOrDefault();
                    txtClave.Text = vObjetoAreaTematica.CL_AREA_TEMATICA;
                    txtAreaTematica.Text = vObjetoAreaTematica.NB_AREA_TEMATICA;
                }
            }
        }

        protected void btnGuardarCatalogo_Click(object sender, EventArgs e)
        {
            E_AREA_TEMATICA vAreaAgregar = new E_AREA_TEMATICA();
            string vAccion = (pID != null ? "A" : "I");

            if (vAccion == "I")
            {
                vAreaAgregar.CL_AREA_TEMATICA = txtClave.Text;
                vAreaAgregar.NB_AREA_TEMATICA = txtAreaTematica.Text;
                vAreaAgregar.FG_ACTIVO = true;

                E_RESULTADO resultado = negocio.InsertaActualiza_C_AREA_TEMATICA(vAccion, vAreaAgregar, vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
            }
            else {
                vAreaAgregar.ID_AREA_TEMATICA = pID;
                vAreaAgregar.CL_AREA_TEMATICA = txtClave.Text;
                vAreaAgregar.NB_AREA_TEMATICA = txtAreaTematica.Text;
                vAreaAgregar.FG_ACTIVO = true;

                E_RESULTADO resultado = negocio.InsertaActualiza_C_AREA_TEMATICA(vAccion, vAreaAgregar, vClUsuario, vNbPrograma);
                UtilMensajes.MensajeResultadoDB(rnMensaje, resultado.MENSAJE[0].DS_MENSAJE.ToString(), resultado.CL_TIPO_ERROR, 400, 150);
            }
        }
    }
}