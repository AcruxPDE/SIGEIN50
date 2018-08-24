using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.PuntoDeEncuentro;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using SIGE.Entidades.Administracion;

namespace SIGE.AccesoDatos.Implementaciones.PuntoDeEncuentro
{
    public class ContextoPDEOperaciones
    {

        private SistemaSigeinEntities context;

        #region OBTIENE USUARIOS DE PDE

        public SPE_OBTIENE_PDE_USUARIO_Result AutenticaUsuarioPDE(E_USUARIO pUsuario)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_PDE_USUARIO(pUsuario.CL_USUARIO, pUsuario.CL_AUTENTICACION.ToString()).FirstOrDefault();
            }
        }

        #endregion

        #region OBTIENE EMPLEADOS PARA PDE

        public List<SPE_OBTIENE_EMPLEADOS_GENERA_CONTRASENA_Result> ObtieneEmpleados(string IdEmpleado = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_OBTIENE_EMPLEADOS_GENERA_CONTRASENA(IdEmpleado).ToList();
            }
        }
        #endregion

    }
}
