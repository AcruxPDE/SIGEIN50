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
using Telerik.Web.UI;
using System.Xml.Linq;
using Newtonsoft.Json;
using SIGE.Entidades;


namespace SIGE.WebApp.MPC
{
    public partial class MercadoSalarial : System.Web.UI.Page
    {
        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        String vTipoTransaccion = "";

        public int vIdTabulador
        {
            get { return (int)ViewState["vsIdTabulador"]; }
            set { ViewState["vsIdTabulador"] = value; }
        }

        private List<E_TABULADOR_PUESTO> vTabuladorPuesto
        {
            get { return (List<E_TABULADOR_PUESTO>)ViewState["vs_vTabuladorPuesto"]; }
            set { ViewState["vs_vTabuladorPuesto"] = value; }
        }

        private List<E_TABULADOR_PUESTO> vObtieneTabuladorPuesto
        {
            get { return (List<E_TABULADOR_PUESTO>)ViewState["vs_vObtieneTabuladorPuesto"]; }
            set { ViewState["vs_vObtieneTabuladorPuesto"] = value; }
        }

        #endregion

        #region Funciones

        protected void CopiarTabuladorMercado(int pIdTabulador)
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            List<SPE_OPTIENE_COPIA_MERCADO_SALARIAL_Result> vLstMercadoPuestos = new List<SPE_OPTIENE_COPIA_MERCADO_SALARIAL_Result>();
            vLstMercadoPuestos = nTabuladores.ObtenerMercadoSalarialTabulador(vIdTabulador, pIdTabulador);
            foreach (GridDataItem item in grdMercadoSalarial.Items)
            {
                int vIdTabuladorPuesto = int.Parse(item.GetDataKeyValue("ID_TABULADOR_PUESTO").ToString());
                var vCopiaMercado = vLstMercadoPuestos.Where(x => x.ID_TABULADOR_PUESTO == vIdTabuladorPuesto).FirstOrDefault();
                if (vCopiaMercado != null)
                {
                    RadNumericTextBox vMin = item.FindControl("txnMinimo") as RadNumericTextBox;
                    vMin.Text = vCopiaMercado.MN_MINIMO.ToString();
                    RadNumericTextBox vMax = item.FindControl("txnMaximo") as RadNumericTextBox;
                    vMax.Text = vCopiaMercado.MN_MAXIMO.ToString();

                }
            }

        }

        private void VerificarMinimos()
        {
            GridItemCollection oListaPuestosNiveles = new GridItemCollection();
            oListaPuestosNiveles = grdMercadoSalarial.Items;
            bool vFgMinCero = false;
            foreach (GridDataItem item in oListaPuestosNiveles)
            {
                var vValorMin = (item.FindControl("txnMinimo") as RadNumericTextBox);
                string vValor = vValorMin.Value.ToString();
                if (vValor == "0")
                {
                    (item.FindControl("txnMinimo") as RadNumericTextBox).EnabledStyle.BorderColor = System.Drawing.Color.Red;
                    vFgMinCero = true;
                }
            }
            if (vFgMinCero)
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No fue posible generar un mercado mínimo para algunos puestos, favor de ingresar esta información.", E_TIPO_RESPUESTA_DB.WARNING, 400, 200, pCallBackFunction: "");

        }

        private void SeguridadProcesos()
        {
            btnGuardarCerrar.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.I.A");
            btnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.I.A");
            btnCopiarMercadoSalarial.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.I.A");
            btnAjuste.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.I.A");

        }

        protected void Actualizar()
        {
            foreach (var item in vObtieneTabuladorPuesto)
            {
                foreach (var iPuesto in vTabuladorPuesto)
                {
                    if (item.ID_PUESTO == iPuesto.ID_PUESTO)
                    {
                        if (item.MN_MAXIMO == iPuesto.MN_MAXIMO && item.MN_MINIMO == iPuesto.MN_MINIMO)
                            iPuesto.CL_ORIGEN = iPuesto.CL_ORIGEN;
                        else if (item.MN_MINIMO == 0 && item.MN_MINIMO == 0)
                            iPuesto.CL_ORIGEN = iPuesto.CL_ORIGEN;
                        else iPuesto.CL_ORIGEN = "MERCADO";
                    }
                }
            }
        }

        public void GuardarMercadoSalarial(bool pFgCerrarVentana)
        {
            int vIdPuesto = 0;
            string vClOrigen = null;
            foreach (GridDataItem item in grdMercadoSalarial.MasterTableView.Items)
            {
                vIdPuesto = (int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString()));
                vClOrigen = item.GetDataKeyValue("CL_ORIGEN").ToString();
                RadNumericTextBox vMinimo = (RadNumericTextBox)item.FindControl("txnMinimo");
                RadNumericTextBox vMaximo = (RadNumericTextBox)item.FindControl("txnMaximo");

                if (!vMinimo.Text.Equals("") & !vMaximo.Text.Equals(""))
                {
                    vTabuladorPuesto.Add(new E_TABULADOR_PUESTO { ID_PUESTO = vIdPuesto, MN_MINIMO = decimal.Parse(vMinimo.Text), MN_MAXIMO = decimal.Parse(vMaximo.Text), CL_ORIGEN = vClOrigen });
                }
            }
            Actualizar();
            //for (int i = 0; i < vTabuladorPuesto.Count; i++)
            //{
            //    if (vTabuladorPuesto.ElementAt(i).MN_MAXIMO != vObtieneTabuladorPuesto.ElementAt(i).MN_MAXIMO || vTabuladorPuesto.ElementAt(i).MN_MINIMO != vObtieneTabuladorPuesto.ElementAt(i).MN_MINIMO)
            //    {
            //        vTabuladorPuesto.ElementAt(i).CL_ORIGEN = "MERCADO";
            //    }
            //}
            var vXelements = vTabuladorPuesto.Select(x =>
                                           new XElement("MERCADO",
                                           new XAttribute("ID_PUESTO", x.ID_PUESTO),
                                           new XAttribute("MN_MINIMO", x.MN_MINIMO),
                                           new XAttribute("MN_MAXIMO", x.MN_MAXIMO),
                                           new XAttribute("CL_ORIGEN", x.CL_ORIGEN)
                                ));
            XElement TABULADORPUESTO =
            new XElement("MERCADOS", vXelements
            );
            vTipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabuladorPuesto(vTipoTransaccion, vIdTabulador, TABULADORPUESTO.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                bool vCerrarVentana = pFgCerrarVentana;
                string vCallBackFunction = vCerrarVentana ? "closeWindow" : null;
                grdMercadoSalarial.Rebind();
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);
            }
        }


        #endregion

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
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                    SeguridadProcesos();

                    txtClTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtNbTabulador.InnerText = vTabulador.NB_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_CREACION.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTabulador.CL_TIPO_PUESTO;
                    if (vTabulador.CL_ESTADO == "CERRADO")
                    {
                        btnGuardarCerrar.Enabled = false;
                        btnGuardar.Enabled = false;
                        btnCopiarMercadoSalarial.Enabled = false;
                        btnAjuste.Enabled = false;
                    }
                }
            }
            vTabuladorPuesto = new List<E_TABULADOR_PUESTO>();
        }

        protected void grdMercadoSalarial_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();

            vObtieneTabuladorPuesto = nTabuladores.ObtienePuestosTabulador(ID_TABULADOR: vIdTabulador).Select(s => new E_TABULADOR_PUESTO
            {
                NO_RENGLON = (int)s.NO_RENGLON,
                ID_TABULADOR_PUESTO = s.ID_TABULADOR_PUESTO,
                ID_PUESTO = s.ID_PUESTO,
                MN_MINIMO = s.MN_MINIMO,
                MN_MAXIMO = s.MN_MAXIMO,
                NB_DEPARTAMENTO = s.NB_DEPARTAMENTO,
                NB_PUESTO = s.NB_PUESTO,
                NO_NIVEL = s.NO_NIVEL,
                CL_ORIGEN = s.CL_ORIGEN
            }).ToList();
            grdMercadoSalarial.DataSource = vObtieneTabuladorPuesto;

            GridGroupByField field = new GridGroupByField();
            field.FieldName = "NO_NIVEL";
            field.HeaderText = "Nivel";
            field.HeaderText = "<strong>Nivel</strong>";
            field.FormatString = "<strong>{0}</strong>";
            GridGroupByExpression ex = new GridGroupByExpression();
            ex.GroupByFields.Add(field);
            ex.SelectFields.Add(field);
            grdMercadoSalarial.MasterTableView.GroupByExpressions.Add(ex);
        }

        protected void btnAjuste_Click(object sender, EventArgs e)
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            grdMercadoSalarial.DataSource = nTabuladores.ObtieneDescriptivosPuestosTabulador(ID_TABULADOR: vIdTabulador);
            grdMercadoSalarial.Rebind();
            VerificarMinimos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarMercadoSalarial(false);
        }

        protected void btnGuardarCerrar_Click(object sender, EventArgs e)
        {
            GuardarMercadoSalarial(true);
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vTabuladorCopia = new E_SELECTOR();

            string vParameter = e.Argument;
            if (vParameter != null)
            {
                vTabuladorCopia = JsonConvert.DeserializeObject<E_SELECTOR>(vParameter);
            }

            if (vTabuladorCopia.clTipo == "TABULADOR")
            {
                CopiarTabuladorMercado(int.Parse(vTabuladorCopia.oSeleccion.ToString()));
            }
        }

        protected void grdMercadoSalarial_ItemDataBound(object sender, GridItemEventArgs e)
        {
            int strId = 0;

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;

                strId = int.Parse(dataItem.GetDataKeyValue("NO_NIVEL").ToString());

                if (strId % 2 == 0)
                    dataItem.CssClass = "RadGrid1Class";
                else dataItem.CssClass = "RadGrid2Class";
            }
        }
    }
}