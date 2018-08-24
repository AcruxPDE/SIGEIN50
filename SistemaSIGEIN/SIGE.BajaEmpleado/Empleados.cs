using SIGE.Entidades.Externas;
using SIGE.Negocio.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.BajaEmpleado
{
    public class Empleados
    {
        public E_RESULTADO ActualizaBajaEmpleados()
        {
            EmpleadoNegocio nEmpleado = new EmpleadoNegocio();
            return nEmpleado.ActualizaBajaEmpleado();
        }
    }
}
