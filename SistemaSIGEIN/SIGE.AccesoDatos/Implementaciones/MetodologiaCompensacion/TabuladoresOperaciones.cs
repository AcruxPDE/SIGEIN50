using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades.Administracion;
using SIGE.Entidades;
using System.Data.Objects;
using System.Xml.Linq;
using SIGE.Entidades.MetodologiaCompensacion;
using System.Data.SqlClient;


namespace SIGE.AccesoDatos.Implementaciones.MetodologiaCompensacion
{
   public class TabuladoresOperaciones
    {
        
       #region OBTIENE DATOS  K_TABULADOR
       private SistemaSigeinEntities context;

       public List<SPE_OBTIENE_TABULADORES_Result> ObtenerTabuladores(int? ID_TABULADOR = null, string CL_TABULADOR = null, string NB_TABULADOR = null, string DS_TABULADOR = null, DateTime? FE_VIGENCIA = null, string CL_TIPO_PUESTO = null, string CL_ESTADO = null, byte? NO_NIVELES = null, byte? NO_CATEGORIAS = null, decimal? PR_PROGRESION = null, decimal? PR_INFLACION = null, int? ID_CUARTIL_INFLACIONAL = null, int? ID_CUARTIL_INCREMENTO = null, int? ID_CUARTIL_MERCADO = null ,string CL_SUELDO_COMPARACION = null, string XML_VARIACION = null, string CL_ORIGEN_NIVELES = null, DateTime? FE_CREACION = null, string CL_USUARIO_APP_CREA = null)
        {
            using (context = new SistemaSigeinEntities())
            {
                var q = from vTabuladores in context.SPE_OBTIENE_TABULADORES(ID_TABULADOR, CL_TABULADOR, NB_TABULADOR, DS_TABULADOR, FE_VIGENCIA, CL_TIPO_PUESTO, CL_ESTADO, NO_NIVELES, NO_CATEGORIAS, PR_PROGRESION, PR_INFLACION, ID_CUARTIL_INFLACIONAL, ID_CUARTIL_INCREMENTO, ID_CUARTIL_MERCADO ,CL_SUELDO_COMPARACION, XML_VARIACION, CL_ORIGEN_NIVELES, FE_CREACION, CL_USUARIO_APP_CREA)
                        select vTabuladores;
                return q.ToList();
            }
        }


       #endregion

       #region OBTIENE DATOS  K_TABULADOR_NIVEL

       public List<SPE_OBTIENE_TABULADORES_NIVEL_Result> ObtenerTabuladoresNivel(int? ID_TABULADOR = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_TABULADORES_NIVEL(ID_TABULADOR)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region  INSERTA ACTUALIZA DATOS  K_TABULADOR
      
       public XElement InsertarActualizarTabulador(string pClTipoOperacion, E_TABULADOR vTabuladores, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_TABULADOR(poutClaveRetorno, vTabuladores.ID_TABULADOR, vTabuladores.CL_TABULADOR, vTabuladores.NB_TABULADOR, vTabuladores.DS_TABULADOR, vTabuladores.FE_VIGENCIA, vTabuladores.CL_TIPO_PUESTO, vTabuladores.CL_ESTADO, vTabuladores.NO_NIVELES, vTabuladores.NO_CATEGORIAS, vTabuladores.PR_PROGRESION, vTabuladores.PR_INFLACION, vTabuladores.ID_CUARTIL_INFLACIONAL, vTabuladores.ID_CUARTIL_INCREMENTO, vTabuladores.ID_CUARTIL_MERCADO ,vTabuladores.CL_SUELDO_COMPARACION, vTabuladores.XML_VARIACION, vTabuladores.CL_ORIGEN_NIVELES, vTabuladores.FE_CREACION, usuario, usuario, programa, programa, pClTipoOperacion);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }
       
        #endregion

       #region  INSERTA  DATOS COPIA  K_TABULADOR
       public XElement InsertarTabuladorCopia(string pClTipoOperacion, E_TABULADOR vTabuladores, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_TABULADOR_COPIA(poutClaveRetorno, vTabuladores.ID_TABULADOR, vTabuladores.CL_TABULADOR, vTabuladores.NB_TABULADOR, vTabuladores.DS_TABULADOR, vTabuladores.FE_VIGENCIA, vTabuladores.CL_TIPO_PUESTO, vTabuladores.CL_ESTADO, vTabuladores.NO_NIVELES, vTabuladores.NO_CATEGORIAS, vTabuladores.PR_PROGRESION, vTabuladores.PR_INFLACION, vTabuladores.ID_CUARTIL_INFLACIONAL, vTabuladores.ID_CUARTIL_INCREMENTO, vTabuladores.ID_CUARTIL_MERCADO, vTabuladores.CL_SUELDO_COMPARACION, vTabuladores.XML_VARIACION, vTabuladores.CL_ORIGEN_NIVELES, vTabuladores.FE_CREACION, usuario, usuario, programa, programa, pClTipoOperacion);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }
       #endregion

       #region  INSERTA  DATOS COPIA  K_TABULADOR
       public XElement InsertarActualizarTabuladorFactor(string pClTipoOperacion, E_EMPLEADOS_SELECCIONADOS vEmpleados, string pNiveles, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_TABULADOR_FACTOR(poutClaveRetorno, vEmpleados.ID_TABULADOR, vEmpleados.XML_ID_SELECCIONADOS,pNiveles, usuario, programa, pClTipoOperacion, vEmpleados.ID_TABULADOR_FACTOR);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }
       #endregion

       #region  INSERTA  FACTOR EVALUACION
       public XElement InsertarFactorTabulador(string pClTipoOperacion, E_EMPLEADOS_SELECCIONADOS vEmpleados, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_TABULADOR_FACTOR(poutClaveRetorno, vEmpleados.ID_TABULADOR, vEmpleados.XML_ID_SELECCIONADOS, usuario, programa, pClTipoOperacion, vEmpleados.ID_TABULADOR_FACTOR);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }
       #endregion
       
       #region ELIMINA DATOS  K_TABULADOR
       public XElement EliminarTabulador(int pIdTabulador, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_ELIMINA_TABULADOR(poutClaveRetorno, pIdTabulador, usuario, programa);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }
       #endregion

       #region INSERTA DATOS  K_TABULADOR_EMPLEADO
   
       public XElement InsertarActualizarTabuladorEmpleado(string pClTipoOperacion, int ID_TABULADOR, string XML_SELECCIONADOS, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_TABULADOR_EMPLEADO(pout_clave_retorno, ID_TABULADOR, XML_SELECCIONADOS, usuario, programa, pClTipoOperacion);
               return XElement.Parse(pout_clave_retorno.Value.ToString());
           }
       }
       #endregion

       #region OBTIENE DATOS  K_TABULADOR_EMPLEADO


       public List<SPE_OBTIENE_EMPLEADOS_TABULADOR_Result> ObtenerEmpleadosTabulador(int? ID_TABULADOR = null, XElement XML_SELECCIONADOS = null, int? ID_EMPRESA = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               string vXmlFiltro = null;
               if (XML_SELECCIONADOS != null)
                   vXmlFiltro = XML_SELECCIONADOS.ToString();
               var q = from vTabuladores in context.SPE_OBTIENE_EMPLEADOS_TABULADOR(ID_TABULADOR, vXmlFiltro, ID_EMPRESA)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region OBTIENE DATOS  K_TABULADOR_FACTOR


       public List<SPE_OBTIENE_COMPETENCIAS_TABULADOR_Result> ObtenerCompetenciasTabulador(int? ID_TABULADOR_FACTOR = null, string NB_COMPETENCIA = null, string DS_COMPETENCIA = null, int? ID_TABULADOR = null, int? ID_COMPETENCIA = null)
       {
           using (context= new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_COMPETENCIAS_TABULADOR(ID_TABULADOR_FACTOR, NB_COMPETENCIA, DS_COMPETENCIA, ID_TABULADOR, ID_COMPETENCIA)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region ELIMINA DATOS  K_TABULADOR_FACTOR
       public XElement EliminarTabuladorFactor( string usuario, string programa, string XML_TABULADOR_FACTOR)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_ELIMINA_TABULADOR_FACTOR(poutClaveRetorno, usuario, programa,XML_TABULADOR_FACTOR);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }
       #endregion

       #region ELIMINA DATOS  K_TABULADOR_EMPLEADO
       public XElement EliminarTabuladorEmpleado(string usuario, string programa, string XML_TABULADOR_EMPLEADOS)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_ELIMINA_TABULADOR_EMPLEADO(poutClaveRetorno,usuario, programa,XML_TABULADOR_EMPLEADOS);
               return XElement.Parse(poutClaveRetorno.Value.ToString());


              
           }
       }
       #endregion

       #region OBTIENE DATOS  K_TABULADOR_PUESTO

       public List<SPE_OBTIENE_TABULADOR_PUESTO_Result> ObtenerPuestosTabulador( int? ID_TABULADOR = null, int? ID_PUESTO = null )
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_TABULADOR_PUESTO(ID_TABULADOR, ID_PUESTO )
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region COPIA MERCADO_SALARIAL

       public List<SPE_OPTIENE_COPIA_MERCADO_SALARIAL_Result> ObtenerMercadoSalarialTabulador(int? ID_TABULADOR = null, int? ID_TABULADOR_COPIAR = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OPTIENE_COPIA_MERCADO_SALARIAL(ID_TABULADOR, ID_TABULADOR_COPIAR)
                       select vTabuladores;
               return q.ToList();
           }
       }

       #endregion

       #region  INSERTA ACTUALIZA DATOS  K_TABULADOR_PUESTO
       public XElement InsertarActualizarTabuladorPuesto(string pClTipoOperacion, int? ID_TABULADOR, string XML_MIN_MAX,string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_TABULADOR_PUESTO(poutClaveRetorno, ID_TABULADOR, XML_MIN_MAX, usuario, programa, pClTipoOperacion);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }

       #endregion

       #region  INSERTA ACTUALIZA SUELDO NUEVO GENERAL
       public XElement InsertarActualizarTabuladorSueldo(int? ID_TABULADOR, string TIPO_INCREMENTO, decimal CANTIDAD_INCREMENTO, string CL_USUARIO, string NB_PROGRAMA)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_NUEVO_SUELDO_GENERAL(poutClaveRetorno, ID_TABULADOR, TIPO_INCREMENTO, CANTIDAD_INCREMENTO, CL_USUARIO, NB_PROGRAMA);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }

       #endregion

       #region  COPIA TABULADOR MAESTRO

       public XElement CopiaTabuladorMaestro(int? ID_TABULADOR, int? ID_TABULADOR_COPIAR, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_COPIA_TABULADOR_MAESTRO(poutClaveRetorno, ID_TABULADOR, ID_TABULADOR_COPIAR, usuario, programa);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }

       #endregion

       #region OBTIENE DATOS DEL DESCRIPTIVO DE PUESTOS K_TABULADOR_PUESTO

       public List<SPE_OBTIENE_TABULADOR_DESCRIPTIVO_PUESTO_Result> ObtenerDescriptivosPuestosTabulador(int? ID_TABULADOR_PUESTO = null, int? ID_TABULADOR = null, int? ID_PUESTO = null, string NB_PUESTO = null, decimal? MN_MINIMO = null, decimal? MN_MAXIMO = null, string CL_ORIGEN = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_TABULADOR_DESCRIPTIVO_PUESTO(ID_TABULADOR_PUESTO, ID_TABULADOR, ID_PUESTO, NB_PUESTO, MN_MINIMO, MN_MAXIMO, CL_ORIGEN)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region OBTIENE DATOS  A K_TABULADOR_VALUACION

       public List<SPE_OBTIENE_VALUACION_PUESTO_Result> ObtenerTabuladorValuacion(int ID_TABULADOR)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_VALUACION_PUESTO( ID_TABULADOR)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region INSERTA Y ACTUALIZA DATOS A K_TABULADOR_VALUACION
       public XElement InsertarActualizarTabuladorValuacion(int? ID_TABULADOR, string XML_VALORES ,string XML_NIVEL_PUESTOS,string pGuardar ,string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_TABULADOR_VALUACION(pout_clave_retorno, ID_TABULADOR, XML_VALORES,XML_NIVEL_PUESTOS,pGuardar, usuario, programa );
               return XElement.Parse(pout_clave_retorno.Value.ToString());
           }
       }
       #endregion

       #region INSERTA Y ACTUALIZA NIVELES PUESTOS
       public XElement InsertarActualizarNivelesPuestos(int? ID_TABULADOR, string XML_NIVEL_PUESTOS, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_NIVELES_PUESTOS(pout_clave_retorno, ID_TABULADOR, XML_NIVEL_PUESTOS, usuario, programa);
               return XElement.Parse(pout_clave_retorno.Value.ToString());
           }
       }
       #endregion

       #region OBTIENE LA NIVELACION DE  K_TABULADOR_VALUACION
       public List<SPE_OBTIENE_VALUACION_NIVELACION_Result> ObtenerValuacionNivelacion( int? ID_TABULADOR = null, int? ID_PUESTO = null )
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_VALUACION_NIVELACION( ID_TABULADOR,ID_PUESTO )
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region ACTUALIZA EL NIVEL A K_TABULADOR_PUESTO
       public XElement ActualizarNivelTabuladorPuesto(int? ID_TABULADOR, string XML_NIVELES, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_ACTUALIZA_NIVEL_TABULADOR_PUESTO(pout_clave_retorno, ID_TABULADOR, XML_NIVELES, usuario, programa);
               return XElement.Parse(pout_clave_retorno.Value.ToString());
           }
       }
       #endregion

       #region OBTIENE LOS NIVELES DE  K_TABULADOR_NIVEL
       public List<SPE_OBTIENE_TABULADOR_NIVEL_Result> ObtenerTabuladorNivel(int? ID_TABULADOR = null, int? ID_TABULADOR_NIVEL = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_TABULADOR_NIVEL(ID_TABULADOR, ID_TABULADOR_NIVEL)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region ACTUALIZA EL NIVEL A K_TABULADOR_NIVEL
       public XElement ActualizarTabuladorNivel(int? ID_TABULADOR, string XML_NIVELES, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_ACTUALIZA_TABULADOR_NIVEL(pout_clave_retorno, ID_TABULADOR, XML_NIVELES, usuario, programa);
               return XElement.Parse(pout_clave_retorno.Value.ToString());
           }
       }
       #endregion

       #region OBTIENE DATOS PARA  K_TABULADOR_MAESTRO
       public List<SPE_OBTIENE_TABULADOR_MAESTRO_Result> ObtenerTabuladorMaestro(int? ID_TABULADOR = null, string RECALCULAR = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_TABULADOR_MAESTRO(ID_TABULADOR,RECALCULAR)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region RECALCULAR Y OBTENER TABULADOR MAESTRO
       public List<SPE_OBTIENE_TABULADOR_MAESTRO_RECALCULA_Result> ObtieneTabuladorRecalculado(int? ID_TABULADOR = null, string XML_NIVELES = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_TABULADOR_MAESTRO_RECALCULA(ID_TABULADOR, XML_NIVELES)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

      
       #region  INSERTA ACTUALIZA DATOS  K_TABULADOR_MAESTRO
       public XElement InsertarActualizarTabuladorMaestro(int? ID_TABULADOR, string XML_TABULADOR_MAESTRO, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_TABULADOR_MAESTRO(poutClaveRetorno, ID_TABULADOR, XML_TABULADOR_MAESTRO, usuario, programa);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }

       #endregion

       #region  INSERTA ACTUALIZA DATOS  K_TABULADOR_NIVEL
       public XElement ActualizarMercadoTabuladorNivel(int? ID_TABULADOR, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_ACTUALIZA_MERCADO_TABULADOR_NIVEL(poutClaveRetorno, ID_TABULADOR, usuario, programa);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }
       #endregion

       #region ACTUALIZA PLANEACION DE INCREMENTOS K_TABULADOR_EMPLEADO
       public XElement ActualizarPlaneacionIncrementoEmpleado(int? ID_TABULADOR, string XML_EMPLEADOS, string usuario, string programa)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter pout_clave_retorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_ACTUALIZA_PLAN_INCREMENTO_EMPLEADO(pout_clave_retorno, ID_TABULADOR, XML_EMPLEADOS, usuario, programa);
               return XElement.Parse(pout_clave_retorno.Value.ToString());
           }
       }
       #endregion

       #region OBTIENE PLANEACION DE INCREMENTOS
       public List<SPE_OBTIENE_PLANEACION_INCREMENTOS_Result> ObtenerPlaneacionIncrementos(int? ID_TABULADOR = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_PLANEACION_INCREMENTOS(ID_TABULADOR)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion


       #region OBTIENE CONSULTA SUELDOS
       public List<SPE_OBTIENE_CONSULTA_SUELDOS_Result> ObtenerConsultaSueldos(int? ID_TABULADOR = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_CONSULTA_SUELDOS(ID_TABULADOR)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion

       #region OBTIENE EMPLEADOS PLANEACION DE INCREMENTOS
       public List<SPE_OBTIENE_EMPLEADOS_PLANEACION_INCREMENTOS_Result> ObtenerEmpleadosPlaneacionIncrementos(int? ID_TABULADOR = null)
       {
           using (context = new SistemaSigeinEntities())
           {
               var q = from vTabuladores in context.SPE_OBTIENE_EMPLEADOS_PLANEACION_INCREMENTOS(ID_TABULADOR)
                       select vTabuladores;
               return q.ToList();
           }
       }
       #endregion
       
       #region  INSERTA ACTUALIZA CAMBIO SUELDO
       public XElement InsertarActualizarNuevoSueldo( int ID_TABULADOR,string XML_CAMBIO_SUELDO , bool FG_CAMBIO_SUELDO, string CL_USUARIO,string NB_PROGRAMA)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_INSERTA_ACTUALIZA_NUEVO_SUELDO(poutClaveRetorno, ID_TABULADOR, XML_CAMBIO_SUELDO, FG_CAMBIO_SUELDO, CL_USUARIO, NB_PROGRAMA);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }
       #endregion

       #region  ACTUALIZA NIVEL PUESTO
       public XElement ActualizarNivelPuesto(int ID_TABULADOR,string CL_USUARIO, string NB_PROGRAMA)
       {
           using (context = new SistemaSigeinEntities())
           {
               ObjectParameter poutClaveRetorno = new ObjectParameter("XML_RESULTADO", typeof(XElement));
               context.SPE_ACTUALIZA_NIVEL_PUESTO(poutClaveRetorno, ID_TABULADOR, CL_USUARIO, NB_PROGRAMA);
               return XElement.Parse(poutClaveRetorno.Value.ToString());
           }
       }
       #endregion

    }
}
