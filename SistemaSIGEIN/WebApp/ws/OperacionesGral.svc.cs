using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SIGE.Entidades;
using SIGE.Entidades.Administracion;
using SIGE.Negocio.AdministracionSitio;
using System.Xml.Linq;
using System.Web.Script.Serialization;

namespace WebApp.ws
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OperacionesGral" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OperacionesGral.svc or OperacionesGral.svc.cs at the Solution Explorer and start debugging.
    public class OperacionesGral : IOperacionesGral
    {
        #region Login
        public string Login(E_LOGIN usuario)
        {
            if( (usuario.Usuario.Equals("admin") && usuario.Password.Equals("admin")) || (usuario.Usuario.Equals("ADMIN") && usuario.Password.Equals("ADMIN")))
            {
                return "Realizado";
            }
            else
            {
                return "Error";
            }
        }
        #endregion

//        #region S_TIPO_COMPETENCIA

//        public List<SPE_OBTIENE_S_TIPO_COMPETENCIA_Result> Get_TIPO_COMPETENCIA(string CL_TIPO_COMPETENCIA = null, string NB_TIPO_COMPETENCIA = null, string DS_TIPO_COMPETENCIA = null)
//        {
//            TipoCompetenciaNegocio negocio = new TipoCompetenciaNegocio();

//            //return "Ok";

//            return negocio.Obtener_S_TIPO_COMPETENCIA(CL_TIPO_COMPETENCIA, NB_TIPO_COMPETENCIA, DS_TIPO_COMPETENCIA);
//        }

//        public bool Insert_update_S_TIPO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_S_TIPO_COMPETENCIA_Result V_S_TIPO_COMPETENCIA, string usuario, string programa)
//        {
//            TipoCompetenciaNegocio negocio = new TipoCompetenciaNegocio();
//            return negocio.InsertaActualiza_S_TIPO_COMPETENCIA(tipo_transaccion, V_S_TIPO_COMPETENCIA, usuario, programa);
//        }

//        public bool Delete_S_TIPO_COMPETENCIA(String CL_TIPO_COMPETENCIA = null, string usuario = null, string programa = null)
//        {
//            TipoCompetenciaNegocio negocio = new TipoCompetenciaNegocio();
//            return negocio.Elimina_S_TIPO_COMPETENCIA(CL_TIPO_COMPETENCIA, usuario, programa);

//        }

//        #endregion	

//        #region C_CALLE

//        public List<SPE_OBTIENE_C_CALLE_Result> Get_C_CALLE(int? ID_CALLE = null, String CL_PAIS = null, String CL_ESTADO = null, String CL_MUNICIPIO = null, String CL_COLONIA = null, String CL_CALLE = null, String NB_CALLE = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            CalleNegocio negocio = new CalleNegocio();
//            return negocio.Obtener_C_CALLE(ID_CALLE, CL_PAIS, CL_ESTADO, CL_MUNICIPIO, CL_COLONIA, CL_CALLE, NB_CALLE, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_CALLE(string tipo_transaccion, SPE_OBTIENE_C_CALLE_Result V_C_CALLE, string usuario, string programa)
//        {
//            CalleNegocio negocio = new CalleNegocio();
//            return negocio.InsertaActualiza_C_CALLE(tipo_transaccion, V_C_CALLE, usuario, programa);
//        }

//        public bool Delete_C_CALLE(int? ID_CALLE = null, string usuario = null, string programa = null)
//        {
//            CalleNegocio negocio = new CalleNegocio();
//            return negocio.Elimina_C_CALLE(ID_CALLE, usuario, programa);

//        }

//        #endregion

//        #region C_CAMPO_ADICIONAL

//        public List<SPE_OBTIENE_C_CAMPO_ADICIONAL_Result> Get_C_CAMPO_ADICIONAL(int? ID_CAMPO = null, String CL_CAMPO = null, String NB_CAMPO = null, String DS_CAMPO = null, bool? FG_REQUERIDO = null, String NO_VALOR_DEFECTO = null, String CL_TIPO_DATO = null, String CL_DIMENSION = null, String CL_TABLA_REFERENCIA = null, String CL_ESQUEMA_REFERENCIA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, bool? FG_MOSTRAR = null, int? ID_CATALOGO_LISTA = null, bool? FG_ADICIONAL = null)
//        {
//            CampoAdicionalNegocio negocio = new CampoAdicionalNegocio();
//            return negocio.Obtener_C_CAMPO_ADICIONAL(ID_CAMPO, CL_CAMPO, NB_CAMPO, DS_CAMPO, FG_REQUERIDO, NO_VALOR_DEFECTO, CL_TIPO_DATO, CL_DIMENSION, CL_TABLA_REFERENCIA, CL_ESQUEMA_REFERENCIA, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA, FG_MOSTRAR,ID_CATALOGO_LISTA,FG_ADICIONAL);
//        }

//        public bool Insert_update_C_CAMPO_ADICIONAL(string tipo_transaccion, SPE_OBTIENE_C_CAMPO_ADICIONAL_Result V_C_CAMPO_ADICIONAL, string usuario, string programa)
//        {
//            CampoAdicionalNegocio negocio = new CampoAdicionalNegocio();
//            return negocio.InsertaActualiza_C_CAMPO_ADICIONAL(tipo_transaccion, V_C_CAMPO_ADICIONAL, usuario, programa);
//        }

//        public bool Delete_C_CAMPO_ADICIONAL(int? ID_CAMPO = null, string usuario = null, string programa = null)
//        {
//            CampoAdicionalNegocio negocio = new CampoAdicionalNegocio();
//            return negocio.Elimina_C_CAMPO_ADICIONAL(ID_CAMPO, usuario, programa);

//        }

//        #endregion	

//        #region C_CAMPO_ADICIONAL_XML

//        public List<SPE_OBTIENE_C_CAMPO_ADICIONAL_XML_Result> Get_C_CAMPO_ADICIONAL_XML(String XML =null,int? ID_CAMPO = null, String CL_CAMPO = null, String NB_CAMPO = null, String DS_CAMPO = null, bool? FG_REQUERIDO = null, String NO_VALOR_DEFECTO = null, String CL_TIPO_DATO = null, String CL_DIMENSION = null, String CL_TABLA_REFERENCIA = null, String CL_ESQUEMA_REFERENCIA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, bool? FG_MOSTRAR = null, int? ID_CATALOGO_LISTA = null, bool? FG_ADICIONAL = null)
//        {
//            CampoAdicionalNegocio negocio = new CampoAdicionalNegocio();
//            return negocio.Obtener_C_CAMPO_ADICIONAL_XML(XML,ID_CAMPO, CL_CAMPO, NB_CAMPO, DS_CAMPO, FG_REQUERIDO, NO_VALOR_DEFECTO, CL_TIPO_DATO, CL_DIMENSION, CL_TABLA_REFERENCIA, CL_ESQUEMA_REFERENCIA, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA, FG_MOSTRAR, ID_CATALOGO_LISTA, FG_ADICIONAL);
//        }

//        #endregion	

//        #region C_COLONIA

//        public List<SPE_OBTIENE_C_COLONIA_Result> Get_C_COLONIA(int? ID_COLONIA = null, String CL_PAIS = null, String CL_ESTADO = null, String CL_MUNICIPIO = null, String CL_COLONIA = null, String NB_COLONIA = null, String CL_TIPO_ASENTAMIENTO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, String CL_CODIGO_POSTAL = null)
//        {
//            ColoniaNegocio negocio = new ColoniaNegocio();
//            return negocio.Obtener_C_COLONIA(ID_COLONIA, CL_PAIS, CL_ESTADO, CL_MUNICIPIO, CL_COLONIA, NB_COLONIA, CL_TIPO_ASENTAMIENTO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA, CL_CODIGO_POSTAL);
//        }
                
//        public List<SPE_OBTENER_TIPO_ASENTAMIENTO_Result> Get_TIPO_ASENTAMIENTO() 
//        { 
//            ColoniaNegocio negocio = new ColoniaNegocio();
//            return negocio.Obtener_TIPO_ASENTAMIENTO();
//        }

//        public bool Insert_update_C_COLONIA(string tipo_transaccion, SPE_OBTIENE_C_COLONIA_Result V_C_COLONIA, string usuario, string programa)
//        {
//            ColoniaNegocio negocio = new ColoniaNegocio();
//            return negocio.InsertaActualiza_C_COLONIA(tipo_transaccion, V_C_COLONIA, usuario, programa);
//        }

//        public bool Delete_C_COLONIA(int? ID_COLONIA = null, string usuario = null, string programa = null)
//        {
//            ColoniaNegocio negocio = new ColoniaNegocio();
//            return negocio.Elimina_C_COLONIA(ID_COLONIA, usuario, programa);

//        }

//        #endregion	

//        #region C_COMPETENCIA

//        public List<SPE_OBTIENE_C_COMPETENCIA_Result> Get_C_COMPETENCIA(int? ID_COMPETENCIA = null, string CL_COMPETENCIA = null, string NB_COMPETENCIA = null, string DS_COMPETENCIA = null, string CL_TIPO_COMPETENCIA = null, string CL_CLASIFICACION = null, bool? FG_ACTIVO = null, string XML_CAMPOS_ADICIONALES = null)  
//        {  
//            CompetenciaNegocio negocio = new CompetenciaNegocio();
//            return negocio.Obtener_SPE_OBTIENE_C_COMPETENCIA( ID_COMPETENCIA,  CL_COMPETENCIA,  NB_COMPETENCIA,  DS_COMPETENCIA,  CL_TIPO_COMPETENCIA,  CL_CLASIFICACION,  FG_ACTIVO,  XML_CAMPOS_ADICIONALES); 
//        }  

//        public bool Insert_update_C_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_C_COMPETENCIA_Result V_C_COMPETENCIA, string usuario, string programa)
//        {
//            CompetenciaNegocio negocio = new CompetenciaNegocio();
//            return negocio.InsertaActualiza_C_COMPETENCIA(tipo_transaccion, V_C_COMPETENCIA, usuario, programa);
//        }

//        public bool Delete_C_COMPETENCIA(int? ID_COMPETENCIA = null, string usuario = null, string programa = null)
//        {
//            CompetenciaNegocio negocio = new CompetenciaNegocio();
//            return negocio.Elimina_C_COMPETENCIA(ID_COMPETENCIA, usuario, programa);

//        }

//        #endregion	

//        #region C_DEPENDIENTE_ECONOMICO

//        public List<SPE_OBTIENE_C_DEPENDIENTE_ECONOMICO_Result> Get_C_DEPENDIENTE_ECONOMICO(int? ID_DEPENDIENTE_ECONOMICO = null, String NB_DEPENDIENTE_ECONOMICO = null, String CL_PARENTEZCO = null, String CL_GENERO = null, DateTime? FE_NACIMIENTO = null, int? ID_BITACORA = null, bool? CL_OCUPACION = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            DependienteNegocio negocio = new DependienteNegocio();
//            return negocio.Obtener_C_DEPENDIENTE_ECONOMICO(ID_DEPENDIENTE_ECONOMICO, NB_DEPENDIENTE_ECONOMICO, CL_PARENTEZCO, CL_GENERO, FE_NACIMIENTO, ID_BITACORA, CL_OCUPACION, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_DEPENDIENTE_ECONOMICO(string tipo_transaccion, SPE_OBTIENE_C_DEPENDIENTE_ECONOMICO_Result V_C_DEPENDIENTE_ECONOMICO, string usuario, string programa)
//        {
//            DependienteNegocio negocio = new DependienteNegocio();
//            return negocio.InsertaActualiza_C_DEPENDIENTE_ECONOMICO(tipo_transaccion, V_C_DEPENDIENTE_ECONOMICO, usuario, programa);
//        }

//        public bool Delete_C_DEPENDIENTE_ECONOMICO(int? ID_DEPENDIENTE_ECONOMICO = null, string usuario = null, string programa = null)
//        {
//            DependienteNegocio negocio = new DependienteNegocio();
//            return negocio.Elimina_C_DEPENDIENTE_ECONOMICO(ID_DEPENDIENTE_ECONOMICO, usuario, programa);

//        }

//        #endregion	
 
//        #region C_DOCUMENTO

//        public List<SPE_OBTIENE_C_DOCUMENTO_Result> Get_C_DOCUMENTO(int? ID_DOCUMENTO = null, String NB_DOCUMENTO = null, String CL_DOCUMENTO = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, String CL_RUTA = null, String CL_TIPO_RUTA = null, DateTime? FE_RECEPCION = null, int? ID_INSTITUCION = null, int? ID_BITACORA = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            DocumentoNegocio negocio = new DocumentoNegocio();
//            return negocio.Obtener_C_DOCUMENTO(ID_DOCUMENTO, NB_DOCUMENTO, CL_DOCUMENTO, ID_CANDIDATO, ID_EMPLEADO, CL_RUTA, CL_TIPO_RUTA, FE_RECEPCION, ID_INSTITUCION, ID_BITACORA, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_DOCUMENTO(string tipo_transaccion, SPE_OBTIENE_C_DOCUMENTO_Result V_C_DOCUMENTO, byte[] archivo, string ruta, string usuario, string programa)
//        {
//            DocumentoNegocio negocio = new DocumentoNegocio();
//            return negocio.InsertaActualiza_C_DOCUMENTO(tipo_transaccion, V_C_DOCUMENTO, archivo, ruta, usuario, programa);
//        }

//        public bool Delete_C_DOCUMENTO(int? ID_DOCUMENTO = null, string usuario = null, string programa = null)
//        {
//            DocumentoNegocio negocio = new DocumentoNegocio();
//            return negocio.Elimina_C_DOCUMENTO(ID_DOCUMENTO, usuario, programa);

//        }

//        #endregion	

//        #region C_EMPLEADO_ESCOLARIDAD

//        public List<SPE_OBTIENE_C_EMPLEADO_ESCOLARIDAD_Result> Get_C_EMPLEADO_ESCOLARIDAD(int? ID_EMPLEADO_ESCOLARIDAD = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_ESCOLARIDAD = null, int? CL_INSTITUCION = null, String NB_INSTITUCION = null, DateTime? FE_PERIODO_INICIO = null, DateTime? FE_PERIODO_FIN = null, String CL_ESTADO_ESCOLARIDAD = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            EmpleadosEscolaridadNegocio negocio = new EmpleadosEscolaridadNegocio();
//            return negocio.Obtener_C_EMPLEADO_ESCOLARIDAD(ID_EMPLEADO_ESCOLARIDAD, ID_EMPLEADO, ID_CANDIDATO, ID_ESCOLARIDAD, CL_INSTITUCION, NB_INSTITUCION, FE_PERIODO_INICIO, FE_PERIODO_FIN, CL_ESTADO_ESCOLARIDAD, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_EMPLEADO_ESCOLARIDAD(E_EMPLEADO_ESCOLARIDAD PRIMARIA = null, E_EMPLEADO_ESCOLARIDAD SECUNDARIA = null, E_EMPLEADO_ESCOLARIDAD PREPA = null, E_EMPLEADO_ESCOLARIDAD UNI1 = null, E_EMPLEADO_ESCOLARIDAD UNI2 = null, E_EMPLEADO_ESCOLARIDAD POS1 = null, E_EMPLEADO_ESCOLARIDAD POS2 = null, string usuario = null, string programa = null)
//        {
//            EmpleadosEscolaridadNegocio negocio = new EmpleadosEscolaridadNegocio();

//            var xmlNuevo = new XElement("ESCOLARIDADES");


//            if (PRIMARIA != null){
//                var xmlPrimaria = new XElement("ESCOLARIDAD",
//                    new XAttribute("ID_CANDIDATO", (PRIMARIA.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (PRIMARIA.ID_EMPLEADO ?? 0)),
//                    new XAttribute("ID_ESCOLARIDAD", PRIMARIA.ID_ESCOLARIDAD),
//                    new XAttribute("NB_INSTITUCION", PRIMARIA.NB_INSTITUCION),
//                    new XAttribute("FE_PERIODO_INICIO", new DateTime(int.Parse(PRIMARIA.FE_PERIODO_INICIO), 1, 1)),
//                    new XAttribute("FE_PERIODO_FIN", new DateTime(int.Parse(PRIMARIA.FE_PERIODO_FIN), 1, 1)),
//                    new XAttribute("CL_ESTADO_ESCOLARIDAD", PRIMARIA.CL_ESTADO_ESCOLARIDAD),
//                    new XAttribute("ID_EMPLEADO_ESCOLARIDAD", (PRIMARIA.ID_EMPLEADO_ESCOLARIDAD ?? 0)),
//                    new XAttribute("CL_INSTITUCION", (PRIMARIA.CL_INSTITUCION ?? 0))
//                );

//                xmlNuevo.Add(xmlPrimaria);
//            }

//            if (SECUNDARIA != null) {
//                var xmlSecundaria = new XElement("ESCOLARIDAD",
//                new XAttribute("ID_CANDIDATO", (SECUNDARIA.ID_CANDIDATO ?? 0)),
//                new XAttribute("ID_EMPLEADO", (SECUNDARIA.ID_EMPLEADO ?? 0)),
//                new XAttribute("ID_ESCOLARIDAD", SECUNDARIA.ID_ESCOLARIDAD),
//                new XAttribute("NB_INSTITUCION", SECUNDARIA.NB_INSTITUCION),
//                new XAttribute("FE_PERIODO_INICIO", new DateTime(int.Parse(SECUNDARIA.FE_PERIODO_INICIO), 1, 1)),
//                new XAttribute("FE_PERIODO_FIN", new DateTime(int.Parse(SECUNDARIA.FE_PERIODO_FIN), 1, 1)),
//                new XAttribute("CL_ESTADO_ESCOLARIDAD", SECUNDARIA.CL_ESTADO_ESCOLARIDAD),
//                new XAttribute("ID_EMPLEADO_ESCOLARIDAD", (SECUNDARIA.ID_EMPLEADO_ESCOLARIDAD ?? 0)),
//                new XAttribute("CL_INSTITUCION", (SECUNDARIA.CL_INSTITUCION ?? 0))
//                );

//                xmlNuevo.Add(xmlSecundaria);
//            }
            

//            if (PREPA != null){
//                var xmlPrepa = new XElement("ESCOLARIDAD",
//                    new XAttribute("ID_CANDIDATO", (PREPA.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (PREPA.ID_EMPLEADO ?? 0)),
//                    new XAttribute("ID_ESCOLARIDAD", PREPA.ID_ESCOLARIDAD),
//                    new XAttribute("NB_INSTITUCION", PREPA.NB_INSTITUCION),
//                    new XAttribute("FE_PERIODO_INICIO", new DateTime(int.Parse(PREPA.FE_PERIODO_INICIO), 1, 1)),
//                    new XAttribute("FE_PERIODO_FIN", new DateTime(int.Parse(PREPA.FE_PERIODO_FIN), 1, 1)),
//                    new XAttribute("CL_ESTADO_ESCOLARIDAD", PREPA.CL_ESTADO_ESCOLARIDAD),
//                    new XAttribute("ID_EMPLEADO_ESCOLARIDAD", (PREPA.ID_EMPLEADO_ESCOLARIDAD ?? 0)),
//                    new XAttribute("CL_INSTITUCION", (PREPA.CL_INSTITUCION ?? 0))
//                );

//                xmlNuevo.Add(xmlPrepa);
//            }

//            if (UNI1 != null) {
//                var xmlUni1 = new XElement("ESCOLARIDAD",
//                    new XAttribute("ID_CANDIDATO", (UNI1.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (UNI1.ID_EMPLEADO ?? 0)),
//                    new XAttribute("ID_ESCOLARIDAD", UNI1.ID_ESCOLARIDAD),
//                    new XAttribute("NB_INSTITUCION", UNI1.NB_INSTITUCION),
//                    new XAttribute("FE_PERIODO_INICIO", new DateTime(int.Parse(UNI1.FE_PERIODO_INICIO), 1, 1)),
//                    new XAttribute("FE_PERIODO_FIN", new DateTime(int.Parse(UNI1.FE_PERIODO_FIN), 1, 1)),
//                    new XAttribute("CL_ESTADO_ESCOLARIDAD", UNI1.CL_ESTADO_ESCOLARIDAD),
//                    new XAttribute("ID_EMPLEADO_ESCOLARIDAD", (UNI1.ID_EMPLEADO_ESCOLARIDAD ?? 0)),
//                    new XAttribute("CL_INSTITUCION", (UNI1.CL_INSTITUCION ?? 0))
//                );

//                xmlNuevo.Add(xmlUni1);
//            }


//            if (UNI2 != null) {
//                var xmlUni2 = new XElement("ESCOLARIDAD",
//                    new XAttribute("ID_CANDIDATO", (UNI2.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (UNI2.ID_EMPLEADO ?? 0)),
//                    new XAttribute("ID_ESCOLARIDAD", UNI2.ID_ESCOLARIDAD),
//                    new XAttribute("NB_INSTITUCION", UNI2.NB_INSTITUCION),
//                    new XAttribute("FE_PERIODO_INICIO", new DateTime(int.Parse(UNI2.FE_PERIODO_INICIO), 1, 1)),
//                    new XAttribute("FE_PERIODO_FIN", new DateTime(int.Parse(UNI2.FE_PERIODO_FIN), 1, 1)),
//                    new XAttribute("CL_ESTADO_ESCOLARIDAD", UNI2.CL_ESTADO_ESCOLARIDAD),
//                    new XAttribute("ID_EMPLEADO_ESCOLARIDAD", (UNI2.ID_EMPLEADO_ESCOLARIDAD ?? 0)),
//                    new XAttribute("CL_INSTITUCION", (UNI2.CL_INSTITUCION ?? 0))
//                );
//                xmlNuevo.Add(xmlUni2);
//            }

//            if (POS1 != null) {
//                var xmlPos1 = new XElement("ESCOLARIDAD",
//                    new XAttribute("ID_CANDIDATO", (POS1.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (POS1.ID_EMPLEADO ?? 0)),
//                    new XAttribute("ID_ESCOLARIDAD", POS1.ID_ESCOLARIDAD),
//                    new XAttribute("NB_INSTITUCION", POS1.NB_INSTITUCION),
//                    new XAttribute("FE_PERIODO_INICIO", new DateTime(int.Parse(POS1.FE_PERIODO_INICIO), 1, 1)),
//                    new XAttribute("FE_PERIODO_FIN", new DateTime(int.Parse(POS1.FE_PERIODO_FIN), 1, 1)),
//                    new XAttribute("CL_ESTADO_ESCOLARIDAD", POS1.CL_ESTADO_ESCOLARIDAD),
//                    new XAttribute("ID_EMPLEADO_ESCOLARIDAD", (POS1.ID_EMPLEADO_ESCOLARIDAD ?? 0)),
//                    new XAttribute("CL_INSTITUCION", (POS1.CL_INSTITUCION ?? 0))
//                );
//                xmlNuevo.Add(xmlPos1);
//            }


//            if (POS2 != null) {
//                var xmlPos2 = new XElement("ESCOLARIDAD",
//                    new XAttribute("ID_CANDIDATO", (POS2.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (POS2.ID_EMPLEADO ?? 0)),
//                    new XAttribute("ID_ESCOLARIDAD", POS2.ID_ESCOLARIDAD),
//                    new XAttribute("NB_INSTITUCION", POS2.NB_INSTITUCION),
//                    new XAttribute("FE_PERIODO_INICIO", new DateTime(int.Parse(POS2.FE_PERIODO_INICIO), 1, 1)),
//                    new XAttribute("FE_PERIODO_FIN", new DateTime(int.Parse(POS2.FE_PERIODO_FIN), 1, 1)),
//                    new XAttribute("CL_ESTADO_ESCOLARIDAD", POS2.CL_ESTADO_ESCOLARIDAD),
//                    new XAttribute("ID_EMPLEADO_ESCOLARIDAD", (POS2.ID_EMPLEADO_ESCOLARIDAD ?? 0)),
//                    new XAttribute("CL_INSTITUCION", (POS2.CL_INSTITUCION ?? 0))
//                );

//                xmlNuevo.Add(xmlPos2);
//            }

//            return negocio.InsertaActualiza_C_EMPLEADO_ESCOLARIDAD(xmlNuevo, usuario, programa);
//        }

//        public bool Delete_C_EMPLEADO_ESCOLARIDAD(int? ID_EMPLEADO_ESCOLARIDAD = null, string usuario = null, string programa = null)
//        {
//            EmpleadosEscolaridadNegocio negocio = new EmpleadosEscolaridadNegocio();
//            return negocio.Elimina_C_EMPLEADO_ESCOLARIDAD(ID_EMPLEADO_ESCOLARIDAD, usuario, programa);

//        }

//        #endregion	

        
//        #region C_EMPLEADO_IDIOMA

//        public List<SPE_OBTIENE_C_EMPLEADO_IDIOMA_Result> Get_C_EMPLEADO_IDIOMA(int? ID_EMPLEADO_IDIOMA = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_IDIOMA = null, Decimal? PR_LECTURA = null, Decimal? PR_ESCRITURA = null, Decimal? PR_CONVERSACIONAL = null, int? CL_INSTITUCION = null, Decimal? NO_PUNTAJE = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            EmpleadoIdiomaNegocio negocio = new EmpleadoIdiomaNegocio();
//            return negocio.Obtener_C_EMPLEADO_IDIOMA(ID_EMPLEADO_IDIOMA, ID_EMPLEADO, ID_CANDIDATO, ID_IDIOMA, PR_LECTURA, PR_ESCRITURA, PR_CONVERSACIONAL, CL_INSTITUCION, NO_PUNTAJE, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_EMPLEADO_IDIOMA(string tipo_transaccion, SPE_OBTIENE_C_EMPLEADO_IDIOMA_Result V_C_EMPLEADO_IDIOMA, string usuario, string programa)
//        {
//            EmpleadoIdiomaNegocio negocio = new EmpleadoIdiomaNegocio();
//            return negocio.InsertaActualiza_C_EMPLEADO_IDIOMA(tipo_transaccion, V_C_EMPLEADO_IDIOMA, usuario, programa);
//        }

//        public bool Delete_C_EMPLEADO_IDIOMA(int? ID_EMPLEADO_IDIOMA = null, string usuario = null, string programa = null)
//        {
//            EmpleadoIdiomaNegocio negocio = new EmpleadoIdiomaNegocio();
//            return negocio.Elimina_C_EMPLEADO_IDIOMA(ID_EMPLEADO_IDIOMA, usuario, programa);

//        }

//        #endregion	

//        #region C_ESCOLARIDAD

//        public List<SPE_OBTIENE_C_ESCOLARIDAD_Result> Get_C_ESCOLARIDAD(int? ID_ESCOLARIDAD = null, String NB_ESCOLARIDAD = null, String DS_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null, int? ID_NIVEL_ESCOLARIDAD = null)
//        {
//            EscolaridadNegocio negocio = new EscolaridadNegocio();
//            return negocio.Obtener_C_ESCOLARIDAD(ID_ESCOLARIDAD, NB_ESCOLARIDAD, DS_ESCOLARIDAD, CL_NIVEL_ESCOLARIDAD, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA, ID_NIVEL_ESCOLARIDAD);
//        }

//        public bool Insert_update_C_ESCOLARIDAD(string tipo_transaccion, SPE_OBTIENE_C_ESCOLARIDAD_Result V_C_ESCOLARIDAD, string usuario, string programa)
//        {
//            EscolaridadNegocio negocio = new EscolaridadNegocio();
//            return negocio.InsertaActualiza_C_ESCOLARIDAD(tipo_transaccion, V_C_ESCOLARIDAD, usuario, programa);
//        }

//        public bool Delete_C_ESCOLARIDAD(SPE_OBTIENE_C_ESCOLARIDAD_Result V_LISTA_DINAMICA = null, string usuario = null, string programa = null)
//        {
//            EscolaridadNegocio negocio = new EscolaridadNegocio();
//            return negocio.Elimina_C_ESCOLARIDAD(V_LISTA_DINAMICA, usuario, programa);

//        }

//        #endregion	

//        #region C_ESTADO

//        public List<SPE_OBTIENE_C_ESTADO_Result> Get_C_ESTADO(int? ID_ESTADO = null, String CL_PAIS = null, String CL_ESTADO = null, String NB_ESTADO = null,
//            DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, 
//            String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            EstadoNegocio negocio = new EstadoNegocio();

//            return negocio.Obtener_C_ESTADO(ID_ESTADO, CL_PAIS, CL_ESTADO, NB_ESTADO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_ESTADO(string tipo_transaccion, SPE_OBTIENE_C_ESTADO_Result V_C_ESTADO, string usuario, string programa)
//        {
//            EstadoNegocio negocio = new EstadoNegocio();
//            return negocio.InsertaActualiza_C_ESTADO(tipo_transaccion, V_C_ESTADO, usuario, programa);
//        }

//        public bool Delete_C_ESTADO(int? ID_ESTADO = null, string usuario = null, string programa = null)
//        {
//            EstadoNegocio negocio = new EstadoNegocio();
//            return negocio.Elimina_C_ESTADO(ID_ESTADO, usuario, programa);

//        }

//        #endregion	

//        #region C_EVALUADOR_EXTERNO

//        public List<SPE_OBTIENE_C_EVALUADOR_EXTERNO_Result> Get_C_EVALUADOR_EXTERNO(int? ID_EVALUADOR_EXTERNO = null, String CL_EVALUADOR_EXTERNO = null, String NB_EVALUADOR_EXTERNO = null, String DS_EVALUARDO_EXTERNO = null, bool? FG_ACTIVO = null, String XML_CAMPOS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            EvaluadorExternoNegocio negocio = new EvaluadorExternoNegocio();
//            return negocio.Obtener_C_EVALUADOR_EXTERNO(ID_EVALUADOR_EXTERNO, CL_EVALUADOR_EXTERNO, NB_EVALUADOR_EXTERNO, DS_EVALUARDO_EXTERNO, FG_ACTIVO, XML_CAMPOS_ADICIONALES, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_EVALUADOR_EXTERNO(string tipo_transaccion, SPE_OBTIENE_C_EVALUADOR_EXTERNO_Result V_C_EVALUADOR_EXTERNO, string usuario, string programa)
//        {
//            EvaluadorExternoNegocio negocio = new EvaluadorExternoNegocio();
//            return negocio.InsertaActualiza_C_EVALUADOR_EXTERNO(tipo_transaccion, V_C_EVALUADOR_EXTERNO, usuario, programa);
//        }

//        public bool Delete_C_EVALUADOR_EXTERNO(int? ID_EVALUADOR_EXTERNO = null, string usuario = null, string programa = null)
//        {
//            EvaluadorExternoNegocio negocio = new EvaluadorExternoNegocio();
//            return negocio.Elimina_C_EVALUADOR_EXTERNO(ID_EVALUADOR_EXTERNO, usuario, programa);

//        }

//        #endregion	

//        #region C_IDIOMA

//        public List<SPE_OBTIENE_C_IDIOMA_Result> Get_C_IDIOMA(int? ID_IDIOMA = null, String CL_IDIOMA = null, String NB_IDIOMA = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            IdiomaNegocio negocio = new IdiomaNegocio();
//            return negocio.Obtener_C_IDIOMA(ID_IDIOMA, CL_IDIOMA, NB_IDIOMA, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_IDIOMA(string tipo_transaccion, SPE_OBTIENE_C_IDIOMA_Result V_C_IDIOMA, string usuario, string programa)
//        {
//            IdiomaNegocio negocio = new IdiomaNegocio();
//            return negocio.InsertaActualiza_C_IDIOMA(tipo_transaccion, V_C_IDIOMA, usuario, programa);
//        }

//        public bool Delete_C_IDIOMA(int? ID_IDIOMA = null, string usuario = null, string programa = null)
//        {
//            IdiomaNegocio negocio = new IdiomaNegocio();
//            return negocio.Elimina_C_IDIOMA(ID_IDIOMA, usuario, programa);

//        }

//        #endregion	

//        #region C_MUNICIPIO

//        public List<SPE_OBTIENE_C_MUNICIPIO_Result> Get_C_MUNICIPIO(int? ID_MUNICIPIO = null, string CL_PAIS = null, string CL_ESTADO = null, string CL_MUNICIPIO = null, string NB_MUNICIPIO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, string CL_USUARIO_APP_CREA = null, string CL_USUARIO_APP_MODIFICA = null, string NB_PROGRAMA_CREA = null, string NB_PROGRAMA_MODIFICA = null)
//        {
//            MunicipioNegocio negocio = new MunicipioNegocio();
//            return negocio.Obtener_C_MUNICIPIO(ID_MUNICIPIO, CL_PAIS, CL_ESTADO, CL_MUNICIPIO, NB_MUNICIPIO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_MUNICIPIO(string tipo_transaccion, SPE_OBTIENE_C_MUNICIPIO_Result V_C_MUNICIPIO, string usuario, string programa)
//        {
//            MunicipioNegocio negocio = new MunicipioNegocio();
//            return negocio.InsertaActualiza_C_MUNICIPIO(tipo_transaccion, V_C_MUNICIPIO, usuario, programa);
//        }

//        public bool Delete_C_MUNICIPIO(int? ID_MUNICIPIO = null, string usuario = null, string programa = null)
//        {
//            MunicipioNegocio negocio = new MunicipioNegocio();
//            return negocio.Elimina_C_MUNICIPIO(ID_MUNICIPIO, usuario, programa);

//        }

//        #endregion	

//        #region C_NIVEL_ESCOLARIDAD

//        public List<SPE_OBTIENE_C_NIVEL_ESCOLARIDAD_Result> Get_C_NIVEL_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null, String CL_NIVEL_ESCOLARIDAD = null, String DS_NIVEL_ESCOLARIDAD = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            NivelEscolaridadNegocio negocio = new NivelEscolaridadNegocio();
//            return negocio.Obtener_C_NIVEL_ESCOLARIDAD(ID_NIVEL_ESCOLARIDAD, CL_NIVEL_ESCOLARIDAD, DS_NIVEL_ESCOLARIDAD, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_NIVEL_ESCOLARIDAD(string tipo_transaccion, SPE_OBTIENE_C_NIVEL_ESCOLARIDAD_Result V_C_NIVEL_ESCOLARIDAD, string usuario, string programa)
//        {
//            NivelEscolaridadNegocio negocio = new NivelEscolaridadNegocio();
//            return negocio.InsertaActualiza_C_NIVEL_ESCOLARIDAD(tipo_transaccion, V_C_NIVEL_ESCOLARIDAD, usuario, programa);
//        }

//        public bool Delete_C_NIVEL_ESCOLARIDAD(int? ID_NIVEL_ESCOLARIDAD = null, string usuario = null, string programa = null)
//        {
//            NivelEscolaridadNegocio negocio = new NivelEscolaridadNegocio();
//            return negocio.Elimina_C_NIVEL_ESCOLARIDAD(ID_NIVEL_ESCOLARIDAD, usuario, programa);

//        }

//        #endregion	

//        #region C_PERIODO

//        public List<SPE_OBTIENE_C_PERIODO_Result> Get_C_PERIODO(int? ID_PERIODO = null, String CL_PERIODO = null, String NB_PERIODO = null, String DS_PERIODO = null, bool? FE_INICIO = null, String FE_TERMINO = null, String CL_ESTADO_PERIODO = null, String XML_CAMPOS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            PeriodoNegocio negocio = new PeriodoNegocio();
//            return negocio.Obtener_C_PERIODO(ID_PERIODO, CL_PERIODO, NB_PERIODO, DS_PERIODO, FE_INICIO, FE_TERMINO, CL_ESTADO_PERIODO, XML_CAMPOS_ADICIONALES, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_PERIODO(string tipo_transaccion, SPE_OBTIENE_C_PERIODO_Result V_C_PERIODO, string usuario, string programa)
//        {
//            PeriodoNegocio negocio = new PeriodoNegocio();
//            return negocio.InsertaActualiza_C_PERIODO(tipo_transaccion, V_C_PERIODO, usuario, programa);
//        }

//        public bool Delete_C_PERIODO(int? ID_PERIODO = null, string usuario = null, string programa = null)
//        {
//            PeriodoNegocio negocio = new PeriodoNegocio();
//            return negocio.Elimina_C_PERIODO(ID_PERIODO, usuario, programa);

//        }

//        #endregion	

//        #region C_ROL

//        public List<SPE_OBTIENE_C_ROL_Result> Get_C_ROL(int? ID_ROL = null, String CL_ROL = null, String NB_ROL = null, String XML_AUTORIZACION = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            RolNegocio negocio = new RolNegocio();
//            return negocio.Obtener_C_ROL(ID_ROL, CL_ROL, NB_ROL, XML_AUTORIZACION, FG_ACTIVO, FE_INACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_ROL(string tipo_transaccion, SPE_OBTIENE_C_ROL_Result V_C_ROL, string usuario, string programa)
//        {
//            RolNegocio negocio = new RolNegocio();
//            return negocio.InsertaActualiza_C_ROL(tipo_transaccion, V_C_ROL, usuario, programa);
//        }

//        public bool Delete_C_ROL(int? ID_ROL = null, string usuario = null, string programa = null)
//        {
//            RolNegocio negocio = new RolNegocio();
//            return negocio.Elimina_C_ROL(ID_ROL, usuario, programa);

//        }

//        #endregion	

//        #region C_ROL_FUNCION

//        public List<SPE_OBTIENE_C_ROL_FUNCION_Result> Get_C_ROL_FUNCION(int?  ID_ROL = null,int?  ID_FUNCION = null)
//        {
//            RolFuncionNegocio negocio = new RolFuncionNegocio();
//            return negocio.Obtener_C_ROL_FUNCION(ID_ROL,ID_FUNCION);
//        }

//        public bool Insert_update_C_ROL_FUNCION (string tipo_transaccion, SPE_OBTIENE_C_ROL_FUNCION_Result V_C_ROL_FUNCION,string usuario, string programa)
//        {
//            RolFuncionNegocio negocio = new RolFuncionNegocio();
//            return negocio.InsertaActualiza_C_ROL_FUNCION(tipo_transaccion,V_C_ROL_FUNCION, usuario,programa);
//        }

//        public bool Delete_C_ROL_FUNCION (SPE_OBTIENE_C_ROL_FUNCION_Result V_C_ROL_FUNCION,string usuario = null, string programa = null)
//        {
//            RolFuncionNegocio negocio = new RolFuncionNegocio();
//            return negocio.Elimina_C_ROL_FUNCION(V_C_ROL_FUNCION, usuario, programa);
            
//        }

//        #endregion	

//        #region C_SECUENCIA

//        public List<SPE_OBTIENE_FOLIO_SECUENCIA_Result> ObtieneFolioSecuencia(String CL_SECUENCIA = null)
//        {
//            SecuenciaNegocio negocio = new SecuenciaNegocio();
//            var s = negocio.ObtieneFolioSecuencia(CL_SECUENCIA);

//            return s;
//        }

//        public List<SPE_OBTIENE_C_SECUENCIA_Result> Get_C_SECUENCIA(String CL_SECUENCIA = null, String CL_PREFIJO = null, int? NO_ULTIMO_VALOR = null, int? NO_VALOR_MAXIMO = null, String CL_SUFIJO = null, byte? NO_DIGITOS = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            SecuenciaNegocio negocio = new SecuenciaNegocio();
//            return negocio.Obtener_C_SECUENCIA(CL_SECUENCIA, CL_PREFIJO, NO_ULTIMO_VALOR, NO_VALOR_MAXIMO, CL_SUFIJO, NO_DIGITOS, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_SECUENCIA(string tipo_transaccion, SPE_OBTIENE_C_SECUENCIA_Result V_C_SECUENCIA, string usuario, string programa)
//        {
//            SecuenciaNegocio negocio = new SecuenciaNegocio();
//            return negocio.InsertaActualiza_C_SECUENCIA(tipo_transaccion, V_C_SECUENCIA, usuario, programa);
//        }

//        public bool Delete_C_SECUENCIA(String CL_SECUENCIA = null, string usuario = null, string programa = null)
//        {
//            SecuenciaNegocio negocio = new SecuenciaNegocio();
//            return negocio.Elimina_C_SECUENCIA(CL_SECUENCIA, usuario, programa);

//        }

//        #endregion	

//        #region M_EMPLEADO


//        public List<SPE_OBTIENE_M_EMPLEADO_Result> Get_M_EMPLEADO(int? ID_EMPLEADO = null, String CL_EMPLEADO = null, String NB_EMPLEADO = null, String NB_APELLIDO_PATERNO = null, String NB_APELLIDO_MATERNO = null, String CL_ESTADO_EMPLEADO = null, String CL_GENERO = null, String CL_ESTADO_CIVIL = null, String NB_CONYUGUE = null, String CL_RFC = null, String CL_CURP = null, String CL_NSS = null, String CL_TIPO_SANGUINEO = null, String CL_NACIONALIDAD = null, String NB_PAIS = null, String NB_ESTADO = null, String NB_MUNICIPIO = null, String NB_COLONIA = null, String NB_CALLE = null, String NO_INTERIOR = null, String NO_EXTERIOR = null, String CL_CODIGO_POSTAL = null, String CL_CORREO_ELECTRONICO = null, bool? FG_ACTIVO = null, System.DateTime? FE_NACIMIENTO = null, String DS_LUGAR_NACIMIENTO = null, System.DateTime? FE_ALTA = null, System.DateTime? FE_BAJA = null, int? ID_PUESTO = null, Decimal? MN_SUELDO = null, Decimal? MN_SUELDO_VARIABLE = null, String DS_SUELDO_COMPOSICION = null, int? ID_CANDIDATO = null, int? ID_EMPRESA = null,
//               bool? MP_FG_ACTIVO = null, System.DateTime? FE_INACTIVO = null, String CL_PUESTO = null, String NB_PUESTO = null, int? ID_PUESTO_JEFE = null, int? ID_DEPARTAMENTO = null, int? ID_BITACORA = null,
//               String NB_CANDIDATO = null, String CC_NB_APELLIDO_PATERNO = null, String CC_NB_APELLIDO_MATERNO = null, String CC_CL_GENERO = null, String CC_CL_RFC = null, String CC_CL_CURP = null, String CC_CL_ESTADO_CIVIL = null, String CC_NB_CONYUGUE = null, String CC_CL_NSS = null, String CC_CL_TIPO_SANGUINEO = null, String CC_NB_PAIS = null, String CC_NB_ESTADO = null, String CC_NB_MUNICIPIO = null, String CC_NB_COLONIA = null, String CC_NB_CALLE = null, String CC_NO_INTERIOR = null, String CC_NO_EXTERIOR = null, String CC_CL_CODIGO_POSTAL = null, String CC_CL_CORREO_ELECTRONICO = null, System.DateTime? CC_FE_NACIMIENTO = null, String CC_DS_LUGAR_NACIMIENTO = null, Decimal? CC_MN_SUELDO = null, String CC_CL_NACIONALIDAD = null, String DS_NACIONALIDAD = null, String NB_LICENCIA = null, String DS_VEHICULO = null, String CL_CARTILLA_MILITAR = null, String CL_CEDULA_PROFESIONAL = null, String DS_DISPONIBILIDAD = null, String CL_DISPONIBILIDAD_VIAJE = null, String DS_COMENTARIO = null, bool? CC_FG_ACTIVO = null,
//               String CL_EMPRESA = null, String NB_EMPRESA = null, String NB_RAZON_SOCIAL = null, bool? MD_FG_ACTIVO = null, System.DateTime? MD_FE_INACTIVO = null, String CL_DEPARTAMENTO = null, String NB_DEPARTAMENTO = null, XElement xml = null)
           
//            //Get_M_EMPLEADO(int?  ID_EMPLEADO = null,String CL_EMPLEADO = null,String NB_EMPLEADO = null,String NB_APELLIDO_PATERNO = null,String NB_APELLIDO_MATERNO = null,String CL_ESTADO_EMPLEADO = null,String CL_GENERO = null,String CL_ESTADO_CIVIL = null,String NB_CONYUGUE = null,String CL_RFC = null,String CL_CURP = null,String CL_NSS = null,String CL_TIPO_SANGUINEO = null,String CL_NACIONALIDAD = null,String NB_PAIS = null,String NB_ESTADO = null,String NB_MUNICIPIO = null,String NB_COLONIA = null,String NB_CALLE = null,String NO_INTERIOR = null,String NO_EXTERIOR = null,String CL_CODIGO_POSTAL = null,String XML_TELEFONOS = null,String CL_CORREO_ELECTRONICO = null,bool?  FG_ACTIVO = null,DateTime?  FE_NACIMIENTO = null,String DS_LUGAR_NACIMIENTO = null,DateTime?  FE_ALTA = null,DateTime?  FE_BAJA = null,int?  ID_PUESTO = null,Decimal?  MN_SUELDO = null,Decimal?  MN_SUELDO_VARIABLE = null,String DS_SUELDO_COMPOSICION = null,int?  ID_CANDIDATO = null,int?  ID_EMPRESA = null,String XML_CAMPOS_ADICIONALES = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
//         {
//            EmpleadoNegocio negocio = new EmpleadoNegocio();
//            return negocio.Obtener_M_EMPLEADO(ID_EMPLEADO,CL_EMPLEADO,NB_EMPLEADO,NB_APELLIDO_PATERNO,NB_APELLIDO_MATERNO,CL_ESTADO_EMPLEADO,CL_GENERO,CL_ESTADO_CIVIL,NB_CONYUGUE,CL_RFC,CL_CURP,CL_NSS,CL_TIPO_SANGUINEO,CL_NACIONALIDAD,NB_PAIS,NB_ESTADO,NB_MUNICIPIO,NB_COLONIA,NB_CALLE,NO_INTERIOR,NO_EXTERIOR,CL_CODIGO_POSTAL,CL_CORREO_ELECTRONICO,FG_ACTIVO,FE_NACIMIENTO,DS_LUGAR_NACIMIENTO,FE_ALTA,FE_BAJA,ID_PUESTO,MN_SUELDO,MN_SUELDO_VARIABLE,DS_SUELDO_COMPOSICION,ID_CANDIDATO,ID_EMPRESA,MP_FG_ACTIVO ,FE_INACTIVO,CL_PUESTO ,NB_PUESTO,ID_PUESTO_JEFE,ID_DEPARTAMENTO,ID_BITACORA,NB_CANDIDATO,CC_NB_APELLIDO_PATERNO,CC_NB_APELLIDO_MATERNO,CC_CL_GENERO,CC_CL_RFC,CC_CL_CURP,CC_CL_ESTADO_CIVIL,CC_NB_CONYUGUE,CC_CL_NSS,CC_CL_TIPO_SANGUINEO,CC_NB_PAIS,CC_NB_ESTADO,CC_NB_MUNICIPIO,CC_NB_COLONIA,CC_NB_CALLE,CC_NO_INTERIOR,CC_NO_EXTERIOR,CC_CL_CODIGO_POSTAL,CC_CL_CORREO_ELECTRONICO,CC_FE_NACIMIENTO,CC_DS_LUGAR_NACIMIENTO,CC_MN_SUELDO,CC_CL_NACIONALIDAD ,DS_NACIONALIDAD,NB_LICENCIA ,DS_VEHICULO,CL_CARTILLA_MILITAR,CL_CEDULA_PROFESIONAL,DS_DISPONIBILIDAD,CL_DISPONIBILIDAD_VIAJE,DS_COMENTARIO,CC_FG_ACTIVO,CL_EMPRESA,NB_EMPRESA,NB_RAZON_SOCIAL, MD_FG_ACTIVO,MD_FE_INACTIVO,CL_DEPARTAMENTO,NB_DEPARTAMENTO, xml);
//                    //return negocio.Obtener_M_EMPLEADO(ID_EMPLEADO,CL_EMPLEADO,NB_EMPLEADO,NB_APELLIDO_PATERNO,NB_APELLIDO_MATERNO,CL_ESTADO_EMPLEADO,CL_GENERO,CL_ESTADO_CIVIL,NB_CONYUGUE,CL_RFC,CL_CURP,CL_NSS,CL_TIPO_SANGUINEO,CL_NACIONALIDAD,NB_PAIS,NB_ESTADO,NB_MUNICIPIO,NB_COLONIA,NB_CALLE,NO_INTERIOR,NO_EXTERIOR,CL_CODIGO_POSTAL,XML_TELEFONOS,CL_CORREO_ELECTRONICO,FG_ACTIVO,FE_NACIMIENTO,DS_LUGAR_NACIMIENTO,FE_ALTA,FE_BAJA,ID_PUESTO,MN_SUELDO,MN_SUELDO_VARIABLE,DS_SUELDO_COMPOSICION,ID_CANDIDATO,ID_EMPRESA,XML_CAMPOS_ADICIONALES,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);
 
//         }

//        public bool Insert_update_M_EMPLEADO (string tipo_transaccion, SPE_OBTIENE_M_EMPLEADO_Result V_M_EMPLEADO,string usuario, string programa)
//        {
//            EmpleadoNegocio negocio = new EmpleadoNegocio();
//            return negocio.InsertaActualiza_M_EMPLEADO(tipo_transaccion,V_M_EMPLEADO, usuario,programa);
//        }

//        public bool Delete_M_EMPLEADO(int? ID_EMPLEADO = null,string CL_EMPLEADO=null, string usuario = null, string programa = null)
//        {
//            EmpleadoNegocio negocio = new EmpleadoNegocio();
//            return negocio.Elimina_M_EMPLEADO(ID_EMPLEADO,CL_EMPLEADO, usuario, programa);
            
//        }

//    #endregion	

//        #region M_PUESTO

   
//           public List<SPE_OBTIENE_M_PUESTO_Result> Get_M_PUESTO(int? ID_PUESTO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, String CL_PUESTO = null, String NB_PUESTO = null, int? ID_PUESTO_JEFE = null, int? ID_DEPARTAMENTO = null, String XML_CAMPOS_ADICIONALES = null, int? ID_BITACORA = null, byte? NO_EDAD_MINIMA = null, byte? NO_EDAD_MAXIMA = null, String CL_GENERO = null, String CL_ESTADO_CIVIL = null, String XML_REQUERIMIENTOS = null, String XML_OBSERVACIONES = null, String XML_RESPONSABILIDAD = null, String XML_AUTORIDAD = null, String XML_CURSOS_ADICIONALES = null, String XML_MENTOR = null, String CL_TIPO_PUESTO = null, int? ID_CENTRO_ADMINISTRATIVO = null, int? ID_CENTRO_OPERATIVO = null, int? ID_PAQUETE_PRESTACIONES = null,String NB_DEPARTAMENTO=null,String CL_DEPARTAMENTO=null)
//        {
//            PuestoNegocio negocio = new PuestoNegocio();
//            return negocio.Obtener_M_PUESTO(ID_PUESTO, FG_ACTIVO, FE_INACTIVO, CL_PUESTO, NB_PUESTO, ID_PUESTO_JEFE, ID_DEPARTAMENTO, XML_CAMPOS_ADICIONALES, ID_BITACORA, NO_EDAD_MINIMA, NO_EDAD_MAXIMA, CL_GENERO, CL_ESTADO_CIVIL, XML_REQUERIMIENTOS, XML_OBSERVACIONES, XML_RESPONSABILIDAD, XML_AUTORIDAD, XML_CURSOS_ADICIONALES, XML_MENTOR, CL_TIPO_PUESTO, ID_CENTRO_ADMINISTRATIVO, ID_CENTRO_OPERATIVO, ID_PAQUETE_PRESTACIONES,NB_DEPARTAMENTO,CL_DEPARTAMENTO);
//        }
         

//        public bool Insert_update_M_PUESTO (string tipo_transaccion, SPE_OBTIENE_M_PUESTO_Result V_M_PUESTO,string usuario, string programa)
//        {
//            PuestoNegocio negocio = new PuestoNegocio();
//            return negocio.InsertaActualiza_M_PUESTO(tipo_transaccion,V_M_PUESTO, usuario,programa);
//        }

//        public bool Delete_M_PUESTO(int? ID_PUESTO= null, string CL_PUESTO=null, string usuario = null, string programa = null)
//        {
//            PuestoNegocio negocio = new PuestoNegocio();
//            return negocio.Elimina_M_PUESTO(ID_PUESTO,CL_PUESTO, usuario, programa);
            
//        }

//    #endregion	

//        #region S_CONFIGURACION

//        public List<SPE_OBTIENE_S_CONFIGURACION_Result> Get_S_CONFIGURACION(String XML_CONFIGURACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_MODIFICA = null,String NB_PROGRAMA_MODIFICA = null)
//        {
//            ConfiguracionNegocio negocio = new ConfiguracionNegocio();
//            return negocio.Obtener_S_CONFIGURACION(XML_CONFIGURACION,FE_MODIFICACION,CL_USUARIO_MODIFICA,NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_S_CONFIGURACION (string tipo_transaccion, SPE_OBTIENE_S_CONFIGURACION_Result V_S_CONFIGURACION,string usuario, string programa)
//        {
//            ConfiguracionNegocio negocio = new ConfiguracionNegocio();
//            return negocio.InsertaActualiza_S_CONFIGURACION(tipo_transaccion,V_S_CONFIGURACION, usuario,programa);
//        }

//        public bool Delete_S_CONFIGURACION (string usuario = null, string programa = null)
//        {
//            ConfiguracionNegocio negocio = new ConfiguracionNegocio();
//            return negocio.Elimina_S_CONFIGURACION( usuario, programa);
            
//        }

//        #endregion	

//        #region S_TIPO_DATO

//        public List<SPE_OBTIENE_S_TIPO_DATO_Result> Get_S_TIPO_DATO(String CL_TIPO_DATO = null,String NB_TIPO_DATO = null)
//        {
//            TipoDatoNegocio negocio = new TipoDatoNegocio();
//            return negocio.Obtener_S_TIPO_DATO(CL_TIPO_DATO,NB_TIPO_DATO);
//        }

//        public bool Insert_update_S_TIPO_DATO (string tipo_transaccion, SPE_OBTIENE_S_TIPO_DATO_Result V_S_TIPO_DATO,string usuario, string programa)
//        {
//            TipoDatoNegocio negocio = new TipoDatoNegocio();
//            return negocio.InsertaActualiza_S_TIPO_DATO(tipo_transaccion,V_S_TIPO_DATO, usuario,programa);
//        }

//        public bool Delete_S_TIPO_DATO (SPE_OBTIENE_S_TIPO_DATO_Result V_S_TIPO_DATO,string usuario = null, string programa = null)
//        {
//            TipoDatoNegocio negocio = new TipoDatoNegocio();
//            return negocio.Elimina_S_TIPO_DATO(V_S_TIPO_DATO, usuario, programa);
            
//        }

//        #endregion	

//        #region S_FUNCION

//        public List<SPE_OBTIENE_S_FUNCION_Result> Get_S_FUNCION(int? ID_FUNCION = null, String CL_FUNCION = null, String CL_TIPO_FUNCION = null, String NB_FUNCION = null, int? ID_FUNCION_PADRE = null, String NB_URL = null, String XML_CONFIGURACION = null)
//        {
//            FuncionNegocio negocio = new FuncionNegocio();
//            return negocio.Obtener_S_FUNCION(ID_FUNCION, CL_FUNCION, CL_TIPO_FUNCION, NB_FUNCION, ID_FUNCION_PADRE, NB_URL, XML_CONFIGURACION);
//        }

//        public bool Insert_update_S_FUNCION(string tipo_transaccion, SPE_OBTIENE_S_FUNCION_Result V_S_FUNCION, string usuario, string programa)
//        {
//            FuncionNegocio negocio = new FuncionNegocio();
//            return negocio.InsertaActualiza_S_FUNCION(tipo_transaccion, V_S_FUNCION, usuario, programa);
//        }

//        public bool Delete_S_FUNCION(int? ID_FUNCION = null, string usuario = null, string programa = null)
//        {
//            FuncionNegocio negocio = new FuncionNegocio();
//            return negocio.Elimina_S_FUNCION(ID_FUNCION, usuario, programa);

//        }

//        #endregion	
        
//        #region DEPARTAMENTOS

//        public bool Insert_M_DEPARTAMENTO(SPE_OBTIENE_M_DEPARTAMENTO_Result departamento, string CL_USUARIO, string NB_PROGRAMA, string CL_TIPO)
//        {
            
//            DepartamentoNegocio negocio = new DepartamentoNegocio();

//            return negocio.InsertaActualiza_M_DEPARTAMENTO(CL_TIPO, departamento, CL_USUARIO, NB_PROGRAMA);
//            //return false;
//        }

//        public bool Delete_M_DEPARTAMENTO(int departamento, string CL_USUARIO, string NB_PROGRAMA)
//        {
//            DepartamentoNegocio negocio = new DepartamentoNegocio();

//            return negocio.Elimina_M_DEPARTAMENTO(departamento, CL_USUARIO, NB_PROGRAMA);
            
//        }

//        public List<SPE_OBTIENE_M_DEPARTAMENTO_Result> Get_M_DEPARTAMENTO(int? ID_DEPARTAMENTO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, String CL_DEPARTAMENTO = null, String NB_DEPARTAMENTO = null, String XML_CAMPOS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            DepartamentoNegocio negocio = new DepartamentoNegocio();

//            return negocio.Obtener_M_DEPARTAMENTO(ID_DEPARTAMENTO,FG_ACTIVO,FE_INACTIVO,CL_DEPARTAMENTO,NB_DEPARTAMENTO,XML_CAMPOS_ADICIONALES,FE_CREACION, FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);

//        }

//        #endregion

//        #region C_USUARIO

//        public List<SPE_OBTIENE_C_USUARIO_Result> Get_C_USUARIO(String CL_USUARIO = null, String NB_USUARIO = null, String NB_CORREO_ELECTRONICO = null, String NB_PASSWORD = null, bool? FG_CAMBIAR_PASSWORD = null, String XML_PERSONALIZACION = null, int? ID_ROL = null, int? ID_EMPLEADO = null, bool? FG_ACTIVO = null, DateTime? FE_INACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            UsuarioNegocio negocio = new UsuarioNegocio();
//            return negocio.Obtener_C_USUARIO(CL_USUARIO, NB_USUARIO, NB_CORREO_ELECTRONICO, NB_PASSWORD, FG_CAMBIAR_PASSWORD, XML_PERSONALIZACION, ID_ROL, ID_EMPLEADO, FG_ACTIVO, FE_INACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_USUARIO(string tipo_transaccion, SPE_OBTIENE_C_USUARIO_Result V_C_USUARIO, string usuario, string programa)
//        {
//            UsuarioNegocio negocio = new UsuarioNegocio();
//            return negocio.InsertaActualiza_C_USUARIO(tipo_transaccion, V_C_USUARIO, usuario, programa);
//        }

//        public bool Delete_C_USUARIO(String CL_USUARIO = null, string usuario = null, string programa = null)
//        {
//            UsuarioNegocio negocio = new UsuarioNegocio();
//            return negocio.Elimina_C_USUARIO(CL_USUARIO, usuario, programa);

//        }

//        #endregion	

//        #region C_PUESTO_COMPETENCIA

//        public List<SPE_OBTIENE_C_PUESTO_COMPETENCIA_Result> Get_C_PUESTO_COMPETENCIA(int? ID_PUESTO_COMPETENCIA = null, int? ID_PUESTO = null, int? ID_COMPETENCIA = null, Decimal? ID_NIVEL_DESEADO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            PuestoCompetenciaNegocio negocio = new PuestoCompetenciaNegocio();
//            return negocio.Obtener_C_PUESTO_COMPETENCIA(ID_PUESTO_COMPETENCIA, ID_PUESTO, ID_COMPETENCIA, ID_NIVEL_DESEADO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_PUESTO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_C_PUESTO_COMPETENCIA_Result V_C_PUESTO_COMPETENCIA, string usuario, string programa)
//        {
//            PuestoCompetenciaNegocio negocio = new PuestoCompetenciaNegocio();
//            return negocio.InsertaActualiza_C_PUESTO_COMPETENCIA(tipo_transaccion, V_C_PUESTO_COMPETENCIA, usuario, programa);
//        }

//        public bool Delete_C_PUESTO_COMPETENCIA(int? ID_PUESTO_COMPETENCIA = null, string usuario = null, string programa = null)
//        {
//            PuestoCompetenciaNegocio negocio = new PuestoCompetenciaNegocio();
//            return negocio.Elimina_C_PUESTO_COMPETENCIA(ID_PUESTO_COMPETENCIA, usuario, programa);

//        }

//        #endregion	

//        #region C_PREGUNTA

//        public List<SPE_OBTIENE_C_PREGUNTA_Result> Get_C_PREGUNTA(int? ID_PREGUNTA = null, String CL_PREGUNTA = null, String NB_PREGUNTA = null, String DS_PREGUNTA = null, String CL_TIPO_PREGUNTA = null, Decimal? NO_VALOR = null, bool? FG_REQUERIDO = null, bool? FG_ACTIVO = null, int? ID_COMPETENCIA = null, int? ID_BITACORA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            PreguntaNegocio negocio = new PreguntaNegocio();
//            return negocio.Obtener_C_PREGUNTA(ID_PREGUNTA, CL_PREGUNTA, NB_PREGUNTA, DS_PREGUNTA, CL_TIPO_PREGUNTA, NO_VALOR, FG_REQUERIDO, FG_ACTIVO, ID_COMPETENCIA, ID_BITACORA, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_PREGUNTA(string tipo_transaccion, SPE_OBTIENE_C_PREGUNTA_Result V_C_PREGUNTA, string usuario, string programa)
//        {
//            PreguntaNegocio negocio = new PreguntaNegocio();
//            return negocio.InsertaActualiza_C_PREGUNTA(tipo_transaccion, V_C_PREGUNTA, usuario, programa);
//        }

//        public bool Delete_C_PREGUNTA(int? ID_PREGUNTA = null, string usuario = null, string programa = null)
//        {
//            PreguntaNegocio negocio = new PreguntaNegocio();
//            return negocio.Elimina_C_PREGUNTA(ID_PREGUNTA, usuario, programa);

//        }

//        #endregion	
        
//        #region S_TIPO_RELACION_PUESTO

//        public List<SPE_OBTIENE_S_TIPO_RELACION_PUESTO_Result> Get_S_TIPO_RELACION_PUESTO(String CL_TIPO_RELACION = null, String NB_TIPO_RELACION = null, String DS_TIPO_RELACION = null)
//        {
//            TipoRelacionNegocio negocio = new TipoRelacionNegocio();
//            return negocio.Obtener_S_TIPO_RELACION_PUESTO(CL_TIPO_RELACION, NB_TIPO_RELACION, DS_TIPO_RELACION);
//        }

//        public bool Insert_update_S_TIPO_RELACION_PUESTO(string tipo_transaccion, SPE_OBTIENE_S_TIPO_RELACION_PUESTO_Result V_S_TIPO_RELACION_PUESTO, string usuario, string programa)
//        {
//            TipoRelacionNegocio negocio = new TipoRelacionNegocio();
//            return negocio.InsertaActualiza_S_TIPO_RELACION_PUESTO(tipo_transaccion, V_S_TIPO_RELACION_PUESTO, usuario, programa);
//        }

//        public bool Delete_S_TIPO_RELACION_PUESTO(String CL_TIPO_RELACION = null, string usuario = null, string programa = null)
//        {
//            TipoRelacionNegocio negocio = new TipoRelacionNegocio();
//            return negocio.Elimina_S_TIPO_RELACION_PUESTO(CL_TIPO_RELACION, usuario, programa);

//        }

//        #endregion	

//        #region C_RESPUESTA

//        public List<SPE_OBTIENE_C_RESPUESTA_Result> Get_C_RESPUESTA(int? ID_RESPUESTA = null, String CL_RESPUESTA = null, String NB_RESPUESTA = null, String DS_RESPUESTA = null, Decimal? NO_VALOR = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            RespuestaNegocio negocio = new RespuestaNegocio();
//            return negocio.Obtener_C_RESPUESTA(ID_RESPUESTA, CL_RESPUESTA, NB_RESPUESTA, DS_RESPUESTA, NO_VALOR, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_RESPUESTA(string tipo_transaccion, SPE_OBTIENE_C_RESPUESTA_Result V_C_RESPUESTA, string usuario, string programa)
//        {
//            RespuestaNegocio negocio = new RespuestaNegocio();
//            return negocio.InsertaActualiza_C_RESPUESTA(tipo_transaccion, V_C_RESPUESTA, usuario, programa);
//        }

//        public bool Delete_C_RESPUESTA(int? ID_RESPUESTA = null, string usuario = null, string programa = null)
//        {
//            RespuestaNegocio negocio = new RespuestaNegocio();
//            return negocio.Elimina_C_RESPUESTA(ID_RESPUESTA, usuario, programa);

//        }

//        #endregion	

//        #region K_EVALUADO_PERIODO

//        public List<SPE_OBTIENE_K_EVALUADO_PERIODO_Result> Get_K_EVALUADO_PERIODO(int? ID_EVALAUDOR_PERIODO = null, int? ID_PERIODO = null, int? ID_EMPLEADO = null, int? ID_PUESTO = null, int? FG_PUESTO_ACTUAL = null, int? NO_CONSUMO_SUP = null, Decimal? MN_CUOTA_BASE = null, Decimal? MN_CUOTA_CONSUMO = null, Decimal? MN_CUOTA_ADICIONAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            EvaluadoPeriodoNegocio negocio = new EvaluadoPeriodoNegocio();
//            return negocio.Obtener_K_EVALUADO_PERIODO(ID_EVALAUDOR_PERIODO, ID_PERIODO, ID_EMPLEADO, ID_PUESTO, FG_PUESTO_ACTUAL, NO_CONSUMO_SUP, MN_CUOTA_BASE, MN_CUOTA_CONSUMO, MN_CUOTA_ADICIONAL, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_K_EVALUADO_PERIODO(string tipo_transaccion, SPE_OBTIENE_K_EVALUADO_PERIODO_Result V_K_EVALUADO_PERIODO, string usuario, string programa)
//        {
//            EvaluadoPeriodoNegocio negocio = new EvaluadoPeriodoNegocio();
//            return negocio.InsertaActualiza_K_EVALUADO_PERIODO(tipo_transaccion, V_K_EVALUADO_PERIODO, usuario, programa);
//        }

//        public bool Delete_K_EVALUADO_PERIODO(int? ID_EVALAUDOR_PERIODO = null, string usuario = null, string programa = null)
//        {
//            EvaluadoPeriodoNegocio negocio = new EvaluadoPeriodoNegocio();
//            return negocio.Elimina_K_EVALUADO_PERIODO(ID_EVALAUDOR_PERIODO, usuario, programa);

//        }

//        #endregion

//        #region K_EVALUADO_EVALUADOR

//        public List<SPE_OBTIENE_K_EVALUADO_EVALUADOR_Result> Get_K_EVALUADO_EVALUADOR(int? ID_EVALAUDO_EVALUADOR = null, int? ID_PERIODO = null, int? ID_EVALUADOR_PERIODO = null, int? ID_PUESTO_EVALUADOR = null, Decimal? NO_VALOR = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            EvaluadoEvaluadorNegocio negocio = new EvaluadoEvaluadorNegocio();
//            return negocio.Obtener_K_EVALUADO_EVALUADOR(ID_EVALAUDO_EVALUADOR, ID_PERIODO, ID_EVALUADOR_PERIODO, ID_PUESTO_EVALUADOR, NO_VALOR, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_K_EVALUADO_EVALUADOR(string tipo_transaccion, SPE_OBTIENE_K_EVALUADO_EVALUADOR_Result V_K_EVALUADO_EVALUADOR, string usuario, string programa)
//        {
//            EvaluadoEvaluadorNegocio negocio = new EvaluadoEvaluadorNegocio();
//            return negocio.InsertaActualiza_K_EVALUADO_EVALUADOR(tipo_transaccion, V_K_EVALUADO_EVALUADOR, usuario, programa);
//        }

//        public bool Delete_K_EVALUADO_EVALUADOR(int? ID_EVALAUDO_EVALUADOR = null, string usuario = null, string programa = null)
//        {
//            EvaluadoEvaluadorNegocio negocio = new EvaluadoEvaluadorNegocio();
//            return negocio.Elimina_K_EVALUADO_EVALUADOR(ID_EVALAUDO_EVALUADOR, usuario, programa);

//        }

//        #endregion	

//        #region K_CUESTIONARIO

//        public List<SPE_OBTIENE_K_CUESTIONARIO_Result> Get_K_CUESTIONARIO(int? ID_CUESTIONARIO = null, int? ID_EVALUADO = null, int? ID_EVALUADO_EVALUADOR = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            CuestionarioNegocio negocio = new CuestionarioNegocio();
//            return negocio.Obtener_K_CUESTIONARIO(ID_CUESTIONARIO, ID_EVALUADO, ID_EVALUADO_EVALUADOR, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_K_CUESTIONARIO(string tipo_transaccion, SPE_OBTIENE_K_CUESTIONARIO_Result V_K_CUESTIONARIO, string usuario, string programa)
//        {
//            CuestionarioNegocio negocio = new CuestionarioNegocio();
//            return negocio.InsertaActualiza_K_CUESTIONARIO(tipo_transaccion, V_K_CUESTIONARIO, usuario, programa);
//        }

//        public bool Delete_K_CUESTIONARIO(int? ID_CUESTIONARIO = null, string usuario = null, string programa = null)
//        {
//            CuestionarioNegocio negocio = new CuestionarioNegocio();
//            return negocio.Elimina_K_CUESTIONARIO(ID_CUESTIONARIO, usuario, programa);

//        }

//        #endregion

//        #region K_CUESTIONARIO_PREGUNTA

//        public List<SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_Result> Get_K_CUESTIONARIO_PREGUNTA(int? ID_CUESTIONARIO_PREGUNTA = null, int? ID_CUESTIONARIO = null, int? ID_PREGUNTA = null, String NB_PREGUNTA = null, String NB_RESPUESTA = null, Decimal? NO_VALOR_RESPUESTA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            CuestionarioPreguntaNegocio negocio = new CuestionarioPreguntaNegocio();
//            return negocio.Obtener_K_CUESTIONARIO_PREGUNTA(ID_CUESTIONARIO_PREGUNTA, ID_CUESTIONARIO, ID_PREGUNTA, NB_PREGUNTA, NB_RESPUESTA, NO_VALOR_RESPUESTA, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_K_CUESTIONARIO_PREGUNTA(string tipo_transaccion, SPE_OBTIENE_K_CUESTIONARIO_PREGUNTA_Result V_K_CUESTIONARIO_PREGUNTA, string usuario, string programa)
//        {
//            CuestionarioPreguntaNegocio negocio = new CuestionarioPreguntaNegocio();
//            return negocio.InsertaActualiza_K_CUESTIONARIO_PREGUNTA(tipo_transaccion, V_K_CUESTIONARIO_PREGUNTA, usuario, programa);
//        }

//        public bool Delete_K_CUESTIONARIO_PREGUNTA(int? ID_CUESTIONARIO_PREGUNTA = null, string usuario = null, string programa = null)
//        {
//            CuestionarioPreguntaNegocio negocio = new CuestionarioPreguntaNegocio();
//            return negocio.Elimina_K_CUESTIONARIO_PREGUNTA(ID_CUESTIONARIO_PREGUNTA, usuario, programa);

//        }

//        #endregion

//        #region K_EMPLEADO_COMPETENCIA

//        public List<SPE_OBTIENE_K_EMPLEADO_COMPETENCIA_Result> Get_K_EMPLEADO_COMPETENCIA(int? ID_EMPLEADO_COMPETENCIA = null, int? ID_EMPLEADO = null, int? ID_COMPETENCIA = null, Decimal? NO_CALIFICACION = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            EmpleadoCompetenciaNegocio negocio = new EmpleadoCompetenciaNegocio();
//            return negocio.Obtener_K_EMPLEADO_COMPETENCIA(ID_EMPLEADO_COMPETENCIA, ID_EMPLEADO, ID_COMPETENCIA, NO_CALIFICACION, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_K_EMPLEADO_COMPETENCIA(string tipo_transaccion, SPE_OBTIENE_K_EMPLEADO_COMPETENCIA_Result V_K_EMPLEADO_COMPETENCIA, string usuario, string programa)
//        {
//            EmpleadoCompetenciaNegocio negocio = new EmpleadoCompetenciaNegocio();
//            return negocio.InsertaActualiza_K_EMPLEADO_COMPETENCIA(tipo_transaccion, V_K_EMPLEADO_COMPETENCIA, usuario, programa);
//        }

//        public bool Delete_K_EMPLEADO_COMPETENCIA(int? ID_EMPLEADO_COMPETENCIA = null, string usuario = null, string programa = null)
//        {
//            EmpleadoCompetenciaNegocio negocio = new EmpleadoCompetenciaNegocio();
//            return negocio.Elimina_K_EMPLEADO_COMPETENCIA(ID_EMPLEADO_COMPETENCIA, usuario, programa);

//        }

//        #endregion
        
//        #region C_PUESTO_FUNCION

//        public List<SPE_OBTIENE_C_PUESTO_FUNCION_Result> Get_C_PUESTO_FUNCION(int? ID_PUESTO_FUNCION = null, String CL_PUESTO_FUNCION = null, String NB_PUESTO_FUNCION = null, String DS_PUESTO_FUNCION = null, int? ID_PUESTO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            PuestoFuncionNegocio negocio = new PuestoFuncionNegocio();
//            return negocio.Obtener_C_PUESTO_FUNCION(ID_PUESTO_FUNCION, CL_PUESTO_FUNCION, NB_PUESTO_FUNCION, DS_PUESTO_FUNCION, ID_PUESTO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_PUESTO_FUNCION(string tipo_transaccion, SPE_OBTIENE_C_PUESTO_FUNCION_Result V_C_PUESTO_FUNCION, string usuario, string programa)
//        {
//            PuestoFuncionNegocio negocio = new PuestoFuncionNegocio();
//            return negocio.InsertaActualiza_C_PUESTO_FUNCION(tipo_transaccion, V_C_PUESTO_FUNCION, usuario, programa);
//        }

//        public bool Delete_C_PUESTO_FUNCION(int? ID_PUESTO_FUNCION = null, string usuario = null, string programa = null)
//        {
//            PuestoFuncionNegocio negocio = new PuestoFuncionNegocio();
//            return negocio.Elimina_C_PUESTO_FUNCION(ID_PUESTO_FUNCION, usuario, programa);

//        }

//        #endregion

//        #region C_PUESTO_RELACIONADO

//        public List<SPE_OBTIENE_C_PUESTO_RELACIONADO_Result> Get_C_PUESTO_RELACIONADO(int?  ID_PUESTO = null,int?  ID_PUESTO_RELACIONADO = null,String CL_TIPO_RELACION = null,String DS_PUESTO_RELACIONADO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
//        {
//            PuestoRelacionadoNegocio negocio = new PuestoRelacionadoNegocio();
//            return negocio.Obtener_C_PUESTO_RELACIONADO(ID_PUESTO,ID_PUESTO_RELACIONADO,CL_TIPO_RELACION,DS_PUESTO_RELACIONADO,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_PUESTO_RELACIONADO (string tipo_transaccion, SPE_OBTIENE_C_PUESTO_RELACIONADO_Result V_C_PUESTO_RELACIONADO,string usuario, string programa)
//        {
//            PuestoRelacionadoNegocio negocio = new PuestoRelacionadoNegocio();
//            return negocio.InsertaActualiza_C_PUESTO_RELACIONADO(tipo_transaccion,V_C_PUESTO_RELACIONADO, usuario,programa);
//        }

//        public bool Delete_C_PUESTO_RELACIONADO (SPE_OBTIENE_C_PUESTO_RELACIONADO_Result V_C_PUESTO_RELACIONADO,string usuario = null, string programa = null)
//        {
//            PuestoRelacionadoNegocio negocio = new PuestoRelacionadoNegocio();
//            return negocio.Elimina_C_PUESTO_RELACIONADO(V_C_PUESTO_RELACIONADO, usuario, programa);
            
//        }

//#endregion	
        
//        #region C_EMPLEADO_RELACIONADO

//        public List<SPE_OBTIENE_C_EMPLEADO_RELACIONADO_Result> Get_C_EMPLEADO_RELACIONADO(int?  ID_EMPLEADO = null,int?  ID_EMPLEADO_RELACIONADO = null,String CL_TIPO_RELACION = null,String DS_EMPLEADO_RELACIONADO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
//        {
//            EmpleadoRelacionadoNegocio negocio = new EmpleadoRelacionadoNegocio();
//            return negocio.Obtener_C_EMPLEADO_RELACIONADO(ID_EMPLEADO,ID_EMPLEADO_RELACIONADO,CL_TIPO_RELACION,DS_EMPLEADO_RELACIONADO,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_EMPLEADO_RELACIONADO (string tipo_transaccion, SPE_OBTIENE_C_EMPLEADO_RELACIONADO_Result V_C_EMPLEADO_RELACIONADO,string usuario, string programa)
//        {
//            EmpleadoRelacionadoNegocio negocio = new EmpleadoRelacionadoNegocio();
//            return negocio.InsertaActualiza_C_EMPLEADO_RELACIONADO(tipo_transaccion,V_C_EMPLEADO_RELACIONADO, usuario,programa);
//        }

//        public bool Delete_C_EMPLEADO_RELACIONADO (SPE_OBTIENE_C_EMPLEADO_RELACIONADO_Result V_C_EMPLEADO_RELACIONADO,string usuario = null, string programa = null)
//        {
//            EmpleadoRelacionadoNegocio negocio = new EmpleadoRelacionadoNegocio();
//            return negocio.Elimina_C_EMPLEADO_RELACIONADO(V_C_EMPLEADO_RELACIONADO, usuario, programa);
            
//        }

//#endregion	
        
//        #region C_GRUPO_PREGUNTA

//        public List<SPE_OBTIENE_C_GRUPO_PREGUNTA_Result> Get_C_GRUPO_PREGUNTA(int?  ID_GRUPO_PREGUNTA = null,String CL_GRUPO_PREGUNTA = null,String NB_GRUPO_PREGUNTA = null,int?  ID_PREGUNTA = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
//        {
//            GrupoPreguntaNegocio negocio = new GrupoPreguntaNegocio();
//            return negocio.Obtener_C_GRUPO_PREGUNTA(ID_GRUPO_PREGUNTA,CL_GRUPO_PREGUNTA,NB_GRUPO_PREGUNTA,ID_PREGUNTA,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_GRUPO_PREGUNTA (string tipo_transaccion, SPE_OBTIENE_C_GRUPO_PREGUNTA_Result V_C_GRUPO_PREGUNTA,string usuario, string programa)
//        {
//            GrupoPreguntaNegocio negocio = new GrupoPreguntaNegocio();
//            return negocio.InsertaActualiza_C_GRUPO_PREGUNTA(tipo_transaccion,V_C_GRUPO_PREGUNTA, usuario,programa);
//        }

//        public bool Delete_C_GRUPO_PREGUNTA (SPE_OBTIENE_C_GRUPO_PREGUNTA_Result V_C_GRUPO_PREGUNTA,string usuario = null, string programa = null)
//        {
//            GrupoPreguntaNegocio negocio = new GrupoPreguntaNegocio();
//            return negocio.Elimina_C_GRUPO_PREGUNTA(V_C_GRUPO_PREGUNTA, usuario, programa);
            
//        }

//#endregion	
        
//        #region C_AREA_INTERES

//        public List<SPE_OBTIENE_C_AREA_INTERES_Result> Get_C_AREA_INTERES(int?  ID_AREA_INTERES = null,String CL_AREA_INTERES = null,String NB_AREA_INTERES = null,bool?  FG_ACTIVO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
//        {
//            AreaInteresNegocio negocio = new AreaInteresNegocio();
//            return negocio.Obtener_C_AREA_INTERES(ID_AREA_INTERES,CL_AREA_INTERES,NB_AREA_INTERES,FG_ACTIVO,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_AREA_INTERES (string tipo_transaccion, SPE_OBTIENE_C_AREA_INTERES_Result V_C_AREA_INTERES,string usuario, string programa)
//        {
//            AreaInteresNegocio negocio = new AreaInteresNegocio();
//            return negocio.InsertaActualiza_C_AREA_INTERES(tipo_transaccion,V_C_AREA_INTERES, usuario,programa);
//        }

//        public bool Delete_C_AREA_INTERES(int? ID_AREA_INTERES = null,string CL_AREA_INTERES=null, string usuario = null, string programa = null)
//        {
//            AreaInteresNegocio negocio = new AreaInteresNegocio();
//            return negocio.Elimina_C_AREA_INTERES(ID_AREA_INTERES,CL_AREA_INTERES, usuario, programa);
//        }

//#endregion	

//        #region K_EXPERIENCIA_LABORAL

//        public List<SPE_OBTIENE_K_EXPERIENCIA_LABORAL_Result> Get_K_EXPERIENCIA_LABORAL(int? ID_EXPERIENCIA_LABORAL = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, String NB_EMPRESA = null, String DS_DOMICILIO = null, String NB_GIRO = null, DateTime? FE_INICIO = null, DateTime? FE_FIN = null, String NB_PUESTO = null, String NB_FUNCION = null, String DS_FUNCIONES = null, Decimal? MN_PRIMER_SUELDO = null, Decimal? MN_ULTIMO_SUELDO = null, String CL_TIPO_CONTRATO = null, String CL_TIPO_CONTRATO_OTRO = null, String NO_TELEFONO_CONTACTO = null, String CL_CORREO_ELECTRONICO = null, String NB_CONTACTO = null, String NB_PUESTO_CONTACTO = null, bool? CL_INFORMACION_CONFIRMADA = null, String DS_COMENTARIOS = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            ExperienciaLaboralNegocio negocio = new ExperienciaLaboralNegocio();
//            return negocio.Obtener_K_EXPERIENCIA_LABORAL(ID_EXPERIENCIA_LABORAL, ID_CANDIDATO, ID_EMPLEADO, NB_EMPRESA, DS_DOMICILIO, NB_GIRO, FE_INICIO, FE_FIN, NB_PUESTO, NB_FUNCION, DS_FUNCIONES, MN_PRIMER_SUELDO, MN_ULTIMO_SUELDO, CL_TIPO_CONTRATO, CL_TIPO_CONTRATO_OTRO, NO_TELEFONO_CONTACTO, CL_CORREO_ELECTRONICO, NB_CONTACTO, NB_PUESTO_CONTACTO, CL_INFORMACION_CONFIRMADA, DS_COMENTARIOS, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_K_EXPERIENCIA_LABORAL(E_EXPERIENCIA_LABORAL ULTIMO, E_EXPERIENCIA_LABORAL ANT1, E_EXPERIENCIA_LABORAL ANT2, E_EXPERIENCIA_LABORAL ANT3, string usuario, string programa, string archivo)
//        {
//            ExperienciaLaboralNegocio negocio = new ExperienciaLaboralNegocio();

//            var xmlNuevo = new XElement("EXPERIENCIA");


//            if (ULTIMO != null)
//            {
//                var xmlUltimo = new XElement("TRABAJO",
//                    new XAttribute("ID_EXPERIENCIA_LABORAL", (ULTIMO.ID_EXPERIENCIA_LABORAL ?? 0)),
//                    new XAttribute("ID_CANDIDATO", (ULTIMO.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (ULTIMO.ID_EMPLEADO ?? 0)),
//                    new XAttribute("NB_EMPRESA", ULTIMO.NB_EMPRESA),
//                    new XAttribute("NB_GIRO", ULTIMO.NB_GIRO),
//                    new XAttribute("FE_INICIO", new DateTime(int.Parse(ULTIMO.FE_INICIO_ANIO), int.Parse(ULTIMO.FE_INICIO_MES), 1)),
//                    new XAttribute("FE_FIN", new DateTime(int.Parse(ULTIMO.FE_FIN_ANIO), int.Parse(ULTIMO.FE_FIN_MES), 1)),
//                    new XAttribute("NB_PUESTO", (ULTIMO.NB_PUESTO ?? "")),
//                    new XAttribute("NB_FUNCION", (ULTIMO.NB_FUNCION ?? "") ),
//                    new XAttribute("DS_FUNCIONES", (ULTIMO.DS_FUNCIONES ?? "")),
//                    new XAttribute("MN_PRIMER_SUELDO", (ULTIMO.MN_PRIMER_SUELDO ?? 0)),
//                    new XAttribute("MN_ULTIMO_SUELDO", (ULTIMO.MN_ULTIMO_SUELDO ?? 0)),
//                    new XAttribute("CL_TIPO_CONTRATO", (ULTIMO.CL_TIPO_CONTRATO ?? "")),
//                    new XAttribute("CL_TIPO_CONTRATO_OTRO", (ULTIMO.CL_TIPO_CONTRATO_OTRO ?? "")),
//                    new XAttribute("NO_TELEFONO_CONTACTO", (ULTIMO.NO_TELEFONO_CONTACTO ?? "")),
//                    new XAttribute("CL_CORREO_ELECTRONICO", (ULTIMO.CL_CORREO_ELECTRONICO ?? "")),
//                    new XAttribute("NB_CONTACTO", (ULTIMO.NB_CONTACTO ?? "")),
//                    new XAttribute("NB_PUESTO_CONTACTO", (ULTIMO.NB_PUESTO_CONTACTO ?? "")),
//                    new XAttribute("DS_COMENTARIOS", (ULTIMO.DS_COMENTARIOS ?? ""))
//                );

//                xmlNuevo.Add(xmlUltimo);
//            }

//            if (ANT1 != null)
//            {
//                var xmlAnt1 = new XElement("TRABAJO",
//                    new XAttribute("ID_EXPERIENCIA_LABORAL", (ANT1.ID_EXPERIENCIA_LABORAL ?? 0)),
//                    new XAttribute("ID_CANDIDATO", (ANT1.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (ANT1.ID_EMPLEADO ?? 0)),
//                    new XAttribute("NB_EMPRESA", ANT1.NB_EMPRESA),
//                    new XAttribute("NB_GIRO", ANT1.NB_GIRO),
//                    new XAttribute("FE_INICIO", new DateTime(int.Parse(ANT1.FE_INICIO_ANIO), int.Parse(ANT1.FE_INICIO_MES), 1)),
//                    new XAttribute("FE_FIN", new DateTime(int.Parse(ANT1.FE_FIN_ANIO), int.Parse(ANT1.FE_FIN_MES), 1)),
//                    new XAttribute("NB_PUESTO", (ANT1.NB_PUESTO ?? "")),
//                    new XAttribute("NB_FUNCION", (ANT1.NB_FUNCION ?? "")),
//                    new XAttribute("DS_FUNCIONES", (ANT1.DS_FUNCIONES ?? "")),
//                    new XAttribute("MN_PRIMER_SUELDO", (ANT1.MN_PRIMER_SUELDO ?? 0)),
//                    new XAttribute("MN_ULTIMO_SUELDO", (ANT1.MN_ULTIMO_SUELDO ?? 0)),
//                    new XAttribute("CL_TIPO_CONTRATO", (ANT1.CL_TIPO_CONTRATO ?? "")),
//                    new XAttribute("CL_TIPO_CONTRATO_OTRO", (ANT1.CL_TIPO_CONTRATO_OTRO ?? "")),
//                    new XAttribute("NO_TELEFONO_CONTACTO", (ANT1.NO_TELEFONO_CONTACTO ?? "")),
//                    new XAttribute("CL_CORREO_ELECTRONICO", (ANT1.CL_CORREO_ELECTRONICO ?? "")),
//                    new XAttribute("NB_CONTACTO", (ANT1.NB_CONTACTO ?? "")),
//                    new XAttribute("NB_PUESTO_CONTACTO", (ANT1.NB_PUESTO_CONTACTO ?? "")),
//                    new XAttribute("DS_COMENTARIOS", (ANT1.DS_COMENTARIOS ?? ""))
//                );

//                xmlNuevo.Add(xmlAnt1);
//            }

//            if (ANT2 != null)
//            {
//                var xmlAnt2 = new XElement("TRABAJO",
//                    new XAttribute("ID_EXPERIENCIA_LABORAL", (ANT2.ID_EXPERIENCIA_LABORAL ?? 0)),
//                    new XAttribute("ID_CANDIDATO", (ANT2.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (ANT2.ID_EMPLEADO ?? 0)),
//                    new XAttribute("NB_EMPRESA", ANT2.NB_EMPRESA),
//                    new XAttribute("NB_GIRO", ANT2.NB_GIRO),
//                    new XAttribute("FE_INICIO", new DateTime(int.Parse(ANT2.FE_INICIO_ANIO), int.Parse(ANT2.FE_INICIO_MES), 1)),
//                    new XAttribute("FE_FIN", new DateTime(int.Parse(ANT2.FE_FIN_ANIO), int.Parse(ANT2.FE_FIN_MES), 1)),
//                    new XAttribute("NB_PUESTO", (ANT2.NB_PUESTO ?? "")),
//                    new XAttribute("NB_FUNCION", (ANT2.NB_FUNCION ?? "")),
//                    new XAttribute("DS_FUNCIONES", (ANT2.DS_FUNCIONES ?? "")),
//                    new XAttribute("MN_PRIMER_SUELDO", (ANT2.MN_PRIMER_SUELDO ?? 0)),
//                    new XAttribute("MN_ULTIMO_SUELDO", (ANT2.MN_ULTIMO_SUELDO ?? 0)),
//                    new XAttribute("CL_TIPO_CONTRATO", (ANT2.CL_TIPO_CONTRATO ?? "")),
//                    new XAttribute("CL_TIPO_CONTRATO_OTRO", (ANT2.CL_TIPO_CONTRATO_OTRO ?? "")),
//                    new XAttribute("NO_TELEFONO_CONTACTO", (ANT2.NO_TELEFONO_CONTACTO ?? "")),
//                    new XAttribute("CL_CORREO_ELECTRONICO", (ANT2.CL_CORREO_ELECTRONICO ?? "")),
//                    new XAttribute("NB_CONTACTO", (ANT2.NB_CONTACTO ?? "")),
//                    new XAttribute("NB_PUESTO_CONTACTO", (ANT2.NB_PUESTO_CONTACTO ?? "")),
//                    new XAttribute("DS_COMENTARIOS", (ANT2.DS_COMENTARIOS ?? ""))
//                );

//                xmlNuevo.Add(xmlAnt2);
//            }

//            if (ANT3 != null)
//            {
//                var xmlAnt3 = new XElement("TRABAJO",
//                    new XAttribute("ID_EXPERIENCIA_LABORAL", (ANT3.ID_EXPERIENCIA_LABORAL ?? 0)),
//                    new XAttribute("ID_CANDIDATO", (ANT3.ID_CANDIDATO ?? 0)),
//                    new XAttribute("ID_EMPLEADO", (ANT3.ID_EMPLEADO ?? 0)),
//                    new XAttribute("NB_EMPRESA", ANT3.NB_EMPRESA),
//                    new XAttribute("NB_GIRO", ANT3.NB_GIRO),
//                    new XAttribute("FE_INICIO", new DateTime(int.Parse(ANT3.FE_INICIO_ANIO), int.Parse(ANT3.FE_INICIO_MES), 1)),
//                    new XAttribute("FE_FIN", new DateTime(int.Parse(ANT3.FE_FIN_ANIO), int.Parse(ANT3.FE_FIN_MES), 1)),
//                    new XAttribute("NB_PUESTO", (ANT3.NB_PUESTO ?? "")),
//                    new XAttribute("NB_FUNCION", (ANT3.NB_FUNCION ?? "")),
//                    new XAttribute("DS_FUNCIONES", (ANT3.DS_FUNCIONES ?? "")),
//                    new XAttribute("MN_PRIMER_SUELDO", (ANT3.MN_PRIMER_SUELDO ?? 0)),
//                    new XAttribute("MN_ULTIMO_SUELDO", (ANT3.MN_ULTIMO_SUELDO ?? 0)),
//                    new XAttribute("CL_TIPO_CONTRATO", (ANT3.CL_TIPO_CONTRATO ?? "")),
//                    new XAttribute("CL_TIPO_CONTRATO_OTRO", (ANT3.CL_TIPO_CONTRATO_OTRO ?? "")),
//                    new XAttribute("NO_TELEFONO_CONTACTO", (ANT3.NO_TELEFONO_CONTACTO ?? "")),
//                    new XAttribute("CL_CORREO_ELECTRONICO", (ANT3.CL_CORREO_ELECTRONICO ?? "")),
//                    new XAttribute("NB_CONTACTO", (ANT3.NB_CONTACTO ?? "")),
//                    new XAttribute("NB_PUESTO_CONTACTO", (ANT3.NB_PUESTO_CONTACTO ?? "")),
//                    new XAttribute("DS_COMENTARIOS", (ANT3.DS_COMENTARIOS ?? ""))
//                );

//                xmlNuevo.Add(xmlAnt3);
//            }

//            return negocio.InsertaActualiza_K_EXPERIENCIA_LABORAL(usuario, programa, xmlNuevo.ToString());
//        }
        
//        public bool Delete_K_EXPERIENCIA_LABORAL (int?  ID_EXPERIENCIA_LABORAL = null,string usuario = null, string programa = null)
//        {
//            ExperienciaLaboralNegocio negocio = new ExperienciaLaboralNegocio();
//            return negocio.Elimina_K_EXPERIENCIA_LABORAL(ID_EXPERIENCIA_LABORAL, usuario, programa);
            
//        }

//        #endregion	

//        #region K_AREA_INTERES

//        public List<SPE_OBTIENE_K_AREA_INTERES_Result> Get_K_AREA_INTERES(int? ID_CANDIDATO_AREA_INTERES = null, int? ID_CANDIDATO = null, int? ID_AREA_INTERES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            AreaInteresCandidatoNegocio negocio = new AreaInteresCandidatoNegocio();
//            return negocio.Obtener_K_AREA_INTERES(ID_CANDIDATO_AREA_INTERES, ID_CANDIDATO, ID_AREA_INTERES, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_K_AREA_INTERES(string tipo_transaccion, SPE_OBTIENE_K_AREA_INTERES_Result V_K_AREA_INTERES, string usuario, string programa)
//        {
//            AreaInteresCandidatoNegocio negocio = new AreaInteresCandidatoNegocio();
//            return negocio.InsertaActualiza_K_AREA_INTERES(tipo_transaccion, V_K_AREA_INTERES, usuario, programa);
//        }

//        public bool Delete_K_AREA_INTERES(int? ID_CANDIDATO_AREA_INTERES = null, string usuario = null, string programa = null)
//        {
//            AreaInteresCandidatoNegocio negocio = new AreaInteresCandidatoNegocio();
//            return negocio.Elimina_K_AREA_INTERES(ID_CANDIDATO_AREA_INTERES, usuario, programa);

//        }

//        #endregion	
        
//        #region C_CLASIFICACION_COMPETENCIA

//        public List<SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result> Get_C_CLASIFICACION_COMPETENCIA(int?  ID_CLASIFICACION_COMPETENCIA = null,String CL_CLASIFICACION = null,String CL_TIPO_COMPETENCIA = null,String NB_CLASIFICACION_COMPETENCIA = null,String DS_CLASIFICACION_COMPETENCIA = null,String DS_NOTAS_CLASIFICACION = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null, bool? FG_ACTIVO = null)
//        {
//            ClasificacionCompetenciaNegocio negocio = new ClasificacionCompetenciaNegocio();
//            return negocio.Obtener_C_CLASIFICACION_COMPETENCIA(ID_CLASIFICACION_COMPETENCIA,CL_CLASIFICACION,CL_TIPO_COMPETENCIA,NB_CLASIFICACION_COMPETENCIA,DS_CLASIFICACION_COMPETENCIA,DS_NOTAS_CLASIFICACION,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA,FG_ACTIVO);
//        }

//        public bool Insert_update_C_CLASIFICACION_COMPETENCIA (string tipo_transaccion, SPE_OBTIENE_C_CLASIFICACION_COMPETENCIA_Result V_C_CLASIFICACION_COMPETENCIA,string usuario, string programa)
//        {
//            ClasificacionCompetenciaNegocio negocio = new ClasificacionCompetenciaNegocio();
//            return negocio.InsertaActualiza_C_CLASIFICACION_COMPETENCIA(tipo_transaccion,V_C_CLASIFICACION_COMPETENCIA, usuario,programa);
//        }

//        public bool Delete_C_CLASIFICACION_COMPETENCIA (int?  ID_CLASIFICACION_COMPETENCIA = null,string usuario = null, string programa = null)
//        {
//            ClasificacionCompetenciaNegocio negocio = new ClasificacionCompetenciaNegocio();
//            return negocio.Elimina_C_CLASIFICACION_COMPETENCIA(ID_CLASIFICACION_COMPETENCIA, usuario, programa);
            
//        }

//        #endregion	

//        #region S_CATALOGO_TIPO

//        public List<SPE_OBTIENE_S_CATALOGO_TIPO_Result> Get_S_CATALOGO_TIPO(int? ID_CATALOGO_TIPO = null, String NB_CATALOGO_TIPO = null, String DS_CATALOGO_TIPO = null)
//        {
//            CatalogoTipoNegocio negocio = new CatalogoTipoNegocio();
//            return negocio.Obtener_S_CATALOGO_TIPO(ID_CATALOGO_TIPO, NB_CATALOGO_TIPO, DS_CATALOGO_TIPO);
//        }

//        public bool Insert_update_S_CATALOGO_TIPO(string tipo_transaccion, SPE_OBTIENE_S_CATALOGO_TIPO_Result V_S_CATALOGO_TIPO, string usuario, string programa)
//        {
//            CatalogoTipoNegocio negocio = new CatalogoTipoNegocio();
//            return negocio.InsertaActualiza_S_CATALOGO_TIPO(tipo_transaccion, V_S_CATALOGO_TIPO, usuario, programa);
//        }

//        public bool Delete_S_CATALOGO_TIPO(int? ID_CATALOGO_TIPO = null, string usuario = null, string programa = null)
//        {
//            CatalogoTipoNegocio negocio = new CatalogoTipoNegocio();
//            return negocio.Elimina_S_CATALOGO_TIPO(ID_CATALOGO_TIPO, usuario, programa);

//        }

//        #endregion	

//        #region C_CATALOGO_LISTA

//        public List<SPE_OBTIENE_C_CATALOGO_LISTA_Result> Get_C_CATALOGO_LISTA(int? ID_CATALOGO_LISTA = null, String NB_CATALOGO_LISTA = null, String DS_CATALOGO_LISTA = null, int? ID_CATALOGO_TIPO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            CatalogoListaNegocio negocio = new CatalogoListaNegocio();
//            return negocio.Obtener_C_CATALOGO_LISTA(ID_CATALOGO_LISTA, NB_CATALOGO_LISTA, DS_CATALOGO_LISTA, ID_CATALOGO_TIPO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_CATALOGO_LISTA(string tipo_transaccion, SPE_OBTIENE_C_CATALOGO_LISTA_Result V_C_CATALOGO_LISTA, string usuario, string programa)
//        {
//            CatalogoListaNegocio negocio = new CatalogoListaNegocio();
//            return negocio.InsertaActualiza_C_CATALOGO_LISTA(tipo_transaccion, V_C_CATALOGO_LISTA, usuario, programa);
//        }

//        public bool Delete_C_CATALOGO_LISTA(int? ID_CATALOGO_LISTA = null, string usuario = null, string programa = null)
//        {
//            CatalogoListaNegocio negocio = new CatalogoListaNegocio();
//            return negocio.Elimina_C_CATALOGO_LISTA(ID_CATALOGO_LISTA, usuario, programa);

//        }

//        #endregion	

//        #region C_CATALOGO_VALOR

//        public List<SPE_OBTIENE_C_CATALOGO_VALOR_Result> Get_C_CATALOGO_VALOR(int? ID_CATALOGO_VALOR = null, String CL_CATALOGO_VALOR = null, String NB_CATALOGO_VALOR = null, String DS_CATALOGO_VALOR = null, int? ID_CATALOGO_LISTA = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            CatalogoValorNegocio negocio = new CatalogoValorNegocio();
//            return negocio.Obtener_C_CATALOGO_VALOR(ID_CATALOGO_VALOR, CL_CATALOGO_VALOR, NB_CATALOGO_VALOR, DS_CATALOGO_VALOR, ID_CATALOGO_LISTA, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_CATALOGO_VALOR(string tipo_transaccion, SPE_OBTIENE_C_CATALOGO_VALOR_Result V_C_CATALOGO_VALOR, string usuario, string programa)
//        {
//            CatalogoValorNegocio negocio = new CatalogoValorNegocio();
//            return negocio.InsertaActualiza_C_CATALOGO_VALOR(tipo_transaccion, V_C_CATALOGO_VALOR, usuario, programa);
//        }

//        public bool Delete_C_CATALOGO_VALOR(int? ID_CATALOGO_VALOR = null, string usuario = null, string programa = null)
//        {
//            CatalogoValorNegocio negocio = new CatalogoValorNegocio();
//            return negocio.Elimina_C_CATALOGO_VALOR(ID_CATALOGO_VALOR, usuario, programa);

//        }

//        #endregion	

//        #region K_REQUISICION

//        public List<SPE_OBTIENE_K_REQUISICION_Result> Get_K_REQUISICION(int?  ID_REQUISICION = null,String NO_REQUISICION = null,DateTime?  FE_SOLICITUD = null,int?  ID_PUESTO = null,String CL_ESTADO = null,String CL_CAUSA = null,String DS_CAUSA = null,int?  ID_NOTIFICACION = null,int?  ID_SOLICITANTE = null,int?  ID_AUTORIZA = null,int?  ID_VISTO_BUENO = null,int?  ID_EMPRESA = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
//        {
//            RequisicionNegocio negocio = new RequisicionNegocio();
//            return negocio.Obtener_K_REQUISICION(ID_REQUISICION,NO_REQUISICION,FE_SOLICITUD,ID_PUESTO,CL_ESTADO,CL_CAUSA,DS_CAUSA,ID_NOTIFICACION,ID_SOLICITANTE,ID_AUTORIZA,ID_VISTO_BUENO,ID_EMPRESA,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_K_REQUISICION (string tipo_transaccion, SPE_OBTIENE_K_REQUISICION_Result V_K_REQUISICION,string usuario, string programa)
//        {
//            RequisicionNegocio negocio = new RequisicionNegocio();
//            return negocio.InsertaActualiza_K_REQUISICION(tipo_transaccion,V_K_REQUISICION, usuario,programa);
//        }

//        public bool Delete_K_REQUISICION (int?  ID_REQUISICION = null,string usuario = null, string programa = null)
//        {
//            RequisicionNegocio negocio = new RequisicionNegocio();
//            return negocio.Elimina_K_REQUISICION(ID_REQUISICION, usuario, programa);
            
//        }

//    #endregion	

//        #region C_EMPRESA

//        public List<SPE_OBTIENE_C_EMPRESA_Result> Get_C_EMPRESA(int? ID_EMPRESA = null, String CL_EMPRESA = null, String NB_EMPRESA = null, String NB_RAZON_SOCIAL = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            EmpresaNegocio negocio = new EmpresaNegocio();
//            return negocio.Obtener_C_EMPRESA(ID_EMPRESA, CL_EMPRESA, NB_EMPRESA, NB_RAZON_SOCIAL, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_EMPRESA(string tipo_transaccion, SPE_OBTIENE_C_EMPRESA_Result V_C_EMPRESA, string usuario, string programa)
//        {
//            EmpresaNegocio negocio = new EmpresaNegocio();
//            return negocio.InsertaActualiza_C_EMPRESA(tipo_transaccion, V_C_EMPRESA, usuario, programa);
//        }

//        public bool Delete_C_EMPRESA(int? ID_EMPRESA = null, string usuario = null, string programa = null)
//        {
//            EmpresaNegocio negocio = new EmpresaNegocio();
//            return negocio.Elimina_C_EMPRESA(ID_EMPRESA, usuario, programa);

//        }

//        #endregion	

//        #region C_PARIENTE

//        public List<SPE_OBTIENE_C_PARIENTE_Result> Get_C_PARIENTE(int? ID_PARIENTE = null, String NB_PARIENTE = null, String CL_PARENTEZCO = null, String CL_GENERO = null, DateTime? FE_NACIMIENTO = null, int? ID_EMPLEADO = null, int? ID_CANDIDATO = null, int? ID_BITACORA = null, String CL_OCUPACION = null, bool? FG_DEPENDIENTE = null, bool? FG_ACTIVO = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            ParienteNegocio negocio = new ParienteNegocio();
//            return negocio.Obtener_C_PARIENTE(ID_PARIENTE, NB_PARIENTE, CL_PARENTEZCO, CL_GENERO, FE_NACIMIENTO, ID_EMPLEADO, ID_CANDIDATO, ID_BITACORA, CL_OCUPACION, FG_DEPENDIENTE, FG_ACTIVO, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_PARIENTE(string tipo_transaccion, SPE_OBTIENE_C_PARIENTE_Result V_C_PARIENTE, string usuario, string programa, string fecha)
//        {
//            ParienteNegocio negocio = new ParienteNegocio();

//            V_C_PARIENTE.FE_NACIMIENTO = DateTime.Parse(fecha);

//            return negocio.InsertaActualiza_C_PARIENTE(tipo_transaccion, V_C_PARIENTE, usuario, programa);
//        }

//        public bool Delete_C_PARIENTE(int? ID_PARIENTE = null, string usuario = null, string programa = null)
//        {
//            ParienteNegocio negocio = new ParienteNegocio();
//            return negocio.Elimina_C_PARIENTE(ID_PARIENTE, usuario, programa);

//        }

//        #endregion	
       
//        #region K_SOLICITUD

//        public List<SPE_OBTIENE_K_SOLICITUD_Result> Get_K_SOLICITUD(int? ID_SOLICITUD = null, int? ID_CANDIDATO = null, int? ID_EMPLEADO = null, int? ID_DESCRIPTIVO = null, int? ID_REQUISICION = null, String CL_SOLICITUD = null, String CL_ACCESO_EVALUACION = null, int? ID_PLANTILLA_SOLICITUD = null, String DS_COMPETENCIAS_ADICIONALES = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            SolicitudNegocio negocio = new SolicitudNegocio();
//            return negocio.Obtener_K_SOLICITUD(ID_SOLICITUD, ID_CANDIDATO, ID_EMPLEADO, ID_DESCRIPTIVO, ID_REQUISICION, CL_SOLICITUD, CL_ACCESO_EVALUACION, ID_PLANTILLA_SOLICITUD, DS_COMPETENCIAS_ADICIONALES, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_K_SOLICITUD(string tipo_transaccion, SPE_OBTIENE_K_SOLICITUD_Result V_K_SOLICITUD, string usuario, string programa)
//        {
//            SolicitudNegocio negocio = new SolicitudNegocio();
//            return negocio.InsertaActualiza_K_SOLICITUD(tipo_transaccion, V_K_SOLICITUD, usuario, programa);
//        }

//        public bool Delete_K_SOLICITUD(int? ID_SOLICITUD = null, string usuario = null, string programa = null)
//        {
//            SolicitudNegocio negocio = new SolicitudNegocio();
//            return negocio.Elimina_K_SOLICITUD(ID_SOLICITUD, usuario, programa);

//        }

//        #endregion	

//        #region VW_PARENTEZCO

//        public List<SPE_OBTIENE_VW_PARENTEZCO_Result> Get_VW_PARENTEZCO(String NB_PARENTEZCO = null)
//        {
//            VistaParentezcoNegocio negocio = new VistaParentezcoNegocio();
//            return negocio.Obtener_VW_PARENTEZCO(NB_PARENTEZCO);
//        }

//        public bool Insert_update_VW_PARENTEZCO (string tipo_transaccion, SPE_OBTIENE_VW_PARENTEZCO_Result V_VW_PARENTEZCO,string usuario, string programa)
//        {
//            VistaParentezcoNegocio negocio = new VistaParentezcoNegocio();
//            return negocio.InsertaActualiza_VW_PARENTEZCO(tipo_transaccion,V_VW_PARENTEZCO, usuario,programa);
//        }

//        public bool Delete_VW_PARENTEZCO ( SPE_OBTIENE_VW_PARENTEZCO_Result V_VW_PARENTEZCO,string usuario = null, string programa = null)
//        {
//            VistaParentezcoNegocio negocio = new VistaParentezcoNegocio();
//            return negocio.Elimina_VW_PARENTEZCO(V_VW_PARENTEZCO, usuario, programa);
            
//        }

//    #endregion	

//        #region VW_OBTIENE_MESES

//        public List<SPE_OBTIENE_VW_OBTIENE_MESES_Result> Get_VW_OBTIENE_MESES(int?  NUMERO = null,String MES = null)
//        {
//            MesesNegocio negocio = new MesesNegocio();
//            return negocio.Obtener_VW_OBTIENE_MESES(NUMERO,MES);
//        }

//    #endregion	

//        #region

//        public List<SP_OBTIENE_ANIOS_Result> Get_OBTIENE_ANIO()
//        {
//            MesesNegocio negocio = new MesesNegocio();
//            return negocio.Obtener_OBTIENE_ANIOS();
//        }

//        #endregion

//        #region C_CANDIDATO

//        public List<SPE_OBTIENE_C_CANDIDATO_Result> Get_C_CANDIDATO(int?  ID_CANDIDATO = null,String NB_CANDIDATO = null,String NB_APELLIDO_PATERNO = null,String NB_APELLIDO_MATERNO = null,String CL_GENERO = null,String CL_RFC = null,String CL_CURP = null,String CL_ESTADO_CIVIL = null,String NB_CONYUGUE = null,String CL_NSS = null,String CL_TIPO_SANGUINEO = null,String NB_PAIS = null,String NB_ESTADO = null,String NB_MUNICIPIO = null,String NB_COLONIA = null,String NB_CALLE = null,String NO_INTERIOR = null,String NO_EXTERIOR = null,String CL_CODIGO_POSTAL = null,String CL_CORREO_ELECTRONICO = null,DateTime?  FE_NACIMIENTO = null,String DS_LUGAR_NACIMIENTO = null,Decimal?  MN_SUELDO = null,String CL_NACIONALIDAD = null,String DS_NACIONALIDAD = null,String NB_LICENCIA = null,String DS_VEHICULO = null,String CL_CARTILLA_MILITAR = null,String CL_CEDULA_PROFESIONAL = null,String XML_TELEFONOS = null,String XML_INGRESOS = null,String XML_EGRESOS = null,String XML_PATRIMONIO = null,String DS_DISPONIBILIDAD = null,String CL_DISPONIBILIDAD_VIAJE = null,String XML_PERFIL_RED_SOCIAL = null,String DS_COMENTARIO = null,bool?  FG_ACTIVO = null,DateTime?  FE_CREACION = null,DateTime?  FE_MODIFICACION = null,String CL_USUARIO_APP_CREA = null,String CL_USUARIO_APP_MODIFICA = null,String NB_PROGRAMA_CREA = null,String NB_PROGRAMA_MODIFICA = null)
//        {
//            NuevaSolicitudNegocio negocio = new NuevaSolicitudNegocio();
//            return negocio.Obtener_C_CANDIDATO(ID_CANDIDATO,NB_CANDIDATO,NB_APELLIDO_PATERNO,NB_APELLIDO_MATERNO,CL_GENERO,CL_RFC,CL_CURP,CL_ESTADO_CIVIL,NB_CONYUGUE,CL_NSS,CL_TIPO_SANGUINEO,NB_PAIS,NB_ESTADO,NB_MUNICIPIO,NB_COLONIA,NB_CALLE,NO_INTERIOR,NO_EXTERIOR,CL_CODIGO_POSTAL,CL_CORREO_ELECTRONICO,FE_NACIMIENTO,DS_LUGAR_NACIMIENTO,MN_SUELDO,CL_NACIONALIDAD,DS_NACIONALIDAD,NB_LICENCIA,DS_VEHICULO,CL_CARTILLA_MILITAR,CL_CEDULA_PROFESIONAL,XML_TELEFONOS,XML_INGRESOS,XML_EGRESOS,XML_PATRIMONIO,DS_DISPONIBILIDAD,CL_DISPONIBILIDAD_VIAJE,XML_PERFIL_RED_SOCIAL,DS_COMENTARIO,FG_ACTIVO,FE_CREACION,FE_MODIFICACION,CL_USUARIO_APP_CREA,CL_USUARIO_APP_MODIFICA,NB_PROGRAMA_CREA,NB_PROGRAMA_MODIFICA);
//        }

//        public int Insert_update_C_CANDIDATO (string tipo_transaccion, SPE_OBTIENE_C_CANDIDATO_Result V_C_CANDIDATO,string usuario, string programa, string fechaNacimiento)
//        {
//            NuevaSolicitudNegocio negocio = new NuevaSolicitudNegocio();

//            V_C_CANDIDATO.FE_NACIMIENTO = DateTime.Parse(fechaNacimiento);

//            return negocio.InsertaActualiza_C_CANDIDATO(tipo_transaccion,V_C_CANDIDATO, usuario,programa);
//        }

//        public bool Delete_C_CANDIDATO (SPE_OBTIENE_C_CANDIDATO_Result V_C_CANDIDATO,string usuario = null, string programa = null)
//        {
//            NuevaSolicitudNegocio negocio = new NuevaSolicitudNegocio();
//            return negocio.Elimina_C_CANDIDATO(V_C_CANDIDATO, usuario, programa);   
//        }

//        #endregion	

//        #region VW_TIPO_LICENCIA

//        public List<SPE_OBTIENE_VW_TIPO_LICENCIA_Result> Get_VW_TIPO_LICENCIA(String CL_LICENCIA = null,String NB_LICENCIA = null)
//        {
//            LicenciaNegocio negocio = new LicenciaNegocio();
//            return negocio.Obtener_VW_TIPO_LICENCIA(CL_LICENCIA,NB_LICENCIA);
//        }

//        #endregion	

//        #region VW_PAIS

//        public List<SPE_OBTIENE_VW_PAIS_Result> Get_VW_PAIS(String CL_PAIS = null,String NB_PAIS = null)
//        {
//            PaisNegocio negocio = new PaisNegocio();
//            return negocio.Obtener_VW_PAIS(CL_PAIS,NB_PAIS);
//        }

//        #endregion	

//        #region Colonia CP

//        public List<SPE_OBTIENE_DATOS_CP_Result> Get_Colonia_CP(String CL_CODIGO_POSTAL = null) {
//            ColoniaNegocio negocio = new ColoniaNegocio();
//            return negocio.ObtenerDatosColoniaCp(CL_CODIGO_POSTAL);
//        }

//        #endregion

//        #region VW_OBTIENE_PORCENTAJES_IDIOMAS

//        public List<SPE_OBTIENE_VW_OBTIENE_PORCENTAJES_IDIOMAS_Result> Get_VW_OBTIENE_PORCENTAJES_IDIOMAS(String NB_PORCENTAJE = null,decimal?  CL_PORCENTAJE = null)
//        {
//            PorcentajeIdiomaNegocio negocio = new PorcentajeIdiomaNegocio();
//            return negocio.Obtener_VW_OBTIENE_PORCENTAJES_IDIOMAS(NB_PORCENTAJE,CL_PORCENTAJE);
//        }

//        #endregion	

//        #region C_PLANTILLA_FORMULARIO

//        public List<SPE_OBTIENE_C_PLANTILLA_FORMULARIO_Result> Get_C_PLANTILLA_FORMULARIO(int? ID_PLANTILLA_SOLICITUD = null, String NB_PLANTILLA_SOLICITUD = null, String DS_PLANTILLA_SOLICITUD = null, String CL_FORMULARIO = null, bool? FG_GENERAL = null, String XML_PLANTILLA_SOLICITUD = null, DateTime? FE_CREACION = null, DateTime? FE_MODIFICACION = null, String CL_USUARIO_APP_CREA = null, String CL_USUARIO_APP_MODIFICA = null, String NB_PROGRAMA_CREA = null, String NB_PROGRAMA_MODIFICA = null)
//        {
//            PlantillaFormularioNegocio negocio = new PlantillaFormularioNegocio();
//            return negocio.Obtener_C_PLANTILLA_FORMULARIO(ID_PLANTILLA_SOLICITUD, NB_PLANTILLA_SOLICITUD, DS_PLANTILLA_SOLICITUD, CL_FORMULARIO, FG_GENERAL, XML_PLANTILLA_SOLICITUD, FE_CREACION, FE_MODIFICACION, CL_USUARIO_APP_CREA, CL_USUARIO_APP_MODIFICA, NB_PROGRAMA_CREA, NB_PROGRAMA_MODIFICA);
//        }

//        public bool Insert_update_C_PLANTILLA_FORMULARIO(string tipo_transaccion, SPE_OBTIENE_C_PLANTILLA_FORMULARIO_Result V_C_PLANTILLA_FORMULARIO, string usuario, string programa)
//        {
//            PlantillaFormularioNegocio negocio = new PlantillaFormularioNegocio();
//            return negocio.InsertaActualiza_C_PLANTILLA_FORMULARIO(tipo_transaccion, V_C_PLANTILLA_FORMULARIO, usuario, programa);
//        }

//        public bool Delete_C_PLANTILLA_FORMULARIO(int? ID_PLANTILLA_SOLICITUD = null, string usuario = null, string programa = null)
//        {
//            PlantillaFormularioNegocio negocio = new PlantillaFormularioNegocio();
//            return negocio.Elimina_C_PLANTILLA_FORMULARIO(ID_PLANTILLA_SOLICITUD, usuario, programa);
//        }

//        #endregion	

    }
}
