using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.MetodologiaCompensacion;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.MetodologiaCompensacion;
using SIGE.Negocio.Utilerias;
using SIGE.WebApp.Comunes;
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
    public partial class ConsultaAnalisisDesviaciones : System.Web.UI.Page
    {

        #region Variables

        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int? vIdRol;

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

        private List<E_SELECCIONADOS> vSeleccionadosTabuladores
        {
            get { return (List<E_SELECCIONADOS>)ViewState["vs_vSeleccionadosTabuladores"]; }
            set { ViewState["vs_vSeleccionadosTabuladores"] = value; }
        }

        private List<E_EMPLEADOS_GRAFICAS> vEmpleados
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vEmpleados"]; }
            set { ViewState["vs_vEmpleados"] = value; }
        }

        private List<E_EMPLEADOS_GRAFICAS> vEmpTabuladores
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vEmpTabuladores"]; }
            set { ViewState["vs_vEmpTabuladores"] = value; }
        }

        private List<int> vTabuladores
        {
            get { return (List<int>)ViewState["vs_vTabuladores"]; }
            set { ViewState["vs_vTabuladores"] = value; }
        }

        private List<E_NIVELES> vNivelesTabulador
        {
            get { return (List<E_NIVELES>)ViewState["vs_vNivelesTabulador"]; }
            set { ViewState["vs_vNivelesTabulador"] = value; }
        }

        private List<E_EMPLEADOS_GRAFICAS> vEmpleadosDesviaciones
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vEmpleadosDesviaciones"]; }
            set { ViewState["vs_vEmpleadosDesviaciones"] = value; }
        }

        private List<E_EMPLEADOS_GRAFICAS> vEmpleadosSeleccionados
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vEmpleadosSeleccionados"]; }
            set { ViewState["vs_vEmpleadosSeleccionados"] = value; }
        }

        private List<E_EMPLEADOS_GRAFICAS> vLstEmpleados
        {
            get { return (List<E_EMPLEADOS_GRAFICAS>)ViewState["vs_vLstEmpleados"]; }
            set { ViewState["vs_vLstEmpleados"] = value; }
        }

        //private List<E_PLANEACION_INCREMENTOS> vListaTabuladorSueldos
        //{
        //    get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vListaTabuladorSueldos"]; }
        //    set { ViewState["vs_vListaTabuladorSueldos"] = value; }
        //}

        #endregion

        #region Funciones

        private void ObtenerPlaneacionIncrementos()
        {
            TabuladoresNegocio nTabuladores = new TabuladoresNegocio();
            vObtienePlaneacionIncremento = nTabuladores.ObtienePlaneacionIncrementos(ID_TABULADOR: vIdTabulador, ID_ROL: vIdRol).Select(s => new E_PLANEACION_INCREMENTOS()
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
            //   rgdIncrementosPlaneados.DataSource = vObtienePlaneacionIncremento;
            foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncremento)
            {
                if (item.MN_SUELDO_ORIGINAL != 0)
                    item.PR_INCREMENTO = (item.INCREMENTO / item.MN_SUELDO_ORIGINAL) * 100;
            }
        }

        protected void GuardarLista()
        {
            ContextoTabuladores.oLstEmpleadosDesviaciones = new List<E_REPORTE_TABULADOR_SUELDOS>();

            ContextoTabuladores.oLstEmpleadosDesviaciones.Add(new E_REPORTE_TABULADOR_SUELDOS
            {
                ID_TABULADOR = vIdTabulador
            });


            if (vEmpleadosSeleccionados.Count > 0)
            {
                foreach (var item in vEmpleadosSeleccionados)
                {
                    if (item.ID_TABULADOR_EMPLEADO != null)
                    {
                        ContextoTabuladores.oLstEmpleadosDesviaciones.Where(t => t.ID_TABULADOR == vIdTabulador).FirstOrDefault().vLstEmpleadosTabulador.Add((int)item.ID_TABULADOR_EMPLEADO);
                    }
                }
            }
            else
            {
                foreach (var item in vEmpleadosDesviaciones)
                {
                    if (item.ID_TABULADOR_EMPLEADO != null)
                    {
                        ContextoTabuladores.oLstEmpleadosDesviaciones.Where(t => t.ID_TABULADOR == vIdTabulador).FirstOrDefault().vLstEmpleadosTabulador.Add((int)item.ID_TABULADOR_EMPLEADO);
                    }
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;

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

                rcbNivelMercado.DataSource = vCuartilesTabulador;
                rcbNivelMercado.DataTextField = "NB_CUARTIL";
                rcbNivelMercado.DataValueField = "ID_CUARTIL".ToString();
                rcbNivelMercado.DataBind();

                rgAnalisisDesviaciones.DataSource = new List<E_EMPLEADOS_GRAFICAS>();
                rtsTabuladorDesviaciones.Tabs[3].Enabled = false;

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

                        agregaTooltipDesciaciones();

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
                            rcbNivelMercado.SelectedValue = UtilXML.ValorAtributo<int>(vXmlCuartilSeleccionado.Attribute("ID_CUARTIL")).ToString();
                        }

                    vLstEmpleados = new List<E_EMPLEADOS_GRAFICAS>();
                    vEmpleadosDesviaciones = new List<E_EMPLEADOS_GRAFICAS>();
                    vEmpleadosSeleccionados = new List<E_EMPLEADOS_GRAFICAS>();
                    //  vListaTabuladorSueldos = new List<E_PLANEACION_INCREMENTOS>();

                    ObtenerPlaneacionIncrementos();
                    CargarDatosTabuladorEmpleadoTodos();
                }


            }

        }

        protected void CargarDatosTabuladorEmpleado(List<int> pIdsSeleccionados)
        {
            ActualizarLista(int.Parse(rcbNivelMercado.SelectedValue));
            var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.Where(w => pIdsSeleccionados.Contains(w.ID_TABULADOR_EMPLEADO)).ToList();
            foreach (var item in vEmpleadosTabuladorSeleccionados)
            {
                if (item.NO_NIVEL >= int.Parse(txtComienza.Text) & item.NO_NIVEL <= int.Parse(txtTermina.Text))
                {
                    if (vEmpleadosSeleccionados.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
                    {
                        vEmpleadosSeleccionados.Add(new E_EMPLEADOS_GRAFICAS { ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO, NB_EMPLEADO = item.NB_EMPLEADO, NO_NIVEL = item.NO_NIVEL, MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL, NB_PUESTO = item.NB_PUESTO, PR_DIFERENCIA = item.PR_DIFERENCIA, CL_PR_DIFERENCIA = vRangos(item.PR_DIFERENCIA) });
                    }
                }
            }

            rgEmpleadosDesviasion.Rebind();
            CalculaPorcentajeNivel();
        }

        protected void CargarDatosTabuladorEmpleadoTodos()
        {
          
            ActualizarLista(int.Parse(rcbNivelMercado.SelectedValue));
            var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.ToList();
            foreach (var item in vEmpleadosTabuladorSeleccionados)
            {
                if (item.NO_NIVEL >= int.Parse(txtComienza.Text) & item.NO_NIVEL <= int.Parse(txtTermina.Text))
                {
                    if (vEmpleadosDesviaciones.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
                    {
                        vEmpleadosDesviaciones.Add(new E_EMPLEADOS_GRAFICAS { ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO, NB_EMPLEADO = item.NB_EMPLEADO, NO_NIVEL = item.NO_NIVEL, MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL, NB_PUESTO = item.NB_PUESTO, PR_DIFERENCIA = item.PR_DIFERENCIA, CL_PR_DIFERENCIA = vRangos(item.PR_DIFERENCIA) });
                    }
                }
            }

            rgEmpleadosDesviasion.Rebind();
            CalculaPorcentajeNivel();
        }

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

        protected void ActualizarLista(int pCuartilComparativo)
        {
            foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncremento)
            {
                item.MN_MINIMO_CUARTIL = CalculaMinimo(pCuartilComparativo, item.MN_MINIMO_MINIMO, item.MN_MINIMO_PRIMER_CUARTIL, item.MN_MINIMO_MEDIO, item.MN_MINIMO_SEGUNDO_CUARTIL, item.MN_MINIMO_MAXIMO);
                item.MN_MAXIMO_CUARTIL = CalculaMaximo(pCuartilComparativo, item.MN_MAXIMO_MINIMO, item.MN_MAXIMO_PRIMER_CUARTIL, item.MN_MAXIMO_MEDIO, item.MN_MAXIMO_SEGUNDO_CUARTIL, item.MN_MAXIMO_MAXIMO);
                item.DIFERENCIA = CalculoDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                if (item.MN_SUELDO_ORIGINAL > 0)
                    //item.PR_DIFERENCIA = (item.DIFERENCIA / item.MN_SUELDO_ORIGINAL) * 100;
                    item.PR_DIFERENCIA = CalculoPrDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                else item.PR_DIFERENCIA = 0;
                item.COLOR_DIFERENCIA = VariacionColor(item.PR_DIFERENCIA, item.MN_SUELDO_ORIGINAL);
                item.ICONO = ObtenerIconoDiferencia(item.PR_DIFERENCIA, item.MN_SUELDO_ORIGINAL);
            }
        }

        protected void ActualizaListaDesciaciones()
        {
            var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.Where(w => vEmpleadosDesviaciones.Select(s => s.ID_TABULADOR_EMPLEADO).Contains(w.ID_TABULADOR_EMPLEADO)).ToList();
            vEmpleadosDesviaciones.Clear();
            foreach (var item in vEmpleadosTabuladorSeleccionados)
            {
                if (item.NO_NIVEL >= int.Parse(txtComienza.Text) & item.NO_NIVEL <= int.Parse(txtTermina.Text))
                {
                    vEmpleadosDesviaciones.Add(new E_EMPLEADOS_GRAFICAS { ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO, NB_EMPLEADO = item.NB_EMPLEADO, NO_NIVEL = item.NO_NIVEL, MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL, NB_PUESTO = item.NB_PUESTO, PR_DIFERENCIA = item.PR_DIFERENCIA, CL_PR_DIFERENCIA = vRangos(item.PR_DIFERENCIA) });
                }
            }
            rgEmpleadosDesviasion.Rebind();
            CalculaPorcentajeNivel();
        }

        protected string vRangos(decimal? pPrDireferencia)
        {
            int? vRangoVerdeNegativo = vRangoVerde * -1;
            int? vRangoAmarilloNegativo = vRangoAmarillo * -1;
            string clave = null;

            if (pPrDireferencia <= vRangoVerde && pPrDireferencia >= vRangoVerdeNegativo)
                clave = "verde";
            else if (pPrDireferencia <= vRangoAmarillo && pPrDireferencia > vRangoVerde)
                clave = "amarilloPositivo";
            else if (pPrDireferencia <= vRangoVerdeNegativo && pPrDireferencia >= vRangoAmarilloNegativo)
                clave = "amarilloNegativo";
            else if (pPrDireferencia > vRangoAmarillo)
                clave = "rojoPositivo";
            else if (pPrDireferencia < vRangoAmarilloNegativo)
                clave = "rojoNegativo";
            return clave;
        }

        protected void CalculaPorcentajeNivel()
        {
            GuardarLista();
            if (vEmpleadosSeleccionados.Count > 0)
            {
                TabuladoresNegocio nNivelesTabulador = new TabuladoresNegocio();
                var vNiveles = vEmpleadosSeleccionados.Select(s => s.NO_NIVEL).ToList();
                List<E_NIVELES> vNivelesEmpleados = nNivelesTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador).Select(s => new E_NIVELES
                {
                    CL_TABULADOR_NIVEL = s.CL_TABULADOR_NIVEL,
                    ID_TABULADOR_NIVEL = s.ID_TABULADOR_NIVEL,
                    NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
                    NO_NIVEL = s.NO_NIVEL,
                    NO_ORDEN = s.NO_ORDEN,
                }).ToList();
                vNivelesTabulador = vNivelesEmpleados.Where(w => vNiveles.Contains(w.NO_NIVEL)).ToList();

                decimal vEmpleadosVerdes = vEmpleadosSeleccionados.Count(c => c.CL_PR_DIFERENCIA.Equals("verde"));
                decimal vEmpleadosAmarillosNeg = vEmpleadosSeleccionados.Count(c => c.CL_PR_DIFERENCIA.Equals("amarilloNegativo"));
                decimal vEmpleadosAmarillosPos = vEmpleadosSeleccionados.Count(c => c.CL_PR_DIFERENCIA.Equals("amarilloPositivo"));
                decimal vEmpleadosRojoPos = vEmpleadosSeleccionados.Count(c => c.CL_PR_DIFERENCIA.Equals("rojoPositivo"));
                decimal vEmpleadosRojoNeg = vEmpleadosSeleccionados.Count(c => c.CL_PR_DIFERENCIA.Equals("rojoNegativo"));
                decimal vEmpleados = vEmpleadosSeleccionados.Count();

                if (vNivelesTabulador.Count > 0)
                {

                    vNivelesTabulador.Add(new E_NIVELES
                    {
                        NB_TABULADOR_NIVEL = "NIVEL GENERAL",
                        PR_VERDE = (vEmpleadosVerdes / vEmpleados) * 100,
                        PR_AMARILLO_POS = (vEmpleadosAmarillosPos / vEmpleados) * 100,
                        PR_AMARILLO_NEG = (vEmpleadosAmarillosNeg / vEmpleados) * 100,
                        PR_ROJO_NEG = (vEmpleadosRojoNeg / vEmpleados) * 100,
                        PR_ROJO_POS = (vEmpleadosRojoPos / vEmpleados) * 100
                    });


                    foreach (var item in vNivelesTabulador)
                    {
                        decimal vSumaVerde = 0;
                        decimal vSumaAmarilloNeg = 0;
                        decimal vSumaAmarilloPos = 0;
                        decimal vSumaRojoNeg = 0;
                        decimal vSumaRojoPos = 0;

                        foreach (var iEmp in vEmpleadosSeleccionados)
                        {
                            if (item.NO_NIVEL == iEmp.NO_NIVEL)
                            {
                                switch (iEmp.CL_PR_DIFERENCIA)
                                {
                                    case "verde":
                                        vSumaVerde = vSumaVerde + 1;
                                        break;
                                    case "amarilloPositivo":
                                        vSumaAmarilloPos = vSumaAmarilloPos + 1;
                                        break;
                                    case "amarilloNegativo":
                                        vSumaAmarilloNeg = vSumaAmarilloNeg + 1;
                                        break;
                                    case "rojoPositivo":
                                        vSumaRojoPos = vSumaRojoPos + 1;
                                        break;
                                    case "rojoNegativo":
                                        vSumaRojoNeg = vSumaRojoNeg + 1;
                                        break;
                                }
                            }
                        }
                        decimal vTotalEmpleados = vEmpleadosSeleccionados.Count(c => c.NO_NIVEL == item.NO_NIVEL);
                        if (vTotalEmpleados != 0)
                        {
                            item.PR_VERDE = (vSumaVerde / vTotalEmpleados) * 100;
                            item.PR_AMARILLO_NEG = (vSumaAmarilloNeg / vTotalEmpleados) * 100;
                            item.PR_AMARILLO_POS = (vSumaAmarilloPos / vTotalEmpleados) * 100;
                            item.PR_ROJO_NEG = (vSumaRojoNeg / vTotalEmpleados) * 100;
                            item.PR_ROJO_POS = (vSumaRojoPos / vTotalEmpleados) * 100;
                        }
                    }
                    rgAnalisisDesviaciones.DataSource = vNivelesTabulador;
                    rgAnalisisDesviaciones.DataBind();
                }
            }
            else
            {
                TabuladoresNegocio nNivelesTabulador = new TabuladoresNegocio();
                var vNiveles = vEmpleadosDesviaciones.Select(s => s.NO_NIVEL).ToList();
                List<E_NIVELES> vNivelesEmpleados = nNivelesTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador).Select(s => new E_NIVELES
                {
                    CL_TABULADOR_NIVEL = s.CL_TABULADOR_NIVEL,
                    ID_TABULADOR_NIVEL = s.ID_TABULADOR_NIVEL,
                    NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
                    NO_NIVEL = s.NO_NIVEL,
                    NO_ORDEN = s.NO_ORDEN,
                }).ToList();
                vNivelesTabulador = vNivelesEmpleados.Where(w => vNiveles.Contains(w.NO_NIVEL)).ToList();

                decimal vEmpleadosVerdes = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("verde"));
                decimal vEmpleadosAmarillosNeg = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("amarilloNegativo"));
                decimal vEmpleadosAmarillosPos = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("amarilloPositivo"));
                decimal vEmpleadosRojoPos = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("rojoPositivo"));
                decimal vEmpleadosRojoNeg = vEmpleadosDesviaciones.Count(c => c.CL_PR_DIFERENCIA.Equals("rojoNegativo"));
                decimal vEmpleados = vEmpleadosDesviaciones.Count();

                if (vNivelesTabulador.Count > 0)
                {

                    vNivelesTabulador.Add(new E_NIVELES
                    {
                        NB_TABULADOR_NIVEL = "NIVEL GENERAL",
                        PR_VERDE = (vEmpleadosVerdes / vEmpleados) * 100,
                        PR_AMARILLO_POS = (vEmpleadosAmarillosPos / vEmpleados) * 100,
                        PR_AMARILLO_NEG = (vEmpleadosAmarillosNeg / vEmpleados) * 100,
                        PR_ROJO_NEG = (vEmpleadosRojoNeg / vEmpleados) * 100,
                        PR_ROJO_POS = (vEmpleadosRojoPos / vEmpleados) * 100
                    });


                    foreach (var item in vNivelesTabulador)
                    {
                        decimal vSumaVerde = 0;
                        decimal vSumaAmarilloNeg = 0;
                        decimal vSumaAmarilloPos = 0;
                        decimal vSumaRojoNeg = 0;
                        decimal vSumaRojoPos = 0;

                        foreach (var iEmp in vEmpleadosDesviaciones)
                        {
                            if (item.NO_NIVEL == iEmp.NO_NIVEL)
                            {
                                switch (iEmp.CL_PR_DIFERENCIA)
                                {
                                    case "verde":
                                        vSumaVerde = vSumaVerde + 1;
                                        break;
                                    case "amarilloPositivo":
                                        vSumaAmarilloPos = vSumaAmarilloPos + 1;
                                        break;
                                    case "amarilloNegativo":
                                        vSumaAmarilloNeg = vSumaAmarilloNeg + 1;
                                        break;
                                    case "rojoPositivo":
                                        vSumaRojoPos = vSumaRojoPos + 1;
                                        break;
                                    case "rojoNegativo":
                                        vSumaRojoNeg = vSumaRojoNeg + 1;
                                        break;
                                }
                            }
                        }
                        decimal vTotalEmpleados = vEmpleadosDesviaciones.Count(c => c.NO_NIVEL == item.NO_NIVEL);
                        if (vTotalEmpleados != 0)
                        {
                            item.PR_VERDE = (vSumaVerde / vTotalEmpleados) * 100;
                            item.PR_AMARILLO_NEG = (vSumaAmarilloNeg / vTotalEmpleados) * 100;
                            item.PR_AMARILLO_POS = (vSumaAmarilloPos / vTotalEmpleados) * 100;
                            item.PR_ROJO_NEG = (vSumaRojoNeg / vTotalEmpleados) * 100;
                            item.PR_ROJO_POS = (vSumaRojoPos / vTotalEmpleados) * 100;
                        }
                    }
                    rgAnalisisDesviaciones.DataSource = vNivelesTabulador;
                    rgAnalisisDesviaciones.DataBind();
                }
            }
        }

        protected void rgAnalisisDesviaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGrafica.Enabled = true;
            rtsTabuladorDesviaciones.Tabs[3].Visible = true;
            GridDataItem item = (GridDataItem)rgAnalisisDesviaciones.SelectedItems[0];
            int dataKey = int.Parse(item.GetDataKeyValue("ID_TABULADOR_NIVEL").ToString());
            var vNivelGrafica = vNivelesTabulador.Where(w => w.ID_TABULADOR_NIVEL == dataKey).ToList().FirstOrDefault();

            PieSeries vSerie = new PieSeries();

            vSerie.SeriesItems.Add(vNivelGrafica.PR_VERDE, System.Drawing.Color.Green, "Sueldo dentro del nivel establecido por el tabulador (variación inferior al " + vRangoVerde.ToString() + "%).", false, true);
            vSerie.SeriesItems.Add(vNivelGrafica.PR_AMARILLO_POS + vNivelGrafica.PR_AMARILLO_NEG, System.Drawing.Color.Yellow, "Sueldo superior o inferior al nivel establecido por el tabulador entre " + vRangoVerde.ToString() + "% y " + vRangoAmarillo.ToString() + "%.", false, true);
           // vSerie.SeriesItems.Add(vNivelGrafica.PR_AMARILLO_NEG, System.Drawing.Color.Gold, "Sueldo inferior al nivel establecido por el tabulador entre " + vRangoVerde.ToString() + "% y " + vRangoAmarillo.ToString() + "%.", false, true);
          //  vSerie.SeriesItems.Add(vNivelGrafica.PR_ROJO_POS, System.Drawing.Color.OrangeRed, "Sueldo superior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%.", false, true);
            vSerie.SeriesItems.Add(vNivelGrafica.PR_ROJO_NEG + vNivelGrafica.PR_ROJO_POS, System.Drawing.Color.Red, "Sueldo superior o inferior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%.", false, true);

            vSerie.TooltipsAppearance.Visible = false;
            vSerie.TooltipsAppearance.DataFormatString = "{0:N2}";
            vSerie.LabelsAppearance.DataFormatString = "{0:N2}%";
            PieChartGraficaDesviaciones.PlotArea.Series.Add(vSerie);
        }

        protected void CargarDatosEmpleados(List<int> pIdsSeleccionados)
        {
            EmpleadoNegocio nEmpleados = new EmpleadoNegocio();
            var vEmpleadosObtenidos = nEmpleados.ObtenerEmpleados();
            var vEmpleadosSeleccionados = vEmpleadosObtenidos.Where(w => pIdsSeleccionados.Contains(w.M_EMPLEADO_ID_EMPLEADO)).ToList();
            List<E_EMPLEADOS_GRAFICAS> vListaEmpleados = new List<E_EMPLEADOS_GRAFICAS>();
            vListaEmpleados = vCargaLista(vEmpleadosSeleccionados);
            foreach (var item in vListaEmpleados)
            {
                if (vLstEmpleados.Where(w => w.ID_EMPLEADO == item.ID_EMPLEADO).Count() == 0)
                {

                    vLstEmpleados.Add(item);
                }
            }
            //rgdEmpleados.Rebind();
            //PintarTabulador();
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

        #endregion

        protected void ramConsultas_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_ARREGLOS vSeleccion = new E_ARREGLOS();
            E_SELECTOR vSeleccionBono = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
            {
                vSeleccion = JsonConvert.DeserializeObject<E_ARREGLOS>(pParameter);
                vSeleccionBono = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);
            }

            if (vSeleccion.clTipo == "EMPLEADO")
            {
                vTabuladores = vSeleccion.arrIdTabulador;
                CargarDatosTabuladores(vTabuladores);
                CargarDatosEmpleados(vSeleccion.arrEmpleados);
            }
            if (vSeleccion.clTipo == "TABULADOR_EMPLEADO")
            {
                CargarDatosTabuladorEmpleado(vSeleccion.arrEmpleados);
            }
        }

        protected void rgdEmpleados_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EliminarEmpleado(int.Parse(item.GetDataKeyValue("ID_EMPLEADO").ToString()));
            }
        }

        protected void EliminarEmpleado(int pIdEmpleado)
        {
            E_EMPLEADOS_GRAFICAS vEmpleado = vLstEmpleados.Where(w => w.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

            if (vEmpleado != null)
            {
                vLstEmpleados.Remove(vEmpleado);
            }
            //PintarTabulador();
        }

        protected void EliminarEmpleadoTabulador(int pIdEmpleado)
        {
            if (vEmpleadosSeleccionados.Count > 0)
            {
                E_EMPLEADOS_GRAFICAS vEmpleado = vEmpleadosSeleccionados.Where(w => w.ID_TABULADOR_EMPLEADO == pIdEmpleado).FirstOrDefault();

                if (vEmpleado != null)
                {
                    vEmpleadosSeleccionados.Remove(vEmpleado);
                }
            }
            else
            {
                E_EMPLEADOS_GRAFICAS vEmpleado = vEmpleadosDesviaciones.Where(w => w.ID_TABULADOR_EMPLEADO == pIdEmpleado).FirstOrDefault();

                if (vEmpleado != null)
                {
                    vEmpleadosDesviaciones.Remove(vEmpleado);
                }
            }
            CalculaPorcentajeNivel();
        }

        protected void rgEmpleadosDesviasion_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = e.Item as GridDataItem;
                EliminarEmpleadoTabulador(int.Parse(item.GetDataKeyValue("ID_TABULADOR_EMPLEADO").ToString()));
            }
        }

        protected void rgEmpleadosDesviasion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            rgEmpleadosDesviasion.DataSource = vEmpleadosSeleccionados;
        }

        protected void rcbNivelMercado_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ActualizarLista(int.Parse(e.Value));
            ActualizaListaDesciaciones();
        }

        protected void grdCodigoColores_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            List<E_CODIGO_COLORES> vCodigoColores = new List<E_CODIGO_COLORES>();
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "green", DESCRIPCION = "Sueldo dentro del nivel establecido por el tabulador (variación inferior al " + vRangoVerde.ToString() + "%)." });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "yellow", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador entre el " + vRangoVerde.ToString() + "% y " + vRangoAmarillo.ToString() + "%." });
            vCodigoColores.Add(new E_CODIGO_COLORES { COLOR = "red", DESCRIPCION = "Sueldo superior o inferior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%." });
            grdCodigoColores.DataSource = vCodigoColores;
            RadGrid1.DataSource = vCodigoColores;
        }

        private void agregaTooltipDesciaciones()
        {
            rgAnalisisDesviaciones.Columns[1].HeaderTooltip = "Sueldo dentro del nivel establecido por el tabulador (variación inferior al " + vRangoVerde.ToString() + "%).";
            rgAnalisisDesviaciones.Columns[2].HeaderTooltip = "Sueldo superior al nivel establecido por el tabulador entre " + vRangoVerde.ToString() + "% y " + vRangoAmarillo.ToString() + "%.";
            rgAnalisisDesviaciones.Columns[3].HeaderTooltip = "Sueldo inferior al nivel establecido por el tabulador entre " + vRangoVerde.ToString() + "% y " + vRangoAmarillo.ToString() + "%.";
            rgAnalisisDesviaciones.Columns[4].HeaderTooltip = "Sueldo superior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%.";
            rgAnalisisDesviaciones.Columns[5].HeaderTooltip = "Sueldo inferior al nivel establecido por el tabulador en más del " + vRangoAmarillo.ToString() + "%.";

        }

        protected void rgEmpleadosDesviasion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgEmpleadosDesviasion.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void rgAnalisisDesviaciones_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridPagerItem)
            {
                RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");

                PageSizeCombo.Items.Clear();
                PageSizeCombo.Items.Add(new RadComboBoxItem("10"));
                PageSizeCombo.FindItemByText("10").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("50"));
                PageSizeCombo.FindItemByText("50").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("100"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgAnalisisDesviaciones.MasterTableView.ClientID);
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }

        protected void txtTermina_TextChanged(object sender, EventArgs e)
        {
            ActualizarLista(int.Parse(rcbNivelMercado.SelectedValue));
            ActualizaListaDesciaciones();
        }
    }


}