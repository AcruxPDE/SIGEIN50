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
using SIGE.Negocio.Administracion;
using Newtonsoft.Json;
using SIGE.Entidades.Administracion;
using Telerik.Web.UI;
using System.Drawing;
using System.Data;
using SIGE.Entidades.EvaluacionOrganizacional;


namespace SIGE.WebApp.MPC
{
    public partial class Consultas : System.Web.UI.Page
    {
    //    private string vClUsuario;
    //    private string vNbPrograma;
    //    private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
    //    int vAnterior = 1;
    //    int vActual = 1;

    //    public int vIdTabulador
    //    {
    //        get { return (int)ViewState["vsIdTabulador"]; }
    //        set { ViewState["vsIdTabulador"] = value; }
    //    }

    //    public int vCuartilInflacional
    //    {
    //        get { return (int)ViewState["vs_vCuartilInflacional"]; }
    //        set { ViewState["vs_vCuartilInflacional"] = value; }
    //    }

    //    public int vCuartilIncremento
    //    {
    //        get { return (int)ViewState["vs_vCuartilIncremento"]; }
    //        set { ViewState["vs_vCuartilIncremento"] = value; }
    //    }

    //    public int vCuartilComparativo
    //    {
    //        get { return (int)ViewState["vs_vCuartilComparativo"]; }
    //        set { ViewState["vs_vCuartilComparativo"] = value; }
    //    }

    //    private decimal vPrInflacional
    //    {
    //        get { return (decimal)ViewState["vs_vInflacional"]; }
    //        set { ViewState["vs_vInflacional"] = value; }
    //    }

    //    private List<E_PLANEACION_INCREMENTOS> vObtienePlaneacionIncremento
    //    {
    //        get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vObtienePlaneacionIncremento"]; }
    //        set { ViewState["vs_vObtienePlaneacionIncremento"] = value; }
    //    }

    //    private int? vRangoVerde
    //    {
    //        get { return (int?)ViewState["vs_vRangoVerde"]; }
    //        set { ViewState["vs_vRangoVerde"] = value; }
    //    }

    //    private int? vRangoAmarillo
    //    {
    //        get { return (int?)ViewState["vs_vRangoAmarillo"]; }
    //        set { ViewState["vs_vRangoAmarillo"] = value; }
    //    }

    //    private List<E_SELECCIONADOS> vSeleccionadosTabuladores
    //    {
    //        get { return (List<E_SELECCIONADOS>)ViewState["vs_vSeleccionadosTabuladores"]; }
    //        set { ViewState["vs_vSeleccionadosTabuladores"] = value; }
    //    }

    //    private List<E_EMPLEADOS_GRAFICAS> vEmpleados
    //    {
    //        get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vEmpleados"]; }
    //        set { ViewState["vs_vEmpleados"] = value; }
    //    }

    //    private List<E_EMPLEADOS_GRAFICAS> vEmpTabuladores
    //    {
    //        get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vEmpTabuladores"]; }
    //        set { ViewState["vs_vEmpTabuladores"] = value; }
    //    }

    //    private List<int> vTabuladores
    //    {
    //        get { return (List<int>)ViewState["vs_vTabuladores"]; }
    //        set { ViewState["vs_vTabuladores"] = value; }
    //    }

    //    private List<E_NIVELES> vNivelesTabulador
    //    {
    //        get { return (List<E_NIVELES>)ViewState["vs_vNivelesTabulador"]; }
    //        set { ViewState["vs_vNivelesTabulador"] = value; }
    //    }

    //    private List<E_EMPLEADOS_GRAFICAS> vEmpleadosDesviaciones
    //    {
    //        get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vEmpleadosDesviaciones"]; }
    //        set { ViewState["vs_vEmpleadosDesviaciones"] = value; }
    //    }

    //    private List<E_EMPLEADOS_GRAFICAS> vLstEmpleados
    //    {
    //        get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vLstEmpleados"]; }
    //        set { ViewState["vs_vLstEmpleados"] = value; }
    //    }

    //    private List<E_PLANEACION_INCREMENTOS> vListaTabuladorSueldos
    //    {
    //        get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vListaTabuladorSueldos"]; }
    //        set { ViewState["vs_vListaTabuladorSueldos"] = value; }
    //    }

    //    public List<E_SELECCION_PERIODOS_DESEMPENO> oLstPeriodos
    //    {
    //        get { return (List<E_SELECCION_PERIODOS_DESEMPENO>)ViewState["vs_lista_periodos"]; }
    //        set { ViewState["vs_lista_periodos"] = value; }
    //    }

    //    private void ObtenerPlaneacionIncrementos()
    //    {
    //        TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
    //        vObtienePlaneacionIncremento = nTabuladores.ObtienePlaneacionIncrementos(ID_TABULADOR: vIdTabulador).Select(s => new E_PLANEACION_INCREMENTOS()
    //        {
    //            NUM_ITEM = (int)s.NUM_RENGLON,
    //            ID_TABULADOR_EMPLEADO = s.ID_TABULADOR_EMPLEADO,
    //            NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
    //            CL_PUESTO = s.CL_PUESTO,
    //            NB_PUESTO = s.NB_PUESTO,
    //            CL_EMPLEADO = s.CL_EMPLEADO,
    //            NB_EMPLEADO = s.NOMBRE,
    //            MN_MINIMO_MINIMO = s.MN_MINIMO_MINIMO,
    //            MN_MAXIMO_MINIMO = s.MN_MAXIMO_MINIMO,
    //            MN_MINIMO_PRIMER_CUARTIL = s.MN_MINIMO_PRIMER_CUARTIL,
    //            MN_MAXIMO_PRIMER_CUARTIL = s.MN_MAXIMO_PRIMER_CUARTIL,
    //            MN_MINIMO_MEDIO = s.MN_MINIMO_MEDIO,
    //            MN_MAXIMO_MEDIO = s.MN_MAXIMO_MEDIO,
    //            MN_MINIMO_SEGUNDO_CUARTIL = s.MN_MINIMO_SEGUNDO_CUARTIL,
    //            MN_MAXIMO_SEGUNDO_CUARTIL = s.MN_MAXIMO_SEGUNDO_CUARTIL,
    //            MN_MAXIMO_MAXIMO = s.MN_MAXIMO_MAXIMO,
    //            MN_MINIMO_MAXIMO = s.MN_MINIMO_MAXIMO,
    //            MN_SUELDO_ORIGINAL = s.MN_SUELDO_ORIGINAL,
    //            MN_SUELDO_NUEVO = s.MN_SUELDO_NUEVO,
    //            MN_MINIMO = s.MN_MINIMO,
    //            MN_MAXIMO = s.MN_MAXIMO,
    //            INCREMENTO = Math.Abs(s.MN_SUELDO_NUEVO - s.MN_SUELDO_ORIGINAL),
    //            NO_NIVEL = s.NO_NIVEL,
    //            XML_CATEGORIAS = s.XML_CATEGORIA,
    //            CUARTIL_SELECCIONADO = vCuartilComparativo,
    //            NO_VALUACION = s.NO_VALUACION
    //        }).ToList();
    //        rgdIncrementosPlaneados.DataSource = vObtienePlaneacionIncremento;
    //        foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncremento)
    //        {
    //            if (item.MN_SUELDO_ORIGINAL != 0)
    //                item.PR_INCREMENTO = (item.INCREMENTO / item.MN_SUELDO_ORIGINAL) * 100;
    //        }
    //    }

    //    private void AgregarPeriodos(string pPeriodos)
    //    {
    //        List<E_SELECCION_PERIODOS_DESEMPENO> vLstPeriodos = JsonConvert.DeserializeObject<List<E_SELECCION_PERIODOS_DESEMPENO>>(pPeriodos);

    //        foreach (E_SELECCION_PERIODOS_DESEMPENO item in vLstPeriodos)
    //        {
    //            if (oLstPeriodos.Where(t => t.idPeriodo == item.idPeriodo).Count() == 0)
    //            {
    //                oLstPeriodos.Add(new E_SELECCION_PERIODOS_DESEMPENO
    //                {
    //                    idPeriodo = item.idPeriodo,
    //                    clPeriodo = item.clPeriodo,
    //                    nbPeriodo = item.nbPeriodo,
    //                    dsPeriodo = item.dsPeriodo
    //                });

    //                ContextoBono.oLstPeriodosBonos.Add(new E_SELECCION_PERIODOS_DESEMPENO
    //                {
    //                    idPeriodo = item.idPeriodo,
    //                    clPeriodo = item.clPeriodo,
    //                    nbPeriodo = item.nbPeriodo,
    //                    dsPeriodo = item.dsPeriodo,
    //                    dsNotas = item.dsNotas,
    //                    feInicio = item.feInicio,
    //                    feTermino = item.feTermino
    //                });
    //            }
    //        }
    //        rgComparativos.Rebind();
    //    }

    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
    //        vNbPrograma = ContextoUsuario.nbPrograma;

    //        if (!IsPostBack)
    //        {
    //            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
    //            SPE_OBTIENE_TABULADORES_Result vCuartiles = nTabuladores.ObtenerTabuladores().FirstOrDefault();
    //            XElement vXlmCuartiles = XElement.Parse(vCuartiles.XML_VW_CUARTILES);
    //            var vCuartilesTabulador = vXlmCuartiles.Elements("ITEM").Select(x => new E_CUARTILES
    //            {
    //                ID_CUARTIL = UtilXML.ValorAtributo<int>(x.Attribute("NB_VALOR")),
    //                NB_CUARTIL = UtilXML.ValorAtributo<string>(x.Attribute("NB_TEXTO")),
    //            }).ToList();
    //            cmbCuartilComparacion.DataSource = vCuartilesTabulador;
    //            cmbCuartilComparacion.DataTextField = "NB_CUARTIL";
    //            cmbCuartilComparacion.DataValueField = "ID_CUARTIL".ToString();
    //            cmbCuartilComparacion.DataBind();
    //            rcbMercadoTabuladorSueldos.DataSource = vCuartilesTabulador;
    //            rcbMercadoTabuladorSueldos.DataTextField = "NB_CUARTIL";
    //            rcbMercadoTabuladorSueldos.DataValueField = "ID_CUARTIL".ToString();
    //            rcbMercadoTabuladorSueldos.DataBind();
    //            rcbNivelMercado.DataSource = vCuartilesTabulador;
    //            rcbNivelMercado.DataTextField = "NB_CUARTIL";
    //            rcbNivelMercado.DataValueField = "ID_CUARTIL".ToString();
    //            rcbNivelMercado.DataBind();

    //            rgAnalisisDesviaciones.DataSource = new List<E_EMPLEADOS_GRAFICAS>();
    //            rgdEmpleados.DataBind();
                

    //            if (Request.QueryString["ID"] != null)
    //            {
    //                vIdTabulador = int.Parse((Request.QueryString["ID"]));
    //                TabuladoresNegocio nTabulador = new TabuladoresNegocio();
    //                var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
    //                txtClTabulador.Text = vTabulador.CL_TABULADOR;
    //                txtDsTabulador.Text = vTabulador.DS_TABULADOR;
    //                rdpCreacion.SelectedDate = vTabulador.FE_CREACION;
    //                rdpVigencia.SelectedDate = vTabulador.FE_VIGENCIA;
    //                vPrInflacional = vTabulador.PR_INFLACION;
    //                if (vTabulador.XML_VARIACION != null)
    //                {
    //                    XElement vXlmVariacion = XElement.Parse(vTabulador.XML_VARIACION);
    //                    foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
    //                        if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("green")))
    //                        {
    //                            vRangoVerde = UtilXML.ValorAtributo<int>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
    //                        }
    //                    foreach (XElement vXmlVaria in vXlmVariacion.Elements("Rango"))
    //                        if ((UtilXML.ValorAtributo<string>(vXmlVaria.Attribute("COLOR")).Equals("yellow")))
    //                        {
    //                            vRangoAmarillo = UtilXML.ValorAtributo<int>(vXmlVaria.Attribute("RANGO_SUPERIOR"));
    //                        }

    //                    agregaTooltipDesciaciones();

    //                }
    //                XElement vXlmCuartil = XElement.Parse(vTabulador.XML_CUARTILES);
    //                foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
    //                    if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INFLACIONAL")).Equals(1)))
    //                    {
    //                        vCuartilInflacional = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
    //                    }
    //                foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
    //                    if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_INCREMENTO")).Equals(1)))
    //                    {
    //                        vCuartilIncremento = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
    //                    }
    //                foreach (XElement vXmlCuartilSeleccionado in vXlmCuartil.Elements("ITEM"))
    //                    if ((UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("FG_SELECCIONADO_MERCADO")).Equals(1)))
    //                    {
    //                        vCuartilComparativo = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL"));
    //                        cmbCuartilComparacion.SelectedValue = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL")).ToString();
    //                        rcbMercadoTabuladorSueldos.SelectedValue = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL")).ToString();
    //                        rcbNivelMercado.SelectedValue = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL")).ToString();
    //                    }

    //                vLstEmpleados = new List<E_EMPLEADOS_GRAFICAS>();
    //                vEmpleadosDesviaciones = new List<E_EMPLEADOS_GRAFICAS>();
    //                vListaTabuladorSueldos = new List<E_PLANEACION_INCREMENTOS>();
    //                oLstPeriodos = new List<E_SELECCION_PERIODOS_DESEMPENO>();
    //                ContextoBono.oLstPeriodosBonos = new List<E_SELECCION_PERIODOS_DESEMPENO>();
                    
    //            }

    //            if (Request.QueryString["pOrigen"] == "TableroControl")
    //            {
    //                tbCpnsultas.Tabs[1].Visible = false;
    //                tbCpnsultas.Tabs[2].Visible = false;
    //                tbCpnsultas.Tabs[3].Visible = false;
    //                tbCpnsultas.Tabs[4].Visible = false;
    //                tbCpnsultas.Tabs[5].Visible = false;
    //                tbCpnsultas.Tabs[6].Visible = false;
    //                tbCpnsultas.Tabs[7].Visible = false;
    //                rtsTabuladorSueldos.Tabs[0].Visible = false;
    //                rtsTabuladorSueldos.Tabs[1].Visible = false;
    //                rtsTabuladorSueldos.Tabs[2].Selected = true;
    //                rmpTabuladorSueldos.PageViews[0].Visible = false;
    //                rmpTabuladorSueldos.PageViews[1].Visible = false;
    //                rmpTabuladorSueldos.PageViews[2].Selected = true;

    //                List<int> LstIdsSeleccionados = new List<int>();
    //                var vObtienePlaneacion = nTabuladores.ObtienePlaneacionIncrementos(ID_TABULADOR: vIdTabulador).ToList();
    //                foreach (var item in vObtienePlaneacion)
    //                {
    //                    LstIdsSeleccionados.Add(item.ID_TABULADOR_EMPLEADO);
    //                }
    //                ObtenerPlaneacionIncrementos();
    //                CargarDatosTabuladorSueldos(LstIdsSeleccionados);
                    
    //            }
    //        }

    //    }

    //    protected void CargarDatosTabuladorEmpleado(List<int> pIdsSeleccionados)
    //    {
    //        ActualizarLista(int.Parse(rcbNivelMercado.SelectedValue));
    //        var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.Where(w => pIdsSeleccionados.Contains(w.ID_TABULADOR_EMPLEADO)).ToList();
    //        foreach (var item in vEmpleadosTabuladorSeleccionados)
    //        {
    //            if (item.NO_NIVEL >= int.Parse(txtComienza.Text) & item.NO_NIVEL <= int.Parse(txtTermina.Text))
    //            {
    //                if (vEmpleadosDesviaciones.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
    //                {
    //                    vEmpleadosDesviaciones.Add(new E_EMPLEADOS_GRAFICAS { ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO, NB_EMPLEADO = item.NB_EMPLEADO, NO_NIVEL = item.NO_NIVEL, MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL, NB_PUESTO = item.NB_PUESTO, PR_DIFERENCIA = item.PR_DIFERENCIA, CL_PR_DIFERENCIA = vRangos(item.PR_DIFERENCIA) });
    //                }
    //            }
    //        }
    //        rgEmpleadosDesviasion.Rebind();
    //        CalculaPorcentajeNivel();
    //    }

    //    protected void CargarDatosTabuladorSueldos(List<int> pIdsSeleccionados)
    //    {
    //        ActualizarLista(int.Parse(rcbMercadoTabuladorSueldos.SelectedValue));
    //        var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.Where(w => pIdsSeleccionados.Contains(w.ID_TABULADOR_EMPLEADO)).ToList();
    //        foreach (var item in vEmpleadosTabuladorSeleccionados)
    //        {
    //            if (item.NO_NIVEL >= int.Parse(rntComienzaNivel.Text) & item.NO_NIVEL <= int.Parse(rntTerminaSueldo.Text))
    //            {
    //                if (vListaTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
    //                {
    //                    vListaTabuladorSueldos.Add(new E_PLANEACION_INCREMENTOS
    //                    {
    //                        NUM_ITEM = item.NUM_ITEM,
    //                        ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO,
    //                        NB_TABULADOR_NIVEL = item.NB_TABULADOR_NIVEL,
    //                        CL_PUESTO = item.CL_PUESTO,
    //                        NB_PUESTO = item.NB_PUESTO,
    //                        CL_DEPARTAMENTO = item.CL_DEPARTAMENTO,
    //                        NB_DEPARTAMENTO = item.NB_DEPARTAMENTO,
    //                        CL_EMPLEADO = item.CL_EMPLEADO,
    //                        NB_EMPLEADO = item.NB_EMPLEADO,
    //                        MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL,
    //                        MN_SUELDO_NUEVO = item.MN_SUELDO_NUEVO,
    //                        NO_NIVEL = item.NO_NIVEL,
    //                        XML_CATEGORIAS = item.XML_CATEGORIAS,
    //                        DIFERENCIA = item.DIFERENCIA,
    //                        PR_DIFERENCIA = item.PR_DIFERENCIA,
    //                        COLOR_DIFERENCIA = item.COLOR_DIFERENCIA,
    //                        ICONO = item.ICONO,
    //                        NO_VALUACION = item.NO_VALUACION,
    //                        lstCategorias = SeleccionCuartil(XElement.Parse(item.XML_CATEGORIAS), item.ID_TABULADOR_EMPLEADO)
                            
    //                    });
    //                }
    //            }
    //        }
    //        var vc = vListaTabuladorSueldos;
    //        rgEmpleadosTabuladorSueldos.Rebind();
    //        rgdComparacionInventarioPersonal.Rebind();
    //        //GridColumn V = rgdComparacionInventarioPersonal.Columns.FindByDataField("NO_NIVEL");
    //        //GridGroupByExpression expression1 = new GridGroupByExpression(V);
    //        //this.rgdComparacionInventarioPersonal.MasterTableView.GroupByExpressions.Add(expression1);
            
    //    }

    //    protected   List<E_CATEGORIA> SeleccionCuartil(XElement xlmCuartiles, int ID_TABULADOR_EMPLEADO)
    //    {
    //        List<E_CATEGORIA> lstCategoria = new List<E_CATEGORIA>();

    //        foreach (XElement vXmlSecuencia in xlmCuartiles.Elements("ITEM"))
    //        {
    //            lstCategoria.Add(new E_CATEGORIA
    //            {
    //                ID_TABULADOR_EMPLEADO = ID_TABULADOR_EMPLEADO,
    //                NO_CATEGORIA = UtilXML.ValorAtributo<int>(vXmlSecuencia.Attribute("NO_CATEGORIA")),
    //                MN_MINIMO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MINIMO")),
    //                MN_PRIMER_CUARTIL = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_PRIMER_CUARTIL")),
    //                MN_MEDIO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MEDIO")),
    //                MN_SEGUNDO_CUARTIL = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_SEGUNDO_CUARTIL")),
    //                MN_MAXIMO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MAXIMO"))
    //            });
    //        }

    //        foreach (var item in lstCategoria)
    //        {
    //            item.CANTIDAD = CalculaCantidadCuartil(int.Parse(rcbMercadoTabuladorSueldos.SelectedValue), item.MN_MINIMO, item.MN_PRIMER_CUARTIL, item.MN_MEDIO, item.MN_SEGUNDO_CUARTIL, item.MN_MAXIMO);
    //        }
            
    //        return lstCategoria;
    //    }

    //    protected decimal? CalculaCantidadCuartil(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
    //    {
    //        decimal? vCantidad = 0;
    //        switch (pMnSeleccionado)
    //        {
    //            case 1: vCantidad = pMnMinimo;
    //                break;
    //            case 2: vCantidad = pMnPrimerCuartil;
    //                break;
    //            case 3: vCantidad = pMnMedio;
    //                break;
    //            case 4: vCantidad = pMnSegundoCuartil;
    //                break;
    //            case 5: vCantidad = pMnMaximo;
    //                break;
    //        }
    //        return vCantidad;
    //    }

    //    protected DataTable CrearDataTable() {

    //        DataTable vDtPivot = new DataTable();

    //        vDtPivot.Columns.Add("ID_TABULADOR_EMPLEADO", typeof(int));
    //        vDtPivot.Columns.Add("NO_NIVEL", typeof(int));
    //        vDtPivot.Columns.Add("NO", typeof(string));
    //        vDtPivot.Columns.Add("NB_PUESTO", typeof(string));
    //        vDtPivot.Columns.Add("NB_DEPARTAMENTO", typeof(string));
    //        vDtPivot.Columns.Add("NB_EMPLEADO", typeof(string));
    //        vDtPivot.Columns.Add("MN_SUELDO_ORIGINAL", typeof(string));
    //        vDtPivot.Columns.Add("DIFERENCIA", typeof(string));
    //        vDtPivot.Columns.Add("PR_DIFERENCIA", typeof(string));
    //        vDtPivot.Columns.Add("NO_VALUACION", typeof(int));

    //        var vLstEmpleados = (from a in vListaTabuladorSueldos select new { a.ID_TABULADOR_EMPLEADO, a.lstCategorias, a.NO_NIVEL }).Distinct().OrderBy(o => o.NO_NIVEL);
    //        var vLstCategorias = (from a in vListaTabuladorSueldos
    //                                select new
    //                                {
    //                                    a.NUM_ITEM,
    //                                    a.ID_TABULADOR_EMPLEADO,
    //                                    a.NB_TABULADOR_NIVEL,
    //                                    a.NB_PUESTO,
    //                                    a.NB_DEPARTAMENTO,
    //                                    a.NB_EMPLEADO,
    //                                    a.MN_SUELDO_ORIGINAL,
    //                                    a.DIFERENCIA,
    //                                    a.PR_DIFERENCIA,
    //                                    a.NO_VALUACION,
    //                                    a.COLOR_DIFERENCIA,
    //                                    a.ICONO,
    //                                    a.NO_NIVEL
    //                                }).Distinct().OrderBy(t => t.NO_NIVEL);


          
    //            var vCategorias = vLstEmpleados.Select(s => s.lstCategorias).FirstOrDefault();

    //            if (vCategorias != null)
    //            {
    //                foreach (var item in vCategorias)
    //                {
    //                    vDtPivot.Columns.Add(item.NO_CATEGORIA.ToString() + "E");
    //                }
    //            }
            

    //        foreach (var vCate in vLstCategorias)
    //        {
    //            DataRow vDr = vDtPivot.NewRow();

    //            vDr["ID_TABULADOR_EMPLEADO"] = vCate.ID_TABULADOR_EMPLEADO;
    //            vDr["NO_NIVEL"] = vCate.NO_NIVEL;
    //            vDr["NO"] = vCate.NUM_ITEM;
    //            vDr["NB_PUESTO"] = vCate.NB_PUESTO;
    //            vDr["NB_DEPARTAMENTO"] = vCate.NB_DEPARTAMENTO;
    //            vDr["NB_EMPLEADO"] = vCate.NB_EMPLEADO;
    //            vDr["MN_SUELDO_ORIGINAL"] = String.Format("{0:C}",vCate.MN_SUELDO_ORIGINAL);
    //            vDr["DIFERENCIA"] = String.Format( "{0:C}" ,Math.Abs((decimal)vCate.DIFERENCIA));
    //            vDr["PR_DIFERENCIA"] = String.Format("{0:N2}", Math.Abs((decimal)vCate.PR_DIFERENCIA) > 100 ? 100 : Math.Abs((decimal)vCate.PR_DIFERENCIA)) + "%"
    //                + "<span style=\"border: 1px solid gray; border-radius: 5px; background:" + vCate.COLOR_DIFERENCIA + ";\">&nbsp;&nbsp;</span>&nbsp;<img src='/Assets/images/Icons/25/Arrow" + vCate.ICONO + ".png' />";
    //            vDr["NO_VALUACION"] = vCate.NO_VALUACION;

    //            var vResultado = vListaTabuladorSueldos.Where(t => t.ID_TABULADOR_EMPLEADO == vCate.ID_TABULADOR_EMPLEADO).FirstOrDefault();
    //                if (vResultado != null)
    //                {
    //                    foreach (var item in vResultado.lstCategorias)
    //                    {
    //                        vDr[item.NO_CATEGORIA.ToString() + "E"] = String.Format("{0:C}",item.CANTIDAD);
    //                    }
    //               }
    //            vDtPivot.Rows.Add(vDr);
    //        }
    //        return vDtPivot;
    //    }

    //    protected void ActualizarLista(int pCuartilComparativo) {
    //        foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncremento)
    //        {
    //            item.MN_MINIMO_CUARTIL = CalculaMinimo(pCuartilComparativo, item.MN_MINIMO_MINIMO, item.MN_MINIMO_PRIMER_CUARTIL, item.MN_MINIMO_MEDIO, item.MN_MINIMO_SEGUNDO_CUARTIL, item.MN_MINIMO_MAXIMO);
    //            item.MN_MAXIMO_CUARTIL = CalculaMaximo(pCuartilComparativo, item.MN_MAXIMO_MINIMO, item.MN_MAXIMO_PRIMER_CUARTIL, item.MN_MAXIMO_MEDIO, item.MN_MAXIMO_SEGUNDO_CUARTIL, item.MN_MAXIMO_MAXIMO);
    //            item.DIFERENCIA = CalculoDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
    //            if (item.MN_SUELDO_ORIGINAL > 0)
    //                item.PR_DIFERENCIA = (item.DIFERENCIA / item.MN_SUELDO_ORIGINAL) * 100;
    //            else item.PR_DIFERENCIA = 0;
    //            item.COLOR_DIFERENCIA = VariacionColor(item.PR_DIFERENCIA,item.MN_SUELDO_ORIGINAL);
    //            item.ICONO = ObtenerIconoDiferencia(item.PR_DIFERENCIA,item.MN_SUELDO_ORIGINAL);
    //        }
    //    }

    //    protected void ActualizaListaDesciaciones() {
    //        var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.Where(w => vEmpleadosDesviaciones.Select(s=> s.ID_TABULADOR_EMPLEADO).Contains( w.ID_TABULADOR_EMPLEADO)).ToList();
    //        vEmpleadosDesviaciones.Clear();
    //        foreach (var item in vEmpleadosTabuladorSeleccionados)
    //        {
    //            if (item.NO_NIVEL >= int.Parse(txtComienza.Text) & item.NO_NIVEL <= int.Parse(txtTermina.Text))
    //            {
    //                vEmpleadosDesviaciones.Add(new E_EMPLEADOS_GRAFICAS { ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO, NB_EMPLEADO = item.NB_EMPLEADO, NO_NIVEL = item.NO_NIVEL, MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL, NB_PUESTO = item.NB_PUESTO, PR_DIFERENCIA = item.PR_DIFERENCIA, CL_PR_DIFERENCIA = vRangos(item.PR_DIFERENCIA) });
    //            }
    //        }
    //        rgEmpleadosDesviasion.Rebind();
    //        CalculaPorcentajeNivel();
    //    }

    //    protected string vRangos(decimal? pPrDireferencia)
    //    {
    //        int? vRangoVerdeNegativo = vRangoVerde * -1;
    //        int? vRangoAmarilloNegativo = vRangoAmarillo * -1;
    //        string clave = null;

    //        if (pPrDireferencia <= vRangoVerde && pPrDireferencia >= vRangoVerdeNegativo)
    //            clave = "verde";
    //        else if (pPrDireferencia <= vRangoAmarillo && pPrDireferencia > vRangoVerde)
    //            clave = "amarilloPositivo";
    //        else if (pPrDireferencia <= vRangoVerdeNegativo && pPrDireferencia >= vRangoAmarilloNegativo)
    //            clave = "amarilloNegativo";
    //        else if (pPrDireferencia > vRangoAmarillo)
    //            clave = "rojoPositivo";
    //        else if (pPrDireferencia < vRangoAmarilloNegativo)
    //            clave = "rojoNegativo";
    //        return clave;
    //    }

    //    protected void CalculaPorcentajeNivel()
    //    {
    //        TabuladoresNegocio nNivelesTabulador = new TabuladoresNegocio();
    //        var vNiveles = vEmpleadosDesviaciones.Select(s => s.NO_NIVEL).ToList();
    //        List<E_NIVELES> vNivelesEmpleados = nNivelesTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador).Select(s => new E_NIVELES
    //        {
    //            CL_TABULADOR_NIVEL = s.CL_TABULADOR_NIVEL,
    //            ID_TABULADOR_NIVEL = s.ID_TABULADOR_NIVEL,
    //            NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
    //            NO_NIVEL = s.NO_NIVEL,
    //            NO_ORDEN = s.NO_ORDEN,
    //        }).ToList();
    //        vNivelesTabulador = vNivelesEmpleados.Where(w => vNiveles.Contains(w.NO_NIVEL)).ToList();

    //        decimal vEmpleadosVerdes = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("verde"));
    //        decimal vEmpleadosAmarillosNeg = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("amarilloNegativo"));
    //        decimal vEmpleadosAmarillosPos = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("amarilloPositivo"));
    //        decimal vEmpleadosRojoPos = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("rojoPositivo"));
    //        decimal vEmpleadosRojoNeg = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("rojoNegativo"));
    //        decimal vEmpleados = vEmpleadosDesviaciones.Count();

    //        if (vNivelesTabulador.Count > 0)
    //        {

    //        vNivelesTabulador.Add(new E_NIVELES
    //        {
    //            NB_TABULADOR_NIVEL = "NIVEL GENERAL",
    //            PR_VERDE = (vEmpleadosVerdes / vEmpleados) * 100,
    //            PR_AMARILLO_POS = (vEmpleadosAmarillosPos / vEmpleados) * 100,
    //            PR_AMARILLO_NEG = (vEmpleadosAmarillosNeg / vEmpleados) * 100,
    //            PR_ROJO_NEG = (vEmpleadosRojoNeg / vEmpleados) * 100,
    //            PR_ROJO_POS = (vEmpleadosRojoPos / vEmpleados) * 100
    //        });

           
    //            foreach (var item in vNivelesTabulador)
    //            {
    //                decimal vSumaVerde = 0;
    //                decimal vSumaAmarilloNeg = 0;
    //                decimal vSumaAmarilloPos = 0;
    //                decimal vSumaRojoNeg = 0;
    //                decimal vSumaRojoPos = 0;

    //                foreach (var iEmp in vEmpleadosDesviaciones)
    //                {
    //                    if (item.NO_NIVEL == iEmp.NO_NIVEL)
    //                    {
    //                        switch (iEmp.CL_PR_DIFERENCIA)
    //                        {
    //                            case "verde":
    //                                vSumaVerde = vSumaVerde + 1;
    //                                break;
    //                            case "amarilloPositivo":
    //                                vSumaAmarilloPos = vSumaAmarilloPos + 1;
    //                                break;
    //                            case "amarilloNegativo":
    //                                vSumaAmarilloNeg = vSumaAmarilloNeg + 1;
    //                                break;
    //                            case "rojoPositivo":
    //                                vSumaRojoPos = vSumaRojoPos + 1;
    //                                break;
    //                            case "rojoNegativo":
    //                                vSumaRojoNeg = vSumaRojoNeg + 1;
    //                                break;
    //                        }
    //                    }
    //                }
    //                decimal vTotalEmpleados = vEmpleadosDesviaciones.Count(c => c.NO_NIVEL == item.NO_NIVEL);
    //                if (vTotalEmpleados != 0)
    //                {
    //                    item.PR_VERDE = (vSumaVerde / vTotalEmpleados) * 100;
    //                    item.PR_AMARILLO_NEG = (vSumaAmarilloNeg / vTotalEmpleados) * 100;
    //                    item.PR_AMARILLO_POS = (vSumaAmarilloPos / vTotalEmpleados) * 100;
    //                    item.PR_ROJO_NEG = (vSumaRojoNeg / vTotalEmpleados) * 100;
    //                    item.PR_ROJO_POS = (vSumaRojoPos / vTotalEmpleados) * 100;
    //                }
    //            }
    //            rgAnalisisDesviaciones.DataSource = vNivelesTabulador;
    //            rgAnalisisDesviaciones.DataBind();
    //        }
    //    }

    //    protected void rgAnalisisDesviaciones_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        GridDataItem item = (GridDataItem)rgAnalisisDesviaciones.SelectedItems[0];
    //        int dataKey = int.Parse(item.GetDataKeyValue("ID_TABULADOR_NIVEL").ToString());
    //        var vNivelGrafica = vNivelesTabulador.Where(w => w.ID_TABULADOR_NIVEL == dataKey).ToList().FirstOrDefault();

    //        PieSeries vSerie = new PieSeries();

    //        //vSerie.SeriesItems.Add(vNivelGrafica.PR_VERDE, System.Drawing.Color.Green, "Sueldo dentro del nivel establecido por el tabulador (variación inferior al semáforo verde).", false, true);
    //        //vSerie.SeriesItems.Add(vNivelGrafica.PR_AMARILLO_POS, System.Drawing.Color.Yellow, "Sueldo superior al nivel establecido por el tabulador entre la variación del semáforo verde y semáforo amarillo.", false, true);
    //          //vSerie.SeriesItems.Add(vNivelGrafica.PR_AMARILLO_NEG, System.Drawing.Color.Gold, "Sueldo inferior al nivel establecido por el tabulador entre la variación del semáforo verde y semáforo amarillo.", false, true);
    //        //vSerie.SeriesItems.Add(vNivelGrafica.PR_ROJO_POS, System.Drawing.Color.OrangeRed, "Sueldo superior al nivel establecido por el tabulador en más del semáforo amarillo", false, true);
    //        //vSerie.SeriesItems.Add(vNivelGrafica.PR_ROJO_NEG, System.Drawing.Color.Red, "Sueldo inferior al nivel establecido por el tabulador en más del semáforo amarillo", false, true);

    //        vSerie.SeriesItems.Add(vNivelGrafica.PR_VERDE, System.Drawing.Color.Green, "Sueldo dentro del nivel establecido por el tabulador (variación inferior al " + vRangoVerde.ToString() + "%).", false, true);
    //        vSerie.SeriesItems.Add(vNivelGrafica.PR_AMARILLO_POS, System.Drawing.Color.Yellow, "Sueldo superior al nivel establecido por el tabulador entre la variación del " + vRangoVerde.ToString() + "% y el " + vRangoAmarillo.ToString() + "%.", false, true);
    //        vSerie.SeriesItems.Add(vNivelGrafica.PR_AMARILLO_NEG, System.Drawing.Color.Gold, "Sueldo inferior al nivel establecido por el tabulador entre la variación del " +vRangoVerde.ToString() +"% y el " + vRangoAmarillo.ToString() + "%.", false, true);
    //        vSerie.SeriesItems.Add(vNivelGrafica.PR_ROJO_POS, System.Drawing.Color.OrangeRed, "Sueldo superior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%.", false, true);
    //        vSerie.SeriesItems.Add(vNivelGrafica.PR_ROJO_NEG, System.Drawing.Color.Red, "Sueldo inferior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%.", false, true);

    //        vSerie.TooltipsAppearance.Visible = false;
    //        vSerie.TooltipsAppearance.DataFormatString = "{0:N2}";
    //        vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
    //        PieChartGraficaDesviaciones.PlotArea.Series.Add(vSerie);
    //    }

    //    protected void CargarDatosEmpleados(List<int> pIdsSeleccionados)
    //    {
    //        EmpleadoNegocio nEmpleados = new EmpleadoNegocio();
    //        var vEmpleadosObtenidos = nEmpleados.ObtenerEmpleados();
    //        var vEmpleadosSeleccionados = vEmpleadosObtenidos.Where(w => pIdsSeleccionados.Contains(w.M_EMPLEADO_ID_EMPLEADO)).ToList();
    //        List<E_EMPLEADOS_GRAFICAS> vListaEmpleados = new List<E_EMPLEADOS_GRAFICAS>();
    //        vListaEmpleados = vCargaLista(vEmpleadosSeleccionados);
    //        foreach (var item in vListaEmpleados)
    //        {
    //            if (vLstEmpleados.Where(w => w.ID_EMPLEADO == item.ID_EMPLEADO).Count() == 0)
    //            {

    //                vLstEmpleados.Add(item);
    //            }
    //        }
    //        rgdEmpleados.Rebind();
    //        PintarTabulador();
    //    }

    //    protected void CargarDatosTabuladores(List<int> pIdsSeleccionados)
    //    {
    //        if (pIdsSeleccionados != null)
    //        {
    //            vSeleccionadosTabuladores = new List<E_SELECCIONADOS>();
    //            foreach (int item in pIdsSeleccionados)
    //            {
    //                vSeleccionadosTabuladores.Add(new E_SELECCIONADOS { ID_SELECCIONADO = item });
    //            }
    //            var vXelements = vSeleccionadosTabuladores.Select(s =>
    //                                           new XElement("FILTRO",
    //                                               new XAttribute("CL_TIPO", "TABULADORES"),
    //                                           new XElement("TIPO",
    //                                               new XAttribute("ID_TABULADOR", s.ID_SELECCIONADO))
    //                            ));
    //            XElement vXmlIdTabuladores =
    //            new XElement("SELECCION", vXelements);

    //            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
    //            vEmpTabuladores = nTabuladores.ObtenieneEmpleadosTabulador(XML_SELECCIONADOS: vXmlIdTabuladores).Select(s =>
    //                                                                   new E_EMPLEADOS_GRAFICAS()
    //                                                                   {
    //                                                                       ID_TABULADOR_EMPLEADO = s.ID_TABULADOR_EMPLEADO,
    //                                                                       ID_EMPLEADO = s.M_EMPLEADO_ID_EMPLEADO,
    //                                                                       ID_TABULADOR = s.ID_TABULADOR,
    //                                                                       NB_TABULADOR = s.NB_TABULADOR,
    //                                                                       NB_EMPLEADO = s.NB_EMPLEADO,
    //                                                                       NB_PUESTO = s.NB_PUESTO,
    //                                                                       MN_SUELDO_ORIGINAL = s.MN_SUELDO_ORIGINAL,
    //                                                                       NO_NIVEL = s.NO_NIVEL,
    //                                                                       CL_TABULADOR = s.CL_TABULADOR
    //                                                                   }).ToList();
    //        }
    //    }

    //    public List<E_EMPLEADOS_GRAFICAS> vCargaLista(List<SPE_OBTIENE_EMPLEADOS_Result> pEmpleados)
    //    {
    //        List<E_EMPLEADOS_GRAFICAS> vLstEmpleados = new List<E_EMPLEADOS_GRAFICAS>();

    //        foreach (var item in pEmpleados)
    //        {
    //            vLstEmpleados.Add(new E_EMPLEADOS_GRAFICAS
    //            {
    //                ID_EMPLEADO = item.M_EMPLEADO_ID_EMPLEADO,
    //                NB_EMPLEADO = item.M_EMPLEADO_NB_EMPLEADO_COMPLETO,
    //                NB_PUESTO = item.M_PUESTO_NB_PUESTO,
    //            });
    //        }
    //        return vLstEmpleados;
    //    }

    //    protected void rgdTabuladorMaestro_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
    //        TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
    //        var vObtieneTabuladorMaestro = nTabuladores.ObtieneTabuladorMaestro(ID_TABULADOR: vIdTabulador).Select(s => new E_TABULADOR_MAESTRO()
    //        {
    //            ID_TABULADOR_MAESTRO = s.ID_TABULADOR_MAESTRO,
    //            ID_TABULADOR_NIVEL = s.ID_TABULADOR_NIVEL,
    //            NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
    //            NO_CATEGORIA = s.NO_CATEGORIA,
    //            NB_CATEGORIA = s.NB_CATEGORIA,
    //            PR_PROGRESION = s.PR_PROGRESION,
    //            MN_MINIMO = s.MN_MINIMO,
    //            MN_PRIMER_CUARTIL = s.MN_PRIMER_CUARTIL,
    //            MN_MEDIO = s.MN_MEDIO,
    //            MN_SEGUNDO_CUARTIL = s.MN_SEGUNDO_CUARTIL,
    //            MN_MAXIMO = s.MN_MAXIMO,
    //            MN_SIGUIENTE = calculaSiguiente(s, vPrInflacional, vCuartilInflacional)
    //        }).ToList();
    //        rgdTabuladorMaestro.DataSource = vObtieneTabuladorMaestro;
    //    }

    //    protected decimal? calculaSiguiente(SPE_OBTIENE_TABULADOR_MAESTRO_Result mnSeleccion, decimal pInflacional, int IdCuartil)
    //    {
    //        decimal? vMnSeleccion = 0;
    //        decimal? vSiguienteSueldo = 0;
    //        switch (IdCuartil)
    //        {
    //            case 1: vMnSeleccion = mnSeleccion.MN_MINIMO;
    //                break;
    //            case 2: vMnSeleccion = mnSeleccion.MN_PRIMER_CUARTIL;
    //                break;
    //            case 3: vMnSeleccion = mnSeleccion.MN_MEDIO;
    //                break;
    //            case 4: vMnSeleccion = mnSeleccion.MN_SEGUNDO_CUARTIL;
    //                break;
    //            case 5: vMnSeleccion = mnSeleccion.MN_MAXIMO;
    //                break;
    //        }
    //        vSiguienteSueldo = (vMnSeleccion * (pInflacional + 100) / 100);
    //        return vSiguienteSueldo;
    //    }

    //    protected void rgdMercadoSalarial_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
    //        TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
    //        var vTabuladorMercado = nTabuladores.ObtienePuestosTabulador(ID_TABULADOR: vIdTabulador);
    //        rgdMercadoSalarial.DataSource = vTabuladorMercado;
    //    }

    //    protected void rgdIncrementosPlaneados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
    //        TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
    //        vObtienePlaneacionIncremento = nTabuladores.ObtienePlaneacionIncrementos(ID_TABULADOR: vIdTabulador).Select(s => new E_PLANEACION_INCREMENTOS()
    //         {
    //             NUM_ITEM = (int)s.NUM_RENGLON,
    //             ID_TABULADOR_EMPLEADO = s.ID_TABULADOR_EMPLEADO,
    //             NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
    //             CL_PUESTO = s.CL_PUESTO,
    //             NB_PUESTO = s.NB_PUESTO,
    //             CL_EMPLEADO = s.CL_EMPLEADO,
    //             CL_DEPARTAMENTO = s.CL_DEPARTAMENTO,
    //             NB_DEPARTAMENTO = s.NB_DEPARTAMENTO,
    //             NB_EMPLEADO = s.NOMBRE,
    //             MN_MINIMO_MINIMO = s.MN_MINIMO_MINIMO,
    //             MN_MAXIMO_MINIMO = s.MN_MAXIMO_MINIMO,
    //             MN_MINIMO_PRIMER_CUARTIL = s.MN_MINIMO_PRIMER_CUARTIL,
    //             MN_MAXIMO_PRIMER_CUARTIL = s.MN_MAXIMO_PRIMER_CUARTIL,
    //             MN_MINIMO_MEDIO = s.MN_MINIMO_MEDIO,
    //             MN_MAXIMO_MEDIO = s.MN_MAXIMO_MEDIO,
    //             MN_MINIMO_SEGUNDO_CUARTIL = s.MN_MINIMO_SEGUNDO_CUARTIL,
    //             MN_MAXIMO_SEGUNDO_CUARTIL = s.MN_MAXIMO_SEGUNDO_CUARTIL,
    //             MN_MAXIMO_MAXIMO = s.MN_MAXIMO_MAXIMO,
    //             MN_MINIMO_MAXIMO = s.MN_MINIMO_MAXIMO,
    //             MN_SUELDO_ORIGINAL = s.MN_SUELDO_ORIGINAL,
    //             MN_SUELDO_NUEVO = s.MN_SUELDO_NUEVO,
    //             MN_MINIMO = s.MN_MINIMO,
    //             MN_MAXIMO = s.MN_MAXIMO,
    //             INCREMENTO = Math.Abs(s.MN_SUELDO_NUEVO - s.MN_SUELDO_ORIGINAL),
    //             NO_NIVEL = s.NO_NIVEL,
    //             XML_CATEGORIAS = s.XML_CATEGORIA,
    //             CUARTIL_SELECCIONADO = vCuartilComparativo,
    //             NO_VALUACION = s.NO_VALUACION
    //         }).ToList();
    //        rgdIncrementosPlaneados.DataSource = vObtienePlaneacionIncremento;
    //        foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncremento)
    //        {
    //            if (item.MN_SUELDO_ORIGINAL != 0)
    //            item.PR_INCREMENTO = (item.INCREMENTO / item.MN_SUELDO_ORIGINAL) * 100;
    //        }
    //    }

    //    protected void rgdComparacionInventarioMercado_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
    //        foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncremento)
    //        {
    //            item.DIFERENCIA_MERCADO = CalculoDiferencia(item.MN_MINIMO, item.MN_MAXIMO, item.MN_SUELDO_ORIGINAL);

    //            if (item.MN_SUELDO_ORIGINAL != 0)
    //                item.PR_DIFERENCIA_MERCADO = (item.DIFERENCIA_MERCADO / item.MN_SUELDO_ORIGINAL) * 100;
    //            else item.PR_DIFERENCIA_MERCADO = 0;
                
    //            item.COLOR_DIFERENCIA_MERCADO = VariacionColor(item.PR_DIFERENCIA_MERCADO,item.MN_SUELDO_ORIGINAL);
    //            item.ICONO_MERCADO = ObtenerIconoDiferencia(item.PR_DIFERENCIA_MERCADO,item.MN_SUELDO_ORIGINAL);
    //        }
    //        rgdComparacionInventarioMercado.DataSource = vObtienePlaneacionIncremento;
    //    }

    //    protected decimal? CalculoDiferencia(decimal? pMnMinimo, decimal? pMnMaximo, decimal? pMnSueldo)
    //    {
    //        decimal? vMnDivisor = 0;
    //        if (pMnSueldo < pMnMinimo)
    //            vMnDivisor = pMnMinimo;
    //        else
    //            if (pMnSueldo > pMnMaximo)
    //                vMnDivisor = pMnMaximo;
    //            else
    //                vMnDivisor = pMnSueldo;
    //        return vMnDivisor = pMnSueldo - vMnDivisor;
    //    }

    //    protected decimal? CalculaMinimo(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
    //    {
    //        decimal? vMinimo = 0;
    //        switch (pMnSeleccionado)
    //        {
    //            case 1: vMinimo = pMnMinimo;
    //                break;
    //            case 2: vMinimo = pMnPrimerCuartil;
    //                break;
    //            case 3: vMinimo = pMnMedio;
    //                break;
    //            case 4: vMinimo = pMnSegundoCuartil;
    //                break;
    //            case 5: vMinimo = pMnMaximo;
    //                break;
    //        }
    //        return vMinimo;
    //    }

    //    protected decimal? CalculaMaximo(int pMnSeleccionado, decimal? pMnMinimo, decimal? pMnPrimerCuartil, decimal? pMnMedio, decimal? pMnSegundoCuartil, decimal? pMnMaximo)
    //    {
    //        decimal? vMaximo = 0;
    //        switch (pMnSeleccionado)
    //        {
    //            case 1: vMaximo = pMnMinimo;
    //                break;
    //            case 2: vMaximo = pMnPrimerCuartil;
    //                break;
    //            case 3: vMaximo = pMnMedio;
    //                break;
    //            case 4: vMaximo = pMnSegundoCuartil;
    //                break;
    //            case 5: vMaximo = pMnMaximo;
    //                break;
    //        }
    //        return vMaximo;
    //    }

    //    protected string ObtenerIconoDiferencia(decimal? pPrDiferencia, decimal? pMnSueldo)
    //    {
    //        //return pPrDiferencia < 0 ? "Down" : pPrDiferencia > 0 ? "Up" : "Equal";
    //        string vImagen = null;
    //        if (pPrDiferencia < 0 & pMnSueldo != 0)
    //            vImagen = "Down";
    //        else if (pPrDiferencia > 0 & pMnSueldo != 0)
    //            vImagen = "Up";
    //        else if (pPrDiferencia == 0 & pMnSueldo == 0)
    //            vImagen = "Delete";
    //        else vImagen = "Equal";

    //        return vImagen;
    //    }

    //    protected string VariacionColor(decimal? pPrDireferencia, decimal? pMnSueldo)
    //    {
    //        string vColor;
    //        if (pPrDireferencia == null)
    //            pPrDireferencia = 0;

    //        decimal vPorcentaje = Math.Abs((decimal)pPrDireferencia);

    //        if (vPorcentaje >= 0 && vPorcentaje <= vRangoVerde & pMnSueldo > 0)
    //            vColor = "green";
    //        else if (vPorcentaje > vRangoVerde && vPorcentaje <= vRangoAmarillo & pMnSueldo > 0)
    //            vColor = "yellow";
    //        else if(pPrDireferencia == 0 & pMnSueldo==0)
    //            vColor = "gray";
    //        else
    //            vColor = "red";
    //        return vColor;
    //    }

    //    protected void PintarTabulador()
    //    {
    //        var vEmpleadosSimilares = vLstEmpleados.Select(s => s.ID_EMPLEADO).ToList();
    //        vEmpleados = vEmpTabuladores.Where(w => vEmpleadosSimilares.Contains(w.ID_EMPLEADO)).ToList();
    //        if (vEmpleados != null)
    //        {
    //            ScatterChartGraficaAnalisis.PlotArea.Series.Clear();

    //            foreach (var iTab in vSeleccionadosTabuladores)
    //            {
    //                ScatterSeries vPrimerScatterSeries = new ScatterSeries();
    //                DataTable dt = new DataTable();
    //                dt.Columns.Add("xValues", typeof(double));
    //                dt.Columns.Add("yValues", typeof(double));
    //                int vComienza = int.Parse(rnComienza.Text);
    //                int vTermina = int.Parse(rnTermina.Text);


    //                foreach (var iEmp in vEmpleados)
    //                {
    //                    if (iTab.ID_SELECCIONADO == iEmp.ID_TABULADOR)
    //                    {
    //                        if (iEmp.NO_NIVEL >= vComienza && iEmp.NO_NIVEL <= vTermina)
    //                        {
    //                            vPrimerScatterSeries.SeriesItems.Add(iEmp.NO_NIVEL, iEmp.MN_SUELDO_ORIGINAL);
    //                            vPrimerScatterSeries.LabelsAppearance.Visible = false;
    //                            vPrimerScatterSeries.Name = "Sueldos" + " " + iEmp.CL_TABULADOR.ToString();
    //                            vPrimerScatterSeries.TooltipsAppearance.DataFormatString = "{1}";
    //                            dt.Rows.Add(iEmp.NO_NIVEL, iEmp.MN_SUELDO_ORIGINAL);
    //                        }
    //                    }
    //                }
    //                ScatterChartGraficaAnalisis.PlotArea.Series.Add(vPrimerScatterSeries);
    //                ScatterChartGraficaAnalisis.PlotArea.YAxis.LabelsAppearance.DataFormatString = "{0:C}";
    //                RegressionType selectedRegressionType = RegressionType.Linear;
    //                CreateRegressionModel.Plot(ScatterChartGraficaAnalisis, dt, "xValues", "yValues", selectedRegressionType);

    //                AddCuartil(iTab.ID_SELECCIONADO, vComienza, vTermina);
    //            }
    //        }
    //    }

    //    protected void AddCuartil(int pIdTabulador, int pComienza, int pTermina)
    //    {
    //        TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
    //        List<E_TABULADOR_MAESTRO> vTabuladores = nTabuladores.ObtieneTabuladorMaestro(ID_TABULADOR: pIdTabulador).Select(s =>
    //                                          new E_TABULADOR_MAESTRO()
    //                                          {
    //                                              NO_CATEGORIA = s.NO_CATEGORIA,
    //                                              MN_MINIMO = s.MN_MINIMO,
    //                                              MN_PRIMER_CUARTIL = s.MN_PRIMER_CUARTIL,
    //                                              MN_MEDIO = s.MN_MEDIO,
    //                                              MN_SEGUNDO_CUARTIL = s.MN_SEGUNDO_CUARTIL,
    //                                              MN_MAXIMO = s.MN_MAXIMO,
    //                                              NO_NIVEL = s.NO_NIVEL,
    //                                              CL_TABULADOR = s.CL_TABULADOR
    //                                          }).ToList();

    //        List<E_CUARTIL_COMPARACION> vCuartiles = Cuartil(vTabuladores, int.Parse(cmbCuartilComparacion.SelectedValue));
    //        var vIdCategorias = vTabuladores.GroupBy(test => test.NO_CATEGORIA).Select(grp => grp.First()).ToList();

    //        foreach (var item in vIdCategorias)
    //        {
    //            ScatterLineSeries vCuartilComparativo = new ScatterLineSeries();
    //            foreach (var itemC in vCuartiles)
    //            {
    //                if (item.NO_CATEGORIA == itemC.NO_CATEGORIA)
    //                {
    //                    if (itemC.NO_NIVEL >= pComienza && itemC.NO_NIVEL <= pTermina)
    //                    {
    //                        vCuartilComparativo.SeriesItems.Add(itemC.NO_NIVEL, itemC.MN_CATEGORIA);
    //                        vCuartilComparativo.LabelsAppearance.Visible = false;
    //                        vCuartilComparativo.MarkersAppearance.Visible = false;
    //                        vCuartilComparativo.Name = itemC.NO_CATEGORIA.ToString();
    //                    }
    //                }
    //            }
    //            char nb = (char)(item.NO_CATEGORIA + 64);
    //            vCuartilComparativo.Name = nb.ToString() + " " + item.CL_TABULADOR;
    //            ScatterChartGraficaAnalisis.PlotArea.Series.Add(vCuartilComparativo);
    //        }
    //    }

    //    private List<E_CUARTIL_COMPARACION> Cuartil(List<E_TABULADOR_MAESTRO> pTabuladores, int IdCuartil)
    //    {
    //        List<E_CUARTIL_COMPARACION> vMnSeleccion = new List<E_CUARTIL_COMPARACION>();
    //        switch (IdCuartil)
    //        {
    //            case 1: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_MINIMO, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
    //                break;
    //            case 2: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_PRIMER_CUARTIL, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
    //                break;
    //            case 3: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_MEDIO, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
    //                break;
    //            case 4: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_SEGUNDO_CUARTIL, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
    //                break;
    //            case 5: vMnSeleccion = pTabuladores.Select(s => new E_CUARTIL_COMPARACION { NO_NIVEL = s.NO_NIVEL, MN_CATEGORIA = s.MN_MAXIMO, NO_CATEGORIA = s.NO_CATEGORIA }).ToList();
    //                break;
    //        }
    //        return vMnSeleccion;
    //    }

    //    protected void ramConsultas_AjaxRequest(object sender, AjaxRequestEventArgs e)
    //    {
    //        E_ARREGLOS vSeleccion = new E_ARREGLOS();
    //        E_SELECTOR vSeleccionBono = new E_SELECTOR();
    //        string pParameter = e.Argument;

    //        if (pParameter != null)
    //        {
    //            vSeleccion = JsonConvert.DeserializeObject<E_ARREGLOS>(pParameter);
    //            vSeleccionBono = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);
    //        }

    //        if (vSeleccion.clTipo == "EMPLEADO")
    //        {
    //            vTabuladores = vSeleccion.arrIdTabulador;
    //            CargarDatosTabuladores(vTabuladores);
    //            CargarDatosEmpleados(vSeleccion.arrEmpleados);
    //        }
    //        if (vSeleccion.clTipo == "TABULADOR_EMPLEADO")
    //        {
    //            CargarDatosTabuladorEmpleado(vSeleccion.arrEmpleados);
    //        }
    //        if (vSeleccion.clTipo == "TABULADOR_SUELDOS")
    //        {
    //            CargarDatosTabuladorSueldos(vSeleccion.arrEmpleados);
    //        }

    //        if (vSeleccionBono.clTipo == "PERIODODESEMPENO")
    //        {
    //            AgregarPeriodos(vSeleccionBono.oSeleccion.ToString());
    //        }
    //    }

    //    //protected void rttmSecuancias_AjaxUpdate(object sender, ToolTipUpdateEventArgs e)
    //    //{
    //    //    this.UpdateToolTip(e.Value, e.UpdatePanel);
    //    //}

    //    //private void UpdateToolTip(string elementID, UpdatePanel panel)
    //    //{
    //    //    Control ctrl = Page.LoadControl("TooltipPlaneacionIncrementos.ascx");
    //    //    ctrl.ID = "UcTooltipPlaneacionIncrementos1";
    //    //    panel.ContentTemplateContainer.Controls.Add(ctrl);
    //    //    TooltipPlaneacionIncrementos planeacion = (TooltipPlaneacionIncrementos)ctrl;
    //    //    var vSecuancias = vObtienePlaneacionIncremento.Where(w => w.ID_TABULADOR_EMPLEADO == int.Parse(elementID)).ToList();
    //    //    planeacion.vSecuanciasEmpleado = vSecuancias;
    //    //}

    //    //protected void rgdComparacionInventarioPersonal_ItemDataBound(object sender, GridItemEventArgs e)
    //    //{
    //    //    if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
    //    //    {
    //    //        Control target = e.Item.FindControl("gbcMinimo");
    //    //        Control targetMaximo = e.Item.FindControl("gbcMaximo");

    //    //        if (!Object.Equals(target, null))
    //    //        {
    //    //            if (!Object.Equals(this.rttmSecuancias, null))
    //    //            {
    //    //                this.rttmSecuancias.TargetControls.Add(target.ClientID, (e.Item as GridDataItem).GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString(), true);
    //    //            }
    //    //        }

    //    //        if (!Object.Equals(targetMaximo, null))
    //    //        {
    //    //            if (!Object.Equals(this.rttmSecuancias, null))
    //    //            {
    //    //                this.rttmSecuancias.TargetControls.Add(targetMaximo.ClientID, (e.Item as GridDataItem).GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString(), true);
    //    //            }
    //    //        }
    //    //    }
    //    //}

    //    protected void rgdComparacionInventarioPersonal_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
    //        //rgdComparacionInventarioPersonal.DataSource = vObtienePlaneacionIncremento;
    //        rgdComparacionInventarioPersonal.DataSource = CrearDataTable();

    //        //if (vListaTabuladorSueldos.Count > 0)
    //        //{
    //        //    GridColumn V = rgdComparacionInventarioPersonal.Columns.FindByUniqueName("NO_NIVEL");
    //        //    GridGroupByExpression expression1 = new GridGroupByExpression(V);
    //        //    this.rgdComparacionInventarioPersonal.MasterTableView.GroupByExpressions.Add(expression1);
    //        //}


    //        GridGroupByField field = new GridGroupByField();
    //        field.FieldName = "NO_NIVEL";
    //        field.HeaderText = "Nivel";
    //        GridGroupByExpression ex = new GridGroupByExpression();
    //        ex.GroupByFields.Add(field);
    //        ex.SelectFields.Add(field);
    //        rgdComparacionInventarioPersonal.MasterTableView.GroupByExpressions.Add(ex);
           
    //    }

    //    protected void rgdComparacionInventarioPersonal_ItemCreated(object sender, GridItemEventArgs e)
    //    {
    //        if (e.Item is GridDataItem)
    //        {
    //            GridDataItem gridItem = e.Item as GridDataItem;

    //            int vIdCategoria = int.Parse(gridItem.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString());
    //            string vClEmpleado = vListaTabuladorSueldos.Where(t => t.ID_TABULADOR_EMPLEADO == vIdCategoria).FirstOrDefault().CL_EMPLEADO;

    //            gridItem["NB_EMPLEADO"].ToolTip = vClEmpleado;
    //        }
    //    }

    //    protected void rgdComparacionInventarioPersonal_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
    //    {
    //        switch (e.Column.UniqueName)
    //        {
    //            case "ID_TABULADOR_EMPLEADO":
    //                ConfigurarColumna(e.Column, 10, "Empleado", false, false, true,false);
    //                break;
    //            case "NO_NIVEL":
    //                ConfigurarColumna(e.Column, 10, "No. Nivel", false, false, true, false);
    //                break;
    //            case "NO":
    //                ConfigurarColumna(e.Column, 40, "No.", true, false, false,false);
    //                break;
    //            case "NB_PUESTO":
    //                ConfigurarColumna(e.Column, 170, "Puesto", true, false, true,false);
    //                break;
    //            case "NB_DEPARTAMENTO":
    //                ConfigurarColumna(e.Column, 150, "Área", true, false, true, false);
    //                break;
    //            case "NB_EMPLEADO":
    //                ConfigurarColumna(e.Column, 250, "Nombre completo", true, false, true,false);
    //                break;
    //            case "MN_SUELDO_ORIGINAL":
    //                ConfigurarColumna(e.Column, 100, "Sueldo", true, false, true,true);
    //                break;
    //            case "DIFERENCIA":
    //                ConfigurarColumna(e.Column, 100, "Diferencia", true, false, true,true);
    //                break;
    //            case "PR_DIFERENCIA":
    //                ConfigurarColumna(e.Column, 120, "Porcentaje", true, false, true,true);
    //                break;
    //            case "NO_VALUACION":
    //                ConfigurarColumna(e.Column, 80, "Valuación", true, false, true,true);
    //                break;
    //            case "column":
    //                break;
    //            case "ExpandColumn": break;
    //            default:
    //                ConfigurarColumna(e.Column, 100, "", true, true, false,true);
    //                break;
    //        }

    //    }

    //    private void ConfigurarColumna(GridColumn pColumna, int pWidth, string pEncabezado, bool pVisible,bool pGenerarEncabezado, bool pFiltrarColumna, bool pAlinear)
    //    {
    //        if (pGenerarEncabezado)
    //        {
    //            pEncabezado = GeneraEncabezado(pColumna);
    //            pColumna.ColumnGroupName = "TABMEDIO";
    //        }

    //        pColumna.HeaderStyle.Width = Unit.Pixel(pWidth);
    //        pColumna.HeaderText = pEncabezado;
    //        pColumna.Visible = pVisible;        

    //        if (pAlinear)
    //        {
    //            pColumna.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Right;
    //        }

    //        if (pFiltrarColumna & pVisible)
    //        {
                
    //            if (pWidth <= 60)
    //            {
    //                (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
    //            }
    //            else if (pEncabezado == "Valuación")
    //            {
    //                (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 40);
    //            }
    //            else
    //            {
    //                (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 70);
    //            }
    //        }
    //        else
    //        {
    //            (pColumna as GridBoundColumn).AllowFiltering = false;
    //        }
    //    }

    //    private string GeneraEncabezado(GridColumn pColumna)
    //    {
    //        int vResultado;
    //        string vEncabezado = "";
    //        string vEmpleado = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

    //        if (int.TryParse(vEmpleado, out vResultado))
    //        {
    //            //var vDatosEmpleado = vListaTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == vResultado).FirstOrDefault();
    //            var vDatosEmpleado = vListaTabuladorSueldos.Select(s => s.lstCategorias.Where(w => w.NO_CATEGORIA == vResultado)).FirstOrDefault();

    //            if (vDatosEmpleado != null)
    //            {
    //                vEncabezado = "<div style=\"text-align:center;\"> " + (char)(vDatosEmpleado.Select( s=> s.NO_CATEGORIA).FirstOrDefault() + 64) + "</div>";
    //            }
    //        }
    //        return vEncabezado;
    //    }

    //    protected void rgdEmpleados_ItemCommand(object sender, GridCommandEventArgs e)
    //    {
    //        if (e.CommandName == "Delete")
    //        {
    //            GridDataItem item = e.Item as GridDataItem;
    //            EliminarEmpleado(int.Parse(item.GetDataKeyValue("ID_EMPLEADO").ToString()));
    //        }
    //    }

    //    protected void EliminarEmpleado(int pIdEmpleado)
    //    {
    //        E_EMPLEADOS_GRAFICAS vEmpleado = vLstEmpleados.Where(w => w.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

    //        if (vEmpleado != null)
    //        {
    //            vLstEmpleados.Remove(vEmpleado);
    //        }
    //        PintarTabulador();
    //    }

    //    protected void EliminarEmpleadoTabulador(int pIdEmpleado)
    //    {
    //        E_EMPLEADOS_GRAFICAS vEmpleado = vEmpleadosDesviaciones.Where(w => w.ID_TABULADOR_EMPLEADO == pIdEmpleado).FirstOrDefault();

    //        if (vEmpleado != null)
    //        {
    //            vEmpleadosDesviaciones.Remove(vEmpleado);
    //        }
    //        CalculaPorcentajeNivel();
    //    }

    //    protected void EliminarEmpleadoSueldos(int pIdTabuladorEmpleado) {

    //        E_PLANEACION_INCREMENTOS vEmpleado = vListaTabuladorSueldos.Where(w => w.ID_TABULADOR_EMPLEADO == pIdTabuladorEmpleado).FirstOrDefault();

    //        if (vEmpleado != null)
    //        {
    //            vListaTabuladorSueldos.Remove(vEmpleado);
    //        }
            
    //        rgdComparacionInventarioPersonal.Rebind();
    //    }
        
    //    protected void rgdEmpleados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
    //        rgdEmpleados.DataSource = vLstEmpleados;
    //    }

    //    protected void rgEmpleadosDesviasion_ItemCommand(object sender, GridCommandEventArgs e)
    //    {
    //        if (e.CommandName == "Delete")
    //        {
    //            GridDataItem item = e.Item as GridDataItem;
    //            EliminarEmpleadoTabulador(int.Parse(item.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString()));
    //        }
    //    }

    //    protected void rgEmpleadosDesviasion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
    //        rgEmpleadosDesviasion.DataSource = vEmpleadosDesviaciones;
    //    }

    //    protected void rgEmpleadosTabuladorSueldos_ItemCommand(object sender, GridCommandEventArgs e)
    //    {
    //        if (e.CommandName == "Delete")
    //        {
    //            GridDataItem item = e.Item as GridDataItem;
    //            EliminarEmpleadoSueldos(int.Parse(item.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString()));
    //        }
    //    }

    //    protected void rgEmpleadosTabuladorSueldos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
    //        rgEmpleadosTabuladorSueldos.DataSource = vListaTabuladorSueldos;
    //    }

    //    protected void rcbNivelMercado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    //    {
    //       ActualizarLista(int.Parse(e.Value));
    //       ActualizaListaDesciaciones();
    //    }

    //    protected void rgComparativos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
    //        rgComparativos.DataSource = oLstPeriodos;
    //    }

    //    protected void rgComparativos_DeleteCommand(object sender, GridCommandEventArgs e)
    //    {
    //        if (e.Item is GridEditableItem)
    //        {
    //            GridEditableItem item = (GridEditableItem)e.Item;

    //            int? vIdPeriodo = int.Parse(item.GetDataKeyValue("idPeriodo").ToString());

    //            if (vIdPeriodo != null)
    //            {
    //                var vItem = oLstPeriodos.First(x => x.idPeriodo == vIdPeriodo);
    //                oLstPeriodos.Remove(vItem);
    //                var vItemContexto = ContextoBono.oLstPeriodosBonos.First(x => x.idPeriodo == vIdPeriodo);
    //                ContextoBono.oLstPeriodosBonos.Remove(vItemContexto);
    //                rgComparativos.Rebind();
    //            }
    //            }
    //    }

    //    protected void grdCodigoColores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    //    {
           
    //        List<E_CODIGO_COLORES> vCodigoColores = new List<E_CODIGO_COLORES>();
    //        vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "green", DESCRIPCION = "Sueldo dentro del nivel establecido por el tabulador (variación inferior al "+vRangoVerde.ToString()+"%)." });
    //        vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "yellow", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador entre el " + vRangoVerde.ToString() + "% y " + vRangoAmarillo.ToString() + "%." });
    //        vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "red", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%." });
    //        grdCodigoColores.DataSource = vCodigoColores;
    //    }

    //    //protected void rgdComparacionInventarioPersonal_ItemDataBound(object sender, GridItemEventArgs e)
    //    //{
    //    //    if (e.Item is GridDataItem)
    //    //    {
    //    //        GridDataItem dataItem = e.Item as GridDataItem;

    //    //        vActual = int.Parse(dataItem.GetDataKeyValue("NO_NIVEL").ToString());
               
    //    //        if (vActual == vAnterior){
                   
    //    //                dataItem.CssClass = "RadGrid1Class";
    //    //        }
    //    //            else dataItem.CssClass = "RadGrid2Class";
    //    //    }
    //    //    vAnterior = vActual;
    //    //}

    //    private void agregaTooltipDesciaciones()
    //    {
    //        rgAnalisisDesviaciones.Columns[1].HeaderTooltip = "Sueldo dentro del nivel establecido por el tabulador (variación inferior al " + vRangoVerde.ToString() + "%).";
    //        rgAnalisisDesviaciones.Columns[2].HeaderTooltip = "Sueldo superior al nivel establecido por el tabulador entre la variación del " + vRangoVerde.ToString() + "% y el " + vRangoAmarillo.ToString() + "%.";
    //        rgAnalisisDesviaciones.Columns[3].HeaderTooltip = "Sueldo inferior al nivel establecido por el tabulador entre la variación del " + vRangoVerde.ToString() + "% y el " + vRangoAmarillo.ToString() + "%.";
    //        rgAnalisisDesviaciones.Columns[4].HeaderTooltip = "Sueldo superior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%.";
    //        rgAnalisisDesviaciones.Columns[5].HeaderTooltip = "Sueldo inferior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%.";

    //    }

    //    protected void rgEmpleadosTabuladorSueldos_ItemDataBound(object sender, GridItemEventArgs e)
    //    {
    //        if (e.Item is GridPagerItem)
    //        {
    //            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

    //            PageSizeCombo.Items.Clear();
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
    //            PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
    //            PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
    //            PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
    //            PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
    //            PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgEmpleadosTabuladorSueldos.MasterTableView.ClientID);
    //            PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
    //        }
			
    //    }

    //    protected void rgdEmpleados_ItemDataBound(object sender, GridItemEventArgs e)
    //    {
    //        if (e.Item is GridPagerItem)
    //        {
    //            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

    //            PageSizeCombo.Items.Clear();
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
    //            PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgdEmpleados.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
    //            PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgdEmpleados.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
    //            PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgdEmpleados.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
    //            PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgdEmpleados.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
    //            PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgdEmpleados.MasterTableView.ClientID);
    //            PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
    //        }
    //    }

    //    protected void rgEmpleadosDesviasion_ItemDataBound(object sender, GridItemEventArgs e)
    //    {
    //        if (e.Item is GridPagerItem)
    //        {
    //            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

    //            PageSizeCombo.Items.Clear();
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
    //            PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
    //            PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
    //            PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
    //            PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
    //            PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
    //            PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
    //        }
    //    }

       

    //    protected void rgAnalisisDesviaciones_ItemDataBound(object sender, GridItemEventArgs e)
    //    {
    //        if (e.Item is GridPagerItem)
    //        {
    //            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

    //            PageSizeCombo.Items.Clear();
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
    //            PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
    //            PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
    //            PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
    //            PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
    //            PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
    //            PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
    //        }

    //    }

    //    protected void rgdTabuladorMaestro_ItemDataBound(object sender, GridItemEventArgs e)
    //    {
    //        if (e.Item is GridPagerItem)
    //        {
    //            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

    //            PageSizeCombo.Items.Clear();
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
    //            PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
    //            PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
    //            PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
    //            PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
    //            PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgdTabuladorMaestro.MasterTableView.ClientID);
    //            PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
    //        }
    //    }

    //    protected void rgdMercadoSalarial_ItemDataBound(object sender, GridItemEventArgs e)
    //    {
    //        if (e.Item is GridPagerItem)
    //        {
    //            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

    //            PageSizeCombo.Items.Clear();
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
    //            PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgdMercadoSalarial.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
    //            PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgdMercadoSalarial.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
    //            PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgdMercadoSalarial.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
    //            PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgdMercadoSalarial.MasterTableView.ClientID);
    //            PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
    //            PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgdMercadoSalarial.MasterTableView.ClientID);
    //            PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
    //        }
    //    }
    //}

    
    //public class E_ARREGLOS
    //{
    //    public string clTipo { set; get; }
    //    public List<int?> arrEmpleados { set; get; }
    //    public List<int> arrIdTabulador { set; get; }

    }
}