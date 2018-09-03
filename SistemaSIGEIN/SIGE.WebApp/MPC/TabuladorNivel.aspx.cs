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
    public partial class TabuladorNivel : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        private int vIdTabulador
        {
            get { return (int)ViewState["vs_vIdTabulador"]; }
            set { ViewState["vs_vIdTabulador"] = value; }
        }

      

        public int vNivelesTabulador
        {
            get { return (int)ViewState["vs_vNivelesTabulador"]; }
            set { ViewState["vs_vNivelesTabulador"] = value; }
        }

        private List<E_NIVELES> vTabuladorNivel
        {
            get { return (List<E_NIVELES>)ViewState["vs_vTabuladorNivel"]; }
            set { ViewState["vs_vTabuladorNivel"] = value; }
        }

        public string vCL_GUARDAR
        {
            get { return (string)ViewState["vs_vCL_GUARDAR"]; }
            set { ViewState["vs_vCL_GUARDAR"] = value; }
        }

        private void SeguridadProcesos()
        {
            btnGuardar.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.E.A");
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    vIdTabulador = int.Parse((Request.QueryString["ID"]));
                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTavulador = nTabulador.ObtenerTabuladores(vIdTabulador).FirstOrDefault();
                    SeguridadProcesos();

                    txtClTabulador.InnerText = vTavulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTavulador.DS_TABULADOR;
                    txtNbTabulador.InnerText = vTavulador.NB_TABULADOR;
                    txtVigencia.InnerText = vTavulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTavulador.FE_CREACION.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTavulador.CL_TIPO_PUESTO;
                    if (vTavulador.CL_ESTADO == "CERRADO")
                    {
                        btnGuardar.Enabled = false;
                    }
                    //txtClTabulador.Text = vTavulador.CL_TABULADOR;
                    //txtNbTabulador.Text = vTavulador.DS_TABULADOR;
                    vNivelesTabulador = vTavulador.NO_NIVELES;
                }
            }
            
        }

        protected void grdNiveles_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            var vTabuladorNivel = nTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador);
            grdNiveles.DataSource = vTabuladorNivel;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int vIdTabuladorNivel = 0;
            vTabuladorNivel = new List<E_NIVELES>();
            foreach (GridDataItem item in grdNiveles.MasterTableView.Items)
            { 
                vIdTabuladorNivel = (int.Parse(item.GetDataKeyValue("ID_TABULADOR_NIVEL").ToString()));
                RadTextBox vClNivel = (RadTextBox)item.FindControl("txtClNivel");
                RadTextBox vNbNivel = (RadTextBox)item.FindControl("txtNbNivel");
                RadNumericTextBox vOrden = (RadNumericTextBox)item.FindControl("txnOrden");
                RadNumericTextBox vProgresion = (RadNumericTextBox)item.FindControl("txnProgresion");

                vTabuladorNivel.Add(new E_NIVELES { ID_TABULADOR_NIVEL = vIdTabuladorNivel, CL_TABULADOR_NIVEL = vClNivel.Text, NB_TABULADOR_NIVEL = vNbNivel.Text, NO_ORDEN = int.Parse(vOrden.Text), PR_PROGRESION = decimal.Parse(vProgresion.Text)});
            }

            var dups = vTabuladorNivel.GroupBy(x => x.NO_ORDEN).Where(x => x.Count() > 1).Select(x => x.Key).ToList();

            if (dups.Count == 0)
            {

                TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                var vTabuladorProgresion = nTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador).ToList();

                var vPrTavulador = vTabuladorNivel.Select(s => s.PR_PROGRESION).ToList();
                var vPrProgresion = vTabuladorProgresion.Select(s => s.PR_PROGRESION).ToList();

                if (vPrTavulador.SequenceEqual(vPrProgresion))
                    vCL_GUARDAR = "GUARDAR";
                else vCL_GUARDAR = "PR_GUARDAR";

                var vXelements = vTabuladorNivel.Select(x =>
                                               new XElement("NIVEL",
                                               new XAttribute("ID_TABULADOR_NIVEL", x.ID_TABULADOR_NIVEL),
                                               new XAttribute("CL_TABULADOR_NIVEL", x.CL_TABULADOR_NIVEL),
                                               new XAttribute("NB_TABULADOR_NIVEL", x.NB_TABULADOR_NIVEL),
                                               new XAttribute("NO_ORDEN", x.NO_ORDEN),
                                               new XAttribute("PR_PROGRESION", x.PR_PROGRESION)
                                    ));
                XElement TABULADORNIVEL =
                new XElement("NIVELES", vXelements
                );

                E_RESULTADO vResultado = nTabulador.ActualizaTabuladorNivel(vIdTabulador, TABULADORNIVEL.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, 400, 150, pCallBackFunction: "closeWindow");
            }

            else {

                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No se pueden repetir valores en el número de orden.", E_TIPO_RESPUESTA_DB.ERROR);
            }
        }

        //protected void Valida()
        //{
        //    GridItemCollection oListaNiveles = new GridItemCollection();
        //    oListaNiveles = grdNiveles.SelectedItems;
        //    foreach (GridDataItem item in oListaNiveles)
        //    {
        //        int vNoOrden = 0;
        //        vNoOrden = int.Parse((item.FindControl("txnOrden") as RadNumericTextBox).Text);

        //    }
        //}
        protected void grdNiveles_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadNumericTextBox txnOrden = (RadNumericTextBox)item.FindControl("txnOrden");
                txnOrden.MaxValue = vNivelesTabulador;
            } 
        }
    }
}