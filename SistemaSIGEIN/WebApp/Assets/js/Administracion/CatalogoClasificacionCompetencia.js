//poner en appPrincipal.js 



//poner en (Nombre igual a Nuestro HTML).js 
app.controller('C_CLASIFICACION_COMPETENCIAController', function ($scope, $http, $modal) {

   // CONFIGURAR = Configuracion();
    //habilita la animacion del popup para que se despliegue de arriba para abajo  
    $scope.animationsEnabled = true;
    //se instancia el objeto que contiene el titulo en español y ingles de las columnas  
    
    
    $scope.titulo = C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA;
    $scope.btnActivarDesactivar = Config.BOTONACTIVARDESACTIVAR;
    $scope.btnAgregar = Config.BOTONAGREGAR;
    $scope.btnModificar = Config.BOTONMODIFICAR;
    $scope.btnEliminar = Config.BOTONELIMINAR;
    ////////////////////////////////////////////////////////////// 
    //inicia evento para consultar los registros  
    //SE CARGA EL TEMPLATE DEL TOOLBAR DEL GRID DONDE IRAN LOS BOTONES DE EDICION Y EXPORTACION  
    //$scope.templateGrid_ClasificacionCompetencias = function () {
    //    return "<div class=\"toolbar\"> " +
 	//		"<div style=\"float:right\">  " +
 	//			"<div style=\"float:left\">" +
 	//				"<button id=\"btnExportarExcel_Grid_ClasificacionCompetencias\" class=\"btn btn-link FontNegro\" onclick=\"ExportarExcel('Grid_ClasificacionCompetencias')\"  > " +
 	//					"<i class=\"fa fa-file-o\" style=\"font-size: 17px;\"></i>  " +
 	//					"" + GridKendo.BOTONEXPORTAREXCEL + " " +
 	//				"</button> " +
 	//			"</div>" +
 	//		"	</div>  " +
 	//	"</div>  "
    //};
    $scope.BOTONEXPORTAREXCEL = GridKendo.BOTONEXPORTAREXCEL
    //se carga el grid  
    $scope.CargarGrid_ClasificacionCompetencias = function () {
        NProgress.start();  //muestra el progreso en la pagina  
        $(document).ready(function () {
            $("#Grid_ClasificacionCompetencias").kendoGrid({
                allowCopy: true//permite copiar los datos para copiar al portapapeles   
                , allowCopy: {
                    delimeter: "," //delimitador cuando se copien los datos del grid   
                }
               ,resizable: true //sirve para mover las columnas
               , excel: {
                   fileName: "ClasificacionCompetencias.xlsx" //nombre del archivo a exportar   
                       , allPages: true // indica si se exportan todas las paginas por default esta en false   
                   //,proxyURL: "//demos.telerik.com/kendo-ui/service/export",   
                       , filterable: true //permite que la exportacion sea por lo que se esta filtrando   
               }
               // , toolbar: kendo.template($scope.templateGrid_ClasificacionCompetencias())
                , dataSource: {
                    type: "JSON", // JSON, JSONP, OData or XML).   
                    transport: {
                        read: function (options) {
                            $.ajax({
                                type: "POST"
                                , url: "ws/OperacionesGral.svc/Get_C_CLASIFICACION_COMPETENCIA"
                                //, data: JSON.stringify({ CL_ESTADO: var_cl_estado })  //sirve para envia los parametros se le indica el nombre de la columna mas dos puntos : luego el valor de la columna 
                                //, contentType: "application/json; charset=utf-8"    
                                , dataType: "json"
                                , success: function (result) {
                                    options.success(result);
                                    NProgress.done();  //se termina el progreso en la pagina 
                                }
                            });
                        }
                    }
                   , schema: {
                       model: {
                           fields: {
                               CL_CLASIFICACION: { type: "string" }
                               , CL_TIPO_COMPETENCIA: { type: "string" }
                               , CL_USUARIO_APP_CREA: { type: "string" }
                               , CL_USUARIO_APP_MODIFICA: { type: "string" }
                               , DS_CLASIFICACION_COMPETENCIA: { type: "string" }
                               , DS_NOTAS_CLASIFICACION: { type: "string" }
                               , FG_ACTIVO: { type: "boolean" }
                               , FE_CREACION: { type: "date" }
                               , FE_MODIFICACION: { type: "date" }
                               , ID_CLASIFICACION_COMPETENCIA: { type: "number" }
                               , NB_CLASIFICACION_COMPETENCIA: { type: "string" }
                               , NB_PROGRAMA_CREA: { type: "string" }
                               , NB_PROGRAMA_MODIFICA: { type: "string" }
                           }
                       }
                   }
                }
               , filterable: {
                   mode: "row" //Indica si se mostrara el filtro en el encabezado de la columna  si se quita no toma los valores que se especifican en las mismas columnas 
                   , extra: false
                   , filterable: true
                   , messages: {
                       and: GridKendo.AND
                       , cancel: GridKendo.CANCEL
                       , checkAll: GridKendo.CHECKALL
                       , clear: GridKendo.CLEAR
                       , IsEqualTo: GridKendo.ISEQUALTO
                       , filter: GridKendo.FILTER
                       , info: GridKendo.INFO
                       , isFalse: GridKendo.ISFALSE
                       , isTrue: GridKendo.ISTRUE
                       , operator: GridKendo.OPERATOR
                       , or: GridKendo.OR
                       , SelectValue: GridKendo.SELECTVALUE
                       , value: GridKendo.VALUE
                       , contains: GridKendo.CONTAINS
                       , doesnotcontain: GridKendo.DOESNOTCONTAIN
                   }
                   , operators: {
                       string: {
                           eq: GridKendo.EQ
                               , contains: GridKendo.CONTAINS
                               , doesnotcontain: GridKendo.DOESNOTCONTAIN
                               , endswith: GridKendo.ENDSWITH
                               , neq: GridKendo.NEQ
                               , startswith: GridKendo.STARTSWITH
                       }
                   }
               }
               , columnMenu: true //indica si se muestra en la columna un menu con el ordinamiento etc etc.   
               , columnMenu: {
                   messages: {
                       columns: GridKendo.COLUMNS  //Configuracion.Nombre //viene del archivo de Configuracion.js   
                       , done: GridKendo.DONE
                       , filter: GridKendo.FILTER
                       , lock: GridKendo.LOCK
                       , settings: GridKendo.SETTINGS
                       , sortAscending: GridKendo.SORTASCENDING
                       , sortDescending: GridKendo.SORTDESCENDING
                       , unlock: GridKendo.UNLOCK
                   }
               }
               , scrollable: true // indica si aparece el scroll en el grid   
               , scrollable: { virtual: true } // indica si hace un scrooll virtual es decir que con el scroll pase de una pagina a otra   
               , selectable: "row"
               , sortable: true //sirve para ordenar   
               , navigatable: true //indica si se puede navegar desde el teclado   
               , sortable: {
                   mode: "multiple" // valores  single  multiple					   
               }
               , messages: {
                   noRecords: GridKendo.NORECORDS //mensaje cuando no hay registros					   
               }
               , groupable: {
                   showFooter: true //si muestra el pie del grid cuando esten agrupados   
                   , messages: {
                       empty: GridKendo.GRUPABLEEMPTY //mensaje cuando se muestra la agrupacion de las columnas en el grid   
                   }
               }
               , groupable: false // agrupacion por columnas en la parte del grid	 debe de ir despues de la definicion de los mensaje de lo contrario si le pones el false no te lo estara tomando en cuenta			   
                //si se quita este codigo pageable ya no estara paginando el grid   
               , pageable: {
                   pageSize: 10
                   , refresh: true //boton de refescar de la barra de navegacion de paginas   
                   , pageSizes: true //combo en la barra de navegacion de paginas para cambiar la cantidad de registros mostrados   
                   , info: true //booleano que muestra o no la informacion del grid de la pagina actual no pude hacerlo funcionar   
                   , input: false // muestra un textbox donde le indicas a que pagina irte ingresando el numero por default esta en false    
                   , numeric: true //indica si se muestra los elementos numericos seleccionables   
                   , previousNext: true //indica si se muestra la barra de botones de siguiente anterior primero y ultimo   
                   , buttonCount: 5 //numero de elementos seleccionables de la paginas del grid    
                   , messages: { // para cambiar el nombre de la barra de navegacion de paginas   
                       display: GridKendo.PAGEABLEDISPLAY //{0} is the index of the first record on the page, {1} - index of the last record on the page, {2} is the total amount of records   
                           , empty: GridKendo.PAGEABLEEMPTY //mensaje para cuando el grid no tenga elementos   
                           , page: GridKendo.PAGEABLEPAGE // titulo de la pagina   
                           , of: GridKendo.PAGEABLEOF //{0} is total amount of pages   
                           , itemsPerPage: GridKendo.PAGEABLEITEMSPERPAGE //titulo de registros por pagina   
                           , morePages: GridKendo.PAGEABLEMOREPAGE //tooltip que indica cuando existe mas paginas   
                           , first: GridKendo.PAGEABLEFIRST //titulo del boton de ir a la primera pagina   
                           , previous: GridKendo.PAGEABLEPREVIOUS //titulo del boton de ir a la anterior pagina   
                           , next: GridKendo.PAGEABLENEXT //titulo del boton de ir a la siguiente pagina   
                           , last: GridKendo.PAGEABLELAST //titulo del boton de ir a la ultima pagina   
                           , refresh: GridKendo.PAGEABLEREFRESH //tooltip del boton de refrescar pagina   
                   }
               }
               , columns: [

                   {
                       headerAttributes: { style: "text-align: center; " }
                       , field: "NB_CLASIFICACION_COMPETENCIA"
                       , align: "center"  //alineacion del titulo de la columna 
                       //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.NB_CLASIFICACION_COMPETENCIA // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: false // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
                   },
                   {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "CL_CLASIFICACION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.CL_CLASIFICACION // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: true // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "NB_TIPO_COMPETENCIA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.NB_TIPO_COMPETENCIA // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: false // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "CL_USUARIO_APP_CREA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.CL_USUARIO_APP_CREA // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: true // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "CL_USUARIO_APP_MODIFICA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.CL_USUARIO_APP_MODIFICA // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: true // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "DS_CLASIFICACION_COMPETENCIA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.DS_CLASIFICACION_COMPETENCIA // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: false // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "DS_NOTAS_CLASIFICACION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.DS_NOTAS_CLASIFICACION // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: false // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "FE_CREACION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.FE_CREACION // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: true // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
                   , format: "{0:dd-MM-yyyy}"
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "FE_MODIFICACION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.FE_MODIFICACION // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: true // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
                   , format: "{0:dd-MM-yyyy}"
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "ID_CLASIFICACION_COMPETENCIA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.ID_CLASIFICACION_COMPETENCIA // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: true // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
                   , format: "{0:N0}"
               },  {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "NB_PROGRAMA_CREA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.NB_PROGRAMA_CREA // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: true // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "NB_PROGRAMA_MODIFICA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_CLASIFICACION_COMPETENCIA.NB_PROGRAMA_MODIFICA // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: true // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           showOperators: true  //indica si se muestran los operadores en la columna  
                            , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                            , operator: true
                            , cell: { //propiedades por celda 
                                showOperators: true //indica si se mostrara el combobox donde indica los operadores de filtrado 
                                , operator: "contains" //indica el operador que se estara filtrando por default, es decir, la forma de filtrado 
                                , inputWidth: "97%" //es el ancho del filtro se pondra el de la columna 
                                , delay: 0 //Tiempo que se tardara en mostrar el filtro 
                                , minLength: 1 //Tiempo que se tardara en mostrar el autocompletado 
                                //,multi:true //indica si se puede seleccionar los check muchas veces 
                                //,checkAll: false //indica si se mostrara el check de selecciona todos 
                            }
                       }
               },
                  {
                      headerAttributes: { style: "text-align: center; " }
                       , field: "FG_ACTIVO"
                       , align: "center"  //alineacion del titulo de la columna 
                      //, attributes: { title: "hola tooltip" }  
                       , title: C_COMPETENCIA.FG_ACTIVO // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: false // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , filterable: {
                           mode: "row"
                            , cell: { //propiedades por celda 
                                showOperators: false,
                                template: function (args) {
                                    args.element.kendoDropDownList({
                                        autoBind: false,
                                        dataTextField: "text",
                                        dataValueField: "value",
                                        dataSource: new kendo.data.DataSource({
                                            data: [{ text: GridKendo.MSJACTIVO, value: "true" },
 											{ text: GridKendo.MSJINACTIVO, value: "false" }]
                                        }),
                                        index: 0,
                                        valuePrimitive: true
                                    })
                                }
                            }
                       }
                      , template: '<div style="text-align: center;" > <input  style="text-align:center;" type="checkbox" #= FG_ACTIVO ? \'checked="checked"\' : "" # class="chkbx" /> </div>'

                  }
               ]
               , change: function (e) {  //evento que indica cuando cambia de elemento entre una y otra linea  
                   var selectedRows = this.select(); //se obtiene el listado de los elementos seleccionados  
                   var dataItem = this.dataItem(selectedRows[0]); //se sacan los datos del registro seleccionado  
                   $scope.entidad_ClasificacionCompetencias = dataItem; //se asigna los datos a la variable de que se crea en el scope  
               }
            });
        });
        
    };
    //termina evento para consultar los registros  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia evento para agregar el registro  
    $scope.Add_Grid_ClasificacionCompetencias = function () {
        var Obj_ClasificacionCompetencias = { //se crea la entidad para enviarla al sp 
            CL_CLASIFICACION: ''
            , CL_TIPO_COMPETENCIA: ''
            , CL_USUARIO_APP_CREA: ''
            , CL_USUARIO_APP_MODIFICA: ''
            , DS_CLASIFICACION_COMPETENCIA: ''
            , DS_NOTAS_CLASIFICACION: ''
            , FE_CREACION: ''
            , FG_ACTIVO: ''
            , FE_MODIFICACION: ''
            , ID_CLASIFICACION_COMPETENCIA: undefined
            , NB_CLASIFICACION_COMPETENCIA: ''
            , NB_PROGRAMA_CREA: ''
            , NB_PROGRAMA_MODIFICA: ''
        };
        var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
            animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
            templateUrl: 'op_ClasificacionCompetencias.html', //se le indica el html que se abrira 
            controller: 'Modal_ClasificacionCompetencias', //se le indica el controlador del html 
            resolve: { //se le envia el objeto 
                OBJETO: function () { return Obj_ClasificacionCompetencias; }
            }
        });
    };

    //termina evento para agregar el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia evento para edita el registro  
    $scope.Edit_Grid_ClasificacionCompetencias = function () {
        if ($scope.entidad_ClasificacionCompetencias == undefined) { // se verifica que el registro este seleccionado  
            BootstrapDialog.show({ //se muestra el mensaje de error  
                title: C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA, //se trae el nombre de la pantalla  
                message: Config.MENSAJEREGISTRO, // se envia el mensaje de error que falta el registro  
                size: 'size-small', //se especifica el tamaño de la ventana del mensaje  
                type: tema.cssClass //se especifica el color en base al modulo que se tiene  
            });
            return false; //se devuelve nulo para que no sigua con la funcion  
        }
        var datos = $scope.entidad_ClasificacionCompetencias; //se asigna el registro a la variable de datos  
        var Obj_ClasificacionCompetencias = { //se define el objeto que se enviara  
            //se le asigna valor a los campos de la entidad en base a lo que tiene el registro  
            CL_CLASIFICACION: datos.CL_CLASIFICACION
            , CL_TIPO_COMPETENCIA: datos.CL_TIPO_COMPETENCIA
            , CL_USUARIO_APP_CREA: datos.CL_USUARIO_APP_CREA
            , CL_USUARIO_APP_MODIFICA: datos.CL_USUARIO_APP_MODIFICA
            , DS_CLASIFICACION_COMPETENCIA: datos.DS_CLASIFICACION_COMPETENCIA
            , DS_NOTAS_CLASIFICACION: datos.DS_NOTAS_CLASIFICACION
            , FE_CREACION: datos.FE_CREACION
            , FG_ACTIVO: datos.FG_ACTIVO
            , FE_MODIFICACION: datos.FE_MODIFICACION
            , ID_CLASIFICACION_COMPETENCIA: datos.ID_CLASIFICACION_COMPETENCIA
            , NB_CLASIFICACION_COMPETENCIA: datos.NB_CLASIFICACION_COMPETENCIA
            , NB_PROGRAMA_CREA: datos.NB_PROGRAMA_CREA
            , NB_PROGRAMA_MODIFICA: datos.NB_PROGRAMA_MODIFICA
        };
        var modalInstance = $modal.open({ //se manda ejecutar en modo modal la instancia definida en el html para la edicion  
            animation: $scope.animationsEnabled, //se le asigna la variable que nos indica si tiene animacion o no  
            templateUrl: 'op_ClasificacionCompetencias.html', //se le indica el html que se abrira  
            controller: 'Modal_ClasificacionCompetencias', //se le indica el controlador del html  
            resolve: { //se le envia el objeto  
                OBJETO: function () { return Obj_ClasificacionCompetencias; }
            }
        });
    };

    //termina evento para edita el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia eventos para eliminar el registro  
    $scope.Del_Grid_ClasificacionCompetencias = function () {
        if ($scope.entidad_ClasificacionCompetencias == undefined) { //se verifica si la entidad que guarda el evento onchange del grid no se encuentra vacia  
            BootstrapDialog.show({ //en caso de que no se encuentre se mostrara un error  
               // title: C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA, //se le pone el titulo especificado en la clase   
                message: Config.MENSAJEREGISTRO, //se manda ejecutar el mensaje de seleccione un registro  
                size: 'size-small',// se le indica el tamaño de la ventana del mensaje  
                type: tema.cssClass //se le indica el color dependiendo del modulo que tiene  
            });
            return false; //se regresa false para que se salga de la funcion  
        }
        BootstrapDialog.show({ //se muestra el mensaje si la entidad es diferente a nulo  
            message: Config.MENSAJECONFIRMADEL,  //mensaje de confirmacion  
            id: 'modalDelete',
            size: 'size-small', //tamaño de la ventana  
            type: tema.cssClass, //se le indica el color dependiendo del modulo que tiene  
            buttons: [{ //se le agregan botones  
                label: Config.BOTONELIMINAR, //se le indica en la etiqueta el mensaje que tendra el boton de eliminar  
                action: function (result) { //se especifica la funcion que hara este boton  
                    if (result) { //en caso de que sea afirmativo  
                        $scope.Eliminar_ClasificacionCompetencias();  //se ejecuta la funcion de elimina_?? especificada para borrar el registro  
                    }
                }
            }, {
                label: Config.BOTONCANCELAR, //se le indica el nombre del boton cancelar  
                action: function (dialogItself) { //se indica la funcion enviando a si mismo para luego cerrar  
                    dialogItself.close(); //se realiza el evento de cerrar ventana de dialogo  
                }
            }]
        });
    };


    $scope.Eliminar_ClasificacionCompetencias = function () {
        var datos = $scope.entidad_ClasificacionCompetencias;  //se extrae el registro obtenido en el evento change del grid 
        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/Delete_C_CLASIFICACION_COMPETENCIA",
            method: "POST",
            data: { //se le pasan los parametros 
                ID_CLASIFICACION_COMPETENCIA: datos.ID_CLASIFICACION_COMPETENCIA, // se le pasa la llave primaria  
                usuario: TraerUsuario(), // se le pasa el usuario 
                //ojo modificar   
                programa: "CatalogoClasificacionCompetencias.html"  //se le pasa  
            },
            headers: {  //SE ESPECIFICAN LAS CABECERAS QUE SE ENVIAN AL WEB SERVICE, ES DECIR COMO SE ENVIAN LOS DATOS: JSON, XML, TEXTO PLANO O FILE 
                'Accept': 'application/json',  //SE INDICA QUE EL EVENTO ACEPTARA EL TIPO JSON  
                'Content-Type': 'application/json' // SE INDICA QUE TIPO CONTENDRA LA INFORMACION QUE SE ENVIA   
            }
        })
        .then(function (response) { // ejecucion de WS 
            if (response.data != true) {  //si el procedimiento regresa falso 
                BootstrapDialog.show({ //se mostrara un mensaje de error  
                    title: C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA,  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
                    message: Config.ERRORELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo ERRORGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass //se le indica el color dependiendo del modulo en que se encuentre 
                });
            } else {
                $("#Grid_ClasificacionCompetencias").data("kendoGrid").dataSource.read(); //se manda a refrescar el grid  
                $("#modalDelete").modal('toggle');
                BootstrapDialog.show({  //si el procedimiento regresa true 
                    title: C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA,  //el titulo del mensaje  
                    message: Config.MENSAJEELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass,  //se le indica el color dependiendo del modulo en que se encuentre 
                });
            }
        },
        function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
            BootstrapDialog.show({
                title: C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA,
                message: Config.ERRORGENERICO,
                size: 'size-small',
                type: tema.cssClass,
            });
        });
    };
    //termina eventos para eliminar el registro  
    ////////////////////////////////////////////////////////////// 

    //Se manda a cargar cuando se genere el controlador en el html 
    $scope.CargarGrid_ClasificacionCompetencias();


    $scope.ActivarDesactivar_Grid_ClasificacionCompetencias = function () {
        var datos = $scope.entidad_ClasificacionCompetencias;  //se extrae el registro obtenido en el evento change del grid 
        var tipoOperacion =  "A" 
        var Obj_ClasificacionCompetencias = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_CLASIFICACION: datos.CL_CLASIFICACION
 			, CL_TIPO_COMPETENCIA: datos.CL_TIPO_COMPETENCIA
 			, CL_USUARIO_APP_CREA: datos.CL_USUARIO_APP_CREA
 			, CL_USUARIO_APP_MODIFICA: datos.CL_USUARIO_APP_MODIFICA
 			, DS_CLASIFICACION_COMPETENCIA: datos.DS_CLASIFICACION_COMPETENCIA
 			, DS_NOTAS_CLASIFICACION: datos.DS_NOTAS_CLASIFICACION
 			, FE_CREACION: datos.FE_CREACION
            , FG_ACTIVO: !datos.FG_ACTIVO
 			, FE_MODIFICACION: datos.FE_MODIFICACION
 			, ID_CLASIFICACION_COMPETENCIA: datos.ID_CLASIFICACION_COMPETENCIA
 			, NB_CLASIFICACION_COMPETENCIA: datos.NB_CLASIFICACION_COMPETENCIA
 			, NB_PROGRAMA_CREA: datos.NB_PROGRAMA_CREA
 			, NB_PROGRAMA_MODIFICA: datos.NB_PROGRAMA_MODIFICA
        };
        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_CLASIFICACION_COMPETENCIA",
            method: "POST",
            data: {
                V_C_CLASIFICACION_COMPETENCIA: Obj_ClasificacionCompetencias,
                usuario: TraerUsuario(),
                //ojo modificar   
                programa: "InsertaClasificacionCompetencias.html",
                tipo_transaccion: tipoOperacion
            },
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
 		.then(function (response) { // ejecucion de WS 
 		    if (response.data != true) {  //si el procedimiento regresa falso 
 		        BootstrapDialog.show({ //se mostrara un mensaje de error  
 		          //  title: C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA,  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
 		            message: Config.ERRORACTIVARDESACTIVAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo ERRORGUARDAR 
 		            size: 'size-small',  //se especifica el tamaño del mensaje de error 
 		            type: tema.cssClass //se le indica el color dependiendo del modulo en que se encuentre 
 		        });
 		    } else {
 		        $("#Grid_ClasificacionCompetencias").data("kendoGrid").dataSource.read(); //se manda a refrescar el grid  
 		        $("#modalDelete").modal('toggle');
 		        BootstrapDialog.show({  //si el procedimiento regresa true 
 		           // title: C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA,  //el titulo del mensaje  
 		            message: Config.MENSAJEACTIVARDESACTIVAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
 		            size: 'size-small',  //se especifica el tamaño del mensaje de error 
 		            type: tema.cssClass,  //se le indica el color dependiendo del modulo en que se encuentre 
 		        });
 		    }
 		},
        function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
            BootstrapDialog.show({
             //   title: C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA,
                message: Config.ERRORGENERICO,
                size: 'size-small',
                type: tema.cssClass,
            });
        });
    };
});


//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR C_CLASIFICACION_COMPETENCIA 
app.controller('Modal_ClasificacionCompetencias', function ($scope, $modalInstance, $http, OBJETO) {


    //se inicializa el tipo de competencia para el combobox 
    $scope.TipoCompetencia = new kendo.data.DataSource(
    {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_TIPO_COMPETENCIA"
                    , dataType: "json"
                    , data: JSON.stringify({ FG_ACTIVO: true })
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        options.success(result);
                    }
                });
            }
        }
    });
    $scope.customOptions_CL_TIPO_COMPETENCIA = {
        dataSource: $scope.TipoCompetencia,
        dataTextField: "DS_TIPO_COMPETENCIA", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_TIPO_COMPETENCIA", //valor del combo cuando se seleccione
        filter: "contains"
        ////titulo de los valores a mostrar
           , headerTemplate:
               '<div class="dropdown-header k-widget k-header" style="padding:3px 0px ;" >' +
               '<table><tbody> <tr> <td style="width:50px;  text-align:left  !important;">Clave</td> <td></td> <td style="width:200px">Descripción</td> </tr> </tbody></table>' +
               '</div>',
        //valor que se muestra al seleccionar el elemento
        // valueTemplate: '<table> <tr> <td style="width:50px"> <span>{{dataItem.DS_NIVEL_ESCOLARIDAD}}</span> </td>  <td></td> <td style="width:100px"> <span>{{dataItem.CL_NIVEL_ESCOLARIDAD}}</span> </td> </tr> </table>', //'<span>{{dataItem.DS_NIVEL_ESCOLARIDAD}}</span><span>{{dataItem.CL_NIVEL_ESCOLARIDAD}}</span>',
        //valor del listado de registros que tiene el datasource    
        //
        template: '<table> <tbody><tr> <td style="width:50px  ">{{dataItem.CL_TIPO_COMPETENCIA}}</td> <td></td> <td style="width:200px">{{dataItem.DS_TIPO_COMPETENCIA}}</td> </tr></tbody> </table>'
    };

    $scope.CL_CLASIFICACION = OBJETO.CL_CLASIFICACION;
    $scope.CL_TIPO_COMPETENCIA = OBJETO.CL_TIPO_COMPETENCIA;
    $scope.CL_USUARIO_APP_CREA = OBJETO.CL_USUARIO_APP_CREA;
    $scope.CL_USUARIO_APP_MODIFICA = OBJETO.CL_USUARIO_APP_MODIFICA;
    $scope.DS_CLASIFICACION_COMPETENCIA = OBJETO.DS_CLASIFICACION_COMPETENCIA;
    $scope.DS_NOTAS_CLASIFICACION = OBJETO.DS_NOTAS_CLASIFICACION;
    $scope.FE_CREACION = OBJETO.FE_CREACION;
    $scope.FG_ACTIVO = Boolean(OBJETO.FG_ACTIVO);
    $scope.FE_MODIFICACION = OBJETO.FE_MODIFICACION;
    $scope.ID_CLASIFICACION_COMPETENCIA = OBJETO.ID_CLASIFICACION_COMPETENCIA //parseFloat(OBJETO.ID_CLASIFICACION_COMPETENCIA);  
    $scope.NB_CLASIFICACION_COMPETENCIA = OBJETO.NB_CLASIFICACION_COMPETENCIA;
    $scope.NB_PROGRAMA_CREA = OBJETO.NB_PROGRAMA_CREA;
    $scope.NB_PROGRAMA_MODIFICA = OBJETO.NB_PROGRAMA_MODIFICA;

    //se generan las traducciones de las etiquetas, placeholder (ayuda en los componentes) 
    $scope.lbl_CL_CLASIFICACION = C_CLASIFICACION_COMPETENCIA.CL_CLASIFICACION;
    $scope.ph_CL_CLASIFICACION = C_CLASIFICACION_COMPETENCIA.CL_CLASIFICACION_ph;
    $scope.lbl_CL_TIPO_COMPETENCIA = C_CLASIFICACION_COMPETENCIA.CL_TIPO_COMPETENCIA;
    $scope.ph_CL_TIPO_COMPETENCIA = C_CLASIFICACION_COMPETENCIA.CL_TIPO_COMPETENCIA_ph;
    $scope.lbl_CL_USUARIO_APP_CREA = C_CLASIFICACION_COMPETENCIA.CL_USUARIO_APP_CREA;
    $scope.ph_CL_USUARIO_APP_CREA = C_CLASIFICACION_COMPETENCIA.CL_USUARIO_APP_CREA_ph;
    $scope.lbl_CL_USUARIO_APP_MODIFICA = C_CLASIFICACION_COMPETENCIA.CL_USUARIO_APP_MODIFICA;
    $scope.ph_CL_USUARIO_APP_MODIFICA = C_CLASIFICACION_COMPETENCIA.CL_USUARIO_APP_MODIFICA_ph;
    $scope.lbl_DS_CLASIFICACION_COMPETENCIA = C_CLASIFICACION_COMPETENCIA.DS_CLASIFICACION_COMPETENCIA;
    $scope.ph_DS_CLASIFICACION_COMPETENCIA = C_CLASIFICACION_COMPETENCIA.DS_CLASIFICACION_COMPETENCIA_ph;
    $scope.lbl_DS_NOTAS_CLASIFICACION = C_CLASIFICACION_COMPETENCIA.DS_NOTAS_CLASIFICACION;
    $scope.ph_DS_NOTAS_CLASIFICACION = C_CLASIFICACION_COMPETENCIA.DS_NOTAS_CLASIFICACION_ph;
    $scope.lbl_FE_CREACION = C_CLASIFICACION_COMPETENCIA.FE_CREACION;
    $scope.ph_FE_CREACION = C_CLASIFICACION_COMPETENCIA.FE_CREACION_ph;
    $scope.lbl_FG_ACTIVO = C_CLASIFICACION_COMPETENCIA.FG_ACTIVO;
    $scope.ph_FG_ACTIVO = C_CLASIFICACION_COMPETENCIA.FG_ACTIVO_ph;

    $scope.lbl_FE_MODIFICACION = C_CLASIFICACION_COMPETENCIA.FE_MODIFICACION;
    $scope.ph_FE_MODIFICACION = C_CLASIFICACION_COMPETENCIA.FE_MODIFICACION_ph;
    $scope.lbl_ID_CLASIFICACION_COMPETENCIA = C_CLASIFICACION_COMPETENCIA.ID_CLASIFICACION_COMPETENCIA;
    $scope.ph_ID_CLASIFICACION_COMPETENCIA = C_CLASIFICACION_COMPETENCIA.ID_CLASIFICACION_COMPETENCIA_ph;
    $scope.lbl_NB_CLASIFICACION_COMPETENCIA = C_CLASIFICACION_COMPETENCIA.NB_CLASIFICACION_COMPETENCIA;
    $scope.ph_NB_CLASIFICACION_COMPETENCIA = C_CLASIFICACION_COMPETENCIA.NB_CLASIFICACION_COMPETENCIA_ph;
    $scope.lbl_NB_PROGRAMA_CREA = C_CLASIFICACION_COMPETENCIA.NB_PROGRAMA_CREA;
    $scope.ph_NB_PROGRAMA_CREA = C_CLASIFICACION_COMPETENCIA.NB_PROGRAMA_CREA_ph;
    $scope.lbl_NB_PROGRAMA_MODIFICA = C_CLASIFICACION_COMPETENCIA.NB_PROGRAMA_MODIFICA;
    $scope.ph_NB_PROGRAMA_MODIFICA = C_CLASIFICACION_COMPETENCIA.NB_PROGRAMA_MODIFICA_ph;

    //se generan las traducciones Genericas 
    $scope.titulo = C_CLASIFICACION_COMPETENCIA.TITULO_PANTALLA;
    $scope.msjRequerido = Config.MSJREQUERIDO;
    $scope.btnSave = Config.BOTONGUARDAR;
    $scope.btnCancel = Config.BOTONCANCELAR;
    $scope.modulo = tema.cssClass;


    $scope.CL_CLASIFICACION_disable = false;
    $scope.CL_TIPO_COMPETENCIA_disable = false;
    if ($scope.ID_CLASIFICACION_COMPETENCIA != undefined) {
        $scope.CL_CLASIFICACION_disable = true;
        $scope.CL_TIPO_COMPETENCIA_disable = true;
    }

    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente  
        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
        if ($scope.ID_CLASIFICACION_COMPETENCIA != undefined)  //se verifica que la clave primaria sea diferente de nulo  
            tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  
        var Obj_ClasificacionCompetencias = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_CLASIFICACION: $scope.CL_CLASIFICACION
 			, CL_TIPO_COMPETENCIA: $scope.CL_TIPO_COMPETENCIA
 			, CL_USUARIO_APP_CREA: $scope.CL_USUARIO_APP_CREA
 			, CL_USUARIO_APP_MODIFICA: $scope.CL_USUARIO_APP_MODIFICA
 			, DS_CLASIFICACION_COMPETENCIA: $scope.DS_CLASIFICACION_COMPETENCIA
 			, DS_NOTAS_CLASIFICACION: $scope.DS_NOTAS_CLASIFICACION
 			, FE_CREACION: $scope.FE_CREACION
            , FG_ACTIVO: $scope.FG_ACTIVO
 			, FE_MODIFICACION: $scope.FE_MODIFICACION
 			, ID_CLASIFICACION_COMPETENCIA: $scope.ID_CLASIFICACION_COMPETENCIA
 			, NB_CLASIFICACION_COMPETENCIA: $scope.NB_CLASIFICACION_COMPETENCIA
 			, NB_PROGRAMA_CREA: $scope.NB_PROGRAMA_CREA
 			, NB_PROGRAMA_MODIFICA: $scope.NB_PROGRAMA_MODIFICA
        };
        $http({                          
            url: "ws/OperacionesGral.svc/Insert_update_C_CLASIFICACION_COMPETENCIA",
            method: "POST",
            data: {
                V_C_CLASIFICACION_COMPETENCIA: Obj_ClasificacionCompetencias,
                usuario: TraerUsuario(),
                //ojo modificar   
                programa: "InsertaClasificacionCompetencias.html",
                tipo_transaccion: tipoOperacion
            },
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
 		.then(function (response) {
 		    if (response.data != true) {
 		        BootstrapDialog.show({
 		            title: Config.MENSAJEERROR,
 		            message: Config.ERRORGUARDAR,
 		            size: 'size-small',
 		            type: tema.cssClass
 		        });
 		    } else {
 		        if ($scope.ID_CLASIFICACION_COMPETENCIA == undefined) {
 		            LimpiarFormulario("newForm");
 		        }
 		        BootstrapDialog.show({
 		            title: '',
 		            message: Config.MENSAJEGUARDAR,
 		            size: 'size-small',
 		            type: tema.cssClass
 		        });
 		    }
 		},
 		function (response) {
 		    BootstrapDialog.show({
 		        title: Config.MENSAJEERROR,
 		        message: Config.ERRORGENERICO,
 		        size: 'size-small',
 		        type: tema.cssClass
 		    });
 		});
    };
    $scope.cancel = function () {
        $("#Grid_ClasificacionCompetencias").data("kendoGrid").dataSource.read();
        $modalInstance.dismiss('cancel');
    };
});


