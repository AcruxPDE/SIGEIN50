using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
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
    public partial class VentanaSeleccionEnvioNotificacion : System.Web.UI.Page
    {
        private string xmlPuestoEmpleado;
        List<E_OBTIENE_PUESTO_EMPLEADOS> vEmpleadosSeleccionados
        {
            get { return (List<E_OBTIENE_PUESTO_EMPLEADOS>)ViewState["vs_vEmpleadosSeleccionados"]; }
            set { ViewState["vs_vEmpleadosSeleccionados"] = value; }
        }
        private XElement SELECCIONEMPLEADOS { get; set; }
        public string vIdPuesto
        {
            get { return (string)ViewState["vs_vsvIdPuesto"]; }
            set { ViewState["vs_vsvIdPuesto"] = value; }

        }
        private List<string> ListaPuestos
        {
            get { return (List<string>)ViewState["vs_vListaPuestos"]; }
            set { ViewState["vs_vListaPuestos"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vNbPrograma = ContextoUsuario.nbPrograma;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vEmpleadosSeleccionados = new List<E_OBTIENE_PUESTO_EMPLEADOS>();
        }

        private E_IDIOMA_ENUM vClIdioma = E_IDIOMA_ENUM.ES;
        public string vNbPrograma;
        public string vClUsuario;

        protected void grdNotificacionEmpleado_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ConfiguracionNotificacionNegocio negocio = new ConfiguracionNotificacionNegocio();
            List<E_EMPLEADO_PDE> lista = new List<E_EMPLEADO_PDE>();
            xmlPuestoEmpleado = negocio.ObtienePuestoEmpleado(null, null);
            XElement root = XElement.Parse(xmlPuestoEmpleado);
            foreach (XElement name in root.Elements("EMPLEADO"))
            {
                E_EMPLEADO_PDE em = new E_EMPLEADO_PDE
                         {
                             ID_EMPLEADO = name.Attribute("ID_EMPLEADO").Value,
                             ID_PUESTO = name.Attribute("ID_PUESTO").Value,
                             NB_PUESTO = name.Attribute("NB_PUESTO").Value,
                             NB_EMPLEADO = name.Attribute("NB_EMPLEADO").Value
                         };
                lista.Add(em);
                var pue = lista.Where(t => t.ID_PUESTO == "0").FirstOrDefault();
                lista.Remove(pue);


            }

           grdNotificacionEmpleado.DataSource = lista;

        }

        protected void btnSeleccion_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in grdNotificacionEmpleado.SelectedItems)
            {
                vIdPuesto = item.GetDataKeyValue("ID_PUESTO").ToString();

                vEmpleadosSeleccionados.Add(new E_OBTIENE_PUESTO_EMPLEADOS
                {
                    ID_PUESTO = vIdPuesto,
                    FG_ACTIVO = true,
                });

                var vXelements = vEmpleadosSeleccionados.Select(x =>
                                         new XElement("PUESTO",
                                         new XAttribute("ID_PUESTO", x.ID_PUESTO),
                                         new XAttribute("FG_ACTIVO", x.FG_ACTIVO)
                              ));
                SELECCIONEMPLEADOS =
                new XElement("SELECCION", vXelements
                );
            }

            if (SELECCIONEMPLEADOS != null)
            {
                ConfiguracionNotificacionNegocio puesto = new ConfiguracionNotificacionNegocio();
                E_RESULTADO vResultado = puesto.SPE_INSERTA_ACTUALIZA_EMPLEADO_NOTIFICACION_PUESTO_PDE(SELECCIONEMPLEADOS.ToString(), vClUsuario, vNbPrograma);
                string vMensaje = vResultado.MENSAJE.Where(w => w.CL_IDIOMA.Equals(vClIdioma.ToString())).FirstOrDefault().DS_MENSAJE;
                UtilMensajes.MensajeResultadoDB(rnMensaje, vMensaje, E_TIPO_RESPUESTA_DB.SUCCESSFUL, pCallBackFunction: "onCloseWindow");

            }
            else
            {
                UtilMensajes.MensajeResultadoDB(rnMensaje, "Selecciona a quién enviar la notificación", E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");

            }

        }
    }
}