using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal;
using System.Xml.Linq;
using SIGE.Entidades.Externas;
using SIGE.Negocio.Utilerias;

namespace SIGE.Negocio.Administracion
{
    public class DepartamentoNegocio
    {
        public List<SPE_OBTIENE_M_DEPARTAMENTO_Result> ObtieneDepartamentos(int? pIdDepartamento = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, string pClDepartamento = null, string pNbDepartamento = null, XElement XML_SELECCIONADOS = null, int? ID_EMPRESA = null)
        {
            DepartamentoOperaciones operaciones = new DepartamentoOperaciones();
            return operaciones.ObtenerDepartamentos(pIdDepartamento, pFgActivo, pFeInactivo, pClDepartamento, pNbDepartamento, XML_SELECCIONADOS, ID_EMPRESA);
        }

        public E_RESULTADO InsertaActualiza_M_DEPARTAMENTO(string tipo_transaccion, E_DEPARTAMENTO V_M_DEPARTAMENTO, string usuario, string programa)
        {
            DepartamentoOperaciones operaciones = new DepartamentoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarDepartamento(tipo_transaccion, V_M_DEPARTAMENTO, usuario, programa));
        }

        public E_RESULTADO Elimina_M_DEPARTAMENTO(int? ID_DEPARTAMENTO = null, string usuario = null, string programa = null)
        {
            DepartamentoOperaciones operaciones = new DepartamentoOperaciones();
            return UtilRespuesta.EnvioRespuesta(operaciones.Elimina_M_DEPARTAMENTO(ID_DEPARTAMENTO, usuario, programa));
        }        
    }
}
