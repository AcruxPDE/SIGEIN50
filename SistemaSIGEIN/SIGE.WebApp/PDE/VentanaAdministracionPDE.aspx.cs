using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;

namespace SIGE.WebApp.PDE
{
    public partial class VentanaAdministracionPDE : System.Web.UI.Page
    {
        public string vNbPrograma;
        public string vClUsuario;
        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public string vUsuarioSeleccion
        {
            set { ViewState["vs_vUsuarioSeleccion"] = value; }
            get { return (string)ViewState["vs_vUsuarioSeleccion"]; }
        }
        private List<E_EMPLEADO_PDE> ListaEmpleados
        {
            get { return (List<E_EMPLEADO_PDE>)ViewState["vs_cg_lista_empleados"]; }
            set { ViewState["vs_cg_lista_empleados"] = value; }
        }

        List<E_OBTIENE_PUESTO_EMPLEADOS> vEmpleadosSeleccionados
        {
            get { return (List<E_OBTIENE_PUESTO_EMPLEADOS>)ViewState["vs_vEmpleadosSeleccionados"]; }
            set { ViewState["vs_vEmpleadosSeleccionados"] = value; }
        }

        public string vNbPuesto
        {
            get { return (string)ViewState["vs_vNbPuesto"]; }
            set { ViewState["vs_vNbPuesto"] = value; }

        }
        private XElement vIdempleados { get; set; }
        private XElement vSeleccion { get; set; }
        private XElement SELECCIONEMPLEADOS { get; set; }
        private List<string> ListaPuestos
        {
            get { return (List<string>)ViewState["vs_vListaPuestos"]; }
            set { ViewState["vs_vListaPuestos"] = value; }
        }
        private string xmlPuestoEmpleado;
        protected void Page_Load(object sender, EventArgs e)
        {
            vUsuarioSeleccion = "";
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vEmpleadosSeleccionados = new List<E_OBTIENE_PUESTO_EMPLEADOS>();
            if (!IsPostBack)
            {
                ConfiguracionNotificacionNegocio negocio = new ConfiguracionNotificacionNegocio();
                ListaEmpleados = new List<E_EMPLEADO_PDE>();
            xmlPuestoEmpleado = negocio.ObtienePuestoEmpleado(null, null);
            XElement root = XElement.Parse(xmlPuestoEmpleado);
            foreach (XElement name in root.Elements("EMPLEADO"))
            {
                E_EMPLEADO_PDE em = new E_EMPLEADO_PDE
                         {
                             ID_EMPLEADO = name.Attribute("ID_EMPLEADO").Value,
                             ID_PUESTO = name.Attribute("ID_PUESTO").Value,
                             NB_PUESTO = name.Attribute("NB_PUESTO").Value,
                             NB_EMPLEADO = name.Attribute("NB_EMPLEADO").Value,
                             M_CL_USUARIO = name.Attribute("CL_USUARIO").Value,
                         };
                ListaEmpleados.Add(em);
            }
           


            }

            grdEmpleadosSeleccionados.DataSource = ListaEmpleados;

        }



        protected void btnSeleccion_Click(object sender, EventArgs e)
        {
            string vIDUsuario;
            string vIDEmpleado;
            string vIdPuesto;
            vEmpleadosSeleccionados = new List<E_OBTIENE_PUESTO_EMPLEADOS>();

            foreach (GridDataItem item in grdEmpleadosSeleccionados.MasterTableView.Items)
            {

                vIDEmpleado = (item.GetDataKeyValue("ID_EMPLEADO").ToString());
                vIDUsuario = item.GetDataKeyValue("M_CL_USUARIO") == null ? "" : (item.GetDataKeyValue("M_CL_USUARIO").ToString());
                vIdPuesto = item.GetDataKeyValue("ID_PUESTO") == null ? "" : (item.GetDataKeyValue("ID_PUESTO").ToString());
                vNbPuesto = item.GetDataKeyValue("NB_PUESTO") == null ? "" : (item.GetDataKeyValue("NB_PUESTO").ToString());

                vEmpleadosSeleccionados.Add(new E_OBTIENE_PUESTO_EMPLEADOS
                {
                    ID_EMPLEADO = vIDEmpleado,
                    CL_USUARIO = vIDUsuario,
                    ID_PUESTO = vIdPuesto,
                    NB_PUESTO = vNbPuesto
               
                });

                var vXelements = vEmpleadosSeleccionados.Select(x =>
                                         new XElement("EMPLEADO",
                                         new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO),
                                         new XAttribute("CL_USUARIO", x.CL_USUARIO),
                                          new XAttribute("ID_PUESTO", x.ID_PUESTO),
                                         new XAttribute("NB_PUESTO", x.NB_PUESTO)
                              ));
                SELECCIONEMPLEADOS =
                new XElement("SELECCION", vXelements
                );
            }

            if (SELECCIONEMPLEADOS != null)
            {
                ConfiguracionNotificacionNegocio puesto = new ConfiguracionNotificacionNegocio();
                E_RESULTADO vResultado = puesto.INSERTA_ACTUALIZA_EMPLEADO_PUESTO_PDE(SELECCIONEMPLEADOS.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "onCloseWindow");

            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rwmMensaje, "Selecciona un puesto", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

            }
        }

        protected void grdRoles_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {

            RolNegocio nRol = new RolNegocio();
            grdRoles.DataSource = nRol.ObtieneRoles(null);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            RolNegocio nRol = new RolNegocio();

            foreach (GridDataItem item in grdRoles.SelectedItems)
            {
                E_RESULTADO vResultado = nRol.EliminaRol(int.Parse(item.GetDataKeyValue("ID_ROL").ToString()), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, vResultado.CL_TIPO_ERROR, pCallBackFunction: "onCloseWindow");
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "PUESTO")
                AgregarSeleccionadosPorPuesto(vSeleccion.oSeleccion.ToString());


            if (vSeleccion.clTipo == "USUARIO")
                AgregarSeleccionadosPorUsuario(vSeleccion.oSeleccion.ToString());
        }
        protected void AgregarSeleccionadosPorPuesto(string pPuestos)
        {
            List<E_SELECTOR_PUESTO_PDE> vPuestos = JsonConvert.DeserializeObject<List<E_SELECTOR_PUESTO_PDE>>(pPuestos);
            vSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "FYD_PUESTO")));
            vUsuarioSeleccion = "";
            if (vPuestos.Count > 0)
            {
                vSeleccion.Element("FILTRO").Add(vPuestos.Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.idPuesto_pde))));


                AgregarSeleccionados(vSeleccion);

            }
        }

        protected void AgregarSeleccionadosPorUsuario(string pUsuario)
        {
            List<E_SELECTOR_USUARIO> vUsuario = JsonConvert.DeserializeObject<List<E_SELECTOR_USUARIO>>(pUsuario);
            vSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "FYD_USUARIO")));
            vUsuarioSeleccion = "seleccionUsuario";

            if (vUsuario.Count > 0)
            {
                vSeleccion.Element("FILTRO").Add(vUsuario.Select(s => new XElement("USUARIO", new XAttribute("CL_USUARIO", s.idUsuario))));
                AgregarSeleccionados(vSeleccion);
            }
        }

        protected void AgregarSeleccionados(XElement pXmlElementos)
        {
            ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
            List<SPE_OBTIENE_EMPLEADOS_PDE_Result> lista = neg.ObtenerEmpleados_PDE(pXmlElementos);
            foreach (SPE_OBTIENE_EMPLEADOS_PDE_Result item in lista)
            {
                E_EMPLEADO_PDE emp;
                if (vUsuarioSeleccion == "")
                {
                    emp = ListaEmpleados.Where(t => t.ID_EMPLEADO == item.M_EMPLEADO_ID_EMPLEADO_PDE).FirstOrDefault();
                }
                else
                {

                    emp = ListaEmpleados.Where(t => t.M_CL_USUARIO  == item.M_EMPLEADO_CL_EMPLEADO).FirstOrDefault();

                }

                if (emp == null)
                {
                    E_EMPLEADO_PDE e = new E_EMPLEADO_PDE
                    {
                        ID_EMPLEADO = item.M_EMPLEADO_ID_EMPLEADO_PDE,
                        CL_EMPLEADO = item.M_EMPLEADO_CL_EMPLEADO,
                        NB_EMPLEADO = item.M_EMPLEADO_NB_EMPLEADO_COMPLETO,
                        ID_DEPARTAMENTO = item.M_DEPARTAMENTO_ID_DEPARTAMENTO_PDE,
                        ID_PUESTO = item.M_PUESTO_ID_PUESTO_PDE,
                        NB_PUESTO = item.M_PUESTO_NB_PUESTO,
                        NB_DEPARTAMENTO = item.M_DEPARTAMENTO_NB_DEPARTAMENTO,
                        M_CL_USUARIO = (item.M_CL_USUARIO == null ? null : item.M_CL_USUARIO)
                    };

                    ListaEmpleados.Add(e);
                }

            }

            grdEmpleadosSeleccionados.Rebind();


        }

        protected void grdEmpleadosSeleccionados_ItemCommand(object sender, GridCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {

                GridDataItem item = e.Item as GridDataItem;
                EliminarEmpleado(item.GetDataKeyValue("ID_EMPLEADO").ToString());

            }
        }

        protected void grdEmpleadosSeleccionados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdEmpleadosSeleccionados.DataSource = ListaEmpleados;
        }

      
        private void EliminarEmpleado(string pIdEmpleado)
        {

            E_EMPLEADO_PDE e = ListaEmpleados.Where(t => t.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

            if (e != null)
            {
                ListaEmpleados.Remove(e);
            }

        }



    }
}