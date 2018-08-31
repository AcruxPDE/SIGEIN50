using SIGE.AccesoDatos.Implementaciones.Administracion;
using SIGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGE.Negocio.AdministracionSitio
{
    public class OrganigramaNegocio
    {
        public E_ORGANIGRAMA ObtieneOrganigramaPuestos(int? pIdPuestoOrigen, int? pIdEmpresa, bool pFgMostrarEmpleados)
        {
            OrganigramaOperaciones oOrganigrama = new OrganigramaOperaciones();
            return oOrganigrama.ObtenerOrganigramaPuestos(pIdPuestoOrigen, pIdEmpresa, pFgMostrarEmpleados);
        }

        public E_ORGANIGRAMA ObtieneOrganigramaPlazas(int? pIdPlazaOrigen, int? pIdEmpresa, bool pFgMostrarEmpleados, int? pIdDepartamento, string pClCampoAdicional, int? pNuNivel)
        {
            OrganigramaOperaciones oOrganigrama = new OrganigramaOperaciones();
            return oOrganigrama.ObtenerOrganigramaPlazas(pIdPlazaOrigen, pIdEmpresa, pFgMostrarEmpleados, pIdDepartamento, pClCampoAdicional, pNuNivel);
        }

        public E_ORGANIGRAMA ObtieneOrganigramaAreas(int? pIdPlazaOrigen, bool pFgMostrarEmpleados)
        {
            OrganigramaOperaciones oOrganigrama = new OrganigramaOperaciones();
            return oOrganigrama.ObtenerOrganigramaAreas(pIdPlazaOrigen, pFgMostrarEmpleados);
        }
    }
}
