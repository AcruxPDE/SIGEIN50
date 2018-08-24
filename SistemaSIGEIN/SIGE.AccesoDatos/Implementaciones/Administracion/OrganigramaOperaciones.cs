using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.Administracion
{
    public class OrganigramaOperaciones
    {
        private SistemaSigeinEntities context;

        public E_ORGANIGRAMA ObtenerOrganigramaPuestos(int? pIdPuestoOrigen, int? pIdEmpresa, bool pFgMostrarEmpleados)
        {
            using (context = new SistemaSigeinEntities())
            {
                E_ORGANIGRAMA vOrganigrama = new E_ORGANIGRAMA();
                List<E_ORGANIGRAMA_NODO> lstNodos = context.SPE_OBTIENE_ORGANIGRAMA_PUESTO(pIdPuestoOrigen, pIdEmpresa).Select(s => new E_ORGANIGRAMA_NODO()
                {
                    idNodo = s.ID_PUESTO,
                    idNodoSuperior = s.ID_PUESTO_JEFE,
                    clNodo = s.CL_PUESTO,
                    nbNodo = s.NB_PUESTO,
                    clTipoNodo = s.CL_POSICION_ORGANIGRAMA,
                    clTipoGenealogia = s.CL_TIPO_GENEALOGIA,
                    noNivel = s.NO_NIVEL ?? 0
                }).ToList();

                vOrganigrama.lstNodoDescendencia = lstNodos.Where(w => w.clTipoGenealogia.Equals("DESCENDENCIA")).ToList();
                vOrganigrama.lstNodoAscendencia = lstNodos.Where(w => w.clTipoGenealogia.Equals("ASCENDENCIA")).ToList();

                if (pFgMostrarEmpleados)
                {
                    XElement vPuestos = new XElement("PUESTOS");
                    vOrganigrama.lstNodoDescendencia.ForEach(f => vPuestos.Add(new XElement("PUESTO", new XAttribute("ID_PUESTO", f.idNodo))));

                    vOrganigrama.lstGrupo = context.SPE_OBTIENE_ORGANIGRAMA_PUESTOS_EMPLEADOS(vPuestos.ToString(), pIdEmpresa).Select(s => new E_ORGANIGRAMA_GRUPO()
                    {
                        idNodo = s.ID_PUESTO,
                        idItem = s.ID_EMPLEADO,
                        clItem = s.CL_EMPLEADO,
                        nbItem = String.Format("{0} ({1})", s.NB_EMPLEADO, s.CL_EMPLEADO),
                        cssItem = (bool)s.FG_VACANTE ? "cssVacante" : String.Empty
                    }).ToList();
                }
                else
                    vOrganigrama.lstGrupo = new List<E_ORGANIGRAMA_GRUPO>();

                return vOrganigrama;
            }
        }

        public E_ORGANIGRAMA ObtenerOrganigramaPlazas(int? pIdPlazaOrigen, int? pIdEmpresa, bool pFgMostrarEmpleados)
        {
            using (context = new SistemaSigeinEntities())
            {
                E_ORGANIGRAMA vOrganigrama = new E_ORGANIGRAMA();
                List<E_ORGANIGRAMA_NODO> lstNodos = context.SPE_OBTIENE_ORGANIGRAMA_PLAZA(pIdPlazaOrigen, pIdEmpresa).Select(s => new E_ORGANIGRAMA_NODO()
                {
                    idNodo = s.ID_PLAZA,
                    idNodoSuperior = s.ID_PLAZA_SUPERIOR,
                    clNodo = s.CL_PUESTO,
                    nbNodo = String.Format("{0} ({1})", s.NB_PUESTO, s.CL_PLAZA),
                    clTipoNodo = s.CL_POSICION_ORGANIGRAMA,
                    clTipoGenealogia = s.CL_TIPO_GENEALOGIA,
                    noNivel = s.NO_NIVEL ?? 0
                }).ToList();

                vOrganigrama.lstNodoDescendencia = lstNodos.Where(w => w.clTipoGenealogia.Equals("DESCENDENCIA")).ToList();
                vOrganigrama.lstNodoAscendencia = lstNodos.Where(w => w.clTipoGenealogia.Equals("ASCENDENCIA")).ToList();

                if (pFgMostrarEmpleados)
                {
                    XElement vPlazas = new XElement("PLAZAS");
                    vOrganigrama.lstNodoDescendencia.ForEach(f => vPlazas.Add(new XElement("PLAZA", new XAttribute("ID_PLAZA", f.idNodo))));

                    vOrganigrama.lstGrupo = context.SPE_OBTIENE_ORGANIGRAMA_PLAZAS_EMPLEADOS(vPlazas.ToString(), pIdEmpresa).Select(s => new E_ORGANIGRAMA_GRUPO()
                    {
                        idNodo = s.ID_PLAZA,
                        idItem = s.ID_EMPLEADO,
                        clItem = s.CL_EMPLEADO,
                        nbItem = String.Format("{0} ({1})", s.NB_EMPLEADO, s.CL_EMPLEADO),
                        cssItem = (bool)s.FG_VACANTE ? "cssVacante" : String.Empty
                    }).ToList();
                }
                else
                    vOrganigrama.lstGrupo = new List<E_ORGANIGRAMA_GRUPO>();

                return vOrganigrama;
            }
        }

        public E_ORGANIGRAMA ObtenerOrganigramaAreas(int? pIdDepartamentoOrigen, bool pFgMostrarPuestos)
        {
            using (context = new SistemaSigeinEntities())
            {
                E_ORGANIGRAMA vOrganigrama = new E_ORGANIGRAMA();
                List<E_ORGANIGRAMA_NODO> lstNodos = context.SPE_OBTIENE_ORGANIGRAMA_DEPARTAMENTO(pIdDepartamentoOrigen).Select(s => new E_ORGANIGRAMA_NODO()
                {
                    idNodo = s.ID_DEPARTAMENTO,
                    idNodoSuperior = s.ID_DEPARTAMENTO_PADRE,
                    clNodo = s.CL_DEPARTAMENTO,
                    nbNodo = s.NB_DEPARTAMENTO,
                    clTipoGenealogia = s.CL_TIPO_GENEALOGIA,
                    noNivel = s.NO_NIVEL ?? 0
                }).ToList();

                vOrganigrama.lstNodoDescendencia = lstNodos.Where(w => w.clTipoGenealogia.Equals("DESCENDENCIA")).ToList();
                vOrganigrama.lstNodoAscendencia = lstNodos.Where(w => w.clTipoGenealogia.Equals("ASCENDENCIA")).ToList();

                if (pFgMostrarPuestos)
                {
                    XElement vAreas = new XElement("DEPARTAMENTOS");
                    vOrganigrama.lstNodoDescendencia.ForEach(f => vAreas.Add(new XElement("DEPARTAMENTO", new XAttribute("ID_DEPARTAMENTO", f.idNodo))));

                    vOrganigrama.lstGrupo = context.SPE_OBTIENE_ORGANIGRAMA_DEPARTAMENTOS_PUESTOS(vAreas.ToString()).Select(s => new E_ORGANIGRAMA_GRUPO()
                    {
                        idNodo = s.ID_DEPARTAMENTO,
                        idItem = s.ID_PUESTO,
                        clItem = s.CL_PUESTO,
                        nbItem = String.Format("{0} ({1})", s.NB_PUESTO, s.CL_PUESTO),
                        cssItem = s.CL_POSICION_ORGANIGRAMA == "STAFF" ? "cssStaff" : String.Empty
                    }).ToList();
                }
                else
                    vOrganigrama.lstGrupo = new List<E_ORGANIGRAMA_GRUPO>();

                return vOrganigrama;
            }
        }
    }
}
