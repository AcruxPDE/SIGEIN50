


app.controller('C_ROLController', function ($scope, $http, $modal, $rootScope) {
    //habilita la animacion del popup para que se despliegue de arriba para abajo  
    $scope.animationsEnabled = true;
    //var C_ROL = new C_ROL();
   

    $scope.btnAgregar = Config.BOTONAGREGAR;
    $scope.btnModificar = Config.BOTONMODIFICAR;
    $scope.btnEliminar = Config.BOTONELIMINAR;
    $scope.titulo = C_ROL.TITULO_PANTALLA;

    //$scope.templateGrid_Rol = function () {
    //    return "<div class=\"toolbar\"> " +
 	//		"<div style=\"float:right\">  " +
 	//			"<div style=\"float:left\">" +
 	//				"<button id=\"btnExportarExcel_Grid_Rol\" class=\"btn btn-link FontNegro\" onclick=\"ExportarExcel('Grid_Rol')\"  > " +
 	//					"<i class=\"fa fa-file-o\" style=\"font-size: 17px;\"></i>  " +
 	//					"" + GridKendo.BOTONEXPORTAREXCEL + " " +
 	//				"</button> " +
 	//			"</div>" +
 	//		"	</div>  " +
 	//	"</div>  "
    //};
    $scope.BOTONEXPORTAREXCEL = GridKendo.BOTONEXPORTAREXCEL
    //se carga el grid  
    $scope.CargarGrid_Rol = function () {
        NProgress.start();  //muestra el progreso en la pagina  
        $(document).ready(function () {
            $("#Grid_Rol").kendoGrid({
                allowCopy: true//permite copiar los datos para copiar al portapapeles   
                , allowCopy: {
                    delimeter: "," //delimitador cuando se copien los datos del grid   
                }
                , resizable: true //sirve para mover las columnas
               , excel: {
                         fileName: "Rol.xlsx" //nombre del archivo a exportar   
                       , allPages: true // indica si se exportan todas las paginas por default esta en false      
                       , filterable: true //permite que la exportacion sea por lo que se esta filtrando   
               }
              //  , toolbar: kendo.template($scope.templateGrid_Rol())
                , dataSource: {
                    type: "JSON", // JSON, JSONP, OData or XML).   
                    transport: {
                        read: function (options) {
                            $.ajax({
                                type: "POST"
                                , url: "ws/OperacionesGral.svc/Get_C_ROL"
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
                               CL_ROL: { type: "string" }
                               , NB_ROL: { type: "string" }
                               , FG_ACTIVO: { type: "boolean" }
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
               , columns: [{
                   headerAttributes: { style: "text-align: center; " }
                       , field: "CL_ROL"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_ROL.CL_ROL // titulo de la columna 
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
                       , field: "NB_ROL"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_ROL.NB_ROL // titulo de la columna 
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
                       , field: "FG_ACTIVO"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_ROL.FG_ACTIVO // titulo de la columna 
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
                   , template: "<div style=\"text-align:center\"><label><input type=\"checkbox\" #= FG_ACTIVO ? \"checked='checked'\" : \"\" # disabled=\"disabled\" name=\"i_FG_ACTIVO\" /></label></div>"
               }
               ]
               , change: function (e) {  //evento que indica cuando cambia de elemento entre una y otra linea  
                   var selectedRows = this.select(); //se obtiene el listado de los elementos seleccionados  
                   var dataItem = this.dataItem(selectedRows[0]); //se sacan los datos del registro seleccionado  
                   $scope.entidad_Rol = dataItem; //se asigna los datos a la variable de que se crea en el scope  
                   
                   $("#Grid_SPE_OBTIENE_C_ROL_FUNCION").empty();
                   $scope.CargarGrid_SPE_OBTIENE_C_ROL_FUNCION();
                   
               }
            });
        });
    };

    //inicia evento para agregar el registro  
    $scope.Add_Grid_Rol = function () {
        var Obj_Rol = { //se crea la entidad para enviarla al sp 
            CL_ROL: ''
            , NB_ROL: ''
            , FG_ACTIVO: ''
            , ID_ROL: undefined
        };
        var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
            animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
            templateUrl: 'op_Rol.html', //se le indica el html que se abrira 
            controller: 'Modal_Rol', //se le indica el controlador del html 
            resolve: { //se le envia el objeto 
                OBJETO: function () { return Obj_Rol; }
            }
        });
    };

    //inicia evento para edita el registro  
    $scope.Edit_Grid_Rol = function () {
        if ($scope.entidad_Rol == undefined) { // se verifica que el registro este seleccionado  
            BootstrapDialog.show({ //se muestra el mensaje de error  
                title: C_ROL.TITULO_PANTALLA, //se trae el nombre de la pantalla  
                message: Config.MENSAJEREGISTRO, // se envia el mensaje de error que falta el registro  
                size: 'size-small', //se especifica el tamaño de la ventana del mensaje  
                type: tema.cssClass //se especifica el color en base al modulo que se tiene  
            });
            return false; //se devuelve nulo para que no sigua con la funcion  
        }
        var datos = $scope.entidad_Rol; //se asigna el registro a la variable de datos  
        var Obj_Rol = { //se define el objeto que se enviara  
            //se le asigna valor a los campos de la entidad en base a lo que tiene el registro  
            CL_ROL: datos.CL_ROL
            , NB_ROL: datos.NB_ROL
            , FG_ACTIVO: datos.FG_ACTIVO
            , ID_ROL: datos.ID_ROL
        };
        var modalInstance = $modal.open({ //se manda ejecutar en modo modal la instancia definida en el html para la edicion  
            animation: $scope.animationsEnabled, //se le asigna la variable que nos indica si tiene animacion o no  
            templateUrl: 'op_Rol.html', //se le indica el html que se abrira  
            controller: 'Modal_Rol', //se le indica el controlador del html  
            resolve: { //se le envia el objeto  
                OBJETO: function () { return Obj_Rol; }
            }
        });
    };
 
    //inicia eventos para eliminar el registro  
    $scope.Del_Grid_Rol = function () {
        if ($scope.entidad_Rol == undefined) { //se verifica si la entidad que guarda el evento onchange del grid no se encuentra vacia  
            BootstrapDialog.show({ //en caso de que no se encuentre se mostrara un error  
                title: C_ROL.TITULO_PANTALLA, //se le pone el titulo especificado en la clase   
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
                        $scope.Eliminar_Rol();  //se ejecuta la funcion de elimina_?? especificada para borrar el registro  
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

    $scope.Eliminar_Rol = function () {
        var datos = $scope.entidad_Rol;  //se extrae el registro obtenido en el evento change del grid 
        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/Delete_C_ROL",
            method: "POST",
            data: { //se le pasan los parametros 
                ID_ROL: datos.ID_ROL, // se le pasa la llave primaria  
                usuario: TraerUsuario(), // se le pasa el usuario 
                //ojo modificar   
                programa: "CatalogoRol.html"  //se le pasa  
            },
            headers: {  //SE ESPECIFICAN LAS CABECERAS QUE SE ENVIAN AL WEB SERVICE, ES DECIR COMO SE ENVIAN LOS DATOS: JSON, XML, TEXTO PLANO O FILE 
                'Accept': 'application/json',  //SE INDICA QUE EL EVENTO ACEPTARA EL TIPO JSON  
                'Content-Type': 'application/json' // SE INDICA QUE TIPO CONTENDRA LA INFORMACION QUE SE ENVIA   
            }
        })
        .then(function (response) { // ejecucion de WS 
            if (response.data != true) {  //si el procedimiento regresa falso 
                BootstrapDialog.show({ //se mostrara un mensaje de error  
                    title: C_ROL.TITULO_PANTALLA,  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
                    message: Config.ERRORELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo ERRORGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass //se le indica el color dependiendo del modulo en que se encuentre 
                });
            } else {
                $("#Grid_Rol").data("kendoGrid").dataSource.read(); //se manda a refrescar el grid  
                $("#modalDelete").modal('toggle');
                BootstrapDialog.show({  //si el procedimiento regresa true 
                    title: C_ROL.TITULO_PANTALLA,  //el titulo del mensaje  
                    message: Config.MENSAJEELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass,  //se le indica el color dependiendo del modulo en que se encuentre 
                });
            }
        },
        function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
            BootstrapDialog.show({
                title: C_ROL.TITULO_PANTALLA,
                message: Config.ERRORGENERICO,
                size: 'size-small',
                type: tema.cssClass,
            });
        });
    };


    //se le asignan los nombres a los botones y el titulo a nuestro asp   
    $scope.titulo2 = SPE_OBTIENE_C_ROL_FUNCION.TITULO_PANTALLA;
    //inicia evento para consultar los registros  
    //SE CARGA EL TEMPLATE DEL TOOLBAR DEL GRID DONDE IRAN LOS BOTONES DE EDICION Y EXPORTACION  
    //$scope.templateGrid_SPE_OBTIENE_C_ROL_FUNCION = function () {
    //    return "<div class=\"toolbar\"> " +
 	//		"<div style=\"float:right\">  " +
 	//			"<div style=\"float:left\">" +
 	//				"<button id=\"btnExportarExcel_Grid_SPE_OBTIENE_C_ROL_FUNCION\" class=\"btn btn-link FontNegro\" onclick=\"ExportarExcel('Grid_SPE_OBTIENE_C_ROL_FUNCION')\"  > " +
 	//					"<i class=\"fa fa-file-o\" style=\"font-size: 17px;\"></i>  " +
 	//					"" + GridKendo.BOTONEXPORTAREXCEL + " " +
 	//				"</button> " +
 	//			"</div>" +
 	//		"	</div>  " +
 	//	"</div>  "
    //};
    $scope.BOTONEXPORTAREXCEL = GridKendo.BOTONEXPORTAREXCEL
    //se carga el grid  
    $scope.CargarGrid_SPE_OBTIENE_C_ROL_FUNCION = function () {
        NProgress.start();  //muestra el progreso en la pagina  
        $(document).ready(function () {
            $rootScope.VAR_ID_ROL = '???';
            if ($scope.entidad_Rol != undefined) { // se verifica que el registro este seleccionado  
                var datos = $scope.entidad_Rol; //se asigna el registro a la variable de datos  
                $rootScope.VAR_ID_ROL = datos.ID_ROL;
            }
            $("#Grid_SPE_OBTIENE_C_ROL_FUNCION").kendoGrid({
                allowCopy: true//permite copiar los datos para copiar al portapapeles   
                , allowCopy: {
                    delimeter: "," //delimitador cuando se copien los datos del grid   
                }
                , resizable: true //sirve para mover las columnas    
               , excel: {
                   fileName: "SPE_OBTIENE_C_ROL_FUNCION.xlsx" //nombre del archivo a exportar   
                       , allPages: true // indica si se exportan todas las paginas por default esta en false   
                   //,proxyURL: "//demos.telerik.com/kendo-ui/service/export",   
                       , filterable: true //permite que la exportacion sea por lo que se esta filtrando   
               }
               // , toolbar: kendo.template($scope.templateGrid_SPE_OBTIENE_C_ROL_FUNCION())
                , dataSource: {
                    type: "JSON", // JSON, JSONP, OData or XML).   
                    transport: {
                        read: function (options) {
                            $.ajax({
                                type: "POST"
                                , url: "ws/OperacionesGral.svc/Get_SPE_OBTIENE_C_ROL_FUNCION"
                                , data: JSON.stringify({ ID_ROL: $rootScope.VAR_ID_ROL })
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
                               ID_ROL: { type: "number" }
                               , ID_FUNCION: { type: "number" }
                               , NB_ROL: { type: "string" }
                               , CL_ROL: { type: "string" }
                               , XML_AUTORIZACION: { type: "string" }
                               , CL_FUNCION: { type: "string" }
                               , CL_TIPO_FUNCION: { type: "string" }
                               , NB_FUNCION: { type: "string" }
                               , NB_URL: { type: "string" }
                               , XML_CONFIGURACION: { type: "string" }
                               , ID_FUNCION_PADRE: { type: "number" }
                               , DS_FILTRO: { type: "string" }
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
               , columns: [{
                   headerAttributes: { style: "text-align: center; " }
                       , field: "ID_ROL"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.ID_ROL // titulo de la columna 
                       , width: 80 // ancho de la columna 
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
                   , template: "<div >  <label>ID_ROL: <input type=\"number\" name=\"i_ID_ROL\" value = #: ID_ROL # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "ID_FUNCION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.ID_FUNCION // titulo de la columna 
                       , width: 80 // ancho de la columna 
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
                   , template: "<div >  <label>ID_FUNCION: <input type=\"number\" name=\"i_ID_FUNCION\" value = #: ID_FUNCION # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "NB_ROL"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.NB_ROL // titulo de la columna 
                       , width: 200 // ancho de la columna 
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
                   , template: "<div >  <label>NB_ROL: <input name=\"i_NB_ROL\" value = #: NB_ROL # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "CL_ROL"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.CL_ROL // titulo de la columna 
                       , width: 200 // ancho de la columna 
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
                   , template: "<div >  <label>CL_ROL: <input name=\"i_CL_ROL\" value = #: CL_ROL # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "XML_AUTORIZACION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.XML_AUTORIZACION // titulo de la columna 
                       , width: 200 // ancho de la columna 
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
                   , template: "<div >  <label>XML_AUTORIZACION: <input name=\"i_XML_AUTORIZACION\" value = #: XML_AUTORIZACION # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "CL_FUNCION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.CL_FUNCION // titulo de la columna 
                       , width: 200 // ancho de la columna 
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
                   , template: "<div >  <label>CL_FUNCION: <input name=\"i_CL_FUNCION\" value = #: CL_FUNCION # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "CL_TIPO_FUNCION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.CL_TIPO_FUNCION // titulo de la columna 
                       , width: 200 // ancho de la columna 
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
                   , template: "<div >  <label>CL_TIPO_FUNCION: <input name=\"i_CL_TIPO_FUNCION\" value = #: CL_TIPO_FUNCION # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "NB_FUNCION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.NB_FUNCION // titulo de la columna 
                       , width: 200 // ancho de la columna 
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
                   , template: "<div >  <label>NB_FUNCION: <input name=\"i_NB_FUNCION\" value = #: NB_FUNCION # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "NB_URL"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.NB_URL // titulo de la columna 
                       , width: 200 // ancho de la columna 
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
                   , template: "<div >  <label>NB_URL: <input name=\"i_NB_URL\" value = #: NB_URL # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "XML_CONFIGURACION"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.XML_CONFIGURACION // titulo de la columna 
                       , width: 200 // ancho de la columna 
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
                   , template: "<div >  <label>XML_CONFIGURACION: <input name=\"i_XML_CONFIGURACION\" value = #: XML_CONFIGURACION # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "ID_FUNCION_PADRE"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.ID_FUNCION_PADRE // titulo de la columna 
                       , width: 80 // ancho de la columna 
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
                   , template: "<div >  <label>ID_FUNCION_PADRE: <input type=\"number\" name=\"i_ID_FUNCION_PADRE\" value = #: ID_FUNCION_PADRE # /></label>  </div> "
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "DS_FILTRO"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: SPE_OBTIENE_C_ROL_FUNCION.DS_FILTRO // titulo de la columna 
                       , width: 200 // ancho de la columna 
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
                   , template: "<div >  <label>DS_FILTRO: <input name=\"i_DS_FILTRO\" value = #: DS_FILTRO # /></label>  </div> "
               }
               ]
               , change: function (e) {  //evento que indica cuando cambia de elemento entre una y otra linea  
                   var selectedRows = this.select(); //se obtiene el listado de los elementos seleccionados  
                   var dataItem = this.dataItem(selectedRows[0]); //se sacan los datos del registro seleccionado  
                   $scope.entidad_SPE_OBTIENE_C_ROL_FUNCION = dataItem; //se asigna los datos a la variable de que se crea en el scope  
               }
            });
        });        
    };
    //termina evento para consultar los registros  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia evento para agregar el registro  
    $scope.Add_Grid_SPE_OBTIENE_C_ROL_FUNCION = function () {
        var Obj_SPE_OBTIENE_C_ROL_FUNCION = { //se crea la entidad para enviarla al sp 
            ID_ROL: undefined
            , ID_FUNCION: ''
            , NB_ROL: ''
            , CL_ROL: ''
            , XML_AUTORIZACION: ''
            , CL_FUNCION: ''
            , CL_TIPO_FUNCION: ''
            , NB_FUNCION: ''
            , NB_URL: ''
            , XML_CONFIGURACION: ''
            , ID_FUNCION_PADRE: ''
            , DS_FILTRO: ''
        };
        var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
            animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
            templateUrl: 'op_SPE_OBTIENE_C_ROL_FUNCION.html', //se le indica el html que se abrira 
            controller: 'Modal_SPE_OBTIENE_C_ROL_FUNCION', //se le indica el controlador del html 
            resolve: { //se le envia el objeto 
                OBJETO: function () { return Obj_SPE_OBTIENE_C_ROL_FUNCION; }
            }
        });
    };

    //termina evento para agregar el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia evento para edita el registro  
    $scope.Edit_Grid_SPE_OBTIENE_C_ROL_FUNCION = function () {
        if ($scope.entidad_SPE_OBTIENE_C_ROL_FUNCION == undefined) { // se verifica que el registro este seleccionado  
            BootstrapDialog.show({ //se muestra el mensaje de error  
                title: SPE_OBTIENE_C_ROL_FUNCION.TITULO_PANTALLA, //se trae el nombre de la pantalla  
                message: Config.MENSAJEREGISTRO, // se envia el mensaje de error que falta el registro  
                size: 'size-small', //se especifica el tamaño de la ventana del mensaje  
                type: tema.RGB //se especifica el color en base al modulo que se tiene  
            });
            return false; //se devuelve nulo para que no sigua con la funcion  
        }
        var datos = $scope.entidad_SPE_OBTIENE_C_ROL_FUNCION; //se asigna el registro a la variable de datos  
        var Obj_SPE_OBTIENE_C_ROL_FUNCION = { //se define el objeto que se enviara  
            //se le asigna valor a los campos de la entidad en base a lo que tiene el registro  
            ID_ROL: datos.ID_ROL
            , ID_FUNCION: datos.ID_FUNCION
            , NB_ROL: datos.NB_ROL
            , CL_ROL: datos.CL_ROL
            , XML_AUTORIZACION: datos.XML_AUTORIZACION
            , CL_FUNCION: datos.CL_FUNCION
            , CL_TIPO_FUNCION: datos.CL_TIPO_FUNCION
            , NB_FUNCION: datos.NB_FUNCION
            , NB_URL: datos.NB_URL
            , XML_CONFIGURACION: datos.XML_CONFIGURACION
            , ID_FUNCION_PADRE: datos.ID_FUNCION_PADRE
            , DS_FILTRO: datos.DS_FILTRO
        };
        var modalInstance = $modal.open({ //se manda ejecutar en modo modal la instancia definida en el html para la edicion  
            animation: $scope.animationsEnabled, //se le asigna la variable que nos indica si tiene animacion o no  
            templateUrl: 'op_SPE_OBTIENE_C_ROL_FUNCION.html', //se le indica el html que se abrira  
            controller: 'Modal_SPE_OBTIENE_C_ROL_FUNCION', //se le indica el controlador del html  
            resolve: { //se le envia el objeto  
                OBJETO: function () { return Obj_SPE_OBTIENE_C_ROL_FUNCION; }
            }
        });
    };

    //termina evento para edita el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia eventos para eliminar el registro  
    $scope.Del_Grid_SPE_OBTIENE_C_ROL_FUNCION = function () {
        if ($scope.entidad_SPE_OBTIENE_C_ROL_FUNCION == undefined) { //se verifica si la entidad que guarda el evento onchange del grid no se encuentra vacia  
            BootstrapDialog.show({ //en caso de que no se encuentre se mostrara un error  
                title: SPE_OBTIENE_C_ROL_FUNCION.TITULO_PANTALLA, //se le pone el titulo especificado en la clase   
                message: Config.MENSAJEREGISTRO, //se manda ejecutar el mensaje de seleccione un registro  
                size: 'size-small',// se le indica el tamaño de la ventana del mensaje  
                type: tema.RGB //se le indica el color dependiendo del modulo que tiene  
            });
            return false; //se regresa false para que se salga de la funcion  
        }
        BootstrapDialog.show({ //se muestra el mensaje si la entidad es diferente a nulo  
            message: Config.MENSAJECONFIRMADEL,  //mensaje de confirmacion  
            id: 'modalDelete',
            size: 'size-small', //tamaño de la ventana  
            type: tema.RGB, //se le indica el color dependiendo del modulo que tiene  
            buttons: [{ //se le agregan botones  
                label: Config.BOTONELIMINAR, //se le indica en la etiqueta el mensaje que tendra el boton de eliminar  
                action: function (result) { //se especifica la funcion que hara este boton  
                    if (result) { //en caso de que sea afirmativo  
                        $scope.Eliminar_SPE_OBTIENE_C_ROL_FUNCION();  //se ejecuta la funcion de elimina_?? especificada para borrar el registro  
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


    $scope.Eliminar_SPE_OBTIENE_C_ROL_FUNCION = function () {
        var datos = $scope.entidad_SPE_OBTIENE_C_ROL_FUNCION;  //se extrae el registro obtenido en el evento change del grid 
        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/Delete_SPE_OBTIENE_C_ROL_FUNCION",
            method: "POST",
            data: { //se le pasan los parametros 
                ID_ROL: datos.ID_ROL, // se le pasa la llave primaria  
                usuario: TraerUsuario(), // se le pasa el usuario 
                //ojo modificar   
                programa: "CatalogoSPE_OBTIENE_C_ROL_FUNCION.html"  //se le pasa  
            },
            headers: {  //SE ESPECIFICAN LAS CABECERAS QUE SE ENVIAN AL WEB SERVICE, ES DECIR COMO SE ENVIAN LOS DATOS: JSON, XML, TEXTO PLANO O FILE 
                'Accept': 'application/json',  //SE INDICA QUE EL EVENTO ACEPTARA EL TIPO JSON  
                'Content-Type': 'application/json' // SE INDICA QUE TIPO CONTENDRA LA INFORMACION QUE SE ENVIA   
            }
        })
        .then(function (response) { // ejecucion de WS 
            if (response.data != true) {  //si el procedimiento regresa falso 
                BootstrapDialog.show({ //se mostrara un mensaje de error  
                    title: SPE_OBTIENE_C_ROL_FUNCION.TITULO_PANTALLA,  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
                    message: Config.ERRORELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo ERRORGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.RGB //se le indica el color dependiendo del modulo en que se encuentre 
                });
            } else {
                $("#Grid_SPE_OBTIENE_C_ROL_FUNCION").data("kendoGrid").dataSource.read(); //se manda a refrescar el grid  
                $("#modalDelete").modal('toggle');
                BootstrapDialog.show({  //si el procedimiento regresa true 
                    title: SPE_OBTIENE_C_ROL_FUNCION.TITULO_PANTALLA,  //el titulo del mensaje  
                    message: Config.MENSAJEELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.RGB,  //se le indica el color dependiendo del modulo en que se encuentre 
                });
            }
        },
        function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
            BootstrapDialog.show({
                title: SPE_OBTIENE_C_ROL_FUNCION.TITULO_PANTALLA,
                message: Config.ERRORGENERICO,
                size: 'size-small',
                type: tema.RGB,
            });
        });
    };
    //termina eventos para eliminar el registro  
    ////////////////////////////////////////////////////////////// 

    //Se manda a cargar cuando se genere el controlador en el html 
    $scope.CargarGrid_SPE_OBTIENE_C_ROL_FUNCION();




    //Se manda a cargar cuando se genere el controlador en el html 
    $scope.CargarGrid_Rol();
});


//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR C_ROL 
app.controller('Modal_Rol', function ($scope, $modalInstance, $http, OBJETO, $rootScope) {
    $scope.CL_ROL = OBJETO.CL_ROL;
    $scope.NB_ROL = OBJETO.NB_ROL;
    $scope.FG_ACTIVO = Boolean(OBJETO.FG_ACTIVO);
    $scope.ID_ROL = OBJETO.ID_ROL;

    //se generan las traducciones de las etiquetas, placeholder (ayuda en los componentes) 
    $scope.lbl_CL_ROL = C_ROL.CL_ROL;
    $scope.ph_CL_ROL = C_ROL.CL_ROL_ph;
    $scope.lbl_NB_ROL = C_ROL.NB_ROL;
    $scope.ph_NB_ROL = C_ROL.NB_ROL_ph;
    $scope.lbl_FG_ACTIVO = C_ROL.FG_ACTIVO;
    $scope.ph_FG_ACTIVO = C_ROL.FG_ACTIVO_ph;

    //se generan las traducciones Genericas 
    $scope.titulo = C_ROL.TITULO_PANTALLA;
    $scope.msjRequerido = Config.MSJREQUERIDO;
    $scope.btnSave = Config.BOTONGUARDAR;
    $scope.btnCancel = Config.BOTONCANCELAR;
    $scope.modulo = tema.cssClass;

    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente  
        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
        if ($scope.ID_ROL != undefined)  //se verifica que la clave primaria sea diferente de nulo  
            tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  
        var Obj_Rol = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_ROL: $scope.CL_ROL
 			, NB_ROL: $scope.NB_ROL
 			, FG_ACTIVO: $scope.FG_ACTIVO
            , ID_ROL: $scope.ID_ROL
        };
        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_ROL",
            method: "POST",
            data: {
                V_C_ROL: Obj_Rol,
                usuario: TraerUsuario(),
                //ojo modificar   
                programa: "InsertaRol.html",
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
 		        if ($scope.ID_ROL == undefined) {
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
        $("#Grid_Rol").data("kendoGrid").dataSource.read();
        $modalInstance.dismiss('cancel');
    };
});


//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR SPE_OBTIENE_C_ROL_FUNCION 
app.controller('Modal_SPE_OBTIENE_C_ROL_FUNCION', function ($scope, $modalInstance, $http, OBJETO, $rootScope) {
    $scope.ID_ROL = $rootScope.VAR_ID_ROL //parseFloat(OBJETO.ID_ROL);  
    $scope.ID_FUNCION = OBJETO.ID_FUNCION //parseFloat(OBJETO.ID_FUNCION);  
   

    //se generan las traducciones de las etiquetas, placeholder (ayuda en los componentes) 
    $scope.lbl_ID_ROL = SPE_OBTIENE_C_ROL_FUNCION.ID_ROL;
    $scope.ph_ID_ROL = SPE_OBTIENE_C_ROL_FUNCION.ID_ROL_ph;
    $scope.lbl_ID_FUNCION = SPE_OBTIENE_C_ROL_FUNCION.ID_FUNCION;
    $scope.ph_ID_FUNCION = SPE_OBTIENE_C_ROL_FUNCION.ID_FUNCION_ph;
    $scope.lbl_NB_ROL = SPE_OBTIENE_C_ROL_FUNCION.NB_ROL;
    $scope.ph_NB_ROL = SPE_OBTIENE_C_ROL_FUNCION.NB_ROL_ph;
    $scope.lbl_CL_ROL = SPE_OBTIENE_C_ROL_FUNCION.CL_ROL;
    $scope.ph_CL_ROL = SPE_OBTIENE_C_ROL_FUNCION.CL_ROL_ph;
    $scope.lbl_XML_AUTORIZACION = SPE_OBTIENE_C_ROL_FUNCION.XML_AUTORIZACION;
    $scope.ph_XML_AUTORIZACION = SPE_OBTIENE_C_ROL_FUNCION.XML_AUTORIZACION_ph;
    $scope.lbl_CL_FUNCION = SPE_OBTIENE_C_ROL_FUNCION.CL_FUNCION;
    $scope.ph_CL_FUNCION = SPE_OBTIENE_C_ROL_FUNCION.CL_FUNCION_ph;
    $scope.lbl_CL_TIPO_FUNCION = SPE_OBTIENE_C_ROL_FUNCION.CL_TIPO_FUNCION;
    $scope.ph_CL_TIPO_FUNCION = SPE_OBTIENE_C_ROL_FUNCION.CL_TIPO_FUNCION_ph;
    $scope.lbl_NB_FUNCION = SPE_OBTIENE_C_ROL_FUNCION.NB_FUNCION;
    $scope.ph_NB_FUNCION = SPE_OBTIENE_C_ROL_FUNCION.NB_FUNCION_ph;
    $scope.lbl_NB_URL = SPE_OBTIENE_C_ROL_FUNCION.NB_URL;
    $scope.ph_NB_URL = SPE_OBTIENE_C_ROL_FUNCION.NB_URL_ph;
    $scope.lbl_XML_CONFIGURACION = SPE_OBTIENE_C_ROL_FUNCION.XML_CONFIGURACION;
    $scope.ph_XML_CONFIGURACION = SPE_OBTIENE_C_ROL_FUNCION.XML_CONFIGURACION_ph;
    $scope.lbl_ID_FUNCION_PADRE = SPE_OBTIENE_C_ROL_FUNCION.ID_FUNCION_PADRE;
    $scope.ph_ID_FUNCION_PADRE = SPE_OBTIENE_C_ROL_FUNCION.ID_FUNCION_PADRE_ph;
    $scope.lbl_DS_FILTRO = SPE_OBTIENE_C_ROL_FUNCION.DS_FILTRO;
    $scope.ph_DS_FILTRO = SPE_OBTIENE_C_ROL_FUNCION.DS_FILTRO_ph;

    //se generan las traducciones Genericas 
    $scope.titulo = SPE_OBTIENE_C_ROL_FUNCION.TITULO_PANTALLA;
    $scope.msjRequerido = Config.MSJREQUERIDO;
    $scope.btnSave = Config.BOTONGUARDAR;
    $scope.btnCancel = Config.BOTONCANCELAR;
    $scope.modulo = tema.ccsClass;

    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente  
        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
        if ($scope.ID_ROL != undefined)  //se verifica que la clave primaria sea diferente de nulo  
            tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  
        var Obj_SPE_OBTIENE_C_ROL_FUNCION = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            ID_ROL: $scope.ID_ROL
 			, ID_FUNCION: $scope.ID_FUNCION
 			//, NB_ROL: $scope.NB_ROL
            //, CL_ROL: $scope.CL_ROL
            //, XML_AUTORIZACION: $scope.XML_AUTORIZACION
            //, CL_FUNCION: $scope.CL_FUNCION
            //, CL_TIPO_FUNCION: $scope.CL_TIPO_FUNCION
            //, NB_FUNCION: $scope.NB_FUNCION
            //, NB_URL: $scope.NB_URL
            //, XML_CONFIGURACION: $scope.XML_CONFIGURACION
            //, ID_FUNCION_PADRE: $scope.ID_FUNCION_PADRE
            //, DS_FILTRO: $scope.DS_FILTRO
        };
        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_ROL_FUNCION",
            method: "POST",
            data: {
                V_SPE_OBTIENE_C_ROL_FUNCION: Obj_SPE_OBTIENE_C_ROL_FUNCION,
                usuario: TraerUsuario(),
                //ojo modificar   
                programa: "Inserta_C_ROL_FUNCION.html",
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
 		            type: tema.RGB
 		        });
 		    } else {
 		        if ($scope.ID_ROL == undefined) {
 		            LimpiarFormulario("newForm");
 		        }
 		        BootstrapDialog.show({
 		            title: '',
 		            message: Config.MENSAJEGUARDAR,
 		            size: 'size-small',
 		            type: tema.RGB
 		        });
 		    }
 		},
 		function (response) {
 		    BootstrapDialog.show({
 		        title: Config.MENSAJEERROR,
 		        message: Config.ERRORGENERICO,
 		        size: 'size-small',
 		        type: tema.RGB
 		    });
 		});
    };
    $scope.cancel = function () {
        $("#Grid_SPE_OBTIENE_C_ROL_FUNCION").data("kendoGrid").dataSource.read();
        $modalInstance.dismiss('cancel');
    };
});



