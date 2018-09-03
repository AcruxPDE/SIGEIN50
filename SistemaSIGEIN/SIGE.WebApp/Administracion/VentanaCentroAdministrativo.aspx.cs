using SIGE.Entidades;
using SIGE.Entidades.Administracion;
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

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaCentroAdministrativo : System.Web.UI.Page
    {
        private string usuario;
        private string programa;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private Guid vIdCentroAdmvo
        {
            get { return (Guid)ViewState["vsIdCentroAdmvo"]; }
            set { ViewState["vsIdCentroAdmvo"] = value; }
        }
        
        private string vTipoTransaccion = "";
        //String consulta =

        RadListBoxItem vNoSeleccionadoEstado = new RadListBoxItem("No seleccionado", String.Empty);
        RadListBoxItem vNoSeleccionadoMunicipio = new RadListBoxItem("No seleccionado", String.Empty);
        RadListBoxItem vNoSeleccionadoColonia = new RadListBoxItem("No seleccionado", String.Empty);
                    
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
                    vIdCentroAdmvo = new Guid(Request.QueryString["ID"]);
                    CentroAdministrativoNegocio nCentroAdministrativo = new CentroAdministrativoNegocio();

                    var vCentroAdmvo = nCentroAdministrativo.Obtener_C_CENTRO_ADMVO(ID_CENTRO_ADMVO: vIdCentroAdmvo).FirstOrDefault();
                    txtClave.Text = vCentroAdmvo.CL_CENTRO_ADMVO;
                    txtNombre.Text = vCentroAdmvo.NB_CENTRO_ADMVO;
                    txtCalle.Text = vCentroAdmvo.NB_CALLE;
                    txtNoExt.Text = vCentroAdmvo.NB_NO_EXTERIOR;
                    txtNoInt.Text = vCentroAdmvo.NB_NO_INTERIOR;
                    txtCP.Text = vCentroAdmvo.CL_CODIGO_POSTAL;
                    String vnEstado = vCentroAdmvo.NB_ESTADO;
                    String vnMunicipio = vCentroAdmvo.NB_MUNICIPIO;
                    String vnColonia = vCentroAdmvo.NB_COLONIA;

                    lstEstado.Items.Add((vnEstado != null) ? new RadListBoxItem(vCentroAdmvo.NB_ESTADO, vCentroAdmvo.CL_ESTADO) : vNoSeleccionadoEstado);
                    lstEstado.Items.FirstOrDefault().Selected = true;

                    lstMunicipio.Items.Add((vnMunicipio != null) ? new RadListBoxItem(vCentroAdmvo.NB_MUNICIPIO, vCentroAdmvo.CL_MUNICIPIO) : vNoSeleccionadoMunicipio);
                    lstMunicipio.Items.FirstOrDefault().Selected = true;

                    lstColonia.Items.Add((vnColonia != null) ? new RadListBoxItem(vnColonia) : vNoSeleccionadoColonia);
                    lstColonia.Items.FirstOrDefault().Selected = true;
            }
                else
                {
                    vIdCentroAdmvo = new Guid();
                    lstEstado.Items.Add(vNoSeleccionadoEstado);
                    lstMunicipio.Items.Add(vNoSeleccionadoMunicipio);
                    lstColonia.Items.Add(vNoSeleccionadoColonia);                  
                }
               
            }

        }
       
        protected void btnGuardarCentroAdmvo_Click(object sender, EventArgs e)
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
            String vClMunicipio = "";
            String vNbMunicipio = "";
            String vNbColonia = "";

            E_CENTROS_ADMVOS vCentrosAdmvo = new E_CENTROS_ADMVOS();
            
            vCentrosAdmvo.ID_CENTRO_ADMVO = vIdCentroAdmvo;
            vCentrosAdmvo.CL_CENTRO_ADMVO = txtClave.Text;
            vCentrosAdmvo.NB_CENTRO_ADMVO = txtNombre.Text;
            vCentrosAdmvo.NB_CALLE = txtCalle.Text;
            vCentrosAdmvo.NB_NO_EXTERIOR = txtNoExt.Text;
            vCentrosAdmvo.NB_NO_INTERIOR = txtNoInt.Text;
            vCentrosAdmvo.CL_CODIGO_POSTAL = txtCP.Text;

            foreach (RadListBoxItem item in lstEstado.Items)
            {
                vClEstado = item.Value;
                vNbEstado = item.Text;
                vCentrosAdmvo.CL_ESTADO = vClEstado;
                vCentrosAdmvo.NB_ESTADO = vNbEstado;
            }

            foreach (RadListBoxItem item in lstMunicipio.Items)
            {
                vNbMunicipio = item.Text;
                vClMunicipio = item.Value;
                vCentrosAdmvo.NB_MUNICIPIO = vNbMunicipio;
                vCentrosAdmvo.CL_MUNICIPIO = vClMunicipio;
            }

            foreach (RadListBoxItem item in lstColonia.Items)
            {
                vNbColonia = item.Text;
                vCentrosAdmvo.NB_COLONIA = vNbColonia;
            }
            CentroAdministrativoNegocio nCentroAdministrativo = new CentroAdministrativoNegocio();
            E_RESULTADO vResultado = nCentroAdministrativo.InsertaActualizaCCentroAdmvo(usuario: usuario, programa: programa, pClTipoOperacion: vTipoTransaccion, vCCentroAdmvo: vCentrosAdmvo);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        }

        protected void btnCancelarCentroAdmvo_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "MyScript", "closeWindow(0);", true);
        }
    }
}