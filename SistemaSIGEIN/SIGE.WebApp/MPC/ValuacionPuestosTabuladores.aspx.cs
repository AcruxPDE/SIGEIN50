//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using SIGE.Entidades.Externas;
//using SIGE.Entidades.MetodologiaCompensacion;
//using SIGE.Negocio.MetodologiaCompensacion;
//using SIGE.WebApp.Comunes;
//using SIGE.Entidades;
//using System.IO;
//using System.Xml.Linq;
//using SIGE.Negocio.Utilerias;
//using SIGE.Negocio.Administracion;
//using Newtonsoft.Json;
//using SIGE.Entidades.Administracion;
//using Telerik.Web.UI;
//using System.Data;
//using System.Web.UI.HtmlControls;


//namespace SIGE.WebApp.MPC
//{
//    public partial class ValuacionPuestosTabuladores : System.Web.UI.Page
//    {
//        private string vClUsuario;
//        private string vNbPrograma;
//        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

//        public int vIdTabulador
//        {
//            get { return (int)ViewState["vsIdTabulador"]; }
//            set { ViewState["vsIdTabulador"] = value; }
//        }

//        private List<E_SIGNIFICADO_VALORES> vSignificadoValores
//        {
//            get { return (List<E_SIGNIFICADO_VALORES>)ViewState["vs_vSignificadoValores"]; }
//            set { ViewState["vs_vSignificadoValores"] = value; }
//        }

//        private List<E_VALUACION_PUESTO> vValuacionPuestos
//        {
//            get { return (List<E_VALUACION_PUESTO>)ViewState["vs_vValuacionPuestos"]; }
//            set { ViewState["vs_vValuacionPuestos"] = value; }
//        }

//        private List<E_VALUACION> vListaValuacion
//        {
//            get { return (List<E_VALUACION>)ViewState["vs_vListaValuacion"]; }
//            set { ViewState["vs_vListaValuacion"] = value; }
//        }

//        private DataTable vValuacion
//        {
//            get { return (DataTable)ViewState["vs_vValuacion"]; }
//            set { ViewState["vs_vValuacion"] = value; }
//        }

//        public int vNivelesTabulador
//        {
//            get { return (int)ViewState["vs_vNivelesTabulador"]; }
//            set { ViewState["vs_vNivelesTabulador"] = value; }
//        }

//        private List<E_NIVELACION> vVistaNivelacion
//        {
//            get { return (List<E_NIVELACION>)ViewState["vs_vVistaNivelacion"]; }
//            set { ViewState["vs_vVistaNivelacion"] = value; }
//        }

//        protected void Page_Init(object sender, EventArgs e)
//        {
//            vIdTabulador = int.Parse((Request.QueryString["ID"]));
//            List<E_VALUACION> vLstValuacion = new List<E_VALUACION>();
//            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
//            vValuacion = nTabulador.ObtieneTabuladorValuacion(vIdTabulador, ref vLstValuacion);
//            vListaValuacion = vLstValuacion;
//            foreach (var item in vValuacion.Columns)
//            {
//                GeneraColumna(item.ToString());
//            }
//        }

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
//            vNbPrograma = ContextoUsuario.nbPrograma;

//            if (!IsPostBack)
//            {
//                if (Request.QueryString["ID"] != null)
//                {
//                    vIdTabulador = int.Parse((Request.QueryString["ID"]));
//                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
//                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
//                    txtClTabulador.Text = vTabulador.CL_TABULADOR;
//                    txtNbTabulador.Text = vTabulador.DS_TABULADOR;
//                    rdpCreacion.SelectedDate = vTabulador.FE_CREACION;
//                    rdpVigencia.SelectedDate = vTabulador.FE_VIGENCIA;
//                    vNivelesTabulador = vTabulador.NO_NIVELES;
//                }
//            }

//        }

//        protected void GeneraColumna(string pColumna)
//        {
//            switch (pColumna)
//            {
//                case "ID_PUESTO":
//                    ConfigurarColumna(pColumna, 10, "Puesto", false, false, true, false);

//                    break;

//                case "ID_TABULADOR_PUESTO":
//                    ConfigurarColumna(pColumna, 10, "Puesto", false, false, true, false);

//                    break;

//                case "NB_PUESTO":
//                    ConfigurarColumna(pColumna, 220, "Puesto", true, false, false, false);
//                    break;

//                case "NO_PROMEDIO_VALUACION":
//                    ConfigurarColumna(pColumna, 80, "Promedio", true, false, false, false);
//                    break;

//                case "NO_NIVEL":
//                    ConfiguraTemplateColumna(pColumna, 50, "Nivel", true, false, false, true);
//                    break;

//                default:

//                    //if (pColumna.EndsWith("F")) 
//                    //    ConfigurarColumna(pColumna, 110, "", true, false, true, false);
//                    //else
//                    ConfiguraTemplateColumna(pColumna, 50, "", true, true, false, true);
//                    break;
//            }
//        }

//        protected void RadValuacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
//        {
//            List<E_SIGNIFICADO_VALORES> vSignificadoValores = new List<E_SIGNIFICADO_VALORES>();
//            vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 1, DESCRIPCION = "Este valor no aplica para el adecuado desempeño del puesto." });
//            vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 2, DESCRIPCION = "El desempeño del puesto demanda solamente lo mínimo para aplicar este valor, pudiendo hacerlo bajo asistencia o supervisión. Los resultados de su trabajo se verán poco afectados por este valor." });
//            vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 3, DESCRIPCION = "El puesto requiere de una aplicación regular de este valor, los resultados de su trabajo se facilitarían con este, sin embargo pudieran lograrse resultados con el adecuado desarrollo de otras competencias." });
//            vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 4, DESCRIPCION = "El puesto requiere de un dominio ligeramente por encima del promedio de la competencia, los recultados de su trabajo dependerán frecuentemente del adecuado desarrollo de esta capacidad." });
//            vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 5, DESCRIPCION = "El puesto requiere de una aplicación alta del valor, los resultados de su trabajo dependerán de manera importante de la aplicación de este." });
//            vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 6, DESCRIPCION = "Requiere una aplicación superior y profunda de este valor. Los resultados de su trabajo dependen directamente del desarrollo de esta capacidad." });
//            RadValuacion.DataSource = vSignificadoValores;
//        }

//        //protected void grdValuacionPuesto_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
//        //{
//        //TabuladoresNegocio nTabulador = new TabuladoresNegocio();
//        //var vTabulador = nTabulador.ObtieneTabuladorValuacion(ID_TABULADOR: vIdTabulador);
//        //    grdValuacionPuesto.DataSource = vTabulador;
//        //}

//        protected void btnGuardar_Click(object sender, EventArgs e)
//        {
//            GuardarPuesto(false, null);
//        }

//        protected void btnGaurdarCerrar_Click(object sender, EventArgs e)
//        {
//            GuardarPuesto(true, null);
//        }

//        protected void grdValuacion_ItemCreated(object sender, GridItemEventArgs e)
//        {
//            if (e.Item is GridDataItem)
//            {
//                GridDataItem gridItem = e.Item as GridDataItem;

//                int vIdPuesto = int.Parse(gridItem.GetDataKeyValue("ID_PUESTO").ToString());
//                string vClPuesto = vListaValuacion.Where(t => t.ID_PUESTO == vIdPuesto).FirstOrDefault().CL_PUESTO;

//                gridItem["NB_PUESTO"].ToolTip = vClPuesto;
//            }
//        }

//        protected void grdValuacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
//        {
//            grdValuacion.DataSource = vValuacion;
//        }

//        private void ConfiguraTemplateColumna(string pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pCentrar)
//        {
//            GridTemplateColumn pColumn = new GridTemplateColumn();
//            pColumn.DataField = pColumna;
//            pColumn.UniqueName = pColumna;

//            if (pGenerarEncabezado)
//                pColumn.HeaderText = GeneraEncabezado(pColumn);
//            else pColumn.HeaderText = pEncabezado;

//            pColumn.ItemTemplate = new MyTemplate(pColumn.UniqueName);

//            pColumn.HeaderStyle.Width = Unit.Pixel(pWidth);
//            pColumn.Visible = pVisible;

//            if (pCentrar)
//            {
//                pColumn.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
//            }

//            if (pFiltrarColumna & pVisible)
//            {
//                if (pWidth <= 60)
//                {
//                    (pColumn as GridTemplateColumn).FilterControlWidth = Unit.Pixel(pWidth);
//                }
//                else
//                {
//                    (pColumn as GridTemplateColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
//                }
//            }
//            else
//            {
//                (pColumn as GridTemplateColumn).AllowFiltering = false;
//            }

//            grdValuacion.MasterTableView.Columns.Add(pColumn);
//        }

//        private class MyTemplate : ITemplate
//        {
//            protected HtmlGenericControl vControlGrid;
//            private string colname;
//            public MyTemplate(string cName)
//            {
//                colname = cName;
//            }

//            public void InstantiateIn(System.Web.UI.Control container)
//            {
//                vControlGrid = new HtmlGenericControl("div");
//                vControlGrid.ID = "DivGridNbPeriodo";
//                container.Controls.Add(vControlGrid);
//            }
//        }

//        private void ConfigurarColumna(string pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pCentrar)
//        {
//            GridBoundColumn pColumn = new GridBoundColumn();
//            pColumn.DataField = pColumna;
//            pColumn.UniqueName = pColumna;
//            pColumn.HeaderStyle.Width = Unit.Pixel(pWidth);
//            pColumn.HeaderText = pEncabezado;
//            pColumn.Visible = pVisible;

//            if (pCentrar)
//            {
//                pColumn.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
//            }

//            if (pFiltrarColumna & pVisible)
//            {
//                if (pWidth <= 60)
//                {
//                    (pColumn as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
//                }
//                else
//                {
//                    (pColumn as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
//                }
//            }
//            else
//            {
//                (pColumn as GridBoundColumn).AllowFiltering = false;
//            }

//            grdValuacion.MasterTableView.Columns.Add(pColumn);
//        }

//        private string GeneraEncabezado(GridTemplateColumn pColumna)
//        {
//            int vResultado;
//            string vEncabezado = "";

//            string vFactor = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

//            if (int.TryParse(vFactor, out vResultado))
//            {
//                var vDatosFactores = vListaValuacion.Where(t => t.ID_COMPETENCIA == vResultado).FirstOrDefault();

//                if (vDatosFactores != null)
//                {
//                    vEncabezado = "<div style=\"writing-mode: tb-rl;height: 130px;font-size: 8pt;\">" + vDatosFactores.NB_COMPETENCIA + "</div>";
//                }
//            }
//            return vEncabezado;
//        }

//        protected void grdValuacion_ItemDataBound(object sender, GridItemEventArgs e)
//        {
//            if (e.Item is GridDataItem)
//            {
//                GridDataItem item = (GridDataItem)e.Item;
//                int vPuesto = 0;
//                foreach (var iColumn in vValuacion.Columns)
//                {
//                    vPuesto = int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString());
//                    RadNumericTextBox vValor = (RadNumericTextBox)item[iColumn.ToString()].FindControl(iColumn.ToString());
//                    int vResultado;

//                    //if (iColumn.ToString().EndsWith("E"))
//                    //{

//                    string vFactor = iColumn.ToString().Substring(0, iColumn.ToString().IndexOf('E'));

//                    if (int.TryParse(vFactor, out vResultado))
//                    {
//                        vValor.NumberFormat.DecimalDigits = 0;
//                        vValor.MaxValue = 5;
//                        vValor.MinValue = 0;
//                        vValor.EnabledStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
//                        vValor.AutoPostBack = false;

//                        var vDatosFactores = vListaValuacion.Where(t => t.ID_COMPETENCIA == vResultado & t.ID_PUESTO == vPuesto).FirstOrDefault();
//                        if (vDatosFactores != null)
//                        {
//                            vValor.Value = (int)vDatosFactores.NO_VALOR;
//                        }
//                        else
//                            vValor.Value = 0;
//                    }
//                    //}
//                    if (iColumn.ToString() == "NO_NIVEL")
//                    {
//                        var vDatosPuestos = vListaValuacion.Where(w => w.ID_PUESTO == vPuesto).FirstOrDefault();
//                        if (vDatosPuestos != null)
//                        {
//                            vValor.Value = (int)vDatosPuestos.NO_NIVEL;
//                            vValor.NumberFormat.DecimalDigits = 0;
//                            vValor.MaxValue = vNivelesTabulador;
//                            vValor.MinValue = 0;
//                            vValor.EnabledStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
//                            vValor.AutoPostBack = false;
//                        }
//                        else
//                            vValor.Value = 0;

//                    }
//                }
//            }
//        }

//        public void GuardarPuesto(bool pFgCerrarVentana, string pGuardar)
//        {
//            int vIdPuesto = 0;
//            int vIdCompetencia = 0;
//            int vIdTabuladorPuesto = 0;
//            int vIdTabuladorFactor = 0;
//            int vNoNivel = 0;
//            int vCompetencia;
//            int vFactor;
//            int vValorFactor = 0;
//            decimal vNoPromedioValuacion = 0;
//            RadNumericTextBox vValor = new RadNumericTextBox();
//            vValuacionPuestos = new List<E_VALUACION_PUESTO>();
//            vVistaNivelacion = new List<E_NIVELACION>();
//            foreach (GridDataItem item in grdValuacion.MasterTableView.Items)
//            {
//                foreach (var iColumn in vValuacion.Columns)
//                {
//                    vIdTabuladorPuesto = (int.Parse(item.GetDataKeyValue("ID_TABULADOR_PUESTO").ToString()));
//                    vNoNivel = (int.Parse(item.GetDataKeyValue("NO_NIVEL").ToString()));
//                    vIdPuesto = (int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString()));
//                    vNoPromedioValuacion = decimal.Parse(item.GetDataKeyValue("NO_PROMEDIO_VALUACION").ToString());

//                    vValor = (RadNumericTextBox)item[iColumn.ToString()].FindControl(iColumn.ToString());

//                    string[] vIds;
//                    vIds = iColumn.ToString().Split(',');
//                    if (vIds.Count() == 2)
//                    {

//                        string vIdCompetencias = vIds[0].ToString().Substring(0, vIds[0].ToString().IndexOf('E'));
//                        if (int.TryParse(vIdCompetencias, out vCompetencia))
//                            vIdCompetencia = vCompetencia;


//                        string vIdFactor = vIds[1].ToString().Substring(0, vIds[1].ToString().IndexOf('F'));
//                        if (int.TryParse(vIdFactor, out vFactor))
//                            vIdTabuladorFactor = vFactor;
//                    }

//                    if (vValor != null)
//                    {
//                        if (iColumn.ToString() == "NO_NIVEL")
//                        {
//                            vNoNivel = int.Parse(vValor.Text);
//                            vVistaNivelacion.Add(new E_NIVELACION { ID_TABULADOR_PUESTO = vIdTabuladorPuesto, NO_NIVEL = vNoNivel });
//                        }
//                        else
//                        {
//                            vValorFactor = int.Parse(vValor.Text);

//                            if (!vValor.Text.Equals(""))
//                                vValuacionPuestos.Add(new E_VALUACION_PUESTO { ID_COMPETENCIA = vIdCompetencia, ID_PUESTO = vIdPuesto, ID_TABULADOR_PUESTO = vIdTabuladorPuesto, NO_NIVEL = vNoNivel, NO_PROMEDIO_VALUACION = vNoPromedioValuacion, NO_VALOR = vValorFactor, ID_TABULADOR_FACTOR = vIdTabuladorFactor });
//                        }
//                    }
//                }
//            }
//            var vXelements = vValuacionPuestos.Select(x =>
//                                           new XElement("VALUACION",
//                                           new XAttribute("ID_COMPETENCIA", x.ID_COMPETENCIA),
//                                           new XAttribute("ID_PUESTO", x.ID_PUESTO),
//                                           new XAttribute("ID_TABULADOR_PUESTO", x.ID_TABULADOR_PUESTO),
//                                           new XAttribute("NO_NIVEL", x.NO_NIVEL),
//                                           new XAttribute("NO_PROMEDIO_VALUACION", x.NO_PROMEDIO_VALUACION),
//                                           new XAttribute("NO_VALOR", x.NO_VALOR),
//                                            new XAttribute("ID_TABULADOR_FACTOR", x.ID_TABULADOR_FACTOR)


//                                ));
//            XElement TABULADORVALUACION =
//            new XElement("VALUACIONES", vXelements
//            );

//            var vNivelPuestos = (from a in vValuacionPuestos select new { a.ID_TABULADOR_PUESTO, a.NO_NIVEL }).Distinct();
//            List<E_NIVELACION> vResultadoVista = new List<E_NIVELACION>();

//            for (int i = 0; i < vVistaNivelacion.Count; i++)
//            {
//                if (vVistaNivelacion.ElementAt(i).NO_NIVEL != vNivelPuestos.ElementAt(i).NO_NIVEL)
//                {
//                    vResultadoVista.Add(vVistaNivelacion.ElementAt(i));
//                }
//            }
//            var vXelementss = vResultadoVista.Select(x =>
//                                           new XElement("NIVEL",
//                                           new XAttribute("ID_TABULADOR_PUESTO", x.ID_TABULADOR_PUESTO),
//                                           new XAttribute("NO_NIVEL", x.NO_NIVEL)
//                                ));
//            XElement NIVELACION =
//            new XElement("NIVELES", vXelementss
//            );

//            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
//            E_RESULTADO vResultado = nTabulador.InsertaActualizaTabuladorValuacion(vIdTabulador, TABULADORVALUACION.ToString(), NIVELACION.ToString(), pGuardar, vClUsuario, vNbPrograma);

//            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.WARNING))
//            {
//                List<E_MENSAJE_COMPETENCIA> MensajeCompetencias = new List<E_MENSAJE_COMPETENCIA>();
//                XElement vDatosRespuesta = vResultado.ObtieneDatosRespuesta();
//                foreach (XElement vXmlDatos in vDatosRespuesta.Elements("DATOS"))
//                {
//                    MensajeCompetencias.Add(new E_MENSAJE_COMPETENCIA
//                    {
//                        NB_PUESTO = UtilXML.ValorAtributo<string>(vXmlDatos.Attribute("NB_PUESTO")),
//                        NB_COMPETENCIA = UtilXML.ValorAtributo<string>(vXmlDatos.Attribute("NB_COMPETENCIA"))
//                    });
//                }
//                string competencias = string.Join(",", MensajeCompetencias.Select(p => p.NB_COMPETENCIA + " " + p.NB_PUESTO));
//                string vMensajeError = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE + "<br>" + " " + competencias;
//                UtilMensajes.MensajeDB(rwmMensaje, vMensajeError, vResultado.CL_TIPO_ERROR, 400, 200, pCallBackFunction: "GuardarWindow");
//            }
//            else
//            {
//                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
//                bool vCerrarVentana = pFgCerrarVentana;
//                string vCallBackFunction = vCerrarVentana ? "closeWindow" : null;
//                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);
//            }
//        }

//        protected void ramGuardar_AjaxRequest(object sender, AjaxRequestEventArgs e)
//        {
//            string pParameter = e.Argument;
//            if (pParameter == "GUARDAR")
//            {

//                GuardarPuesto(false, pParameter);
//            }
//        }

//        protected void btnRecalcularNivel_Click(object sender, EventArgs e)
//        {
//            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
//            E_RESULTADO vResultado = nTabulador.ActualizaNivelPuesto(vIdTabulador, vClUsuario, vNbPrograma);
//            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
//            UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");

//            grdValuacion.Rebind();
//        }
//    }
//}

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
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using OfficeOpenXml;
using System.Reflection;


namespace SIGE.WebApp.MPC
{
    public partial class ValuacionPuestosTabuladores : System.Web.UI.Page
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

        private List<E_SIGNIFICADO_VALORES> vSignificadoValores
        {
            get { return (List<E_SIGNIFICADO_VALORES>)ViewState["vs_vSignificadoValores"]; }
            set { ViewState["vs_vSignificadoValores"] = value; }
        }

        private List<E_VALUACION_PUESTO> vValuacionPuestos
        {
            get { return (List<E_VALUACION_PUESTO>)ViewState["vs_vValuacionPuestos"]; }
            set { ViewState["vs_vValuacionPuestos"] = value; }
        }

        private List<E_VALUACION> vListaValuacion
        {
            get { return (List<E_VALUACION>)ViewState["vs_vListaValuacion"]; }
            set { ViewState["vs_vListaValuacion"] = value; }
        }

        private DataTable vValuacion
        {
            get { return (DataTable)ViewState["vs_vValuacion"]; }
            set { ViewState["vs_vValuacion"] = value; }
        }

        public int vNivelesTabulador
        {
            get { return (int)ViewState["vs_vNivelesTabulador"]; }
            set { ViewState["vs_vNivelesTabulador"] = value; }
        }

        public int vNumeroCompetencias
        {
            get { return (int)ViewState["vs_vNumeroCompetencias"]; }
            set { ViewState["vs_vNumeroCompetencias"] = value; }
        }

        public int vIdNivelFactor
        {
            get { return (int)ViewState["vs_vIdNivelFactor"]; }
            set { ViewState["vs_vIdNivelFactor"] = value; }
        }

        private List<E_NIVELACION> vVistaNivelacion
        {
            get { return (List<E_NIVELACION>)ViewState["vs_vVistaNivelacion"]; }
            set { ViewState["vs_vVistaNivelacion"] = value; }
        }

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            vIdTabulador = int.Parse((Request.QueryString["ID"]));
            List<E_VALUACION> vLstValuacion = new List<E_VALUACION>();
            TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            vValuacion = nTabulador.ObtieneTabuladorValuacion(vIdTabulador, ref vLstValuacion);
            vListaValuacion = vLstValuacion;
            foreach (var item in vValuacion.Columns)
            {
                GeneraColumna(item.ToString());
            }
        }

        private void SeguridadProcesos()
        {
            btnGuardar.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.H.A");
            btnGaurdarCerrar.Enabled = ContextoUsuario.oUsuario.TienePermiso("O.A.A.H.A");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    vIdNivelFactor = 0;
                    vIdTabulador = int.Parse((Request.QueryString["ID"]));
                    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                    var vTabulador = nTabulador.ObtenerTabuladores(ID_TABULADOR: vIdTabulador).FirstOrDefault();
                    vNumeroCompetencias = nTabulador.ObtieneDatosValuacion(vIdTabulador).Where(x => x.ID_TABULADOR_FACTOR != 0).Count();
                    SeguridadProcesos();

                    txtClTabulador.InnerText = vTabulador.CL_TABULADOR;
                    txtDescripción.InnerText = vTabulador.DS_TABULADOR;
                    txtNbTabulador.InnerText = vTabulador.NB_TABULADOR;
                    txtVigencia.InnerText = vTabulador.FE_VIGENCIA.ToString("dd/MM/yyyy");
                    txtFecha.InnerText = vTabulador.FE_CREACION.ToString("dd/MM/yyyy");
                    txtPuestos.InnerText = vTabulador.CL_TIPO_PUESTO;
                    vNivelesTabulador = vTabulador.NO_NIVELES;
                    if (vTabulador.CL_ESTADO == "CERRADO")
                    {
                        btnGaurdarCerrar.Enabled = false;
                        btnGuardar.Enabled = false;
                    }

                    int TabNivel = 0;
                    int vCount = nTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador).Count;
                    if (vCount > 0)
                    {
                        TabNivel = nTabulador.ObtieneTabuladorNivel(ID_TABULADOR: vIdTabulador).OrderByDescending(w => w.NO_NIVEL).FirstOrDefault().NO_NIVEL;
                    }

                    if (vTabulador.FG_RECALCULAR_NIVELES == true && vNumeroCompetencias > 0 && TabNivel != vTabulador.NO_NIVELES )
                    { 
                        lblAdvertencia.Visible = true;
                        lblAdvertencia.InnerText = "No es posible generar el # de niveles solicitados porque el número de puestos y/o sus valuaciones no son suficientes.";
                    }

                    if (vNumeroCompetencias < 1)
                    {
                        lblAdvertencia.Visible = true;
                        lblAdvertencia.InnerText = "No se han seleccionado valores de valuación. Puedes ingresar el nivel para un puesto capturando el nivel correspondiente.";
                    }
                }
            }

        }

        
        //vListaValuacion

        protected void GeneraColumna(string pColumna)
        {
            switch (pColumna)
            {
                case "ID_PUESTO":
                    ConfigurarColumna(pColumna, 10, "Puesto", false, false, true, false);

                    break;

                case "ID_TABULADOR_PUESTO":
                    ConfigurarColumna(pColumna, 10, "Puesto", false, false, true, false);

                    break;

                case "NB_PUESTO":
                    ConfigurarColumna(pColumna, 220, "Puesto", true, false, false, false);
                    break;

                case "NO_PROMEDIO_VALUACION":
                    ConfigurarColumna(pColumna, 80, "Promedio", true, false, false, false);
                    break;

                case "NO_NIVEL":
                    ConfiguraTemplateColumna(pColumna, 50, "Nivel", true, false, false, true, false);
                    break;

                default:

                    //if (pColumna.EndsWith("F")) 
                    //    ConfigurarColumna(pColumna, 110, "", true, false, true, false);
                    //else
                    ConfiguraTemplateColumna(pColumna, 50, "", true, true, false, true, true);
                    break;
            }
        }

        protected void RadValuacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            TabuladoresNegocio nTabuladorCompetencia = new TabuladoresNegocio();
            var vLstNiveles = nTabuladorCompetencia.ObtenieneCompetenciasTabulador(vIdNivelFactor).FirstOrDefault();

            List<E_SIGNIFICADO_VALORES> vSignificadoValores = new List<E_SIGNIFICADO_VALORES>();
            if (vLstNiveles != null)
            {
                lblTitulo.Text = vLstNiveles.NB_COMPETENCIA;
                lblCompetencia.Text = vLstNiveles.DS_COMPETENCIA;
                vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 0, DESCRIPCION = vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N0 });
                vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 1, DESCRIPCION = vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N1 });
                vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 2, DESCRIPCION = vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N2 });
                vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 3, DESCRIPCION = vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N3 });
                vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 4, DESCRIPCION = vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N4 });
                vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 5, DESCRIPCION = vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N5 });
            }
            RadValuacion.DataSource = vSignificadoValores;

            //List<E_SIGNIFICADO_VALORES> vSignificadoValores = new List<E_SIGNIFICADO_VALORES>();
            //vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 0, DESCRIPCION = "Este valor no aplica para el adecuado desempeño del puesto." });
            //vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 1, DESCRIPCION = "El desempeño del puesto demanda solamente lo mínimo para aplicar este valor, pudiendo hacerlo bajo asistencia o supervisión. Los resultados de su trabajo se verán poco afectados por este valor." });
            //vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 2, DESCRIPCION = "El puesto requiere de una aplicación regular de este valor, los resultados de su trabajo se facilitarían con este, sin embargo pudieran lograrse resultados con el adecuado desarrollo de otras competencias." });
            //vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 3, DESCRIPCION = "El puesto requiere de un dominio ligeramente por encima del promedio de la competencia, los recultados de su trabajo dependerán frecuentemente del adecuado desarrollo de esta capacidad." });
            //vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 4, DESCRIPCION = "El puesto requiere de una aplicación alta del valor, los resultados de su trabajo dependerán de manera importante de la aplicación de este." });
            //vSignificadoValores.Add(new E_SIGNIFICADO_VALORES { NIVEL = 5, DESCRIPCION = "Requiere una aplicación superior y profunda de este valor. Los resultados de su trabajo dependen directamente del desarrollo de esta capacidad." });
            //RadValuacion.DataSource = vSignificadoValores;
        }

        //protected void grdValuacionPuesto_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        //{
        //TabuladoresNegocio nTabulador = new TabuladoresNegocio();
        //var vTabulador = nTabulador.ObtieneTabuladorValuacion(ID_TABULADOR: vIdTabulador);
        //    grdValuacionPuesto.DataSource = vTabulador;
        //}

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarPuesto(false, null);
        }

        protected void btnGaurdarCerrar_Click(object sender, EventArgs e)
        {
            GuardarPuesto(true, null);
        }

        protected void grdValuacion_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                int vIdPuesto = int.Parse(gridItem.GetDataKeyValue("ID_PUESTO").ToString());
                string vClPuesto = vListaValuacion.Where(t => t.ID_PUESTO == vIdPuesto).FirstOrDefault().CL_PUESTO;

                gridItem["NB_PUESTO"].ToolTip = vClPuesto;
            }
        }

        protected void grdValuacion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdValuacion.DataSource = vValuacion;
        }

        private void ConfiguraTemplateColumna(string pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pCentrar, bool pColorEncabezado)
        {
            GridTemplateColumn pColumn = new GridTemplateColumn();
            pColumn.DataField = pColumna;
            pColumn.UniqueName = pColumna;

            if (pColorEncabezado)
                pColumn.HeaderStyle.BackColor = System.Drawing.ColorTranslator.FromHtml(GenerarColor(pColumn));

            if (pGenerarEncabezado)
                pColumn.HeaderText = GeneraEncabezado(pColumn);
            else pColumn.HeaderText = pEncabezado;

            pColumn.ItemTemplate = new MyTemplate(pColumn.UniqueName);

            pColumn.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumn.Visible = pVisible;
            pColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            pColumn.HeaderStyle.VerticalAlign = VerticalAlign.Middle;

            if (pCentrar)
            {
                pColumn.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            }

            if (pFiltrarColumna & pVisible)
            {
                if (pWidth <= 60)
                {
                    (pColumn as GridTemplateColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (pColumn as GridTemplateColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
                }
            }
            else
            {
                (pColumn as GridTemplateColumn).AllowFiltering = false;
            }

            grdValuacion.MasterTableView.Columns.Add(pColumn);
        }

        private class MyTemplate : ITemplate
        {
            protected RadNumericTextBox vRadNumeric;
            private string colname;
            public MyTemplate(string cName)
            {
                colname = cName;
            }

            public void InstantiateIn(System.Web.UI.Control container)
            {
                vRadNumeric = new RadNumericTextBox();
                vRadNumeric.ID = colname;
                vRadNumeric.AutoPostBack = true;
                vRadNumeric.Width = 30;
                string vIdFactor = colname.Substring(colname.LastIndexOf(',') + 1);
                vIdFactor = vIdFactor.Replace("F", "");
                vRadNumeric.Attributes["onfocus"] = string.Format("ValoresNiveles('{0}');", vIdFactor);
                container.Controls.Add(vRadNumeric);
            }
        }

        private void ConfigurarColumna(string pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna, bool pCentrar)
        {
            GridBoundColumn pColumn = new GridBoundColumn();
            pColumn.DataField = pColumna;
            pColumn.UniqueName = pColumna;
            pColumn.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumn.HeaderText = pEncabezado;
            pColumn.Visible = pVisible;

            if (pCentrar)
            {
                pColumn.ItemStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            }

            if (pFiltrarColumna & pVisible)
            {
                if (pWidth <= 60)
                {
                    (pColumn as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (pColumn as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
                }
            }
            else
            {
                (pColumn as GridBoundColumn).AllowFiltering = false;
            }

            grdValuacion.MasterTableView.Columns.Add(pColumn);
        }

        private string GeneraEncabezado(GridTemplateColumn pColumna)
        {
            int vResultado;
            string vEncabezado = "";

            string vFactor = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

            if (vFactor != "0")
            {
                if (int.TryParse(vFactor, out vResultado))
                {
                    var vDatosFactores = vListaValuacion.Where(t => t.ID_COMPETENCIA == vResultado).FirstOrDefault();

                    if (vDatosFactores != null)
                    {
                        vEncabezado = "<div title=\"" + vDatosFactores.DS_COMPETENCIA + "\" style=\"writing-mode: tb-rl; height: 130px; font-size: 10pt;  color: #FFFFFF;\">" + vDatosFactores.NB_COMPETENCIA + "</div>";
                    }
                }
            }
            else
            {
                var vFactorN = pColumna.UniqueName.ToString().Split(new string[] { "," }, StringSplitOptions.None);
                string vIdColumna = vFactorN[1].ToString().Substring(0, vFactorN[1].ToString().IndexOf('F'));
                if (int.TryParse(vIdColumna, out vResultado))
                {
                    var vDatosFactores = vListaValuacion.Where(t => t.ID_TABULADOR_FACTOR.ToString() == vIdColumna).FirstOrDefault();
                    if (vDatosFactores != null)
                    {
                        vEncabezado = "<div title=\"" + vDatosFactores.DS_COMPETENCIA + "\" style=\"writing-mode: tb-rl; height: 130px; font-size: 10pt;  \">" + vDatosFactores.NB_COMPETENCIA + "</div>";
                    }
                }
            }
            return vEncabezado;
        }

        private string GenerarColor(GridTemplateColumn pColumna)
        {
            int vResultado;
            string vColor = "#0087CF";

            string vFactor = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

            if (vFactor != "0")
            {
                if (int.TryParse(vFactor, out vResultado))
                {
                    var vDatosFactores = vListaValuacion.Where(t => t.ID_COMPETENCIA == vResultado).FirstOrDefault();

                    if (vDatosFactores.CL_TIPO_COMPETENCIA == "ESP")
                    {
                        vColor = "#A52A2A";
                    }
                }
            }
            else
            {
                vColor = "#E6EAEC";
            }


            return vColor;
        }

        protected void grdValuacion_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                int vPuesto = 0;
                foreach (var iColumn in vValuacion.Columns)
                {
                    vPuesto = int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString());
                    RadNumericTextBox vValor = (RadNumericTextBox)item[iColumn.ToString()].FindControl(iColumn.ToString());
                    int vResultado;

                    //if (iColumn.ToString().EndsWith("E"))
                    //{

                    string vFactor = iColumn.ToString().Substring(0, iColumn.ToString().IndexOf('E'));
                    // string vIdNivelesFactor = iColumn.ToString().Substring(iColumn.ToString().LastIndexOf(",")+1).Replace("F","");

                    if (vFactor != "0")
                    {

                        if (int.TryParse(vFactor, out vResultado))
                        {
                            vValor.NumberFormat.DecimalDigits = 0;
                            vValor.MaxValue = 5;
                            vValor.MinValue = 0;
                            vValor.EnabledStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                            vValor.AutoPostBack = false;


                            var vDatosFactores = vListaValuacion.Where(t => t.ID_COMPETENCIA == vResultado & t.ID_PUESTO == vPuesto).FirstOrDefault();
                            if (vDatosFactores != null)
                            {

                                //TabuladoresNegocio nTabuladorCompetencia = new TabuladoresNegocio();
                                //var vLstNiveles = nTabuladorCompetencia.ObtenieneCompetenciasTabulador(int.Parse(vIdNivelesFactor)).FirstOrDefault();

                                //vValor.ToolTip = "Nivel 0: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N0 + "\r\n" +
                                //                 "Nivel 1: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N1 + "\r\n" +
                                //                 "Nivel 2: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N2 + "\r\n" +
                                //                 "Nivel 3: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N3 + "\r\n" +
                                //                 "Nivel 4: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N4 + "\r\n" +
                                //                 "Nivel 5: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N5 + "\r\n";
                                vValor.ToolTip = vDatosFactores.NB_COMPETENCIA;
                                vValor.Value = (int)vDatosFactores.NO_VALOR;
                            }
                            else
                                vValor.Value = 0;
                        }
                    }
                    else
                    {
                        var vFactorN = iColumn.ToString().Split(new string[] { "," }, StringSplitOptions.None);
                        string vIdColumna = vFactorN[1].ToString().Substring(0, vFactorN[1].ToString().IndexOf('F'));
                        if (int.TryParse(vIdColumna, out vResultado))
                        {

                            vValor.NumberFormat.DecimalDigits = 0;
                            vValor.MaxValue = 5;
                            vValor.MinValue = 0;
                            vValor.EnabledStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                            vValor.AutoPostBack = false;

                            var vDatosFactores = vListaValuacion.Where(t => t.ID_TABULADOR_FACTOR == vResultado & t.ID_PUESTO == vPuesto).FirstOrDefault();
                            if (vDatosFactores != null)
                            {
                                //TabuladoresNegocio nTabuladorCompetencia = new TabuladoresNegocio();
                                //var vLstNiveles = nTabuladorCompetencia.ObtenieneCompetenciasTabulador(int.Parse(vIdNivelesFactor)).FirstOrDefault();

                                //vValor.ToolTip = "Nivel 0: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N0 + "\r\n" +
                                //                 "Nivel 1: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N1 + "\r\n" +
                                //                 "Nivel 2: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N2 + "\r\n" +
                                //                 "Nivel 3: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N3 + "\r\n" +
                                //                 "Nivel 4: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N4 + "\r\n" +
                                //                 "Nivel 5: " + vLstNiveles.DS_NIVEL_COMPETENCIA_PUESTO_N5 + "\r\n";
                                vValor.ToolTip = vDatosFactores.NB_COMPETENCIA;
                                vValor.Value = (int)vDatosFactores.NO_VALOR;
                            }
                            else
                                vValor.Value = 0;
                        }
                    }
                    //}
                    if (iColumn.ToString() == "NO_NIVEL")
                    {
                        var vDatosPuestos = vListaValuacion.Where(w => w.ID_PUESTO == vPuesto).FirstOrDefault();
                        if (vDatosPuestos != null)
                        {
                            vValor.Value = (int)vDatosPuestos.NO_NIVEL;
                            vValor.NumberFormat.DecimalDigits = 0;
                            vValor.MaxValue = vNivelesTabulador;
                            vValor.MinValue = 0;
                            vValor.EnabledStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                            vValor.AutoPostBack = false;
                        }
                        else
                            vValor.Value = 0;

                    }
                }
            }
        }

        public void GuardarPuesto(bool pFgCerrarVentana, string pGuardar)
        {
            int vIdPuesto = 0;
            int vIdCompetencia = 0;
            int vIdTabuladorPuesto = 0;
            int vIdTabuladorFactor = 0;
            int vNoNivel = 0;
            int vCompetencia;
            int vFactor;
            int vValorFactor = 0;
            decimal vNoPromedioValuacion = 0;
            RadNumericTextBox vValor = new RadNumericTextBox();
            vValuacionPuestos = new List<E_VALUACION_PUESTO>();
            vVistaNivelacion = new List<E_NIVELACION>();

            if (vNumeroCompetencias > 0)
            {
                foreach (GridDataItem item in grdValuacion.MasterTableView.Items)
                {
                    foreach (var iColumn in vValuacion.Columns)
                    {
                        vIdTabuladorPuesto = (int.Parse(item.GetDataKeyValue("ID_TABULADOR_PUESTO").ToString()));
                        vNoNivel = (int.Parse(item.GetDataKeyValue("NO_NIVEL").ToString()));
                        vIdPuesto = (int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString()));
                        vNoPromedioValuacion = decimal.Parse(item.GetDataKeyValue("NO_PROMEDIO_VALUACION").ToString());

                        vValor = (RadNumericTextBox)item[iColumn.ToString()].FindControl(iColumn.ToString());

                        string[] vIds;
                        vIds = iColumn.ToString().Split(',');
                        if (vIds.Count() == 2)
                        {

                            string vIdCompetencias = vIds[0].ToString().Substring(0, vIds[0].ToString().IndexOf('E'));
                            if (int.TryParse(vIdCompetencias, out vCompetencia))
                                vIdCompetencia = vCompetencia;


                            string vIdFactor = vIds[1].ToString().Substring(0, vIds[1].ToString().IndexOf('F'));
                            if (int.TryParse(vIdFactor, out vFactor))
                                vIdTabuladorFactor = vFactor;
                        }

                        if (vValor != null)
                        {
                            if (iColumn.ToString() == "NO_NIVEL")
                            {
                                vNoNivel = int.Parse(vValor.Text);
                                vVistaNivelacion.Add(new E_NIVELACION { ID_TABULADOR_PUESTO = vIdTabuladorPuesto, NO_NIVEL = vNoNivel });
                            }
                            else
                            {
                                vValorFactor = int.Parse(vValor.Text);

                                if (!vValor.Text.Equals(""))
                                    vValuacionPuestos.Add(new E_VALUACION_PUESTO { ID_COMPETENCIA = vIdCompetencia, ID_PUESTO = vIdPuesto, ID_TABULADOR_PUESTO = vIdTabuladorPuesto, NO_NIVEL = vNoNivel, NO_PROMEDIO_VALUACION = vNoPromedioValuacion, NO_VALOR = vValorFactor, ID_TABULADOR_FACTOR = vIdTabuladorFactor });
                            }
                        }
                    }
                }
                var vXelements = vValuacionPuestos.Select(x =>
                                               new XElement("VALUACION",
                                               new XAttribute("ID_COMPETENCIA", x.ID_COMPETENCIA),
                                               new XAttribute("ID_PUESTO", x.ID_PUESTO),
                                               new XAttribute("ID_TABULADOR_PUESTO", x.ID_TABULADOR_PUESTO),
                                               new XAttribute("NO_NIVEL", x.NO_NIVEL),
                                               new XAttribute("NO_PROMEDIO_VALUACION", x.NO_PROMEDIO_VALUACION),
                                               new XAttribute("NO_VALOR", x.NO_VALOR),
                                                new XAttribute("ID_TABULADOR_FACTOR", x.ID_TABULADOR_FACTOR)


                                    ));
                XElement TABULADORVALUACION =
                new XElement("VALUACIONES", vXelements
                );

                var vNivelPuestos = (from a in vValuacionPuestos select new { a.ID_TABULADOR_PUESTO, a.NO_NIVEL }).Distinct();
                List<E_NIVELACION> vResultadoVista = new List<E_NIVELACION>();

                for (int i = 0; i < vVistaNivelacion.Count; i++)
                {
                    if (vVistaNivelacion.ElementAt(i).NO_NIVEL != vNivelPuestos.ElementAt(i).NO_NIVEL)
                    {
                        vResultadoVista.Add(vVistaNivelacion.ElementAt(i));
                    }
                }
                var vXelementss = vResultadoVista.Select(x =>
                                               new XElement("NIVEL",
                                               new XAttribute("ID_TABULADOR_PUESTO", x.ID_TABULADOR_PUESTO),
                                               new XAttribute("NO_NIVEL", x.NO_NIVEL)
                                    ));
                XElement NIVELACION =
                new XElement("NIVELES", vXelementss
                );

                TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                E_RESULTADO vResultado = nTabulador.InsertaActualizaTabuladorValuacion(vIdTabulador, TABULADORVALUACION.ToString(), NIVELACION.ToString(), pGuardar, vClUsuario, vNbPrograma);

                if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.WARNING))
                {
                    List<E_MENSAJE_COMPETENCIA> MensajeCompetencias = new List<E_MENSAJE_COMPETENCIA>();
                    XElement vDatosRespuesta = vResultado.ObtieneDatosRespuesta();
                    foreach (XElement vXmlDatos in vDatosRespuesta.Elements("DATOS"))
                    {
                        MensajeCompetencias.Add(new E_MENSAJE_COMPETENCIA
                        {
                            NB_PUESTO = UtilXML.ValorAtributo<string>(vXmlDatos.Attribute("NB_PUESTO")),
                            NB_COMPETENCIA = UtilXML.ValorAtributo<string>(vXmlDatos.Attribute("NB_COMPETENCIA"))
                        });
                    }
                    string competencias = string.Join(",", MensajeCompetencias.Select(p => p.NB_COMPETENCIA + " " + p.NB_PUESTO));
                    string vMensajeError = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE + "<br>" + " " + competencias;
                    UtilMensajes.MensajeDB(rwmMensaje, vMensajeError, vResultado.CL_TIPO_ERROR, 400, 200, pCallBackFunction: "GuardarWindow");
                }
                else
                {
                    E_RESULTADO vResultadoRecalcula = nTabulador.ActualizaNivelPuesto(vIdTabulador, vClUsuario, vNbPrograma);
                    if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
                    {
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        bool vCerrarVentana = pFgCerrarVentana;
                        string vCallBackFunction = vCerrarVentana ? "closeWindow" : "OnCloseWindow";
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);
                        grdValuacion.Rebind();
                    }
                    else
                    {
                        string vMensajeRecalcula = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensajeRecalcula, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");
                        grdValuacion.Rebind();
                    }
                }
            }
            else
            {

                foreach (GridDataItem item in grdValuacion.MasterTableView.Items)
                {
                    vIdTabuladorPuesto = (int.Parse(item.GetDataKeyValue("ID_TABULADOR_PUESTO").ToString()));
                    vIdPuesto = (int.Parse(item.GetDataKeyValue("ID_PUESTO").ToString()));

                    vValor = (RadNumericTextBox)item["NO_NIVEL"].FindControl("NO_NIVEL");

                    vValuacionPuestos.Add(new E_VALUACION_PUESTO { ID_PUESTO = vIdPuesto, ID_TABULADOR_PUESTO = vIdTabuladorPuesto, NO_NIVEL = (int)vValor.Value });
                }

                var vXelementss = vValuacionPuestos.Select(x => new XElement("NIVEL",
                                                      new XAttribute("ID_TABULADOR_PUESTO", x.ID_TABULADOR_PUESTO),
                                                      new XAttribute("NO_NIVEL", x.NO_NIVEL)
                                           ));
                XElement NIVELACION =
                new XElement("NIVELES", vXelementss
                );

                TabuladoresNegocio nTabulador = new TabuladoresNegocio();
                E_RESULTADO vResultado = nTabulador.InsertarActualizarNivelesPuestos(vIdTabulador, NIVELACION.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                bool vCerrarVentana = pFgCerrarVentana;
                string vCallBackFunction = vCerrarVentana ? "closeWindow" : "";
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: vCallBackFunction);

            }

        }

        protected void ramGuardar_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string pParameter = e.Argument;
            if (pParameter == "GUARDAR")
            {

                GuardarPuesto(false, pParameter);
            }
            else if (pParameter != "NO_NIVEL")
            {
                vIdNivelFactor = int.Parse(pParameter);
                RadValuacion.Rebind();
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

            UDTT_ARCHIVO excel = obtieneValuacionPuestos(vIdTabulador, txtClTabulador.InnerText, txtNbTabulador.InnerText);

            if (excel.FI_ARCHIVO.Length != 0)
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + excel.NB_ARCHIVO);
                Response.BinaryWrite(excel.FI_ARCHIVO);
                Response.Flush();
                Response.End();
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "No hay datos para exportar.", E_TIPO_RESPUESTA_DB.WARNING, 400, 150, null);
            }
            //TabuladoresNegocio nTabulador = new TabuladoresNegocio();
            //SqlConnection sqlConection = new SqlConnection();
            //if (vIdTabulador != null) 
            //{
            //    try
            //    {
            //        string cadenaConexion = ConfigurationManager.ConnectionStrings["SistemaSigeinEntities"].ConnectionString.Substring(ConfigurationManager.ConnectionStrings["SistemaSigeinEntities"].ConnectionString.IndexOf("connection string=", 0) + 19).Substring(0, ConfigurationManager.ConnectionStrings["SistemaSigeinEntities"].ConnectionString.Substring(ConfigurationManager.ConnectionStrings["SistemaSigeinEntities"].ConnectionString.IndexOf("connection string=", 0) + 19).Length - 1);
            //        sqlConection.ConnectionString = cadenaConexion;

            //        SqlCommand mySqlCommand = sqlConection.CreateCommand();
            //        mySqlCommand.CommandText = "MC.SPE_OBTIENE_VALUACION_PUESTO_PIVOT";

            //        mySqlCommand.CommandType = CommandType.StoredProcedure;
            //        mySqlCommand.Parameters.Add("@PIN_ID_TABULADOR", SqlDbType.Int).Value = vIdTabulador;

            //        SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
            //        mySqlDataAdapter.SelectCommand = mySqlCommand;
            //        DataSet myDataSet = new DataSet();
            //        sqlConection.Open();
            //        mySqlDataAdapter.Fill(myDataSet);
            //        sqlConection.Close();

            //        DataTable dt = new DataTable();
            //        if (myDataSet.Tables.Count > 0)
            //        {
            //            dt = myDataSet.Tables[0];
            //            ExportarAExcel(dt);
            //        }
            //    }
            //    catch (Exception ex) { sqlConection.Close(); }
            //}
        }

        //public void ExportarAExcel(DataTable dataTable)
        //{
        //    Stream newStream = null;
        //    using (ExcelPackage excelPackage = new ExcelPackage(newStream ?? new MemoryStream()))
        //    {
        //        excelPackage.Workbook.Properties.Author = "Sigein 5.0";
        //        excelPackage.Workbook.Properties.Title = "Valuacion de puestos";
        //        excelPackage.Workbook.Properties.Comments = "Sigein 5.0";

        //        excelPackage.Workbook.Worksheets.Add("Valuacion de puestos");
        //        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[1];
        //        worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
        //       // string[] propertyNames = { "No", "Nómina", "Apellidos", "Nombres", "Fecha_Inicio_Servicio", "Opción_Medica" };
        //       // MemberInfo[] membersToInclude = typeof(E_CREDENCIALES)
        //       //.GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //       //.Where(p => propertyNames.Contains(p.Name))
        //       //.ToArray();

        //       // worksheet.Cells[1, 1].LoadFromCollection(lstCredenciales, true, OfficeOpenXml.Table.TableStyles.None, BindingFlags.Instance | BindingFlags.Public, membersToInclude);
        //        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        //       // Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#86909D");
        //       // worksheet.Cells["A1:F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //       // worksheet.Cells["A1:F1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
        //       // worksheet.Cells["A1:F1"].Style.Font.Bold = true;

        //        excelPackage.Save();
        //        newStream = excelPackage.Stream;
        //    }
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment; filename=ValuacionPuestosTabuladores.xlsx");
        //    Response.BinaryWrite(((MemoryStream)newStream).ToArray());
        //    Response.End();

        //}


        private void asignarEstiloCelda(ExcelRange celda, string clTipoCompetencia = null, bool border = true, OfficeOpenXml.Style.ExcelHorizontalAlignment alineacion = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left)
        {

            if (border)
            {
                celda.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, System.Drawing.Color.Black);
            }

            if (!string.IsNullOrEmpty(clTipoCompetencia))
            {
                celda.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                if(clTipoCompetencia == "GEN")
                    celda.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#0087CF"));
                else if (clTipoCompetencia == "ESP")
                    celda.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#A52A2A"));
                else if (clTipoCompetencia == "FACTOR")
                    celda.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#E6EAEC"));
            }

            celda.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
            celda.Style.HorizontalAlignment = alineacion;
        }

        public UDTT_ARCHIVO obtieneValuacionPuestos(int pIdTabulador, string pClVersion, string pDsVersion)
        {

            UDTT_ARCHIVO oValuacionPuestos = new UDTT_ARCHIVO();
            Stream newStream = new MemoryStream();
            List<E_VALUACION> vListaFactorsValuacion = new List<E_VALUACION>();

            int vFila = 8;
            int vColumna = 2;
            string vClCelda = "";

            //Creamos el archivo de excel
            using (ExcelPackage pck = new ExcelPackage(newStream))
            {
                var ws = pck.Workbook.Worksheets.Add("ValuacionPuestos");

                ws.Column(1).Width = 50;

                ws.Cells["A3"].Value = "Valuación puestos";
                ws.Cells["A3"].Style.Font.Size = 18;
                ws.Cells["A3"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);

                ws.Cells["A5"].Value = "Versión: " + pClVersion + "     Descripción: " + pDsVersion;
                ws.Cells["A5"].Style.Font.Bold = true;

                ws.Cells["A7"].Value = "Puesto";
                ws.Cells["A7"].Style.Font.Bold = true;

                var vLstCompetencias = (from a in vListaValuacion select new { a.ID_COMPETENCIA, a.NB_COMPETENCIA, a.ID_TABULADOR_FACTOR, a.CL_TIPO_COMPETENCIA }).Distinct().OrderBy(t => t.ID_COMPETENCIA);

                var vLstPuestos = (from a in vListaValuacion
                                   select new
                                   {
                                       a.CL_PUESTO,
                                       a.NB_PUESTO,
                                       a.ID_TABULADOR_PUESTO,
                                       a.ID_PUESTO,
                                       a.NO_PROMEDIO_VALUACION,
                                       a.NO_NIVEL
                                   }).Distinct().OrderByDescending(o => o.NO_PROMEDIO_VALUACION).OrderBy(t => t.NO_NIVEL);


                foreach (var item in vLstCompetencias.Where(t => t.CL_TIPO_COMPETENCIA == "GEN"))
                {

                    E_VALUACION f = new E_VALUACION
                    {
                        ID_COMPETENCIA = item.ID_COMPETENCIA,
                        NB_COMPETENCIA = item.NB_COMPETENCIA,
                        ID_TABULADOR_FACTOR = item.ID_TABULADOR_FACTOR,
                        CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA
                    };
                    vListaFactorsValuacion.Add(f);

                }

                foreach (var item in vLstCompetencias.Where(t => t.CL_TIPO_COMPETENCIA == "ESP"))
                {
                    E_VALUACION f = new E_VALUACION
                    {
                        ID_COMPETENCIA = item.ID_COMPETENCIA,
                        NB_COMPETENCIA = item.NB_COMPETENCIA,
                        ID_TABULADOR_FACTOR = item.ID_TABULADOR_FACTOR,
                        CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA
                    };
                    vListaFactorsValuacion.Add(f);

                }

                foreach (var item in vLstCompetencias.Where(t => t.CL_TIPO_COMPETENCIA == "FACTOR"))
                {
                    E_VALUACION f = new E_VALUACION
                    {
                        ID_COMPETENCIA = item.ID_COMPETENCIA,
                        NB_COMPETENCIA = item.NB_COMPETENCIA,
                        ID_TABULADOR_FACTOR = item.ID_TABULADOR_FACTOR,
                        CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA
                    };
                    vListaFactorsValuacion.Add(f);

                }


                foreach (var item in vLstPuestos)
                {
                    vClCelda = "A" + vFila.ToString();
                    ws.Cells[vClCelda].Value = item.NB_PUESTO;
                    vFila++;
                }

                foreach (var item in vListaFactorsValuacion)
                {
                    vClCelda = ((char)(vColumna + 64)).ToString()  + "7";
                    ws.Cells[vClCelda].Value = item.NB_COMPETENCIA;
                    ws.Cells[vClCelda].Style.Font.Bold = true;
                    asignarEstiloCelda(ws.Cells[vClCelda], item.CL_TIPO_COMPETENCIA, false);
                    vColumna++;
                }

                ws.Cells[((char)(vColumna + 64)).ToString() + "7"].Value = "Promedio";
                ws.Cells[((char)(vColumna + 64)).ToString() + "7"].Style.Font.Bold = true;
                vColumna++;
                ws.Cells[((char)(vColumna + 64)).ToString() + "7"].Value = "Nivel";
                ws.Cells[((char)(vColumna + 64)).ToString() + "7"].Style.Font.Bold = true;

                int vFilaMatriz = 8;
                int vColumnaMatriz = 2;
                foreach(var item in vLstPuestos)
                {
                    vColumnaMatriz = 2;
                    foreach (var itemCompetencia in vListaFactorsValuacion)
                    {
                        var vValor = vListaValuacion.Where(w => w.ID_COMPETENCIA == itemCompetencia.ID_COMPETENCIA && w.ID_PUESTO == item.ID_PUESTO).FirstOrDefault();
                        if (vValor != null)
                        {
                            vClCelda = ((char)(vColumnaMatriz + 64)).ToString() + vFilaMatriz.ToString();
                            ws.Cells[vClCelda].Value = vValor.NO_VALOR;
                        }
                        else
                        {
                            vClCelda = ((char)(vColumnaMatriz + 64)).ToString() + vFilaMatriz.ToString();
                            ws.Cells[vClCelda].Value = 0;
                        }
                        vColumnaMatriz++;
                    }
                    vFilaMatriz++;
                }

                vFilaMatriz = 8;
                foreach (var item in vLstPuestos)
                {  
                        var vValor = vListaValuacion.Where(w => w.ID_PUESTO == item.ID_PUESTO).FirstOrDefault();
                        if (vValor != null)
                        {
                            vClCelda = ((char)(vColumnaMatriz + 64)).ToString() + vFilaMatriz.ToString();
                            ws.Cells[vClCelda].Value = vValor.NO_PROMEDIO_VALUACION;
                        }
                    
                    vFilaMatriz++;
                }

                vColumnaMatriz++;

                vFilaMatriz = 8;
                foreach (var item in vLstPuestos)
                {
                    var vValor = vListaValuacion.Where(w => w.ID_PUESTO == item.ID_PUESTO).FirstOrDefault();
                    if (vValor != null)
                    {
                        vClCelda = ((char)(vColumnaMatriz + 64)).ToString() + vFilaMatriz.ToString();
                        ws.Cells[vClCelda].Value = vValor.NO_NIVEL;
                    }

                    vFilaMatriz++;
                }


                pck.Save();
                newStream = pck.Stream;
            }

            oValuacionPuestos.NB_ARCHIVO = "ValuacionPuestos.xlsx";
            oValuacionPuestos.FI_ARCHIVO = ((MemoryStream)newStream).ToArray();

            return oValuacionPuestos;

        }

        //protected void btnRecalcularNivel_Click(object sender, EventArgs e)
        //{
        //    TabuladoresNegocio nTabulador = new TabuladoresNegocio();
        //    E_RESULTADO vResultado = nTabulador.ActualizaNivelPuesto(vIdTabulador, vClUsuario, vNbPrograma);
        //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
        //    UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "OnCloseWindow");

        //    grdValuacion.Rebind();
        //}
    }
}