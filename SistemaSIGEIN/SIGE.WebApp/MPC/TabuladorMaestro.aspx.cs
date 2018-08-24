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
using Telerik.Web.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Data;

namespace SIGE.WebApp.MPC
{
    public partial class TabuladorMaestro : System.Web.UI.Page
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

        private List<E_TABULADOR_MAESTRO> vTabuladorMaestro
        {
            get { return (List<E_TABULADOR_MAESTRO>)ViewState["vs_vTabuladorMaestro"]; }
            set { ViewState["vs_vTabuladorMaestro"] = value; }
        }

        private List<E_TABULADOR_MAESTRO> vObtieneTabuladorMaestro
        {
            get { return (List<E_TABULADOR_MAESTRO>)ViewState["vs_vObtieneTabuladorMaestro"]; }
            set { ViewState["vs_vObtieneTabuladorMaestro"] = value; }
        }

        private List<E_CUARTILES> vCuartilesTabulador
        {
            get { return (List<E_CUARTILES>)ViewState["vs_vCuartilesTabulador"]; }
            set { ViewState["vs_vCuartilesTabulador"] = value; }
        }

        private decimal vPrInflacional
        {
            get { return (decimal)ViewState["vs_vInflacional"]; }
            set { ViewState["vs_vInflacional"] = value; }
        }

        #endregion

        private void CopiarTabuladorMaestro(int pIdTabulador)
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabuladores.CopiaTabuladorMaestro(vIdTabulador, pIdTabulador, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
            txtPorcentajeIncremento.Text = vTabulador.PR_INFLACION.ToString();
            Actualizar(null);
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
        }

        private void SeguridadProcesos()
        {
            btnGuardarCerrar.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.J.A");
            btnGuardar.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.J.A");
           // btnConfiguracion.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.J.A");
            btnCopiarTabulador.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.J.A");
           // btnVerNiveles.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.J.A");
            //btnRecalcular.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.J.A");
           // btnMercadoSalarial.Enabled = !ContextoUsuario.oUsuario.TienePermiso("K.A.A.J.A");
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

                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                    XElement vXlmCuartiles = XElement.Parse(vTabulador.XML_VW_CUARTILES);
                    vCuartilesTabulador = vXlmCuartiles.Elements("ITEM").Select(x => new E_CUARTILES
                    {
                        ID_CUARTIL = UtilXML.ValorAtributo<int>(x.Attribute("NB_VALOR")),
                        NB_CUARTIL = UtilXML.ValorAtributo<string>(x.Attribute("NB_TEXTO")),
                    }).ToList();
                    SeguridadProcesos();

                    cmbCuartil.DataSource = vCuartilesTabulador;
                    cmbCuartil.DataTextField = "NB_CUARTIL";
                    cmbCuartil.DataValueField = "ID_CUARTIL".ToString();
                    cmbCuartil.DataBind();
                    //txtClTabulador.Text = vTabulador.CL_TABULADOR;
                    //txtNbTabulador.Text = vTabulador.DS_TABULADOR;
                    //rdpCreacion.SelectedDate = vTabulador.FE_CREACION;
                    //rdpVigencia.SelectedDate = vTabulador.FE_VIGENCIA;
                    if (vTabulador.CL_ESTADO == "CERRADO")
                    {
                        btnGuardarCerrar.Enabled = false;
                        btnGuardar.Enabled = false;
                       // btnConfiguracion.Enabled = false;
                        btnCopiarTabulador.Enabled = false;
                       // btnVerNiveles.Enabled = false;
                        //btnRecalcular.Enabled = false;
                      
                    }
                    txtNbTabulador.InnerText = vTabulador.NB_TABULADOR;
                    txtClTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_CREACION.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTabulador.CL_TIPO_PUESTO;
                    txtNoCategorias.Text = vTabulador.NO_CATEGORIAS.ToString();
                    vPrInflacional = vTabulador.PR_INFLACION;
                    txtPorcentajeIncremento.Text = vTabulador.PR_INFLACION.ToString();
                    XElement vXlmCuartil = XElement.Parse(vTabulador.XML_CUARTILES);
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INFLACIONAL")).Equals(1)))
                        {
                            cmbCuartil.SelectedValue = UtilXML.ValorAtributo<string>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
                        }

                    Actualizar(null);
                }
            }
        }

        protected void grdTabuladorMaestro_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            grdTabuladorMaestro.DataSource = vObtieneTabuladorMaestro;
        }

        protected void cmbCuartil_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ActualizarItems();
            grdTabuladorMaestro.DataSource = vObtieneTabuladorMaestro;
            grdTabuladorMaestro.Rebind();
        }

        protected decimal? calculaSiguiente(E_TABULADOR_MAESTRO mnSeleccion, decimal pInflacional, int IdCuartil)
        {
            decimal? vMnSeleccion = 0;
            decimal? vSiguienteSueldo = 0;
            switch (IdCuartil)
            {
                case 1: vMnSeleccion = mnSeleccion.MN_MINIMO;
                    break;
                case 2: vMnSeleccion = mnSeleccion.MN_PRIMER_CUARTIL;
                    break;
                case 3: vMnSeleccion = mnSeleccion.MN_MEDIO;
                    break;
                case 4: vMnSeleccion = mnSeleccion.MN_SEGUNDO_CUARTIL;
                    break;
                case 5: vMnSeleccion = mnSeleccion.MN_MAXIMO;
                    break;
            }
            vSiguienteSueldo = (vMnSeleccion * (pInflacional + 100) / 100);
            return vSiguienteSueldo;
        }

        protected void ActualizarItems()
        {
            Guid? vIdItem;
            foreach (GridDataItem item in grdTabuladorMaestro.MasterTableView.Items)
            {
                vIdItem = Guid.Parse(item.GetDataKeyValue("ID_ITEM").ToString());
                RadNumericTextBox vMinimo = (RadNumericTextBox)item.FindControl("txnMinimo");
                RadNumericTextBox vCuartilPrimero = (RadNumericTextBox)item.FindControl("txnCuartilPrimero");
                RadNumericTextBox vCuartilMedio = (RadNumericTextBox)item.FindControl("txnCuartilMedio");
                RadNumericTextBox vCuartilSegundo = (RadNumericTextBox)item.FindControl("txnCuartilSegundo");
                RadNumericTextBox vMaximo = (RadNumericTextBox)item.FindControl("txnMaximo");
                E_TABULADOR_MAESTRO vLineaTabulador = vObtieneTabuladorMaestro.FirstOrDefault(f => f.ID_ITEM.Equals(vIdItem));
                vLineaTabulador.MN_MINIMO = vMinimo.Value == null? 0 : (decimal)vMinimo.Value;
                vLineaTabulador.MN_PRIMER_CUARTIL = vCuartilPrimero.Value == null? 0: (decimal)vCuartilPrimero.Value;
                vLineaTabulador.MN_MEDIO = vCuartilMedio.Value == null? 0: (decimal)vCuartilMedio.Value;
                vLineaTabulador.MN_SEGUNDO_CUARTIL = vCuartilSegundo.Value == null? 0 : (decimal)vCuartilSegundo.Value;
                vLineaTabulador.MN_MAXIMO = vMaximo.Value == null? 0: (decimal)vMaximo.Value;
                vLineaTabulador.MN_SIGUIENTE = calculaSiguiente(vLineaTabulador, vPrInflacional, int.Parse(cmbCuartil.SelectedValue));
            }
        }

        //protected void btnRecalcular_Click(object sender, EventArgs e)
        //{
        //    ActualizarItems();
        //    grdTabuladorMaestro.Rebind();
        //}

        protected void btnMercadoSalarialBoton_Click(object sender, EventArgs e)
        {
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.ActualizaMercadoTabuladorNivel(vIdTabulador, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
            Actualizar("RECALCULAR");
        }

        public void MercadoSalarial()
        {
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.ActualizaMercadoTabuladorNivel(vIdTabulador, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
            Actualizar("RECALCULAR");
        }

        protected void Actualizar(string pTipoObtener)
        {
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
            vPrInflacional = vTabulador.PR_INFLACION;
            vObtieneTabuladorMaestro = nTabulador.ObtieneTabuladorMaestro(ID_TABULADOR: vIdTabulador, RECALCULAR: pTipoObtener).Select(s => new E_TABULADOR_MAESTRO()
            {
                ID_TABULADOR_MAESTRO = s.ID_TABULADOR_MAESTRO,
                ID_TABULADOR_NIVEL = s.ID_TABULADOR_NIVEL,
                NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
                NO_CATEGORIA = s.NO_CATEGORIA,
                NB_CATEGORIA = s.NB_CATEGORIA,
                PR_PROGRESION = s.PR_PROGRESION,
                MN_MINIMO = s.MN_MINIMO,
                MN_PRIMER_CUARTIL = s.MN_PRIMER_CUARTIL,
                MN_MEDIO = s.MN_MEDIO,
                MN_SEGUNDO_CUARTIL = s.MN_SEGUNDO_CUARTIL,
                MN_MAXIMO = s.MN_MAXIMO,
                NO_NIVEL = s.NO_NIVEL,
                MN_SIGUIENTE = calculaSiguiente(new E_TABULADOR_MAESTRO()
                {
                    MN_MINIMO = s.MN_MINIMO,
                    MN_PRIMER_CUARTIL = s.MN_PRIMER_CUARTIL,
                    MN_MEDIO = s.MN_MEDIO,
                    MN_SEGUNDO_CUARTIL = s.MN_SEGUNDO_CUARTIL,
                    MN_MAXIMO = s.MN_MAXIMO
                }, vPrInflacional, int.Parse(cmbCuartil.SelectedValue))
            }).ToList();

            //var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
            txtNoCategorias.Text = vTabulador.NO_CATEGORIAS.ToString();
            txtPorcentajeIncremento.Text = vTabulador.PR_INFLACION.ToString();

            grdTabuladorMaestro.DataSource = vObtieneTabuladorMaestro;
            grdTabuladorMaestro.DataBind();
            grdTabuladorMaestro.Rebind();
        }             

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarTabuladorMaestro(false);
        }

        protected void btnGuardarCerrar_Click(object sender, EventArgs e)
        {
            GuardarTabuladorMaestro(true);
        }

        public void GuardarTabuladorMaestro(bool pFgCerrarVentana)
        {
            int vIdTabuladorNivel = 0;
            int vCategoria = 0;
            int vNoNivel = 0;
            vTabuladorMaestro = new List<E_TABULADOR_MAESTRO>();
            foreach (GridDataItem item in grdTabuladorMaestro.MasterTableView.Items)
            {
                vIdTabuladorNivel = (int.Parse(item.GetDataKeyValue("ID_TABULADOR_NIVEL").ToString()));
                vCategoria = (int.Parse(item.GetDataKeyValue("NO_CATEGORIA").ToString()));
                vNoNivel = (int.Parse(item.GetDataKeyValue("NO_NIVEL").ToString()));
                RadNumericTextBox vMinimo = (RadNumericTextBox)item.FindControl("txnMinimo");
                RadNumericTextBox vCuartilPrimero = (RadNumericTextBox)item.FindControl("txnCuartilPrimero");
                RadNumericTextBox vCuartilMedio = (RadNumericTextBox)item.FindControl("txnCuartilMedio");
                RadNumericTextBox vCuartilSegundo = (RadNumericTextBox)item.FindControl("txnCuartilSegundo");
                RadNumericTextBox vMaximo = (RadNumericTextBox)item.FindControl("txnMaximo");

                vTabuladorMaestro.Add(new E_TABULADOR_MAESTRO
                    {
                        ID_TABULADOR_NIVEL = vIdTabuladorNivel,
                        NO_CATEGORIA = vCategoria,
                        NO_NIVEL = vNoNivel,
                        MN_MINIMO = decimal.Parse(vMinimo.Text),
                        MN_PRIMER_CUARTIL = decimal.Parse(vCuartilPrimero.Text),
                        MN_MEDIO = decimal.Parse(vCuartilMedio.Text),
                        MN_SEGUNDO_CUARTIL = decimal.Parse(vCuartilSegundo.Text),
                        MN_MAXIMO = decimal.Parse(vMaximo.Text),
                    });
            }
            var vXelements = vTabuladorMaestro.Select(x =>
                                        new XElement("MAESTRO",
                                        new XAttribute("ID_TABULADOR_NIVEL", x.ID_TABULADOR_NIVEL),
                                        new XAttribute("NO_NIVEL", x.NO_NIVEL),
                                        new XAttribute("SECUENCIA", x.NO_CATEGORIA),
                                        new XAttribute("MN_MINIMO", x.MN_MINIMO),
                                        new XAttribute("MN_PRIMER_CUARTIL", x.MN_PRIMER_CUARTIL),
                                        new XAttribute("MN_CUARTIL_MEDIO", x.MN_MEDIO),
                                        new XAttribute("MN_SEGUNDO_CUARTIL", x.MN_SEGUNDO_CUARTIL),
                                        new XAttribute("MN_MAXIMO", x.MN_MAXIMO)
                             ));
            XElement TABULADORMAESTRO =
            new XElement("MAESTROS", vXelements
            );

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabuladorMaestro(vIdTabulador, TABULADORMAESTRO.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                bool vCerrarVentana = pFgCerrarVentana;
                string vCallBackFunction = vCerrarVentana ? "closeWindow" : null;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);
            }
        }

        protected void ramTabuladorMaestro_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string pParameter = e.Argument;
            E_SELECTOR vTabuladorCopia = new E_SELECTOR();

            if (pParameter != null)
            {
                vTabuladorCopia = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);
            }

           if(vTabuladorCopia != null){
               if (vTabuladorCopia.clTipo == "GUARDAR")
                    Actualizar(null);
               if (vTabuladorCopia.clTipo == "PR_GUARDAR")
                    Actualizar("RECALCULAR");
               if (vTabuladorCopia.clTipo == "TABULADOR")
                CopiarTabuladorMaestro(int.Parse(vTabuladorCopia.oSeleccion.ToString()));
               if (vTabuladorCopia.clTipo == "ACTUALIZAR")
                   MercadoSalarial();

           }
        }

        protected void grdTabuladorMaestro_ItemDataBound(object sender, GridItemEventArgs e)
        {
           
            int strId = 0;

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;

                strId = int.Parse(dataItem.GetDataKeyValue("ID_TABULADOR_NIVEL").ToString());
              
                if (strId % 2 == 0)
                    dataItem.CssClass = "RadGrid1Class";
                else dataItem.CssClass = "RadGrid2Class";
            }
        }

        //protected void txnMaximo_TextChanged(object sender, EventArgs e)
        //{
           
        //}

        //protected void txnMinimo_TextChanged(object sender, EventArgs e)
        //{

        //}

        protected void txnCuartilPrimero_TextChanged(object sender, EventArgs e)
        {
            ActualizarItems();
            grdTabuladorMaestro.Rebind();
        }

        protected void btnRecalcular_Click(object sender, EventArgs e)
        {
            List<E_TABULADOR_MAESTRO> vLstNivelesMaxMin = new List<E_TABULADOR_MAESTRO>();
            foreach (GridDataItem item in grdTabuladorMaestro.MasterTableView.Items)
            {
                if (item.GetDataKeyValue("NO_CATEGORIA").ToString() == "1")
                {
                    RadNumericTextBox vMin = (RadNumericTextBox)item.FindControl("txnMinimo");
                    RadNumericTextBox vMax = (RadNumericTextBox)item.FindControl("txnMaximo");
                    vLstNivelesMaxMin.Add(
                        new E_TABULADOR_MAESTRO { 
                            NO_NIVEL = int.Parse(item.GetDataKeyValue("NO_NIVEL").ToString()),
                            MN_MINIMO = vMin.Text == ""? 0 : decimal.Parse(vMin.Text),
                            MN_MAXIMO = vMax.Text == ""? 0 : decimal.Parse(vMax.Text)
                        });
                }
            }

            var vXelements = vLstNivelesMaxMin.Select(x =>
                                     new XElement("NIVELES",
                                     new XAttribute("NO_NIVEL", x.NO_NIVEL),
                                     new XAttribute("MN_MINIMO", x.MN_MINIMO),
                                     new XAttribute("MN_MAXIMO", x.MN_MAXIMO)
                          ));


            XElement TABULADORMAESTRO =
            new XElement("TABULADOR", vXelements
            );

            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
            vPrInflacional = vTabulador.PR_INFLACION;
            vObtieneTabuladorMaestro = nTabulador.ObtieneTabuladorRecalculado(ID_TABULADOR: vIdTabulador, XML_NIVELES: TABULADORMAESTRO.ToString()).Select(s => new E_TABULADOR_MAESTRO()
            {
                ID_TABULADOR_MAESTRO = s.ID_TABULADOR_MAESTRO,
                ID_TABULADOR_NIVEL = s.ID_TABULADOR_NIVEL,
                NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
                NO_CATEGORIA = s.NO_CATEGORIA,
                NB_CATEGORIA = s.NB_CATEGORIA,
                PR_PROGRESION = s.PR_PROGRESION,
                MN_MINIMO = s.MN_MINIMO,
                MN_PRIMER_CUARTIL = s.MN_PRIMER_CUARTIL,
                MN_MEDIO = s.MN_MEDIO,
                MN_SEGUNDO_CUARTIL = s.MN_SEGUNDO_CUARTIL,
                MN_MAXIMO = s.MN_MAXIMO,
                NO_NIVEL = s.NO_NIVEL,
                MN_SIGUIENTE = calculaSiguiente(new E_TABULADOR_MAESTRO()
                {
                    MN_MINIMO = s.MN_MINIMO,
                    MN_PRIMER_CUARTIL = s.MN_PRIMER_CUARTIL,
                    MN_MEDIO = s.MN_MEDIO,
                    MN_SEGUNDO_CUARTIL = s.MN_SEGUNDO_CUARTIL,
                    MN_MAXIMO = s.MN_MAXIMO
                }, vPrInflacional, int.Parse(cmbCuartil.SelectedValue))
            }).ToList();

            txtNoCategorias.Text = vTabulador.NO_CATEGORIAS.ToString();
            txtPorcentajeIncremento.Text = vTabulador.PR_INFLACION.ToString();

            grdTabuladorMaestro.DataSource = vObtieneTabuladorMaestro;
            grdTabuladorMaestro.DataBind();
            grdTabuladorMaestro.Rebind();
        }
    }
}