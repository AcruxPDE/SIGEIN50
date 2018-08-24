using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.Entidades.Administracion
{
    public class E_USUARIO
    {
        private string _nbPassword;
        private Guid _clAutenticacion;

        public string CL_USUARIO { get; set; }
        public string NB_USUARIO { get; set; }
        public string NB_CORREO_ELECTRONICO { get; set; }
        public string CL_TIPO_MULTIEMPRESA { get; set; }
        public string NB_PASSWORD
        {
            get { return _nbPassword; }
            set { _nbPassword = PasswordHash.PasswordHash.CreateHash(value); }
        }
        public Guid CL_AUTENTICACION
        {
            get { return _clAutenticacion; }
        }
        public bool FG_CAMBIAR_PASSWORD { get; set; }
        public string CL_CAMBIAR_PASSWORD { get; set; }
        public int ID_ROL { get; set; }
        public Nullable<int> ID_EMPLEADO { get; set; }
        public Nullable<int> ID_PUESTO { get; set; }
        public string ID_EMPLEADO_PDE { get; set; }    
        public string ID_PUESTO_PDE { get; set; }    
        public bool FG_ACTIVO { get; set; }
        public XElement XML_CATALOGOS { get; set; }
        public Nullable<int> ID_EMPRESA { get; set; }

        public E_ROL oRol { get; set; }

        public List<E_FUNCION> oFunciones { get; set; }

        public E_USUARIO()
        {
            this._clAutenticacion = Guid.NewGuid();
        }

        public bool ValidarToken(string pClAutenticacion)
        {
            return this._clAutenticacion.Equals(new Guid(pClAutenticacion));
        }

        public bool TienePermiso(string pClFuncion)
        {
            return oFunciones.Any(f => f.CL_FUNCION.Equals(pClFuncion));
        }
        public string CONTRASENA { get; set; }


    }
}
