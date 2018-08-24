//CONTROLADOR PARA M_DEPARTAMENTO
app.controller('M_DEPARTAMENTOController', function ($scope, $http, $modal,$rootScope) {

   

    $scope.animationsEnabled = true;

    $scope.btnAgregar = Config.BOTONAGREGAR;
    $scope.btnModificar = Config.BOTONMODIFICAR;
    $scope.btnEliminar = Config.BOTONELIMINAR;
    $scope.titulo = M_DEPARTAMENTO.TITULODEPTO;

    $scope.BOTONEXPORTAREXCEL = GridKendo.BOTONEXPORTAREXCEL
    $scope.CargarGrid_Departamento = function () {

        NProgress.start();

        $(document).ready(function () {
            $("#Grid_Departamento").kendoGrid({
                allowCopy: true//permite copiar los datos para copiar al portapapeles   
                , allowCopy: {
                    delimeter: "," //delimitador cuando se copien los datos del grid   
                }
                , resizable: true //sirve para mover las columnas
               , excel: {
                   fileName: ".xlsx" //nombre del archivo a exportar   
                       , allPages: true // indica si se exportan todas las paginas por default esta en false   
                   //,proxyURL: "//demos.telerik.com/kendo-ui/service/export",   
                       , filterable: true //permite que la exportacion sea por lo que se esta filtrando   
               }
              //  , toolbar: kendo.template($scope.templateGrid_())
                , dataSource: {
                    type: "JSON", // JSON, JSONP, OData or XML).   
                    transport: {
                        read: function (options) {
                            $.ajax({
                                type: "POST"
                                , url: "ws/OperacionesGral.svc/Get_M_DEPARTAMENTO"
                                , dataType: "json"
                                , success: function (result) {
                                    options.success(result);
                                    NProgress.done();
                                }
                            });
                        }
                    }
                    ,schema: {
                        model: {
                            fields: {
                                CL_DEPARTAMENTO: { type: "string" },
                                NB_DEPARTAMENTO: { type: "string" },
                                FG_ACTIVO: { type: "boolean" }
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
                //groupable  si solo esta el true se pone, si solo se especifica el titulo se pone, si le pone el groupable: false  pero deja el mensaje este sigue apareciendo   
               , groupable: {
                   showFooter: true //si muestra el pie del grid cuando esten agrupados   
                   , messages: {
                       empty: GridKendo.GRUPABLEEMPTY //mensaje cuando se muestra la agrupacion de las columnas en el grid   
                   }
               }
                , groupable: false // agrupacion por columnas en la parte del grid				   
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
                       , field: "CL_DEPARTAMENTO"
                       , align: "center"  //alineacion del titulo de la columna 
                       , title: Depto.CLAVEDEPTO // titulo de la columna 
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
                            ,
                            }
                       }
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "NB_DEPARTAMENTO"
                       , align: "center"  //alineacion del titulo de la columna 
                       , title: Depto.NOMBREDEPTO // titulo de la columna 
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
                            ,
                            }
                       }
               }, {
                   headerAttributes: { style: "text-align: center; " }
                       , field: "FG_ACTIVO"
                       , align: "center"  //alineacion del titulo de la columna 
                       , title: Depto.ACTIVODEPTO // titulo de la columna 
                       , width: 99 // ancho de la columna 
                       , hidden: false // indica si se esconde o no la columna 
                       , locked: false // indica si estara congelada la columna 
                       , template: "<div style=\"text-align:center\"><label><input type=\"checkbox\" #= FG_ACTIVO ? \"checked='checked'\" : \"\" # disabled=\"disabled\" name=\"i_FG_ACTIVO\" /></label></div>"
                   //, filterable: { extra: false }
                       , filterable: {
                           mode: "row",
                           cell: {
                               showOperators: false,
                               template: function (args) {
                                   args.element.kendoDropDownList({
                                       autoBind:false,
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

               }],
                change: function (e) {
                    var selectedRows = this.select();
                    var dataItem = this.dataItem(selectedRows[0]);
                    $scope.entidadDepartamento = dataItem;
                }
            });
        });

    };

    $scope.Add_Grid_Departamento = function () {

        var ObjDepartamento = {
            CL_DEPARTAMENTO: '',
            FE_INACTIVO: '',
            FG_ACTIVO: '',
            ID_DEPARTAMENTO: undefined,
            NB_DEPARTAMENTO: '',
            XML_CAMPOS_ADICIONALES: ''
        };

        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'opDepartamento.html',
            controller: 'ModalDepartamento',
            resolve: {
                OBJETO: function () { return ObjDepartamento; }
            }
        });
    };

    $scope.Edit_Grid_Departamento = function () {

        if ($scope.entidadDepartamento == undefined) {
            BootstrapDialog.show({
                title: Depto.TITULODEPTO,
                message: Config.MENSAJEREGISTRO,
                size: 'size-small',
                type: tema.cssClass
            });
            return false;
        }

        var datos = $scope.entidadDepartamento;

        var ObjDepartamento = {
            CL_DEPARTAMENTO: datos.CL_DEPARTAMENTO,
            FE_INACTIVO: datos.FE_INACTIVO,
            FG_ACTIVO: datos.FG_ACTIVO,
            ID_DEPARTAMENTO: datos.ID_DEPARTAMENTO,
            NB_DEPARTAMENTO: datos.NB_DEPARTAMENTO,
            XML_CAMPOS_ADICIONALES: ''
        };

        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'opDepartamento.html',
            controller: 'ModalDepartamento',
            resolve: {
                OBJETO: function () { return ObjDepartamento; }
            }
        });
    };

    $scope.Del_Grid_Departamento = function () {

        if ($scope.entidadDepartamento == undefined) {
            BootstrapDialog.show({
                title: Depto.TITULODEPTO,
                message: Config.MENSAJEREGISTRO,
                size: 'size-small',
                type: tema.cssClass
            });
            return false;
        }

        BootstrapDialog.show({
            message: Config.MENSAJECONFIRMADEL,
            id: 'modalDelete',
            size: 'size-small',
            type: tema.cssClass,
            buttons: [{
                label: Config.BOTONELIMINAR,
                action: function (result) {
                    if (result) {
                        $scope.EliminarDepto();
                    }
                }
            }, {
                label: Config.BOTONCANCELAR,
                action: function (dialogItself) {
                    dialogItself.close();
                }
            }]
        });
    };

    $scope.EliminarDepto = function () {
        var datos = $scope.entidadDepartamento;

        $http({
            url: "ws/OperacionesGral.svc/Delete_M_DEPARTAMENTO",
            method: "POST",
            data: {
                departamento: datos.ID_DEPARTAMENTO,
                CL_USUARIO: TraerUsuario(),
                NB_PROGRAMA: "CatalogoDepartamento.html"
            },
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(function (response) {
            console.log(response);
            console.log(response.data);
            if (response.data != true) {
                BootstrapDialog.show({
                    title: Depto.TITULODEPTO,
                    message: Config.ERRORELIMINAR,
                    size: 'size-small',
                    type: tema.cssClass
                });
            } else {
                $("#Grid_Departamento").data("kendoGrid").dataSource.read();
                $("#modalDelete").modal('toggle');
                BootstrapDialog.show({
                    title: Depto.TITULODEPTO,
                    message: Config.MENSAJEELIMINAR,
                    size: 'size-small',
                    type: tema.cssClass,
                });
            }
        },
        function (response) {
            BootstrapDialog.show({
                title: Config.MENSAJEERROR,
                message: Config.ERRORGENERICO,
                size: 'size-small',
                type: tema.cssClass,
            });
        });

    };

    $scope.CargarGrid_Departamento();
    $rootScope.configuracion = { titulo: $scope.titulo, grid: "#Grid_Departamento" };
});

//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR DEPARTAMENTO
app.controller('ModalDepartamento', function ($scope, $modalInstance, $http, OBJETO) {

    $scope.CL_DEPARTAMENTO = OBJETO.CL_DEPARTAMENTO;
    $scope.NB_DEPARTAMENTO = OBJETO.NB_DEPARTAMENTO;
    $scope.FG_ACTIVO = Boolean(OBJETO.FG_ACTIVO);
    $scope.ID_DEPARTAMENTO = OBJETO.ID_DEPARTAMENTO;
     
    $scope.titulo = Depto.TITULODEPTO;
    $scope.lblClave = Depto.CLAVEDEPTO;
    $scope.lblName = Depto.NOMBREDEPTO;
    $scope.lblActivo = Depto.ACTIVODEPTO;

    $scope.plClave = Config.MSJCLAVE;
    $scope.plName = Config.MSJNOMBRE;

    $scope.btnSave = Config.BOTONGUARDAR;
    $scope.btnCancel = Config.BOTONCANCELAR;
    $scope.modulo = tema.cssClass;

    $scope.CL_DEPARTAMENTO_disable = false;
    if (($scope.CL_DEPARTAMENTO != undefined))
        if ($scope.CL_DEPARTAMENTO != "")
            $scope.CL_DEPARTAMENTO_disable = true;

    $scope.ok = function () {
        var tipoOperacion = ""

        if ($scope.ID_DEPARTAMENTO != undefined) {
            tipoOperacion = "A"
        }
        else {
            tipoOperacion = "I"
        }

        var ObjDepartamento = {
            CL_DEPARTAMENTO: $scope.CL_DEPARTAMENTO,
            FE_INACTIVO: null,
            FG_ACTIVO: $scope.FG_ACTIVO,
            ID_DEPARTAMENTO: $scope.ID_DEPARTAMENTO,
            NB_DEPARTAMENTO: $scope.NB_DEPARTAMENTO,
            XML_CAMPOS_ADICIONALES: ''
        };

        $http({
            url: "ws/OperacionesGral.svc/Insert_M_DEPARTAMENTO",
            method: "POST",
            data: {
                departamento: ObjDepartamento,
                CL_USUARIO: TraerUsuario(),
                NB_PROGRAMA: "InsertaDepartamento.html",
                CL_TIPO: tipoOperacion
            },
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(function (response) {
            console.log(response);
            console.log(response.data);
            if (response.data != true) {
                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: Config.ERRORGUARDAR,
                    size: 'size-small',
                    type: tema.cssClass
                });

            } else {
                
                if ($scope.ID_DEPARTAMENTO == undefined) {
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
        $("#Grid_Departamento").data("kendoGrid").dataSource.read();
        $modalInstance.dismiss('cancel');
    };


});