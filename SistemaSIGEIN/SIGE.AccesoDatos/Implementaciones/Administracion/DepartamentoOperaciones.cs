using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
//using SIGE.Entidades.Administracion;
using System.Data;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.Administracion;
using SIGE.Entidades.Externas;
//using SIGE.Entidades.Administracion;
//using SIGE.Entidades.EvaluacionOrganizacional;
//using SIGE.Entidades.FormacionDesarrollo;
//using SIGE.Entidades.MetodologiaCompensacion;


namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal
{
    public class DepartamentoOperaciones
    {
        private SistemaSigeinEntities context;

        #region OBTIENE DATOS  M_DEPARTAMENTO
        public List<SPE_OBTIENE_M_DEPARTAMENTO_Result> ObtenerDepartamentos(int? pIdDepartamento = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, string pClDepartamento = null, string pNbDepartamento = null, XElement XML_SELECCIONADOS = null, int? pIdEmpresa= null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (XML_SELECCIONADOS != null)
                    vXmlFiltro = XML_SELECCIONADOS.ToString();
                return context.SPE_OBTIENE_M_DEPARTAMENTO(pIdDepartamento, pFgActivo, pFeInactivo, pClDepartamento, pNbDepartamento, vXmlFiltro,pIdEmpresa).ToList();
            }
        }

        #endregion

        #region INSERTA ACTUALIZA DATOS  M_DEPARTAMENTO
        public XElement InsertarActualizarDepartamento(string pClTipoTransaccion, E_DEPARTAMENTO pDepartamento, string pClUsuario, string pNbPrograma)
        {
            using (context = new SistemaSigeinEntities())
            {
                ObjectParameter pOutClRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_INSERTA_ACTUALIZA_M_DEPARTAMENTO(pOutClRetorno, pDepartamento.ID_DEPARTAMENTO, pDepartamento.FG_ACTIVO, pDepartamento.FE_INACTIVO, pDepartamento.CL_DEPARTAMENTO, pDepartamento.NB_DEPARTAMENTO, pDepartamento.CL_TIPO_DEPARTAMENTO, pDepartamento.ID_DEPARTAMENTO_PADRE, pDepartamento.XML_CAMPOS_ADICIONALES, pClUsuario, pNbPrograma, pClTipoTransaccion);

                return XElement.Parse(pOutClRetorno.Value.ToString());
            }
        }
        #endregion

        #region ELIMINA DATOS  M_DEPARTAMENTO
        public XElement Elimina_M_DEPARTAMENTO(int? ID_DEPARTAMENTO = null, string usuario = null, string programa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                // pout_clave_retorno.Value = "";
                context.SPE_ELIMINA_M_DEPARTAMENTO(pout_clave_retorno, ID_DEPARTAMENTO, usuario, programa);
                //regresamos el valor de retorno de sql				
                //      return Convert.ToInt32(pout_clave_retorno.Value);
                //  return Convert.ToString(pout_clave_retorno.Value);
                return XElement.Parse(pout_clave_retorno.Value.ToString());


            }
        }
        #endregion

    }
}
