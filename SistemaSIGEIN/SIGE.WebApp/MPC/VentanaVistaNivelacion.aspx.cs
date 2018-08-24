using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.Entidades;
using Telerik.Web.UI;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System.Xml.Linq;

namespace SIGE.WebApp.MPC
{
    public partial class VentanaVistaNivelacion : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdTabulador
        {
            get { return (int)ViewState["vs_vIdTabulador"]; }
            set { ViewState["vs_vIdTabulador"] = value; }
        }

        private List<E_NIVELACION> vVistaNivelacion
        {
            get { return (List<E_NIVELACION>)ViewState["vs_vVistaNivelacion"]; }
            set { ViewState["vs_vVistaNivelacion"] = value; }
        }
        private List<E_NIVELACION> vVistaObtenida
        {
            get { return (List<E_NIVELACION>)ViewState["vs_vVistaObtenida"]; }
            set { ViewState["vs_vVistaObtenida"] = value; }
        }

        private List<E_NIVELACION> vResultadoVista
        {
            get { return (List<E_NIVELACION>)ViewState["vs_vResultadoVista"]; }
            set { ViewState["vs_vResultadoVista"] = value; }
        }

        public int vNivelesTabulador
        {
            get { return (int)ViewState["vs_vNivelesTabulador"]; }
            set { ViewState["vs_vNivelesTabulador"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                vIdTabulador = int.Parse((Request.QueryString["ID"]));
                TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                var vTavulador = nTabulador.ObtenerTabuladores(vIdTabulador).FirstOrDefault();
                txtClTabulador.Text = vTavulador.CL_TABULADOR;
                txtNbTabulador.Text = vTavulador.DS_TABULADOR;
                vNivelesTabulador = vTavulador.NO_NIVELES;
            }

        }

        protected void grdValuacionPuesto_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            var vNivelacion = nTabulador.ObtieneValuacionNivelacion(ID_TABULADOR: vIdTabulador);
            vVistaObtenida = new List<E_NIVELACION>();

            foreach (var Item in vNivelacion)
            {
                vVistaObtenida.Add(new E_NIVELACION { ID_TABULADOR_PUESTO = int.Parse(Item.ID_TABULADOR_PUESTO.ToString()), NO_NIVEL = int.Parse(Item.NO_NIVEL.ToString()) });
            }
            grdValuacionPuesto.DataSource = vNivelacion;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int vIdTabuladorPuesto = 0;
            vVistaNivelacion = new List<E_NIVELACION>();
            foreach (GridDataItem item in grdValuacionPuesto.MasterTableView.Items)
            {
                vIdTabuladorPuesto = (int.Parse(item.GetDataKeyValue("ID_TABULADOR_PUESTO").ToString()));

                RadNumericTextBox vNivel = (RadNumericTextBox)item.FindControl("txnNivel");

                if (!vNivel.Text.Equals(""))
                {
                    vVistaNivelacion.Add(new E_NIVELACION { ID_TABULADOR_PUESTO = vIdTabuladorPuesto, NO_NIVEL = int.Parse(vNivel.Text) });
                }
            }

            vResultadoVista = new List<E_NIVELACION>();

            for (int i = 0; i < vVistaNivelacion.Count; i++)
            {
            if(vVistaNivelacion.ElementAt(i).NO_NIVEL != vVistaObtenida.ElementAt(i).NO_NIVEL)
                {
                    vResultadoVista.Add(vVistaNivelacion.ElementAt(i));
                }
            }
            var vXelements = vResultadoVista.Select(x =>
                                           new XElement("NIVEL",
                                           new XAttribute("ID_TABULADOR_PUESTO", x.ID_TABULADOR_PUESTO),
                                           new XAttribute("NO_NIVEL", x.NO_NIVEL)
                                ));
            XElement NIVELACION =
            new XElement("NIVELES", vXelements
            );
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.ActualizaNivelTabuladorPuesto(vIdTabulador, NIVELACION.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
        }

       
        protected void grdValuacionPuesto_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadNumericTextBox txnNivel = (RadNumericTextBox)item.FindControl("txnNivel");
                txnNivel.MaxValue = vNivelesTabulador;
            } 
        }
    }
}