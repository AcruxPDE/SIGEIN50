using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGE.Entidades;
using SIGE.AccesoDatos.Implementaciones.MetodologiaCompensacion;
using SIGE.Entidades.MetodologiaCompensacion;
using System.Xml.Linq;
using SIGE.Negocio.Utilerias;
using SIGE.Entidades.Externas;
using System.Data;


namespace SIGE.Negocio.MetodologiaCompensacion
{
   public class TabuladoresNegocio
    {
       #region OBTIENE DATOS  K_TABULADOR
       public List<SPE_OBTIENE_TABULADORES_Result> ObtenerTabuladores(int? ID_TABULADOR = null, string CL_TABULADOR = null, string NB_TABULADOR = null, string DS_TABULADOR = null, DateTime? FE_VIGENCIA = null, string CL_TIPO_PUESTO = null, string CL_ESTADO = null, byte? NO_NIVELES = null, byte? NO_CATEGORIAS = null, decimal? PR_PROGRESION = null, decimal? PR_INFLACION = null, int? ID_CUARTIL_INFLACIONAL = null, int? ID_CUARTIL_INCREMENTO = null, int? ID_CUARTIL_MERCADO = null ,string CL_SUELDO_COMPARACION = null, string XML_VARIACION = null, string CL_ORIGEN_NIVELES = null ,DateTime? FE_CREACION = null, string CL_USUARIO_APP_CREA = null)
        {
            TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
            return operaciones.ObtenerTabuladores(ID_TABULADOR, CL_TABULADOR, NB_TABULADOR, DS_TABULADOR, FE_VIGENCIA, CL_TIPO_PUESTO, CL_ESTADO, NO_NIVELES, NO_CATEGORIAS, PR_PROGRESION, PR_INFLACION, ID_CUARTIL_INFLACIONAL, ID_CUARTIL_INCREMENTO,ID_CUARTIL_MERCADO ,CL_SUELDO_COMPARACION, XML_VARIACION, CL_ORIGEN_NIVELES, FE_CREACION, CL_USUARIO_APP_CREA);
        }


        #endregion
       
      #region VERIFICA CONFIGURACIÓN  K_TABULADOR
       public List<SPE_VERIFICA_CONFIGURACION_TABULADOR_Result> VerificarTabulador(int? ID_TABULADOR = null)
        {
            TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
            return operaciones.VerificarTabulador(ID_TABULADOR);
        }


        #endregion

       #region OBTIENE DATOS  K_TABULADOR_NIVEL
       public List<SPE_OBTIENE_TABULADORES_NIVEL_Result> ObtenerTabuladoresNivel(int? ID_TABULADOR = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerTabuladoresNivel(ID_TABULADOR);
       }
       #endregion
       
       #region INSERTA ACTUALIZA DATOS  K_TABULADOR
       public E_RESULTADO InsertaActualizaTabulador(String pClTipoOperacion, E_TABULADOR vTabulador, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarTabulador(pClTipoOperacion, vTabulador, usuario, programa));
       }
       #endregion

       
       #region ACTUALIZA ESTATUS  K_TABULADOR
       public E_RESULTADO ActualizarEstatusTabulador(int? pID_TABULADOR, string pCL_ESTATUS_TABULADOR, string usuario, string programa, string pClTipoOperacion)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarEstatusTabulador(pID_TABULADOR, pCL_ESTATUS_TABULADOR, usuario, programa, pClTipoOperacion));
       }
       #endregion
       
       #region INSERTA DATOS COPIA K_TABULADOR
       public E_RESULTADO InsertaTabuladorCopia(String pClTipoOperacion, E_TABULADOR vTabulador, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarTabuladorCopia(pClTipoOperacion, vTabulador, usuario, programa));
       }
       #endregion

       #region INSERTA ACTUALIZA DATOS  K_TABULADOR_FACTOR
       public E_RESULTADO InsertaActualizaTabuladorFactor(String pClTipoOperacion, E_EMPLEADOS_SELECCIONADOS vEmpleados, string pNiveles, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarTabuladorFactor(pClTipoOperacion, vEmpleados, pNiveles, usuario, programa));
       }
       #endregion

       #region INSERTA FACTOR EVALUACION
       public E_RESULTADO InsertarFactorTabulador(String pClTipoOperacion, E_EMPLEADOS_SELECCIONADOS vEmpleados, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarFactorTabulador(pClTipoOperacion, vEmpleados, usuario, programa));
       }
       #endregion

       #region ELIMINA DATOS  K_TABULADOR
       public E_RESULTADO EliminaTabulador(int pIdTabulador, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.EliminarTabulador(pIdTabulador, usuario, programa));
       }
       #endregion

       #region INSERTA ACTUALIZA DATOS  K_TABULADOR_EMPLEADO
       public E_RESULTADO InsertaActualizaTabuladorEmpleado(String pClTipoOperacion, int ID_TABULADOR, string XML_SELECCIONADOS, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarTabuladorEmpleado(pClTipoOperacion, ID_TABULADOR, XML_SELECCIONADOS, usuario, programa));
       }
       #endregion

       #region OBTIENE DATOS  K_TABULADOR_EMPLEADO
       public List<SPE_OBTIENE_EMPLEADOS_TABULADOR_Result> ObtenieneEmpleadosTabulador(int? ID_TABULADOR = null, XElement XML_SELECCIONADOS = null, int? ID_EMPRESA = null, int? pIdRol = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerEmpleadosTabulador(ID_TABULADOR, XML_SELECCIONADOS, ID_EMPRESA, pIdRol);
       }
       #endregion

       #region OBTIENE DATOS  K_TABULADOR_FACTOR
       public List<SPE_OBTIENE_COMPETENCIAS_TABULADOR_Result> ObtenieneCompetenciasTabulador(int? ID_TABULADOR_FACTOR = null, string NB_COMPETENCIA = null, string DS_COMPETENCIA = null, int? ID_TABULADOR = null, int? ID_COMPETENCIA = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerCompetenciasTabulador(ID_TABULADOR_FACTOR, NB_COMPETENCIA, DS_COMPETENCIA, ID_TABULADOR, ID_COMPETENCIA);
       }
       #endregion

       #region ELIMINA DATOS  K_TABULADOR_EMPLEADO
       public E_RESULTADO EliminaTabuladorEmpleado( string usuario, string programa, string XML_TABULADOR_EMPLEADOS)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.EliminarTabuladorEmpleado(usuario, programa,XML_TABULADOR_EMPLEADOS));
       }
       #endregion

       #region ELIMINA DATOS  K_TABULADOR_FACTOR
       public E_RESULTADO EliminaTabuladorFactor(string usuario, string programa, string XML_TABULADOR_FACTOR)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.EliminarTabuladorFactor( usuario, programa,XML_TABULADOR_FACTOR));
       }
       #endregion

       #region OBTIENE DATOS  K_TABULADOR_PUESTO
       public List<SPE_OBTIENE_TABULADOR_PUESTO_Result> ObtienePuestosTabulador(int? ID_TABULADOR = null, int? ID_PUESTO = null )
        {
            TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
            return operaciones.ObtenerPuestosTabulador(ID_TABULADOR, ID_PUESTO);
        }
        #endregion

       #region INSERTA ACTUALIZA DATOS  K_TABULADOR_PUESTO
       public E_RESULTADO InsertaActualizaTabuladorPuesto(string pClTipoOperacion, int? ID_TABULADOR, string XML_MIN_MAX, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarTabuladorPuesto(pClTipoOperacion,ID_TABULADOR, XML_MIN_MAX, usuario, programa));
       }
       #endregion

       #region INSERTA ACTUALIZA SUELDO NUEVO GENERAL
       public E_RESULTADO InsertarActualizarTabuladorSueldo(int? ID_TABULADOR, string TIPO_INCREMENTO, decimal CANTIDAD_INCREMENTO, string CL_USUARIO, string NB_PROGRAMA)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarTabuladorSueldo(ID_TABULADOR, TIPO_INCREMENTO, CANTIDAD_INCREMENTO, CL_USUARIO, NB_PROGRAMA));
       }
       #endregion

       #region COPIA TABULADOR MAESTRO
       public E_RESULTADO CopiaTabuladorMaestro(int? ID_TABULADOR, int? ID_TABULADOR_COPIAR, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.CopiaTabuladorMaestro(ID_TABULADOR, ID_TABULADOR_COPIAR, usuario, programa));
       }
       #endregion

       #region OBTENER COPIA MERCADO_SALARIAL

       public List<SPE_OPTIENE_COPIA_MERCADO_SALARIAL_Result> ObtenerMercadoSalarialTabulador(int? ID_TABULADOR = null, int? ID_TABULADOR_COPIAR = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerMercadoSalarialTabulador(ID_TABULADOR,ID_TABULADOR_COPIAR);
       }

       #endregion

       #region OBTIENE DATOS DEL DESCRIPTIVO DE PUESTOS K_TABULADOR_PUESTO
       public List<SPE_OBTIENE_TABULADOR_DESCRIPTIVO_PUESTO_Result> ObtieneDescriptivosPuestosTabulador(int? ID_TABULADOR_PUESTO = null, int? ID_TABULADOR = null, int? ID_PUESTO = null, string NB_PUESTO = null, decimal? MN_MINIMO = null, decimal? MN_MAXIMO = null, string CL_ORIGEN = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerDescriptivosPuestosTabulador(ID_TABULADOR_PUESTO, ID_TABULADOR, ID_PUESTO, NB_PUESTO, MN_MINIMO, MN_MAXIMO, CL_ORIGEN);
       }
       #endregion

       #region OBTIENE DATOS DE VALUACION 
       public List<SPE_OBTIENE_VALUACION_PUESTO_Result> ObtieneDatosValuacion(int ID_TABULADOR)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerTabuladorValuacion(ID_TABULADOR);
       }
       #endregion
       
       #region OBTIENE DATOS A K_TABULADOR_VALUACION
       public DataTable ObtieneTabuladorValuacion( int ID_TABULADOR , ref List<E_VALUACION> vLstValuacion)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           //return operaciones.ObtenerTabuladorValuacion(ID_TABULADOR_VALUACION, ID_TABULADOR, ID_PUESTO, ID_COMPETENCIA);
           List<SPE_OBTIENE_VALUACION_PUESTO_Result> vListaValuacion = new List<SPE_OBTIENE_VALUACION_PUESTO_Result>();
           List<E_VALUACION> vListaFactorsValuacion = new List<E_VALUACION>();

           DataTable vDtPivot = new DataTable();

           vListaValuacion = operaciones.ObtenerTabuladorValuacion(ID_TABULADOR);
           vLstValuacion = vListaValuacion.Select(s => new E_VALUACION
           {
              CL_PUESTO = s.CL_PUESTO,
              ID_COMPETENCIA = s.ID_COMPETENCIA,
              ID_PUESTO = s.ID_PUESTO,
              ID_TABULADOR_FACTOR = s.ID_TABULADOR_FACTOR,
              ID_TABULADOR_PUESTO = s.ID_TABULADOR_PUESTO,
              ID_TABULADOR_VALUACION = s.ID_TABULADOR_VALUACION,
              NB_COMPETENCIA = s.NB_COMPETENCIA,
              DS_COMPETENCIA = s.DS_COMPETENCIA,
              NB_PUESTO = s.NB_PUESTO,
              //NB_TIPO_COMPETENCIA = s.NB_TIPO_COMPETENCIA,
              NO_VALOR = s.NO_VALOR,
              NO_PROMEDIO_VALUACION = s.NO_PROMEDIO_VALUACION,
              NO_NIVEL = s.NO_NIVEL
              ,CL_TIPO_COMPETENCIA = s.CL_TIPO_COMPETENCIA
           }).ToList();

           vDtPivot.Columns.Add("ID_PUESTO", typeof(int));
           vDtPivot.Columns.Add("ID_TABULADOR_PUESTO", typeof(int));
           //vDtPivot.Columns.Add("ID_TABULADOR_FACTOR", typeof(int));
           vDtPivot.Columns.Add("NB_PUESTO", typeof(string));
           
           var vLstCompetencias = (from a in vListaValuacion select new { a.ID_COMPETENCIA, a.NB_COMPETENCIA,a.ID_TABULADOR_FACTOR, a.CL_TIPO_COMPETENCIA }).Distinct().OrderBy(t => t.ID_COMPETENCIA);
          
           var vLstPuestos = (from a in vListaValuacion
                                   select new
                                   {
                                      a.CL_PUESTO,
                                      a.NB_PUESTO,
                                      a.ID_TABULADOR_PUESTO,
                                      a.ID_PUESTO,
                                      a.NO_PROMEDIO_VALUACION,
                                      a.NO_NIVEL
                                   }).Distinct().OrderByDescending(o=> o.NO_PROMEDIO_VALUACION).OrderBy(t => t.NO_NIVEL);

           //foreach (var item in vlstcompetencias)
           //{
           //    vdtpivot.columns.add(item.id_tabulador_factor.tostring() + "f");
           //}

           foreach (var item in vLstCompetencias.Where(t => t.CL_TIPO_COMPETENCIA == "GEN"))
           {
               
               E_VALUACION f = new E_VALUACION
               {
                   ID_COMPETENCIA = item.ID_COMPETENCIA,
                   NB_COMPETENCIA = item.NB_COMPETENCIA,
                   ID_TABULADOR_FACTOR = item.ID_TABULADOR_FACTOR,
                   CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA
               };
               vListaFactorsValuacion.Add(f);
                   
           }

           foreach (var item in vLstCompetencias.Where(t => t.CL_TIPO_COMPETENCIA == "ESP"))
           {
               E_VALUACION f = new E_VALUACION
               {
                   ID_COMPETENCIA = item.ID_COMPETENCIA,
                   NB_COMPETENCIA = item.NB_COMPETENCIA,
                   ID_TABULADOR_FACTOR = item.ID_TABULADOR_FACTOR,
                   CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA
               };
               vListaFactorsValuacion.Add(f);

           }

           foreach (var item in vLstCompetencias.Where(t => t.CL_TIPO_COMPETENCIA == "FACTOR"))
           {
               E_VALUACION f = new E_VALUACION
               {
                   ID_COMPETENCIA = item.ID_COMPETENCIA,
                   NB_COMPETENCIA = item.NB_COMPETENCIA,
                   ID_TABULADOR_FACTOR = item.ID_TABULADOR_FACTOR,
                   CL_TIPO_COMPETENCIA = item.CL_TIPO_COMPETENCIA
               };
               vListaFactorsValuacion.Add(f);

           }


           foreach (var item in vListaFactorsValuacion)
            {
                vDtPivot.Columns.Add(item.ID_COMPETENCIA.ToString() + "E" + "," + item.ID_TABULADOR_FACTOR.ToString() + "F");
            }

           //vDtPivot.Columns.Add("ID_COMPETENCIA", typeof(int));
           vDtPivot.Columns.Add("NO_PROMEDIO_VALUACION", typeof(decimal));
           vDtPivot.Columns.Add("NO_NIVEL", typeof(int));


           foreach (var vPues in vLstPuestos)
           {
               DataRow vDr = vDtPivot.NewRow();
               vDr["ID_PUESTO"] =vPues.ID_PUESTO;
               vDr["ID_TABULADOR_PUESTO"] = vPues.ID_TABULADOR_PUESTO;
               vDr["NB_PUESTO"] = vPues.NB_PUESTO;
               vDr["NO_PROMEDIO_VALUACION"] = vPues.NO_PROMEDIO_VALUACION;
               vDr["NO_NIVEL"] = vPues.NO_NIVEL;


               foreach (var vCom in vListaFactorsValuacion)
               {
                   var vResultado = vListaValuacion.Where(t => t.ID_PUESTO == vPues.ID_PUESTO & t.ID_COMPETENCIA == vCom.ID_COMPETENCIA).FirstOrDefault();
                   if (vResultado != null)
                   {
                       //vDr["ID_COMPETENCIA"] = vResultado.ID_COMPETENCIA;
                       vDr[vCom.ID_COMPETENCIA.ToString() + "E" + "," + vCom.ID_TABULADOR_FACTOR.ToString() + "F"] = vResultado.NO_VALOR;
                       //vDr[vCom.ID_TABULADOR_FACTOR.ToString() + "F"] = vResultado.ID_TABULADOR_FACTOR;
                   }
                   //else {
                   //    vDr["ID_COMPETENCIA"] = vResultado.ID_COMPETENCIA;
                   //    vDr[vCom.ID_COMPETENCIA.ToString() + "E"] = 0;
                   //}
               }


               vDtPivot.Rows.Add(vDr);
           }
           return vDtPivot;
       }
       #endregion

       #region INSERTA ACTUALIZA DATOS A K_TABULADOR_VALUACION
       public E_RESULTADO InsertaActualizaTabuladorValuacion(int ID_TABULADOR, string XML_VALORES,string XML_NIVEL_PUESTOS ,string pGuardar ,string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           XElement vRespuestaXML = operaciones.InsertarActualizarTabuladorValuacion(ID_TABULADOR, XML_VALORES,XML_NIVEL_PUESTOS,pGuardar, usuario, programa);
           E_RESULTADO vResultado = new E_RESULTADO(vRespuestaXML);
           return vResultado;
       }
       #endregion

       #region INSERTA Y ACTUALIZA NIVELES PUESTOS
       public E_RESULTADO InsertarActualizarNivelesPuestos(int? ID_TABULADOR, string XML_NIVEL_PUESTOS, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           XElement vRespuestaXML = operaciones.InsertarActualizarNivelesPuestos(ID_TABULADOR, XML_NIVEL_PUESTOS, usuario, programa);
           E_RESULTADO vResultado = new E_RESULTADO(vRespuestaXML);
           return vResultado;
       }
       #endregion

       #region OBTIENE LA NIVELACION A K_TABULADOR_VALUACION
       public List<SPE_OBTIENE_VALUACION_NIVELACION_Result> ObtieneValuacionNivelacion( int? ID_TABULADOR = null, int? ID_PUESTO = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerValuacionNivelacion(ID_TABULADOR, ID_PUESTO);
       }
       #endregion

       #region ACTUALIZA NIVEL A K_TABULADOR_PUESTO
       public E_RESULTADO ActualizaNivelTabuladorPuesto(int? ID_TABULADOR, string XML_NIVELES, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarNivelTabuladorPuesto( ID_TABULADOR, XML_NIVELES, usuario, programa));
       }
       #endregion

       #region OBTIENE LOS NIVELES DE K_TABULADOR_NIVEL
       public List<SPE_OBTIENE_TABULADOR_NIVEL_Result> ObtieneTabuladorNivel(int? ID_TABULADOR = null, int? ID_TABULADOR_NIVEL = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerTabuladorNivel(ID_TABULADOR, ID_TABULADOR_NIVEL);
       }
        #endregion

        #region OBTIENE EL NIVEL MAXIMO QUE SE PUEDE GENERAR DE ACUERDO A LOS PUESTOS
        public List<int> ObtieneMaximoNivel(int? ID_TABULADOR = null)
        {
            TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
            return operaciones.ObtieneMaximoNivel(ID_TABULADOR);
        }
        #endregion

        #region OBTIENE EL TOTAL DE NIVELES DE K_TABULADOR_NIVEL
        public int ObtieneTotalTabuladorNivel(int? ID_TABULADOR)
        {
            TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
            return operaciones.ObtieneTotalTabuladorNivel(ID_TABULADOR);
        }
        #endregion

        #region ACTUALIZA NIVEL A K_TABULADOR_NIVEL
        public E_RESULTADO ActualizaTabuladorNivel(int? ID_TABULADOR, string XML_NIVELES, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarTabuladorNivel(ID_TABULADOR, XML_NIVELES, usuario, programa));
       }
       #endregion

       #region OBTIENE LOS NIVELES DE K_TABULADOR_MAESTRO
       public List<SPE_OBTIENE_TABULADOR_MAESTRO_Result> ObtieneTabuladorMaestro(int? ID_TABULADOR = null,string RECALCULAR = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerTabuladorMaestro(ID_TABULADOR,RECALCULAR);
       }
       #endregion


       #region RECALCULAR Y OBTENER TABULADOR MAESTRO
       public List<SPE_OBTIENE_TABULADOR_MAESTRO_RECALCULA_Result> ObtieneTabuladorRecalculado(int? ID_TABULADOR = null, string XML_NIVELES = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtieneTabuladorRecalculado(ID_TABULADOR, XML_NIVELES);
       }
       #endregion

       #region INSERTA ACTUALIZA DATOS  K_TABULADOR_MAESTRO
       public E_RESULTADO InsertaActualizaTabuladorMaestro(int? ID_TABULADOR, string XML_TABULADOR_MAESTRO, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarTabuladorMaestro( ID_TABULADOR, XML_TABULADOR_MAESTRO, usuario, programa));
       }
       #endregion

       #region ACTUALIZA MN_MAXIMO Y MN_MINIMO DEL MERCADO SALARIAL A K_TABULADOR_NIVEL
       public E_RESULTADO ActualizaMercadoTabuladorNivel(int? ID_TABULADOR, string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarMercadoTabuladorNivel(ID_TABULADOR, usuario, programa));
       }
       #endregion

       #region ACTUALIZA PLANEACION DE INCREMENTO K_TABULADOR_EMPLEADOS
       public E_RESULTADO ActualizaPlaneacionIncrementosEmpleado(int? ID_TABULADOR,string XML_EMPLEADOS ,string usuario, string programa)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarPlaneacionIncrementoEmpleado(ID_TABULADOR,XML_EMPLEADOS ,usuario, programa));
       }
       #endregion

       #region OBTIENE PLANEACION DE INCREMENTOS
       public List<SPE_OBTIENE_PLANEACION_INCREMENTOS_Result> ObtienePlaneacionIncrementos(int? ID_TABULADOR = null, int? ID_ROL = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerPlaneacionIncrementos(ID_TABULADOR, ID_ROL);
       }
       #endregion

       #region OBTIENE CONSULTA SUELDOS
        public List<SPE_OBTIENE_CONSULTA_SUELDOS_Result> ObtenerConsultaSueldos(int? ID_TABULADOR = null, int? ID_ROL = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerConsultaSueldos(ID_TABULADOR, ID_ROL);
       }
         #endregion

       #region OBTIENE EMPLEADOS PLANEACION DE INCREMENTOS
       public List<SPE_OBTIENE_EMPLEADOS_PLANEACION_INCREMENTOS_Result> ObtieneEmpleadosPlaneacionIncrementos(int? ID_TABULADOR = null, int? pID_ROL = null, bool? pFG_PLANECAION = null)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return operaciones.ObtenerEmpleadosPlaneacionIncrementos(ID_TABULADOR, pID_ROL, pFG_PLANECAION);
       }
        #endregion

        #region OBTIENE LOS VALORES DE VALUACION
        public List<decimal> ObtieneValoresValuacion(int? ID_TABULADOR = null)
        {
            TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
            return operaciones.ObtieneValoresValuacion(ID_TABULADOR);
        }       

        #endregion

        //#region OBTIENE CONSULTA TABULADOR SUELDOS
        //public List<SPE_OBTIENE_PLANEACION_INCREMENTOS_Result> ObtieneTabuladorSueldos(int? ID_TABULADOR = null, ref List<E_PLANEACION_INCREMENTOS> vLstTabuladorSueldos)
        //{
        //    TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
        //    //return operaciones.ObtenerPlaneacionIncrementos(ID_TABULADOR);

        //    List<SPE_OBTIENE_PLANEACION_INCREMENTOS_Result> vListaTabuladorSueldos = new List<SPE_OBTIENE_PLANEACION_INCREMENTOS_Result>();
        //    //string vDivsCeldasChk = "<div class=\"divCheckbox\"> <input type=\"checkbox\" runat=\"server\" class=\"{4}\" id=\"{2}\" value=\"{2}\" {3}> </div>  <div class=\"divPorcentaje\">{0:N2}</div><div class=\"{1}\">&nbsp;</div>";
        //    string vNbPorcentaje = "";
        //    string vDivsCeldasPo = "<table class=\"tablaColor\"> " +
        //        "<tr> " +
        //        "<td class=\"porcentaje\"> " +
        //        "<div class=\"divPorcentaje\">{0}</div> " +
        //        "</td> " +
        //        "<td class=\"color\"> " +
        //        "<div class=\"{1}\">&nbsp;</div> " +
        //        "</td> </tr> </table>";
        //    string vClaseDivs = "";
        //    string vClaseColor = "";

        //    DataTable vDtPivot = new DataTable();

        //    vListaTabuladorSueldos = operaciones.ObtenerPlaneacionIncrementos(ID_TABULADOR);
        //    vLstTabuladorSueldos = vListaTabuladorSueldos.Select(s => new E_PLANEACION_INCREMENTOS
        //    {
        //        ID_TABULADOR_EMPLEADO = s.ID_TABULADOR_EMPLEADO,
        //        NB_TABULADOR_NIVEL = s.NB_TABULADOR_NIVEL,
        //        CL_PUESTO = s.CL_PUESTO,
        //        NB_PUESTO = s.NB_PUESTO,
        //        CL_EMPLEADO = s.CL_EMPLEADO,
        //        NB_EMPLEADO = s.NOMBRE,
        //        MN_MINIMO_MINIMO = s.MN_MINIMO_MINIMO,
        //        MN_MAXIMO_MINIMO = s.MN_MAXIMO_MINIMO,
        //        MN_MINIMO_PRIMER_CUARTIL = s.MN_MINIMO_PRIMER_CUARTIL,
        //        MN_MAXIMO_PRIMER_CUARTIL = s.MN_MAXIMO_PRIMER_CUARTIL,
        //        MN_MINIMO_MEDIO = s.MN_MINIMO_MEDIO,
        //        MN_MAXIMO_MEDIO = s.MN_MAXIMO_MEDIO,
        //        MN_MINIMO_SEGUNDO_CUARTIL = s.MN_MINIMO_SEGUNDO_CUARTIL,
        //        MN_MAXIMO_SEGUNDO_CUARTIL = s.MN_MAXIMO_SEGUNDO_CUARTIL,
        //        MN_MAXIMO_MAXIMO = s.MN_MAXIMO_MAXIMO,
        //        MN_MINIMO_MAXIMO = s.MN_MINIMO_MAXIMO,
        //        MN_SUELDO_ORIGINAL = s.MN_SUELDO_ORIGINAL,
        //        MN_SUELDO_NUEVO = s.MN_SUELDO_NUEVO,
        //        MN_MINIMO = s.MN_MINIMO,
        //        MN_MAXIMO = s.MN_MAXIMO,
        //        INCREMENTO = Math.Abs(s.MN_SUELDO_NUEVO - s.MN_SUELDO_ORIGINAL),
        //        NO_NIVEL = s.NO_NIVEL,
        //        XML_CATEGORIAS = s.XML_CATEGORIA,
        //        //CUARTIL_SELECCIONADO = vCuartilComparativo
        //    }).ToList();


        //    foreach (XElement vXmlSecuencia in .Elements("ITEM")){
        //         lstCategoria.Add(new E_CATEGORIA
        //         {
        //             NO_CATEGORIA = UtilXML.ValorAtributo<int>(vXmlSecuencia.Attribute("NO_CATEGORIA")),
        //             MN_MINIMO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MINIMO")),
        //             MN_PRIMER_CUARTIL = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_PRIMER_CUARTIL")),
        //             MN_MEDIO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MEDIO")),
        //             MN_SEGUNDO_CUARTIL = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_SEGUNDO_CUARTIL")),
        //             MN_MAXIMO = UtilXML.ValorAtributo<decimal>(vXmlSecuencia.Attribute("MN_MAXIMO"))
        //         });
        //     }


        //    vDtPivot.Columns.Add("ID_COMPETENCIA", typeof(int));
        //    vDtPivot.Columns.Add("CL_COLOR", typeof(string));
        //    vDtPivot.Columns.Add("NB_COMPETENCIA", typeof(string));
        //    vDtPivot.Columns.Add("DS_COMPETENCIA", typeof(string));

        //    var vLstEmpleados = (from a in vListaTabuladorSueldos select new { a.ID_TABULADOR_EMPLEADO}).Distinct().OrderBy(t => t.);
        //    var vLstCompetencias = (from a in vListaTabuladorSueldos
        //                            select new
        //                            {
        //                                a.ID_COMPETENCIA,
        //                                a.CL_COLOR,
        //                                a.NB_COMPETENCIA,
        //                                a.DS_COMPETENCIA
        //                            }).Distinct().OrderBy(t => t.ID_COMPETENCIA);








        //}
        //#endregion

        #region INSERTA ACTUALIZA NUEVO SUELDO
        public E_RESULTADO InsertaActualizaNuevoSueldo(int ID_TABULADOR, string XML_CAMBIO_SUELDO,bool FG_CAMBIO_SUELDO, string CL_USUARIO, string NB_PROGRAMA)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.InsertarActualizarNuevoSueldo(ID_TABULADOR, XML_CAMBIO_SUELDO,FG_CAMBIO_SUELDO, CL_USUARIO, NB_PROGRAMA));
       }
       #endregion

       #region ACTUALIZA NIVEL PUESTO
       public E_RESULTADO ActualizaNivelPuesto(int ID_TABULADOR,string CL_USUARIO, string NB_PROGRAMA)
       {
           TabuladoresOperaciones operaciones = new TabuladoresOperaciones();
           return UtilRespuesta.EnvioRespuesta(operaciones.ActualizarNivelPuesto(ID_TABULADOR, CL_USUARIO, NB_PROGRAMA));
       }
       #endregion
    }
}
