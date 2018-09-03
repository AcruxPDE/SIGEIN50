

app.controller('C_AREA_INTERESController', function ($scope, $http, $modal, $rootScope) {


    // console.info($rootScope.configuracion);
    //habilita la animacion del popup para que se despliegue de arriba para abajo  
    $scope.animationsEnabled = true;
    //se instancia el objeto que contiene el titulo en español y ingles de las columnas  

    //se le asignan los nombres a los botones y el titulo a nuestro asp   
    $scope.titulo = C_AREA_INTERES.TITULO_PANTALLA;
    //$scope.btnActivarDesactivar = Config.BOTONACTIVARDESACTIVAR;   
    $scope.btnAgregar = Config.BOTONAGREGAR;
    $scope.btnModificar = Config.BOTONMODIFICAR;
    $scope.btnEliminar = Config.BOTONELIMINAR;

    var campos_mostrar = { "array": ["CL_AREA_INTERES", "NB_AREA_INTERES"] };
    var filtros = null;
    var INSERT_UPDATE = {
        ID_AREA_INTERES: undefined
             , CL_AREA_INTERES: ''
             , NB_AREA_INTERES: ''
             , FG_ACTIVO: ''
    }



    var xml = "<root>" +
                "<row1>" +
                 "<value ID=\"C_AREA_INTERES\"></value>" + //DECLARAR TABLAS PARA GENERAR LA BUSQUEDA AVANZADA Y PERSONALIZAR DEL GRID DINAMICO
                "</row1>" +
                "</root>";
    ////////////////////////////////////////////////
    $scope.Cargar_xml_Area = function () {
        $scope.Obtiene_Area(xml);
    }; //carga de xml  <root> </root>
    $scope.Obtiene_Area = function (xML) { //funcion obtiene que va ir a SPE_OBTIENE_C_CAMPO_ADICIONAL_XML para traer las tablas indicadas en el xml
        $scope.Datos_Area_Interes(xML, function (Objeto) { //SE CARGA EL XML MANDADO DEL CATALAGO CON $rootScope
            if (Objeto.length != 0) {//checamos que traiga datos el objeto
                $scope.array_valores = Objeto; //si es verdad asignamos a la variable $scope.array_valores
            }
          /*  else { //Se ejecuta un error 
                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: Config.PERSONALIZARERRORGUARDAR,
                    size: 'size-small',
                    type: tema.cssClass
                });

            }*/
        });

    }
    $scope.Datos_Area_Interes = function (Xml, response) { //Peticion a C_CAMPO_ADICIONAL_XML
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_C_CAMPO_ADICIONAL_XML",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ XML: Xml }), //FILTRO DE XML
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });
    }
    $scope.Cargar_xml_Area(); // EJECUTAMOS NUESTRA FUNCION DE Cargar xml_root()
    /////////////////////////////////////


    ///////////////////////////////////////
    $scope.Cargar_C_Area = function () {
        $scope.Obtiene_C_Area();
    };
    $scope.Obtiene_C_Area = function () { //funcion obtiene que va ir a SPE_OBTIENE_C_CAMPO_ADICIONAL_XML para traer las tablas indicadas en el xml
        $scope.Datos_C_Area(function (Objeto) { //
            if (Objeto.length != 0) {//checamos que traiga datos el objeto
                // alert(Objeto);
                $scope.array_area = Objeto; //si es verdad asignamos a la variable $scope.array_valores
            }
            else { //Se ejecuta un error 
                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: Config.PERSONALIZARERRORGUARDAR,
                    size: 'size-small',
                    type: tema.cssClass
                });
            }
        });

    }
    $scope.Datos_C_Area = function (response) { //
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_C_AREA_INTERES",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: null,
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });

    }
    $scope.Cargar_C_Area(); // EJECUTAMOS   
    //////////////////////////////////////
    var arreg = new Array();
    for (var i = 0; i < $scope.array_area.length; i++) {
        var Objeto = { nb_campo: $scope.array_area[i].CL_CAMPO, tipo: $scope.array_area[i].CL_TIPO_DATO };
        arreg.push(Objeto);
    }


    ////////////////////////////////////////////////////////////// 
    //inicia evento para agregar el registro  
    $scope.Add_Grid = function () {

        var Obj_ = INSERT_UPDATE;//$rootScope.atributos.ObjInsUpd;
        var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
            animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
            templateUrl: 'op_Area_Interes.html',
            controller: 'Modal_Area_Interes',
            resolve: { //se le envia el objeto 
                OBJETO: function () { return Obj_; }
            }
        });
    };

    //termina evento para agregar el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia evento para edita el registro  
    $scope.Edit_Grid = function () {
        if ($rootScope.entidad == undefined) { // se verifica que el registro este seleccionado  
            BootstrapDialog.show({ //se muestra el mensaje de error  
                title: $scope.titulo, //se trae el nombre de la pantalla  
                message: Config.MENSAJEREGISTRO, // se envia el mensaje de error que falta el registro  
                size: 'size-small', //se especifica el tamaño de la ventana del mensaje  
                type: tema.cssClass //se especifica el color en base al modulo que se tiene  
            });
            return false; //se devuelve nulo para que no sigua con la funcion  
        }
        var datos = $rootScope.entidad; //se asigna el registro a la variable de datos
        //console.info(datos);
        var Objeto_Edit = datos;


        var modalInstance = $modal.open({ //se manda ejecutar en modo modal la instancia definida en el html para la edicion  
            animation: $scope.animationsEnabled, //se le asigna la variable que nos indica si tiene animacion o no  
            templateUrl: 'op_Area_Interes.html',
            controller: 'Modal_Area_Interes',
            resolve: { //se le envia el objeto  
                OBJETO: function () { return Objeto_Edit; }
            }
        });
    };



  


    $rootScope.atributos =
        {
            id: 'Grid_Area_Interes',
            titulo: $scope.titulo,
            grid: "#Grid_Area_Interes",
            getgrid: "Get_C_AREA_INTERES",
            deletegrid: "Delete_C_AREA_INTERES",
            entidad: 'C_AREA_INTERES',
            templateUrl: 'op_Area_Interes.html',
            controller: 'Modal_Area_Interes',
            programa: 'CatalogoArea_Interes.html',
            campos: campos_mostrar.array
            , ObjInsUpd: INSERT_UPDATE
             , btn_adicional: 'hidden'
            , array_get: arreg
            , datos: $scope.array_area
        };

    //BUSQUEDA AVANZADA
    $rootScope.configuracion = {
        titulo: $scope.titulo,
        grid: "#Grid_Area_Interes",
        campos: campos_mostrar.array,
        xml: "<root>" +
                  "<row1>" +
                   "<value ID=\"C_AREA_INTERES\"></value>" + //DECLARAR TABLAS PARA GENERAR LA BUSQUEDA AVANZADA Y PERSONALIZAR DEL GRID DINAMICO
                  "</row1>" +
                  "</root>"
    };


    //termina evento para edita el registro  
    ////////////////////////////////////////////////////////////// 


    ////////////////////////////////////////////////////////////// 
    //inicia eventos para eliminar el registro  
    $scope.Del_Grid = function () {
      
        if ($rootScope.entidad == undefined) { //se verifica si la entidad que guarda el evento onchange del grid no se encuentra vacia  
            BootstrapDialog.show({ //en caso de que no se encuentre se mostrara un error  
                title: $scope.titulo, //se le pone el titulo especificado en la clase   
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

        var datos = $rootScope.entidad;  //se extrae el registro obtenido en el evento change del grid

        console.info(datos);
        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/Delete_C_AREA_INTERES",
            method: "POST",
            data: { //se le pasan los parametros 
                ID_AREA_INTERES: datos.ID_AREA_INTERES, // se le pasa la llave primaria  OJO poner V_LISTA DINAMICA EN TU DELETE METODO
                CL_AREA_INTERES: datos.CL_AREA_INTERES,
                usuario: TraerUsuario(), // se le pasa el usuario 
                programa: 'CatalogoArea_Interes.html'  //se le pasa  
            },
            headers: {  //SE ESPECIFICAN LAS CABECERAS QUE SE ENVIAN AL WEB SERVICE, ES DECIR COMO SE ENVIAN LOS DATOS: JSON, XML, TEXTO PLANO O FILE 
                'Accept': 'application/json',  //SE INDICA QUE EL EVENTO ACEPTARA EL TIPO JSON  
                'Content-Type': 'application/json' // SE INDICA QUE TIPO CONTENDRA LA INFORMACION QUE SE ENVIA   
            }
        })
        .then(function (response) { // ejecucion de WS 
            if (response.data != true) {  //si el procedimiento regresa falso 
                BootstrapDialog.show({ //se mostrara un mensaje de error  
                    title: $scope.titulo,  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
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
                    title: $scope.titulo,  //el titulo del mensaje  
                    message: Config.MENSAJEELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.cssClass,  //se le indica el color dependiendo del modulo en que se encuentre 
                });
            }
        },
        function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
            BootstrapDialog.show({
                title: $scope.titulo,
                message: Config.ERRORGENERICO,
                size: 'size-small',
                type: tema.cssClass,
            });
        });
    };


});
//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR C_AREA_INTERES 
app.controller('Modal_Area_Interes', function ($scope, $modalInstance, $http, OBJETO, $rootScope) {


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




    $scope.Valida_K_area = function (x) {
        $scope.Filtro_K_Area_Interes(x, function (Objeto) {

            if (Objeto.length != 0) {
                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: C_AREA_INTERES.EXCEPTION,
                    size: 'size-small',
                    type: tema.cssClass
                });
            }
            else {
                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: C_AREA_INTERES.EXCEPTION2,
                    size: 'size-small',
                    type: tema.cssClass
                });
            }
        }
        );
    }

    //Proceso de validacion del campo NB_AREA_INTERES
    $scope.ObtieneDatos = function () {

        $scope.Datos_Filtrar(true, $scope.NB_AREA_INTERES, function (Objeto) {

            //console.info(Objeto);  
            if (Objeto.length != 0 && $scope.ID_AREA_INTERES == undefined) {
                $scope.ID_AREA_INT = Objeto[0].ID_AREA_INTERES;
                $scope.Valida_K_area($scope.ID_AREA_INT);
            }
            else {

                var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
                if ($scope.ID_AREA_INTERES != undefined)  //se verifica que la clave primaria sea diferente de nulo  
                    tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  
                var Obj_Area_Interes = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
                    ID_AREA_INTERES: $scope.ID_AREA_INTERES
                    , CL_AREA_INTERES: $scope.CL_AREA_INTERES
                    , NB_AREA_INTERES: $scope.NB_AREA_INTERES
                    , FG_ACTIVO: $scope.FG_ACTIVO,
                };
                $http({
                    url: "ws/OperacionesGral.svc/Insert_update_C_AREA_INTERES",
                    method: "POST",
                    data: {
                        V_C_AREA_INTERES: Obj_Area_Interes,
                        usuario: TraerUsuario(),
                        //ojo modificar   
                        programa: "InsertaArea_Interes.html",
                        tipo_transaccion: tipoOperacion
                    },
                    headers: {
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    }
                })
                .then(function (response) {
                    //  alert(response.data);

                    if (response.data != true) {

                        BootstrapDialog.show({
                            title: Config.MENSAJEERROR,
                            message: Config.ERRORGUARDAR,
                            size: 'size-small',
                            type: tema.cssClass
                        });

                    } else {

                        $rootScope.Llena_Grid_Dinamico();

                        if ($scope.ID_AREA_INTERES == undefined) {
                            LimpiarFormulario("newForm");


                        };

                        BootstrapDialog.show({
                            title: Config.MENSAJEERROR,
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
        });

    }

    //VALIDA SI LA DESCRIPCION NOMBRE DE AREA INTERES YA EXISTE EN C_AREA_INTERES
    $scope.Datos_Filtrar = function (flag, nombre, response) {
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_C_AREA_INTERES",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FG_ACTIVO: flag, NB_AREA_INTERES: nombre }),
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });
    }
    //VALIDA SI LA DESCRIOCION ID_AREA_INTERES DE AREA INTERES YA EXISTE EN K_AREA_INTERES
    $scope.Filtro_K_Area_Interes = function (id, response) {
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_K_AREA_INTERES",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ ID_AREA_INTERES: id }),
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });
    }

    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente  

        $scope.ObtieneDatos();
    };

    $scope.cancel = function () {
        //   $("#Grid_Area_Interes").data("kendoGrid").dataSource.read();  
        $modalInstance.dismiss('cancel');
    };
});

