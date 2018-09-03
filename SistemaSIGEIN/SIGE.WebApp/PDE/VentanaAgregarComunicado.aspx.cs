using Newtonsoft.Json;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.FormacionDesarrollo;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.Administracion;
using SIGE.Negocio.AdministracionSitio;
using SIGE.Negocio.FormacionDesarrollo;
using SIGE.Negocio.PuntoDeEncuentro;
using SIGE.WebApp.Comunes;
using SIGE.WebApp.FYD;
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
    public partial class VentanaAgregarComunicado : System.Web.UI.Page
    {

        #region Variables

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        private XElement vIdempleados { get; set; }
        private XElement vSeleccion { get; set; }
        private XElement SELECCIONEMPLEADOS { get; set; }
        private XElement SELECCIONEMPLEADOSINFO { get; set; }

        public int? vIdComunicado
        {
            set { ViewState["vs_IdComunicado"] = value; }
            get { return (int?)ViewState["vs_IdComunicado"]; }
        }

        public string vTipoComunicado
        {
            set { ViewState["vs_vTipoComunicado"] = value; }
            get { return (string)ViewState["vs_vTipoComunicado"]; }
        }

        public string vTipoAccion
        {
            set { ViewState["vs_vTipoAccion"] = value; }
            get { return (string)ViewState["vs_vTipoAccion"]; }
        }

        public string vRepetidos
        {
            set { ViewState["vs_vRepetidos"] = value; }
            get { return (string)ViewState["vs_vRepetidos"]; }
        }


        public string vAsignados
        {
            set { ViewState["vs_vAsignados"] = value; }
            get { return (string)ViewState["vs_vAsignados"]; }
        }


        public string vIdEmpleadoValida
        {
            set { ViewState["vs_vIdEmpleadoValida"] = value; }
            get { return (string)ViewState["vs_vIdEmpleadoValida"]; }
        }

        public int? vIdArchivo
        {
            set { ViewState["vs_vIdArchivo"] = value; }
            get { return (int?)ViewState["vs_vIdArchivo"]; }
        }

        public string vTipoTransaccion
        {
            set { ViewState["vs_vTipoTransaccion"] = value; }
            get { return (string)ViewState["vs_vTipoTransaccion"]; }
        }

        public string vName
        {
            set { ViewState["vs_vName"] = value; }
            get { return (string)ViewState["vs_vName"]; }
        }

        public string vNombreArchivo
        {
            set { ViewState["vs_vNombreArchivo"] = value; }
            get { return (string)ViewState["vs_vNombreArchivo"]; }
        }

        private List<E_OBTIENE_ADM_COMUNICADO> vAdmcomunicados
        {
            get { return (List<E_OBTIENE_ADM_COMUNICADO>)ViewState["vs_vAdmcomunicados"]; }
            set { ViewState["vs_vAdmcomunicados"] = value; }
        }

        private List<E_OBTIENE_EMPLEADOS_COMUNICADO> vEmpleadosSeleccionados
        {
            get { return (List<E_OBTIENE_EMPLEADOS_COMUNICADO>)ViewState["vs_vEmpleadosSeleccionados"]; }
            set { ViewState["vs_vEmpleadosSeleccionados"] = value; }
        }

        private List<E_OBTIENE_EMPLEADOS_COMUNICADO> vEmpleadosSeleccionados_Info
        {
            get { return (List<E_OBTIENE_EMPLEADOS_COMUNICADO>)ViewState["vs_vEmpleadosSeleccionados_Info"]; }
            set { ViewState["vs_vEmpleadosSeleccionados_Info"] = value; }
        }

        private List<E_EMPLEADO_PDE> ListaEmpleados
        {
            get { return (List<E_EMPLEADO_PDE>)ViewState["vs_cg_lista_empleados"]; }
            set { ViewState["vs_cg_lista_empleados"] = value; }
        }

        private List<E_EMPLEADO_PDE> ListaEmpleadosPuestos
        {
            get { return (List<E_EMPLEADO_PDE>)ViewState["vs_cg_ListaEmpleadosPuestos"]; }
            set { ViewState["vs_cg_ListaEmpleadosPuestos"] = value; }
        }


        private List<E_EMPLEADO_PDE> ListaEmpleadosInf
        {
            get { return (List<E_EMPLEADO_PDE>)ViewState["vs_ListaEmpleadosInf"]; }
            set { ViewState["vs_ListaEmpleadosInf"] = value; }
        }

        List<E_CONFIGURACION_NOTIFICACION> vConfiguracionesNotificacion
        {
            get { return (List<E_CONFIGURACION_NOTIFICACION>)ViewState["vs_vConfiguracionesNotificacion"]; }
            set { ViewState["vs_vConfiguracionesNotificacion"] = value; }
        }

        E_DESCRIPCION_NOTIFICACION vNotificacionRegistrar
        {
            get { return (E_DESCRIPCION_NOTIFICACION)ViewState["vs_vConfiguracionNotificacionRegistrar"]; }
            set { ViewState["vs_vConfiguracionNotificacionRegistrar"] = value; }
        }

        public string vUsuarioSeleccion
        {
            set { ViewState["vs_vUsuarioSeleccion"] = value; }
            get { return (string)ViewState["vs_vUsuarioSeleccion"]; }
        }

        #endregion

        protected void AgregarSeleccionPorEmpleado(string pSeleccionados)
        {
            List<E_SELECTOR_EMPLEADO_PDE> vSeleccionados = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO_PDE>>(pSeleccionados);

            vSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "EMPLEADO")));
            vUsuarioSeleccion = "";

            if (vSeleccionados.Count > 0)
            {
                vSeleccion.Element("FILTRO").Add(vSeleccionados.Select(s => new XElement("EMP", new XAttribute("ID_EMPLEADO", s.idEmpleado_pde))));
                if (Tipo1.Checked == true)
                {
                    AgregarSeleccionados(vSeleccion);
                }
                else
                {
                    AgregarSeleccionados_informacion(vSeleccion);
                }
            }
        }

        protected void AgregarSeleccionadosPorPuesto(string pPuestos)
        {
            List<E_SELECTOR_PUESTO_PDE> vPuestos = JsonConvert.DeserializeObject<List<E_SELECTOR_PUESTO_PDE>>(pPuestos);
            vSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "FYD_PUESTO")));
            vUsuarioSeleccion = "";
            if (vPuestos.Count > 0)
            {
                vSeleccion.Element("FILTRO").Add(vPuestos.Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.idPuesto_pde))));
                if (Tipo2.Checked == true)
                {
                    AgregarSeleccionados_puestos(vSeleccion);

                }
                else
                {
                    AgregarSeleccionados(vSeleccion);
                }
            }
        }

        protected void AgregarSeleccionadosPorArea(string pAreas)
        {
            List<E_SELECTOR_AREA> vAreas = JsonConvert.DeserializeObject<List<E_SELECTOR_AREA>>(pAreas);
            vSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "FYD_AREA")));
            vUsuarioSeleccion = "";
            if (vAreas.Count > 0)
            {
                vSeleccion.Element("FILTRO").Add(vAreas.Select(s => new XElement("AREA", new XAttribute("ID_AREA", s.idArea_pde))));
                AgregarSeleccionados(vSeleccion);
            }
        }
        protected void AgregarSeleccionadosPorAdscripcion(string pAdscripcion)
        {
            List<E_SELECTOR_ADSCRIPCION> vAdscripcion = JsonConvert.DeserializeObject<List<E_SELECTOR_ADSCRIPCION>>(pAdscripcion);
            vSeleccion = new XElement("SELECCION", new XElement("FILTRO", new XAttribute("CL_TIPO", "FYD_ADSCRIPCION")));
            vUsuarioSeleccion = "";
            if (vAdscripcion.Count > 0)
            {
                vSeleccion.Element("FILTRO").Add(vAdscripcion.Select(s => new XElement("ADSCRIPCION", new XAttribute("ID", s.idAdscripcion))));
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

                    emp = ListaEmpleados.Where(t => t.CL_EMPLEADO == item.M_EMPLEADO_CL_EMPLEADO).FirstOrDefault();

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

        protected void AgregarSeleccionados_informacion(XElement pXmlElementos)
        {
            ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
            List<SPE_OBTIENE_EMPLEADOS_PDE_Result> lista = neg.ObtenerEmpleados_PDE(pXmlElementos);
            foreach (SPE_OBTIENE_EMPLEADOS_PDE_Result item in lista)
            {
                var emp = ListaEmpleadosInf.Where(t => t.ID_EMPLEADO == item.M_EMPLEADO_ID_EMPLEADO_PDE).FirstOrDefault();

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
                        NB_DEPARTAMENTO = item.M_DEPARTAMENTO_NB_DEPARTAMENTO
                    };

                    ListaEmpleadosInf.Add(e);
                }

            }

            RadGridSeleccion.Rebind();


        }

        protected void AgregarSeleccionados_puestos(XElement pXmlElementos)
        {
            ConsultaGeneralNegocio neg = new ConsultaGeneralNegocio();
            List<SPE_OBTIENE_EMPLEADOS_PDE_Result> lista = neg.ObtenerEmpleados_PDE(pXmlElementos);
            foreach (SPE_OBTIENE_EMPLEADOS_PDE_Result item in lista)
            {
                var emp = ListaEmpleadosInf.Where(t => t.ID_PUESTO == item.M_PUESTO_ID_PUESTO_PDE).FirstOrDefault();

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
                        NB_DEPARTAMENTO = item.M_DEPARTAMENTO_NB_DEPARTAMENTO
                    };

                    ListaEmpleadosInf.Add(e);
                }

            }

            RadGridSeleccion.Rebind();

        }

        private void EliminarEmpleado_Info(string pIdEmpleado)
        {

            E_EMPLEADO_PDE e = ListaEmpleadosInf.Where(t => t.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

            if (e != null)
            {
                ListaEmpleadosInf.Remove(e);
            }

        }

        private void EliminarEmpleado(string pIdEmpleado)
        {

            E_EMPLEADO_PDE e = ListaEmpleados.Where(t => t.ID_EMPLEADO == pIdEmpleado).FirstOrDefault();

            if (e != null)
            {
                ListaEmpleados.Remove(e);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ListaEmpleados = new List<E_EMPLEADO_PDE>();
                ListaEmpleadosInf = new List<E_EMPLEADO_PDE>();
                rbLectura.Checked = true;
                if (Request.Params["IdComunicado"] != null)
                {
                    vTipoComunicado = Request.Params["TipoComunicado"];
                    vTipoAccion = Request.Params["Tipo"];
                    vIdComunicado = int.Parse((Request.Params["IdComunicado"]));
                    vTipoTransaccion = E_TIPO_OPERACION_DB.A.ToString();
                    ListaComunicadosNegocio nComunicado = new ListaComunicadosNegocio();

                    vEmpleadosSeleccionados = nComunicado.ObtenerEmpleadosComunicado(vIdComunicado).ToList();
                    grdEmpleadosSeleccionados.DataSource = vEmpleadosSeleccionados;

                    VisorComunicadoNegocio nLista = new VisorComunicadoNegocio();
                    List<SPE_OBTIENE_LISTA_EMPLEADOS_INFORMACION_Result> ListaEmp = new List<SPE_OBTIENE_LISTA_EMPLEADOS_INFORMACION_Result>();
                    ListaEmp = nLista.ObtenerListaEmpleadosInformacion((int)vIdComunicado);
                    RadGridSeleccion.DataSource = ListaEmp;

                    foreach (E_OBTIENE_EMPLEADOS_COMUNICADO item in vEmpleadosSeleccionados)
                    {
                        E_EMPLEADO_PDE f = new E_EMPLEADO_PDE
                        {
                            ID_EMPLEADO = item.ID_EMPLEADO,
                            NB_EMPLEADO = item.NB_EMPLEADO,
                            NB_PUESTO = item.NB_PUESTO,
                            M_CL_USUARIO = item.M_CL_USUARIO
                        };

                        ListaEmpleados.Add(f);
                    }

                    var vObtieneComunicado = nComunicado.ObtenerComunicados(vIdComunicado).ToList().FirstOrDefault();
                    txtTituloComunicado.Text = vObtieneComunicado.NB_COMUNICADO;
                    txtContenido.Content = vObtieneComunicado.DS_COMUNICADO;
                    rdtiIniciofecha.SelectedDate = vObtieneComunicado.FE_VISIBLE_DEL;
                    rdtFinfecha.MaxDate = new DateTime(2200, 12, 20);
                    rdtFinfecha.SelectedDate = vObtieneComunicado.FE_VISIBLE_AL;
                    vIdArchivo = vObtieneComunicado.ID_ARCHIVO_PDE;
                    vNombreArchivo = vObtieneComunicado.NB_ARCHIVO;


                    if (vObtieneComunicado.FG_PRIVADO == "Privado")
                    {
                        rcbprivado.Checked = true;
                    }
                    if (vIdArchivo != null)
                    {
                        rcbEliminarAdjunto.Visible = true;
                        ArchivosAdjuntos.Visible = true;
                    }
                    else
                    {
                        vIdArchivo = 0;
                    }



                    if (vTipoComunicado != "null")
                    {
                        if (vTipoAccion == "E")
                        {
                            rbEditar.Checked = true;
                        }
                        else { rbLectura.Checked = true; }

                        rbLectura.Visible = true;
                        rbEditar.Visible = true;
                        rbLectura.Enabled = false;
                        rbEditar.Enabled = false;
                        RadGridSeleccion.Visible = true;
                        rcbAuto.Visible = true;
                        rbHabilitar.Visible = true;
                        rbAgregar2.Visible = false;
                        rbAgregar.Visible = true;
                        RadGridSeleccion.Enabled = false;
                        rcbAuto.Enabled = true;
                        rbHabilitar.Enabled = false;
                        rbAgregar.Enabled = true;
                        rcbprivado.Enabled = false;
                        grdEmpleadosSeleccionados.Enabled = false;
                        btnSeleccionarAdscripcion.Enabled = false;
                        btnSeleccionarAreas.Enabled = false;
                        btnSeleccionarEmpleados.Enabled = false;
                        btnSeleccionarPuestos.Enabled = false;
                        btnSeleccionUsuario.Enabled = false;


                        if (vTipoComunicado == "I")
                        {
                            Tipo3.Checked = true;
                            Tipo1.Checked = false;
                            Tipo2.Checked = false;
                            Tipo1.Enabled = false;
                            Tipo2.Enabled = false;
                            Tipo3.Enabled = false;
                            btnPuestos.Visible = false;
                            btnSeleccion.Visible = true;
                            btnSeleccion.Enabled = false;

                        }
                        else if (vTipoComunicado == "D")
                        {

                            Tipo3.Checked = false;
                            Tipo1.Checked = false;
                            Tipo2.Checked = true;
                            Tipo1.Enabled = false;
                            Tipo2.Enabled = false;
                            Tipo3.Enabled = false;
                            btnPuestos.Visible = true;
                            btnSeleccion.Visible = false;
                            btnPuestos.Enabled = false;
                            RadGridSeleccion.MasterTableView.Columns.FindByUniqueName("NB_EMPLEADO").Visible = false;
                        }
                    }
                    else
                    {
                        Tipo3.Checked = false;
                        Tipo1.Checked = true;
                        Tipo2.Checked = false;
                        Tipo1.Enabled = false;
                        Tipo2.Enabled = false;
                        Tipo3.Enabled = false;
                        btnPuestos.Visible = false;
                        btnSeleccion.Visible = false;
                    }



                }
                else
                {
                    vIdArchivo = 0;
                    vIdComunicado = 0;
                    vTipoTransaccion = E_TIPO_OPERACION_DB.I.ToString();
                    rdtiIniciofecha.SelectedDate = DateTime.Now.Date;
                    rdtFinfecha.SelectedDate = DateTime.Now.AddDays(1);
                }

                ConfiguracionNotificacionNegocio negocio = new ConfiguracionNotificacionNegocio();
                var notificacion = negocio.ObtenerNotificaciones();
                parseNotificarConfiguracion(notificacion);
                if (vConfiguracionesNotificacion != null)
                {
                    XElement notificaciones = XElement.Parse(vConfiguracionesNotificacion.FirstOrDefault().XML_INSTRUCCION);
                    DeserializarDocumentoAutorizar(notificaciones);
                    if (vNotificacionRegistrar != null)
                    {

                        //pIns.Text = vNotificacionRegistrar.DS_INSTRUCCION.ToString();
                        pIns.Text = vNotificacionRegistrar.DS_INSTRUCCION.ToString();
                    }
                }

                AdscripcionesNegocio negocioa = new AdscripcionesNegocio();
                string adscripcionVisible = negocioa.SeleccionaAdscripcion().ToString();
                if (adscripcionVisible != "No")
                {
                    btnSeleccionarAdscripcion.Visible = true;
                }
                else
                {
                    btnSeleccionarAdscripcion.Visible = false;
                }

                vUsuarioSeleccion = "";

            }

        }

        public void parseNotificarConfiguracion(List<SPE_OBTIENE_CONFIGURACION_NOTIFICACION_Result> lista)
        {
            vConfiguracionesNotificacion = new List<E_CONFIGURACION_NOTIFICACION>();
            foreach (SPE_OBTIENE_CONFIGURACION_NOTIFICACION_Result item in lista)
            {
                vConfiguracionesNotificacion.Add(new E_CONFIGURACION_NOTIFICACION
                {
                    ID_CONFIGURACION_NOTIFICACION = item.ID_CONFIGURACION_NOTIFICACION
                    ,
                    XML_INSTRUCCION = item.XML_INSTRUCCION
                });
            }
        }

        public void DeserializarDocumentoAutorizar(XElement tablas)
        {
            if (ValidarRamaXml(tablas, "COMUNICADOS"))
            {
                vNotificacionRegistrar = tablas.Element("COMUNICADOS").Elements("INSTRUCCION").Select(el => new E_DESCRIPCION_NOTIFICACION
                {
                    DS_INSTRUCCION = el.Attribute("DS_INSTRUCCION").Value
                }).FirstOrDefault();
            }

        }

        public Boolean ValidarRamaXml(XElement parentEl, string elementsName)
        {
            var foundEl = parentEl.Element(elementsName);

            if (foundEl != null)
            {
                return true;
            }

            return false;
        }

        protected void grdEmpleadosSeleccionados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

            grdEmpleadosSeleccionados.DataSource = ListaEmpleados;
            if (ListaEmpleados.Count() >= 1)
            {
                rcbAuto.Enabled = true;
                if (ListaEmpleados.Count() > 1)
                {
                    btnSeleccion.Enabled = false;
                }
                else
                {
                    btnSeleccion.Enabled = true ;
                }
                btnPuestos.Enabled = true;
            }
            else
            {
                rcbAuto.Enabled = false;
                btnSeleccion.Enabled = false;
                btnPuestos.Enabled = false;
            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, Telerik.Web.UI.AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "EMPLEADO")
                AgregarSeleccionPorEmpleado(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "PUESTO")
                AgregarSeleccionadosPorPuesto(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "DEPARTAMENTO")
                AgregarSeleccionadosPorArea(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "ADSCRIPCION")
                AgregarSeleccionadosPorAdscripcion(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "USUARIO")
                AgregarSeleccionadosPorUsuario(vSeleccion.oSeleccion.ToString());
        }

        protected void grdEmpleadosSeleccionados_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {

                GridDataItem item = e.Item as GridDataItem;
                EliminarEmpleado(item.GetDataKeyValue("ID_EMPLEADO").ToString());

            }


        }

        protected void rbAgregar_Click(object sender, EventArgs e)
        {
            DateTime vFechainicial;
            DateTime vFechafinal;
            byte[] data = null;
            byte? vPrivado = 0;
            string vIDUsuario;
            string vIDEmpleado;

            ListaComunicadosNegocio nComunicado = new ListaComunicadosNegocio();
            vEmpleadosSeleccionados = new List<E_OBTIENE_EMPLEADOS_COMUNICADO>();

            foreach (GridDataItem item in grdEmpleadosSeleccionados.MasterTableView.Items)
            {

                vIDEmpleado = (item.GetDataKeyValue("ID_EMPLEADO").ToString());
                vIDUsuario = item.GetDataKeyValue("M_CL_USUARIO") == null ? "" : (item.GetDataKeyValue("M_CL_USUARIO").ToString());


                vEmpleadosSeleccionados.Add(new E_OBTIENE_EMPLEADOS_COMUNICADO
                {
                    ID_EMPLEADO = vIDEmpleado,
                    CL_USUARIO = vIDUsuario
                });

                var vXelements = vEmpleadosSeleccionados.Select(x =>
                                         new XElement("EMPLEADO",
                                         new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO),
                                         new XAttribute("CL_USUARIO", x.CL_USUARIO)
                              ));
                SELECCIONEMPLEADOS =
                new XElement("SELECCION", vXelements
                );
            }

            string vTitulocomunicado = txtTituloComunicado.Text;
            string vContenidocomunicado = txtContenido.Content;

            if (rcbprivado.Checked == true)
            {
                vPrivado = 1;
            }


            if (rauArchivo.UploadedFiles.Count > 0)
            {
                data = new byte[rauArchivo.UploadedFiles[0].ContentLength];
                rauArchivo.UploadedFiles[0].InputStream.Read(data, 0, int.Parse(rauArchivo.UploadedFiles[0].ContentLength.ToString()));
                vName = rauArchivo.UploadedFiles[0].FileName.ToString();
            }

            if (rbPermanente.Checked == true)
            {
                rdtFinfecha.Enabled = false;
                vFechainicial = Convert.ToDateTime(rdtiIniciofecha.SelectedDate);
                vFechafinal = DateTime.Now.AddYears(100);
            }
            else
            {
                vFechainicial = Convert.ToDateTime(rdtiIniciofecha.SelectedDate);
                vFechafinal = Convert.ToDateTime(rdtFinfecha.SelectedDate);
            }

            if (rcbEliminarAdjunto.Checked == true)
            {
                if (rauArchivo.UploadedFiles.Count == 0)
                {
                    vIdArchivo = vIdArchivo;
                }
            }



            if (SELECCIONEMPLEADOS != null && vTitulocomunicado != "")
            {
                E_RESULTADO vResultado = nComunicado.InsertaActualizaComunicado((int)vIdArchivo, vName, data, (int)vIdComunicado, vTitulocomunicado, vContenidocomunicado, vFechainicial, vFechafinal, SELECCIONEMPLEADOS.ToString(), vPrivado, ContextoUsuario.oUsuario.CL_USUARIO, ContextoUsuario.nbPrograma, vTipoTransaccion);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "No hay empleados seleccionados y/o comunicado sin titulo", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }



        }

        protected void rbPermanente_Click(object sender, EventArgs e)
        {
            rdtFinfecha.Enabled = false;
        }

        protected void rbRango_Click(object sender, EventArgs e)
        {
            rdtFinfecha.Enabled = true;
        }

        protected void RadGridSeleccion_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGridSeleccion.DataSource = ListaEmpleadosInf;
        }

        protected void RadGridSeleccion_ItemCommand(object sender, GridCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {

                GridDataItem item = e.Item as GridDataItem;
                EliminarEmpleado_Info(item.GetDataKeyValue("ID_EMPLEADO").ToString());

            }


        }

        protected void Tipo1_Click(object sender, EventArgs e)
        {
            RadGridSeleccion.Visible = false;
            btnSeleccion.Visible = false;
            btnPuestos.Visible = false;
            rcbAuto.Visible = false;
            rbHabilitar.Visible = false;
            rbLectura.Visible = false;
            rbEditar.Visible = false;
            rbAgregar2.Visible = false;
            rbAgregar.Visible = true;
            btnSeleccionarAreas.Enabled = true;
            btnSeleccionarEmpleados.Enabled = true;
            btnSeleccionarPuestos.Enabled = true;
            btnSeleccionarAdscripcion.Enabled = true;
            btnSeleccionUsuario.Enabled = true;
            rlMensajeInformación.Visible = false;
            AdscripcionesNegocio negocioa = new AdscripcionesNegocio();
            string adscripcionVisible = negocioa.SeleccionaAdscripcion().ToString();
            if (adscripcionVisible != "No")
            {
                btnSeleccionarAdscripcion.Visible = true;
            }
            else
            {
                btnSeleccionarAdscripcion.Visible = false;
            }
        }

        protected void Tipo2_Click(object sender, EventArgs e)
        {
            RadGridSeleccion.Visible = true;
            btnSeleccion.Visible = false;
            btnPuestos.Visible = true;
            rcbAuto.Visible = true;
            rbHabilitar.Visible = true;
            rbLectura.Visible = true;
            rbEditar.Visible = true;
            rbAgregar2.Visible = true;
            rbAgregar.Visible = false;
            btnSeleccionarAreas.Enabled = false;
            btnSeleccionarEmpleados.Enabled = false;
            btnSeleccionarPuestos.Enabled = false;
            btnSeleccionarAdscripcion.Enabled = false;
            btnSeleccionUsuario.Enabled = false;
            rlMensajeInformación.Visible = true;
            RadGridSeleccion.MasterTableView.Columns.FindByUniqueName("NB_EMPLEADO").Visible = false;

        }

        protected void Tipo3_Click(object sender, EventArgs e)
        {
            RadGridSeleccion.Visible = true;
            btnSeleccion.Visible = true;
            btnPuestos.Visible = false;
            rcbAuto.Visible = true;
            rbHabilitar.Visible = true;
            rbLectura.Visible = true;
            rbEditar.Visible = true;
            rbAgregar2.Visible = true;
            rbAgregar.Visible = false;
            btnSeleccionarAreas.Enabled = false;
            btnSeleccionarEmpleados.Enabled = false;
            btnSeleccionarPuestos.Enabled = false;
            btnSeleccionarAdscripcion.Enabled = false;
            rlMensajeInformación.Visible = true;
            RadGridSeleccion.MasterTableView.Columns.FindByUniqueName("NB_EMPLEADO").Visible = true;
        }

        protected void rbAgregar2_Click(object sender, EventArgs e)
        {

            DateTime vFechainicial;
            DateTime vFechafinal;
            byte[] data = null;
            byte? vPrivado = 0;
            string vIDEmpleado;
            string vIDUsuario;


            //string vTipoComunicado = "";
            //string vTipoAccion = "";


            if (rcbprivado.Checked == true)
            {
                vPrivado = 1;
            }

            ListaComunicadosNegocio nComunicado = new ListaComunicadosNegocio();
            vEmpleadosSeleccionados = new List<E_OBTIENE_EMPLEADOS_COMUNICADO>();

            foreach (GridDataItem item in grdEmpleadosSeleccionados.MasterTableView.Items)
            {

                vIDEmpleado = (item.GetDataKeyValue("ID_EMPLEADO").ToString());
                vIDUsuario = item.GetDataKeyValue("M_CL_USUARIO") == null ? "" : (item.GetDataKeyValue("M_CL_USUARIO").ToString());


                vEmpleadosSeleccionados.Add(new E_OBTIENE_EMPLEADOS_COMUNICADO
                {
                    ID_EMPLEADO = vIDEmpleado,
                    CL_USUARIO = vIDUsuario
                });

                var vXelements = vEmpleadosSeleccionados.Select(x =>
                                         new XElement("EMPLEADO",
                                         new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO),
                                         new XAttribute("CL_USUARIO", x.CL_USUARIO)
                              ));
                SELECCIONEMPLEADOS =
                new XElement("SELECCION", vXelements
                );
            }
            string vTitulocomunicado = txtTituloComunicado.Text;

            string vContenidocomunicado = txtContenido.Content;


            if (rauArchivo.UploadedFiles.Count > 0)
            {
                data = new byte[rauArchivo.UploadedFiles[0].ContentLength];
                rauArchivo.UploadedFiles[0].InputStream.Read(data, 0, int.Parse(rauArchivo.UploadedFiles[0].ContentLength.ToString()));
                vName = rauArchivo.UploadedFiles[0].FileName.ToString();
            }

            if (rbPermanente.Checked == true)
            {
                rdtFinfecha.Enabled = false;
                vFechainicial = Convert.ToDateTime(rdtiIniciofecha.SelectedDate);
                vFechafinal = DateTime.Now.AddYears(100);
            }
            else
            {
                vFechainicial = Convert.ToDateTime(rdtiIniciofecha.SelectedDate);
                vFechafinal = Convert.ToDateTime(rdtFinfecha.SelectedDate);
            }

            if (rcbEliminarAdjunto.Checked == true)
            {
                if (rauArchivo.UploadedFiles.Count == 0)
                {
                    vIdArchivo = vIdArchivo;
                }
            }

            if (Tipo2.Checked == true)
            {
                vTipoComunicado = "D";

                ListaEmpleadosPuestos = new List<E_EMPLEADO_PDE>();
                foreach (E_EMPLEADO_PDE item in ListaEmpleados)
                {
                    var pue = ListaEmpleadosPuestos.Where(t => t.ID_PUESTO == item.ID_PUESTO).FirstOrDefault();
                    if (pue == null)
                    {
                        E_EMPLEADO_PDE f = new E_EMPLEADO_PDE
                        {
                            ID_EMPLEADO = item.ID_EMPLEADO,
                            NB_EMPLEADO = item.NB_EMPLEADO,
                            ID_PUESTO = item.ID_PUESTO,
                            NB_PUESTO = item.NB_PUESTO
                        };

                        ListaEmpleadosPuestos.Add(f);

                    }
                    else
                    {
                        vRepetidos = vRepetidos + pue.NB_PUESTO.ToString() + " ,";

                    }
                }




                foreach (E_EMPLEADO_PDE item in ListaEmpleadosInf)
                {

                    var vAsignado = nComunicado.ValidaDescriptivoAsignado(item.NB_PUESTO);

                    if (vAsignado == "Si")
                    {
                        vAsignados = vAsignados + ", " + item.NB_PUESTO;
                    }

                }


            }
            else
            {
                vTipoComunicado = "I";

                foreach (E_EMPLEADO_PDE item in ListaEmpleadosInf)
                {

                    var vAsignado = nComunicado.ValidaInventarioAsignado(item.ID_EMPLEADO);

                    if (vAsignado == "Si")
                    {
                        vAsignados = vAsignados + ", " + item.NB_EMPLEADO;
                    }

                }

            }

            if (rbLectura.Checked == true)
            {
                vTipoAccion = "L";
            }
            else if (rbEditar.Checked == true)
            {
                vTipoAccion = "E";
            }

            if (rcbprivado.Checked == true)
            {
                vPrivado = 1;
            }
            else
            {
                vPrivado = 0;
            }


            vEmpleadosSeleccionados_Info = new List<E_OBTIENE_EMPLEADOS_COMUNICADO>();

            if (rcbAuto.Checked == true)
            {

                foreach (GridDataItem item in RadGridSeleccion.MasterTableView.Items)
                {
                    vIDEmpleado = (item.GetDataKeyValue("ID_EMPLEADO").ToString());

                    vEmpleadosSeleccionados_Info.Add(new E_OBTIENE_EMPLEADOS_COMUNICADO
                    {
                        ID_EMPLEADO = vIDEmpleado
                    });

                    var vXelements = vEmpleadosSeleccionados_Info.Select(x =>
                                             new XElement("EMPLEADO",
                                             new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO),
                                             new XElement("EMPLEADOMOD",
                                             new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO))
                                  )
                                  );
                    SELECCIONEMPLEADOSINFO =
                    new XElement("SELECCION", vXelements
                    );
                }
            }
            else
            {

                foreach (GridDataItem item in RadGridSeleccion.MasterTableView.Items)
                {

                    vIDEmpleado = (item.GetDataKeyValue("ID_EMPLEADO").ToString());

                    vEmpleadosSeleccionados_Info.Add(new E_OBTIENE_EMPLEADOS_COMUNICADO
                           {
                               ID_EMPLEADO = vIDEmpleado,
                           });

                    var vXelements = vEmpleadosSeleccionados_Info.Select(x =>
                                         new XElement("EMPLEADOMOD",
                                          new XAttribute("ID_EMPLEADO", x.ID_EMPLEADO)
                               )
                              );

                    var vXelement_Padre = vEmpleadosSeleccionados.Select(x =>
                                              new XElement("EMPLEADO",
                                              new XAttribute("ID_EMPLEADO", x.CL_USUARIO),
                                              vXelements
                                   )
                                   );



                    SELECCIONEMPLEADOSINFO =
                    new XElement("SELECCION", vXelement_Padre
                    );
                }


            }

            rcbAuto.Checked = false;

            ListaComunicadosNegocio negocioa = new ListaComunicadosNegocio();
            string vExisteComunicado = negocioa.ValidaExisteComunicado(vIdEmpleadoValida, vTipoComunicado, vTipoAccion).ToString();
            if (vRepetidos == null && vAsignados == null)
            {
                if (vExisteComunicado == "Si")
                {
                    if (SELECCIONEMPLEADOS != null && vTitulocomunicado != "" && SELECCIONEMPLEADOSINFO != null)
                    {
                        E_RESULTADO vResultado = nComunicado.InsertaActualizaComunicadoInformacion((int)vIdArchivo, vName, data, (int)vIdComunicado, vTitulocomunicado, vContenidocomunicado, vFechainicial, vFechafinal, SELECCIONEMPLEADOS.ToString(), vPrivado, ContextoUsuario.oUsuario.CL_USUARIO, ContextoUsuario.nbPrograma, vTipoTransaccion, vTipoComunicado, vTipoAccion, SELECCIONEMPLEADOSINFO.ToString());
                        string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                        UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, vResultado.CL_TIPO_ERROR);
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, "No hay empleados seleccionados y/o comunicado sin titulo", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Hay un comunicado pendiente por revisar, para este empleado", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

                }
            }
            else
            {

                if (Tipo2.Checked == true)
                {
                    if (vRepetidos != null && vAsignados == null)
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, "No se pueden seleccionar empleados con el mismo puesto: " + vRepetidos + " para enviar el comunicado.", E_TIPO_RESPUESTA_DB.WARNING, 400, 250, pCallBackFunction: "");
                    }
                    else if (vRepetidos == null && vAsignados != null)
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, "Los siguentes puestos ya están en revisión: " + vAsignados + ".", E_TIPO_RESPUESTA_DB.WARNING, 400, 250, pCallBackFunction: "");
                    }
                    else
                    {
                        UtilMensajes.MensajeResultadoDB(rnMensaje, "No se pueden seleccionar empleados con el mismo puesto: " + vRepetidos + " y los siguientes puestos ya están en revisión: " + vAsignados + ".", E_TIPO_RESPUESTA_DB.WARNING, 400, 250, pCallBackFunction: "");
                    }
                }
                else
                {
                    UtilMensajes.MensajeResultadoDB(rnMensaje, "Los siguentes empleados ya están en revisión: " + vAsignados + ".", E_TIPO_RESPUESTA_DB.WARNING, 400, 250, pCallBackFunction: "");
                }
                ListaEmpleadosInf = new List<E_EMPLEADO_PDE>();
                RadGridSeleccion.Rebind();
                vRepetidos = null;
                vAsignados = null;

            }


        }

        protected void rcbAuto_Click(object sender, EventArgs e)
        {
            if (rcbAuto.Checked == true)
            {
                if (ListaEmpleadosInf.Count == 0)
                {

                    foreach (E_EMPLEADO_PDE item in ListaEmpleados)
                    {
                        E_EMPLEADO_PDE f = new E_EMPLEADO_PDE
                        {
                            ID_EMPLEADO = item.ID_EMPLEADO,
                            NB_EMPLEADO = item.NB_EMPLEADO,
                            NB_PUESTO = item.NB_PUESTO
                        };

                        ListaEmpleadosInf.Add(f);
                    }
                    RadGridSeleccion.Rebind();

                }
            }
        }
    }

}

