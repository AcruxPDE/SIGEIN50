using SIGE.Entidades;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.MPC
{
    public partial class ReporteGraficaAnalisis : System.Web.UI.Page
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

        private List<E_EMPLEADOS_GRAFICAS> vLstEmpleadosSeleccionados
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vLstEmpleadosSeleccionados"]; }
            set { ViewState["vs_vLstEmpleadosSeleccionados"] = value; }
        }

        private List<E_EMPLEADOS_GRAFICAS> vLstEmpleados
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vLstEmpleados"]; }
            set { ViewState["vs_vLstEmpleados"] = value; }
        }

        private List<E_PLANEACION_INCREMENTOS> vListaTabuladorSueldos
        {
            get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vListaTabuladorSueldos"]; }
            set { ViewState["vs_vListaTabuladorSueldos"] = value; }
        }

        private List<E_PLANEACION_INCREMENTOS> vObtienePlaneacionIncremento
        {
            get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vObtienePlaneacionIncremento"]; }
            set { ViewState["vs_vObtienePlaneacionIncremento"] = value; }
        }

        private List<E_SELECCIONADOS> vSeleccionadosTabuladores
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vSeleccionadosTabuladores"]; }
            set { ViewState["vs_vSeleccionadosTabuladores"] = value; }
        }

        private List<E_EMPLEADOS_GRAFICAS> vEmpTabuladores
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vEmpTabuladores"]; }
            set { ViewState["vs_vEmpTabuladores"] = value; }
        }

        public List<E_EMPLEADOS_GRAFICAS> vCargaLista(List<SPE_OBTIENE_EMPLEADOS_Result> pEmpleados)
        {
            List<E_EMPLEADOS_GRAFICAS> vLstEmpleados = new List<E_EMPLEADOS_GRAFICAS>();

            foreach (var item in pEmpleados)
            {
                vLstEmpleados.Add(new E_EMPLEADOS_GRAFICAS
                {
                    ID_EMPLEADO = item.M_EMPLEADO_ID_EMPLEADO,
                    NB_EMPLEADO = item.M_EMPLEADO_NB_EMPLEADO_COMPLETO,
                    NB_PUESTO = item.M_PUESTO_NB_PUESTO,
                });
            }
            return vLstEmpleados;
        }

        private List<E_EMPLEADOS_GRAFICAS> vEmpleados
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vEmpleados"]; }
            set { ViewState["vs_vEmpleados"] = value; }
        }

        #endregion

        #region Metodos

        protected void CargarDatosEmpleados(List<int> pIdsSeleccionados)
        {
            EmpleadoNegocio nEmpleados = new EmpleadoNegocio();
            var vEmpleadosObtenidos = nEmpleados.ObtenerEmpleados();
            var vEmpleadosSeleccionados = vEmpleadosObtenidos.Where(w => pIdsSeleccionados.Contains(w.M_EMPLEADO_ID_EMPLEADO)).ToList();
            List<E_EMPLEADOS_GRAFICAS> vListaEmpleados = new List<E_EMPLEADOS_GRAFICAS>();
            vListaEmpleados = vCargaLista(vEmpleadosSeleccionados);
            foreach (var item in vListaEmpleados)
            {
                if (vLstEmpleadosSeleccionados.Where(w => w.ID_EMPLEADO == item.ID_EMPLEADO).Count() == 0)
                {

                    vLstEmpleadosSeleccionados.Add(item);
                }
            }

            PintarTabulador(true);
        }

        protected void CargarDatosTabuladores(List<int> pIdsSeleccionados)
        {
            if (pIdsSeleccionados != null)
            {

                vSeleccionadosTabuladores = new List<E_SELECCIONADOS>();
                foreach (int item in pIdsSeleccionados)
                {
                    vSeleccionadosTabuladores.Add(new E_SELECCIONADOS { ID_SELECCIONADO = item });
                }
                var vXelements = vSeleccionadosTabuladores.Select(s =>
                                               new XElement("FILTRO",
                                                   new XAttribute("CL_TIPO", "TABULADORES"),
                                               new XElement("TIPO",
                                                   new XAttribute("ID_TABULADOR", s.ID_SELECCIONADO))
                                ));
                XElement vXmlIdTabuladores =
                new XElement("SELECCION", vXelements);

                TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
                vEmpTabuladores = nTabuladores.ObtenieneEmpleadosTabulador(XML_SELECCIONADOS: vXmlIdTabuladores).Select(s =>
                                                                       new E_EMPLEADOS_GRAFICAS()
                                                                       {
                                                                           ID_TABULADOR_EMPLEADO = s.ID_TABULADOR_EMPLEADO,
                                                                           ID_EMPLEADO = s.M_EMPLEADO_ID_EMPLEADO,
                                                                           ID_TABULADOR = s.ID_TABULADOR,
                                                                           NB_TABULADOR = s.NB_TABULADOR,
                                                                           NB_EMPLEADO = s.NB_EMPLEADO,
                                                                           NB_PUESTO = s.NB_PUESTO,
                                                                           MN_SUELDO_ORIGINAL = s.MN_SUELDO_ORIGINAL,
                                                                           NO_NIVEL = s.NO_NIVEL,
                                                                           CL_TABULADOR = s.CL_TABULADOR
                                                                       }).ToList();
            }
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

        protected void PintarTabulador(bool pFgSeleccionados)
        {
                var vEmpleadosSeleccionados = vLstEmpleadosSeleccionados.Select(s => s.ID_EMPLEADO).ToList();
                vEmpleados = vEmpTabuladores.Where(w => vEmpleadosSeleccionados.Contains(w.ID_EMPLEADO)).ToList();
           
            if (vEmpleados != null)
            {
                ScatterChartGraficaAnalisis.PlotArea.Series.Clear();

                foreach (var iTab in vSeleccionadosTabuladores)
                {
                    ScatterSeries vPrimerScatterSeries = new ScatterSeries();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("xValues", typeof(double));
                    dt.Columns.Add("yValues", typeof(double));
                    //int vComienza = int.Parse(rnComienza.Text);
                    //int vTermina = int.Parse(rnTermina.Text);


                    foreach (var iEmp in vEmpleados)
                    {
                        if (iTab.ID_SELECCIONADO == iEmp.ID_TABULADOR)
                        {
                            //if (iEmp.NO_NIVEL >= vComienza && iEmp.NO_NIVEL <= vTermina)
                            //{
                                vPrimerScatterSeries.SeriesItems.Add(iEmp.NO_NIVEL, iEmp.MN_SUELDO_ORIGINAL);
                                vPrimerScatterSeries.LabelsAppearance.Visible = false;
                                vPrimerScatterSeries.Name = "Sueldos" + " " + iEmp.CL_TABULADOR.ToString();
                                vPrimerScatterSeries.TooltipsAppearance.DataFormatString = "{1}";
                                dt.Rows.Add(iEmp.NO_NIVEL, iEmp.MN_SUELDO_ORIGINAL);
                          //  }
                        }
                    }

                    ScatterChartGraficaAnalisis.PlotArea.Series.Add(vPrimerScatterSeries);
                    ScatterChartGraficaAnalisis.PlotArea.YAxis.MinValue = 0;
                    ScatterChartGraficaAnalisis.PlotArea.YAxis.LabelsAppearance.DataFormatString = "{0:C}";
                    RegressionType selectedRegressionType = RegressionType.Linear;
                    CreateRegressionModel.Plot(ScatterChartGraficaAnalisis, dt, "xValues", "yValues", selectedRegressionType);

                    AddCuartil(iTab.ID_SELECCIONADO);
                }
            }
        }

        private List<E_CUARTIL_COMPARACION> Cuartil(List<E_TABULADOR_MAESTRO> pTabuladores, int IdCuartil)
        {
            List<E_CUARTIL_COMPARACION> vMnSeleccion = new List<E_CUARTIL_COMPARACION>();
            switch (IdCuartil)
            {
                case 1: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_MINIMO, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
                    break;
                case 2: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_PRIMER_CUARTIL, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
                    break;
                case 3: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_MEDIO, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
                    break;
                case 4: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_SEGUNDO_CUARTIL, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
                    break;
                case 5: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_MAXIMO, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
                    break;
            }
            return vMnSeleccion;
        }

        protected void AddCuartil(int pIdTabulador)
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            List<E_TABULADOR_MAESTRO> vTabuladores = nTabuladores.ObtieneTabuladorMaestro(ID_TABULADOR: pIdTabulador).Select(s =>
                                              new E_TABULADOR_MAESTRO()
                                              {
                                                  NO_CATEGORIA = s.NO_CATEGORIA,
                                                  MN_MINIMO = s.MN_MINIMO,
                                                  MN_PRIMER_CUARTIL = s.MN_PRIMER_CUARTIL,
                                                  MN_MEDIO = s.MN_MEDIO,
                                                  MN_SEGUNDO_CUARTIL = s.MN_SEGUNDO_CUARTIL,
                                                  MN_MAXIMO = s.MN_MAXIMO,
                                                  NO_NIVEL = s.NO_NIVEL,
                                                  CL_TABULADOR = s.CL_TABULADOR
                                              }).ToList();

            List<E_CUARTIL_COMPARACION> vCuartiles = Cuartil(vTabuladores, vCuartilComparativo);
            var vIdCategorias = vTabuladores.GroupBy(test => test.NO_CATEGORIA).Select(grp => grp.First()).ToList();

            foreach (var item in vIdCategorias)
            {
                ScatterLineSeries vCuartilComparativoG = new ScatterLineSeries();
                foreach (var itemC in vCuartiles)
                {
                    if (item.NO_CATEGORIA == itemC.NO_CATEGORIA)
                    {
                        //if (itemC.NO_NIVEL >= pComienza && itemC.NO_NIVEL <= pTermina)
                        //{
                        vCuartilComparativoG.SeriesItems.Add(itemC.NO_NIVEL, itemC.MN_CATEGORIA);
                        vCuartilComparativoG.LabelsAppearance.Visible = false;
                        vCuartilComparativoG.MarkersAppearance.Visible = false;
                        vCuartilComparativoG.Name = itemC.NO_CATEGORIA.ToString();
                       // }
                    }
                }
                char nb = (char)(item.NO_CATEGORIA + 64);
                vCuartilComparativoG.Name = nb.ToString() + " " + item.CL_TABULADOR;
                ScatterChartGraficaAnalisis.PlotArea.Series.Add(vCuartilComparativoG);
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
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

                    if (Request.QueryString["pNivelMercado"] != null)
                        vCuartilComparativo = int.Parse(Request.QueryString["pNivelMercado"].ToString());

                    vLstEmpleados = new List<E_EMPLEADOS_GRAFICAS>();
                    vLstEmpleadosSeleccionados = new List<E_EMPLEADOS_GRAFICAS>();
                    vListaTabuladorSueldos = new List<E_PLANEACION_INCREMENTOS>();
                    ObtenerPlaneacionIncrementos();
                    if (ContextoTabuladores.oLstTabuladorAnalisis != null)
                    CargarDatosTabuladores(ContextoTabuladores.oLstTabuladorAnalisis.Where(w=>w.ID_TABULADOR == vIdTabulador).FirstOrDefault().vLstEmpleadosTabulador);
                    if (ContextoTabuladores.oLstEmpleadoAnalisis != null)
                    CargarDatosEmpleados(ContextoTabuladores.oLstEmpleadoAnalisis.Where(w => w.ID_TABULADOR == vIdTabulador).FirstOrDefault().vLstEmpleadosTabulador);
                }
            }
        }
    }
}