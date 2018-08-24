using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using System.Xml.Linq;

namespace WebApp.ws
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOperacionesGral" in both code and config file together.
    [ServiceContract]
    public interface IOperacionesGral
    {

//        #region S_TIPO_COMPETENCIA
        
//        [OperationContract]
//        List<SPE_OBTIENE_S_TIPO_COMPETENCIA_Result> Get_TIPO_COMPETENCIA(string CL_TIPO_COMPETENCIA = null, string NB_TIPO_COMPETENCIA = null, string DS_TIPO_COMPETENCIA = null);

//        [OperationContract]
//        bool Insert_update_S_TIPO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_S_TIPO_COMPETENCIA_Result V_S_TIPO_COMPETENCIA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_S_TIPO_COMPETENCIA(String CL_TIPO_COMPETENCIA = null, string usuario = null, string programa = null);

//        #endregion
        
//        #region Login
//        [OperationContract]
//        string Login(E_LOGIN usuario);
//        #endregion

//        #region S_TIPO_COMPETENCI
//        [OperationContract]
//        bool Insert_M_DEPARTAMENTO(SPE_OBTIENE_M_DEPARTAMENTO_Result departamento, string CL_USUARIO, string NB_PROGRAMA, string CL_TIPO);

//        [OperationContract]
//        bool Delete_M_DEPARTAMENTO(int departamento, string CL_USUARIO, string NB_PROGRAMA);

//        [OperationContract]
//        List<SPE_OBTIENE_M_DEPARTAMENTO_Result> Get_M_DEPARTAMENTO(int? ID_DEPARTAMENTO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, String CL_DEPARTAMENTO = null, String NB_DEPARTAMENTO = null, String XML_CAMPOS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);
//        #endregion

//        #region C_CALLE
            
//        [OperationContract]
//        List<SPE_OBTIENE_C_CALLE_Result> Get_C_CALLE(int? ID_CALLE = null, String CL_PAIS = null, String CL_ESTADO = null, String CL_MUNICIPIO = null, String CL_COLONIA = null, String CL_CALLE = null, String NB_CALLE = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_CALLE(string tipo_transaccion, SPE_OBTIENE_C_CALLE_Result V_C_CALLE, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_CALLE(int? ID_CALLE = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_CAMPO_ADICIONAL

//        [OperationContract]
//        List<SPE_OBTIENE_C_CAMPO_ADICIONAL_Result> Get_C_CAMPO_ADICIONAL(int? ID_CAMPO = null, String CL_CAMPO = null, String NB_CAMPO = null, String DS_CAMPO = null, bool? FG_REQUERIDO = null, String NO_VALOR_DEFECTO = null, String CL_TIPO_DATO = null, String CL_DIMENSION = null, String CL_TABLA_REFERENCIA = null, String CL_ESQUEMA_REFERENCIA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, bool? FG_MOSTRAR = null, int? ID_CATALOGO_LISTA = null, bool? FG_ADICIONAL = null);

//        [OperationContract]
//        bool Insert_update_C_CAMPO_ADICIONAL(string tipo_transaccion, SPE_OBTIENE_C_CAMPO_ADICIONAL_Result V_C_CAMPO_ADICIONAL, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_CAMPO_ADICIONAL(int? ID_CAMPO = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_CAMPO_ADICIONAL_XML
//        [OperationContract]
//        List<SPE_OBTIENE_C_CAMPO_ADICIONAL_XML_Result> Get_C_CAMPO_ADICIONAL_XML(String XML=null,int? ID_CAMPO = null, String CL_CAMPO = null, String NB_CAMPO = null, String DS_CAMPO = null, bool? FG_REQUERIDO = null, String NO_VALOR_DEFECTO = null, String CL_TIPO_DATO = null, String CL_DIMENSION = null, String CL_TABLA_REFERENCIA = null, String CL_ESQUEMA_REFERENCIA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, bool? FG_MOSTRAR = null, int? ID_CATALOGO_LISTA = null, bool? FG_ADICIONAL = null);

//        #endregion

//        #region C_COLONIA

//        [OperationContract]
//        List<SPE_OBTIENE_C_COLONIA_Result> Get_C_COLONIA(int? ID_COLONIA = null, String CL_PAIS = null, String CL_ESTADO = null, String CL_MUNICIPIO = null, String CL_COLONIA = null, String NB_COLONIA = null, String CL_TIPO_ASENTAMIENTO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, String CL_CODIGO_POSTAL = null);

//        [OperationContract]
//        List<SPE_OBTENER_TIPO_ASENTAMIENTO_Result> Get_TIPO_ASENTAMIENTO(); // Obtener_TIPO_ASENTAMIENTO
        
//        [OperationContract]
//        bool Insert_update_C_COLONIA(string tipo_transaccion, SPE_OBTIENE_C_COLONIA_Result V_C_COLONIA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_COLONIA(int? ID_COLONIA = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_COMPETENCIA

//        [OperationContract]
//        //List<SPE_OBTIENE_C_COMPETENCIA_Result> Get_C_COMPETENCIA(int? ID_COMPETENCIA = null, String CL_COMPETENCIA = null, String NB_COMPETENCIA = null, String DS_COMPETENCIA = null, String CL_TIPO_COMPETENCIA = null, String CL_CLASIFICACION = null, bool? FG_ACTIVO = null, String XML_CAMPOS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);
//        List<SPE_OBTIENE_C_COMPETENCIA_Result> Get_C_COMPETENCIA(int? ID_COMPETENCIA = null, string CL_COMPETENCIA = null, string NB_COMPETENCIA = null, string DS_COMPETENCIA = null, string CL_TIPO_COMPETENCIA = null, string CL_CLASIFICACION = null, bool? FG_ACTIVO = null, string XML_CAMPOS_ADICIONALES = null);


//        [OperationContract]
//        bool Insert_update_C_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_C_COMPETENCIA_Result V_C_COMPETENCIA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_COMPETENCIA(int? ID_COMPETENCIA = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_DEPENDIENTE_ECONOMICO

//        [OperationContract]
//        List<SPE_OBTIENE_C_DEPENDIENTE_ECONOMICO_Result> Get_C_DEPENDIENTE_ECONOMICO(int? ID_DEPENDIENTE_ECONOMICO = null, String NB_DEPENDIENTE_ECONOMICO = null, String CL_PARENTEZCO = null, String CL_GENERO = null, DateTime? FE_NACIMIENTO = null, int? ID_BITACORA = null, bool? CL_OCUPACION = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_DEPENDIENTE_ECONOMICO(string tipo_transaccion, SPE_OBTIENE_C_DEPENDIENTE_ECONOMICO_Result V_C_DEPENDIENTE_ECONOMICO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_DEPENDIENTE_ECONOMICO(int? ID_DEPENDIENTE_ECONOMICO = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_DOCUMENTO

//        [OperationContract]
//        List<SPE_OBTIENE_C_DOCUMENTO_Result> Get_C_DOCUMENTO(int? ID_DOCUMENTO = null, String NB_DOCUMENTO = null, String CL_DOCUMENTO = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, String CL_RUTA = null, String CL_TIPO_RUTA = null, DateTime? FE_RECEPCION = null, int? ID_INSTITUCION = null, int? ID_BITACORA = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_DOCUMENTO(string tipo_transaccion, SPE_OBTIENE_C_DOCUMENTO_Result V_C_DOCUMENTO, byte[] archivo, string ruta, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_DOCUMENTO(int? ID_DOCUMENTO = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_EMPLEADO_ESCOLARIDAD

//        [OperationContract]
//        List<SPE_OBTIENE_C_EMPLEADO_ESCOLARIDAD_Result> Get_C_EMPLEADO_ESCOLARIDAD(int? ID_EMPLEADO_ESCOLARIDAD = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_ESCOLARIDAD = null, int? CL_INSTITUCION = null, String NB_INSTITUCION = null, DateTime? FE_PERIODO_INICIO = null, DateTime? FE_PERIODO_FIN = null, String CL_ESTADO_ESCOLARIDAD = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_EMPLEADO_ESCOLARIDAD(E_EMPLEADO_ESCOLARIDAD PRIMARIA, E_EMPLEADO_ESCOLARIDAD SECUNDARIA, E_EMPLEADO_ESCOLARIDAD PREPA, E_EMPLEADO_ESCOLARIDAD UNI1, E_EMPLEADO_ESCOLARIDAD UNI2, E_EMPLEADO_ESCOLARIDAD POS1, E_EMPLEADO_ESCOLARIDAD POS2, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_EMPLEADO_ESCOLARIDAD(int? ID_EMPLEADO_ESCOLARIDAD = null, string usuario = null, string programa = null);

//        #endregion


//        #region C_EMPLEADO_IDIOMA

//        [OperationContract]
//        List<SPE_OBTIENE_C_EMPLEADO_IDIOMA_Result> Get_C_EMPLEADO_IDIOMA(int? ID_EMPLEADO_IDIOMA = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_IDIOMA = null, Decimal? PR_LECTURA = null, Decimal? PR_ESCRITURA = null, Decimal? PR_CONVERSACIONAL = null, int? CL_INSTITUCION = null, Decimal? NO_PUNTAJE = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_EMPLEADO_IDIOMA(string tipo_transaccion, SPE_OBTIENE_C_EMPLEADO_IDIOMA_Result V_C_EMPLEADO_IDIOMA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_EMPLEADO_IDIOMA(int? ID_EMPLEADO_IDIOMA = null, string usuario = null, string programa = null);

//        #endregion


//        #region C_ESCOLARIDAD

//        [OperationContract]
//        List<SPE_OBTIENE_C_ESCOLARIDAD_Result> Get_C_ESCOLARIDAD(int? ID_ESCOLARIDAD = null, String NB_ESCOLARIDAD = null, String DS_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, int? ID_NIVEL_ESCOLARIDAD = null);

//        [OperationContract]
//        bool Insert_update_C_ESCOLARIDAD(string tipo_transaccion, SPE_OBTIENE_C_ESCOLARIDAD_Result V_C_ESCOLARIDAD, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_ESCOLARIDAD(SPE_OBTIENE_C_ESCOLARIDAD_Result V_C_AREA_INTERES=null, string usuario = null, string programa = null);

//        #endregion

//        #region C_ESTADO

//        [OperationContract]
//        List<SPE_OBTIENE_C_ESTADO_Result> Get_C_ESTADO(int? ID_ESTADO = null, String CL_PAIS = null, String CL_ESTADO = null, String NB_ESTADO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_ESTADO(string tipo_transaccion, SPE_OBTIENE_C_ESTADO_Result V_C_ESTADO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_ESTADO(int? ID_ESTADO = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_EVALUADOR_EXTERNO

//        [OperationContract]
//        List<SPE_OBTIENE_C_EVALUADOR_EXTERNO_Result> Get_C_EVALUADOR_EXTERNO(int? ID_EVALUADOR_EXTERNO = null, String CL_EVALUADOR_EXTERNO = null, String NB_EVALUADOR_EXTERNO = null, String DS_EVALUARDO_EXTERNO = null, bool? FG_ACTIVO = null, String XML_CAMPOS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_EVALUADOR_EXTERNO(string tipo_transaccion, SPE_OBTIENE_C_EVALUADOR_EXTERNO_Result V_C_EVALUADOR_EXTERNO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_EVALUADOR_EXTERNO(int? ID_EVALUADOR_EXTERNO = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_IDIOMA

//        [OperationContract]
//        List<SPE_OBTIENE_C_IDIOMA_Result> Get_C_IDIOMA(int? ID_IDIOMA = null, String CL_IDIOMA = null, String NB_IDIOMA = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_IDIOMA(string tipo_transaccion, SPE_OBTIENE_C_IDIOMA_Result V_C_IDIOMA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_IDIOMA(int? ID_IDIOMA = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_MUNICIPIO

//        [OperationContract]
//        List<SPE_OBTIENE_C_MUNICIPIO_Result> Get_C_MUNICIPIO(int? ID_MUNICIPIO = null, String CL_PAIS = null, String CL_ESTADO = null, String CL_MUNICIPIO = null, String NB_MUNICIPIO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_MUNICIPIO(string tipo_transaccion, SPE_OBTIENE_C_MUNICIPIO_Result V_C_MUNICIPIO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_MUNICIPIO(int? ID_MUNICIPIO = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_NIVEL_ESCOLARIDAD

//        [OperationContract]
//        List<SPE_OBTIENE_C_NIVEL_ESCOLARIDAD_Result> Get_C_NIVEL_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, String DS_NIVEL_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_NIVEL_ESCOLARIDAD(string tipo_transaccion, SPE_OBTIENE_C_NIVEL_ESCOLARIDAD_Result V_C_NIVEL_ESCOLARIDAD, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_NIVEL_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_PERIODO

//        [OperationContract]
//        List<SPE_OBTIENE_C_PERIODO_Result> Get_C_PERIODO(int? ID_PERIODO = null, String CL_PERIODO = null, String NB_PERIODO = null, String DS_PERIODO = null, bool? FE_INICIO = null, String FE_TERMINO = null, String CL_ESTADO_PERIODO = null, String XML_CAMPOS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_PERIODO(string tipo_transaccion, SPE_OBTIENE_C_PERIODO_Result V_C_PERIODO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_PERIODO(int? ID_PERIODO = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_ROL

//        [OperationContract]
//        List<SPE_OBTIENE_C_ROL_Result> Get_C_ROL(int? ID_ROL = null, String CL_ROL = null, String NB_ROL = null, String XML_AUTORIZACION = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_ROL(string tipo_transaccion, SPE_OBTIENE_C_ROL_Result V_C_ROL, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_ROL(int? ID_ROL = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_ROL_FUNCION

//        [OperationContract]
//        List<SPE_OBTIENE_C_ROL_FUNCION_Result> Get_C_ROL_FUNCION(int?  ID_ROL = null,int?  ID_FUNCION = null);

//        [OperationContract]
//        bool Insert_update_C_ROL_FUNCION (string tipo_transaccion, SPE_OBTIENE_C_ROL_FUNCION_Result V_C_ROL_FUNCION,string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_ROL_FUNCION (SPE_OBTIENE_C_ROL_FUNCION_Result V_C_ROL_FUNCION,string usuario = null, string programa = null);

//        #endregion

//        #region C_SECUENCIA

//        [OperationContract]
//        List<SPE_OBTIENE_FOLIO_SECUENCIA_Result> ObtieneFolioSecuencia(string CL_SECUENCIA = null);

//        [OperationContract]
//        List<SPE_OBTIENE_C_SECUENCIA_Result> Get_C_SECUENCIA(String CL_SECUENCIA = null, String CL_PREFIJO = null, int? NO_ULTIMO_VALOR = null, int? NO_VALOR_MAXIMO = null, String CL_SUFIJO = null, byte? NO_DIGITOS = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_SECUENCIA(string tipo_transaccion, SPE_OBTIENE_C_SECUENCIA_Result V_C_SECUENCIA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_SECUENCIA(String CL_SECUENCIA = null, string usuario = null, string programa = null);

//        #endregion

//        #region M_EMPLEADO

//        [OperationContract]
//       //List<SPE_OBTIENE_M_EMPLEADO_Result> Get_M_EMPLEADO(int?  ID_EMPLEADO = null,String CL_EMPLEADO = null,String NB_EMPLEADO = null,String NB_APELLIDO_PATERNO = null,String NB_APELLIDO_MATERNO = null,String CL_ESTADO_EMPLEADO = null,String CL_GENERO = null,String CL_ESTADO_CIVIL = null,String NB_CONYUGUE = null,String CL_RFC = null,String CL_CURP = null,String CL_NSS = null,String CL_TIPO_SANGUINEO = null,String CL_NACIONALIDAD = null,String NB_PAIS = null,String NB_ESTADO = null,String NB_MUNICIPIO = null,String NB_COLONIA = null,String NB_CALLE = null,String NO_INTERIOR = null,String NO_EXTERIOR = null,String CL_CODIGO_POSTAL = null,String XML_TELEFONOS = null,String CL_CORREO_ELECTRONICO = null,bool?  FG_ACTIVO = null,DateTime?  FE_NACIMIENTO = null,String DS_LUGAR_NACIMIENTO = null,DateTime?  FE_ALTA = null,DateTime?  FE_BAJA = null,int?  ID_PUESTO = null,Decimal?  MN_SUELDO = null,Decimal?  MN_SUELDO_VARIABLE = null,String DS_SUELDO_COMPOSICION = null,int?  ID_CANDIDATO = null,int?  ID_EMPRESA = null,String XML_CAMPOS_ADICIONALES = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null);

//        List<SPE_OBTIENE_M_EMPLEADO_Result> Get_M_EMPLEADO(int? ID_EMPLEADO = null, String CL_EMPLEADO = null, String NB_EMPLEADO = null, String NB_APELLIDO_PATERNO = null, String NB_APELLIDO_MATERNO = null, String CL_ESTADO_EMPLEADO = null, String CL_GENERO = null, String CL_ESTADO_CIVIL = null, String NB_CONYUGUE = null, String CL_RFC = null, String CL_CURP = null, String CL_NSS = null, String CL_TIPO_SANGUINEO = null, String CL_NACIONALIDAD = null, String NB_PAIS = null, String NB_ESTADO = null, String NB_MUNICIPIO = null, String NB_COLONIA = null, String NB_CALLE = null, String NO_INTERIOR = null, String NO_EXTERIOR = null, String CL_CODIGO_POSTAL = null, String CL_CORREO_ELECTRONICO = null, bool? FG_ACTIVO = null, System.DateTime? FE_NACIMIENTO = null, String DS_LUGAR_NACIMIENTO = null, System.DateTime? FE_ALTA = null, System.DateTime? FE_BAJA = null, int? ID_PUESTO = null, Decimal? MN_SUELDO = null, Decimal? MN_SUELDO_VARIABLE = null, String DS_SUELDO_COMPOSICION = null, int? ID_CANDIDATO = null, int? ID_EMPRESA = null,
//               bool? MP_FG_ACTIVO = null, System.DateTime? FE_INACTIVO = null, String CL_PUESTO = null, String NB_PUESTO = null, int? ID_PUESTO_JEFE = null, int? ID_DEPARTAMENTO = null, int? ID_BITACORA = null,
//               String NB_CANDIDATO = null, String CC_NB_APELLIDO_PATERNO = null, String CC_NB_APELLIDO_MATERNO = null, String CC_CL_GENERO = null, String CC_CL_RFC = null, String CC_CL_CURP = null, String CC_CL_ESTADO_CIVIL = null, String CC_NB_CONYUGUE = null, String CC_CL_NSS = null, String CC_CL_TIPO_SANGUINEO = null, String CC_NB_PAIS = null, String CC_NB_ESTADO = null, String CC_NB_MUNICIPIO = null, String CC_NB_COLONIA = null, String CC_NB_CALLE = null, String CC_NO_INTERIOR = null, String CC_NO_EXTERIOR = null, String CC_CL_CODIGO_POSTAL = null, String CC_CL_CORREO_ELECTRONICO = null, System.DateTime? CC_FE_NACIMIENTO = null, String CC_DS_LUGAR_NACIMIENTO = null, Decimal? CC_MN_SUELDO = null, String CC_CL_NACIONALIDAD = null, String DS_NACIONALIDAD = null, String NB_LICENCIA = null, String DS_VEHICULO = null, String CL_CARTILLA_MILITAR = null, String CL_CEDULA_PROFESIONAL = null, String DS_DISPONIBILIDAD = null, String CL_DISPONIBILIDAD_VIAJE = null, String DS_COMENTARIO = null, bool? CC_FG_ACTIVO = null,
//               String CL_EMPRESA = null, String NB_EMPRESA = null, String NB_RAZON_SOCIAL = null, bool? MD_FG_ACTIVO = null, System.DateTime? MD_FE_INACTIVO = null, String CL_DEPARTAMENTO = null, String NB_DEPARTAMENTO = null, XElement xml = null)
//           ;
        
//        [OperationContract]
//        bool Insert_update_M_EMPLEADO(string tipo_transaccion, SPE_OBTIENE_M_EMPLEADO_Result V_LISTA_DINAMICA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_M_EMPLEADO(int? ID_EMPLEADO=null,string CL_EMPLEADO=null, string usuario = null, string programa = null);

//    #endregion
                
//        #region M_PUESTO


        
//          [OperationContract]
//        List<SPE_OBTIENE_M_PUESTO_Result> Get_M_PUESTO(int? ID_PUESTO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, String CL_PUESTO = null, String NB_PUESTO = null, int? ID_PUESTO_JEFE = null, int? ID_DEPARTAMENTO = null, String XML_CAMPOS_ADICIONALES = null, int? ID_BITACORA = null, byte? NO_EDAD_MINIMA = null, byte? NO_EDAD_MAXIMA = null, String CL_GENERO = null, String CL_ESTADO_CIVIL = null, String XML_REQUERIMIENTOS = null, String XML_OBSERVACIONES = null, String XML_RESPONSABILIDAD = null, String XML_AUTORIDAD = null, String XML_CURSOS_ADICIONALES = null, String XML_MENTOR = null, String CL_TIPO_PUESTO = null, int? ID_CENTRO_ADMINISTRATIVO = null, int? ID_CENTRO_OPERATIVO = null, int? ID_PAQUETE_PRESTACIONES = null,String NB_DEPARTAMENTO=null,String CL_DEPARTAMENTO=null);
         
//       /* [OperationContract]
//        List<SPE_OBTIENE_M_PUESTO_Result> Get_M_PUESTO(int? ID_PUESTO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, String CL_PUESTO = null, String NB_PUESTO = null, int? ID_PUESTO_JEFE = null, int? ID_DEPARTAMENTO = null, String XML_CAMPOS_ADICIONALES = null, int? ID_BITACORA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);
//        */
//        [OperationContract]
//        bool Insert_update_M_PUESTO(string tipo_transaccion, SPE_OBTIENE_M_PUESTO_Result V_M_PUESTO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_M_PUESTO(int? ID_PUESTO= null, string CL_PUESTO=null, string usuario = null, string programa = null);

//        #endregion

//        #region S_CONFIGURACION

//        [OperationContract]
//        List<SPE_OBTIENE_S_CONFIGURACION_Result> Get_S_CONFIGURACION(String XML_CONFIGURACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_MODIFICA = null,String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_S_CONFIGURACION (string tipo_transaccion, SPE_OBTIENE_S_CONFIGURACION_Result V_S_CONFIGURACION,string usuario, string programa);

//        [OperationContract]
//        bool Delete_S_CONFIGURACION (string usuario = null, string programa = null);

//        #endregion

//        #region S_TIPO_DATO

//        [OperationContract]
//        List<SPE_OBTIENE_S_TIPO_DATO_Result> Get_S_TIPO_DATO(String CL_TIPO_DATO = null,String NB_TIPO_DATO = null);

//        [OperationContract]
//        bool Insert_update_S_TIPO_DATO (string tipo_transaccion, SPE_OBTIENE_S_TIPO_DATO_Result V_S_TIPO_DATO,string usuario, string programa);

//        [OperationContract]
//        bool Delete_S_TIPO_DATO(SPE_OBTIENE_S_TIPO_DATO_Result V_S_TIPO_DATO, string usuario = null, string programa = null);

//        #endregion

//        #region S_FUNCION

//        [OperationContract]
//        List<SPE_OBTIENE_S_FUNCION_Result> Get_S_FUNCION(int? ID_FUNCION = null, String CL_FUNCION = null, String CL_TIPO_FUNCION = null, String NB_FUNCION = null, int? ID_FUNCION_PADRE = null, String NB_URL = null, String XML_CONFIGURACION = null);

//        [OperationContract]
//        bool Insert_update_S_FUNCION(string tipo_transaccion, SPE_OBTIENE_S_FUNCION_Result V_S_FUNCION, string usuario, string programa);

//        [OperationContract]
//        bool Delete_S_FUNCION(int? ID_FUNCION = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_USUARIO

//        [OperationContract]
//        List<SPE_OBTIENE_C_USUARIO_Result> Get_C_USUARIO(String CL_USUARIO = null, String NB_USUARIO = null, String NB_CORREO_ELECTRONICO = null, String NB_PASSWORD = null, bool? FG_CAMBIAR_PASSWORD = null, String XML_PERSONALIZACION = null, int? ID_ROL = null, int? ID_EMPLEADO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_USUARIO(string tipo_transaccion, SPE_OBTIENE_C_USUARIO_Result V_C_USUARIO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_USUARIO(String CL_USUARIO = null, string usuario = null, string programa = null);

//        #endregion

//    //    #region C_CANDIDATO

//    //    [OperationContract]
//    //    List<SPE_OBTIENE_C_CANDIDATO_Result> Get_C_CANDIDATO(int?  ID_CANDIDATO = null,String NB_CANDIDATO = null,String NB_APELLIDO_PATERNO = null,String NB_APELLIDO_MATERNO = null,String CL_GENERO = null,String CL_RFC = null,String CL_CURP = null,String CL_ESTADO_CIVIL = null,String NB_CONYUGUE = null,String CL_NSS = null,String CL_TIPO_SANGUINEO = null,String NB_PAIS = null,String NB_ESTADO = null,String NB_MUNICIPIO = null,String NB_COLONIA = null,String NB_CALLE = null,String NO_INTERIOR = null,String NO_EXTERIOR = null,String CL_CODIGO_POSTAL = null,String CL_CORREO_ELECTRONICO = null,DateTime?  FE_NACIMIENTO = null,String DS_LUGAR_NACIMIENTO = null,Decimal?  MN_SUELDO = null,String CL_NACIONALIDAD = null,String DS_NACIONALIDAD = null,String NB_LICENCIA = null,String DS_VEHICULO = null,String CL_CARTILLA_MILITAR = null,String CL_CEDULA_PROFESIONAL = null,String XML_TELEFONOS = null,String XML_INGRESOS = null,String XML_EGRESOS = null,String XML_PATRIMONIO = null,String DS_DISPONIBILIDAD = null,String CL_DISPONIBILIDAD_VIAJE = null,String XML_PERFIL_RED_SOCIAL = null,String DS_COMENTARIO = null,bool?  FG_ACTIVO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null);

//    //    [OperationContract]
//    //    bool Insert_update_C_CANDIDATO (string tipo_transaccion, SPE_OBTIENE_C_CANDIDATO_Result V_C_CANDIDATO,string usuario, string programa);

//    //    [OperationContract]
//    //    bool Delete_C_CANDIDATO(SPE_OBTIENE_C_CANDIDATO_Result V_C_CANDIDATO, string usuario = null, string programa = null);

//    //#endregion

//        #region C_PUESTO_COMPETENCIA

//        [OperationContract]
//        List<SPE_OBTIENE_C_PUESTO_COMPETENCIA_Result> Get_C_PUESTO_COMPETENCIA(int? ID_PUESTO_COMPETENCIA = null, int? ID_PUESTO = null, int? ID_COMPETENCIA = null, Decimal? ID_NIVEL_DESEADO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_PUESTO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_C_PUESTO_COMPETENCIA_Result V_C_PUESTO_COMPETENCIA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_PUESTO_COMPETENCIA(int? ID_PUESTO_COMPETENCIA = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_PREGUNTA

//        [OperationContract]
//        List<SPE_OBTIENE_C_PREGUNTA_Result> Get_C_PREGUNTA(int? ID_PREGUNTA = null, String CL_PREGUNTA = null, String NB_PREGUNTA = null, String DS_PREGUNTA = null, String CL_TIPO_PREGUNTA = null, Decimal? NO_VALOR = null, bool? FG_REQUERIDO = null, bool? FG_ACTIVO = null, int? ID_COMPETENCIA = null, int? ID_BITACORA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_PREGUNTA(string tipo_transaccion, SPE_OBTIENE_C_PREGUNTA_Result V_C_PREGUNTA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_PREGUNTA(int? ID_PREGUNTA = null, string usuario = null, string programa = null);

//        #endregion

//        #region S_TIPO_RELACION_PUESTO

//        [OperationContract]
//        List<SPE_OBTIENE_S_TIPO_RELACION_PUESTO_Result> Get_S_TIPO_RELACION_PUESTO(String CL_TIPO_RELACION = null, String NB_TIPO_RELACION = null, String DS_TIPO_RELACION = null);

//        [OperationContract]
//        bool Insert_update_S_TIPO_RELACION_PUESTO(string tipo_transaccion, SPE_OBTIENE_S_TIPO_RELACION_PUESTO_Result V_S_TIPO_RELACION_PUESTO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_S_TIPO_RELACION_PUESTO(String CL_TIPO_RELACION = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_RESPUESTA

//        [OperationContract]
//        List<SPE_OBTIENE_C_RESPUESTA_Result> Get_C_RESPUESTA(int? ID_RESPUESTA = null, String CL_RESPUESTA = null, String NB_RESPUESTA = null, String DS_RESPUESTA = null, Decimal? NO_VALOR = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_RESPUESTA(string tipo_transaccion, SPE_OBTIENE_C_RESPUESTA_Result V_C_RESPUESTA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_RESPUESTA(int? ID_RESPUESTA = null, string usuario = null, string programa = null);

//        #endregion

//        #region K_EVALUADO_PERIODO

//        [OperationContract]
//        List<SPE_OBTIENE_K_EVALUADO_PERIODO_Result> Get_K_EVALUADO_PERIODO(int? ID_EVALAUDOR_PERIODO = null, int? ID_PERIODO = null, int? ID_EMPLEADO = null, int? ID_PUESTO = null, int? FG_PUESTO_ACTUAL = null, int? NO_CONSUMO_SUP = null, Decimal? MN_CUOTA_BASE = null, Decimal? MN_CUOTA_CONSUMO = null, Decimal? MN_CUOTA_ADICIONAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_K_EVALUADO_PERIODO(string tipo_transaccion, SPE_OBTIENE_K_EVALUADO_PERIODO_Result V_K_EVALUADO_PERIODO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_K_EVALUADO_PERIODO(int? ID_EVALAUDOR_PERIODO = null, string usuario = null, string programa = null);

//        #endregion

//        #region K_EVALUADO_EVALUADOR

//        [OperationContract]
//        List<SPE_OBTIENE_K_EVALUADO_EVALUADOR_Result> Get_K_EVALUADO_EVALUADOR(int? ID_EVALAUDO_EVALUADOR = null, int? ID_PERIODO = null, int? ID_EVALUADOR_PERIODO = null, int? ID_PUESTO_EVALUADOR = null, Decimal? NO_VALOR = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_K_EVALUADO_EVALUADOR(string tipo_transaccion, SPE_OBTIENE_K_EVALUADO_EVALUADOR_Result V_K_EVALUADO_EVALUADOR, string usuario, string programa);

//        [OperationContract]
//        bool Delete_K_EVALUADO_EVALUADOR(int? ID_EVALAUDO_EVALUADOR = null, string usuario = null, string programa = null);

//        #endregion

//        #region K_CUESTIONARIO

//        [OperationContract]
//        List<SPE_OBTIENE_K_CUESTIONARIO_Result> Get_K_CUESTIONARIO(int? ID_CUESTIONARIO = null, int? ID_EVALUADO = null, int? ID_EVALUADO_EVALUADOR = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_K_CUESTIONARIO(string tipo_transaccion, SPE_OBTIENE_K_CUESTIONARIO_Result V_K_CUESTIONARIO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_K_CUESTIONARIO(int? ID_CUESTIONARIO = null, string usuario = null, string programa = null);

//        #endregion

//        #region K_CUESTIONARIO_PREGUNTA

//        [OperationContract]
//        List<SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_Result> Get_K_CUESTIONARIO_PREGUNTA(int? ID_CUESTIONARIO_PREGUNTA = null, int? ID_CUESTIONARIO = null, int? ID_PREGUNTA = null, String NB_PREGUNTA = null, String NB_RESPUESTA = null, Decimal? NO_VALOR_RESPUESTA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_K_CUESTIONARIO_PREGUNTA(string tipo_transaccion, SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_Result V_K_CUESTIONARIO_PREGUNTA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_K_CUESTIONARIO_PREGUNTA(int? ID_CUESTIONARIO_PREGUNTA = null, string usuario = null, string programa = null);

//        #endregion

//        #region K_EMPLEADO_COMPETENCIA

//        [OperationContract]
//        List<SPE_OBTIENE_K_EMPLEADO_COMPETENCIA_Result> Get_K_EMPLEADO_COMPETENCIA(int? ID_EMPLEADO_COMPETENCIA = null, int? ID_EMPLEADO = null, int? ID_COMPETENCIA = null, Decimal? NO_CALIFICACION = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_K_EMPLEADO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_K_EMPLEADO_COMPETENCIA_Result V_K_EMPLEADO_COMPETENCIA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_K_EMPLEADO_COMPETENCIA(int? ID_EMPLEADO_COMPETENCIA = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_PUESTO_FUNCION

//        [OperationContract]
//        List<SPE_OBTIENE_C_PUESTO_FUNCION_Result> Get_C_PUESTO_FUNCION(int? ID_PUESTO_FUNCION = null, String CL_PUESTO_FUNCION = null, String NB_PUESTO_FUNCION = null, String DS_PUESTO_FUNCION = null, int? ID_PUESTO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_PUESTO_FUNCION(string tipo_transaccion, SPE_OBTIENE_C_PUESTO_FUNCION_Result V_C_PUESTO_FUNCION, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_PUESTO_FUNCION(int? ID_PUESTO_FUNCION = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_PUESTO_RELACIONADO

//        [OperationContract]
//        List<SPE_OBTIENE_C_PUESTO_RELACIONADO_Result> Get_C_PUESTO_RELACIONADO(int?  ID_PUESTO = null,int?  ID_PUESTO_RELACIONADO = null,String CL_TIPO_RELACION = null,String DS_PUESTO_RELACIONADO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_PUESTO_RELACIONADO (string tipo_transaccion, SPE_OBTIENE_C_PUESTO_RELACIONADO_Result V_C_PUESTO_RELACIONADO,string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_PUESTO_RELACIONADO (SPE_OBTIENE_C_PUESTO_RELACIONADO_Result V_C_PUESTO_RELACIONADO,string usuario = null, string programa = null);

//        #endregion

//        #region C_EMPLEADO_RELACIONADO

//        [OperationContract]
//        List<SPE_OBTIENE_C_EMPLEADO_RELACIONADO_Result> Get_C_EMPLEADO_RELACIONADO(int?  ID_EMPLEADO = null,int?  ID_EMPLEADO_RELACIONADO = null,String CL_TIPO_RELACION = null,String DS_EMPLEADO_RELACIONADO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_EMPLEADO_RELACIONADO (string tipo_transaccion, SPE_OBTIENE_C_EMPLEADO_RELACIONADO_Result V_C_EMPLEADO_RELACIONADO,string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_EMPLEADO_RELACIONADO (SPE_OBTIENE_C_EMPLEADO_RELACIONADO_Result V_C_EMPLEADO_RELACIONADO,string usuario = null, string programa = null);

//        #endregion

//        #region C_GRUPO_PREGUNTA

//        [OperationContract]
//        List<SPE_OBTIENE_C_GRUPO_PREGUNTA_Result> Get_C_GRUPO_PREGUNTA(int?  ID_GRUPO_PREGUNTA = null,String CL_GRUPO_PREGUNTA = null,String NB_GRUPO_PREGUNTA = null,int?  ID_PREGUNTA = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_GRUPO_PREGUNTA (string tipo_transaccion, SPE_OBTIENE_C_GRUPO_PREGUNTA_Result V_C_GRUPO_PREGUNTA,string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_GRUPO_PREGUNTA (SPE_OBTIENE_C_GRUPO_PREGUNTA_Result V_C_GRUPO_PREGUNTA,string usuario = null, string programa = null);

//#endregion

//        #region C_AREA_INTERES

//        [OperationContract]
//        List<SPE_OBTIENE_C_AREA_INTERES_Result> Get_C_AREA_INTERES(int?  ID_AREA_INTERES = null,String CL_AREA_INTERES = null,String NB_AREA_INTERES = null,bool?  FG_ACTIVO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_AREA_INTERES (string tipo_transaccion, SPE_OBTIENE_C_AREA_INTERES_Result V_C_AREA_INTERES,string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_AREA_INTERES(int? ID_AREA_INTERES=null,string CL_AREA_INTERES=null, string usuario = null, string programa = null);

//#endregion

//        #region K_EXPERIENCIA_LABORAL

//        [OperationContract]
//        List<SPE_OBTIENE_K_EXPERIENCIA_LABORAL_Result> Get_K_EXPERIENCIA_LABORAL(int? ID_EXPERIENCIA_LABORAL = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, String NB_EMPRESA = null, String DS_DOMICILIO = null, String NB_GIRO = null, DateTime? FE_INICIO = null, DateTime? FE_FIN = null, String NB_PUESTO = null, String NB_FUNCION = null, String DS_FUNCIONES = null, Decimal? MN_PRIMER_SUELDO = null, Decimal? MN_ULTIMO_SUELDO = null, String CL_TIPO_CONTRATO = null, String CL_TIPO_CONTRATO_OTRO = null, String NO_TELEFONO_CONTACTO = null, String CL_CORREO_ELECTRONICO = null, String NB_CONTACTO = null, String NB_PUESTO_CONTACTO = null, bool? CL_INFORMACION_CONFIRMADA = null, String DS_COMENTARIOS = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_K_EXPERIENCIA_LABORAL(E_EXPERIENCIA_LABORAL ULTIMO, E_EXPERIENCIA_LABORAL ANT1, E_EXPERIENCIA_LABORAL ANT2, E_EXPERIENCIA_LABORAL ANT3, string usuario, string programa, string archivo);
        
//        [OperationContract]
//        bool Delete_K_EXPERIENCIA_LABORAL (int?  ID_EXPERIENCIA_LABORAL = null,string usuario = null, string programa = null);

//#endregion

//        #region K_AREA_INTERES

//        [OperationContract]
//        List<SPE_OBTIENE_K_AREA_INTERES_Result> Get_K_AREA_INTERES(int? ID_CANDIDATO_AREA_INTERES = null, int? ID_CANDIDATO = null, int? ID_AREA_INTERES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_K_AREA_INTERES(string tipo_transaccion, SPE_OBTIENE_K_AREA_INTERES_Result V_K_AREA_INTERES, string usuario, string programa);

//        [OperationContract]
//        bool Delete_K_AREA_INTERES(int? ID_CANDIDATO_AREA_INTERES = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_CLASIFICACION_COMPETENCIA

//        [OperationContract]
//        List<SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result> Get_C_CLASIFICACION_COMPETENCIA(int?  ID_CLASIFICACION_COMPETENCIA = null,String CL_CLASIFICACION = null,String CL_TIPO_COMPETENCIA = null,String NB_CLASIFICACION_COMPETENCIA = null,String DS_CLASIFICACION_COMPETENCIA = null,String DS_NOTAS_CLASIFICACION = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null, bool? FG_ACTIVO = null);

//        [OperationContract]
//        bool Insert_update_C_CLASIFICACION_COMPETENCIA (string tipo_transaccion, SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result V_C_CLASIFICACION_COMPETENCIA,string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_CLASIFICACION_COMPETENCIA (int?  ID_CLASIFICACION_COMPETENCIA = null,string usuario = null, string programa = null);

//        #endregion

//        #region S_CATALOGO_TIPO

//        [OperationContract]
//        List<SPE_OBTIENE_S_CATALOGO_TIPO_Result> Get_S_CATALOGO_TIPO(int? ID_CATALOGO_TIPO = null, String NB_CATALOGO_TIPO = null, String DS_CATALOGO_TIPO = null);

//        [OperationContract]
//        bool Insert_update_S_CATALOGO_TIPO(string tipo_transaccion, SPE_OBTIENE_S_CATALOGO_TIPO_Result V_S_CATALOGO_TIPO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_S_CATALOGO_TIPO(int? ID_CATALOGO_TIPO = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_CATALOGO_LISTA

//        [OperationContract]
//        List<SPE_OBTIENE_C_CATALOGO_LISTA_Result> Get_C_CATALOGO_LISTA(int? ID_CATALOGO_LISTA = null, String NB_CATALOGO_LISTA = null, String DS_CATALOGO_LISTA = null, int? ID_CATALOGO_TIPO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_CATALOGO_LISTA(string tipo_transaccion, SPE_OBTIENE_C_CATALOGO_LISTA_Result V_C_CATALOGO_LISTA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_CATALOGO_LISTA(int? ID_CATALOGO_LISTA = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_CATALOGO_VALOR

//        [OperationContract]
//        List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> Get_C_CATALOGO_VALOR(int? ID_CATALOGO_VALOR = null, String CL_CATALOGO_VALOR = null, String NB_CATALOGO_VALOR = null, String DS_CATALOGO_VALOR = null, int? ID_CATALOGO_LISTA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_CATALOGO_VALOR(string tipo_transaccion, SPE_OBTIENE_C_CATALOGO_VALOR_Result V_C_CATALOGO_VALOR, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_CATALOGO_VALOR(int? ID_CATALOGO_VALOR = null, string usuario = null, string programa = null);

//        #endregion

//        #region K_REQUISICION

//        [OperationContract]
//        List<SPE_OBTIENE_K_REQUISICION_Result> Get_K_REQUISICION(int?  ID_REQUISICION = null,String NO_REQUISICION = null,DateTime?  FE_SOLICITUD = null,int?  ID_PUESTO = null,String CL_ESTADO = null,String CL_CAUSA = null,String DS_CAUSA = null,int?  ID_NOTIFICACION = null,int?  ID_SOLICITANTE = null,int?  ID_AUTORIZA = null,int?  ID_VISTO_BUENO = null,int?  ID_EMPRESA = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_K_REQUISICION (string tipo_transaccion, SPE_OBTIENE_K_REQUISICION_Result V_K_REQUISICION,string usuario, string programa);

//        [OperationContract]
//        bool Delete_K_REQUISICION (int?  ID_REQUISICION = null,string usuario = null, string programa = null);

//    #endregion

//        #region C_EMPRESA

//        [OperationContract]
//        List<SPE_OBTIENE_C_EMPRESA_Result> Get_C_EMPRESA(int? ID_EMPRESA = null, String CL_EMPRESA = null, String NB_EMPRESA = null, String NB_RAZON_SOCIAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_EMPRESA(string tipo_transaccion, SPE_OBTIENE_C_EMPRESA_Result V_C_EMPRESA, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_EMPRESA(int? ID_EMPRESA = null, string usuario = null, string programa = null);

//        #endregion

//        #region C_PARIENTE

//        [OperationContract]
//        List<SPE_OBTIENE_C_PARIENTE_Result> Get_C_PARIENTE(int? ID_PARIENTE = null, String NB_PARIENTE = null, String CL_PARENTEZCO = null, String CL_GENERO = null, DateTime? FE_NACIMIENTO = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_BITACORA = null, String CL_OCUPACION = null, bool? FG_DEPENDIENTE = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_PARIENTE(string tipo_transaccion, SPE_OBTIENE_C_PARIENTE_Result V_C_PARIENTE, string usuario, string programa, string fecha);

//        [OperationContract]
//        bool Delete_C_PARIENTE(int? ID_PARIENTE = null, string usuario = null, string programa = null);

//        #endregion

//        #region K_SOLICITUD

//        [OperationContract]
//        List<SPE_OBTIENE_K_SOLICITUD_Result> Get_K_SOLICITUD(int? ID_SOLICITUD = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, int? ID_DESCRIPTIVO = null, int? ID_REQUISICION = null, String CL_SOLICITUD = null, String CL_ACCESO_EVALUACION = null, int? ID_PLANTILLA_SOLICITUD = null, String DS_COMPETENCIAS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_K_SOLICITUD(string tipo_transaccion, SPE_OBTIENE_K_SOLICITUD_Result V_K_SOLICITUD, string usuario, string programa);

//        [OperationContract]
//        bool Delete_K_SOLICITUD(int? ID_SOLICITUD = null, string usuario = null, string programa = null);

//        #endregion

//        #region VW_PARENTEZCO

//        [OperationContract]
//        List<SPE_OBTIENE_VW_PARENTEZCO_Result> Get_VW_PARENTEZCO(String NB_PARENTEZCO = null);

//        [OperationContract]
//        bool Insert_update_VW_PARENTEZCO (string tipo_transaccion, SPE_OBTIENE_VW_PARENTEZCO_Result V_VW_PARENTEZCO,string usuario, string programa);

//        [OperationContract]
//        bool Delete_VW_PARENTEZCO(SPE_OBTIENE_VW_PARENTEZCO_Result V_VW_PARENTEZCO, string usuario = null, string programa = null);

//        #endregion

//        #region VW_OBTIENE_MESES

//        [OperationContract]
//        List<SPE_OBTIENE_VW_OBTIENE_MESES_Result> Get_VW_OBTIENE_MESES(int?  NUMERO = null,String MES = null);

//        #endregion

//        #region
//        [OperationContract]
//        List<SP_OBTIENE_ANIOS_Result> Get_OBTIENE_ANIO();
//        #endregion

//        #region C_CANDIDATO

//        [OperationContract]
//        List<SPE_OBTIENE_C_CANDIDATO_Result> Get_C_CANDIDATO(int?  ID_CANDIDATO = null,String NB_CANDIDATO = null,String NB_APELLIDO_PATERNO = null,String NB_APELLIDO_MATERNO = null,String CL_GENERO = null,String CL_RFC = null,String CL_CURP = null,String CL_ESTADO_CIVIL = null,String NB_CONYUGUE = null,String CL_NSS = null,String CL_TIPO_SANGUINEO = null,String NB_PAIS = null,String NB_ESTADO = null,String NB_MUNICIPIO = null,String NB_COLONIA = null,String NB_CALLE = null,String NO_INTERIOR = null,String NO_EXTERIOR = null,String CL_CODIGO_POSTAL = null,String CL_CORREO_ELECTRONICO = null,DateTime?  FE_NACIMIENTO = null,String DS_LUGAR_NACIMIENTO = null,Decimal?  MN_SUELDO = null,String CL_NACIONALIDAD = null,String DS_NACIONALIDAD = null,String NB_LICENCIA = null,String DS_VEHICULO = null,String CL_CARTILLA_MILITAR = null,String CL_CEDULA_PROFESIONAL = null,String XML_TELEFONOS = null,String XML_INGRESOS = null,String XML_EGRESOS = null,String XML_PATRIMONIO = null,String DS_DISPONIBILIDAD = null,String CL_DISPONIBILIDAD_VIAJE = null,String XML_PERFIL_RED_SOCIAL = null,String DS_COMENTARIO = null,bool?  FG_ACTIVO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        int Insert_update_C_CANDIDATO(string tipo_transaccion, SPE_OBTIENE_C_CANDIDATO_Result V_C_CANDIDATO, string usuario, string programa, string fechaNacimiento);

//        [OperationContract]
//        bool Delete_C_CANDIDATO(SPE_OBTIENE_C_CANDIDATO_Result V_C_CANDIDATO, string usuario = null, string programa = null);

//        #endregion

//        #region VW_TIPO_LICENCIA

//        [OperationContract]
//        List<SPE_OBTIENE_VW_TIPO_LICENCIA_Result> Get_VW_TIPO_LICENCIA(String CL_LICENCIA = null,String NB_LICENCIA = null);

//        #endregion

//        #region VW_PAIS

//        [OperationContract]
//        List<SPE_OBTIENE_VW_PAIS_Result> Get_VW_PAIS(String CL_PAIS = null, String NB_PAIS = null);

//        #endregion

//        #region Colonia CP
//        [OperationContract]
//        List<SPE_OBTIENE_DATOS_CP_Result> Get_Colonia_CP(String CL_CODIGO_POSTAL = null);
//        #endregion

//        #region VW_OBTIENE_PORCENTAJES_IDIOMAS

//        [OperationContract]
//        List<SPE_OBTIENE_VW_OBTIENE_PORCENTAJES_IDIOMAS_Result> Get_VW_OBTIENE_PORCENTAJES_IDIOMAS(String NB_PORCENTAJE = null,decimal?  CL_PORCENTAJE = null);

//        #endregion

//        #region C_PLANTILLA_FORMULARIO

//        [OperationContract]
//        List<SPE_OBTIENE_C_PLANTILLA_FORMULARIO_Result> Get_C_PLANTILLA_FORMULARIO(int? ID_PLANTILLA_SOLICITUD = null, String NB_PLANTILLA_SOLICITUD = null, String DS_PLANTILLA_SOLICITUD = null, String CL_FORMULARIO = null, bool? FG_GENERAL = null, String XML_PLANTILLA_SOLICITUD = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null);

//        [OperationContract]
//        bool Insert_update_C_PLANTILLA_FORMULARIO(string tipo_transaccion, SPE_OBTIENE_C_PLANTILLA_FORMULARIO_Result V_C_PLANTILLA_FORMULARIO, string usuario, string programa);

//        [OperationContract]
//        bool Delete_C_PLANTILLA_FORMULARIO(int? ID_PLANTILLA_SOLICITUD = null, string usuario = null, string programa = null);

//        #endregion

    }
}
