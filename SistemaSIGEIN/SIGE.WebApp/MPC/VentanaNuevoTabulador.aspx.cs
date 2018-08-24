using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.Externas;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.WebApp.Comunes;

namespace SIGE.WebApp.MPC
{
    public partial class VentanaNuevoMenu : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int? vIdTabulador
        {
            get { return (int?)ViewState["vs_vIdTabulador"]; }
            set { ViewState["vs_vIdTabulador"] = value; }
        }

        private E_TIPO_OPERACION_DB vTipoTransaccion
        {
            get { return (E_TIPO_OPERACION_DB)ViewState["vs_vTipoTransaccion"]; }
            set { ViewState["vs_vTipoTransaccion"] = value; }
        }

        private void SeguridadProcesos()
        {
            btnGuardarNuevo.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.B");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                rdpCreacion.SelectedDate = DateTime.Now;

                DateTime vFechaVigencia = DateTime.Now.AddYears(1);
                rdpVigencia.SelectedDate = vFechaVigencia;

                int vIdTabulaQS = -1;
                vTipoTransaccion = E_TIPO_OPERACION_DB.I;
                if (int.TryParse(Request.QueryString["ID"], out vIdTabulaQS))
                {
                    vIdTabulador = vIdTabulaQS;
                    vTipoTransaccion = E_TIPO_OPERACION_DB.A;
                }

                CargarDatos(vIdTabulador);
                SeguridadProcesos();

            }
        }

        DateTime GetLastDayOf(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        protected void CargarDatos(int? pIdTabulador)
        {
            if (Request.QueryString["ID"] != null)
            {
                TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                var vTavulador = nTabulador.ObtenerTabuladores(pIdTabulador).FirstOrDefault();
                txtVersionTabulador.Text = vTavulador.CL_TABULADOR;
                txtNombreTabulador.Text = vTavulador.NB_TABULADOR;
                txtDescripcion.Text = vTavulador.DS_TABULADOR;
                rdpCreacion.SelectedDate = vTavulador.FE_CREACION;
                rdpVigencia.SelectedDate = vTavulador.FE_VIGENCIA;
                if (vTavulador.CL_TIPO_PUESTO == "DIRECTO")
                {
                    btDirectos.Checked = true;
                    btAmbos.Checked = false;
                }
                if (vTavulador.CL_TIPO_PUESTO == "INDIRECTO")
                {
                    btIndirectos.Checked = true;
                    btAmbos.Checked = false;
                }
                if (vTavulador.CL_TIPO_PUESTO == "AMBOS")
                {
                    btAmbos.Checked = true;
                }

            }
        }

        protected void btnGuardarNuevo_Click(object sender, EventArgs e)
        {
            E_TABULADOR vTabulador = new E_TABULADOR();
            vTabulador.CL_TABULADOR = txtVersionTabulador.Text;
            vTabulador.NB_TABULADOR = txtNombreTabulador.Text;
            vTabulador.DS_TABULADOR = txtDescripcion.Text;
            vTabulador.FE_CREACION = rdpCreacion.SelectedDate ?? DateTime.Now;
            vTabulador.FE_VIGENCIA = rdpVigencia.SelectedDate ?? DateTime.Now;
            if (btDirectos.Checked)
            {
                vTabulador.CL_TIPO_PUESTO = "DIRECTO";
            }
            if (btIndirectos.Checked)
            {
                vTabulador.CL_TIPO_PUESTO = "INDIRECTO";
            }
            if (btAmbos.Checked)
            {
                vTabulador.CL_TIPO_PUESTO = "AMBOS";
            }
            if (vTipoTransaccion.Equals(E_TIPO_OPERACION_DB.A) && vIdTabulador != null)
                vTabulador.ID_TABULADOR = (int)vIdTabulador;

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabulador(usuario: vClUsuario, programa: vNbPrograma, pClTipoOperacion: vTipoTransaccion.ToString(), vTabulador: vTabulador);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            if (vTipoTransaccion.ToString() == E_TIPO_OPERACION_DB.I.ToString())
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeWindow");
            else
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "closeEditWindow");
        }
    }
}