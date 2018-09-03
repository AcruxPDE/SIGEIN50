using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.MPC
{
    public partial class ConsultaTabuladorMaestro : System.Web.UI.Page
    {

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        int vAnterior = 1;
        int vActual = 1;

        public int vIdTabulador
        {
            get { return (int)ViewState["vsIdTabulador"]; }
            set { ViewState["vsIdTabulador"] = value; }
        }

        public int vCuartilInflacional
        {
            get { return (int)ViewState["vs_vCuartilInflacional"]; }
            set { ViewState["vs_vCuartilInflacional"] = value; }
        }

        private int? vRangoVerde
        {
            get { return (int?)ViewState["vs_vRangoVerde"]; }
            set { ViewState["vs_vRangoVerde"] = value; }
        }

        private int? vRangoAmarillo
        {
            get { return (int?)ViewState["vs_vRangoAmarillo"]; }
            set { ViewState["vs_vRangoAmarillo"] = value; }
        }

        private decimal vPrInflacional
        {
            get { return (decimal)ViewState["vs_vInflacional"]; }
            set { ViewState["vs_vInflacional"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
                SPE_OBTIENE_TABULADORES_Result vCuartiles = nTabuladores.ObtenerTabuladores().FirstOrDefault();
                XElement vXlmCuartiles = XElement.Parse(vCuartiles.XML_VW_CUARTILES);
                var vCuartilesTabulador = vXlmCuartiles.Elements("ITEM").Select(x => new E_CUARTILES
                {
                    ID_CUARTIL = UtilXML.ValorAtributo<int>(x.Attribute("NB_VALOR")),
                    NB_CUARTIL = UtilXML.ValorAtributo<string>(x.Attribute("NB_TEXTO")),
                }).ToList();



                if (Request.QueryString["ID"] != null)
                {
                    vIdTabulador = int.Parse((Request.QueryString["ID"]));
                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                    txtClaveTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTabulador.CL_TIPO_PUESTO;

                    vPrInflacional = vTabulador.PR_INFLACION;
                    if (vTabulador.XML_VARIACION != null)
                    {
                        XElement vXlmVariacion = XElement.Parse(vTabulador.XML_VARIACION);
                        foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
                            if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("green")))
                            {
                                vRangoVerde = UtilXML.ValorAtributo<int>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
                            }
                        foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
                            if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("yellow")))
                            {
                                vRangoAmarillo = UtilXML.ValorAtributo<int>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
                            }
                    }
                    XElement vXlmCuartil = XElement.Parse(vTabulador.XML_CUARTILES);
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INFLACIONAL")).Equals(1)))
                        {
                            vCuartilInflacional = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
                        }
    
                }
            }
        }

        protected void rgdTabuladorMaestro_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            var vObtieneTabuladorMaestro = nTabuladores.ObtieneTabuladorMaestro(ID_TABULADOR: vIdTabulador).Select(s => new E_TABULADOR_MAESTRO()
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
                MN_SIGUIENTE = calculaSiguiente(s, vPrInflacional, vCuartilInflacional)
            }).ToList();
            rgdTabuladorMaestro.DataSource = vObtieneTabuladorMaestro;
        }

        protected decimal? calculaSiguiente(SPE_OBTIENE_TABULADOR_MAESTRO_Result mnSeleccion, decimal pInflacional, int IdCuartil)
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

        protected void rgdTabuladorMaestro_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

      
    }
}