using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SIGE.WebApp.MPC
{
    public partial class ReporteTabuladorSueldos : System.Web.UI.Page
    {
        #region Variables

        public int vIdTabulador
        {
            get { return (int)ViewState["vsIdTabulador"]; }
            set { ViewState["vsIdTabulador"] = value; }
        }

        private decimal vPrInflacional
        {
            get { return (decimal)ViewState["vs_vInflacional"]; }
            set { ViewState["vs_vInflacional"] = value; }
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

        public int vCuartilComparativo
        {
            get { return (int)ViewState["vs_vCuartilComparativo"]; }
            set { ViewState["vs_vCuartilComparativo"] = value; }
        }

        private List<E_CONSULTA_SUELDOS> vObtienePlaneacionIncremento
        {
            get { return (List<E_CONSULTA_SUELDOS>)ViewState["vs_vObtienePlaneacionIncremento"]; }
            set { ViewState["vs_vObtienePlaneacionIncremento"] = value; }
        }

        private List<E_CONSULTA_SUELDOS> vLstSeleccionadosTabuladorSueldos
        {
            get { return (List<E_CONSULTA_SUELDOS>)ViewState["vs_vLstSeleccionadosTabuladorSueldos"]; }
            set { ViewState["vs_vLstSeleccionadosTabuladorSueldos"] = value; }
        }

        #endregion

        #region Metodos

        protected decimal? CalculaCantidadCuartil(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
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

        protected List<E_CATEGORIA> SeleccionCuartil(XElement xlmCuartiles, int? ID_TABULADOR_EMPLEADO)
        {
            List<E_CATEGORIA> lstCategoria = new List<E_CATEGORIA>();

            foreach (XElement vXmlSecuencia in xlmCuartiles.Elements("ITEM"))
            {
                lstCategoria.Add(new E_CATEGORIA
                {
                    ID_TABULADOR_EMPLEADO = ID_TABULADOR_EMPLEADO,
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
                item.CANTIDAD = CalculaCantidadCuartil(vCuartilComparativo, item.MN_MINIMO, item.MN_PRIMER_CUARTIL, item.MN_MEDIO, item.MN_SEGUNDO_CUARTIL, item.MN_MAXIMO);
            }

            return lstCategoria;
        }

        private void ObtenerPlaneacionIncrementos()
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();

            vObtienePlaneacionIncremento = nTabuladores.ObtenerConsultaSueldos(ID_TABULADOR: vIdTabulador).Select(s => new E_CONSULTA_SUELDOS()
            {
                NUM_ITEM = (int?)s.NUM_RENGLON,
                ID_TABULADOR_EMPLEADO = (int?)s.ID_TABULADOR_EMPLEADO,
                NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
                CL_PUESTO = s.CL_PUESTO,
                NB_PUESTO = s.NB_PUESTO,
                CL_DEPARTAMENTO = s.CL_DEPARTAMENTO,
                NB_DEPARTAMENTO = s.NB_DEPARTAMENTO,
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
                INCREMENTO = s.MN_SUELDO_NUEVO == null && s.MN_SUELDO_ORIGINAL == null ? 0 : Math.Abs((decimal)s.MN_SUELDO_NUEVO - (decimal)s.MN_SUELDO_ORIGINAL),
                NO_NIVEL = s.NO_NIVEL,
                XML_CATEGORIAS = s.XML_CATEGORIA,
                CUARTIL_SELECCIONADO = vCuartilComparativo,
                NO_VALUACION = s.NO_VALUACION
            }).ToList();
            foreach (E_CONSULTA_SUELDOS item in vObtienePlaneacionIncremento)
            {
                if (item.MN_SUELDO_ORIGINAL != 0)
                    item.PR_INCREMENTO = (item.INCREMENTO / item.MN_SUELDO_ORIGINAL) * 100;
            }
        }

        protected decimal? CalculoPrDiferencia(decimal? pMnMinimo, decimal? pMnMaximo, decimal? pMnSueldo) // SE CREA NUEVO METODO PARA CALCULAR EL PR DIFERENCIA 30/04/2018 
        {
            decimal? vMnDivisor = 0;
            if (pMnSueldo > 0)
            {

                if (pMnSueldo < pMnMinimo)
                    vMnDivisor = pMnMinimo;
                else
                    if (pMnSueldo > pMnMaximo)
                        vMnDivisor = pMnMaximo;
                    else
                        vMnDivisor = pMnSueldo;
                vMnDivisor = (((pMnSueldo * 100) / vMnDivisor) - 100);
            }
            return vMnDivisor;
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

        protected decimal? CalculaMinimo(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
        {
            decimal? vMinimo = 0;
            switch (pMnSeleccionado)
            {
                case 1: vMinimo = pMnMinimo;
                    break;
                case 2: vMinimo = pMnPrimerCuartil;
                    break;
                case 3: vMinimo = pMnMedio;
                    break;
                case 4: vMinimo = pMnSegundoCuartil;
                    break;
                case 5: vMinimo = pMnMaximo;
                    break;
            }
            return vMinimo;
        }

        protected decimal? CalculaMaximo(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
        {
            decimal? vMaximo = 0;
            switch (pMnSeleccionado)
            {
                case 1: vMaximo = pMnMinimo;
                    break;
                case 2: vMaximo = pMnPrimerCuartil;
                    break;
                case 3: vMaximo = pMnMedio;
                    break;
                case 4: vMaximo = pMnSegundoCuartil;
                    break;
                case 5: vMaximo = pMnMaximo;
                    break;
            }
            return vMaximo;
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

        protected void ActualizarLista(int pCuartilComparativo)
        {
            foreach (E_CONSULTA_SUELDOS item in vObtienePlaneacionIncremento)
            {
                item.MN_MINIMO_CUARTIL = CalculaMinimo(pCuartilComparativo, item.MN_MINIMO_MINIMO, item.MN_MINIMO_PRIMER_CUARTIL, item.MN_MINIMO_MEDIO, item.MN_MINIMO_SEGUNDO_CUARTIL, item.MN_MINIMO_MAXIMO);
                item.MN_MAXIMO_CUARTIL = CalculaMaximo(pCuartilComparativo, item.MN_MAXIMO_MINIMO, item.MN_MAXIMO_PRIMER_CUARTIL, item.MN_MAXIMO_MEDIO, item.MN_MAXIMO_SEGUNDO_CUARTIL, item.MN_MAXIMO_MAXIMO);
                item.DIFERENCIA = CalculoDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                if (item.MN_SUELDO_ORIGINAL > 0)
                    // item.PR_DIFERENCIA = (item.DIFERENCIA / item.MN_SUELDO_ORIGINAL) * 100;
                    item.PR_DIFERENCIA = CalculoPrDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                else item.PR_DIFERENCIA = 0;
                item.COLOR_DIFERENCIA = VariacionColor(item.PR_DIFERENCIA, item.MN_SUELDO_ORIGINAL);
                item.ICONO = ObtenerIconoDiferencia(item.PR_DIFERENCIA, item.MN_SUELDO_ORIGINAL);
            }
        }

        protected void CargarDatosSeleccionadosSueldos(List<int> pIdsSeleccionados)
        {
            ActualizarLista(vCuartilComparativo);
            int vNumeroItem = 1;
            var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.Where(w => pIdsSeleccionados.Contains(w.ID_TABULADOR_EMPLEADO == null ? 0 : (int)w.ID_TABULADOR_EMPLEADO)).ToList();
            foreach (var item in vEmpleadosTabuladorSeleccionados)
            {
                if (item.ID_TABULADOR_EMPLEADO != null)
                {
                    if (vLstSeleccionadosTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
                    {
                        vLstSeleccionadosTabuladorSueldos.Add(new E_CONSULTA_SUELDOS
                        {
                            NUM_ITEM = vNumeroItem,
                            ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO,
                            NB_TABULADOR_NIVEL = item.NB_TABULADOR_NIVEL,
                            CL_PUESTO = item.CL_PUESTO,
                            NB_PUESTO = item.NB_PUESTO,
                            CL_DEPARTAMENTO = item.CL_DEPARTAMENTO,
                            NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
                            CL_EMPLEADO = item.CL_EMPLEADO,
                            NB_EMPLEADO = item.NB_EMPLEADO,
                            MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL,
                            MN_SUELDO_NUEVO = item.MN_SUELDO_NUEVO,
                            NO_NIVEL = item.NO_NIVEL,
                            XML_CATEGORIAS = item.XML_CATEGORIAS,
                            DIFERENCIA = item.DIFERENCIA,
                            PR_DIFERENCIA = item.PR_DIFERENCIA,
                            COLOR_DIFERENCIA = item.COLOR_DIFERENCIA,
                            ICONO = item.ICONO,
                            NO_VALUACION = item.NO_VALUACION,
                            lstCategorias = SeleccionCuartil(XElement.Parse(item.XML_CATEGORIAS), item.ID_TABULADOR_EMPLEADO)

                        });

                        vNumeroItem++;
                    }
                }
            }
        }

        protected string ObtenerEncabezado(int PCuartilComparativo)
        {
            string vTitulo = "";
            switch (PCuartilComparativo)
            {
                case 1:
                    vTitulo = "Tabulador Mínimo";
                    break;
                case 2:
                    vTitulo = "Tabulador Primer Cuartil";
                    break;
                case 3:
                    vTitulo = "Tabulador Medio";
                    break;
                case 4:
                    vTitulo = "Tabulador Tercer Cuartil";
                    break;
                case 5:
                    vTitulo = "Tabulador Máximo";
                    break;
                default:
                    vTitulo = "Tabulador Medio";
                    break;
            }

            return vTitulo;
        }

        protected HtmlGenericControl GeneraTablaReporte()
        {
            HtmlGenericControl vTabla = new HtmlGenericControl("table");
            vTabla.Attributes.Add("style", "border-collapse: collapse;");

            HtmlGenericControl vCtrlColumn = new HtmlGenericControl("thead");
            vCtrlColumn.Attributes.Add("style", "background: #E6E6E6;");

            HtmlGenericControl vCtrlRowEncabezado1 = new HtmlGenericControl("tr");
            vCtrlRowEncabezado1.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

            HtmlGenericControl vCtrlTh1 = new HtmlGenericControl("td");
            vCtrlTh1.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:50px;");
            vCtrlTh1.Attributes.Add("rowspan", "2");
            vCtrlTh1.Attributes.Add("align", "center");
            vCtrlTh1.InnerText = String.Format("{0}", "No");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh1);

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("td");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:120px;");
            vCtrlTh2.Attributes.Add("rowspan", "2");
            vCtrlTh2.InnerText = String.Format("{0}", "Puesto");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh2);

            HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("td");
            vCtrlTh3.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:120px;");
            vCtrlTh3.Attributes.Add("rowspan", "2");
            vCtrlTh3.InnerText = String.Format("{0}", "Área");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh3);

            HtmlGenericControl vCtrlTh4 = new HtmlGenericControl("td");
            vCtrlTh4.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:250px;");
            vCtrlTh4.Attributes.Add("rowspan", "2");
            vCtrlTh4.InnerText = String.Format("{0}", "Nombre completo");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh4);

            HtmlGenericControl vCtrlTh5 = new HtmlGenericControl("td");
            vCtrlTh5.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:100px;");
            vCtrlTh5.Attributes.Add("rowspan", "2");
            vCtrlTh5.InnerText = String.Format("{0}", "Sueldo");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh5);

            HtmlGenericControl vCtrlTh6 = new HtmlGenericControl("td");
            vCtrlTh6.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:100px;");
            vCtrlTh6.Attributes.Add("rowspan", "2");
            vCtrlTh6.InnerText = String.Format("{0}", "Diferencia");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh6);

            HtmlGenericControl vCtrlTh7 = new HtmlGenericControl("td");
            vCtrlTh7.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:120px;");
            vCtrlTh7.Attributes.Add("rowspan", "2");
            vCtrlTh7.InnerText = String.Format("{0}", "Porcentaje");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh7);

            HtmlGenericControl vCtrlTh8 = new HtmlGenericControl("td");
            vCtrlTh8.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:100px;");
            vCtrlTh8.Attributes.Add("rowspan", "2");
            vCtrlTh8.InnerText = String.Format("{0}", "Valuación");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh8);


            var vResultado = vLstSeleccionadosTabuladorSueldos.FirstOrDefault();
            if (vResultado != null)
            {
                HtmlGenericControl vCtrlThGrupo = new HtmlGenericControl("td");
                vCtrlThGrupo.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:100px;");
                vCtrlThGrupo.Attributes.Add("align", "center");
                vCtrlThGrupo.Attributes.Add("COLSPAN", vResultado.lstCategorias.Count().ToString());
                vCtrlThGrupo.InnerText = String.Format("{0}", ObtenerEncabezado(vCuartilComparativo));

                vCtrlRowEncabezado1.Controls.Add(vCtrlThGrupo);
            }

            vCtrlColumn.Controls.Add(vCtrlRowEncabezado1);

            HtmlGenericControl vCtrlRowEncabezado2 = new HtmlGenericControl("tr");
            vCtrlRowEncabezado2.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

            var vResultadoCategorias = vLstSeleccionadosTabuladorSueldos.FirstOrDefault();
            if (vResultadoCategorias != null)
            {
                foreach (var item in vResultadoCategorias.lstCategorias)
                {
                    HtmlGenericControl vCtrlThNuevo2 = new HtmlGenericControl("td");
                    vCtrlThNuevo2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:50px;");
                    vCtrlThNuevo2.Attributes.Add("align", "center");
                    vCtrlThNuevo2.InnerText = String.Format("{0}", (char)(item.NO_CATEGORIA + 64));
                    vCtrlRowEncabezado2.Controls.Add(vCtrlThNuevo2);
                }
            }


            vCtrlColumn.Controls.Add(vCtrlRowEncabezado2);

            vTabla.Controls.Add(vCtrlColumn);

            HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");

            List<int?> vLstNiveles = vLstSeleccionadosTabuladorSueldos.Select(s => s.NO_NIVEL).Distinct().ToList();

            foreach (var iNivel in vLstNiveles)
            {
                HtmlGenericControl vCtrlRowNivel = new HtmlGenericControl("tr");
                vCtrlRowNivel.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

                HtmlGenericControl vCtrlNivel = new HtmlGenericControl("td");
                vCtrlNivel.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                vCtrlNivel.Attributes.Add("COLSPAN", (vResultado.lstCategorias.Count() + 8).ToString());
                vCtrlNivel.InnerText = String.Format("{0}", "Nivel: " + iNivel);
                vCtrlRowNivel.Controls.Add(vCtrlNivel);

                vCtrlTbody.Controls.Add(vCtrlRowNivel);

                foreach (var item in vLstSeleccionadosTabuladorSueldos.Where(w => w.NO_NIVEL == iNivel))
                {
                    HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");
                    vCtrlRow.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

                    HtmlGenericControl vCtrlNumero = new HtmlGenericControl("td");
                    vCtrlNumero.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlNumero.Attributes.Add("align", "center");
                    vCtrlNumero.InnerText = String.Format("{0}", item.NUM_ITEM);
                    vCtrlRow.Controls.Add(vCtrlNumero);

                    HtmlGenericControl vCtrlNbPuesto = new HtmlGenericControl("td");
                    vCtrlNbPuesto.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlNbPuesto.InnerText = String.Format("{0}", item.NB_PUESTO);
                    vCtrlRow.Controls.Add(vCtrlNbPuesto);

                    HtmlGenericControl vCtrlNbDepartamento = new HtmlGenericControl("td");
                    vCtrlNbDepartamento.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlNbDepartamento.InnerText = String.Format("{0}", item.NB_DEPARTAMENTO);
                    vCtrlRow.Controls.Add(vCtrlNbDepartamento);

                    HtmlGenericControl vCtrlNbEmpleado = new HtmlGenericControl("td");
                    vCtrlNbEmpleado.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlNbEmpleado.InnerText = String.Format("{0}", item.NB_EMPLEADO);
                    vCtrlRow.Controls.Add(vCtrlNbEmpleado);

                    HtmlGenericControl vCtrlMnSueldo = new HtmlGenericControl("td");
                    vCtrlMnSueldo.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlMnSueldo.InnerText = String.Format("{0:C}", item.MN_SUELDO_ORIGINAL);
                    vCtrlRow.Controls.Add(vCtrlMnSueldo);

                    HtmlGenericControl vCtrlMnDiferencia = new HtmlGenericControl("td");
                    vCtrlMnDiferencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlMnDiferencia.InnerText = String.Format("{0:C}", Math.Abs((decimal)item.DIFERENCIA));
                    vCtrlRow.Controls.Add(vCtrlMnDiferencia);

                    HtmlGenericControl vCtrlPrDiferencia = new HtmlGenericControl("td");
                    vCtrlPrDiferencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlPrDiferencia.InnerHtml = String.Format("{0}", String.Format("{0:N2}", Math.Abs(item.PR_DIFERENCIA == null ? 0 : (decimal)item.PR_DIFERENCIA) > 100 ? 100 : Math.Abs(item.PR_DIFERENCIA == null ? 0 : (decimal)item.PR_DIFERENCIA)) + "%"
                       + "<span style=\"border: 1px solid gray; border-radius: 5px; background:" + item.COLOR_DIFERENCIA + ";\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<img src='../Assets/images/Icons/25/Arrow" + item.ICONO + ".png' />");
                    vCtrlRow.Controls.Add(vCtrlPrDiferencia);

                    HtmlGenericControl vCtrlValuacion = new HtmlGenericControl("td");
                    vCtrlValuacion.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlValuacion.Attributes.Add("align", "center");
                    vCtrlValuacion.InnerText = String.Format("{0}", item.NO_VALUACION);
                    vCtrlRow.Controls.Add(vCtrlValuacion);

                    var vValor = vLstSeleccionadosTabuladorSueldos.Where(t => t.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).FirstOrDefault();
                    if (vValor != null)
                    {
                        foreach (var itemValor in vValor.lstCategorias)
                        {
                            HtmlGenericControl vCtrlTabMedio = new HtmlGenericControl("td");
                            vCtrlTabMedio.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                            vCtrlTabMedio.InnerText = String.Format("{0:C}", itemValor.CANTIDAD);
                            vCtrlRow.Controls.Add(vCtrlTabMedio);
                        }
                    }


                    vCtrlTbody.Controls.Add(vCtrlRow);
                }
            }

            vTabla.Controls.Add(vCtrlTbody);

            return vTabla;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TabuladoresNegocio nTabuladores = new TabuladoresNegocio();

                if (Request.QueryString["ID"] != null)
                {
                    vIdTabulador = int.Parse((Request.QueryString["ID"]));

                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                    txtClaveTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtNbTabulador.InnerText = vTabulador.NB_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_CREACION.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTabulador.CL_TIPO_PUESTO;

                    if (Request.QueryString["pNivelMercado"] != null)
                        vCuartilComparativo = int.Parse(Request.QueryString["pNivelMercado"].ToString());

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

                    ObtenerPlaneacionIncrementos();
                }

                vLstSeleccionadosTabuladorSueldos = new List<E_CONSULTA_SUELDOS>();

                CargarDatosSeleccionadosSueldos(ContextoTabuladores.oLstEmpleadoTabulador.Where(w => w.ID_TABULADOR == vIdTabulador).FirstOrDefault().vLstEmpleadosTabulador);
                dvTabla.Controls.Add(GeneraTablaReporte());
            }
        }
    }
}