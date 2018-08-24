using SIGE.Entidades.Externas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.BajaEmpleado
{
    class BajaEmpleados
    {
        static void Main(string[] args)
        {
            try
            {
                Empleados vEmp = new Empleados();
                E_RESULTADO vRes = vEmp.ActualizaBajaEmpleados();
            }
            catch (Exception e)
            {
                
               // throw;
            }
        }
    }
}
