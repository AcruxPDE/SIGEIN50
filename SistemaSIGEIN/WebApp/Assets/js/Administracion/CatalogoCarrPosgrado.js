

//poner en (Nombre igual a Nuestro HTML).js 
app.controller('C_CARRERA_POSGRADOController', function ($scope, $http, $modal, $rootScope) {

   
    /////////////////// OBTIENE EL ID DE POSTGRADO DE C_NIVEL_ESCOLARIDAD PARA DESPUES USARSE EN ESTE CATALAGO
    $scope.ObtieneDatos_C_NIVEL_ESCOLARIDAD = function () {
        $scope.C_NIVEL_ESCOLARIDAD( "POSGRADO", function (Objeto)
        {
            $scope.ID_NIV_ESC = Objeto[0].ID_NIVEL_ESCOLARIDAD;
            //console.info($scope.ID_NIVEL_ESCOLARIDAD);
        }
        );
    }
    $scope.C_NIVEL_ESCOLARIDAD = function (clave, response) {
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_C_NIVEL_ESCOLARIDAD",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ CL_NIVEL_ESCOLARIDAD: clave }),
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });
    }
    $scope.ObtieneDatos_C_NIVEL_ESCOLARIDAD();
   
    //console.info($scope.ID_NIV_ESC);
    
    //habilita la animacion del popup para que se despliegue de arriba para abajo  
    $scope.animationsEnabled = true;  
    //se instancia el objeto que contiene el titulo en español y ingles de las columnas  
  
  
    //se le asignan los nombres a los botones y el titulo a nuestro asp   
    $scope.titulo = C_CARRERA_POSGRADO.TITULO_PANTALLA;
    //$scope.btnActivarDesactivar = Config.BOTONACTIVARDESACTIVAR;   
    $scope.btnAgregar = Config.BOTONAGREGAR;   
    $scope.btnModificar = Config.BOTONMODIFICAR;   
    $scope.btnEliminar = Config.BOTONELIMINAR;
    $scope.btnxml = "XML";


    var campos_mostrar = { "array": ["CL_NIVEL_ESCOLARIDAD", "DS_ESCOLARIDAD","NB_ESCOLARIDAD"] };
    var filtros = { ID_NIVEL_ESCOLARIDAD: $scope.ID_NIV_ESC };

    var INSERT_UPDATE = {
        CL_NIVEL_ESCOLARIDAD: ''
        , DS_ESCOLARIDAD: ''
        , ID_ESCOLARIDAD: undefined
        , NB_ESCOLARIDAD: ''
        , ID_NIVEL_ESCOLARIDAD: $scope.ID_NIV_ESC
        , FG_ACTIVO: ''
    };

    $rootScope.atributos =
        {
            id: 'Grid_CatalogoEscolaridad_Posgrado',
            titulo: $scope.titulo,
            grid: "#Grid_CatalogoEscolaridad_Posgrado",
            getgrid: "Get_C_ESCOLARIDAD",
            deletegrid: "Delete_C_ESCOLARIDAD",
            entidad: 'C_CARRERA_POSGRADO',
            templateUrl: 'op_Escolaridad_Pos.html',
            controller: 'Modal_CatalogoEscolaridad_P',
            programa: 'CatalogoCatalogoEscolaridad.html',
            campos: campos_mostrar.array,
            filtro: filtros,
            ObjInsUpd: INSERT_UPDATE
             , btn_adicional: 'hidden'
        };



    $rootScope.configuracion =
        {
            titulo: $scope.titulo,
            gridd: "#Grid_CatalogoEscolaridad_Posgrado",
            campos: campos_mostrar.array,
            xml: "<root>" +
                   "<row1>" +
                    "<value ID=\"C_ESCOLARIDAD\"></value>" + //DECLARAR TABLAS PARA GENERAR LA BUSQUEDA AVANZADA Y PERSONALIZAR DEL GRID DINAMICO
                    //"<value ID=\"C_ESCOLARIDAD\"></value>" 
                   "</row1>" +
                   "</root>"
        };
});  
  
  
//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR C_ESCOLARIDAD 
app.controller('Modal_CatalogoEscolaridad_P', function ($scope, $modalInstance, $http, OBJETO, $rootScope) {
   
    $scope.modulo = tema.cssClass;
    //console.info($scope.modulo);
    $scope.CL_NIVEL_ESCOLARIDAD = OBJETO.CL_NIVEL_ESCOLARIDAD;  
    $scope.DS_ESCOLARIDAD = OBJETO.DS_ESCOLARIDAD;  
    $scope.ID_ESCOLARIDAD = OBJETO.ID_ESCOLARIDAD //parseFloat(OBJETO.ID_ESCOLARIDAD);  
    $scope.NB_ESCOLARIDAD = OBJETO.NB_ESCOLARIDAD;
    $scope.FG_ACTIVO = Boolean(OBJETO.FG_ACTIVO);
    $scope.ID_NIVEL_ESCOLARIDAD = OBJETO.ID_NIVEL_ESCOLARIDAD
  
    //se generan las traducciones de las etiquetas, placeholder (ayuda en los componentes) 
    $scope.lbl_CL_NIVEL_ESCOLARIDAD = C_CARRERA_PROFESIONAL.CL_NIVEL_ESCOLARIDAD;
    $scope.ph_CL_NIVEL_ESCOLARIDAD = C_CARRERA_PROFESIONAL.CL_NIVEL_ESCOLARIDAD_ph;
    $scope.lbl_DS_ESCOLARIDAD = C_CARRERA_PROFESIONAL.DS_ESCOLARIDAD;
    $scope.ph_DS_ESCOLARIDAD = C_CARRERA_PROFESIONAL.DS_ESCOLARIDAD_ph;
    $scope.lbl_ID_ESCOLARIDAD = C_CARRERA_PROFESIONAL.ID_ESCOLARIDAD;
    $scope.ph_ID_ESCOLARIDAD = C_CARRERA_PROFESIONAL.ID_ESCOLARIDAD_ph;
    $scope.lbl_NB_ESCOLARIDAD = C_CARRERA_PROFESIONAL.NB_ESCOLARIDAD;
    $scope.ph_NB_ESCOLARIDAD = C_CARRERA_PROFESIONAL.NB_ESCOLARIDAD_ph;
    $scope.lblActivo = C_CARRERA_PROFESIONAL.FG_ACTIVO;
  
    //se generan las traducciones Genericas 
    $scope.titulo = C_CARRERA_POSGRADO.TITULO_PANTALLA; 
    $scope.msjRequerido = Config.MSJREQUERIDO; 
    $scope.btnSave = Config.BOTONGUARDAR; 
    $scope.btnCancel = Config.BOTONCANCELAR; 
 
  
    //////////////////////////validaciones//////////////7


    $scope.Validaciones = function (x) {
        $scope.Filtro_C_NIVEL_ESCOLARIDAD(x, function (Objeto) {


            //alert($scope.NB_ESCOLARIDAD + "," + OBJETO.NB_ESCOLARIDAD);
          
            if (Objeto.length != 0 && x != undefined) {
             
                var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
                if ($scope.ID_ESCOLARIDAD != undefined) {  //se verifica que la clave primaria sea diferente de nulo  
                    tipoOperacion = "A"
                }//si se tiene la clave primaria se hara una modificacion en vez de insercion
                var Obj_Escolaridad = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
                    ID_ESCOLARIDAD: $scope.ID_ESCOLARIDAD,
                    ID_NIVEL_ESCOLARIDAD: Objeto[0].ID_NIVEL_ESCOLARIDAD
                    , CL_NIVEL_ESCOLARIDAD: $scope.CL_NIVEL_ESCOLARIDAD
                    , NB_ESCOLARIDAD: $scope.NB_ESCOLARIDAD
                    , DS_ESCOLARIDAD: $scope.DS_ESCOLARIDAD
                    , FG_ACTIVO: $scope.FG_ACTIVO
                    , DS_FILTRO: ""
                };
                $http({
                    url: "ws/OperacionesGral.svc/Insert_update_C_ESCOLARIDAD",
                    method: "POST",
                    data: {
                        V_C_ESCOLARIDAD: Obj_Escolaridad,
                        usuario: TraerUsuario(),
                        programa: "InsertaCatalogoCarrPosgrado.html",
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

                        $rootScope.Llena_Grid_Dinamico();
                        if ($scope.ID_ESCOLARIDAD == undefined) {
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

            }
            else {

                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: "La descripción " + $scope.NB_ESCOLARIDAD + " ya está en el catálogo.",
                    size: 'size-small',
                    type: tema.cssClass
                });
                
            }

        }
        );
    }

    /////////////////////
    $scope.Realiza_Inserccion = function (ID_NIVEL_ESC) {
        //"CLAVE INSTITUCION AQUI"
        $scope.Filtro_C_CATALOGO_VALOR(0, function (Objeto) {


            console.info(Objeto);
            if (Objeto.length != 0) {

                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: "Esta acción no se puede completar porque el Postgrado en " + $scope.NB_ESCOLARIDAD + " está actualmente asociada a un empleado.",
                    size: 'size-small',
                    type: tema.cssClass
                });
            }
            else {

                        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
                        if ($scope.ID_ESCOLARIDAD != undefined) {  //se verifica que la clave primaria sea diferente de nulo  
                            tipoOperacion = "A"
                        }//si se tiene la clave primaria se hara una modificacion en vez de insercion
                        var Obj_Escolaridad = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
                            ID_ESCOLARIDAD: $scope.ID_ESCOLARIDAD,
                            ID_NIVEL_ESCOLARIDAD: ID_NIVEL_ESC
                            , CL_NIVEL_ESCOLARIDAD: $scope.CL_NIVEL_ESCOLARIDAD
                            , NB_ESCOLARIDAD: $scope.NB_ESCOLARIDAD
                            , DS_ESCOLARIDAD: $scope.DS_ESCOLARIDAD
                            , FG_ACTIVO: $scope.FG_ACTIVO
                            , DS_FILTRO: ""
                        };
                        $http({
                            url: "ws/OperacionesGral.svc/Insert_update_C_ESCOLARIDAD",
                            method: "POST",
                            data: {
                                V_C_ESCOLARIDAD: Obj_Escolaridad,
                                usuario: TraerUsuario(),
                                programa: "InsertaCatalogoCarrPosgrado.html",
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

                                $rootScope.Llena_Grid_Dinamico();// SE MANDA REFRESCAR EL GRID "grid dinamico"
                                if ($scope.ID_ESCOLARIDAD == undefined) {
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
                         
            }
           

        }
        );
    }

    ////////////////////FIN Validaciones///////////////////////////

    $scope.ObtieneDatos = function () {

        $scope.Datos_Filtrar(true, $scope.NB_ESCOLARIDAD, function (Objeto) {

            if (Objeto.length == 0 && $scope.ID_ESCOLARIDAD == undefined) {
               $scope.Realiza_Inserccion($scope.ID_NIVEL_ESCOLARIDAD);
               
            }
            else {
                // $scope.ID_ESC = Objeto[0].ID_NIVEL_ESCOLARIDAD;
                //alert($scope.ID_ESCOLARIDAD);
                 $scope.Validaciones($scope.ID_NIVEL_ESCOLARIDAD);
                
            }
        });

    }



    ////////////// FUNCIONES PARA FILTRAR   //////////////
    //VALIDA SI LA DESCRIPCION NOMBRE  YA EXISTE EN C_AREA_INTERES
    $scope.Datos_Filtrar = function (flag,clave, response) {
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_C_ESCOLARIDAD",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FG_ACTIVO: flag, NB_ESCOLARIDAD: clave }),
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });
    }

    $scope.Filtro_C_NIVEL_ESCOLARIDAD = function (clave, response) {
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_C_NIVEL_ESCOLARIDAD",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ID_NIVEL_ESCOLARIDAD: clave }),
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });
    }


    $scope.Filtro_C_CATALOGO_VALOR = function (clave, response) {
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_C_CATALOGO_VALOR",
            method: "POST",
           contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID_CATALOGO_LISTA: clave }),
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });
    }

    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente 
        //1//
        $scope.ObtieneDatos();
};  
    $scope.cancel = function () {  
  //  $("#Grid_CatalogoEscolaridad_Posgrado").data("kendoGrid").dataSource.read();
    $modalInstance.dismiss('cancel');  
};  
});



 	 
  
