
function ObtieneRegistroSeleccionado(nombregrid) {
    var grid = $("#" + nombregrid).data("kendoGrid");
    //var selectedItem = grid.dataItem(grid.select());
    var selectedItem = grid.select();
    selectedItem.each(function (index, row) {
        var selectedItem = grid.dataItem(row);
        return selectedItem
        //alert(selectedItem.CL_ESTADO) asi se obtiene el valor de la columna especificada
    });
}


//poner en appAdministracion.js 
app.controller('C_ESTADOController', function ($scope, $http, $modal, $rootScope) {

    $scope.animationsEnabled = true;


    //se instancia el objeto que contiene el titulo en español y ingles de las columnas  
    //$scope.btnActivarDesactivar = Config.BOTONACTIVARDESACTIVAR;   
    $scope.btnAgregar = Config.BOTONAGREGAR;
    $scope.btnModificar = Config.BOTONMODIFICAR;
    $scope.btnEliminar = Config.BOTONELIMINAR;
    $scope.titulo_estado = C_ESTADO.TITULO_PANTALLA;
    $scope.titulo_municipio = C_MUNICIPIO.TITULO_PANTALLA;
    $scope.titulo_colonia = C_COLONIA.TITULO_PANTALLA;
    $scope.ph_CL_ESTADO = C_ESTADO.CL_ESTADO_ph;
    $scope.ph_CL_MUNICIPIO = C_MUNICIPIO.CL_MUNICIPIO_ph;
    $scope.ph_CL_TIPO_ASENTAMIENTO
    //se inicializa la variable de control
    $scope.filtrado = false;
    $scope.CL_ESTADO_filtrado = '';
    $scope.CL_MUNICIPIO_filtrado = '';
    //se obtiene el listado de estados
    $scope.DS_ESTADOS = //new kendo.data.DataSource(
    {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_C_ESTADO"
                    , dataType: "json"
                   // , data: JSON.stringify({ FG_ACTIVO: true })
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        console.log(result);
                        options.success(result);
                    }
                });
            }
        }
    };
     
    //se le asigna las opciones al combo de estados
    $scope.customOptions_ESTADOS = {
        dataSource: $scope.DS_ESTADOS,
        dataTextField: "NB_ESTADO", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_ESTADO", //valor del combo cuando se seleccione
        filter: "contains"
    };


    
    

    //ojo se debe de generar el customOptions_MUNICIPIO antes de llamar la funcion de CargaMunicipios ya que de lo contrario no encuentra el dropdow
    //se le asigna las opciones al combo de estados
    $scope.customOptions_MUNICIPIO = {
        dataSource: $scope.DS_MUNICIPIOS,
        dataTextField: "NB_MUNICIPIO", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_MUNICIPIO", //valor del combo cuando se seleccione
        filter: "contains"
    };
    //se asigna el datasource al cargar la pagina como nulo
    $scope.DS_MUNICIPIOS = //new kendo.data.DataSource(
    {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_C_MUNICIPIO"
                    , dataType: "json"
                    , data: JSON.stringify({ CL_ESTADO: 'none' })
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        console.log(result);
                        options.success(result);
                    }
                });
            }
        }
    };


   
    //funcion en onchange del combo de estados cargara el listado de municipios y sus opciones

    $scope.CargaMunicipios = function (PARAMETRO_CL_ESTADO) {
            //se obtiene el listado de MUNICIPIOS FILTRADO POR CL_ESTADO
            $scope.DS_MUNICIPIOS = //new kendo.data.DataSource(
            {
                type: "json",
                transport: {
                    read: function (options) {
                        $.ajax({
                            type: "post"
                            , url: "ws/OperacionesGral.svc/Get_C_MUNICIPIO"
                            , dataType: "json"
                            , data: JSON.stringify({ CL_ESTADO: PARAMETRO_CL_ESTADO })
                            , contentType: "application/json; charset=utf-8"
                            , success: function (result) {
                                console.log(result);
                                options.success(result);
                            }
                        });
                    }
                }
            };
        
        var txt_municipio = $("#txt_MUNICIPIO").data("kendoDropDownList");        
        txt_municipio.setDataSource($scope.DS_MUNICIPIOS);
        txt_municipio.value("");
        txt_municipio.refresh();
        $scope.CL_MUNICIPIO = '';
    
    };

    $scope.CargaColonias = function () {
        $("#Grid_Colonia").empty();
        $scope.CargarGrid_Colonia();
        $scope.filtrado = false;
        $rootScope.CL_ESTADO_filtrado = $scope.CL_ESTADO;
        $rootScope.CL_MUNICIPIO_filtrado = $scope.CL_MUNICIPIO;

        if ($scope.CL_MUNICIPIO != "") 
            $scope.filtrado = true;
    }

    

    
    
    ////////////////////////////////////////////////////////////// 
    //inicia evento para consultar los registros  
    //SE CARGA EL TEMPLATE DEL TOOLBAR DEL GRID DONDE IRAN LOS BOTONES DE EDICION Y EXPORTACION  
    //$scope.templateGrid_Colonia = function () {
    //    return "<div class=\"toolbar\"> " +
 	//		"<div style=\"float:right\">  " +
 	//			"<div style=\"float:left\">" +
 	//				"<button id=\"btnExportarExcel_Grid_Colonia\" class=\"btn btn-link FontNegro\" onclick=\"ExportarExcel('Grid_Colonia')\"  > " +
 	//					"<i class=\"fa fa-file-o\" style=\"font-size: 17px;\"></i>  " +
 	//					"" + GridKendo.BOTONEXPORTAREXCEL + " " +
 	//				"</button> " +
 	//			"</div>" +
 	//		"	</div>  " +
 	//	"</div>  "
    //};
    $scope.BOTONEXPORTAREXCEL = GridKendo.BOTONEXPORTAREXCEL
    //se carga el grid  
    $scope.CargarGrid_Colonia = function () {
        NProgress.start();  //muestra el progreso en la pagina  
        //var var_cl_municipio = '???';
        //if ($scope.entidad_Municipio != undefined) { // se verifica que el registro este seleccionado  
        //    var datos = $scope.entidad_Municipio; //se asigna el registro a la variable de datos  
        //    var_cl_municipio = datos.CL_MUNICIPIO;
        //}
        $(document).ready(function () {
            $("#Grid_Colonia").kendoGrid({
                allowCopy: true//permite copiar los datos para copiar al portapapeles   
                , allowCopy: {
                    delimeter: "," //delimitador cuando se copien los datos del grid   
                }
                , resizable: true //sirve para mover las columnas
               , excel: {
                   fileName: "Colonia.xlsx" //nombre del archivo a exportar   
                       , allPages: true // indica si se exportan todas las paginas por default esta en false   
                   //,proxyURL: "//demos.telerik.com/kendo-ui/service/export",   
                       , filterable: true //permite que la exportacion sea por lo que se esta filtrando   
               }
              //  , toolbar: kendo.template($scope.templateGrid_Colonia())
                , dataSource: {
                    type: "JSON", // JSON, JSONP, OData or XML).   
                    transport: {
                        read: function (options) {
                            $.ajax({
                                type: "POST"
                                , url: "ws/OperacionesGral.svc/Get_C_COLONIA"
                                , data: JSON.stringify({ CL_MUNICIPIO: $scope.CL_MUNICIPIO })  //sirve para envia los parametros se le indica el nombre de la columna mas dos puntos : luego el valor de la columna 
                                , contentType: "application/json; charset=utf-8"    
                                , dataType: "json"
                                , success: function (result) {
                                    options.success(result);
                                    NProgress.done();
                                }
                            });
                        }
                    }
                   , schema: {
                       model: {
                           fields: {
                               CL_CODIGO_POSTAL: { type: "string" }
                               , CL_COLONIA: { type: "string" }
                               , CL_ESTADO: { type: "string" }
                               , CL_MUNICIPIO: { type: "string" }
                               , CL_PAIS: { type: "string" }
                               , CL_TIPO_ASENTAMIENTO: { type: "string" }
                               , CL_USUARIO_APP_CREA: { type: "string" }
                               , CL_USUARIO_APP_MODIFICA: { type: "string" }
                               , FE_CREACION: { type: "date" }
                               , FE_MODIFICACION: { type: "date" }
                               , ID_COLONIA: { type: "number" }
                               , NB_COLONIA: { type: "string" }
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
               , columns: [ {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "CL_COLONIA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COLONIA.CL_COLONIA // titulo de la columna 
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
                       , field: "NB_COLONIA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COLONIA.NB_COLONIA // titulo de la columna 
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
                       , field: "CL_ESTADO"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COLONIA.CL_ESTADO // titulo de la columna 
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
                       , field: "CL_MUNICIPIO"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COLONIA.CL_MUNICIPIO // titulo de la columna 
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
                       , field: "CL_PAIS"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COLONIA.CL_PAIS // titulo de la columna 
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
                       , field: "CL_CODIGO_POSTAL"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COLONIA.CL_CODIGO_POSTAL // titulo de la columna 
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
                       , field: "CL_TIPO_ASENTAMIENTO"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COLONIA.CL_TIPO_ASENTAMIENTO // titulo de la columna 
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
                       , title: C_COLONIA.CL_USUARIO_APP_CREA // titulo de la columna 
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
                       , title: C_COLONIA.CL_USUARIO_APP_MODIFICA // titulo de la columna 
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
                       , field: "FE_CREACION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COLONIA.FE_CREACION // titulo de la columna 
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
                       , title: C_COLONIA.FE_MODIFICACION // titulo de la columna 
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
                       , field: "ID_COLONIA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COLONIA.ID_COLONIA // titulo de la columna 
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
                       , title: C_COLONIA.NB_PROGRAMA_CREA // titulo de la columna 
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
                       , title: C_COLONIA.NB_PROGRAMA_MODIFICA // titulo de la columna 
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
               }
               ]
               , change: function (e) {  //evento que indica cuando cambia de elemento entre una y otra linea  
                   var selectedRows = this.select(); //se obtiene el listado de los elementos seleccionados  
                   var dataItem = this.dataItem(selectedRows[0]); //se sacan los datos del registro seleccionado  
                   $scope.entidad_Colonia = dataItem; //se asigna los datos a la variable de que se crea en el scope  
               }
            });
        });

    };
    //termina evento para consultar los registros  
    ////////////////////////////////////////////////////////////// 

    
    ////////////////////////////////////////////////////////////// 
    //inicia evento para agregar el registro  
    $scope.Add_Grid_Colonia = function () {

        if ($scope.filtrado) {

            var Obj_Colonia = { //se crea la entidad para enviarla al sp 
                CL_CODIGO_POSTAL: ''
                , CL_COLONIA: ''
                , CL_ESTADO: ''
                , CL_MUNICIPIO: ''
                , CL_PAIS: ''
                , CL_TIPO_ASENTAMIENTO: ''
                , CL_USUARIO_APP_CREA: ''
                , CL_USUARIO_APP_MODIFICA: ''
                , FE_CREACION: ''
                , FE_MODIFICACION: ''
                , ID_COLONIA: undefined
                , NB_COLONIA: ''
                , NB_PROGRAMA_CREA: ''
                , NB_PROGRAMA_MODIFICA: ''
            };
            var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
                animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
                templateUrl: 'op_Colonia.html', //se le indica el html que se abrira 
                controller: 'Modal_Colonia', //se le indica el controlador del html 
                resolve: { //se le envia el objeto 
                    OBJETO: function () { return Obj_Colonia; }
                }
            });
        } else {
            BootstrapDialog.show({
                title: '',
                message: Config.SELECCIONE_ESTADO_MUNICIPIO,
                size: 'size-small',
                type: tema.cssClass,
            });
        }
    
    };  
  
    //termina evento para agregar el registro  
    ////////////////////////////////////////////////////////////// 
  
  
    ////////////////////////////////////////////////////////////// 
    //inicia evento para edita el registro  
    $scope.Edit_Grid_Colonia = function () {  
        if ($scope.entidad_Colonia == undefined) { // se verifica que el registro este seleccionado  
            BootstrapDialog.show({ //se muestra el mensaje de error  
                title: C_COLONIA.TITULO_PANTALLA, //se trae el nombre de la pantalla  
                message: Config.MENSAJEREGISTRO, // se envia el mensaje de error que falta el registro  
                size: 'size-small', //se especifica el tamaño de la ventana del mensaje  
                type: tema.cssClass //se especifica el color en base al modulo que se tiene  
            });  
            return false; //se devuelve nulo para que no sigua con la funcion  
        }  
        var datos = $scope.entidad_Colonia; //se asigna el registro a la variable de datos  
        var Obj_Colonia = { //se define el objeto que se enviara  
            //se le asigna valor a los campos de la entidad en base a lo que tiene el registro  
            CL_CODIGO_POSTAL:  datos.CL_CODIGO_POSTAL 
            ,CL_COLONIA:  datos.CL_COLONIA 
            ,CL_ESTADO:  datos.CL_ESTADO 
            ,CL_MUNICIPIO:  datos.CL_MUNICIPIO 
            ,CL_PAIS:  datos.CL_PAIS 
            ,CL_TIPO_ASENTAMIENTO:  datos.CL_TIPO_ASENTAMIENTO 
            ,CL_USUARIO_APP_CREA:  datos.CL_USUARIO_APP_CREA 
            ,CL_USUARIO_APP_MODIFICA:  datos.CL_USUARIO_APP_MODIFICA 
            ,FE_CREACION:  datos.FE_CREACION 
            ,FE_MODIFICACION:  datos.FE_MODIFICACION 
            ,ID_COLONIA:  datos.ID_COLONIA 
            ,NB_COLONIA:  datos.NB_COLONIA 
            ,NB_PROGRAMA_CREA:  datos.NB_PROGRAMA_CREA 
            ,NB_PROGRAMA_MODIFICA:  datos.NB_PROGRAMA_MODIFICA 
        };  
        var modalInstance = $modal.open({ //se manda ejecutar en modo modal la instancia definida en el html para la edicion  
            animation: $scope.animationsEnabled, //se le asigna la variable que nos indica si tiene animacion o no  
            templateUrl: 'op_Colonia.html', //se le indica el html que se abrira  
            controller: 'Modal_Colonia', //se le indica el controlador del html  
            resolve: { //se le envia el objeto  
                OBJETO: function () { return Obj_Colonia; }    
            }  
        });  
    };  
  
    //termina evento para edita el registro  
    ////////////////////////////////////////////////////////////// 
  
  
    ////////////////////////////////////////////////////////////// 
    //inicia eventos para eliminar el registro  
    $scope.Del_Grid_Colonia = function () {  
        if ($scope.entidad_Colonia == undefined) { //se verifica si la entidad que guarda el evento onchange del grid no se encuentra vacia  
            BootstrapDialog.show({ //en caso de que no se encuentre se mostrara un error  
                title: C_COLONIA.TITULO_PANTALLA, //se le pone el titulo especificado en la clase   
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
                        $scope.Eliminar_Colonia();  //se ejecuta la funcion de elimina_?? especificada para borrar el registro  
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
  
  
    $scope.Eliminar_Colonia = function () {  
        var datos = $scope.entidad_Colonia;  //se extrae el registro obtenido en el evento change del grid 
        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/Delete_C_COLONIA",  
            method: "POST",  
            data: { //se le pasan los parametros 
                ID_COLONIA: datos.ID_COLONIA, // se le pasa la llave primaria  
                usuario : TraerUsuario(), // se le pasa el usuario 
                //ojo modificar   
                programa : "CatalogoColonia.html"  //se le pasa  
            },  
            headers: {  //SE ESPECIFICAN LAS CABECERAS QUE SE ENVIAN AL WEB SERVICE, ES DECIR COMO SE ENVIAN LOS DATOS: JSON, XML, TEXTO PLANO O FILE 
                'Accept': 'application/json',  //SE INDICA QUE EL EVENTO ACEPTARA EL TIPO JSON  
                'Content-Type': 'application/json' // SE INDICA QUE TIPO CONTENDRA LA INFORMACION QUE SE ENVIA   
            }  
        })  
        .then(function (response) { // ejecucion de WS 
            if (response.data != true) {  //si el procedimiento regresa falso 
                BootstrapDialog.show({ //se mostrara un mensaje de error  
                    title: C_COLONIA.TITULO_PANTALLA,  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
                    message: Config.ERRORELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo ERRORGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass //se le indica el color dependiendo del modulo en que se encuentre 
                });  
            } else {  
                $("#Grid_Colonia").data("kendoGrid").dataSource.read(); //se manda a refrescar el grid  
                $("#modalDelete").modal('toggle');  
                BootstrapDialog.show({  //si el procedimiento regresa true 
                    title: C_COLONIA.TITULO_PANTALLA,  //el titulo del mensaje  
                    message: Config.MENSAJEELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass,  //se le indica el color dependiendo del modulo en que se encuentre 
                });  
            }  
        },  
        function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
            BootstrapDialog.show({  
                title: C_COLONIA.TITULO_PANTALLA,  
                message: Config.ERRORGENERICO,  
                size: 'size-small',  
                type: tema.cssClass,  
            });  
        });  
    };  
    //termina eventos para eliminar el registro  
    ////////////////////////////////////////////////////////////// 
  
    //Se manda a cargar cuando se genere el controlador en el html 
    //$scope.CargarGrid_Colonia ();  
});  
  
  
//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR C_COLONIA 
app.controller('Modal_Colonia', function ($scope, $modalInstance, $http, OBJETO, $rootScope) {
    $scope.CL_CODIGO_POSTAL = OBJETO.CL_CODIGO_POSTAL;  
    $scope.CL_COLONIA = OBJETO.CL_COLONIA;  
    $scope.CL_ESTADO = OBJETO.CL_ESTADO;  
    $scope.CL_MUNICIPIO = OBJETO.CL_MUNICIPIO;  
    $scope.CL_PAIS = OBJETO.CL_PAIS;  
    $scope.CL_TIPO_ASENTAMIENTO = OBJETO.CL_TIPO_ASENTAMIENTO;  
    $scope.CL_USUARIO_APP_CREA = OBJETO.CL_USUARIO_APP_CREA;  
    $scope.CL_USUARIO_APP_MODIFICA = OBJETO.CL_USUARIO_APP_MODIFICA;  
    $scope.FE_CREACION = OBJETO.FE_CREACION;  
    $scope.FE_MODIFICACION = OBJETO.FE_MODIFICACION;  
    $scope.ID_COLONIA = OBJETO.ID_COLONIA //parseFloat(OBJETO.ID_COLONIA);  
    $scope.NB_COLONIA = OBJETO.NB_COLONIA;  
    $scope.NB_PROGRAMA_CREA = OBJETO.NB_PROGRAMA_CREA;  
    $scope.NB_PROGRAMA_MODIFICA = OBJETO.NB_PROGRAMA_MODIFICA;  
    
    //se generan las traducciones de las etiquetas, placeholder (ayuda en los componentes) 
    $scope.lbl_CL_CODIGO_POSTAL = C_COLONIA.CL_CODIGO_POSTAL;  
    $scope.ph_CL_CODIGO_POSTAL = C_COLONIA.CL_CODIGO_POSTAL_ph;  
    $scope.lbl_CL_COLONIA = C_COLONIA.CL_COLONIA;  
    $scope.ph_CL_COLONIA = C_COLONIA.CL_COLONIA_ph;  
    $scope.lbl_CL_ESTADO = C_COLONIA.CL_ESTADO;  
    $scope.ph_CL_ESTADO = C_COLONIA.CL_ESTADO_ph;  
    $scope.lbl_CL_MUNICIPIO = C_COLONIA.CL_MUNICIPIO;  
    $scope.ph_CL_MUNICIPIO = C_COLONIA.CL_MUNICIPIO_ph;  
    $scope.lbl_CL_PAIS = C_COLONIA.CL_PAIS;  
    $scope.ph_CL_PAIS = C_COLONIA.CL_PAIS_ph;  
    $scope.lbl_CL_TIPO_ASENTAMIENTO = C_COLONIA.CL_TIPO_ASENTAMIENTO;  
    $scope.ph_CL_TIPO_ASENTAMIENTO = C_COLONIA.CL_TIPO_ASENTAMIENTO_ph;  
    $scope.lbl_CL_USUARIO_APP_CREA = C_COLONIA.CL_USUARIO_APP_CREA;  
    $scope.ph_CL_USUARIO_APP_CREA = C_COLONIA.CL_USUARIO_APP_CREA_ph;  
    $scope.lbl_CL_USUARIO_APP_MODIFICA = C_COLONIA.CL_USUARIO_APP_MODIFICA;  
    $scope.ph_CL_USUARIO_APP_MODIFICA = C_COLONIA.CL_USUARIO_APP_MODIFICA_ph;  
    $scope.lbl_FE_CREACION = C_COLONIA.FE_CREACION;  
    $scope.ph_FE_CREACION = C_COLONIA.FE_CREACION_ph;  
    $scope.lbl_FE_MODIFICACION = C_COLONIA.FE_MODIFICACION;  
    $scope.ph_FE_MODIFICACION = C_COLONIA.FE_MODIFICACION_ph;  
    $scope.lbl_ID_COLONIA = C_COLONIA.ID_COLONIA;  
    $scope.ph_ID_COLONIA = C_COLONIA.ID_COLONIA_ph;  
    $scope.lbl_NB_COLONIA = C_COLONIA.NB_COLONIA;  
    $scope.ph_NB_COLONIA = C_COLONIA.NB_COLONIA_ph;  
    $scope.lbl_NB_PROGRAMA_CREA = C_COLONIA.NB_PROGRAMA_CREA;  
    $scope.ph_NB_PROGRAMA_CREA = C_COLONIA.NB_PROGRAMA_CREA_ph;  
    $scope.lbl_NB_PROGRAMA_MODIFICA = C_COLONIA.NB_PROGRAMA_MODIFICA;  
    $scope.ph_NB_PROGRAMA_MODIFICA = C_COLONIA.NB_PROGRAMA_MODIFICA_ph;


    //se obtiene el listado de estados
    $scope.DATASOURCER_TIPO_ASENTAMIENTO = //new kendo.data.DataSource(
    {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_TIPO_ASENTAMIENTO"
                    , dataType: "json"
                    // , data: JSON.stringify({ FG_ACTIVO: true })
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        console.log(result);
                        options.success(result);
                    }
                });
            }
        }
    };
    //se le asigna las opciones al combo de estados
    $scope.customOptions_TIPO_ASENTAMIENTO = {
        dataSource: $scope.DATASOURCER_TIPO_ASENTAMIENTO,
        dataTextField: "CL_TIPO_ASENTAMIENTO", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_TIPO_ASENTAMIENTO", //valor del combo cuando se seleccione
        filter: "contains"
    };

  
    //se generan las traducciones Genericas 
    $scope.titulo = C_COLONIA.TITULO_PANTALLA; 
    $scope.msjRequerido = Config.MSJREQUERIDO; 
    $scope.btnSave = Config.BOTONGUARDAR; 
    $scope.btnCancel = Config.BOTONCANCELAR; 
    $scope.modulo = tema.cssClass; 
  
    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente  
        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
        if ($scope.ID_COLONIA != undefined)  //se verifica que la clave primaria sea diferente de nulo  
            tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  
        var Obj_Colonia = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_CODIGO_POSTAL: $scope.CL_CODIGO_POSTAL 
 			,CL_COLONIA: $scope.CL_COLONIA 
 			, CL_ESTADO: $rootScope.CL_ESTADO_filtrado
 			, CL_MUNICIPIO: $rootScope.CL_MUNICIPIO_filtrado
 			,CL_PAIS: $scope.CL_PAIS 
 			,CL_TIPO_ASENTAMIENTO: $scope.CL_TIPO_ASENTAMIENTO 
 			,CL_USUARIO_APP_CREA: $scope.CL_USUARIO_APP_CREA 
 			,CL_USUARIO_APP_MODIFICA: $scope.CL_USUARIO_APP_MODIFICA 
 			,FE_CREACION: $scope.FE_CREACION 
 			,FE_MODIFICACION: $scope.FE_MODIFICACION 
 			,ID_COLONIA: $scope.ID_COLONIA 
 			,NB_COLONIA: $scope.NB_COLONIA 
 			,NB_PROGRAMA_CREA: $scope.NB_PROGRAMA_CREA 
 			,NB_PROGRAMA_MODIFICA: $scope.NB_PROGRAMA_MODIFICA 
        };  
        $http({  
            url: "ws/OperacionesGral.svc/Insert_update_C_COLONIA",  
            method: "POST",  
            data: {  
                V_C_COLONIA: Obj_Colonia,  
                usuario: TraerUsuario(),  
                //ojo modificar   
                programa: "InsertaColonia.html",  
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
 		        if ($scope.ID_COLONIA == undefined) {  
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
        $("#Grid_Colonia").data("kendoGrid").dataSource.read();  
        $modalInstance.dismiss('cancel');  
    };  
    
});