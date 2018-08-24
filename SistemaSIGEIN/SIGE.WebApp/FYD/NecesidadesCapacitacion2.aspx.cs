using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class NecesidadesCapacitacion2 : System.Web.UI.Page
    {
        //TODO: NO PERTENECE A ESTA PANTALLA. Reducir el tamaño de la columna de los porcentajes en la tabla de compatibilidad 

        #region Propiedades

        private string vClUsuario;
        private string vNbPrograma;
        private bool vFgCargarGrid
        {
            set { ViewState["vs_nc_fg_cargar_grid"] = value; }
            get { return (bool)ViewState["vs_nc_fg_cargar_grid"]; }
        }
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdPeriodo
        {
            set { ViewState["vs_nc_id_periodo"] = value; }
            get { return (int)ViewState["vs_nc_id_periodo"]; }
        }

        private int? vIdDepartamento
        {
            set { ViewState["vs_nc_id_departamento"] = value; }
            get { return (int?)ViewState["vs_nc_id_departamento"]; }
        }

        private string vNbDepartamento
        {
            set { ViewState["vs_nc_nb_departamento"] = value; }
            get { return (string)ViewState["vs_nc_nb_departamento"]; }
        }

        private int? vIdPrograma
        {
            get { return (int?)ViewState["vs_nc_id_programa"]; }
            set { ViewState["vs_nc_id_programa"] = value; }
        }

        private string prioridades
        {
            get { return (String)ViewState["vs_nc_prioridades"]; }
            set { ViewState["vs_nc_prioridades"] = value; }
        }

        private string vListaEmpleadoCompetencia
        {
            get { return (String)ViewState["vs_nc_empleado_competencia"]; }
            set { ViewState["vs_nc_empleado_competencia"] = value; }
        }

        private bool vFgAlta {
            get { return (bool)ViewState["vs_nc_fg_alta"]; }
            set { ViewState["vs_nc_fg_alta"] = value; }
        }

        private bool vFgIntermedia
        {
            get { return (bool)ViewState["vs_nc_fg_intermedia"]; }
            set { ViewState["vs_nc_fg_intermedia"] = value; }
        }

        private bool vFgNonecesaria
        {
            get { return (bool)ViewState["vs_nc_fg_nonecesaria"]; }
            set { ViewState["vs_nc_fg_nonecesaria"] = value; }
        }

        XElement xmlPrioridades;
        XElement xmlCapacitaciones;


        private List<E_NECESIDADES_CAPACITACION> vLstDnc
        {
            get { return (List<E_NECESIDADES_CAPACITACION>)ViewState["vs_nc_lst_dnc"]; }
            set { ViewState["vs_nc_lst_dnc"] = value; }
        }

        #endregion

        #region Metodos

        private void ObtenerDatosCheckbox()
        {
            string nombre;
            foreach (E_NECESIDADES_CAPACITACION item in vLstDnc)
            {
                nombre = "C" + item.ID_COMPETENCIA.ToString() + "E" + item.ID_EMPLEADO.ToString();

                if (Request.Form[nombre] != null)
                {
                    vListaEmpleadoCompetencia = vListaEmpleadoCompetencia + Request.Form[nombre] + ",";
                }
            }
        }

        private void CargarGrid()
        {
            //grdCapacitacion.DataSource = null;
            //grdCapacitacion.MasterTableView.Columns.Clear();

            //if (Request.Params["PeriodoId"] != null)
            //{
            //    vIdPeriodo = int.Parse(Request.Params["PeriodoId"].ToString());
            //}

            //if (vFgAlta != null)
            //{
            //    vFgAlta = true;
            //}


            vFgCargarGrid = true;

            //if (lstDepartamento.Items[0].Value != "")
            //{
            //    vIdDepartamento = int.Parse(lstDepartamento.Items[0].Value);
            //}
            //else
            //{
            //    vIdDepartamento = null;
            //}

            xmlPrioridades = new XElement("PRIORIDADES");

            if (vFgAlta)
            {
                xmlPrioridades.Add(new XElement("PRIORIDAD", new XAttribute("NOMBRE", "Alta")));
            }

            if (vFgIntermedia)
            {
                xmlPrioridades.Add(new XElement("PRIORIDAD", new XAttribute("NOMBRE", "Intermedia")));
            }

            if (vFgNonecesaria)
            {
                xmlPrioridades.Add(new XElement("PRIORIDAD", new XAttribute("NOMBRE", "No Necesaria")));
            }

            if (xmlPrioridades.HasElements)
            {
                prioridades = xmlPrioridades.ToString();
            }


            grdCapacitacion.Rebind();
        }

        private bool ValidarDatos(E_TIPO_OPERACION_DB vCltrans)
        {
            bool vAceptado = true;
            string vMensaje = "";

            if (vCltrans == E_TIPO_OPERACION_DB.I)
            {
                if (txtClavePrograma.Text == "" & vAceptado)
                {
                    vMensaje = "El campo clave de programa es obligatorio";
                    vAceptado = false;
                }

                if (txtNombrePrograma.Text == "" & vAceptado)
                {
                    vMensaje = "El campo nombre de programa es obligatorio";
                    vAceptado = false;
                }
            }
            else
            {
                if (lstProgramas.Items[0].Value == "")
                {
                    vMensaje = "Seleccione un programa";
                    vAceptado = false;
                }
            }



            //if (grdCapacitacion.SelectedItems.Count == 0 & vAceptado)
            //{
            //    vMensaje = "Seleccione al menos una empleado de la lista";
            //    vAceptado = false;
            //}

            if (!vAceptado)
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: null);
            }

            return vAceptado;
        }

        private void GuardarPrograma(E_SELECTOR_NECESIDADES pSeleccionados)
        {
            List<E_NECESIDADES_CAPACITACION> vListaSeleccion = ObtenerSeleccionados(pSeleccionados);
            NecesidadesCapacitacionNegocio neg = new NecesidadesCapacitacionNegocio();
            xmlCapacitaciones = new XElement("CAPACITACIONES");

            xmlCapacitaciones.Add(new XAttribute("CL_PROGRAMA", txtClavePrograma.Text), new XAttribute("NB_PROGRAMA", txtNombrePrograma.Text), new XAttribute("ID_PERIODO", vIdPeriodo + ""));

            foreach (E_NECESIDADES_CAPACITACION item in vListaSeleccion)
            {
                xmlCapacitaciones.Add(new XElement("CAPACITACION",
                new XAttribute("ID_EMPLEADO", item.ID_EMPLEADO.ToString()),
                new XAttribute("NB_EMPLEADO", item.NB_EVALUADO),
                new XAttribute("CL_EMPLEADO", item.CL_EVALUADO),
                new XAttribute("CL_PUESTO", item.CL_PUESTO),
                new XAttribute("NB_PUESTO", item.NB_PUESTO),
                new XAttribute("NB_DEPARTAMENTO", item.NB_DEPARTAMENTO),
                new XAttribute("ID_COMPETENCIA", item.ID_COMPETENCIA),
                new XAttribute("NB_COMPETENCIA", item.NB_COMPETENCIA),
                new XAttribute("NB_CLASIFICACION", item.CL_CLASIFICACION),
                new XAttribute("NB_CATEGORIA", item.CL_TIPO_COMPETENCIA),
                new XAttribute("CL_PRIORIDAD", item.NB_PRIORIDAD),
                new XAttribute("PR_RESULTADO", item.PR_RESULTADO)));
            }


            E_RESULTADO res = neg.InsertaActualizaProgramaDesdeDNC(vIdPrograma, xmlCapacitaciones.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = res.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, res.CL_TIPO_ERROR);
        }

        private void ActualizarPrograma(E_SELECTOR_NECESIDADES pSeleccionados)
        {
           
            vIdPrograma = pSeleccionados.idPrograma;
            List<E_NECESIDADES_CAPACITACION> vListaSeleccion = ObtenerSeleccionados(pSeleccionados);

            NecesidadesCapacitacionNegocio neg = new NecesidadesCapacitacionNegocio();
            xmlCapacitaciones = new XElement("CAPACITACIONES");

            foreach (E_NECESIDADES_CAPACITACION item in vListaSeleccion)
            {
                xmlCapacitaciones.Add(new XElement("CAPACITACION",
                new XAttribute("ID_EMPLEADO", item.ID_EMPLEADO),
                new XAttribute("NB_EMPLEADO", item.NB_EVALUADO),
                new XAttribute("CL_EMPLEADO", item.CL_EVALUADO),
                new XAttribute("CL_PUESTO", item.CL_PUESTO),
                new XAttribute("NB_PUESTO", item.NB_PUESTO),
                new XAttribute("NB_DEPARTAMENTO", item.NB_DEPARTAMENTO),
                new XAttribute("ID_COMPETENCIA", item.ID_COMPETENCIA),
                new XAttribute("NB_COMPETENCIA", item.NB_COMPETENCIA),
                new XAttribute("NB_CLASIFICACION", item.CL_CLASIFICACION),
                new XAttribute("NB_CATEGORIA", item.CL_TIPO_COMPETENCIA),
                new XAttribute("CL_PRIORIDAD", item.NB_PRIORIDAD),
                new XAttribute("PR_RESULTADO", item.PR_RESULTADO)));
            }

            E_RESULTADO res = neg.InsertaActualizaProgramaDesdeDNC(vIdPrograma, xmlCapacitaciones.ToString(), vClUsuario, vNbPrograma);
            string vMensaje = res.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
            UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, res.CL_TIPO_ERROR);

        }

        private void GenerarExcel()
        {
            NecesidadesCapacitacionNegocio neg = new NecesidadesCapacitacionNegocio();
            string prioridades = null;

            //if (lstDepartamento.Items[0].Value != "")
            //{
            //    vIdDepartamento = int.Parse(lstDepartamento.Items[0].Value);
            //    vNbDepartamento = lstDepartamento.Items[0].Text;
            //}
            //else
            //{
            //    vIdDepartamento = null;
            //}

            xmlPrioridades = new XElement("PRIORIDADES");

            if (chkAlta.Checked)
            {
                xmlPrioridades.Add(new XElement("PRIORIDAD", new XAttribute("NOMBRE", "Alta")));
            }

            if (chkIntermedia.Checked)
            {
                xmlPrioridades.Add(new XElement("PRIORIDAD", new XAttribute("NOMBRE", "Intermedia")));
            }

            if (chkNoNecesaria.Checked)
            {
                xmlPrioridades.Add(new XElement("PRIORIDAD", new XAttribute("NOMBRE", "No Necesaria")));
            }

            if (xmlPrioridades.HasElements)
            {
                prioridades = xmlPrioridades.ToString();
            }

            UDTT_ARCHIVO excel = neg.ExportarDatosExcel(vIdPeriodo, vIdDepartamento, vNbDepartamento, prioridades);

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + excel.NB_ARCHIVO);
            Response.BinaryWrite(excel.FI_ARCHIVO);
            Response.Flush();
            Response.End();

        }

        private void ConfigurarColumna(GridColumn pColumna, int pWidth, string pEncabezado, bool pVisible, bool pGenerarEncabezado, bool pFiltrarColumna)
        {
            if (pGenerarEncabezado)
            {
                pEncabezado = GeneraEncabezado(pColumna);
            }

            pColumna.HeaderStyle.Width = Unit.Pixel(pWidth);
            pColumna.HeaderStyle.Font.Bold = true;
            pColumna.HeaderText = pEncabezado;
            pColumna.Visible = pVisible;

            if (pFiltrarColumna & pVisible)
            {
                if (pWidth <= 60)
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth);
                }
                else
                {
                    (pColumna as GridBoundColumn).FilterControlWidth = Unit.Pixel(pWidth - 60);
                }
            }
            else
            {
                (pColumna as GridBoundColumn).AllowFiltering = false;
            }
        }

        private string GeneraEncabezado(GridColumn pColumna)
        {
            int vResultado;
            string vEncabezado = "";
            string vEmpleado = pColumna.UniqueName.ToString().Substring(0, pColumna.UniqueName.ToString().IndexOf('E'));

            if (int.TryParse(vEmpleado, out vResultado))
            {
                var vDatosEmpleado = vLstDnc.Where(t => t.ID_EMPLEADO == vResultado).FirstOrDefault();

                if (vDatosEmpleado != null)
                {
                    vEncabezado = "<div style=\"text-align:center;\"> <input type=\"checkbox\" runat=\"server\" class=\"empleado\" name=\"" + vDatosEmpleado.ID_EMPLEADO.ToString() + "\" value=\"" + vDatosEmpleado.ID_EMPLEADO.ToString() + "\" onchange=\"SeleccionaEmpleado(this);\"> " + vDatosEmpleado.CL_PUESTO + "<br />" + vDatosEmpleado.NB_PUESTO + "<br />" + vDatosEmpleado.CL_EVALUADO + "<br />" + vDatosEmpleado.NB_EVALUADO + "</div>";
                }
            }

            return vEncabezado;
        }

        private List<E_NECESIDADES_CAPACITACION> ObtenerSeleccionados(E_SELECTOR_NECESIDADES pSeleccionados)
        {
            List<E_NECESIDADES_CAPACITACION> vListaSeleccionados = new List<E_NECESIDADES_CAPACITACION>();

            int vIdCompetencia;
            int vIdEmpleado;

            int vIndexCompetencia;
            int vIndexEmpleado;

            //string[] vListaCE = vListaEmpleadoCompetencia.Split(',');

            foreach (E_SELECTOR_SELECCION item in pSeleccionados.oSeleccion)
            {
                if (item.control != "")
                {
                    vIndexCompetencia = item.control.IndexOf('C');
                    vIndexEmpleado = item.control.IndexOf("E");

                    vIdCompetencia = int.Parse(item.control.Substring(vIndexCompetencia + 1, vIndexEmpleado - 1));
                    vIdEmpleado = int.Parse(item.control.Substring(vIndexEmpleado + 1, item.control.Length - (vIndexEmpleado + 1)));

                    E_NECESIDADES_CAPACITACION oSel = vLstDnc.Where(t => t.ID_EMPLEADO == vIdEmpleado & t.ID_COMPETENCIA == vIdCompetencia).FirstOrDefault();

                    if (oSel != null)
                    {
                        vListaSeleccionados.Add(oSel);
                    }
                    else
                    {
                        E_NECESIDADES_CAPACITACION oNecesidadNueva = new E_NECESIDADES_CAPACITACION();

                        oNecesidadNueva.ID_COMPETENCIA = vIdCompetencia;
                        oNecesidadNueva.ID_EMPLEADO = vIdEmpleado;
                        oNecesidadNueva.PR_RESULTADO = 0;
                        oNecesidadNueva.CL_CLASIFICACION = "";
                        oNecesidadNueva.CL_COLOR = "";
                        oNecesidadNueva.CL_DEPARTAMENTO = "";
                        oNecesidadNueva.CL_EVALUADO = "";
                        oNecesidadNueva.CL_PUESTO = "";
                        oNecesidadNueva.CL_TIPO_COMPETENCIA = "";
                        oNecesidadNueva.DS_COMPETENCIA = "";
                        oNecesidadNueva.ID_PERIODO = vIdPeriodo;
                        oNecesidadNueva.ID_PUESTO = 0;
                        oNecesidadNueva.NB_CLASIFICACION_COMPETENCIA = "";
                        oNecesidadNueva.NB_COMPETENCIA = "";
                        oNecesidadNueva.NB_DEPARTAMENTO = "";
                        oNecesidadNueva.NB_EVALUADO = "";
                        oNecesidadNueva.NB_PRIORIDAD = "ALTA";
                        oNecesidadNueva.NB_PUESTO = "";
                        oNecesidadNueva.NB_TIPO_COMPETENCIA = "";
                        

                        vListaSeleccionados.Add(oNecesidadNueva);
                    }
                }
            }

            return vListaSeleccionados;
        }

        #endregion

        //protected void Page_Init(object sender, System.EventArgs e)
        //{
        //    if (!Page.IsPostBack)
        //    {
        //        vFgAlta = true;
        //        vFgIntermedia = true;
        //        vFgNonecesaria = false;
        //    }
        //    //else
        //    //{
        //    //    vFgAlta = true;
        //    //    vFgIntermedia = false;
        //    //    vFgNonecesaria = false;
        //    //}
        //    ////vFgAlta = chkAlta.Checked;
        //    ////vFgIntermedia = chkIntermedia.Checked;
        //    ////vFgNonecesaria = chkNoNecesaria.Checked;
        //    CargarGrid();
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
            vListaEmpleadoCompetencia = "";

            vFgAlta = chkAlta.Checked;
            vFgIntermedia = chkIntermedia.Checked;
            vFgNonecesaria = chkNoNecesaria.Checked;

            if (!Page.IsPostBack)
            {
                vIdPeriodo = 0;
                vIdPrograma = null;
                vFgCargarGrid = false;

                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["PeriodoId"].ToString());

                    SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result vPeriodo = new SPE_OBTIENE_FYD_PERIODO_EVALUACION_Result();
                    PeriodoNegocio oPeriodo = new PeriodoNegocio();

                    vPeriodo = oPeriodo.ObtienePeriodoEvaluacion(vIdPeriodo);

                    if (vPeriodo != null)
                    {
                        vFgCargarGrid = true;
                        txtPeriodo.InnerText = vPeriodo.CL_PERIODO;
                        txtNombrePeriodo.InnerText = vPeriodo.DS_PERIODO;
                        vFgAlta = chkAlta.Checked;
                        vFgIntermedia = chkIntermedia.Checked;
                        vFgNonecesaria = chkNoNecesaria.Checked;
                        CargarGrid();
                    }

                }
            }
        }

        protected void grdCapacitacion_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            List<E_NECESIDADES_CAPACITACION> vListaTemporal = new List<E_NECESIDADES_CAPACITACION>();
            NecesidadesCapacitacionNegocio neg = new NecesidadesCapacitacionNegocio();
            //grdCapacitacion.Columns.Clear();
            if (vFgCargarGrid)
            {
                grdCapacitacion.DataSource = neg.ObtieneNecesidadesCapacitacionPivot(vIdPeriodo, vIdDepartamento, prioridades, ref vListaTemporal);
                vLstDnc = vListaTemporal;
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            //grdCapacitacion.DataSource = null;
            //grdCapacitacion.Rebind();
            vFgAlta = chkAlta.Checked;
            vFgIntermedia = chkIntermedia.Checked;
            vFgNonecesaria = chkNoNecesaria.Checked;
            CargarGrid();

        }

        protected void btnGuardarPrograma_Click(object sender, EventArgs e)
        {

            // GuardarPrograma();
        }

        protected void btnAgregarPrograma_Click(object sender, EventArgs e)
        {
            //ActualizarPrograma();
        }

        protected void btnFormatoExcel_Click(object sender, EventArgs e)
        {
            GenerarExcel();
        }

        protected void grdCapacitacion_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            switch (e.Column.UniqueName)
            {
                case "ID_COMPETENCIA":
                    ConfigurarColumna(e.Column, 10, "No Competencia", false, false, true);
                    break;

                case "NB_TIPO_COMPETENCIA":
                    ConfigurarColumna(e.Column, 100, "Categoría", true, false, true);
                    break;

                case "NB_CLASIFICACION_COMPETENCIA":
                    ConfigurarColumna(e.Column, 200, "Clasificación", true, false, true);
                    break;

                case "NB_COMPETENCIA":
                    ConfigurarColumna(e.Column, 200, "Competencia", true, false, true);
                    break;
                case "ExpandColumn": break;
                default:
                    ConfigurarColumna(e.Column, 200, "", true, true, false);
                    break;
            }
        }

        protected void grdCapacitacion_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void grdCapacitacion_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem gridItem = e.Item as GridDataItem;

                int vIdCompetencia = int.Parse(gridItem.GetDataKeyValue("ID_COMPETENCIA").ToString());
                string vDsCompetencia = vLstDnc.Where(t => t.ID_COMPETENCIA == vIdCompetencia).FirstOrDefault().DS_COMPETENCIA;

                gridItem["NB_COMPETENCIA"].ToolTip = vDsCompetencia;
            }
        }

        protected void ramNecesidades_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            string pParameter = e.Argument;
            E_SELECTOR_NECESIDADES vSeleccion = new E_SELECTOR_NECESIDADES();

            if (pParameter != null)
            {
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR_NECESIDADES>(pParameter);

                if (vSeleccion.clTipoAccion.Equals("INSERTAR"))
                {
                    GuardarPrograma(vSeleccion);
                }
                else
                {
                    ActualizarPrograma(vSeleccion);
                }

                
            }
        }

        protected void chkNoNecesaria_Click(object sender, EventArgs e)
        {
           // vFgAlta = chkAlta.Checked;
        }

        protected void chkIntermedia_CheckedChanged(object sender, EventArgs e)
        {
           // vFgIntermedia = chkIntermedia.Checked;
        }

        protected void chkNoNecesaria_CheckedChanged(object sender, EventArgs e)
        {
           // vFgNonecesaria = chkNoNecesaria.Checked;
        }

        protected void chkAlta_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    public class E_SELECTOR_SELECCION {
        public string control { get; set; }
    }

    public class E_SELECTOR_NECESIDADES
    {
        public string clTipoAccion { get; set; }
        public int idPrograma { get; set; }
        public string clPrograma { get; set; }
        public string nbPrograma { get; set; }
        public List<E_SELECTOR_SELECCION> oSeleccion { get; set; }
    }

}