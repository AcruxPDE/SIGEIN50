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
using SIGE.Entidades;
using System.IO;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.MPC
{
    public partial class VentanaCopiarTabulador : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        int? vIdTabulador
        {
            get { return (int?)ViewState["vs_vIdTabulador"]; }
            set { ViewState["vs_vIdTabulador"] = value; }
        }

        E_TABULADOR vCopiaTabulador
        {
            get { return (E_TABULADOR)ViewState["vs_vCopiaTabulador"]; }
            set { ViewState["vs_vCopiaTabulador"] = value; }
        }

        E_TABULADOR vTabula
        {
            get { return (E_TABULADOR)ViewState["vs_vTabula"]; }
            set { ViewState["vs_vTabula"] = value; }
        }

        private void SeguridadProcesos()
        {
           // btnGuardarCopiar.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.A.A");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                //rdpCreacion.SelectedDate = DateTime.Now;
                if (Request.QueryString["ID"] != null)
                {
                    vIdTabulador = int.Parse((Request.QueryString["ID"]));
                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                   vTabula = new E_TABULADOR  
                    {
                      CL_TABULADOR = vTabulador.CL_TABULADOR, 
                      ID_TABULADOR = vTabulador.ID_TABULADOR,
                      NB_TABULADOR = vTabulador.NB_TABULADOR,
                      DS_TABULADOR = vTabulador.DS_TABULADOR,
                      NO_NIVELES = vTabulador.NO_NIVELES,
                      NO_CATEGORIAS = vTabulador.NO_CATEGORIAS,
                      PR_INFLACION = vTabulador.PR_INFLACION,
                      PR_PROGRESION = vTabulador.PR_PROGRESION,
                      XML_VARIACION = vTabulador.XML_VARIACION,
                      ID_CUARTIL_INCREMENTO = vTabulador.ID_CUARTIL_INCREMENTO,
                      ID_CUARTIL_INFLACIONAL = vTabulador.ID_CUARTIL_INFLACIONAL,
                      ID_CUARTIL_MERCADO = vTabulador.ID_CUARTIL_MERCADO,
                      FE_CREACION = vTabulador.FE_CREACION,
                      FE_VIGENCIA = vTabulador.FE_VIGENCIA,
                      CL_ESTADO = vTabulador.CL_ESTADO,
                      CL_SUELDO_COMPARACION = vTabulador.CL_SUELDO_COMPARACION,
                      CL_TIPO_PUESTO = vTabulador.CL_TIPO_PUESTO
                    };
                   txtCrearAPartir.Text = vTabula.CL_TABULADOR;
                   txtVersionTabuladorCopiar.Text = vTabula.CL_TABULADOR;
                   txtNombreTabulador.Text = vTabula.NB_TABULADOR;
                   txtDescripcion.Text = vTabula.DS_TABULADOR;
                   rdpCreacion.SelectedDate = vTabula.FE_CREACION;
                   //DateTime vFechaVigencia = DateTime.Now.AddYears(1);
                   // int mes = vFechaVigencia.Month;
                   // int vDiferencia = 12 - mes;
                   // DateTime vFechaUltimoDiaMes = GetLastDayOf(vFechaVigencia);
                   // rdpVigencia.SelectedDate = vFechaUltimoDiaMes.AddMonths(vDiferencia);
                   rdpVigencia.SelectedDate = vTabula.FE_VIGENCIA;

                }
                SeguridadProcesos();
            }
        }
        
        DateTime GetLastDayOf(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        protected void btnGuardarCopiar_Click(object sender, EventArgs e)
        {
            E_TABULADOR vTabulador = new E_TABULADOR();
            vTabulador.ID_TABULADOR = vIdTabulador;
            vTabulador.CL_TABULADOR = txtVersionTabuladorCopiar.Text;
            vTabulador.NB_TABULADOR = txtNombreTabulador.Text;
            vTabulador.DS_TABULADOR = txtDescripcion.Text;
            vTabulador.FE_CREACION = rdpCreacion.SelectedDate ?? DateTime.Now;
            vTabulador.FE_VIGENCIA = rdpVigencia.SelectedDate ?? DateTime.Now;
            vTabulador.CL_ESTADO = "ABIERTO";
            vTabulador.CL_SUELDO_COMPARACION = vTabula.CL_SUELDO_COMPARACION;
            vTabulador.CL_TIPO_PUESTO = vTabula.CL_TIPO_PUESTO;
            vTabulador.ID_CUARTIL_INCREMENTO = vTabula.ID_CUARTIL_INCREMENTO;
            vTabulador.ID_CUARTIL_INFLACIONAL = vTabula.ID_CUARTIL_INFLACIONAL;
            vTabulador.ID_CUARTIL_MERCADO = vTabula.ID_CUARTIL_MERCADO;
            vTabulador.NO_CATEGORIAS = vTabula.NO_CATEGORIAS;
            vTabulador.NO_NIVELES = vTabula.NO_NIVELES;
            vTabulador.PR_INFLACION = vTabula.PR_INFLACION;
            vTabulador.PR_PROGRESION = vTabula.PR_PROGRESION;
            vTabulador.XML_VARIACION = vTabula.XML_VARIACION;

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaTabuladorCopia(usuario: vClUsuario, programa: vNbPrograma, pClTipoOperacion: "I", vTabulador: vTabulador);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        }
    }
}