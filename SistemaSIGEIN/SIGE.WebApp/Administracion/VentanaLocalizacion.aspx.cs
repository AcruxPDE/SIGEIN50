using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System.Net;

namespace SIGE.WebApp.Administracion
{
    public partial class VentanaLocalizacion : System.Web.UI.Page
    {
        private List<SPE_OBTENER_TIPO_ASENTAMIENTO_Result> listaTipoAsentamiento;
        private List<SPE_OBTIENE_C_COLONIA_Result> listaColonias;
       
        private string vClUsuario = string.Empty;
        private string vNbPrograma = string.Empty;
        
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        
        private E_TIPO_OPERACION_DB vClOperacion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vClOperacion"]; }
            set { ViewState["vs_vClOperacion"] = value; }
        }
      
        private string vIdColonia= string.Empty;
        private string vClEstado = string.Empty;
        private string vClMunicipio = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString != null)
            {
                vIdColonia = Request.QueryString["pID"];
                vClEstado = Request.QueryString["pClEstado"];
                vClMunicipio = Request.QueryString["pClMunicipio"];   
            }

            vClOperacion = E_TIPO_OPERACION_DB.I;
             
            if (!Page.IsPostBack)
            {
                //CONSULTA LOS TIPOS DE ASENTAMIENTO
                 traerTipoAsentamiento();

                 //TRAER LA COLONIA CORRESPONDIENTE
                 if (vIdColonia != null)
                 {
                     traerColonia(vIdColonia);
                 }
            }

            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        public void traerColonia(string pidColonia)
        {
            ColoniaNegocio nColonia = new ColoniaNegocio();
            listaColonias = nColonia.ObtieneColonias(pIdColonia: int.Parse(vIdColonia));

            if (listaColonias.Count > 0)
            {
                txtColonia.Text = listaColonias.FirstOrDefault().NB_COLONIA;
                txtCodigoPostal.Text = listaColonias.FirstOrDefault().CL_CODIGO_POSTAL;
                cmbTipoAsentamiento.SelectedValue = listaColonias.FirstOrDefault().CL_TIPO_ASENTAMIENTO;                
            }
            
        }

        public void traerTipoAsentamiento()
        {
            ColoniaNegocio nColonia = new ColoniaNegocio();
            listaTipoAsentamiento = nColonia.Obtener_TIPO_ASENTAMIENTO();
            cmbTipoAsentamiento.DataSource = listaTipoAsentamiento;
            cmbTipoAsentamiento.DataBind();
        }

        protected void radBtnGuardar_Click(object sender, EventArgs e)
        {
            string vTipoAsentamiento = string.Empty;
            string vNombreColonia = string.Empty;
            int vCodigoPostal = 0;

            vTipoAsentamiento = cmbTipoAsentamiento.SelectedValue;
            vNombreColonia = txtColonia.Text;
            bool vValidacion = int.TryParse(txtCodigoPostal.Text, out vCodigoPostal);
            if (vValidacion == false)
            {
                //Error no es un numero
            }
            vNombreColonia = txtColonia.Text;

            ColoniaNegocio nColonia = new ColoniaNegocio();
            SPE_OBTIENE_C_COLONIA_Result vColonia = new SPE_OBTIENE_C_COLONIA_Result();

            //SABER SI ESTA ACTUALIZANDO O INSERTANDO
            vColonia.CL_TIPO_ASENTAMIENTO = vTipoAsentamiento;
            vColonia.NB_COLONIA = vNombreColonia;
            vColonia.CL_CODIGO_POSTAL = vCodigoPostal.ToString();

            if (vIdColonia != null)
            {
                vClOperacion = E_TIPO_OPERACION_DB.A;
                vColonia = nColonia.ObtieneColonias(pIdColonia: int.Parse(vIdColonia)).FirstOrDefault();
                vColonia.CL_TIPO_ASENTAMIENTO = vTipoAsentamiento;
                vColonia.NB_COLONIA =vNombreColonia;
                vColonia.CL_CODIGO_POSTAL = vCodigoPostal.ToString();
            }
            else
            {
                vColonia.CL_ESTADO = vClEstado;
                vColonia.CL_MUNICIPIO = vClMunicipio;
            
            }
            
            //REALIZA LA TRANSACCION CORRESPONDIENTE
            E_RESULTADO vResultado = UtilRespuesta.EnvioRespuesta(nColonia.InsertaActualiza_C_COLONIA(vClOperacion.ToString(), vColonia, vClUsuario, vNbPrograma));
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR);
           
        }

    }
}