using Newtonsoft.Json;
using SIGE.Entidades.Externas;
using SIGE.WebApp.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Telerik.Web.UI;
using WebApp.Comunes;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.PuntoDeEncuentro;

namespace SIGE.WebApp.PDE
{
    public partial class Usuarios : System.Web.UI.Page
    {
        #region VARIABLES

        private string vClUsuario;
        private string vNbPrograma;
        private int? vIdRol;

        private List<E_USUARIO_PDE> vUsuarios
        {
            get { return (List<E_USUARIO_PDE>)ViewState["vs_vUsuarios"]; }
            set { ViewState["vs_vUsuarios"] = value; }
        }

        private List<E_USUARIO_PDE> vUsuariosPassword
        {
            get { return (List<E_USUARIO_PDE>)ViewState["vs_vUsuariosPassword"]; }
            set { ViewState["vs_vUsuariosPassword"] = value; }
        }

        #endregion

        #region METODOS

        protected void AgregarUsuarioPorEmpleado(string pEmpleados)
        {
            List<E_SELECTOR_EMPLEADO> vUsuariosNuevos = JsonConvert.DeserializeObject<List<E_SELECTOR_EMPLEADO>>(pEmpleados);
            ListaUsuariosNegocio vNegocioPDE = new ListaUsuariosNegocio();
            List<string> vMenssages = new List<string>();
            XElement vEmpleados = new XElement("EMPLEADOS");

            int vCount = 1;
            bool vFlag = false;
            
            foreach (E_SELECTOR_EMPLEADO item in vUsuariosNuevos)
            {
                XElement vEmpleado = new XElement("EMPLEADO");

                vEmpleado.Add(new XAttribute("ID_EMPLEADO", item.idEmpleado));
                vEmpleado.Add(new XAttribute("NO_ORDEN", vCount));
                vEmpleados.Add(vEmpleado);
                vCount++;
            }

            //GET THE EMPLOYEES ACCORDING TO THE EMPLOYEES SELECTOR
            vUsuarios = vNegocioPDE.ObtieneEmpleadosSelector(vEmpleados.ToString(), "EMPLEADOS");
            grdUsuarios.Rebind();

            foreach (E_USUARIO_PDE item in vUsuarios)
            {
                if(item.MN_COUNT > 1)
                {
                    string vMessage = "El correo electronico: " + item.CL_CORREO_ELECTRONICO.ToString() + ", se encuentra duplicado: " + item.MN_COUNT.ToString() + " veces." ;
                    vMenssages.Add(vMessage);
                    vFlag = true;
                }
            }

            if(vFlag)
            {
                string vMensaje = "";

                foreach(string item in vMenssages)
                {
                    vMensaje = vMensaje + item + "\n";
                }

                vMensaje = vMensaje + "No pueden existir usuarios con el mismo correo";

                UtilMensajes.MensajeResultadoDB(rwmMensaje, vMensaje, E_TIPO_RESPUESTA_DB.WARNING, pCallBackFunction: "");
            }

            
        }

        protected void AgregarUsuarioPorPuesto(string pPuestos)
        {
            List<E_USUARIO_PDE> vUsuariosPuesto = new List<E_USUARIO_PDE>();
            List<E_SELECTOR_PUESTO> vPuestos = JsonConvert.DeserializeObject<List<E_SELECTOR_PUESTO>>(pPuestos);
            ListaUsuariosNegocio vNegocioPDE = new ListaUsuariosNegocio();

            var vPuestosSelector = (new XElement("PUESTOS", vPuestos.Select(s => new XElement("PUESTO", new XAttribute("ID_PUESTO", s.idPuesto)))));

            //GET THE EMPLOYESS ACCORDING TO THE POSITION
            //vUsuariosPuesto = vNegocioPDE.ObtieneEmpleadosPorPuesto(vPuestosSelector.ToString());

            //if (vUsuariosNuevos.Count > 0)
            //{
            //    foreach (E_SELECTOR_EMPLEADO item in vUsuariosNuevos)
            //    {
            //        if (vUsuarios.FindIndex(x => x.idEmpleado == item.idEmpleado) < 0)
            //            vUsuarios.Add(item);
            //    }

            //    AsignarContraseñas(vUsuariosNuevos);
            //    grdUsuarios.Rebind();
            //}
        }


        protected void AsignarContraseñas(List<E_SELECTOR_EMPLEADO> vUsuarios)
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 10;
            string vContrasena = "";

            foreach (E_SELECTOR_EMPLEADO item in vUsuarios)
            {
                if (vUsuariosPassword.FindIndex(x => x.ID_EMPLEADO == item.idEmpleado) < 0)
                {
                    E_USUARIO_PDE vUsuario = new E_USUARIO_PDE();
                    vContrasena = "";

                    for (int i = 0; i < longitudContrasenia; i++)
                    {
                        letra = caracteres[rdn.Next(longitud)];
                        vContrasena += letra.ToString();
                    }

                    vUsuario.ID_EMPLEADO = int.Parse(item.idEmpleado.ToString());
                    vUsuario.CL_EMPLEADO = item.clEmpleado;
                    vUsuario.NB_EMPLEADO = item.nbEmpleado;
                    vUsuario.NB_PUESTO = item.nbPuesto;
                    vUsuario.NB_CONTRASEÑA = vContrasena;

                    vUsuariosPassword.Add(vUsuario);
                }
            }

            grdContrasenas.Rebind();
        }

        protected void GenerarContraseñas()
        {

        }

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                vUsuarios = new List<E_USUARIO_PDE>();
                vUsuariosPassword = new List<E_USUARIO_PDE>();
            }

            vIdRol = ContextoUsuario.oUsuario.oRol.ID_ROL;
            vClUsuario = ContextoUsuario.oUsuario.CL_USUARIO;
            vNbPrograma = ContextoUsuario.nbPrograma;
        }

        protected void ramUsuarios_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            E_SELECTOR vSeleccion = new E_SELECTOR();
            string pParameter = e.Argument;

            if (pParameter != null)
                vSeleccion = JsonConvert.DeserializeObject<E_SELECTOR>(pParameter);

            if (vSeleccion.clTipo == "EVALUADO")
                AgregarUsuarioPorEmpleado(vSeleccion.oSeleccion.ToString());

            if (vSeleccion.clTipo == "PUESTO")
                AgregarUsuarioPorPuesto(vSeleccion.oSeleccion.ToString());

            //if (vSeleccion.clTipo == "AREA")
            //    AgregarEvaluadosPorArea(vSeleccion.oSeleccion.ToString());

            //if (vSeleccion.clTipo == "EVALUADOR")
            //    AgregarEvaluadores(vSeleccion.oSeleccion.ToString());

            //if (vSeleccion.clTipo == "VERIFICACONFIGURACION")
            //{
            //    PeriodoDesempenoNegocio nPeriodo = new PeriodoDesempenoNegocio();
            //    var vFgConfigurado = nPeriodo.VerificaConfiguracion(vIdPeriodo).FirstOrDefault();
            //    if (vFgConfigurado != null)
            //        SeguridadProcesos(vFgConfigurado.FG_ESTATUS);
            //}
        }

        protected void grdUsuarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdUsuarios.DataSource = vUsuarios;
        }

        protected void grdContrasenas_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdContrasenas.DataSource = vUsuariosPassword;
        }
    }
}