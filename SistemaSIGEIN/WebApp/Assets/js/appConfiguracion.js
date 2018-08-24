//SACAR EL IDIOMA ELEGIDO PARA MOSTRAR LA INFORMACION DEPENDIENDO LA ELECCIONs
var idioma = "es";
if (localStorage.getItem("idioma") != null) {
    idioma = localStorage.getItem("idioma");
}

var numModulo = (sessionStorage.getItem("modulo") != null) ? sessionStorage.getItem("modulo") : "";

var Configuracion = function () {  //Config
    this.MENSAJEGUARDAR = (idioma == "es") ? "Se ha guardado correctamente" : "It has been saved successfully",
    this.ERRORGUARDAR = (idioma == "es") ? "Error al intentar guardar el registro" : "Error while trying to save the record",
    this.MENSAJEERROR = (idioma == "es") ? "Ha ocurrido un error" : "An error has occurred",
    this.MENSAJECORRECTO = (idioma == "es") ? "Proceso exitoso" : "Successful process",
    this.ACCESOALSISTEMA = (idioma == "es") ? "Acceso al sistema" : "System Access",
    this.ERRORACCESO = (idioma == "es") ? "Usuario y/o contraseña incorrectos" : "UserNombre and password incorrect",
    this.ERRORGENERICO = (idioma == "es") ? "Ocurrio un error al procesar la información. Intente de nuevo" : "An error occurred while processing the information, try again",
    this.RECUPERAPASSWORD = (idioma == "es") ? "Recupera contraseña" : "Recover password",
    this.BOTONENVIAR = (idioma == "es") ? "Enviar" : "Send",
    this.BOTONCANCELAR = (idioma == "es") ? "Cancelar" : "Cancel"
    this.BOTONGUARDAR = (idioma == "es") ? "Guardar" : "Save"
    this.BOTONELIMINAR = (idioma == "es") ? "Eliminar" : "Delete"
    this.MENSAJECONFIRMADEL = (idioma == "es") ? "¿Desea eliminar este registro?" : "Delete this record?"
    this.MENSAJEELIMINAR = (idioma == "es") ? "Se ha eliminado correctamente" : "It has been deleted successfully",
    this.ERRORELIMINAR = (idioma == "es" ) ? "Error al intentar eliminar el registro" : "Error while trying to delete the record",
    this.MENSAJEREGISTRO = (idioma == "es") ? "Seleccionar registro" : "Select register"
    this.MSJCLAVE = (idioma == "es") ? "Clave" : "Key"
    this.MSJNOMBRE = (idioma == "es") ? "Nombre" : "Name"
    this.MSJREQUERIDO = (idioma == "es") ? "Este campo es requerido" : "this it is required"
    this.BOTONAGREGAR = (idioma == "es") ? "Agregar" : "Add"
    this.BOTONMODIFICAR = (idioma == "es") ? "Modificar" : "Update"
    this.BOTONACTIVARDESACTIVAR = (idioma == "es") ? "Activar/Desactivar" : "Activate/Deactivate"
    this.MENSAJEACTIVARDESACTIVAR = (idioma == "es") ? "Se ha activado/desactivado correctamente" : "It has been activated/deactivated correctly"
    this.ERRORACTIVARDESACTIVAR = (idioma == "es") ? "Error al intentar activar/desactivar el registro" : "Error while trying activate/deactivate the record"
    this.BOTONACEPTAR = (idioma == "es") ? "Aceptar" : "OK"
    this.BOTONSI = (idioma == "es") ? "Sí" : "Yes"
    this.BOTONNO = (idioma == "es") ? "No" : "No"
    this.BOTONADJUNTAR = (idioma == "es") ? "No" : "No"
    this.MSJFILTROVALOR1 = (idioma == "es") ? "Falta que ingrese el valor a filtrar" : "No value income"
    this.MSJFILTROVALOR2 = (idioma == "es") ? "Falta que ingrese el segundo valor a filtrar" : "Need to enter the second value"
    this.MSJFILTROCONDICION = (idioma == "es") ? "Falta que ingrese la condicion de filtrado" : "Need to enter the filter condition"
    this.FILTROCONDICIONESDEFILTRADO = (idioma == "es") ? "Condiciones de filtrado" : "Filter terms"
    this.FILTROVALORES = (idioma == "es") ? "Valor (es)" : "Value"
    this.FILTROCAMPOSPARAFILTRAR = (idioma == "es") ? "Campo(s) para filtrar" : "Field to filter"
    this.FILTROCONDICIONESAFILTRAR = (idioma == "es") ? "Condiciones a filtrar" : "Filter conditions"
    this.FILTROTITULO = (idioma == "es") ? "Filtrado avanzado" : "Advanced filter"
    this.BTNFILTROAVANZADO = (idioma == "es") ? "Filtrar" : "Filter"
    this.FILTROCONDICIONESFILTRARCONTIENE = (idioma == "es") ? "Contiene" : "contains"
    this.FILTROCONDICIONESFILTRARIGUALA = (idioma == "es") ? "Igual a" : "Equal to"
    this.FILTROCONDICIONESFILTRARENTRE = (idioma == "es") ? "Entre" : "Between"
    this.FILTROCONDICIONESFILTRAREMPIEZAPOR = (idioma == "es") ? "Empieza por" : "starts with"
    this.FILTROCONDICIONESFILTRARTERMINAPOR = (idioma == "es") ? "Termina por" : "Ends with"
    this.FILTROCONDICIONESFILTRARMAYORQUE = (idioma == "es") ? "Mayor que" : "Greater than"
    this.FILTROCONDICIONESFILTRARMENORQUE = (idioma == "es") ? "Menor que" : "less than"
    this.FILTROCONDICIONESFILTRARMAYORIGUALQUE = (idioma == "es") ? "Mayor o igual que" : "Greater than or equal"
    this.FILTROCONDICIONESFILTRARMENORIGUALQUE = (idioma == "es") ? "Menor o igual que" : "Less than or equal"
    this.FILTROCONDICIONESFILTROMAYORQUE = (idioma == "es") ? "Mayor que" : "Is greater than"
    this.FILTROCONDICIONESFILTROMENORQUEOIGUALQUE = (idioma == "es") ? "Menor que o igual a" : "Is less than or equal to"
    this.FILTROCONDICIONESFILTRODESPUESOIGUALQUE = (idioma == "es") ? "Después o igual a" : "Is after or equal to"
    this.FILTROCONDICIONESFILTRODESPUES = (idioma == "es") ? "Después" : "Is after"
    this.FILTROCONDICIONESFILTROESANTESOIGUALQUE = (idioma == "es") ? "Antes o igual a" : "Is before or equal to"
    this.FILTROCONDICIONESFILTROANTES = (idioma == "es") ? "Antes" : "Is before"
    this.FILTROFALTACONDICIONES = (idioma == "es") ? "Ingrese al menos una condición de búsqueda" : "Enter at least one search condition"
    this.BOTONCATALOGO = (idioma == "es") ? "Catálogo" : "Catalog",
    this.PERMISOSPAGINA = (idioma == "es") ? "No tienes acceso a esta página" : "You do not have access to this page"
    this.SELECCIONAR = (idioma == "es") ? "Seleccione" : "Select"
    this.MENSAJECATALOGO = (idioma == "es") ? "Seleccionar catálogo" : "Select catalog"
    this.PERMISOELIMINAR = (idioma == "es") ? "No tienes permiso para eliminar este catálogo." : "You don't have permission to delete this catalog."
    this.NACIONALIDAD = (idioma == "es") ? "Mexicana" : "Mexicana"
    this.BOTONFILTROAVANZADO = (idioma == "es") ? "Filtro avanzado" : "Advanced filter"
    this.BOTONPERSONALIZAR = (idioma == "es") ? "Personalizar" : "Customize"
    this.BOTONFILTRAR = (idioma == "es") ? "Agregar" : "Add"
    this.OTRA = (idioma == "es") ? "Otra" : "Otra"
    this.PERSONALIZARENCABEZADO = (idioma == "es") ? "Campos disponibles" : "Availabe fileds"
    this.PERSONALIZARGRILLA = (idioma == "es") ? "Grilla de datos" : "Data grid"
    this.BUSQUEDAAVANZADACAMPO = (idioma == "es") ? "Campo" : "Field"
    this.BUSQUEDAAVANZADACONDICION = (idioma == "es") ? "Condición" : "Condition"
    this.BUSQUEDAAVANZADAVALOR1 = (idioma == "es") ? "Valor 1" : "Value 1"
    this.BUSQUEDAAVANZADAVALOR2 = (idioma == "es") ? "Valor 2" : "Value 2"
    this.BUSQUEDAAVANZADAELIMINAR = (idioma == "es") ? "Eliminar" : "Delete"
    this.BUSQUEDAAVANZADAOPCIONES = (idioma == "es") ? "Opciones" : "Options"
    this.BUSQUEDAAVANZADAERROR = (idioma == "es") ? "El segundo valor debe de ser mayor que el primero." : "The second value must be greater than the first one."
        
    this.PERSONALIZARERRORGUARDAR = (idioma == "es") ? "No hay columnas que personalizar" : "There are not columns to customize."
  
   };

var GridKendo = function () {
    this.AND = (idioma == "es") ? "y" : "and"
    , this.CANCEL = (idioma == "es") ? "cancelar" : "cancel"
    , this.CHECKALL = (idioma == "es") ? "seleccionar todas" : "check all"
    , this.CLEAR = (idioma == "es") ? "borrar" : "clear"
    , this.ISEQUALTO = (idioma == "es") ? "es igual a" : "is equal to"
    , this.FILTER = (idioma == "es") ? "Filtrar" : "Filter"
    , this.INFO = (idioma == "es") ? "filtrar por: " : "filter to:"
    , this.ISFALSE = (idioma == "es") ? "es falso" : "is false"
    , this.ISTRUE = (idioma == "es") ? "es verdadero" : "is true"
    , this.OPERATOR = (idioma == "es") ? "operador" : "operator"
    , this.OR = (idioma == "es") ? "o" : "or"
    , this.SELECTVALUE = (idioma == "es") ? "selecciona valor" : "select value"
    , this.VALUE = (idioma == "es") ? "valor" : "value"
    , this.CONTAINS = (idioma == "es") ? "contiene" : "contains"
    , this.DOESNOTCONTAIN = (idioma == "es") ? "no contiene" : "does not contain"
    , this.ENDSWITH = (idioma == "es") ? "finaliza con" : "ends with"
    , this.EQ = (idioma == "es") ? "igual" : "equal"
    , this.NEQ = (idioma == "es") ? "no igual" : "does not equal"
    , this.STARTSWITH = (idioma == "es") ? "inicializa con" : "starts with"
    , this.COLUMNS = (idioma == "es") ? "Columnas a mostrar" : "Columns"
    , this.NONE = (idioma == "es") ? "Listo" : "Done"
    , this.LOQ = (idioma == "es") ? "Bloquear" : "Lock"
    , this.SETTINGS = (idioma == "es") ? "Ajustes" : "Settings"
    , this.SORTASCENDING = (idioma == "es") ? "Ordenar ascendente" : "Sort ascending"
    , this.SORTDESCENDING = (idioma == "es") ? "Ordenar descendente" : "Sort descending"
    , this.UNLOCK = (idioma == "es") ? "Desbloquear" : "Unlock"
    , this.NORECORDS = (idioma == "es") ? "No hay registros" : "No records"
    , this.GRUPABLEEMPTY = (idioma == "es") ? "Arrastre aquí la columna para agruparla" : "Drag a column header and drop it here to group by that column"
    , this.PAGEABLEDISPLAY = (idioma == "es") ? "{0} - {1} de {2} elementos" : "{0} - {1} OF {2} items"
    , this.PAGEABLEEMPTY = (idioma == "es") ? "no hay elementos" : "no items"
    , this.PAGEABLEPAGE = (idioma == "es") ? "página" : "page"
    , this.PAGEABLEOF = (idioma == "es") ? "de {0}" : "of {0}"
    , this.PAGEABLEITEMSPERPAGE = (idioma == "es") ? "registros por página" : "items per page"
    , this.PAGEABLEMOREPAGE = (idioma == "es") ? "mas páginas" : "more page"
    , this.PAGEABLEFIRST = (idioma == "es") ? "primera página" : "first page"
    , this.PAGEABLEPREVIOUS = (idioma == "es") ? "anterior página" : "previous page"
    , this.PAGEABLENEXT = (idioma == "es") ? "siguiente página" : "next page"
    , this.PAGEABLELAST = (idioma == "es") ? "ultima página" : "last page"
    , this.PAGEABLEREFRESH = (idioma == "es") ? "refrescar" : "refresh"
    , this.BOTONAGREGAR = (idioma == "es") ? "Agregar" : "Add"
    , this.BOTONMODIFICAR = (idioma == "es") ? "Modificar" : "Update"
    , this.BOTONELIMINAR = (idioma == "es") ? "Eliminar" : "Delete"
    , this.BOTONEXPORTAREXCEL = (idioma == "es") ? "Exportar a Excel" : "Export Excel"
    , this.BOTONEXPORTARPDF = (idioma == "es") ? "Exportar a PDF" : "Export PDF"
    , this.BOTONGUARDAR = (idioma == "es") ? "Guardar" : "Save"
    , this.BOTONCANCELAR = (idioma == "es") ? "Cancelar" : "Cancel"
    , this.MSJACTIVO = (idioma == "es") ? "Activo" : "Active"
    , this.MSJINACTIVO = (idioma == "es") ? "Inactivo" : "Inctive"

};

var M_DEPARTAMENTO = function () {
    this.TITULODEPTO = (idioma == "es") ? "Área" : "Area"
   , this.CLAVEDEPTO = (idioma == "es") ? "Clave del área" : "Area Clave"
   , this.NOMBREDEPTO = (idioma == "es") ? "Nombre del área" : "Area Nombre"
   , this.ACTIVODEPTO = (idioma == "es") ? "Activo" : "Activo"

}

var C_ESTADO = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Estado" : "Estado"
   , this.CL_ESTADO = (idioma == "es") ? "Clave del estado" : "Clave del estado"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_ESTADO_ph = (idioma == "es") ? "Seleccione el estado" : "Seleccione el estado" //traduccion para los placeholder  
   , this.CL_PAIS = (idioma == "es") ? "Clave del país" : "Clave del país"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_PAIS_ph = (idioma == "es") ? "Clave del país" : "Clave del país" //traduccion para los placeholder  
   , this.CL_USUARIO_APP_CREA = (idioma == "es") ? "Clave del usuario que crea" : "Clave del usuario que crea"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_USUARIO_APP_CREA_ph = (idioma == "es") ? "Clave del usuario que crea" : "Clave del usuario que crea" //traduccion para los placeholder  
   , this.CL_USUARIO_APP_MODIFICA = (idioma == "es") ? "Clave del usuario que modifica" : "Clave del usuario que modifica"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_USUARIO_APP_MODIFICA_ph = (idioma == "es") ? "Clave del usuario que modifica" : "Clave del usuario que modifica" //traduccion para los placeholder  
   , this.FE_CREACION = (idioma == "es") ? "Fecha de creación" : "Fecha de creación"  //traduccion para las etiquetas y titulos de columnas 
   , this.FE_CREACION_ph = (idioma == "es") ? "Fecha de creación" : "Fecha de creación" //traduccion para los placeholder  
   , this.FE_MODIFICACION = (idioma == "es") ? "Fecha de modificación" : "Fecha de modificación"  //traduccion para las etiquetas y titulos de columnas 
   , this.FE_MODIFICACION_ph = (idioma == "es") ? "Fecha de modificación" : "Fecha de modificación" //traduccion para los placeholder  
   , this.ID_ESTADO = (idioma == "es") ? "ID estado" : "ID estado"  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_ESTADO_ph = (idioma == "es") ? "ID estado" : "ID estado" //traduccion para los placeholder  
   , this.NB_ESTADO = (idioma == "es") ? "Estado" : "Estado"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_ESTADO_ph = (idioma == "es") ? "Nombre del estado" : "Nombre del estado" //traduccion para los placeholder  
   , this.NB_PROGRAMA_CREA = (idioma == "es") ? "Programa que crea" : "Programa que crea"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_PROGRAMA_CREA_ph = (idioma == "es") ? "Programa que crea" : "Programa que crea" //traduccion para los placeholder  
   , this.NB_PROGRAMA_MODIFICA = (idioma == "es") ? "Programa que modifica" : "Programa que modifica"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_PROGRAMA_MODIFICA_ph = (idioma == "es") ? "Programa que modifica" : "Programa que modifica" //traduccion para los placeholder  
};

var C_NIVEL_ESCOLARIDAD = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Nivel Escolaridad" : "Nivel Escolaridad"
   , this.CL_NIVEL_ESCOLARIDAD = (idioma == "es") ? "Clave del Nivel de Escolaridad" : "Clave del Nivel de Escolaridad"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_NIVEL_ESCOLARIDAD_ph = (idioma == "es") ? "Clave del Nivel de Escolaridad" : "Clave del Nivel de Escolaridad" //traduccion para los placeholder  
   , this.DS_NIVEL_ESCOLARIDAD = (idioma == "es") ? "Descripción del Nivel de Escolaridad" : "Descripción del Nivel de Escolaridad"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_NIVEL_ESCOLARIDAD_ph = (idioma == "es") ? "Descripción del Nivel de Escolaridad" : "Descripción del Nivel de Escolaridad" //traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder  
};

var C_ROL = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Rol" : "Rol"
   , this.CL_ROL = (idioma == "es") ? "Clave del Rol" : "Clave del Rol"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_ROL_ph = (idioma == "es") ? "Clave del Rol" : "Clave del Rol" //traduccion para los placeholder  
   , this.NB_ROL = (idioma == "es") ? "Nombre del Rol" : "Nombre del Rol"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_ROL_ph = (idioma == "es") ? "Nombre del Rol" : "Nombre del Rol" //traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder  
};

var S_FUNCION = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Funcion" : "Funcion"
   , this.CL_FUNCION = (idioma == "es") ? "Clave" : "Clave"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_FUNCION_ph = (idioma == "es") ? "Clave" : "Clave" //traduccion para los placeholder  
   , this.NB_FUNCION = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_FUNCION_ph = (idioma == "es") ? "Nombre" : "Nombre" //traduccion para los placeholder  
   , this.CL_TIPO_FUNCION = (idioma == "es") ? "Tipo" : "Tipo"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_TIPO_FUNCION_ph = (idioma == "es") ? "Tipo" : "Tipo" //traduccion para los placeholder  
};

var S_TIPO_COMPETENCIA = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Tipo competencia laborales" : "Tipo competencia laborales"
   , this.CL_TIPO_COMPETENCIA = (idioma == "es") ? "Clave" : "Clave"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_TIPO_COMPETENCIA_ph = (idioma == "es") ? "Clave" : "Clave" //traduccion para los placeholder  
   , this.DS_TIPO_COMPETENCIA = (idioma == "es") ? "Descripción" : "Descripción"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_TIPO_COMPETENCIA_ph = (idioma == "es") ? "Descripción" : "Descripción" //traduccion para los placeholder  
   , this.NB_TIPO_COMPETENCIA = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_TIPO_COMPETENCIA_ph = (idioma == "es") ? "Nombre" : "Nombre" //traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder  

};

var C_COMPETENCIA = function () {
    
    this.GENERALES = (idioma == "es") ? "Generales" : "Generales"
    this.DEFINICION_DE_NIVELES = (idioma == "es") ? "Definición de niveles " : "Definición de niveles"
    , this.TITULO_PANTALLA = (idioma == "es") ? "Competencias laborales" : "Competencias laborales"
   , this.CL_CLASIFICACION = (idioma == "es") ? "Clasificación" : "Clasificación"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_CLASIFICACION_ph = (idioma == "es") ? "Clasificación" : "Clasificación" //traduccion para los placeholder  
   , this.CL_COMPETENCIA = (idioma == "es") ? "Clave" : "Clave"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_COMPETENCIA_ph = (idioma == "es") ? "Clave" : "Clave" //traduccion para los placeholder  
   , this.CL_TIPO_COMPETENCIA = (idioma == "es") ? "Tipo Competencia" : "Tipo Competencia"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_TIPO_COMPETENCIA_ph = (idioma == "es") ? "Tipo Competencia" : "Tipo Competencia" //traduccion para los placeholder  
   , this.DS_COMPETENCIA = (idioma == "es") ? "Descripción" : "Descripción"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_COMPETENCIA_ph = (idioma == "es") ? "Descripción" : "Descripción" //traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder  
   , this.NB_COMPETENCIA = (idioma == "es") ? "Competencia" : "Competencia"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_COMPETENCIA_ph = (idioma == "es") ? "Competencia" : "Competencia" //traduccion para los placeholder  
   , this.NB_TIPO_COMPETENCIA = (idioma == "es") ? "Categoría" : "Categoría"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_CLASIFICACION_COMPETENCIA = (idioma == "es") ? "Clasificación" : "Categoría"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_NIVEL0 = (idioma == "es") ? "Nivel 0" : "Nivel 0"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_NIVEL1 = (idioma == "es") ? "Nivel 1" : "Nivel 1"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_NIVEL2 = (idioma == "es") ? "Nivel 2" : "Nivel 2"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_NIVEL3 = (idioma == "es") ? "Nivel 3" : "Nivel 3"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_NIVEL4 = (idioma == "es") ? "Nivel 4" : "Nivel 4"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_NIVEL5 = (idioma == "es") ? "Nivel 5" : "Nivel 5"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_PORPUESTO = (idioma == "es") ? "Por puesto " : "Por puesto "  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_PORPERSONA = (idioma == "es") ? "Por persona" : "Por persona"  //traduccion para las etiquetas y titulos de columnas 

};

var C_EVALUADOR_EXTERNO = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Evaluador Externo" : "Evaluador Externo"
   , this.CL_EVALUADOR_EXTERNO = (idioma == "es") ? "Clave" : "Clave"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_EVALUADOR_EXTERNO_ph = (idioma == "es") ? "Clave" : "Clave" //traduccion para los placeholder  
   , this.NB_EVALUADOR_EXTERNO = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_EVALUADOR_EXTERNO_ph = (idioma == "es") ? "Nombre" : "Nombre" //traduccion para los placeholder  
   , this.DS_EVALUARDO_EXTERNO = (idioma == "es") ? "Descipción" : "Descripción"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_EVALUARDO_EXTERNO_ph = (idioma == "es") ? "Descipción" : "Descripción" //traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder  
};

var C_CARRERA_TECNICA = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Catálogo técnicas" : "Catálogo técnicas"
    , this.ID_ESCOLARIDAD = (idioma == "es") ? "Id escolaridad" : "Id escolaridad"  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_ESCOLARIDAD_ph = (idioma == "es") ? "Id escolaridad" : "Id escolaridad"  //traduccion para los placeholder  
    , this.NB_ESCOLARIDAD = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_ESCOLARIDAD_ph = (idioma == "es") ? "Nombre de la carrera" : "Nombre de la carrera" //traduccion para los placeholder  
   , this.DS_ESCOLARIDAD = (idioma == "es") ? "Descripción" : "Descripción"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_ESCOLARIDAD_ph = (idioma == "es") ? "Descripción de la carrera" : "Descripción de la carrera" //traduccion para los placeholder  
   , this.CL_NIVEL_ESCOLARIDAD = (idioma == "es") ? "Clave Nivel escolaridad" : "Clave Nivel escolaridad" //traduccion para las etiquetas y titulos de columnas 
   , this.CL_NIVEL_ESCOLARIDAD_ph = (idioma == "es") ? "Clave Nivel escolaridad" : "Clave Nivel escolaridad" //traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder  
   , this.ID_NIVEL_ESCOLARIDAD = (idioma == "es") ? "Id Nivel escolaridad" : "Id Nivel escolaridad" //traduccion para las etiquetas y titulos de columnas 
   , this.ID_NIVEL_ESCOLARIDAD_ph = (idioma == "es") ? "Id Nivel escolaridad" : "Id Nivel escolaridad"  //traduccion para los placeholder  
   , this.CL_INSTITUCION = (idioma == "es") ? "Clave institución" : "Clave institución"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_INSTITUCION_ph = (idioma == "es") ? "Clave institución" : "Clave institución" //traduccion para los placeholder  

};

var C_MUNICIPIO = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Municipio" : "Municipio"
   , this.CL_ESTADO = (idioma == "es") ? "Clave de estado" :  "Clave de estado"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_ESTADO_ph = (idioma == "es") ? "Clave del estado" : "Clave del estado"  //traduccion para los placeholder  
   , this.CL_MUNICIPIO = (idioma == "es") ? "Clave de municipio" : "Clave de municipio"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_MUNICIPIO_ph = (idioma == "es") ? "Seleccione el municipio" : "Seleccione el municipio" //traduccion para los placeholder  
   , this.CL_PAIS = (idioma == "es") ? "Clave de pais" : "Clave de pais"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_PAIS_ph = (idioma == "es") ? "Clave de pais" : "Clave de pais" //traduccion para los placeholder  
   , this.ID_MUNICIPIO = (idioma == "es") ? "id municipio" : "id municipio" //traduccion para las etiquetas y titulos de columnas 
   , this.ID_MUNICIPIO_ph = (idioma == "es") ? "id municipio" : "id municipio" //traduccion para los placeholder  
   , this.NB_MUNICIPIO = (idioma == "es") ? "Municipio" : "Municipio"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_MUNICIPIO_ph = (idioma == "es") ? "Municipio" : "Municipio" //traduccion para los placeholder  
   , this.NB_PROGRAMA_CREA = (idioma == "es") ? "Programa que crea" : "Programa que crea"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_PROGRAMA_CREA_ph = (idioma == "es") ? "Programa que crea" : "Programa que crea" //traduccion para los placeholder  
   , this.NB_PROGRAMA_MODIFICA = (idioma == "es") ? "Programa que modifica" : "Programa que modifica"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_PROGRAMA_MODIFICA_ph = (idioma == "es") ? "Programa que modifica" : "Programa que modifica" //traduccion para los placeholder  
   , this.CL_USUARIO_APP_CREA = (idioma == "es") ? "Clave del usuario que crea" : "Clave del usuario que crea"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_USUARIO_APP_CREA_ph = (idioma == "es") ? "Clave del usuario que crea" : "Clave del usuario que crea" //traduccion para los placeholder  
   , this.CL_USUARIO_APP_MODIFICA = (idioma == "es") ? "Clave del usuario que modifica" : "Clave del usuario que modifica"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_USUARIO_APP_MODIFICA_ph = (idioma == "es") ? "Clave del usuario que modifica" : "Clave del usuario que modifica" //traduccion para los placeholder  
   , this.FE_CREACION = (idioma == "es") ? "Fecha de creación" : "Fecha de creación"  //traduccion para las etiquetas y titulos de columnas 
   , this.FE_CREACION_ph = (idioma == "es") ? "Fecha de creación" : "Fecha de creación" //traduccion para los placeholder  
   , this.FE_MODIFICACION = (idioma == "es") ? "Fecha de modificación" : "Fecha de modificación "  //traduccion para las etiquetas y titulos de columnas 
   , this.FE_MODIFICACION_ph = (idioma == "es") ? "Fecha de modificación" : "Fecha de modificación" //traduccion para los placeholder  

   
};

var C_COLONIA = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Localización" : "Localización"
   , this.CL_CODIGO_POSTAL = (idioma == "es") ? "Código postal" : "Código postal"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_CODIGO_POSTAL_ph = (idioma == "es") ? "Código postal" : "Código postal" //traduccion para los placeholder  
   , this.CL_COLONIA = (idioma == "es") ? "Clave de colonia" : "Clave de colonia"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_COLONIA_ph = (idioma == "es") ? "Clave de colonia" : "Clave de colonia" //traduccion para los placeholder  
   , this.CL_ESTADO = (idioma == "es") ? "Clave de estado" : "Clave de estado"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_ESTADO_ph = (idioma == "es") ? "Clave de estado" : "Clave de estado" //traduccion para los placeholder  
   , this.CL_MUNICIPIO = (idioma == "es") ? "Clave de municipio" : "Clave de municipio"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_MUNICIPIO_ph = (idioma == "es") ? "Clave de municipio" : "Clave de municipio" //traduccion para los placeholder  
   , this.CL_PAIS = (idioma == "es") ? "Clave de pais" : "Clave de pais"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_PAIS_ph = (idioma == "es") ? "Clave de pais" : "Clave de pais" //traduccion para los placeholder  
   , this.CL_TIPO_ASENTAMIENTO = (idioma == "es") ? "Tipo de Asentamiento" : "Tipo de Asentamiento"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_TIPO_ASENTAMIENTO_ph = (idioma == "es") ? "Tipo de Asentamiento" : "Tipo de Asentamiento" //traduccion para los placeholder  
   , this.ID_COLONIA = (idioma == "es") ? "ID colonia" : "ID colonia "  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_COLONIA_ph = (idioma == "es") ? "ID colonia" : "ID colonia " //traduccion para los placeholder  
   , this.NB_COLONIA = (idioma == "es") ? "Colonia" : "Colonia"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_COLONIA_ph = (idioma == "es") ? "Colonia" : "Colonia" //traduccion para los placeholder  
   , this.NB_PROGRAMA_CREA = (idioma == "es") ? "Programa que crea" : "Programa que crea"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_PROGRAMA_CREA_ph = (idioma == "es") ? "Programa que crea" : "Programa que crea" //traduccion para los placeholder  
   , this.NB_PROGRAMA_MODIFICA = (idioma == "es") ? "Programa que modifica" : "Programa que modifica"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_PROGRAMA_MODIFICA_ph = (idioma == "es") ? "Programa que modifica" : "Programa que modifica" //traduccion para los placeholder  
   , this.CL_USUARIO_APP_CREA = (idioma == "es") ? "Clave del usuario que crea" : "Clave del usuario que crea"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_USUARIO_APP_CREA_ph = (idioma == "es") ? "Clave del usuario que crea" : "Clave del usuario que crea" //traduccion para los placeholder  
   , this.CL_USUARIO_APP_MODIFICA = (idioma == "es") ? "Clave del usuario que modifica" : "Clave del usuario que modifica"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_USUARIO_APP_MODIFICA_ph = (idioma == "es") ? "Clave del usuario que modifica" : "Clave del usuario que modifica" //traduccion para los placeholder  
   , this.FE_CREACION = (idioma == "es") ? "Fecha de creación" : "Fecha de creación"  //traduccion para las etiquetas y titulos de columnas 
   , this.FE_CREACION_ph = (idioma == "es") ? "Fecha de creación" : "Fecha de creación" //traduccion para los placeholder  
   , this.FE_MODIFICACION = (idioma == "es") ? "Fecha de modificación" : "Fecha de modificación"  //traduccion para las etiquetas y titulos de columnas 
   , this.FE_MODIFICACION_ph = (idioma == "es") ? "Fecha de modificación" : "Fecha de modificación" //traduccion para los placeholder  
   , this.SELECCIONE_ESTADO_MUNICIPIO = (idioma == "es") ? "Para ingresar una colonia debe seleccionar previamente un estado y municipio" : "To enter a colony must first select a state and municipality" //traduccion PARA MENSAJES 
   
};

var C_CLASIFICACION_COMPETENCIA = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Clasificación de competencias laborales" : "Clasificación de competencias laborales"
   , this.CL_CLASIFICACION = (idioma == "es") ? "Clave " : "Clave"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_CLASIFICACION_ph = (idioma == "es") ? "Clave clasificación de competencia" : "Clasificación Clave competence" //traduccion para los placeholder  
   , this.CL_TIPO_COMPETENCIA = (idioma == "es") ? "Tipo de competencia" : "Tipo de competencia"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_TIPO_COMPETENCIA_ph = (idioma == "es") ? "Tipo de competencia" : "Tipo de competencia" //traduccion para los placeholder  
   , this.DS_CLASIFICACION_COMPETENCIA = (idioma == "es") ? "Descripción" : "Descripción"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_CLASIFICACION_COMPETENCIA_ph = (idioma == "es") ? "Descripción" : "Descripción" //traduccion para los placeholder  
   , this.DS_NOTAS_CLASIFICACION = (idioma == "es") ? "Notas" : "Notas"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_NOTAS_CLASIFICACION_ph = (idioma == "es") ? "Notas" : "Notas" //traduccion para los placeholder  
   , this.ID_CLASIFICACION_COMPETENCIA = (idioma == "es") ? "ID Clasificación de competencia" : "ID Clasificación de competencia"  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_CLASIFICACION_COMPETENCIA_ph = (idioma == "es") ? "ID Clasificación de competencia" : "Ranking Competencia ID" //traduccion para los placeholder  
   , this.NB_CLASIFICACION_COMPETENCIA = (idioma == "es") ? "Clasificación" : "Clasificación"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_CLASIFICACION_COMPETENCIA_ph = (idioma == "es") ? "Nombre de la clasificación" : "Nombre de la clasificación" //traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder  
   , this.NB_PROGRAMA_CREA = (idioma == "es") ? "Programa que crea" : "Programa que crea"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_PROGRAMA_CREA_ph = (idioma == "es") ? "Programa que crea" : "Programa que crea" //traduccion para los placeholder  
   , this.NB_PROGRAMA_MODIFICA = (idioma == "es") ? "Programa que modifica" : "Programa que modifica"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_PROGRAMA_MODIFICA_ph = (idioma == "es") ? "Programa que modifica" : "Programa que modifica" //traduccion para los placeholder  
   , this.CL_USUARIO_APP_CREA = (idioma == "es") ? "Clave del usuario que crea" : "Clave del usuario que crea"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_USUARIO_APP_CREA_ph = (idioma == "es") ? "Clave del usuario que crea" : "Clave del usuario que crea" //traduccion para los placeholder  
   , this.CL_USUARIO_APP_MODIFICA = (idioma == "es") ? "Clave del usuario que modifica" : "Clave del usuario que modifica"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_USUARIO_APP_MODIFICA_ph = (idioma == "es") ? "Clave del usuario que modifica" : "Clave del usuario que modifica" //traduccion para los placeholder  
   , this.FE_CREACION = (idioma == "es") ? "Fecha de creación" : "Fecha de creación"  //traduccion para las etiquetas y titulos de columnas 
   , this.FE_CREACION_ph = (idioma == "es") ? "Fecha de creación" : "Fecha de creación" //traduccion para los placeholder  
   , this.FE_MODIFICACION = (idioma == "es") ? "Fecha de modificación" : "Fecha de modificación"  //traduccion para las etiquetas y titulos de columnas 
   , this.FE_MODIFICACION_ph = (idioma == "es") ? "Fecha de modificación" : "Fecha de modificación" //traduccion para los placeholder  
   , this.NB_TIPO_COMPETENCIA = (idioma == "es") ? "Categoría" : "Categoría"  //traduccion para las etiquetas y titulos de columnas 
};

var K_SOLICITUD = function () {
    this.OBLIGATORIO = (idioma == "es") ? "Campo Obligatorio" : "Campo Obligatorio"
  , this.TABULADOR1 = (idioma == "es") ? "Información Personal" : "Información Personal"
  , this.TABULADOR2 = (idioma == "es") ? "Datos Familiares" : "Datos Familiares"
  , this.TABULADOR3 = (idioma == "es") ? "Formación Académica" : "Formación Académica"
  , this.TABULADOR4 = (idioma == "es") ? "Experiencia Laboral" : "Experiencia Laboral"
  , this.TABULADOR5 = (idioma == "es") ? "Intereses y Competencias" : "Intereses y Competencias"
  , this.TABULADOR6 = (idioma == "es") ? "Información Adicional" : "Información Adicional"
  , this.TABULADOR7 = (idioma == "es") ? "Vista Previa" : "Vista Previa"
  , this.GENFEMENINO = (idioma == "es") ? "Femenino" : "Femenino"
  , this.GENMASCULINO = (idioma == "es") ? "Masculino" : "Masculino"
  , this.TITULO_PANTALLA = (idioma == "es") ? "Pariente" : "Pariente"
  , this.NB_PARIENTE = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
  , this.NB_PARIENTE_ph = (idioma == "es") ? "Nombre" : "Nombre" //traduccion para los placeholder  
  , this.CL_PARENTEZCO = (idioma == "es") ? "Parentezco" : "Parentezco"  //traduccion para las etiquetas y titulos de columnas 
  , this.CL_PARENTEZCO_ph = (idioma == "es") ? "Parentezco" : "Parentezco" //traduccion para los placeholder  
  , this.FE_NACIMIENTO = (idioma == "es") ? "Fecha de nacimiento" : "Fecha de nacimiento"  //traduccion para las etiquetas y titulos de columnas 
  , this.FE_NACIMIENTO_ph = (idioma == "es") ? "Fecha de nacimiento" : "Fecha de nacimiento" //traduccion para los placeholder  
  , this.CL_OCUPACION = (idioma == "es") ? "Ocupación" : "Ocupación"  //traduccion para las etiquetas y titulos de columnas 
  , this.CL_OCUPACION_ph = (idioma == "es") ? "Ocupación" : "Ocupación" //traduccion para los placeholder  
  , this.FG_DEPENDIENTE = (idioma == "es") ? "Dependiente económico" : "Dependiente económico"  //traduccion para las etiquetas y titulos de columnas 
  , this.FG_DEPENDIENTE_ph = (idioma == "es") ? "Dependiente wconómico" : "Dependiente económico"//traduccion para los placeholder  
  , this.ESTUDIOS_CONCLUIDOS = (idioma == "es") ? "Estudios concluidos" : "Estudios concluidos"  //traduccion para los placeholder  
  , this.ESTUDIOS_CONCLUIDOS = (idioma == "es") ? "En curso" : "En curso"
  , this.DE = (idioma == "es") ? "De" : "De"
  , this.A = (idioma == "es") ? "A" : "A"
  , this.PAIS = (idioma == "es") ? "México" : "México"
  , this.MSJRFC = (idioma == "es") ? "Formato de RFC incorrecta" : "Formato de RFC incorrecta"
  , this.MSJCURP = (idioma == "es") ? "Fomato de CURP incorrecto" : "Fomato de CURP incorrecto"
  , this.TITULO_PRINCIPAL == (idioma == "es") ? "Nueva solicitud" : "Nueva solicitud"



};

var FILTRO_M_PUESTO = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Catalogo de puestos" : "Catalog of positions"
     this.FG_ACTIVO = (idioma == "es") ? "activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
     this.FG_ACTIVO_ph = (idioma == "es") ? "activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
    , this.FE_INACTIVO = (idioma == "es") ? "Fecha inactivo" : "Fecha inactivo"  //traduccion para las etiquetas y titulos de columnas 
    , this.FE_INACTIVO_ph = (idioma == "es") ? "Fecha inactivo" : "Fecha inactivo"  //traduccion para las etiquetas y titulos de columnas 
     , this.CL_PUESTO = (idioma == "es") ? "Clave del puesto" : "Clave del puesto"  //traduccion para las etiquetas y titulos de columnas 
     , this.CL_PUESTO_ph = (idioma == "es") ? "Clave del puesto" : "Clave del puesto"  //traduccion para las etiquetas y titulos de columnas 
     , this.NB_PUESTO = (idioma == "es") ? "Puesto" : "Puesto"  //traduccion para las etiquetas y titulos de columnas 
     , this.NB_PUESTO_ph = (idioma == "es") ? "Puesto" : "Puesto"  //traduccion para las etiquetas y titulos de columnas 
     , this.ID_PUESTO_JEFE = (idioma == "es") ? "ID del puesto del jefe" : "ID_PUESTO_JEFE"  //traduccion para las etiquetas y titulos de columnas 
     , this.ID_PUESTO_JEFE_ph = (idioma == "es") ? "ID del puesto del jefe" : "ID_PUESTO_JEFE"  //traduccion para las etiquetas y titulos de columnas 
     , this.ID_DEPARTAMENTO = (idioma == "es") ? "ID departamento" : "ID_DEPARTAMENTO"  //traduccion para las etiquetas y titulos de columnas 
     , this.ID_DEPARTAMENTO_ph = (idioma == "es") ? "ID departamento" : "ID_DEPARTAMENTO"  //traduccion para las etiquetas y titulos de columnas 
      , this.XML_CAMPOS_ADICIONALES = (idioma == "es") ? "Campos adicionales" : "XML_CAMPOS_ADICIONALES"  //traduccion para las etiquetas y titulos de columnas 
      , this.XML_CAMPOS_ADICIONALES_ph = (idioma == "es") ? "Campos adicionales" : "XML_CAMPOS_ADICIONALES"  //traduccion para las etiquetas y titulos de columnas 
      , this.ID_BITACORA = (idioma == "es") ? "ID bitacora" : "ID_BITACORA"  //traduccion para las etiquetas y titulos de columnas 
      , this.ID_BITACORA_ph = (idioma == "es") ? "ID bitacora" : "ID_BITACORA"  //traduccion para las etiquetas y titulos de columnas 
      , this.NB_DEPARTAMENTO = (idioma == "es") ? "Departamento" : "Departamento"  //traduccion para las etiquetas y titulos de columnas 
      , this.NB_DEPARTAMENTO_ph = (idioma == "es") ? "Departamento" : "Departamento"  //traduccion para las etiquetas y titulos de columnas 
      , this.CL_DEPARTAMENTO = (idioma == "es") ? "Clave de dapartamento" : "Clave de dapartamento"//traduccion para las etiquetas y titulos de columnas 
      , this.CL_DEPARTAMENTO_ph = (idioma == "es") ? "Clave de dapartamento" : "Clave de dapartamento"//traduccion para las etiquetas y titulos de columnas 
      , this.XML_CAMPOS_ADICIONALES_DEPARTAMENTO = (idioma == "es") ? "CAMPOS ADICIONALES DEPARTAMENTO" : "XML_CAMPOS_ADICIONALES_DEPARTAMENTO"  //traduccion para las etiquetas y titulos de columnas 
      , this.XML_CAMPOS_ADICIONALES_DEPARTAMENTO_ph = (idioma == "es") ? "CAMPOS ADICIONALES DEPARTAMENTO" : "XML_CAMPOS_ADICIONALES_DEPARTAMENTO"  //traduccion para las etiquetas y titulos de columnas 
};

var LOGIN = function () {
    this.TITULO_CORREO = (idioma == "es") ? "Escribe tu correo" : "Escribe tu correo"
   , this.PLACEHOLDERCORREO = (idioma == "es") ? "Correo" : "Correo"
   , this.USERREQUIRED = (idioma == "es") ? "El usuario es requerido" : "El usuario es requerido"
   , this.PASSWORDREQUIRED = (idioma == "es") ? "La contraseña es requerida." : "La contraseña es requerida."
    
};

var C_CATALOGO_LISTA = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Valores de Catálogos" : "Valores de Catálogos"
   , this.NB_CATALOGO_LISTA = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_CATALOGO_LISTA_ph = (idioma == "es") ? "Nombre" : "Nombre" //traduccion para los placeholder  
   , this.DS_CATALOGO_LISTA = (idioma == "es") ? "Descripcion" : "Descripción"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_CATALOGO_LISTA_ph = (idioma == "es") ? "Descripcion" : "Descripción" //traduccion para los placeholder  
   , this.ID_CATALOGO_TIPO = (idioma == "es") ? "Tipo de Catálogo" : "Tipo de Catálogo" //traduccion para las etiquetas y titulos de columnas 
   , this.ID_CATALOGO_TIPO_ph = (idioma == "es") ? "Tipo de Catálogo" : "Tipo de Catálogo" //traduccion para los placeholder  
};

var C_CATALOGO_VALOR = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Valores de Catálogo" : "Valores de Catálogo"
   , this.CL_CATALOGO_VALOR = (idioma == "es") ? "Clave" : "Clave"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_CATALOGO_VALOR_ph = (idioma == "es") ? "Clave" : "Clave" //traduccion para los placeholder  
   , this.NB_CATALOGO_VALOR = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_CATALOGO_VALOR_ph = (idioma == "es") ? "Nombre" : "Nombre" //traduccion para los placeholder  
   , this.DS_CATALOGO_VALOR = (idioma == "es") ? "Descripción" : "Descripción"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_CATALOGO_VALOR_ph = (idioma == "es") ? "Descripción" : "Descripción" //traduccion para los placeholder  
};

var SPE_OBTIENE_K_REQUISICION = function () {
    this.PANELINFOAREA = (idioma == "es") ? "Información del Área" : "Información del Área"
   , this.PANELCAUSAVACANTE = (idioma == "es") ? "Causa de la vacante" : "Causa de la vacante"
   , this.PANELAUTORIZACIONES = (idioma == "es") ? "Autorizaciones" : "Autorizaciones"
   , this.INFOPUESTONOSEENCUENTRA = (idioma == "es") ? 'Te informamos que si el puesto a cubrir no está en el catálogo de puestos, deberás dar click en el botón Notificar a RRHH, para que el área de Recursos Humanos cree el nuevo ' : 'Te informamos que si el puesto a cubrir no está en el catálogo de puestos, deberás dar click en el botón Notificar a RRHH, para que el área de Recursos Humanos cree el nuevo '
   , this.btnCandidatoIdeoneo = (idioma == "es") ? "Buscar candidato idóneo" : "Buscar candidato idóneo"
   , this.TITULO_PANTALLA = (idioma == "es") ? "Requisiciones" : "Requisiciones"
   , this.ID_REQUISICION = (idioma == "es") ? "ID Requisición" : "ID Requisición"  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_REQUISICION_ph = (idioma == "es") ? "ID Requisición" : "ID Requisición" //traduccion para los placeholder  
   , this.NO_REQUISICION = (idioma == "es") ? "No. Requisición" : "No.  Requisición"  //traduccion para las etiquetas y titulos de columnas 
   , this.NO_REQUISICION_ph = (idioma == "es") ? "No. Requisición" : "No.  Requisition" //traduccion para los placeholder  
   , this.FE_SOLICITUD = (idioma == "es") ? "Fecha de solicitud" : "Fecha de solicitud"  //traduccion para las etiquetas y titulos de columnas 
   , this.FE_SOLICITUD_ph = (idioma == "es") ? "Fecha de solicitud" : "Fecha de solicitud" //traduccion para los placeholder  
   , this.ID_PUESTO = (idioma == "es") ? "ID puesto" : "ID puesto"  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_PUESTO_ph = (idioma == "es") ? "ID puesto" : "ID puesto" //traduccion para los placeholder  
   , this.CL_ESTADO = (idioma == "es") ? "Estatus" : "Estatus"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_ESTADO_ph = (idioma == "es") ? "Estatus" : "Estatus" //traduccion para los placeholder  
   , this.CL_CAUSA = (idioma == "es") ? "Causa" : "Causa"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_CAUSA_ph = (idioma == "es") ? "Causa" : "Causa" //traduccion para los placeholder  
   , this.DS_CAUSA = (idioma == "es") ? "Descripción de causa" : "Descripción de causa"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_CAUSA_ph = (idioma == "es") ? "Descripción de causa" : "Descripción de causa"  //traduccion para los placeholder  
   , this.ID_NOTIFICACION = (idioma == "es") ? "ID notificación" : "ID notificación"   //traduccion para las etiquetas y titulos de columnas 
   , this.ID_NOTIFICACION_ph = (idioma == "es") ? "ID notificación" : "ID notificación"   //traduccion para los placeholder  
   , this.ID_SOLICITANTE = (idioma == "es") ? "ID solicitante" : "ID solicitante"//traduccion para las etiquetas y titulos de columnas 
   , this.ID_SOLICITANTE_ph = (idioma == "es") ? "ID solicitante" : "ID solicitante" //traduccion para los placeholder  
   , this.ID_AUTORIZA = (idioma == "es") ? "ID autoriza" : "ID autoriza"  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_AUTORIZA_ph = (idioma == "es") ? "ID autoriza" : "ID autoriza" //traduccion para los placeholder  
   , this.ID_VISTO_BUENO = (idioma == "es") ? "ID visto bueno" : "ID visto bueno" //traduccion para las etiquetas y titulos de columnas 
   , this.ID_VISTO_BUENO_ph = (idioma == "es") ? "ID visto bueno" : "ID visto bueno" //traduccion para los placeholder  
   , this.ID_EMPRESA = (idioma == "es") ? "ID Empresa" : "ID Empresa" //traduccion para las etiquetas y titulos de columnas 
   , this.ID_EMPRESA_ph = (idioma == "es") ? "ID Empresa" : "ID Empresa" //traduccion para los placeholder  
   , this.CL_EMPRESA = (idioma == "es") ? "Clave empresa" : "Clave empresa" //traduccion para las etiquetas y titulos de columnas 
   , this.CL_EMPRESA_ph = (idioma == "es") ? "Clave empresa" : "Clave empresa" //traduccion para los placeholder  
   , this.NB_EMPRESA = (idioma == "es") ? "Empresa" : "Empresa"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_EMPRESA_ph = (idioma == "es") ? "Empresa" : "Empresa"//traduccion para los placeholder  
   , this.NB_RAZON_SOCIAL = (idioma == "es") ? "Razón Social" : "Razón Social"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_RAZON_SOCIAL_ph = (idioma == "es") ? "Razón Social" : "Razón Social" //traduccion para los placeholder  
};

var SPE_OBTIENE_C_ROL_FUNCION = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Funciones por rol" : "Funciones por rol"
   , this.ID_ROL = (idioma == "es") ? "ID rol" : "ID rol" //traduccion para las etiquetas y titulos de columnas 
   , this.ID_ROL_ph = (idioma == "es") ? "ID rol" : "ID rol" //traduccion para los placeholder  
   , this.ID_FUNCION = (idioma == "es") ? "Pantalla" : "Pantalla"    //traduccion para las etiquetas y titulos de columnas 
   , this.ID_FUNCION_ph = (idioma == "es") ? "Pantalla" : "Pantalla" //traduccion para los placeholder  
   , this.NB_ROL = (idioma == "es") ? "Rol" : "Rol"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_ROL_ph = (idioma == "es") ? "Rol" : "Rol" //traduccion para los placeholder  
   , this.CL_ROL = (idioma == "es") ? "Clave del rol" : "Clave del rol"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_ROL_ph = (idioma == "es") ? "Clave del rol" : "Clave del rol"  //traduccion para los placeholder  
   , this.XML_AUTORIZACION = (idioma == "es") ? "XML_AUTORIZACION" : "XML_AUTORIZACION"  //traduccion para las etiquetas y titulos de columnas 
   , this.XML_AUTORIZACION_ph = (idioma == "es") ? "XML_AUTORIZACION" : "XML_AUTORIZACION" //traduccion para los placeholder  
   , this.CL_FUNCION = (idioma == "es") ? "Clave de la función" : "Clave de la función"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_FUNCION_ph = (idioma == "es") ? "Clave de la función" : "Clave de la función" //traduccion para los placeholder  
   , this.CL_TIPO_FUNCION = (idioma == "es") ? "Clave tipo función" : "Clave tipo función"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_TIPO_FUNCION_ph = (idioma == "es") ? "Clave tipo función" : "Clave tipo función" //traduccion para los placeholder  
   , this.NB_FUNCION = (idioma == "es") ? "Pantalla" : "Pantalla"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_FUNCION_ph = (idioma == "es") ? "Pantalla" : "Pantalla"  //traduccion para los placeholder  
   , this.NB_URL = (idioma == "es") ? "URL" : "URL"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_URL_ph = (idioma == "es") ? "URL" : "URL" //traduccion para los placeholder  
   , this.XML_CONFIGURACION = (idioma == "es") ? "XML_CONFIGURACION" : "XML_CONFIGURACION"  //traduccion para las etiquetas y titulos de columnas 
   , this.XML_CONFIGURACION_ph = (idioma == "es") ? "XML_CONFIGURACION" : "XML_CONFIGURACION" //traduccion para los placeholder  
   , this.ID_FUNCION_PADRE = (idioma == "es") ? "ID_FUNCION_PADRE" : "ID_FUNCION_PADRE"  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_FUNCION_PADRE_ph = (idioma == "es") ? "ID_FUNCION_PADRE" : "ID_FUNCION_PADRE" //traduccion para los placeholder  
   , this.DS_FILTRO = (idioma == "es") ? "DS_FILTRO" : "DS_FILTRO"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_FILTRO_ph = (idioma == "es") ? "DS_FILTRO" : "DS_FILTRO" //traduccion para los placeholder  
};


//poner en appConfiguracion.js 
var C_AREA_INTERES = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Catalogo Experiencia profesional" : "Catalogo Experiencia profesional "
   , this.ID_AREA_INTERES = (idioma == "es") ? "Numero" : "Numero"  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_AREA_INTERES_ph = (idioma == "es") ? "Numero" : "Numero" //traduccion para los placeholder  
   , this.CL_AREA_INTERES = (idioma == "es") ? "Clave Experiencia profesional" : "Clave Experiencia profesional"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_AREA_INTERES_ph = (idioma == "es") ? "Clave Experiencia profesional" : "Clave Experiencia profesional" //traduccion para los placeholder  
   , this.NB_AREA_INTERES = (idioma == "es") ? "Nombre Área" : "Nombre Área"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_AREA_INTERES_ph = (idioma == "es") ? "Nombre Área" : "Nombre Área"//traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder ,
   , this.EXCEPTION = (idioma == "es") ? "Esta acción no se puede completar porque la Experiencia profesional está actualmente asociada a un empleado." : "This action cannot be completed because the Professional experience is asociated with a employee."
    , this.EXCEPTION2 = (idioma == "es") ? "La descripción ya está en el catálogo." : "The description is already in the catalog"
       
};

var C_CARRERA_PROFESIONAL = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Catálogo profesionales" : "Catálogo profesionales"
    , this.ID_ESCOLARIDAD = (idioma == "es") ? "Id Escolaridad" : "Id Escolaridad"  //traduccion para las etiquetas y titulos de columnas 
   , this.ID_ESCOLARIDAD_ph = (idioma == "es") ? "Id Escolaridad" : "Id Escolaridad" //traduccion para los placeholder  
    , this.NB_ESCOLARIDAD = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_ESCOLARIDAD_ph = (idioma == "es") ? "Nombre de la carrera" : "Nombre de la carrera"//traduccion para los placeholder  
   , this.DS_ESCOLARIDAD = (idioma == "es") ? "Descripción" : "Descripción"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_ESCOLARIDAD_ph = (idioma == "es") ? "Descripción de la carrera" : "Descripción de la carrera" //traduccion para los placeholder  
   , this.CL_NIVEL_ESCOLARIDAD = (idioma == "es") ? "Clave Nivel escolaridad" : "Clave Nivel escolaridad"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_NIVEL_ESCOLARIDAD_ph = (idioma == "es") ? "Clave Nivel escolaridad" : "Clave Nivel escolaridad" //traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder  
   , this.ID_NIVEL_ESCOLARIDAD = (idioma == "es") ? "Id Nivel escolaridad" : "Id Nivel escolaridad" //traduccion para las etiquetas y titulos de columnas 
   , this.ID_NIVEL_ESCOLARIDAD_ph = (idioma == "es") ? "Id Nivel escolaridad" : "Id Nivel escolaridad"  //traduccion para los placeholder  
   , this.CL_INSTITUCION = (idioma == "es") ? "Clave institución" : "Clave institución"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_INSTITUCION_ph = (idioma == "es") ? "Clave institución" : "Clave institución" //traduccion para los placeholder  


};

var C_CARRERA_POSGRADO = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Catálogo de posgrados" : "Catálogo posgrados"
    , this.ID_ESCOLARIDAD = (idioma == "es") ? "Id Escolaridad" : "Id Escolaridad" //traduccion para las etiquetas y titulos de columnas 
   , this.ID_ESCOLARIDAD_ph = (idioma == "es") ? "Id Escolaridad" : "Id Escolaridad"  //traduccion para los placeholder  
   , this.NB_ESCOLARIDAD = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_ESCOLARIDAD_ph = (idioma == "es") ? "Nombre de la carrera" : "Nombre de la carrera" //traduccion para los placeholder  
   , this.DS_ESCOLARIDAD = (idioma == "es") ? "Descripción" : "Descripción"  //traduccion para las etiquetas y titulos de columnas 
   , this.DS_ESCOLARIDAD_ph = (idioma == "es") ? "Descripción de la carrera" : "Descripción de la carrera" //traduccion para los placeholder  
   , this.CL_NIVEL_ESCOLARIDAD = (idioma == "es") ? "Clave Nivel escolaridad" : "Clave Nivel escolaridad" //traduccion para las etiquetas y titulos de columnas 
   , this.CL_NIVEL_ESCOLARIDAD_ph = (idioma == "es") ? "Clave Nivel escolaridad" : "Clave Nivel escolaridad"  //traduccion para los placeholder  
   , this.FG_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.FG_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder  
   , this.ID_NIVEL_ESCOLARIDAD = (idioma == "es") ? "Id Nivel escolaridad" : "Id Nivel escolaridad" //traduccion para las etiquetas y titulos de columnas 
   , this.ID_NIVEL_ESCOLARIDAD_ph = (idioma == "es") ? "Id Nivel escolaridad" : "Id Nivel escolaridad"  //traduccion para los placeholder  
   , this.CL_INSTITUCION = (idioma == "es") ? "Clave institución" : "Clave institución"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_INSTITUCION_ph = (idioma == "es") ? "Clave institución" : "Clave institución" //traduccion para los placeholder  
};

//poner en appConfiguracion.js 
var C_EMPRESA = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Empresa" : "Empresa"
   , this.CL_EMPRESA = (idioma == "es") ? "Clave" : "Clave"  //traduccion para las etiquetas y titulos de columnas 
   , this.CL_EMPRESA_ph = (idioma == "es") ? "Clave" : "Clave" //traduccion para los placeholder  
   , this.NB_EMPRESA = (idioma == "es") ? "Nombre" : "Nombre"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_EMPRESA_ph = (idioma == "es") ? "Nombre" : "Nombre" //traduccion para los placeholder  
   , this.NB_RAZON_SOCIAL = (idioma == "es") ? "Razón Social" : "Razón Social"  //traduccion para las etiquetas y titulos de columnas 
   , this.NB_RAZON_SOCIAL_ph = (idioma == "es") ? "Razón Social" : "Razón Social" //traduccion para los placeholder  
};

var SELECCION_PERSONAL = function () {
    this.TITULO_PANTALLA = (idioma == "es") ? "Selección de Personal" : "Selección de Personal"
    this.MENSAJESELECCION = (idioma == "es") ? "Candidato sin registrar, verifique." : "Candidato sin registrar, verifique."
};

var M_EMPLEADO = function () {
    this.M_EMPLEADO_TITULO_PANTALLA = (idioma == "es") ? "Inventario de personal" : "Personal inventory"

     , this.M_EMPLEADO_NB_EMPLEADO_COMPLETO = (idioma == "es") ? "Nombre empleado" : "Employee name"  //traduccion para las etiquetas y titulos de columnas 
     , this.M_EMPLEADO_NB_EMPLEADO_COMPLETO_ph = (idioma == "es") ? "Nombre empleado" : "Employee name" //traduccion para los placeholder  
     , this.M_EMPLEADO_CL_EMPLEADO = (idioma == "es") ? "Clave empleado" : "Employee key"  //traduccion para las etiquetas y titulos de columnas 
     , this.M_EMPLEADO_CL_EMPLEADO_ph = (idioma == "es") ? "Clave empleado" : "Employee key"  //traduccion para los placeholder  
   , this.M_EMPLEADO_ID_EMPLEADO = (idioma == "es") ? "Id empleado" : "Employee key"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_ID_EMPLEADO_ph = (idioma == "es") ? "Id empleado" : "Employee key"//traduccion para los placeholder  
   , this.M_EMPLEADO_NB_EMPLEADO = (idioma == "es") ? "Nombre empleado" : "Employee name"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NB_EMPLEADO_ph = (idioma == "es") ? "Nombre empleado" : "Employee name" //traduccion para los placeholder  
   , this.M_EMPLEADO_NB_APELLIDO_PATERNO = (idioma == "es") ? "Apellido paterno" : "Last name"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NB_APELLIDO_PATERNO_ph = (idioma == "es") ? "Apellido paterno" : "Last name"//traduccion para los placeholder  
   , this.M_EMPLEADO_NB_APELLIDO_MATERNO = (idioma == "es") ? "Apellido materno" : "Last Name"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NB_APELLIDO_MATERNO_ph = (idioma == "es") ? "Apellido materno" : "Last Name"//traduccion para los placeholder  
   , this.M_EMPLEADO_CL_ESTADO_EMPLEADO = (idioma == "es") ? "Clave estado" : "State key"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_ESTADO_EMPLEADO_ph = (idioma == "es") ? "Clave estado" : "State key" //traduccion para los placeholder  
   , this.M_EMPLEADO_CL_GENERO = (idioma == "es") ? "Clave genero" : "Gender key"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_GENERO_ph = (idioma == "es") ? "Clave genero" : "Gender key"//traduccion para los placeholder  
   , this.M_EMPLEADO_CL_ESTADO_CIVIL = (idioma == "es") ? "Estado civil" : "Marital status"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_ESTADO_CIVIL_ph = (idioma == "es") ? "Estado civil" : "Marital status"//traduccion para los placeholder  
   , this.M_EMPLEADO_NB_CONYUGUE = (idioma == "es") ? "Nombre conyuge" : "Spouse name"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NB_CONYUGUE_ph = (idioma == "es") ? "Nombre conyuge" : "Spouse name" //traduccion para los placeholder  
   , this.M_EMPLEADO_CL_RFC = (idioma == "es") ? "RFC" : "RFC"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_RFC_ph = (idioma == "es") ? "RFC" : "RFC"//traduccion para los placeholder  
   , this.M_EMPLEADO_CL_CURP = (idioma == "es") ? "CURP" : "CURP"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_CURP_ph = (idioma == "es") ? "CURP" : "CURP"//traduccion para los placeholder  
   , this.M_EMPLEADO_CL_NSS = (idioma == "es") ? "Clave seguro social" : "Clave seguro social"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_NSS_ph = (idioma == "es") ? "Clave seguro social" : "Clave seguro social" //traduccion para los placeholder  
   , this.M_EMPLEADO_CL_TIPO_SANGUINEO = (idioma == "es") ? "Tipo sanguineo" : "Tipo sanguineo"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_TIPO_SANGUINEO_ph = (idioma == "es") ? "Tipo sanguineo" : "Tipo sanguineo"//traduccion para los placeholder  
   , this.M_EMPLEADO_CL_NACIONALIDAD = (idioma == "es") ? "Nacionalidad" : "Nacionalidad"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_NACIONALIDAD_ph = (idioma == "es") ? "Nacionalidad" : "Nacionalidad" //traduccion para los placeholder  
   , this.M_EMPLEADO_NB_PAIS = (idioma == "es") ? "Pais" : "Country"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NB_PAIS_ph = (idioma == "es") ? "Pais" : "Country" //traduccion para los placeholder  
   , this.M_EMPLEADO_NB_ESTADO = (idioma == "es") ? "Nombre estado" : "State name"//traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NB_ESTADO_ph = (idioma == "es") ? "Nombre estado" : "State name"//traduccion para los placeholder  
   , this.M_EMPLEADO_NB_MUNICIPIO = (idioma == "es") ? "Ciudad" : "City"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NB_MUNICIPIO_ph = (idioma == "es") ? "Ciudad" : "City"  //traduccion para los placeholder  
   , this.M_EMPLEADO_NB_COLONIA = (idioma == "es") ? "Colonia" : "Suburb"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NB_COLONIA_ph = (idioma == "es") ? "Colonia" : "Suburb"  //traduccion para los placeholder  
   , this.M_EMPLEADO_NB_CALLE = (idioma == "es") ? "Nombre calle" : "Street name"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NB_CALLE_ph = (idioma == "es") ? "Nombre calle" : "Street name"//traduccion para los placeholder  
   , this.M_EMPLEADO_NO_INTERIOR = (idioma == "es") ? "Numero interior" : "Number"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NO_INTERIOR_ph = (idioma == "es") ? "Numero interior" : "Number" //traduccion para los placeholder  
   , this.M_EMPLEADO_NO_EXTERIOR = (idioma == "es") ? "Numero exterior" : "Number"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_NO_EXTERIOR_ph = (idioma == "es") ? "Numero exterior" : "Number" //traduccion para los placeholder  
   , this.M_EMPLEADO_CL_CODIGO_POSTAL = (idioma == "es") ? "Codigo postal" : "Zip code"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_CODIGO_POSTAL_ph = (idioma == "es") ? "Codigo postal" : "Zip code"//traduccion para los placeholder  
   , this.M_EMPLEADO_XML_TELEFONOS = (idioma == "es") ? "Telefonos" : "Telephones"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_XML_TELEFONOS_ph = (idioma == "es") ? "Telefonos" : "Telephones" //traduccion para los placeholder  
   , this.M_EMPLEADO_CL_CORREO_ELECTRONICO = (idioma == "es") ? "Correo electronico" : "Email"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_CL_CORREO_ELECTRONICO_ph = (idioma == "es") ? "Correo electronico" : "Email"  //traduccion para los placeholder  
   , this.M_EMPLEADO_FE_NACIMIENTO = (idioma == "es") ? "Fecha de nacimiento" : "Date born"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_FE_NACIMIENTO_ph = (idioma == "es") ? "Fecha de nacimiento" : "Date born" //traduccion para los placeholder  
   , this.M_EMPLEADO_DS_LUGAR_NACIMIENTO = (idioma == "es") ? "Lugar de Nacimiento" : "Place of birth"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_DS_LUGAR_NACIMIENTO_ph = (idioma == "es") ? "Lugar de Nacimiento" : "Place of birth" //traduccion para los placeholder  
   , this.M_EMPLEADO_FE_ALTA = (idioma == "es") ? "Fecha alta" : "Date up"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_FE_ALTA_ph = (idioma == "es") ? "Fecha alta" : "Date up" //traduccion para los placeholder  
   , this.M_EMPLEADO_FE_BAJA = (idioma == "es") ? "Fecha baja" : "Date down"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_FE_BAJA_ph = (idioma == "es") ? "Fecha baja" : "Date down" //traduccion para los placeholder  
   , this.M_EMPLEADO_ID_PUESTO = (idioma == "es") ? "Id puesto" : "Position key"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_ID_PUESTO_ph = (idioma == "es") ? "Id puesto" : "Position key" //traduccion para los placeholder  
   , this.M_EMPLEADO_MN_SUELDO = (idioma == "es") ? "Monto Sueldo" : "Salary amount"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_MN_SUELDO_ph = (idioma == "es") ? "Monto Sueldo" : "Salary amount"//traduccion para los placeholder  
   , this.M_EMPLEADO_MN_SUELDO_VARIABLE = (idioma == "es") ? "Monto sueldo variable" : "Salary amount"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_MN_SUELDO_VARIABLE_ph = (idioma == "es") ? "Monto sueldo variable" : "Amount variable salary" //traduccion para los placeholder  
   , this.M_EMPLEADO_DS_SUELDO_COMPOSICION = (idioma == "es") ? "Monto sueldo composicion" : "Salary compose amount" //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_DS_SUELDO_COMPOSICION_ph = (idioma == "es") ? "Monto sueldo composicion" : "Salary compose amount" //traduccion para los placeholder  
   , this.M_EMPLEADO_ID_CANDIDATO = (idioma == "es") ? "Id candidato" : "Candidate key"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_ID_CANDIDATO_ph = (idioma == "es") ? "Id candidato" : "Candidate key" //traduccion para los placeholder  
   , this.M_EMPLEADO_ID_EMPRESA = (idioma == "es") ? "Id empresa" : "Employee key"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_ID_EMPRESA_ph = (idioma == "es") ? "Id empresa" : "Employee key" //traduccion para los placeholder  
   , this.M_EMPLEADO_XML_CAMPOS_ADICIONALES = (idioma == "es") ? "Campos adicionales" : "Aditional fields"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_XML_CAMPOS_ADICIONALES_ph = (idioma == "es") ? "Campos adicionales" : "Aditional fields"//traduccion para los placeholder  
   , this.M_EMPLEADO_ACTIVO = (idioma == "es") ? "Activo" : "Activo"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_EMPLEADO_ACTIVO_ph = (idioma == "es") ? "Activo" : "Activo" //traduccion para los placeholder ,
   , this.EXCEPTION = (idioma == "es") ? "Esta acción no se puede completar porque el Área de interés está actualmente asociada a un empleado." : "This action cannot be completed because the Interested Area is asociated with a employee."
   , this.EXCEPTION2 = (idioma == "es") ? "La descripción ya está en el catálogo." : "The description is already in the catalog"


    ///////// MP PUESTO
   , this.M_PUESTO_ACTIVO = (idioma == "es") ? "Activo" : "Active"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_PUESTO_ACTIVO_ph = (idioma == "es") ? "Activo" : "Active"   //traduccion para los placeholder  
   , this.M_PUESTO_FE_INACTIVO = (idioma == "es") ? "Fecha Inactivo" : "Date inactive"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_PUESTO_FE_INACTIVO_ph = (idioma == "es") ? "Fecha Inactivo" : "Date inactive"  //traduccion para los placeholder  
   , this.M_PUESTO_CL_PUESTO = (idioma == "es") ? "Clave puesto" : "Position key"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_PUESTO_CL_PUESTO_ph = (idioma == "es") ? "Clave puesto" : "Position key" //traduccion para los placeholder  
   , this.M_PUESTO_NB_PUESTO = (idioma == "es") ? "Nombre puesto" : "Position name"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_PUESTO_NB_PUESTO_ph = (idioma == "es") ? "Nombre puesto" : "Position name"  //traduccion para los placeholder  
   , this.M_PUESTO_ID_PUESTO_JEFE = (idioma == "es") ? "Id puesto jefe" : "Boss position key"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_PUESTO_ID_PUESTO_JEFE_ph = (idioma == "es") ? "Id puesto jefe" : "Boss position key"//traduccion para los placeholder  
   , this.M_PUESTO_ID_DEPARTAMENTO = (idioma == "es") ? "Id departamento" : "Department key"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_PUESTO_ID_DEPARTAMENTO_ph = (idioma == "es") ? "Id departamento" : "Department key" //traduccion para los placeholder  
   , this.M_PUESTO_XML_CAMPOS_ADICIONALES = (idioma == "es") ? "Campos adicionales" : "Aditional fields" //traduccion para las etiquetas y titulos de columnas 
   , this.M_PUESTO_XML_CAMPOS_ADICIONALES_ph = (idioma == "es") ? "Campos adicionales" : "Aditional fields"  //traduccion para los placeholder  
   , this.M_PUESTO_ID_BITACORA = (idioma == "es") ? "Id bitacora" : "Id bitacora"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_PUESTO_ID_BITACORA_ph = (idioma == "es") ? "Id bitacora" : "Id bitacora" //traduccion para los placeholder  
  
    ///CC CANDIDATO

     , this.C_CANDIDATO_NB_CANDIDATO = (idioma == "es") ? "Nombre candidato" : "Candidate name"  //traduccion para las etiquetas y titulos de columnas 
     , this.C_CANDIDATO_NB_CANDIDATO_ph = (idioma == "es") ? "Nombre candidato" : "Candidate name" //traduccion para los placeholder  
     , this.C_CANDIDATO_NB_APELLIDO_PATERNO = (idioma == "es") ? "Apellido paterno" : "Last Name"  //traduccion para las etiquetas y titulos de columnas 
     , this.C_CANDIDATO_NB_APELLIDO_PATERNO_ph = (idioma == "es") ? "Apellido paterno" : "Last Name"   //traduccion para los placeholder  
   , this.C_CANDIDATO_NB_APELLIDO_MATERNO = (idioma == "es") ? "Apellido materno" : "Last Name"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NB_APELLIDO_MATERNO_ph = (idioma == "es") ? "Apellido materno" : "Last Name"//traduccion para los placeholder  
   , this.C_CANDIDATO_CL_GENERO = (idioma == "es") ? "Clave genero" : "Gender key"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_GENERO_ph = (idioma == "es") ? "Clave genero" : "Gender key"  //traduccion para los placeholder  
   , this.C_CANDIDATO_CL_RFC = (idioma == "es") ? "RFC" : "RFC"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_RFC_ph = (idioma == "es") ? "RFC" : "RFC" //traduccion para los placeholder  
   , this.C_CANDIDATO_CL_CURP = (idioma == "es") ? "CURP" : "CURP"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_CURP_ph = (idioma == "es") ? "CURP" : "CURP"//traduccion para los placeholder  
   , this.C_CANDIDATO_CL_ESTADO_CIVIL = (idioma == "es") ? "Clave estado civil" : "Marital status"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_ESTADO_CIVIL_ph = (idioma == "es") ? "Clave estado civil" : "Marital status"//traduccion para los placeholder  
   , this.C_CANDIDATO_NB_CONYUGUE = (idioma == "es") ? "Nombre conyuge" : "Spouse name"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NB_CONYUGUE_ph = (idioma == "es") ? "Nombre conyuge" : "Spouse name"//traduccion para los placeholder  
   , this.C_CANDIDATO_CL_NSS = (idioma == "es") ? "Clave seguro social" : "NSS"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_NSS_ph = (idioma == "es") ? "Clave seguro social" : "NSS" //traduccion para los placeholder  
   , this.C_CANDIDATO_CL_TIPO_SANGUINEO = (idioma == "es") ? "Tipo sanguineo" : "Blood type"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_TIPO_SANGUINEO_ph = (idioma == "es") ? "Tipo sanguineo" : "Blood type"  //traduccion para los placeholder  
   , this.C_CANDIDATO_NB_PAIS = (idioma == "es") ? "Pais" : "Country"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NB_PAIS_ph = (idioma == "es") ? "Pais" : "Country" //traduccion para los placeholder  
   , this.C_CANDIDATO_NB_ESTADO = (idioma == "es") ? "Nombre estado" : "State name"//traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NB_ESTADO_ph = (idioma == "es") ? "Nombre estado" : "State name"//traduccion para los placeholder  
   , this.C_CANDIDATO_NB_MUNICIPIO = (idioma == "es") ? "Ciudad" : "City"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NB_MUNICIPIO_ph = (idioma == "es") ? "Ciudad" : "City"  //traduccion para los placeholder  
   , this.C_CANDIDATO_NB_COLONIA = (idioma == "es") ? "Colonia" : "Suburb"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NB_COLONIA_ph = (idioma == "es") ? "Colonia" : "Suburb"  //traduccion para los placeholder  
   , this.C_CANDIDATO_NB_CALLE = (idioma == "es") ? "Nombre calle" : "Street name"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NB_CALLE_ph = (idioma == "es") ? "Nombre calle" : "Street name"//traduccion para los placeholder  
   , this.C_CANDIDATO_NO_INTERIOR = (idioma == "es") ? "Numero interior" : "Number"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NO_INTERIOR_ph = (idioma == "es") ? "Numero interior" : "Number" //traduccion para los placeholder  
   , this.C_CANDIDATO_NO_EXTERIOR = (idioma == "es") ? "Numero exterior" : "Number"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NO_EXTERIOR_ph = (idioma == "es") ? "Numero exterior" : "Number" //traduccion para los placeholder  
   , this.C_CANDIDATO_CL_CODIGO_POSTAL = (idioma == "es") ? "Codigo postal" : "Zip code"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_CODIGO_POSTAL_ph = (idioma == "es") ? "Codigo postal" : "Zip code"//traduccion para los placeholder  
   , this.C_CANDIDATO_CL_CORREO_ELECTRONICO = (idioma == "es") ? "Correo electronico" : "Email"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_CORREO_ELECTRONICO_ph = (idioma == "es") ? "Correo electronico" : "Email"  //traduccion para los placeholder  
   , this.C_CANDIDATO_FE_NACIMIENTO = (idioma == "es") ? "Fecha de nacimiento" : "Date born"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_FE_NACIMIENTO_ph = (idioma == "es") ? "Fecha de nacimiento" : "Date born" //traduccion para los placeholder  
   , this.C_CANDIDATO_DS_LUGAR_NACIMIENTO = (idioma == "es") ? "Lugar de Nacimiento" : "Place of birth"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_DS_LUGAR_NACIMIENTO_ph = (idioma == "es") ? "Lugar de Nacimiento" : "Place of birth" //traduccion para los placeholder  
   , this.C_CANDIDATO_MN_SUELDO = (idioma == "es") ? "Monto sueldo" : "Salary amount"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_MN_SUELDO_ph = (idioma == "es") ? "Monto sueldo" : "Salary amount" //traduccion para los placeholder  
   , this.C_CANDIDATO_CL_NACIONALIDAD = (idioma == "es") ? "Clave nacionalidad" : "Nacionality key"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_NACIONALIDAD_ph = (idioma == "es") ? "Clave nacionalidad" : "Nacionality key" //traduccion para los placeholder  
   , this.C_CANDIDATO_DS_NACIONALIDAD = (idioma == "es") ? "Descripcion nacionalidad" : "Nacionality description"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_DS_NACIONALIDAD_ph = (idioma == "es") ? "Descripcion nacionalidad" : "Nacionality description" //traduccion para los placeholder  
   , this.C_CANDIDATO_NB_LICENCIA = (idioma == "es") ? "Nombre licencia" : "License name"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_NB_LICENCIA_ph = (idioma == "es") ? "Nombre licencia" : "License name"//traduccion para los placeholder  
   , this.C_CANDIDATO_DS_VEHICULO = (idioma == "es") ? "Descripcion Vehiculo" : "Car description"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_DS_VEHICULO_ph = (idioma == "es") ? "Descripcion Vehiculo" : "Car description" //traduccion para los placeholder  
   , this.C_CANDIDATO_CL_CARTILLA_MILITAR = (idioma == "es") ? "Clave cartilla militar" : "Key military ID" //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_CARTILLA_MILITAR_ph = (idioma == "es") ? "Clave cartilla militar" : "Key military ID" //traduccion para los placeholder  
   , this.C_CANDIDATO_CL_CEDULA_PROFESIONAL = (idioma == "es") ? "Clave cedula profesional" : "Key professional license" //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_CEDULA_PROFESIONAL_ph = (idioma == "es") ? "Clave cedula profesional" : "Key professional license" //traduccion para los placeholder  
   , this.C_CANDIDATO_XML_TELEFONOS = (idioma == "es") ? "Telefonos" : "Telephones"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_XML_TELEFONOS_ph = (idioma == "es") ? "Telefonos" : "Telephones" //traduccion para los placeholder  
   , this.C_CANDIDATO_XML_INGRESOS = (idioma == "es") ? "Ingresos" : "Income"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_XML_INGRESOS_ph = (idioma == "es") ? "Ingresos" : "Income"//traduccion para los placeholder  
   , this.C_CANDIDATO_XML_EGRESOS = (idioma == "es") ? "Egresos" : "expenses"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_XML_EGRESOS_ph = (idioma == "es") ? "Egresos" : "expenses"//traduccion para los placeholder ,
   , this.C_CANDIDATO_XML_PATRIMONIO = (idioma == "es") ? "Patrimonio" : "Heritage"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_XML_PATRIMONIO_ph = (idioma == "es") ? "Patrimonio" : "Heritage" //traduccion para los placeholder  
   , this.C_CANDIDATO_DS_DISPONIBILIDAD = (idioma == "es") ? "Descripcion disponibilidad" : "Description availability"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_DS_DISPONIBILIDAD_ph = (idioma == "es") ? "Descripcion disponibilidad" : "Description availability"//traduccion para los placeholder  
   , this.C_CANDIDATO_CL_DISPONIBILIDAD_VIAJE = (idioma == "es") ? "Clave disponibilidad viaje" : "Travel availability key" //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_CL_DISPONIBILIDAD_VIAJE_ph = (idioma == "es") ? "Clave disponibilidad viaje" : "Travel availability key" //traduccion para los placeholder  
   , this.C_CANDIDATO_ACTIVO = (idioma == "es") ? "Activo" : "Active"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_CANDIDATO_ACTIVO_ph = (idioma == "es") ? "Activo" : "Active"  //traduccion para los placeholder  
  
    //CC_empresa

   , this.C_EMPRESA_CL_EMPRESA = (idioma == "es") ? "Clave empresa" : "Company key"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_EMPRESA_CL_EMPRESA_ph = (idioma == "es") ? "Clave empresa" : "Company key" //traduccion para los placeholder  
   , this.C_EMPRESA_NB_EMPRESA = (idioma == "es") ? "Nombre empresa" : "Company name" //traduccion para las etiquetas y titulos de columnas 
   , this.C_EMPRESA_NB_EMPRESA_ph = (idioma == "es") ? "Nombre empresa" : "Company name"//traduccion para los placeholder  
   , this.C_EMPRESA_NB_RAZON_SOCIAL = (idioma == "es") ? "Nombre razon social" : "name business name"  //traduccion para las etiquetas y titulos de columnas 
   , this.C_EMPRESA_NB_RAZON_SOCIAL_ph = (idioma == "es") ? "Nombre razon social" : "name business name"   //traduccion para los placeholder  

		/////////////////M_DEPARTAMENTO


      , this.M_DEPARTAMENTO_ACTIVO = (idioma == "es") ? "Activo" : "Active"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_DEPARTAMENTO_ACTIVO_ph = (idioma == "es") ? "Activo" : "Active"  //traduccion para los placeholder  
   , this.M_DEPARTAMENTO_FE_INACTIVO = (idioma == "es") ? "Fecha inactivo" : "Inactive date" //traduccion para las etiquetas y titulos de columnas 
   , this.M_DEPARTAMENTO_FE_INACTIVO_ph = (idioma == "es") ? "Fecha inactivo" : "Inactive date"//traduccion para los placeholder  
   , this.M_DEPARTAMENTO_CL_DEPARTAMENTO_SOCIAL = (idioma == "es") ? "Clave departamento social" : "Key social department"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_DEPARTAMENTO_CL_DEPARTAMENTO_ph = (idioma == "es") ? "Clave departamento social" : "Key social department"   //traduccion para los placeholder  
   , this.M_DEPARTAMENTO_NB_DEPARTAMENTO = (idioma == "es") ? "Nombre departamento" : "Department name" //traduccion para las etiquetas y titulos de columnas 
   , this.M_DEPARTAMENTO_NB_DEPARTAMENTO_ph = (idioma == "es") ? "Nombre departamento" : "Department name" //traduccion para los placeholder  
   , this.M_DEPARTAMENTO_XML_CAMPOS_ADICIONALES = (idioma == "es") ? "Campos adicionales" : "Aditional fields"  //traduccion para las etiquetas y titulos de columnas 
   , this.M_DEPARTAMENTO_XML_CAMPOS_ADICIONALES_ph = (idioma == "es") ? "Campos adicionales" : "Aditional fields"  //traduccion para los placeholder  


};







