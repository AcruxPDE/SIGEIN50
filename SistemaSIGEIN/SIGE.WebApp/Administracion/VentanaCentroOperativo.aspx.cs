using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.Externas;
using SIGE.Entidades.Administracion;
using SIGE.WebApp.Comunes;
using Telerik.Web.UI;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCentroOperativo : System.Web.UI.Page
    {
        private string usuario;
        private string programa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private Guid vIdCentroOptvo
        {
            get { return (Guid)ViewState["vsIdCentroOptvo"]; }
            set { ViewState["vsIdCentroOptvo"] = value; }
        }

        String vTipoTransaccion = "";
        RadListBoxItem vNoSeleccionadoEstado = new RadListBoxItem("No seleccionado", String.Empty);
        RadListBoxItem vNoSeleccionadoMunicipio = new RadListBoxItem("No seleccionado", String.Empty);
        RadListBoxItem vNoSeleccionadoColonia= new RadListBoxItem("No seleccionado", String.Empty);

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = ContextoUsuario.oUsuario.CL_USUARIO;
            programa = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                vNoSeleccionadoEstado.Selected = true;
                vNoSeleccionadoMunicipio.Selected = true;
                vNoSeleccionadoColonia.Selected = true;
     
                if (Request.QueryString["ID"] != null)
                {
                    vIdCentroOptvo = new Guid(Request.QueryString["ID"]);
                    CentroOperativoNegocio nCentroOperativo = new CentroOperativoNegocio();

                    var vCentroOperativo = nCentroOperativo.Obtener_C_CENTRO_OPTVO(ID_CENTRO_OPTVO: vIdCentroOptvo).FirstOrDefault();

                    txtClave.Text = vCentroOperativo.CL_CENTRO_OPTVO;
                    txtNombre.Text = vCentroOperativo.NB_CENTRO_OPTVO;
                    txtCalle.Text = vCentroOperativo.NB_CALLE;
                    txtNoExt.Text = vCentroOperativo.NB_NO_EXTERIOR;
                    txtNoInt.Text = vCentroOperativo.NB_NO_INTERIOR;
                    txtCP.Text = vCentroOperativo.CL_CODIGO_POSTAL;
                    String vnEstado = vCentroOperativo.NB_ESTADO;
                    String vnMunicipio = vCentroOperativo.NB_MUNICIPIO;
                    String vnColonia = vCentroOperativo.NB_COLONIA;

                    lstEstado.Items.Add((vnEstado != null) ? new RadListBoxItem(vCentroOperativo.NB_ESTADO, vCentroOperativo.CL_ESTADO) : vNoSeleccionadoEstado);
                    lstEstado.Items.FirstOrDefault().Selected = true;
                
                    lstMunicipio.Items.Add((vnMunicipio != null) ? new RadListBoxItem(vCentroOperativo.NB_MUNICIPIO, vCentroOperativo.CL_MUNICIPIO) : vNoSeleccionadoMunicipio);
                    lstMunicipio.Items.FirstOrDefault().Selected = true;

                    lstColonia.Items.Add((vnColonia != null) ? new RadListBoxItem(vnColonia) : vNoSeleccionadoColonia);
                    lstColonia.Items.FirstOrDefault().Selected = true;

                }
                else
                {
                    vIdCentroOptvo = new Guid();
                    lstEstado.Items.Add(vNoSeleccionadoEstado);
                    lstMunicipio.Items.Add(vNoSeleccionadoMunicipio);
                    lstColonia.Items.Add(vNoSeleccionadoColonia);
                }
            }
        }

        protected void btnGuardarCentroOptvo_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                vTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
            }

            else
            {
                vTipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();
            }

            String vClEstado = "";
            String vNbEstado = "";
            String vNbMunicipio = "";
            String vClMunicipio = "";
            String vNbColonia = "";

            E_CENTROS_OPTVOS vCentroOptvo = new E_CENTROS_OPTVOS();
            vCentroOptvo.ID_CENTRO_OPTVO = vIdCentroOptvo;
            vCentroOptvo.CL_CENTRO_OPTVO = txtClave.Text;
            vCentroOptvo.NB_CENTRO_OPTVO = txtNombre.Text;
            vCentroOptvo.NB_CALLE = txtCalle.Text;
            vCentroOptvo.NB_NO_EXTERIOR = txtNoExt.Text;
            vCentroOptvo.NB_NO_INTERIOR = txtNoInt.Text;
            vCentroOptvo.CL_CODIGO_POSTAL = txtCP.Text;

            foreach (RadListBoxItem item in lstEstado.Items)
            {
                vClEstado = item.Value;
                vNbEstado = item.Text;
                vCentroOptvo.CL_ESTADO = vClEstado;
                vCentroOptvo.NB_ESTADO = vNbEstado;
            }
           
            foreach (RadListBoxItem item in lstMunicipio.Items)
            {
                vClMunicipio = item.Value;
                vNbMunicipio = item.Text;
                vCentroOptvo.NB_MUNICIPIO = vNbMunicipio;
                vCentroOptvo.CL_MUNICIPIO = vClMunicipio;
            }
            
            foreach (RadListBoxItem item in lstColonia.Items)
            {
                vNbColonia = item.Text;
                vCentroOptvo.NB_COLONIA = vNbColonia;
            }

            CentroOperativoNegocio nCentroOptvo = new CentroOperativoNegocio();
            E_RESULTADO vResultado = nCentroOptvo.InsertaActualizaCCentroAdmvo(usuario: usuario, programa: programa, pClTipoOperacion: vTipoTransaccion, vCCentroOptvo: vCentroOptvo);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        }
        protected void btnCancelarCentroOptvo_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "closeWindow(0);", true);
        }

    }
}