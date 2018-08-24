using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;

namespace SIGE.AccesoDatos.Implementaciones.IntegracionDePersonal  // reemplazar por la carpeta correspondiente
{

    public class PuestoOperaciones
    {
        private SistemaSigeinEntities context;

        public List<SPE_OBTIENE_M_PUESTO_Result> ObtenerPuestos(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, string pClPuesto = null, string pNbPuesto = null, int? pIdDepartamento = null, string pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, string pClGenero = null, string pClEstadoCivil = null, string pXmlRequerimientos = null, string pXmlObservaciones = null, string pXmlResponsabilidades = null, string pXmlAutoridad = null, string pXmlCursosAdicionales = null, string pXmlMentor = null, string pClTipoPuesto = null, Guid? pIdCentroAdministrativo = null, Guid? pIdCentroOperativo = null, int? pIdPaquetePrestaciones = null, string pNbDepartamento = null, string pClDepartamento = null, string pXmlPuestos = null, XElement pXmlPuestosSeleccionados = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlPuestosSeleccionados != null)
                    vXmlFiltro = pXmlPuestosSeleccionados.ToString();

                return context.SPE_OBTIENE_M_PUESTO(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidades, pXmlAutoridad, pXmlCursosAdicionales, pXmlMentor, pClTipoPuesto, pIdCentroAdministrativo, pIdCentroOperativo, pIdPaquetePrestaciones, pXmlPuestos, pNbDepartamento, pClDepartamento, vXmlFiltro, pIdEmpresa).ToList();
            }
        }


        public List<SPE_VERIFICA_PUESTO_FACTOR_Result> ValidarConfiguracionPuestos(string pXmlPuestos = null, int? pIdPuesto = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                return context.SPE_VERIFICA_PUESTO_FACTOR(pXmlPuestos, pIdPuesto).ToList();
            }
        }

        public List<SPE_OBTIENE_M_PUESTO_REQUISICION_Result> ObtenerPuestosRequisicion(int? pIdPuesto = null, bool? pFgActivo = null, DateTime? pFeInactivo = null, string pClPuesto = null, string pNbPuesto = null, int? pIdDepartamento = null, string pXmlCamposAdicionales = null, int? pIdBitacora = null, byte? pNoEdadMinima = null, byte? pNoEdadMaxima = null, string pClGenero = null, string pClEstadoCivil = null, string pXmlRequerimientos = null, string pXmlObservaciones = null, string pXmlResponsabilidades = null, string pXmlAutoridad = null, string pXmlCursosAdicionales = null, string pXmlMentor = null, string pClTipoPuesto = null, Guid? pIdCentroAdministrativo = null, Guid? pIdCentroOperativo = null, int? pIdPaquetePrestaciones = null, string pNbDepartamento = null, string pClDepartamento = null, string pXmlPuestos = null, XElement pXmlPuestosSeleccionados = null, int? pIdEmpresa = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                string vXmlFiltro = null;
                if (pXmlPuestosSeleccionados != null)
                    vXmlFiltro = pXmlPuestosSeleccionados.ToString();

                return context.SPE_OBTIENE_M_PUESTO_REQUISICION(pIdPuesto, pFgActivo, pFeInactivo, pClPuesto, pNbPuesto, pIdDepartamento, pXmlCamposAdicionales, pIdBitacora, pNoEdadMinima, pNoEdadMaxima, pClGenero, pClEstadoCivil, pXmlRequerimientos, pXmlObservaciones, pXmlResponsabilidades, pXmlAutoridad, pXmlCursosAdicionales, pXmlMentor, pClTipoPuesto, pIdCentroAdministrativo, pIdCentroOperativo, pIdPaquetePrestaciones, pXmlPuestos, pNbDepartamento, pClDepartamento, vXmlFiltro, pIdEmpresa).ToList();
            }
        }

        public XElement InsertarActualizarPuesto(string tipo_transaccion, E_PUESTO V_M_PUESTO, string usuario, string programa, string pXmlOcupacion)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                context.SPE_INSERTA_ACTUALIZA_M_PUESTO(pout_clave_retorno, V_M_PUESTO.ID_PUESTO, usuario, programa, tipo_transaccion, V_M_PUESTO.XML_PUESTO, pXmlOcupacion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }
        public XElement InsertarActualizarPuestoRequisicion(string pTipoTransaccion = null, int? pIdPuesto = null, string pXmlPuesto = null, string pXmlPuestoOcupacion = null, int? pIdRequisicion = null, string pDsComentarios = null, string pClAutorizaRechaza = null, string pClAutorizaPuesto = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));

                context.SPE_INSERTA_ACTUALIZA_M_PUESTO_REQUISICION(pout_clave_retorno, pIdPuesto, pXmlPuesto, pXmlPuestoOcupacion, pIdRequisicion, pDsComentarios, pClAutorizaRechaza, pClAutorizaPuesto, pClUsuario, pNbPrograma, pTipoTransaccion);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());

            }
        }

        public XElement EliminarPuesto(int? pIdPuesto = null, string pClPuesto = null, string pClUsuario = null, string pNbPrograma = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                //Declaramos el objeto de valor de retorno
                ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
                context.SPE_ELIMINA_M_PUESTO(pout_clave_retorno, pIdPuesto, pClUsuario, pNbPrograma);
                //regresamos el valor de retorno de sql
                return XElement.Parse(pout_clave_retorno.Value.ToString());
            }
        }
    }
}
