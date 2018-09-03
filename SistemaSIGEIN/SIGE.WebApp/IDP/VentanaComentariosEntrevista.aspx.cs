using SIGE.Entidades.Externas;
using SIGE.Negocio.IntegracionDePersonal;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.IDP
{
    public partial class VentanaComentariosEntrevista : System.Web.UI.Page
    {
        #region Variables
        public int vIdCandidato
        {
            get { return (int)ViewState["vs_ps_id_candidato"]; }
            set { ViewState["vs_ps_id_candidato"] = value; }
        }


        public int vIdProcesoSeleccion
        {
            get { return (int)ViewState["vs_vIdProcesoSeleccion"]; }
            set { ViewState["vs_vIdProcesoSeleccion"] = value; }
        }

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                if (Request.Params["IdCandidato"] != null)
                {
                    vIdCandidato = int.Parse(Request.Params["IdCandidato"].ToString());
                }

                if (Request.Params["IdProcesoSeleccion"] != null)
                {
                    vIdProcesoSeleccion = int.Parse(Request.Params["IdProcesoSeleccion"].ToString());
                }

                ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();

                var vProcesoSeleccion = nProcesoSeleccion.ObtieneProcesoSeleccion(pIdProcesoSeleccion: vIdProcesoSeleccion).FirstOrDefault();

                if (vProcesoSeleccion != null)
                {
                    txtCandidato.InnerText = vProcesoSeleccion.NB_CANDIDATO_COMPLETO;
                    txtClaveRequisicion.InnerText = vProcesoSeleccion.NO_REQUISICION;
                    
                }
            }
        }

        protected void rgComentariosEntrevistas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ProcesoSeleccionNegocio nProcesoSeleccion = new ProcesoSeleccionNegocio();
            var vProcesoSeleccion = nProcesoSeleccion.ObtieneProcesoSeleccion(pIdCandidato: vIdCandidato).FirstOrDefault();
            rgComentariosEntrevistas.DataSource = nProcesoSeleccion.ObtieneComentariosEntrevistaProcesoSeleccion(pIdProceso: vIdProcesoSeleccion);

        }
    }
}