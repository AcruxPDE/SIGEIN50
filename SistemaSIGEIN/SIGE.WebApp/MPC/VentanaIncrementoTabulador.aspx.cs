using SIGE.Entidades.Externas;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIGE.WebApp.MPC
{
    public partial class VentanaIncrementoTabulador : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdTabulador
        {
            get { return (int)ViewState["vsIdTabulador"]; }
            set { ViewState["vsIdTabulador"] = value; }
        }

        public string vTipoIncremento
        {
            get { return (string)ViewState["vs_vTipoIncremento"]; }
            set { ViewState["vs_vTipoIncremento"] = value; }
        }

        public decimal vMontoIncremento
        {
            get { return (decimal)ViewState["vs_vMontoIncremento"]; }
            set { ViewState["vs_vMontoIncremento"] = value; }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["IdTabulador"] != null)
                {
                    vIdTabulador = int.Parse((Request.QueryString["IdTabulador"]));
                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                    txtClTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_CREACION.ToString("dd/MM/yyyy");

                    rbPorcentaje.Checked = true;
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (rbPorcentaje.Checked == true && txtMontoIncremento.Text != "" || rbMonto.Checked == true && txtMontoIncremento.Text != "")
            {
                if (rbPorcentaje.Checked == true)
                {
                    vTipoIncremento = "P";
                }
                else if (rbMonto.Checked == true)
                {
                    vTipoIncremento = "M";
                }
                vMontoIncremento = decimal.Parse(txtMontoIncremento.Text);

                TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                E_RESULTADO vResultado = nTabulador.InsertarActualizarTabuladorSueldo(vIdTabulador, vTipoIncremento, vMontoIncremento, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseUpdate");
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
                }
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Ingrese una cantidad valida.", E_TIPO_RESPUESTA_DB.ERROR, pCallBackFunction: "");
            }
        }

    }
}