using SIGE.Entidades;
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
    public partial class ReporteAnalisisDesviaciones : System.Web.UI.Page
    {
        #region Variables

        public int vIdTabulador
        {
            get { return (int)ViewState["vsIdTabulador"]; }
            set { ViewState["vsIdTabulador"] = value; }
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

        private List<E_PLANEACION_INCREMENTOS> vObtienePlaneacionIncremento
        {
            get { return (List<E_PLANEACION_INCREMENTOS>)ViewState["vs_vObtienePlaneacionIncremento"]; }
            set { ViewState["vs_vObtienePlaneacionIncremento"] = value; }
        }

        private List<E_NIVELES> vNivelesTabulador
        {
            get { return (List<E_NIVELES>)ViewState["vs_vNivelesTabulador"]; }
            set { ViewState["vs_vNivelesTabulador"] = value; }
        }

        #endregion

        #region Metodos

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

        protected void CargarDatosTabuladorEmpleado(List<int> pIdsSeleccionados)
        {
            ActualizarLista(vCuartilComparativo);
            var vEmpleadosTabuladorSeleccionados = vObtienePlaneacionIncremento.Where(w => pIdsSeleccionados.Contains(w.ID_TABULADOR_EMPLEADO)).ToList();
            foreach (var item in vEmpleadosTabuladorSeleccionados)
            {
                    if (vEmpleadosSeleccionados.Where(w => w.ID_TABULADOR_EMPLEADO == item.ID_TABULADOR_EMPLEADO).Count() == 0)
                    {
                        vEmpleadosSeleccionados.Add(new E_EMPLEADOS_GRAFICAS { ID_TABULADOR_EMPLEADO = item.ID_TABULADOR_EMPLEADO, NB_EMPLEADO = item.NB_EMPLEADO, NO_NIVEL = item.NO_NIVEL, MN_SUELDO_ORIGINAL = item.MN_SUELDO_ORIGINAL, NB_PUESTO = item.NB_PUESTO, PR_DIFERENCIA = item.PR_DIFERENCIA, CL_PR_DIFERENCIA = vRangos(item.PR_DIFERENCIA) });
                    }
            }

            CalculaPorcentajeNivel();
        }

        protected void CalculaPorcentajeNivel()
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
                    //rgAnalisisDesviaciones.DataSource = vNivelesTabulador;
                    //rgAnalisisDesviaciones.DataBind();
                }
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

        protected void ActualizarLista(int pCuartilComparativo)
        {
            foreach (E_PLANEACION_INCREMENTOS item in vObtienePlaneacionIncremento)
            {
                item.MN_MINIMO_CUARTIL = CalculaMinimo(pCuartilComparativo, item.MN_MINIMO_MINIMO, item.MN_MINIMO_PRIMER_CUARTIL, item.MN_MINIMO_MEDIO, item.MN_MINIMO_SEGUNDO_CUARTIL, item.MN_MINIMO_MAXIMO);
                item.MN_MAXIMO_CUARTIL = CalculaMaximo(pCuartilComparativo, item.MN_MAXIMO_MINIMO, item.MN_MAXIMO_PRIMER_CUARTIL, item.MN_MAXIMO_MEDIO, item.MN_MAXIMO_SEGUNDO_CUARTIL, item.MN_MAXIMO_MAXIMO);
                item.DIFERENCIA = CalculoDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                if (item.MN_SUELDO_ORIGINAL > 0)
                    item.PR_DIFERENCIA = CalculoPrDiferencia(item.MN_MINIMO_CUARTIL, item.MN_MAXIMO_CUARTIL, item.MN_SUELDO_ORIGINAL);
                else item.PR_DIFERENCIA = 0;
                item.COLOR_DIFERENCIA = VariacionColor(item.PR_DIFERENCIA, item.MN_SUELDO_ORIGINAL);
                item.ICONO = ObtenerIconoDiferencia(item.PR_DIFERENCIA, item.MN_SUELDO_ORIGINAL);
            }
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
            vCtrlTh1.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:200px;");
            vCtrlTh1.InnerText = String.Format("{0}", "Nivel");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh1);

            HtmlGenericControl vCtrlTh2 = new HtmlGenericControl("td");
            vCtrlTh2.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:150px; padding: 3px;");
            vCtrlTh2.InnerHtml = String.Format("{0}", "<span style=\"border: 1px solid gray; border-radius: 5px; background: green;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<img src='../Assets/images/Icons/25/ArrowEqual.png' />");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh2);

            HtmlGenericControl vCtrlTh3 = new HtmlGenericControl("td");
            vCtrlTh3.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:150px; padding: 3px;");
            vCtrlTh3.InnerHtml = String.Format("{0}", "<span style=\"border: 1px solid gray; border-radius: 5px; background: yellow;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<img src='../Assets/images/Icons/25/ArrowUp.png' />");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh3);

            HtmlGenericControl vCtrlTh4 = new HtmlGenericControl("td");
            vCtrlTh4.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:150px; padding: 3px;");
            vCtrlTh4.InnerHtml = String.Format("{0}", "<span style=\"border: 1px solid gray; border-radius: 5px; background: yellow;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<img src='../Assets/images/Icons/25/ArrowDown.png' />");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh4);

            HtmlGenericControl vCtrlTh5 = new HtmlGenericControl("td");
            vCtrlTh5.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:150px; padding: 3px;");
            vCtrlTh5.InnerHtml = String.Format("{0}", "<span style=\"border: 1px solid gray; border-radius: 5px; background: red;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<img src='../Assets/images/Icons/25/ArrowUp.png' />");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh5);

            HtmlGenericControl vCtrlTh6 = new HtmlGenericControl("td");
            vCtrlTh6.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; font-weight:bold; width:150px; padding: 3px;");
            vCtrlTh6.InnerHtml = String.Format("{0}", "<span style=\"border: 1px solid gray; border-radius: 5px; background: red;\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>&nbsp;<img src='../Assets/images/Icons/25/ArrowDown.png' />");
            vCtrlRowEncabezado1.Controls.Add(vCtrlTh6);
          
            vCtrlColumn.Controls.Add(vCtrlRowEncabezado1);

            vTabla.Controls.Add(vCtrlColumn);

            HtmlGenericControl vCtrlTbody = new HtmlGenericControl("tbody");


            foreach (var item in vNivelesTabulador)
                {
                    HtmlGenericControl vCtrlRow = new HtmlGenericControl("tr");
                    vCtrlRow.Attributes.Add("style", "page-break-inside:avoid; page-break-after:auto;");

                    HtmlGenericControl vCtrlNumero = new HtmlGenericControl("td");
                    vCtrlNumero.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlNumero.Attributes.Add("align", "center");
                    vCtrlNumero.InnerText = String.Format("{0}", item.NB_TABULADOR_NIVEL);
                    vCtrlRow.Controls.Add(vCtrlNumero);

                    HtmlGenericControl vCtrlNbPuesto = new HtmlGenericControl("td");
                    vCtrlNbPuesto.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlNbPuesto.InnerText = String.Format("{0:N2}%", item.PR_VERDE);
                    vCtrlRow.Controls.Add(vCtrlNbPuesto);

                    HtmlGenericControl vCtrlNbDepartamento = new HtmlGenericControl("td");
                    vCtrlNbDepartamento.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlNbDepartamento.InnerText = String.Format("{0:N2}%", item.PR_AMARILLO_POS);
                    vCtrlRow.Controls.Add(vCtrlNbDepartamento);

                    HtmlGenericControl vCtrlNbEmpleado = new HtmlGenericControl("td");
                    vCtrlNbEmpleado.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlNbEmpleado.InnerText = String.Format("{0:N2}%", item.PR_AMARILLO_NEG);
                    vCtrlRow.Controls.Add(vCtrlNbEmpleado);

                    HtmlGenericControl vCtrlMnSueldo = new HtmlGenericControl("td");
                    vCtrlMnSueldo.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlMnSueldo.InnerText = String.Format("{0:N2}%", item.PR_ROJO_POS);
                    vCtrlRow.Controls.Add(vCtrlMnSueldo);

                    HtmlGenericControl vCtrlMnDiferencia = new HtmlGenericControl("td");
                    vCtrlMnDiferencia.Attributes.Add("style", "border: 1px solid #000000; font-family:arial; font-size: 11pt; padding: 10px;");
                    vCtrlMnDiferencia.InnerText = String.Format("{0:N2}%", item.PR_ROJO_NEG);
                    vCtrlRow.Controls.Add(vCtrlMnDiferencia);

                    vCtrlTbody.Controls.Add(vCtrlRow);
                }            

            vTabla.Controls.Add(vCtrlTbody);

            return vTabla;
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

                    if (Request.QueryString["pNivelMercado"] != null)
                        vCuartilComparativo = int.Parse(Request.QueryString["pNivelMercado"].ToString());


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

                    vLstEmpleados = new List<E_EMPLEADOS_GRAFICAS>();
                    vEmpleadosSeleccionados = new List<E_EMPLEADOS_GRAFICAS>();

                    ObtenerPlaneacionIncrementos();

                    if(ContextoTabuladores.oLstEmpleadosDesviaciones != null)
                        CargarDatosTabuladorEmpleado(ContextoTabuladores.oLstEmpleadosDesviaciones.Where(w=> w.ID_TABULADOR == vIdTabulador).FirstOrDefault().vLstEmpleadosTabulador);

                    dvTabla.Controls.Add(GeneraTablaReporte());
                }


            }
        }
    }
}