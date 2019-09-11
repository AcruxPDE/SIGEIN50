using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Entidades.PuntoDeEncuentro
{
    [Serializable]
    public class E_OBTIENE_EMPLEADOS_GENERA_CONTRASENA
    {
        private string _nbPassword;
        public string ID_EMPLEADO { get; set; }
        public string NB_EMPLEADO { get; set; }
        public string NB_PATERNO { get; set; }
        public string NB_COMPLETO { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public int ID_ROL { get; set; }
        public string ID_Grupo { get; set; }
        public string ID_USUARIO { get; set; }
        public string CONTRASENA { get; set; }

        public string NB_PASSWORD
        {
            get { return _nbPassword; }
            set { _nbPassword = PasswordHash.PasswordHash.CreateHash(value); }
        }
    }
}
