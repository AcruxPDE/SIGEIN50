using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.FYD
{
    public partial class MatrizPlaneacionCuestionarios : System.Web.UI.Page
    {

        #region Variables
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;

        public int vIdPeriodo
        {
            get { return (int)ViewState["vs_v_id_periodo"]; }
            set { ViewState["vs_v_id_periodo"] = value; }
        }

        private List<E_PLANEACION_CUESTINOARIOS> vLstPlaneacion
        {
            get { return (List<E_PLANEACION_CUESTINOARIOS>)ViewState["vs_lst_planeacion"]; }
            set { ViewState["vs_lst_planeacion"] = value; }
        }


        #endregion

        #region Funciones

        private void CrearCuestionarios()
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaCuestionarios(vIdPeriodo, false, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
            {
                CargarDatos();
                if (vLstPlaneacion.Where(w => w.FG_CUESTIONARIO == true).Count() < 1)
                {
                    btnCrearCuestionarios.Enabled = false;
                    btnRegistroAutorizacion.Enabled = false;
                }

            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: null);
            }
        }

        private void CargarDatos()
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            vLstPlaneacion = nPeriodo.ObtienePlaneacionMatriz(vIdPeriodo, "NA", "NA");

            SeleccionarChechkbox("AUTOEVALUACION");
            SeleccionarChechkbox("SUPERIOR");
            SeleccionarChechkbox("SUBORDINADO");
            SeleccionarChechkbox("INTERRELACIONADO");
            SeleccionarChechkbox("OTRO");

        }

        private void SeleccionarChechkbox(string pClRol)
        {
            int vTotalPorRol = 0;
            int vTotalSeleccionados = 0;

            vTotalPorRol = vLstPlaneacion.Where(t => t.CL_ROL_EVALUADOR == pClRol).Count();
            vTotalSeleccionados = vLstPlaneacion.Where(t => t.CL_ROL_EVALUADOR == pClRol & t.FG_CUESTIONARIO).Count();
            if (vTotalPorRol > 0)
            {
                if (vTotalPorRol == vTotalSeleccionados)
                {
                    switch (pClRol)
                    {
                        case "AUTOEVALUACION":
                            chkAutoevaluacion.Checked = true;
                            break;
                        case "SUPERIOR":
                            chkSuperior.Checked = true;
                            break;
                        case "SUBORDINADO":
                            chkSubordinado.Checked = true;
                            break;
                        case "INTERRELACIONADO":
                            chkInter.Checked = true;
                            break;
                        case "OTRO":
                            chkOtro.Checked = true;
                            break;
                    }
                }
            }

        }

        private DataTable CrearDataTableEvaluacion(int vIdEmpleado, int vIdEvaluado)
        {

            DataTable dtCuestionarios = new DataTable();

            dtCuestionarios.Columns.Add("ID_EVALUADO");
            dtCuestionarios.Columns.Add("AUTOEVALUACION");
            dtCuestionarios.Columns.Add("SUPERIOR");
            dtCuestionarios.Columns.Add("SUBORDINADO");
            dtCuestionarios.Columns.Add("INTERRELACIONADO");
            dtCuestionarios.Columns.Add("OTROS");

            StringBuilder vSbAuto = new StringBuilder();
            StringBuilder vSbSup = new StringBuilder();
            StringBuilder vSbSub = new StringBuilder();
            StringBuilder vSbInter = new StringBuilder();
            StringBuilder vSbOtros = new StringBuilder();

            string vNameCheck = "";
            string vIsChecked = "";

            string vPlantillaDivs = " <table border=\"0\" style=\"width:100%; padding:3px;\"> " +
                                        "<tr>" +
                                            "<td style=\"width:90%;\" title=\"{0}\">" +
                                                "<div class=\"{3}\"> " +
                                                    "<span>{1}<br /> " +
                                                    "<span style=\"font-weight: bold;\">{2}</span>" +
                                                    "</span> " +
                                                " </div> " +
                                                " <div style=\"clear:both; height: 2px;\"></div>" +
                                            "</td>" +
                                            "<td style=\"width:10%;\">" +
                                            "<div class=\"checkc\">" +
                                            "<input type=\"checkbox\" name=\"{4}\" {5} class=\"{6}\" />" +
                                            "</div> " +
                                            "</td>" +
                                        "</tr>" +
                                    "</table>";

            var vListaEvaluado = vLstPlaneacion.Where(t => t.ID_EMPLEADO_EVALUADO == vIdEmpleado).ToList();

            foreach (var item in vListaEvaluado)
            {
                switch (item.CL_ROL_EVALUADOR)
                {
                    case "AUTOEVALUACION":

                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",AE";
                        vIsChecked = item.FG_CUESTIONARIO ? "checked" : "";

                        vSbAuto.Append(string.Format(vPlantillaDivs, "Clave empledo: " + item.CL_EMPLEADO + ", Clave puesto: " + item.CL_PUESTO, item.NB_EMPLEADO_COMPLETO, item.NB_PUESTO, item.CL_ROL_EVALUADOR, vNameCheck, vIsChecked, "AE"));
                        break;

                    case "SUPERIOR":
                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",SP";
                        vIsChecked = item.FG_CUESTIONARIO ? "checked" : "";

                        vSbSup.Append(string.Format(vPlantillaDivs, "Clave empledo: " + item.CL_EMPLEADO + ", Clave puesto: " + item.CL_PUESTO, item.NB_EMPLEADO_COMPLETO, item.NB_PUESTO, item.CL_ROL_EVALUADOR, vNameCheck, vIsChecked, "SP"));
                        break;

                    case "SUBORDINADO":
                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",SB";
                        vIsChecked = item.FG_CUESTIONARIO ? "checked" : "";

                        vSbSub.Append(string.Format(vPlantillaDivs, "Clave empledo: " + item.CL_EMPLEADO + ", Clave puesto: " + item.CL_PUESTO, item.NB_EMPLEADO_COMPLETO, item.NB_PUESTO, item.CL_ROL_EVALUADOR, vNameCheck, vIsChecked, "SB"));
                        break;

                    case "INTERRELACIONADO":
                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",IN";
                        vIsChecked = item.FG_CUESTIONARIO ? "checked" : "";

                        vSbInter.Append(string.Format(vPlantillaDivs, "Clave empledo: " + item.CL_EMPLEADO + ", Clave puesto: " + item.CL_PUESTO, item.NB_EMPLEADO_COMPLETO, item.NB_PUESTO, item.CL_ROL_EVALUADOR, vNameCheck, vIsChecked, "IN"));
                        break;

                    case "OTRO":
                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",OT";
                        vIsChecked = item.FG_CUESTIONARIO ? "checked" : "";
                        vSbOtros.Append(string.Format(vPlantillaDivs,"Clave empledo: " + item.CL_EMPLEADO +", Clave puesto: "+item.CL_PUESTO, item.NB_EMPLEADO_COMPLETO, item.NB_PUESTO, item.CL_ROL_EVALUADOR, vNameCheck, vIsChecked, "OT"));
                        break;

                }
            }

            dtCuestionarios.Rows.Add(vIdEmpleado, vSbAuto.ToString(), vSbSup.ToString(), vSbSub.ToString(), vSbInter.ToString(), vSbOtros.ToString());

            return dtCuestionarios;

        }

        private void ObtenerDatosCheckbox()
        {
            string vNameCheck = "";

            foreach (E_PLANEACION_CUESTINOARIOS item in vLstPlaneacion)
            {
                switch (item.CL_ROL_EVALUADOR)
                {
                    case "AUTOEVALUACION":

                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",AE";
                        break;

                    case "SUPERIOR":
                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",SP";
                        break;

                    case "SUBORDINADO":
                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",SB";
                        break;

                    case "INTERRELACIONADO":
                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",IN";
                        break;

                    case "OTRO":
                        vNameCheck = item.ID_EVALUADO.ToString() + "," + item.ID_EMPLEADO_EVALUADOR.ToString() + ",OT";
                        break;
                }


                if (Request.Form[vNameCheck] != null)
                {
                    //if (!item.FG_CUESTIONARIO)
                    //{
                    //    item.CL_ACCION = "INSERT";
                    //}

                    item.FG_CUESTIONARIO = true;
                }
                else
                {
                    //if (item.FG_CUESTIONARIO)
                    //{
                    //    item.CL_ACCION = "DELETE";
                    //}
                    item.FG_CUESTIONARIO = false;
                }

            }
        }

        private void Guardar(bool pFgCrearCuestionarios)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();

            XElement vXmlMatriz = new XElement("MATRIZ");

            vXmlMatriz.Add(vLstPlaneacion.Select(t =>
                new XElement("CUESTIONARIO",
                new XAttribute("ID_EVALUADO_EVALUADOR", t.ID_EVALUADO_EVALUADOR),
                new XAttribute("FG_CREAR_CUESTIONARIO", t.FG_CUESTIONARIO)
                    //new XAttribute("CL_ROL_EVALUADOR", t.CL_ROL_EVALUADOR),
                    //new XAttribute("ID_EMPLEADO_EVALUADO", t.ID_EMPLEADO_EVALUADO),
                    //new XAttribute("ID_EVALUADO", t.ID_EVALUADO),
                    //new XAttribute("FG_CUESTIONARIO", t.FG_CUESTIONARIO),
                    //new XAttribute("CL_ACCION", t.CL_ACCION)
                )));

            E_RESULTADO vRespuesta = nPeriodo.InsertaActualizaCuestionariosMatriz(vIdPeriodo, vXmlMatriz.ToString(), pFgCrearCuestionarios, vClUsuario, vNbPrograma);

            string vMensaje = vRespuesta.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (pFgCrearCuestionarios & vRespuesta.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: "generateDataForParent");
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vRespuesta.CL_TIPO_ERROR, pCallBackFunction: null);
            }

            //if (vResultado.CL_TIPO_ERROR.Equals(E_TIPO_RESPUESTA_DB.SUCCESSFUL))
        }

        private void AgregarEmpleados(E_SELECTOR_MATRIZ pSeleccion)
        {
            PeriodoNegocio neg = new PeriodoNegocio();
            XElement pXmlSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO")));
            List<E_SELECTOR_EVALUADOR> vLstEvaluadores = JsonConvert.DeserializeObject<List<E_SELECTOR_EVALUADOR>>(pSeleccion.oEvaluador.ToString());
            List<E_SELECTOR_EVALUADO> vLstEvaluados = JsonConvert.DeserializeObject<List<E_SELECTOR_EVALUADO>>(pSeleccion.oEvaluado.ToString());

            foreach (E_SELECTOR_EVALUADOR vIdEmpleadoEvaluador in vLstEvaluadores)
            {
                pXmlSeleccion.Element("FILTRO").Add(new XElement("EMP", new XAttribute("ID_EMPLEADO", vIdEmpleadoEvaluador.idEvaluador)));
            }

            List<SPE_OBTIENE_EMPLEADOS_Result> vListaEmpleados = neg.ObtenerEmpleados(pXmlSeleccion);

            foreach (E_SELECTOR_EVALUADO item in vLstEvaluados)
            {

                foreach (SPE_OBTIENE_EMPLEADOS_Result iEmp in vListaEmpleados)
                {

                    var oCuestionario = vLstPlaneacion.Where(t => t.ID_EVALUADO == item.idEvaluado & t.ID_EMPLEADO_EVALUADOR == iEmp.M_EMPLEADO_ID_EMPLEADO).FirstOrDefault();

                    if (oCuestionario == null)
                    {

                        var vIdEmpleadoEvaluado = vLstPlaneacion.Where(t => t.ID_EVALUADO == item.idEvaluado).FirstOrDefault().ID_EMPLEADO_EVALUADO;

                        E_PLANEACION_CUESTINOARIOS vNuevoCuestionario = new E_PLANEACION_CUESTINOARIOS();
                        vNuevoCuestionario.CL_EMPLEADO = iEmp.M_EMPLEADO_CL_EMPLEADO;
                        vNuevoCuestionario.CL_PUESTO = iEmp.M_PUESTO_CL_PUESTO;
                        vNuevoCuestionario.CL_ROL_EVALUADOR = pSeleccion.clRolEvaluador;
                        vNuevoCuestionario.FG_CUESTIONARIO = false;
                        vNuevoCuestionario.ID_EMPLEADO_EVALUADO = vIdEmpleadoEvaluado;
                        vNuevoCuestionario.ID_EMPLEADO_EVALUADOR = iEmp.M_EMPLEADO_ID_EMPLEADO;
                        vNuevoCuestionario.ID_EVALUADO = item.idEvaluado;
                        vNuevoCuestionario.ID_PUESTO = iEmp.M_PUESTO_ID_PUESTO.Value;
                        vNuevoCuestionario.NB_EMPLEADO_COMPLETO = iEmp.M_EMPLEADO_NB_EMPLEADO_COMPLETO;
                        vNuevoCuestionario.NB_PUESTO = iEmp.M_PUESTO_NB_PUESTO;


                        vLstPlaneacion.Add(vNuevoCuestionario);
                    }

                }
            }
            grdCuestionarios.Rebind();
            //grdCuestionarios.MasterTableView.DetailTables[0].Rebind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;

            if (!Page.IsPostBack)
            {
                //vFgCuestionariosGuardados = false;

                if (Request.Params["IdPeriodo"] != null)
                {
                    vIdPeriodo = int.Parse(Request.Params["IdPeriodo"].ToString());
                    CrearCuestionarios();
                }
            }
        }

        protected void grdCuestionarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PeriodoNegocio nPeriodo = new PeriodoNegocio();
            grdCuestionarios.DataSource = nPeriodo.ObtieneEvaluados(vIdPeriodo);
        }

        protected void grdCuestionarios_DeleteCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void grdCuestionarios_DetailTableDataBind(object sender, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;

            switch (e.DetailTableView.Name)
            {
                case "gtvEvaluadores":

                    int vIdEmpleado = int.Parse(vDataItem.GetDataKeyValue("ID_EMPLEADO").ToString());
                    int vIdEvaluado = int.Parse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString());

                    e.DetailTableView.DataSource = CrearDataTableEvaluacion(vIdEmpleado, vIdEvaluado);
                    break;
            }
        }

        protected void grdCuestionarios_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
            {
                GridDataItem item = e.Item as GridDataItem;

                (item["ExapandColumn"].Controls[0]).Visible = false;

            }
            //if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "ParentGrid")
            //{
            //    // your logic should come here
            //}
            //else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "gtvEvaluadores")
            //{
            //    // your logic should come here

            //    //GridDataItem vDataItem = (GridDataItem)e.Item;

            //    //int vIdEvaluado = int.Parse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString());

            //    //(vDataItem.FindControl("grdAutoevaluador") as RadGrid).DataSource = vLstPlaneacion.Where(t => t.CL_ROL_EVALUADOR == "AUTOEVALUACION" & t.ID_EMPLEADO_EVALUADO == vIdEvaluado);



            //}
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ObtenerDatosCheckbox();
            Guardar(false);
            CargarDatos();
            //grdCuestionarios.MasterTableView.DetailTables[0].Rebind();
            btnRegistroAutorizacion.Enabled = true;
            btnCrearCuestionarios.Enabled = true;
            grdCuestionarios.Rebind();
        }

        protected void RadButton1_Click(object sender, EventArgs e)
        {
            //grdCuestionarios.MasterTableView.DetailTables[0].Rebind();
        }

        protected void ramPLaneacion_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //E_SELECTOR_MATRIZ vSeleccion = new E_SELECTOR_MATRIZ();
            //string pParameter = e.Argument;

            //if (pParameter != null)
            //{

            //    vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR_MATRIZ>(pParameter);
            //    AgregarEmpleados(vSeleccion);
            //}
            CargarDatos();
            grdCuestionarios.Rebind();
        }

        protected void btnAgregarEmpleado_Click(object sender, EventArgs e)
        {
            //ObtenerDatosCheckbox();
            //Guardar(false);

            ////"=<%= vIdPeriodo %>", "", "Agregar evaluadores"
            //string ventana = "window.radopen('AgregarCuestionario.aspx?PeriodoId=" + vIdPeriodo + "&AccionCerrarCl=REBIND&FgCrearCuestionarios=true'";
            //string AbrirVentana = ventana + ",'winAgregarCuestionario', document.documentElement.clientWidth - 10, document.documentElement.clientHeight - 10);";

            //Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), AbrirVentana, true);
        }

        protected void btnCrearCuestionarios_Click(object sender, EventArgs e)
        {
            ObtenerDatosCheckbox();
            Guardar(true);
        }
    }
}