

//poner en (Nombre igual a Nuestro HTML).js 
app.controller('C_COMPETENCIAController', function ($scope, $http, $modal) {

    //habilita la animacion del popup para que se despliegue de arriba para abajo  
    $scope.animationsEnabled = true;
    


    $scope.btnActivarDesactivar = Config.BOTONACTIVARDESACTIVAR;
    $scope.btnAgregar = Config.BOTONAGREGAR;
    $scope.btnModificar = Config.BOTONMODIFICAR;
    $scope.btnEliminar = Config.BOTONELIMINAR;
    

    $scope.titulo = C_COMPETENCIA.TITULO_PANTALLA;
    ////////////////////////////////////////////////////////////// 
    //inicia evento para consultar los registros  
    //SE CARGA EL TEMPLATE DEL TOOLBAR DEL GRID DONDE IRAN LOS BOTONES DE EDICION Y EXPORTACION  
    //$scope.templateGrid_Competencia = function () {
    //    return "<div class=\"toolbar\"> " +
 	//		"<div style=\"float:right\">  " +
 	//			"<div style=\"float:left\">" +
 	//				"<button id=\"btnExportarExcel_Grid_Competencia\" class=\"btn btn-link FontNegro\" onclick=\"ExportarExcel('Grid_Competencia')\"  > " +
 	//					"<i class=\"fa fa-file-o\" style=\"font-size: 17px;\"></i>  " +
 	//					"" + GridKendo.BOTONEXPORTAREXCEL + " " +
 	//				"</button> " +
 	//			"</div>" +
 	//		"	</div>  " +
 	//	"</div>  "
    //};


    $scope.BOTONEXPORTAREXCEL = GridKendo.BOTONEXPORTAREXCEL
   
    //se carga el grid  
    $scope.CargarGrid_Competencia = function () {
        NProgress.start();  //muestra el progreso en la pagina  
        $(document).ready(function () {
            $("#Grid_Competencia").kendoGrid({
                allowCopy: true//permite copiar los datos para copiar al portapapeles   
                , allowCopy: {
                    delimeter: "," //delimitador cuando se copien los datos del grid   
                }
                , resizable: true //sirve para mover las columnas
               , excel: {
                   fileName: "Competencia.xlsx" //nombre del archivo a exportar   
                       , allPages: true // indica si se exportan todas las paginas por default esta en false   
                   //,proxyURL: "//demos.telerik.com/kendo-ui/service/export",   
                       , filterable: true //permite que la exportacion sea por lo que se esta filtrando   
               }
                //, toolbar: kendo.template($scope.templateGrid_Competencia())
                , dataSource: {
                    type: "JSON", // JSON, JSONP, OData or XML).   
                    transport: {
                        read: function (options) {
                            $.ajax({
                                type: "POST"
                                , url: "ws/OperacionesGral.svc/Get_C_COMPETENCIA"
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
                               , CL_COMPETENCIA: { type: "string" }
                               , CL_TIPO_COMPETENCIA: { type: "string" }
                               , DS_COMPETENCIA: { type: "string" }
                               , FG_ACTIVO: { type: "boolean" }
                               , NB_COMPETENCIA: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PUESTO_N0: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PERSONA_N0: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PUESTO_N1: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PERSONA_N1: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PUESTO_N2: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PERSONA_N2: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PUESTO_N3: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PERSONA_N3: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PUESTO_N4: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PERSONA_N4: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PUESTO_N5: { type: "string" }
                                , DS_NIVEL_COMPETENCIA_PERSONA_N5: { type: "string" }
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
               , groupable: true // agrupacion por columnas en la parte del grid	 debe de ir despues de la definicion de los mensaje de lo contrario si le pones el false no te lo estara tomando en cuenta			   
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
                       , field: "CL_COMPETENCIA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COMPETENCIA.CL_COMPETENCIA // titulo de la columna 
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
                       , field: "NB_COMPETENCIA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COMPETENCIA.NB_COMPETENCIA // titulo de la columna 
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
                    , field: "NB_CLASIFICACION_COMPETENCIA"
                    , align: "center"  //alineacion del titulo de la columna 
                    //, attributes: { title: "hola tooltip" }  
                    , title: C_COMPETENCIA.NB_CLASIFICACION_COMPETENCIA // titulo de la columna 
                    , width: 50 // ancho de la columna 
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
                    , field: "NB_TIPO_COMPETENCIA"
                    , align: "center"  //alineacion del titulo de la columna 
                    //, attributes: { title: "hola tooltip" }  
                    , title: C_COMPETENCIA.NB_TIPO_COMPETENCIA // titulo de la columna 
                    , width: 50 // ancho de la columna 
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
                       , field: "DS_COMPETENCIA"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COMPETENCIA.DS_COMPETENCIA // titulo de la columna 
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
               },
                  {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "FG_ACTIVO"
                       , align: "center"  //alineacion del titulo de la columna 
                   //, attributes: { title: "hola tooltip" }  
                       , title: C_COMPETENCIA.FG_ACTIVO // titulo de la columna 
                       , width: 50 // ancho de la columna 
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
                   $scope.entidad_Competencia = dataItem; //se asigna los datos a la variable de que se crea en el scope  
               }
            });
        });
       
    };
    //termina evento para consultar los registros  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia evento para agregar el registro  
    $scope.Add_Grid_Competencia = function () {
        var Obj_Competencia = { //se crea la entidad para enviarla al sp 
            CL_CLASIFICACION: ''
            , CL_COMPETENCIA: ''
            , CL_TIPO_COMPETENCIA: ''
            , DS_COMPETENCIA: ''
            , FG_ACTIVO: ''
            , NB_COMPETENCIA: ''
            , ID_COMPETENCIA: undefined
            , DS_NIVEL_COMPETENCIA_PUESTO_N0: ''
            , DS_NIVEL_COMPETENCIA_PERSONA_N0: ''
            , DS_NIVEL_COMPETENCIA_PUESTO_N1: ''
            , DS_NIVEL_COMPETENCIA_PERSONA_N1: ''
            , DS_NIVEL_COMPETENCIA_PUESTO_N2: ''
            , DS_NIVEL_COMPETENCIA_PERSONA_N2: ''
            , DS_NIVEL_COMPETENCIA_PUESTO_N3: ''
            , DS_NIVEL_COMPETENCIA_PERSONA_N3: ''
            , DS_NIVEL_COMPETENCIA_PUESTO_N4: ''
            , DS_NIVEL_COMPETENCIA_PERSONA_N4: ''
            , DS_NIVEL_COMPETENCIA_PUESTO_N5: ''
            , DS_NIVEL_COMPETENCIA_PERSONA_N5: ''
        };
        var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
            animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
            templateUrl: 'op_Competencia.html', //se le indica el html que se abrira 
            controller: 'Modal_Competencia', //se le indica el controlador del html 
            resolve: { //se le envia el objeto 
                OBJETO: function () { return Obj_Competencia; }
            }
        });
    };

    //termina evento para agregar el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia evento para edita el registro  
    $scope.Edit_Grid_Competencia = function () {
        if ($scope.entidad_Competencia == undefined) { // se verifica que el registro este seleccionado  
            BootstrapDialog.show({ //se muestra el mensaje de error  
                title: C_COMPETENCIA.TITULO_PANTALLA, //se trae el nombre de la pantalla  
                message: Config.MENSAJEREGISTRO, // se envia el mensaje de error que falta el registro  
                size: 'size-small', //se especifica el tamaño de la ventana del mensaje  
                type: tema.cssClass //se especifica el color en base al modulo que se tiene  
            });
            return false; //se devuelve nulo para que no sigua con la funcion  
        }
        var datos = $scope.entidad_Competencia; //se asigna el registro a la variable de datos  
        var Obj_Competencia = { //se define el objeto que se enviara  
            //se le asigna valor a los campos de la entidad en base a lo que tiene el registro  
            CL_CLASIFICACION: datos.CL_CLASIFICACION
            , CL_COMPETENCIA: datos.CL_COMPETENCIA
            , CL_TIPO_COMPETENCIA: datos.CL_TIPO_COMPETENCIA
            , DS_COMPETENCIA: datos.DS_COMPETENCIA
            , FG_ACTIVO: datos.FG_ACTIVO
            , NB_COMPETENCIA: datos.NB_COMPETENCIA
            , ID_COMPETENCIA: datos.ID_COMPETENCIA
            , DS_NIVEL_COMPETENCIA_PUESTO_N0: datos.DS_NIVEL_COMPETENCIA_PUESTO_N0
            , DS_NIVEL_COMPETENCIA_PERSONA_N0: datos.DS_NIVEL_COMPETENCIA_PERSONA_N0
            , DS_NIVEL_COMPETENCIA_PUESTO_N1: datos.DS_NIVEL_COMPETENCIA_PUESTO_N1
            , DS_NIVEL_COMPETENCIA_PERSONA_N1: datos.DS_NIVEL_COMPETENCIA_PERSONA_N1
            , DS_NIVEL_COMPETENCIA_PUESTO_N2: datos.DS_NIVEL_COMPETENCIA_PUESTO_N2
            , DS_NIVEL_COMPETENCIA_PERSONA_N2: datos.DS_NIVEL_COMPETENCIA_PERSONA_N2
            , DS_NIVEL_COMPETENCIA_PUESTO_N3: datos.DS_NIVEL_COMPETENCIA_PUESTO_N3
            , DS_NIVEL_COMPETENCIA_PERSONA_N3: datos.DS_NIVEL_COMPETENCIA_PERSONA_N3
            , DS_NIVEL_COMPETENCIA_PUESTO_N4: datos.DS_NIVEL_COMPETENCIA_PUESTO_N4
            , DS_NIVEL_COMPETENCIA_PERSONA_N4: datos.DS_NIVEL_COMPETENCIA_PERSONA_N4
            , DS_NIVEL_COMPETENCIA_PUESTO_N5: datos.DS_NIVEL_COMPETENCIA_PUESTO_N5
            , DS_NIVEL_COMPETENCIA_PERSONA_N5: datos.DS_NIVEL_COMPETENCIA_PERSONA_N5
        };
        var modalInstance = $modal.open({ //se manda ejecutar en modo modal la instancia definida en el html para la edicion  
            animation: $scope.animationsEnabled, //se le asigna la variable que nos indica si tiene animacion o no  
            templateUrl: 'op_Competencia.html', //se le indica el html que se abrira  
            controller: 'Modal_Competencia', //se le indica el controlador del html  
            resolve: { //se le envia el objeto  
                OBJETO: function () { return Obj_Competencia; }
            }
        });
    };

    //termina evento para edita el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia eventos para eliminar el registro  
    $scope.Del_Grid_Competencia = function () {
        if ($scope.entidad_Competencia == undefined) { //se verifica si la entidad que guarda el evento onchange del grid no se encuentra vacia  
            
            BootstrapDialog.show({ //en caso de que no se encuentre se mostrara un error  
                title: '', //se le pone el titulo especificado en la clase   
                message: C_COMPETENCIA.MENSAJEREGISTRO, //se manda ejecutar el mensaje de seleccione un registro   
                size: 'size-small',// se le indica el tamaño de la ventana del mensaje  
                type: tema.cssClass//se le indica el color dependiendo del modulo que tiene  
            });
            return false; //se regresa false para que se salga de la funcion  
        }
        var vCL_TIPO_COMPETENCIA = $scope.entidad_Competencia.CL_TIPO_COMPETENCIA
        if (vCL_TIPO_COMPETENCIA == "GEN") 
        {
            BootstrapDialog.show({
                title: '',
                message: "No puedes eliminar este campo porque forma parte del catálogo predefinido de competencias genéricas.",
                size: 'size-normal',
                type: tema.cssClass
                , buttons: [{ //se le agregan botones  
                    label: Config.BOTONACEPTAR, //se le indica en la etiqueta el mensaje que tendra el boton de eliminar  
                    action: function (dialogItself) { //se indica la funcion enviando a si mismo para luego cerrar  
                        dialogItself.close(); //se realiza el evento de cerrar ventana de dialogo  
                    }
                }]
            });
            return false; //se regresa false para que se salga de la funcion  
        };
        BootstrapDialog.show({ //se muestra el mensaje si la entidad es diferente a nulo  
               
            message: "¿Deseas eliminar la Competencia laboral Expresión escrita?  <br> " +
                        "(Esta acción no se puede deshacer)", //se manda ejecutar el mensaje de seleccione un registro  
            id: 'modalDelete',
            size: 'size-normal', //tamaño de la ventana  
            type: tema.cssClass, //se le indica el color dependiendo del modulo que tiene  
            buttons: [{ //se le agregan botones  
                label: Config.BOTONELIMINAR, //se le indica en la etiqueta el mensaje que tendra el boton de eliminar  
                action: function (result) { //se especifica la funcion que hara este boton  
                    if (result) { //en caso de que sea afirmativo  
                        $scope.Eliminar_Competencia();  //se ejecuta la funcion de elimina_?? especificada para borrar el registro  
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


    $scope.Eliminar_Competencia = function () {
        var datos = $scope.entidad_Competencia;  //se extrae el registro obtenido en el evento change del grid 
        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/Delete_C_COMPETENCIA",
            method: "POST",
            data: { //se le pasan los parametros 
                ID_COMPETENCIA: datos.ID_COMPETENCIA, // se le pasa la llave primaria  
                usuario: TraerUsuario(), // se le pasa el usuario 
                //ojo modificar   
                programa: "CatalogoCompetencia.html"  //se le pasa  
            },
            headers: {  //SE ESPECIFICAN LAS CABECERAS QUE SE ENVIAN AL WEB SERVICE, ES DECIR COMO SE ENVIAN LOS DATOS: JSON, XML, TEXTO PLANO O FILE 
                'Accept': 'application/json',  //SE INDICA QUE EL EVENTO ACEPTARA EL TIPO JSON  
                'Content-Type': 'application/json' // SE INDICA QUE TIPO CONTENDRA LA INFORMACION QUE SE ENVIA   
            }
        })
        .then(function (response) { // ejecucion de WS 
            if (response.data != true) {  //si el procedimiento regresa falso 
                BootstrapDialog.show({ //se mostrara un mensaje de error  
                    title: C_COMPETENCIA.TITULO_PANTALLA,  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
                    message: Config.ERRORELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo ERRORGUARDAR 
                    size: 'size-normal',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass //se le indica el color dependiendo del modulo en que se encuentre 
                });
            } else {
                $("#Grid_Competencia").data("kendoGrid").dataSource.read(); //se manda a refrescar el grid  
                $("#modalDelete").modal('toggle');
                BootstrapDialog.show({  //si el procedimiento regresa true 
                    title: C_COMPETENCIA.TITULO_PANTALLA,  //el titulo del mensaje  
                    message: Config.MENSAJEELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass,  //se le indica el color dependiendo del modulo en que se encuentre 
                });
            }
        },
        function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
            BootstrapDialog.show({
                title: C_COMPETENCIA.TITULO_PANTALLA,
                message: Config.ERRORGENERICO,
                size: 'size-small',
                type: tema.cssClass,
            });
        });
    };
    //termina eventos para eliminar el registro  
    ////////////////////////////////////////////////////////////// 

    //Se manda a cargar cuando se genere el controlador en el html 
    $scope.CargarGrid_Competencia();


    $scope.ActivarDesactivar_Grid_Competencia = function () {
        var datos = $scope.entidad_Competencia;  //se extrae el registro obtenido en el evento change del grid 
        var tipoOperacion = "A"
        var Obj_Competencia = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_CLASIFICACION: datos.CL_CLASIFICACION
            ,ID_COMPETENCIA : datos.ID_COMPETENCIA
 			, CL_COMPETENCIA: datos.CL_COMPETENCIA
 			, CL_TIPO_COMPETENCIA: datos.CL_TIPO_COMPETENCIA
 			, DS_COMPETENCIA: datos.DS_COMPETENCIA
 			, FG_ACTIVO: !datos.FG_ACTIVO
 			, NB_COMPETENCIA: datos.NB_COMPETENCIA
        };
        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_COMPETENCIA",
            method: "POST",
            data: {
                V_C_COMPETENCIA: Obj_Competencia,
                usuario: TraerUsuario(),
                //ojo modificar   
                programa: "InsertaCompetencia.html",
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
 		          //  title: C_COMPETENCIA.TITULO_PANTALLA,
 		            message: Config.ERRORACTIVARDESACTIVAR,
 		            size: 'size-small',
 		            type: tema.cssClass
 		        });
 		    } else {
 		        $("#Grid_Competencia").data("kendoGrid").dataSource.read(); //se manda a refrescar el grid   
 		        $("#modalDelete").modal('toggle');
 		        BootstrapDialog.show({
 		           // title: C_COMPETENCIA.TITULO_PANTALLA,
 		            message: Config.MENSAJEACTIVARDESACTIVAR,
 		            size: 'size-small',
 		            type: tema.cssClass
 		        });
 		    }
 		},
 		function (response) {
 		    BootstrapDialog.show({
 		      //  title: Config.MENSAJEERROR,
 		        message: Config.ERRORGENERICO,
 		        size: 'size-small',
 		        type: tema.cssClass
 		    });
 		});
    };
    

  
});


//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR C_COMPETENCIA 
app.controller('Modal_Competencia', function ($scope, $modalInstance, $http, OBJETO) {

    //habilita la animacion del popup para que se despliegue de arriba para abajo  
    $scope.animationsEnabled = true;
    $scope.GENERALES = C_COMPETENCIA.GENERALES;
    $scope.DEFINICION_DE_NIVELES = C_COMPETENCIA.DEFINICION_DE_NIVELES;
    $scope.panelBarOptions = {
        contentUrls: [null, null, "../content/web/loremIpsum.html"]
    };
    $scope.hello = "Hello from controller";
    
    //se inicializa el tipo de competencia para el combobox 
    $scope.TipoCompetencia = //new kendo.data.DataSource(
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
                        console.log(result);
                        options.success(result);
                    }
                });
            }
        }
    };

   // var TipoCompetencia2 = TipoCompetencia.push({ CL_TIPO_COMPETENCIA: "", DS_TIPO_COMPETENCIA: "", FG_ACTIVO: "", NB_TIPO_COMPETENCIA: "" });

    $scope.customOptions_CL_TIPO_COMPETENCIA = {
        dataSource: $scope.TipoCompetencia,
        dataTextField: "DS_TIPO_COMPETENCIA", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_TIPO_COMPETENCIA", //valor del combo cuando se seleccione
        filter: "contains"
    };
    //se inicializa la clasificacion de competencia para el combobox
    $scope.ClasificacionCompetencia =// new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_C_CLASIFICACION_COMPETENCIA"
                        , dataType: "json"
                        , data: JSON.stringify({ FG_ACTIVO: true})
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            console.log(result);
                            options.success(result);
                        }
                    });
                }
            }
        }//)
    ;    

    $scope.customOptions_CL_CLASIFICACION = {
        cascadeFrom: "txt_CL_TIPO_COMPETENCIA",
        cascadeFromField: "CL_TIPO_COMPETENCIA",
        dataSource: $scope.ClasificacionCompetencia,
        dataTextField: "NB_CLASIFICACION_COMPETENCIA", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_CLASIFICACION", //valor del combo cuando se seleccione
        filter: "contains"
    };
     
    $scope.CL_CLASIFICACION = OBJETO.CL_CLASIFICACION;
    $scope.CL_COMPETENCIA = OBJETO.CL_COMPETENCIA;
    $scope.CL_TIPO_COMPETENCIA = OBJETO.CL_TIPO_COMPETENCIA;
    $scope.DS_COMPETENCIA = OBJETO.DS_COMPETENCIA;    
    $scope.FG_ACTIVO = Boolean(OBJETO.FG_ACTIVO);
    $scope.NB_COMPETENCIA = OBJETO.NB_COMPETENCIA;
    $scope.ID_COMPETENCIA = OBJETO.ID_COMPETENCIA;
    $scope.DS_NIVEL_COMPETENCIA_PUESTO_N0 = OBJETO.DS_NIVEL_COMPETENCIA_PUESTO_N0;
    $scope.DS_NIVEL_COMPETENCIA_PERSONA_N0 = OBJETO.DS_NIVEL_COMPETENCIA_PERSONA_N0;
    $scope.DS_NIVEL_COMPETENCIA_PUESTO_N1 = OBJETO.DS_NIVEL_COMPETENCIA_PUESTO_N1;
    $scope.DS_NIVEL_COMPETENCIA_PERSONA_N1 = OBJETO.DS_NIVEL_COMPETENCIA_PERSONA_N1;
    $scope.DS_NIVEL_COMPETENCIA_PUESTO_N2 = OBJETO.DS_NIVEL_COMPETENCIA_PUESTO_N2;
    $scope.DS_NIVEL_COMPETENCIA_PERSONA_N2 = OBJETO.DS_NIVEL_COMPETENCIA_PERSONA_N2;
    $scope.DS_NIVEL_COMPETENCIA_PUESTO_N3 = OBJETO.DS_NIVEL_COMPETENCIA_PUESTO_N3;
    $scope.DS_NIVEL_COMPETENCIA_PERSONA_N3 = OBJETO.DS_NIVEL_COMPETENCIA_PERSONA_N3;
    $scope.DS_NIVEL_COMPETENCIA_PUESTO_N4 = OBJETO.DS_NIVEL_COMPETENCIA_PUESTO_N4;
    $scope.DS_NIVEL_COMPETENCIA_PERSONA_N4 = OBJETO.DS_NIVEL_COMPETENCIA_PERSONA_N4;
    $scope.DS_NIVEL_COMPETENCIA_PUESTO_N5 = OBJETO.DS_NIVEL_COMPETENCIA_PUESTO_N5;
    $scope.DS_NIVEL_COMPETENCIA_PERSONA_N5 = OBJETO.DS_NIVEL_COMPETENCIA_PERSONA_N5;

    

    //se generan las traducciones de las etiquetas, placeholder (ayuda en los componentes) 
    $scope.lbl_CL_CLASIFICACION = C_COMPETENCIA.CL_CLASIFICACION;
    $scope.ph_CL_CLASIFICACION = C_COMPETENCIA.CL_CLASIFICACION_ph;
    $scope.lbl_CL_COMPETENCIA = C_COMPETENCIA.CL_COMPETENCIA;
    $scope.ph_CL_COMPETENCIA = C_COMPETENCIA.CL_COMPETENCIA_ph;
    $scope.lbl_CL_TIPO_COMPETENCIA = C_COMPETENCIA.CL_TIPO_COMPETENCIA;
    $scope.ph_CL_TIPO_COMPETENCIA = C_COMPETENCIA.CL_TIPO_COMPETENCIA_ph;
    $scope.lbl_DS_COMPETENCIA = C_COMPETENCIA.DS_COMPETENCIA;
    $scope.ph_DS_COMPETENCIA = C_COMPETENCIA.DS_COMPETENCIA_ph;
    $scope.lbl_FG_ACTIVO = C_COMPETENCIA.FG_ACTIVO;
    $scope.ph_FG_ACTIVO = C_COMPETENCIA.FG_ACTIVO_ph;
    $scope.lbl_NB_COMPETENCIA = C_COMPETENCIA.NB_COMPETENCIA;
    $scope.ph_NB_COMPETENCIA = C_COMPETENCIA.NB_COMPETENCIA_ph;
    $scope.lblPorPersona = C_COMPETENCIA.DS_PORPERSONA;
    $scope.lblPorPuesto = C_COMPETENCIA.DS_PORPUESTO;
    $scope.lblnivel0 = C_COMPETENCIA.DS_NIVEL0;    
    $scope.lblnivel1 = C_COMPETENCIA.DS_NIVEL1;    
    $scope.lblnivel2 = C_COMPETENCIA.DS_NIVEL2;
    $scope.lblnivel3 = C_COMPETENCIA.DS_NIVEL3;
    $scope.lblnivel4 = C_COMPETENCIA.DS_NIVEL4;
    $scope.lblnivel5 = C_COMPETENCIA.DS_NIVEL5;
 
    $scope.ph_puesto_nivel0 = '';
    $scope.ph_puesto_nivel1 = '';
    $scope.ph_puesto_nivel2 = '';
    $scope.ph_puesto_nivel3 = '';
    $scope.ph_puesto_nivel4 = '';
    $scope.ph_puesto_nivel5 = '';
    $scope.ph_persona_nivel0 = '';
    $scope.ph_persona_nivel1 = '';
    $scope.ph_persona_nivel2 = '';
    $scope.ph_persona_nivel3 = '';
    $scope.ph_persona_nivel4 = '';
    $scope.ph_persona_nivel5 = '';
    



    $scope.ph_NB_COMPETENCIA = C_COMPETENCIA.NB_COMPETENCIA_ph;

    //se generan las traducciones Genericas 
    $scope.titulo = C_COMPETENCIA.TITULO_PANTALLA;
    $scope.msjRequerido = Config.MSJREQUERIDO;
    $scope.btnSave = Config.BOTONGUARDAR;
    $scope.btnCancel = Config.BOTONCANCELAR;
    $scope.modulo = tema.cssClass;

    $scope.CL_COMPETENCIA_disable = false;   
    if (($scope.CL_COMPETENCIA != undefined))
        if ($scope.CL_COMPETENCIA != "")
            $scope.CL_COMPETENCIA_disable = true;
      

    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente  
        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
        if ($scope.ID_COMPETENCIA != undefined)  //se verifica que la clave primaria sea diferente de nulo  
            tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  
        var Obj_Competencia = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_CLASIFICACION: $scope.CL_CLASIFICACION
            , ID_COMPETENCIA : $scope.ID_COMPETENCIA
 			, CL_COMPETENCIA: $scope.CL_COMPETENCIA
 			, CL_TIPO_COMPETENCIA: $scope.CL_TIPO_COMPETENCIA
 			, DS_COMPETENCIA: $scope.DS_COMPETENCIA
 			, FG_ACTIVO: $scope.FG_ACTIVO
 			, NB_COMPETENCIA: $scope.NB_COMPETENCIA
            , DS_NIVEL_COMPETENCIA_PUESTO_N0: $scope.DS_NIVEL_COMPETENCIA_PUESTO_N0
            , DS_NIVEL_COMPETENCIA_PERSONA_N0: $scope.DS_NIVEL_COMPETENCIA_PERSONA_N0
            , DS_NIVEL_COMPETENCIA_PUESTO_N1: $scope.DS_NIVEL_COMPETENCIA_PUESTO_N1
            , DS_NIVEL_COMPETENCIA_PERSONA_N1: $scope.DS_NIVEL_COMPETENCIA_PERSONA_N1
            , DS_NIVEL_COMPETENCIA_PUESTO_N2: $scope.DS_NIVEL_COMPETENCIA_PUESTO_N2
            , DS_NIVEL_COMPETENCIA_PERSONA_N2: $scope.DS_NIVEL_COMPETENCIA_PERSONA_N2
            , DS_NIVEL_COMPETENCIA_PUESTO_N3: $scope.DS_NIVEL_COMPETENCIA_PUESTO_N3
            , DS_NIVEL_COMPETENCIA_PERSONA_N3: $scope.DS_NIVEL_COMPETENCIA_PERSONA_N3
            , DS_NIVEL_COMPETENCIA_PUESTO_N4: $scope.DS_NIVEL_COMPETENCIA_PUESTO_N4
            , DS_NIVEL_COMPETENCIA_PERSONA_N4: $scope.DS_NIVEL_COMPETENCIA_PERSONA_N4
            , DS_NIVEL_COMPETENCIA_PUESTO_N5: $scope.DS_NIVEL_COMPETENCIA_PUESTO_N5
            , DS_NIVEL_COMPETENCIA_PERSONA_N5: $scope.DS_NIVEL_COMPETENCIA_PERSONA_N5


            
        };
        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_COMPETENCIA",
            method: "POST",
            data: {
                V_C_COMPETENCIA: Obj_Competencia,
                usuario: TraerUsuario(),
                //ojo modificar   
                programa: "InsertaCompetencia.html",
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
 		            title: C_COMPETENCIA.TITULO_PANTALLA,
 		            message: Config.ERRORGUARDAR,
 		            size: 'size-small',
 		            type: tema.cssClass
 		        });
 		    } else {
 		        if ($scope.ID_COMPETENCIA == undefined) {
 		            LimpiarFormulario("newForm");
 		        }
 		        BootstrapDialog.show({
 		            title: C_COMPETENCIA.TITULO_PANTALLA,
 		            message: Config.MENSAJEGUARDAR,
 		            size: 'size-small',
 		            type: tema.cssClass
 		        });
 		        $scope.focusElement = "txt_CL_COMPETENCIA";
 		        $("#panelcontenedor").data("kendoPanelBar").collapse($("li", "#panelbar2"));
 		        $("#panelcontenedor").data("kendoPanelBar").collapse($("li", "#panelbar1"));
 		    }
 		},
 		function (response) {
 		    BootstrapDialog.show({
 		        title: C_COMPETENCIA.TITULO_PANTALLA,
 		        message: Config.ERRORGENERICO,
 		        size: 'size-small',
 		        type: tema.cssClass
 		    });
 		});
    };
    $scope.cancel = function () {
        $("#Grid_Competencia").data("kendoGrid").dataSource.read();
        $modalInstance.dismiss('cancel');
    };

    //document.getElementById("txt_CL_TIPO_COMPETENCIA").value = OBJETO.CL_TIPO_COMPETENCIA;
    //document.getElementById("txt_CL_CLASIFICACION").value = OBJETO.CL_CLASIFICACION;
  
   
});


