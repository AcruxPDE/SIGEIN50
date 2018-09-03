using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class Configuracion : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }
        protected void grdPlantillas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();
            grdPlantillas.DataSource = nPlantilla.ObtienePlantillasPDE();
        }

        protected void btnEliminarPlantilla_Click(object sender, EventArgs e)
        {
            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();

            foreach (GridDataItem item in grdPlantillas.SelectedItems)
            {
                E_RESULTADO vResultado = nPlantilla.EliminaPlantillaFormulario(int.Parse(item.GetDataKeyValue("ID_PLANTILLA_SOLICITUD").ToString()), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void btnEstablecerGeneral_Click(object sender, EventArgs e)
        {
            PlantillaFormularioNegocio nPlantilla = new PlantillaFormularioNegocio();

            foreach (GridDataItem item in grdPlantillas.SelectedItems)
            {
                E_RESULTADO vResultado = nPlantilla.EstablecerPlantillaPorDefecto(int.Parse(item.GetDataKeyValue("ID_PLANTILLA_SOLICITUD").ToString()), item.GetDataKeyValue("CL_FORMULARIO").ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }
    }
}