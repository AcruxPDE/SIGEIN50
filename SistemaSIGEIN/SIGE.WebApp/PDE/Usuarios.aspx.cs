using Newtonsoft.Json;
using SIGE.Entidades.Externas;
using SIGE.Entidades.EvaluacionOrganizacional;
using SIGE.Negocio.EvaluacionOrganizacional;
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
using SIGE.Entidades;
using Telerik.Web.UI;
using WebApp.Comunes;
using SIGE.Negocio.Utilerias;

namespace SIGE.WebApp.PDE
{
    public partial class Usuarios : System.Web.UI.Page
    {
        private string vClUsuario;
        private string vNbPrograma;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private int? vIdRol;

        
        private XElement SeleccionMetasEvaluado { get; set; }
        private XElement RESULTADOS { get; set; }
 
        public int vIdPeriodo
        {
            get { return (int)ViewState["vsIdPeriodo"]; }
            set { ViewState["vsIdPeriodo"] = value; }
        }
        public string vClOrigenPeriodo
        {
            get { return (string)ViewState["vs_vClOrigenPeriodo"]; }
            set { ViewState["vs_vClOrigenPeriodo"] = value; }
        }
        public int? vNoReplica
        {
            get { return (int?)ViewState["vs_vNoReplica"]; }
            set { ViewState["vs_vNoReplica"] = value; }
        }
        public string vClTipoMetas
        {
            get { return (string)ViewState["vs_vClTipoMetas"]; }
            set { ViewState["vs_vClTipoMetas"] = value; }
        }
        protected void AgregarEvaluadosPorEmpleado(string pEvaluados)
        {
            List<E_SELECTOR_EMPLEADO> vEvaluados = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(pEvaluados);

            if (vEvaluados.Count > 0)
                AgregarEvaluados(new XElement("EMPLEADOS", vEvaluados.Select(s => new XElement("EMPLEADO", new XAttribute("ID_EMPLEADO", s.idEmpleado)))));
        }

        protected void AgregarEvaluadosPorPuesto(string pPuestos)
        {
            List<E_SELECTOR_PUESTO> vPuestos = JsonConvert.DeserializeObject<List<E_SELECTOR_PUESTO>>(pPuestos);

            if (vPuestos.Count > 0)
                AgregarEvaluados(new XElement("PUESTOS", vPuestos.Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.idPuesto)))));
        }

        protected void AgregarEvaluadosPorArea(string pAreas)
        {
            List<E_SELECTOR_AREA> vAreas = JsonConvert.DeserializeObject<List<E_SELECTOR_AREA>>(pAreas);

            if (vAreas.Count > 0)
                AgregarEvaluados(new XElement("AREAS", vAreas.Select(s => new XElement("AREA", new XAttribute("ID_AREA", s.idArea)))));
        }

        protected void AgregarEvaluadores(string pEvaluadores)
        {

        }
        protected void AgregarEvaluados(XElement pXmlElementos)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            E_RESULTADO vResultado = nPeriodo.InsertaEvaluados(vIdPeriodo, pXmlElementos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            if (vResultado.CL_TIPO_ERROR == E_TIPO_RESPUESTA_DB.SUCCESSFUL)
            {
                grdEvaluados.Rebind();

                // grdContrasenaEvaluadores.Rebind();



            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "");
            }
        }

        protected void SeguridadProcesos(bool? pFgConfiguracionCompleta)
        {
            bool vFgConfiguracionCompleta = false;
            if (pFgConfiguracionCompleta == true)
                vFgConfiguracionCompleta = true;


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            if (!IsPostBack)
            {
                if (Request.Params["PeriodoId"] != null)
                {
                    vIdPeriodo = int.Parse(Request.QueryString["PeriodoId"]);
                    CargarDatos();
                }
                else
                {
                    vIdPeriodo = 0;
                }
            }
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void AgregarEvaluadores(XElement pXmlElementos)
        {
            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            XElement vXmlEvaluados = new XElement("EVALUADOS");
            foreach (GridDataItem item in grdEvaluados.SelectedItems)
                vXmlEvaluados.Add(new XElement("EVALUADO", new XAttribute("ID_EVALUADO", item.GetDataKeyValue("ID_EMPLEADO").ToString())));

            E_RESULTADO vResultado = nPeriodo.InsertaEvaluadoresOtro(vIdPeriodo, vXmlEvaluados, pXmlElementos, vClUsuario, vNbPrograma);
            string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            
        }

        protected void grdUsuarios_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            //UsuarioNegocio nUsuarios = new UsuarioNegocio();
            //grdUsuarios.DataSource = nUsuarios.ObtieneUsuarios(null);

            PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            grdEvaluados.DataSource = nPeriodo.ObtieneEvaluados(pIdPeriodo: vIdPeriodo, pIdRol: vIdRol);
        }

        protected void ramConfiguracionPeriodo_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "EVALUADO")
                AgregarEvaluadosPorEmpleado(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "PUESTO")
                AgregarEvaluadosPorPuesto(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "AREA")
                AgregarEvaluadosPorArea(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "EVALUADOR")
                AgregarEvaluadores(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "VERIFICACONFIGURACION")
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                var vFgConfigurado = nPeriodo.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
                if (vFgConfigurado != null)
                    SeguridadProcesos(vFgConfigurado.FG_ESTATUS);
            }
        }
        private void CargarDatos()
        {


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //UsuarioNegocio nUsuario = new UsuarioNegocio();

            //foreach (GridDataItem item in grdUsuarios.SelectedItems)
            //{
            //    E_RESULTADO vResultado = nUsuario.EliminaUsuario(item.GetDataKeyValue("CL_USUARIO").ToString(), vClUsuario, vNbPrograma);
            //    string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

            //    UtilMensajes.MensajeResultadoDB(rwmAlertas, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            //}
        }
        protected void grdEvaluados_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                XElement vXmlEvaluados = new XElement("EVALUADORES");

                GridDataItem item = e.Item as GridDataItem;
                vXmlEvaluados.Add(new XElement("EVALUADOR", new XAttribute("ID_EVALUADO", item.GetDataKeyValue("ID_EVALUADO").ToString()),
                                                            new XAttribute("ID_EVALUADOR", item.GetDataKeyValue("ID_EVALUADOR").ToString())));

                E_RESULTADO vResultado = nPeriodo.EliminaEvaluadorEvaluado(vIdPeriodo, vXmlEvaluados, vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "recargarEvaluados()");

            }

            if (e.CommandName == RadGrid.ExpandCollapseCommandName)
            {
                foreach (GridItem item in e.Item.OwnerTableView.Items)
                {
                    if (item.Expanded && item != e.Item)
                    {
                        item.Expanded = false;
                    }
                }
            }
        }

        protected void grdEvaluados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem && e.Item.OwnerTableView.Name != "gtvEvaluadores")
            {
                GridDataItem item = (GridDataItem)e.Item;

                string vClEstadoEmpleado = item.GetDataKeyValue("CL_ESTADO_EMPLEADO").ToString();
                int vIdEvaluado = int.Parse(item.GetDataKeyValue("ID_EVALUADO").ToString());
                if (vClEstadoEmpleado != null && vClEstadoEmpleado != "Alta")
                {
                    //item["CL_ESTADO_EMPLEADO"].Text = "<a href='javascript:OpenRemplazaBaja(" + vIdEvaluado + "," + vIdPeriodo + ")'>" + vClEstadoEmpleado + "</a>";
                    item["CL_ESTADO_EMPLEADO"].Text = vClEstadoEmpleado;
                }

            }
        }

        protected void grdEvaluados_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem vDataItem = (GridDataItem)e.DetailTableView.ParentItem;

            if (e.DetailTableView.Name == "gtvEvaluadores")
            {
                int vIdEmpleado;
                PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
                vIdEmpleado = int.Parse(vDataItem.GetDataKeyValue("ID_EVALUADO").ToString());
                e.DetailTableView.DataSource = nPeriodo.ObtieneEvaluadoresPorEvaluado(vIdPeriodo, vIdEmpleado);
            }
        }

      


    }
}