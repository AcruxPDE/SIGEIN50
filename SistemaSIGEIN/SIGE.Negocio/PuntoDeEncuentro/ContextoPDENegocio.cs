using SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Negocio.Utilerias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace SIGE.Negocio.PuntoDeEncuentro
{
    public class ContextoPDENegocio
    {

        public E_USUARIO AutenticaUsuario(string pClUsuario)
        {
            E_USUARIO vUsuario = new E_USUARIO()
            {
                CL_USUARIO = pClUsuario,
            };

            ContextoPDEOperaciones oUsuario = new ContextoPDEOperaciones();
            SPE_OBTIENE_PDE_USUARIO_Result vAutenticacion = oUsuario.AutenticaUsuarioPDE(vUsuario);

            if (vAutenticacion != null) 
            {
             
                vUsuario.NB_CORREO_ELECTRONICO = vAutenticacion.NB_CORREO_ELECTRONICO;
                vUsuario.NB_USUARIO = vAutenticacion.NB_USUARIO;
                vUsuario.FG_ACTIVO = vAutenticacion.FG_ACTIVO;
                vUsuario.ID_EMPLEADO = 1;
                vUsuario.ID_EMPLEADO_PDE = vAutenticacion.ID_EMPLEADO;
                vUsuario.ID_PUESTO = 1;
                vUsuario.ID_PUESTO_PDE = vAutenticacion.ID_PUESTO;
                vUsuario.oRol = new E_ROL() { NB_ROL = vAutenticacion.NB_ROL };
                vUsuario.oFunciones = XElement.Parse(vAutenticacion.XML_DATA).Elements("FUNCION").Select(f => new E_FUNCION()
                {
                    CL_FUNCION = f.Attribute("CL_FUNCION").Value,
                    CL_TIPO_FUNCION = f.Attribute("CL_TIPO_FUNCION").Value,
                    ID_FUNCION = (int)UtilXML.ValorAtributo(f.Attribute("ID_FUNCION"), E_TIPO_DATO.INT),
                    ID_FUNCION_PADRE = (int?)UtilXML.ValorAtributo(f.Attribute("ID_FUNCION_PADRE"), E_TIPO_DATO.INT),
                    NB_FUNCION = f.Attribute("NB_FUNCION").Value,
                    NB_URL = f.Attribute("NB_URL").Value,
                    XML_CONFIGURACION = f.Element("XML_CONFIGURACION").ToString()
                }).OrderBy(o => o.NO_ORDEN).ToList();
            }
            return vUsuario;
        }

        #region OBTIENE EMPLEADOS PARA PDE

        public List<SPE_OBTIENE_EMPLEADOS_GENERA_CONTRASENA_Result> ObtenerEmpleados_Pde(string ID_EMPLEADO = null)
        {
            ContextoPDEOperaciones Operaciones = new ContextoPDEOperaciones();
            return Operaciones.ObtieneEmpleados(ID_EMPLEADO).ToList();
        }

        #endregion

    }
}
