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
    public partial class VentanaComparacionSueldo : System.Web.UI.Page
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

        public int vCuartilInflacional
        {
            get { return (int)ViewState["vs_vCuartilInflacional"]; }
            set { ViewState["vs_vCuartilInflacional"] = value; }
        }

        public int vCuartilIncremento
        {
            get { return (int)ViewState["vs_vCuartilIncremento"]; }
            set { ViewState["vs_vCuartilIncremento"] = value; }
        }

        public int vCuartilComparativo
        {
            get { return (int)ViewState["vs_vCuartilComparativo"]; }
            set { ViewState["vs_vCuartilComparativo"] = value; }
        }

        private decimal vPrInflacional
        {
            get { return (decimal)ViewState["vs_vInflacional"]; }
            set { ViewState["vs_vInflacional"] = value; }
        }

        private List<E_PLANEACION_INCREMENTOS> vObtienePlaneacionIncremento
        {
            get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vObtienePlaneacionIncremento"]; }
            set { ViewState["vs_vObtienePlaneacionIncremento"] = value; }
        }


        protected decimal? CalculoDiferencia(decimal? pMnMinimo, decimal? pMnMaximo, decimal? pMnSueldo)
        {
            decimal? vMnDivisor = 0;
            if (pMnSueldo < pMnMinimo)
                vMnDivisor = pMnMinimo;
            else
                if (pMnSueldo > pMnMaximo)
                    vMnDivisor = pMnMaximo;
                else
                    vMnDivisor = pMnSueldo;
            return vMnDivisor = pMnSueldo - vMnDivisor;
        }

        protected string ObtenerIconoDiferencia(decimal? pPrDiferencia, decimal? pMnSueldo)
        {
            //return pPrDiferencia < 0 ? "Down" : pPrDiferencia > 0 ? "Up" : "Equal";
            string vImagen = null;
            if (pPrDiferencia < 0 & pMnSueldo != 0)
                vImagen = "Down";
            else if (pPrDiferencia > 0 & pMnSueldo != 0)
                vImagen = "Up";
            else if (pPrDiferencia == 0 & pMnSueldo == 0)
                vImagen = "Delete";
            else vImagen = "Equal";

            return vImagen;
        }

        protected string VariacionColor(decimal? pPrDireferencia, decimal? pMnSueldo)
        {
            string vColor;
            if (pPrDireferencia == null)
                pPrDireferencia = 0;

            decimal vPorcentaje = Math.Abs((decimal)pPrDireferencia);

            if (vPorcentaje >= 0 && vPorcentaje <= vRangoVerde & pMnSueldo > 0)
                vColor = "green";
            else if (vPorcentaje > vRangoVerde && vPorcentaje <= vRangoAmarillo & pMnSueldo > 0)
                vColor = "yellow";
            else if (pPrDireferencia == 0 & pMnSueldo == 0)
                vColor = "gray";
            else
                vColor = "red";
            return vColor;
        }

        private void ObtenerPlaneacionIncrementos()
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            vObtienePlaneacionIncremento = nTabuladores.ObtienePlaneacionIncrementos(ID_TABULADOR: vIdTabulador).Select(s => new E_PLANEACION_INCREMENTOS()
            {
                NUM_ITEM = (int)s.NUM_RENGLON,
                ID_TABULADOR_EMPLEADO = s.ID_TABULADOR_EMPLEADO,
                NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
                CL_PUESTO = s.CL_PUESTO,
                NB_PUESTO = s.NB_PUESTO,
                CL_EMPLEADO = s.CL_EMPLEADO,
                NB_EMPLEADO = s.NOMBRE,
                MN_MINIMO_MINIMO = s.MN_MINIMO_MINIMO,
                MN_MAXIMO_MINIMO = s.MN_MAXIMO_MINIMO,
                MN_MINIMO_PRIMER_CUARTIL = s.MN_MINIMO_PRIMER_CUARTIL,
                MN_MAXIMO_PRIMER_CUARTIL = s.MN_MAXIMO_PRIMER_CUARTIL,
                MN_MINIMO_MEDIO = s.MN_MINIMO_MEDIO,
                MN_MAXIMO_MEDIO = s.MN_MAXIMO_MEDIO,
                MN_MINIMO_SEGUNDO_CUARTIL = s.MN_MINIMO_SEGUNDO_CUARTIL,
                MN_MAXIMO_SEGUNDO_CUARTIL = s.MN_MAXIMO_SEGUNDO_CUARTIL,
                MN_MAXIMO_MAXIMO = s.MN_MAXIMO_MAXIMO,
                MN_MINIMO_MAXIMO = s.MN_MINIMO_MAXIMO,
                MN_SUELDO_ORIGINAL = s.MN_SUELDO_ORIGINAL,
                MN_SUELDO_NUEVO = s.MN_SUELDO_NUEVO,
                MN_MINIMO = s.MN_MINIMO,
                MN_MAXIMO = s.MN_MAXIMO,
                INCREMENTO = Math.Abs(s.MN_SUELDO_NUEVO - s.MN_SUELDO_ORIGINAL),
                NO_NIVEL = s.NO_NIVEL,
                XML_CATEGORIAS = s.XML_CATEGORIA,
                CUARTIL_SELECCIONADO = vCuartilComparativo,
                NO_VALUACION = s.NO_VALUACION
            }).ToList();

            foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncremento)
            {
                if (item.MN_SUELDO_ORIGINAL != 0)
                    item.PR_INCREMENTO = (item.INCREMENTO / item.MN_SUELDO_ORIGINAL) * 100;
            }
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
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INCREMENTO")).Equals(1)))
                        {
                            vCuartilIncremento = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
                        }
                    foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
                        if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_MERCADO")).Equals(1)))
                        {
                            vCuartilComparativo = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
                          
                          
                        }


                    ObtenerPlaneacionIncrementos();
                }
            }
        }

        protected void rgdComparacionInventarioMercado_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncremento)
            {
                item.DIFERENCIA_MERCADO = CalculoDiferencia(item.MN_MINIMO, item.MN_MAXIMO, item.MN_SUELDO_ORIGINAL);

                if (item.MN_SUELDO_ORIGINAL != 0)
                    item.PR_DIFERENCIA_MERCADO = (item.DIFERENCIA_MERCADO / item.MN_SUELDO_ORIGINAL) * 100;
                else item.PR_DIFERENCIA_MERCADO = 0;

                item.COLOR_DIFERENCIA_MERCADO = VariacionColor(item.PR_DIFERENCIA_MERCADO, item.MN_SUELDO_ORIGINAL);
                item.ICONO_MERCADO = ObtenerIconoDiferencia(item.PR_DIFERENCIA_MERCADO, item.MN_SUELDO_ORIGINAL);
            }
            rgdComparacionInventarioMercado.DataSource = vObtienePlaneacionIncremento;
        }

        protected void grdCodigoColores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            List<E_CODIGO_COLORES> vCodigoColores = new List<E_CODIGO_COLORES>();
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "green", DESCRIPCION = "Sueldo dentro del nivel establecido por el tabulador (variación inferior al " + vRangoVerde.ToString() + "%)." });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "yellow", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador entre el " + vRangoVerde.ToString() + "% y " + vRangoAmarillo.ToString() + "%." });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "red", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%." });
            grdCodigoColores.DataSource = vCodigoColores;
        }

        protected void rgdComparacionInventarioMercado_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgdComparacionInventarioMercado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgdComparacionInventarioMercado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgdComparacionInventarioMercado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgdComparacionInventarioMercado.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgdComparacionInventarioMercado.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

     
    }
}