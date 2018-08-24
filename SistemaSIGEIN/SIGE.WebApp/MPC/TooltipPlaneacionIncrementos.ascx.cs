using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using SIGE.Negocio.Utilerias;


namespace SIGE.WebApp.MPC
{
    public partial class TooltipPlaneacionIncrementos : System.Web.UI.UserControl
    {

        public List<E_PLANEACION_INCREMENTOS> vSecuanciasEmpleado
        {
            get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vObtienePlaneacionIncrementos"]; }
            set { ViewState["vs_vObtienePlaneacionIncrementos"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void PintaGrid() {
            var vSecuancia = vSecuanciasEmpleado.FirstOrDefault();
            XElement vXlmCategorias = XElement.Parse(vSecuancia.XML_CATEGORIAS);
            E_CATEGORIA vCategoria = new E_CATEGORIA();

            List<E_CATEGORIA> lstCategoria = new List<E_CATEGORIA>();
           
            foreach (XElement vXmlSecuencia in vXlmCategorias.Elements("ITEM")){
                lstCategoria.Add(new E_CATEGORIA
                {
                    NO_CATEGORIA = UtilXML.ValorAtributo<int>(vXmlSecuencia.Attribute("NO_CATEGORIA")),
                    MN_MINIMO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MINIMO")),
                    MN_PRIMER_CUARTIL = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_PRIMER_CUARTIL")),
                    MN_MEDIO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MEDIO")),
                    MN_SEGUNDO_CUARTIL = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_SEGUNDO_CUARTIL")),
                    MN_MAXIMO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MAXIMO"))
                });
            }

            foreach (var item in lstCategoria)
            {
                item.CANTIDAD = CalculaCantidad(vSecuancia.CUARTIL_SELECCIONADO, item.MN_MINIMO, item.MN_PRIMER_CUARTIL, item.MN_MEDIO, item.MN_SEGUNDO_CUARTIL, item.MN_MAXIMO);
            }

            rgSecuancias.DataSource = lstCategoria;
            rgSecuancias.DataBind();
        }

        protected decimal? CalculaCantidad(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
        {
            decimal? vCantidad = 0;
            switch (pMnSeleccionado)
            {
                case 1: vCantidad = pMnMinimo;
                    break;
                case 2: vCantidad = pMnPrimerCuartil;
                    break;
                case 3: vCantidad = pMnMedio;
                    break;
                case 4: vCantidad = pMnSegundoCuartil;
                    break;
                case 5: vCantidad = pMnMaximo;
                    break;
            }
            return vCantidad;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PintaGrid();
        }
    }
}