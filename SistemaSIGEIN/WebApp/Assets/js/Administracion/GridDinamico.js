﻿
app.controller('gridDinamicoController', function ($scope, $http, $modal, $rootScope) {

    $scope.getdata = function () {
        //     console.info($rootScope.atributos);
    }

    $scope.getdata();

    $rootScope.data = { array: [] };
    //habilita la animacion del popup para que se despliegue de arriba para abajo  
    $scope.animationsEnabled = true;
    //se instancia el objeto que contiene el titulo en español y ingles de las columnas  
    //se le asignan los nombres a los botones y el titulo a nuestro asp   
    $scope.titulo = $rootScope.atributos.titulo;

    $scope.Style_btn = $rootScope.atributos.btn_adicional;

    //$scope.btnActivarDesactivar = Config.BOTONACTIVARDESACTIVAR;   
    $scope.btnAgregar = Config.BOTONAGREGAR;
    $scope.btnModificar = Config.BOTONMODIFICAR;
    $scope.btnEliminar = Config.BOTONELIMINAR;
    $scope.btnexportar = "Exportar";
    $scope.btncopiar = "Copiar de..";
    ////////////////////////////////////////////////////////////// 
    //inicia evento para consultar los registros  
    //SE CARGA EL TEMPLATE DEL TOOLBAR DEL GRID DONDE IRAN LOS BOTONES DE EDICION Y EXPORTACION  
    $scope.templateGrid_Area_Interes = function () {
        return "<div class=\"toolbar\"> " +
 			"<div style=\"float:right\">  " +
 				"<div style=\"float:left\">" +
 					"<button id=\"btnExportarExcel_Grid_Area_Interes\" class=\"btn btn-link FontNegro\" onclick=\"ExportarExcel('Grid_Area_Interes')\"  > " +
 						"<i class=\"fa fa-file-o\" style=\"font-size: 17px;\"></i>  " +
 						"" + GridKendo.BOTONEXPORTAREXCEL + " " +
 					"</button> " +
 				"</div>" +
 			"	</div>  " +
 		"</div>  "
    };
    //se carga el grid  

    //termina evento para consultar los registros  
    ////////////////////////////////////////////////////////////// 


    //termina evento para agregar el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia evento para edita el registro  
    $scope.Edit_Grid = function () {
        if ($scope.entidad == undefined) { // se verifica que el registro este seleccionado  
            BootstrapDialog.show({ //se muestra el mensaje de error  
                title: $scope.c.titulo, //se trae el nombre de la pantalla  
                message: Config.MENSAJEREGISTRO, // se envia el mensaje de error que falta el registro  
                size: 'size-small', //se especifica el tamaño de la ventana del mensaje  
                type: tema.cssClass //se especifica el color en base al modulo que se tiene  
            });
            return false; //se devuelve nulo para que no sigua con la funcion  
        }
        var datos = $scope.entidad; //se asigna el registro a la variable de datos  
        //console.info(datos);
        var Objeto_Edit = datos;



        var modalInstance = $modal.open({ //se manda ejecutar en modo modal la instancia definida en el html para la edicion  
            animation: $scope.animationsEnabled, //se le asigna la variable que nos indica si tiene animacion o no  
            templateUrl: $rootScope.atributos.templateUrl, //se le indica el html que se abrira  
            controller: $rootScope.atributos.controller, //se le indica el controlador del html  
            resolve: { //se le envia el objeto  
                OBJETO: function () { return Objeto_Edit; }
            }
        });
    };

    //termina evento para edita el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia eventos para eliminar el registro  
    $scope.Del_Grid = function () {

        if ($scope.entidad == undefined) { //se verifica si la entidad que guarda el evento onchange del grid no se encuentra vacia  
            BootstrapDialog.show({ //en caso de que no se encuentre se mostrara un error  
                title: $rootScope.atributos.titulo, //se le pone el titulo especificado en la clase   
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
                        $scope.Eliminar();  //se ejecuta la funcion de elimina_?? especificada para borrar el registro  
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



    $scope.Eliminar = function () {

        var datos = $scope.entidad;  //se extrae el registro obtenido en el evento change del grid 

        //console.info(datos);
        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/" + $rootScope.atributos.deletegrid,
            method: "POST",
            data: { //se le pasan los parametros 
                V_LISTA_DINAMICA: datos, // se le pasa la llave primaria  OJO poner V_LISTA DINAMICA EN TU DELETE METODO
                usuario: TraerUsuario(), // se le pasa el usuario 
                //ojo modificar   
                programa: $rootScope.atributos.programa  //se le pasa  
            },
            headers: {  //SE ESPECIFICAN LAS CABECERAS QUE SE ENVIAN AL WEB SERVICE, ES DECIR COMO SE ENVIAN LOS DATOS: JSON, XML, TEXTO PLANO O FILE 
                'Accept': 'application/json',  //SE INDICA QUE EL EVENTO ACEPTARA EL TIPO JSON  
                'Content-Type': 'application/json' // SE INDICA QUE TIPO CONTENDRA LA INFORMACION QUE SE ENVIA   
            }
        })
        .then(function (response) { // ejecucion de WS 
            if (response.data != true) {  //si el procedimiento regresa falso 
                BootstrapDialog.show({ //se mostrara un mensaje de error  
                    title: $rootScope.atributos.titulo,  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
                    message: Config.ERRORELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo ERRORGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass //se le indica el color dependiendo del modulo en que se encuentre 
                });
            } else {

                //METODO
                $("#Grid_Dinamico").html("");
                $rootScope.Llena_Grid_Dinamico();

                $("#modalDelete").modal('toggle');
                BootstrapDialog.show({  //si el procedimiento regresa true 
                    title: $rootScope.atributos.titulo,  //el titulo del mensaje  
                    message: Config.MENSAJEELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass,  //se le indica el color dependiendo del modulo en que se encuentre 
                });
            }
        },
        function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
            BootstrapDialog.show({
                title: $rootScope.atributos.titulo,
                message: Config.ERRORGENERICO,
                size: 'size-small',
                type: tema.cssClass,
            });
        });
    };


    //termina eventos para eliminar el registro  
    ////////////////////////////////////////////////////////////// 

    //Se manda a cargar cuando se genere el controlador en el html 
    //$scope.CargarGrid_Area_Interes ();  



    //CONSULTA CATALAGO DE AREAS DE INTERESS 
    $rootScope.Llena_Grid_Dinamico = function (elemento, parametro) {

        getDatos(parametro, function (response) {
            //PROPIEDADES

            propiedadesGrid.idElemento = 'Grid_Dinamico';
            propiedadesGrid.NombreArchivo = $rootScope.atributos.id + '.xlsx';
            propiedadesGrid.Agrupable = false;
            propiedadesGrid.Entidad = $rootScope.atributos.entidad;
            //METODO
            $("#Grid_Dinamico").html("");
<<<<<<< .mine
            //var response = $rootScope.atributos.array_get;
=======
            
>>>>>>> .r297
            GenerarGrid(response, propiedadesGrid);
            console.info(response);
        });

    }

    //ENTIDAD  SOBRE LAS PROPIEDADES DEL GRID
    var propiedadesGrid =
    {
        idElemento: "",
        NombreArchivo: "",
        Agrupable: false,
        Entidad: ""
    }

    //REGRESA TODA LA INFORMACION DE AREAS DE INTERES
    var filtrar = ($rootScope.atributos.filtro != null) ? JSON.stringify($rootScope.atributos.filtro) : null;

    getDatos = function (parametro, response) {
        $.ajax({
            type: "post",
            url: "ws/OperacionesGral.svc/" + $rootScope.atributos.getgrid,
            data: filtrar,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                response(data);
            }
        });
    };

    //VARIABLE  GLOBAL
    var dateFields = [];

    //LLAMADDA DEL GRID 
    $(document).ready(function () {
        $rootScope.Llena_Grid_Dinamico();
    });

    //DATOS
    var TiposDatos = '{' +
                        '"TiposDatos":[' +
                        '{"tipo":"bigint","gridtipo":"number" },' +
                        '{"tipo":"decimal","gridtipo":"number" },' +
                        '{"tipo":"float","gridtipo":"number" },' +
                        '{"tipo":"int","gridtipo":"number" },' +
                        '{"tipo":"money","gridtipo":"number" },' +
                        '{"tipo":"numeric","gridtipo":"number" },' +
                        '{"tipo":"real","gridtipo":"number" },' +
                        '{"tipo":"smallint","gridtipo":"number" },' +
                        '{"tipo":"smallmoney","gridtipo":"number" },' +
                        '{"tipo":"tinyint","gridtipo":"number" },' +
                        '{"tipo":"float","gridtipo":"number" },' +
                        '{"tipo":"bit","gridtipo":"boolean" },' +
                        '{"tipo":"char","gridtipo":"string" },' +
                        '{"tipo":"nchar","gridtipo":"string" },' +
                        '{"tipo":"ntext","gridtipo":"string" },' +
                        '{"tipo":"nvarchar","gridtipo":"string" },' +
                        '{"tipo":"xml","gridtipo":"string" },' +
                        '{"tipo":"nvarcharMAX","gridtipo":"string" },' +
                        '{"tipo":"text","gridtipo":"string" },' +
                        '{"tipo":"varchar","gridtipo":"string" },' +
                        '{"tipo":"date","gridtipo":"date" },' +
                        '{"tipo":"datetime","gridtipo":"date" },' +
                        '{"tipo":"datetime2","gridtipo":"date" },' +
                        '{"tipo":"datetimeoffset","gridtipo":"date" },' +
                        '{"tipo":"smalldatetime","gridtipo":"date" },' +
                        '{"tipo":"time","gridtipo":"date" },' +
                        '{"tipo":"timestamp","gridtipo":"date" }' +
                        ']}';



    //GENERACION DEL GRID 
    GenerarGrid = function (gridDatos, propiedadesGrid) {
       

        // EXTRAER EL MODELO Y LAS COLUMNAS DE UN GRID
<<<<<<< .mine
        console.log(gridDatos);
        var modelo = GenerarModelo();
        var Columnas = GenerarColumnas(gridDatos[0],propiedadesGrid.Entidad);
=======
      
        var modelo = GenerarModelo();
        var Columnas = GenerarColumnas(gridDatos[0],propiedadesGrid.Entidad);
>>>>>>> .r297

        var parseFunction;
        if (dateFields.length > 0) {
            parseFunction = function (response) {
                for (var i = 0; i < response.length; i++) {
                    for (var fieldIndex = 0; fieldIndex < dateFields.length; fieldIndex++) {
                        var record = response[i];
                        record[dateFields[fieldIndex]] = kendo.parseDate(record[dateFields[fieldIndex]]);
                    }
                }
                return response;
            };
        }

        var grid = $("#" + propiedadesGrid.idElemento).kendoGrid({
            dataSource: {
                data: gridDatos,
                schema: {
                    model: modelo
                }
            },
            sortable: true,
            reorderable: true,
            allowCopy: true,
            allowCopy: {
                delimeter: ","
            },
            resizable: true,
            excel: {
                fileName: propiedadesGrid.NombreArchivo,
                allPages: true,
                filterable: true
            },
            filterable: {
                mode: "row",
                extra: false,
                filterable: true,
                messages: {
                    and: GridKendo.AND,
                    cancel: GridKendo.CANCEL,
                    checkAll: GridKendo.CHECKALL,
                    clear: GridKendo.CLEAR,
                    IsEqualTo: GridKendo.ISEQUALTO,
                    filter: GridKendo.FILTER,
                    info: GridKendo.INFO,
                    isFalse: GridKendo.ISFALSE,
                    isTrue: GridKendo.ISTRUE,
                    operator: GridKendo.OPERATOR,
                    or: GridKendo.OR,
                    SelectValue: GridKendo.SELECTVALUE,
                    value: GridKendo.VALUE,
                    contains: GridKendo.CONTAINS,
                    doesnotcontain: GridKendo.DOESNOTCONTAIN
                },
                operators: {
                    string: {
                        eq: GridKendo.EQ,
                        neq: GridKendo.NEQ,
                        startswith: GridKendo.STARTSWITH,
                        contains: GridKendo.CONTAINS,
                        doesnotcontain: GridKendo.DOESNOTCONTAIN,
                        endswith: GridKendo.ENDSWITH
                    },
                    number: {
                        eq: GridKendo.EQ,
                        neq: GridKendo.NEQ,
                        gte: Config.FILTROCONDICIONESFILTRARMAYORIGUALQUE,
                        gt: Config.FILTROCONDICIONESFILTROMAYORQUE,
                        lte: Config.FILTROCONDICIONESFILTROMENORQUEOIGUALQUE,
                        lt: Config.FILTROCONDICIONESFILTRARMENORQUE
                    },
                    date: {
                        eq: GridKendo.EQ,
                        neq: GridKendo.NEQ,
                        gte: Config.FILTROCONDICIONESFILTRODESPUESOIGUALQUE,
                        gt: Config.FILTROCONDICIONESFILTRODESPUES,
                        lte: Config.FILTROCONDICIONESFILTROESANTESOIGUALQUE,
                        lt: Config.FILTROCONDICIONESFILTRODESPUES
                    },
                    enums: {
                        eq: GridKendo.EQ,
                        neq: GridKendo.NEQ
                    }
                }
            },
            columnMenu: true,
            columnMenu: {
                messages: {
                    columns: GridKendo.COLUMNS,
                    done: GridKendo.DONE,
                    filter: GridKendo.FILTER,
                    lock: GridKendo.LOCK,
                    settings: GridKendo.SETTINGS,
                    sortAscending: GridKendo.SORTASCENDING,
                    sortDescending: GridKendo.SORTDESCENDING,
                    unlock: GridKendo.UNLOCK
                }
            },
            scrollable: true,
            scrollable: { virtual: true },
            selectable: "row",
            sortable: true,
            navigatable: true,
            sortable: {
                mode: "multiple"
            },
            messages: {
                noRecords: GridKendo.NORECORDS
            },
            groupable: {
                showFooter: true,
                messages: {
                    empty: GridKendo.GRUPABLEEMPTY
                }
            },
            groupable: false,
            pageable: {
                pageSize: 10,
                refresh: true,
                pageSizes: true,
                info: true,
                input: false,
                numeric: true,
                previousNext: true,
                buttonCount: 5,
                messages: {
                    display: GridKendo.PAGEABLEDISPLAY,
                    empty: GridKendo.PAGEABLEEMPTY,
                    page: GridKendo.PAGEABLEPAGE,
                    of: GridKendo.PAGEABLEOF,
                    itemsPerPage: GridKendo.PAGEABLEITEMSPERPAGE,
                    morePages: GridKendo.PAGEABLEMOREPAGE,
                    first: GridKendo.PAGEABLEFIRST,
                    previous: GridKendo.PAGEABLEPREVIOUS,
                    next: GridKendo.PAGEABLENEXT,
                    last: GridKendo.PAGEABLELAST,
                    refresh: GridKendo.PAGEABLEREFRESH,
                }
            },
            columns: Columnas,
            groupable: propiedadesGrid.Agrupable,
            change: function (e) {
                var selectedRows = this.select();
                var dataItem = this.dataItem(selectedRows[0]);
                $rootScope.entidad = dataItem;
            }
        });

        $rootScope.kendo = { data: gridDatos };

    }

    //GENERAR EL MODELO A PARTIR DEL GRID
    GenerarModelo = function () {
        var model = {};        
        var fields = {};

        var info = $rootScope.atributos.array_get;
        for (var i = 0; i < info.length; i++) {

<<<<<<< .mine
            var c = info[i].nb_campo;
            var t = info[i].tipo.toLowerCase();
            var tipogrid=  TraerTipoDatoGrid(t);
=======
            var c = info[i].nb_campo;
            var t = info[i].tipo;
            var tipogrid=  TraerTipoDatoGrid(t);
>>>>>>> .r297

            //ASIGNACION AL GRID
            if (t=="date") {
                fields[info[i].nb_campo] = {
                    type: tipogrid,
                    template: "#= kendo.toString(kendo.parseDate(" + info[i].nb_campo + ", 'yyyy-MM-dd'), 'MM/dd/yyyy') #"
                };
<<<<<<< .mine
               
            } else {
                fields[info[i].nb_campo] = {
                type: tipogrid
=======
               
            } else {
                fields[info[i].nb_campo] = {
                    type: tipogrid
>>>>>>> .r297
                };
            }
            

        }

        model.fields = fields;
        return model;

    }

    //TRAER EL TIPO DE DATO KENDO GRID DESDE LA DEFINICION DEL MODELO BD
    TraerTipoDatoGrid = function (tipo)
    {
        var Tipos = JSON.parse(TiposDatos);
        var t = "";
        var gridtipo = "";

        for (var i = 0; i < Tipos.TiposDatos.length; i++) {
            var t = Tipos.TiposDatos[i].tipo;
            if (t == tipo) {
                gridtipo = Tipos.TiposDatos[i].gridtipo;
                break;
            }
        }

        return gridtipo;
    }

    //GENERA EL NOMBRE LAS COLUMNAS 
    GenerarColumnas = function (gridDatos,entidad) {
        var columnas = [];

<<<<<<< .mine
        var info = $rootScope.atributos.array_get;
        for (var i = 0; i < info.length; i++) {
=======
        var info = $rootScope.atributos.array_get;
       /// console.info(info);
        for (var i = 0; i < info.length; i++) {
>>>>>>> .r297

<<<<<<< .mine
            var property = info[i].nb_campo;
            var t = info[i].tipo.toLowerCase();
            var tipogrid = TraerTipoDatoGrid(t);
            
            var _format = "";
            if (tipogrid=="date") {
                _format = "{0:MM-dd-yyyy}";
            } else {
                _format = "";
            }

=======
            var property = info[i].nb_campo;
            var t = info[i].tipo;
            var tipogrid = TraerTipoDatoGrid(t);
            
            var _format = "";
            if (tipogrid=="date") {
                _format = "{0:MM-dd-yyyy}";
            } else {
                _format = "";
            }

>>>>>>> .r297
            //AGREGA EL NONBRE DE LAS COLUMNAS
            //console.log(info[i].nb_campo);
            if ($rootScope.atributos.campos.indexOf(property) > -1) { //VALIDA LOS CAMPOS QUE SE VAN A MOSTRAR POR DEFAULT
                //PENDIENTE VALORES PARA FILTRO AVANZADO
                //alert(property);
                var index = $rootScope.atributos.campos.indexOf(property);
                var lista = { campo: property, tipo: tipogrid, title: Remplazartextos(property, $rootScope.atributos.entidad), hide: true,format:_format };
                $rootScope.data.array.push(lista);

                columnas.splice(index, 0, {
                    headerAttributes: { style: "text-align: center; " },
                    field: property,
                    title: Remplazartextos(property, entidad),
                    reorderable: true,
                    filterable: {
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
                });
            }
            else {
                var lista = { campo: property, tipo: tipogrid, title: Remplazartextos(property, $rootScope.atributos.entidad), hide: true, format: _format };
                $rootScope.data.array.push(lista);
            }
        }
        return columnas;
    }

    //RENOMBRAR LOS KEYS DE OBJECTO
    RenombrarKeyofObject = function (obj, oldName, newName) {
        if (!obj.hasOwnProperty(oldName)) {
            return false;
        }

        obj[newName] = obj[oldName];
        delete obj[oldName];
        return true;
    }

    //REMPLAZA LOS TEXTOS INGLES- ESPAÑOL
    Remplazartextos = function (property, entidad) {
        var obj = eval("(" + entidad + ")");
        Object.getOwnPropertyNames(obj).forEach(function (val, idx, array) {
            if (val == property) {
                property = obj[val];
            }
        });

        return property;
    }

    //CONTADOR DE LAS PRROPIEDADES DE UN OBJECTO
    Object.size = function (obj) {
        var size = 0, key;
        for (key in obj) {
            if (obj.hasOwnProperty(key)) size++;
        }
        return size;
    };

    ParseoFecha = function (d, fecha) {
        var Fecha = new Date();
        var sFecha = fecha || (Fecha.getDate() + "/" + (Fecha.getMonth() + 1) + "/" + Fecha.getFullYear());
        var sep = sFecha.indexOf('/') != -1 ? '/' : '-';
        var aFecha = sFecha.split(sep);
        var fecha = aFecha[1] + '/' + aFecha[2] + '/' + aFecha[0];
        fecha = new Date(fecha);
        fecha.setDate(fecha.getDate() + parseInt(d));
        var anno = fecha.getFullYear();
        var mes = fecha.getMonth() + 1;
        var dia = fecha.getDate();
        mes = (mes < 10) ? ("0" + mes) : mes;
        dia = (dia < 10) ? ("0" + dia) : dia;
        var fechaFinal = dia + sep.replace("-", "/") + mes + sep.replace("-", "/") + anno;
        return (fechaFinal);
    }

});



//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR C_AREA_INTERES 
app.controller('Modal_Area_Interes_dinamico', function ($scope, $modalInstance, $http, OBJETO, $rootScope) {



    /*
        $scope.modulo = tema.cssClass;
        $scope.ID_AREA_INTERES = OBJETO.ID_AREA_INTERES //parseFloat(OBJETO.ID_AREA_INTERES);  
        $scope.CL_AREA_INTERES = OBJETO.CL_AREA_INTERES;  
        $scope.NB_AREA_INTERES = OBJETO.NB_AREA_INTERES;
        $scope.FG_ACTIVO = Boolean(OBJETO.FG_ACTIVO);
      
        //se generan las traducciones de las etiquetas, placeholder (ayuda en los componentes) 
        $scope.lbl_ID_AREA_INTERES = C_AREA_INTERES.ID_AREA_INTERES;  
        $scope.ph_ID_AREA_INTERES = C_AREA_INTERES.ID_AREA_INTERES_ph;  
        $scope.lbl_CL_AREA_INTERES = C_AREA_INTERES.CL_AREA_INTERES;  
        $scope.ph_CL_AREA_INTERES = C_AREA_INTERES.CL_AREA_INTERES_ph;  
        $scope.lbl_NB_AREA_INTERES = C_AREA_INTERES.NB_AREA_INTERES;  
        $scope.ph_NB_AREA_INTERES = C_AREA_INTERES.NB_AREA_INTERES_ph;
        $scope.lblActivo = C_AREA_INTERES.FG_ACTIVO;
      
        //se generan las traducciones Genericas 
        $scope.titulo = C_AREA_INTERES.TITULO_PANTALLA;
        $scope.msjRequerido = Config.MSJREQUERIDO; 
        $scope.btnSave = Config.BOTONGUARDAR; 
        $scope.btnCancel = Config.BOTONCANCELAR;
    
     
    
       $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente  
           $scope.ObtieneDatos();       
       };
    
       $scope.cancel = function () {  
            $("#Grid_Area_Interes").data("kendoGrid").dataSource.read();  
            $modalInstance.dismiss('cancel');  
        };
        */

});

