
app.controller('M_PUESTOSController', function ($scope, $http, $modal, $rootScope) {


    $scope.animationsEnabled = true;
    //se instancia el objeto que contiene el titulo en español y ingles de las columnas  


    //se le asignan los nombres a los botones y el titulo a nuestro asp   
    $scope.titulo = FILTRO_M_PUESTO.TITULO_PANTALLA;
    //$scope.btnActivarDesactivar = Config.BOTONACTIVARDESACTIVAR;   
    $scope.btnAgregar = Config.BOTONAGREGAR;
    $scope.btnModificar = Config.BOTONMODIFICAR;
    $scope.btnEliminar = Config.BOTONELIMINAR;


    var campos_mostrar = { "array": ["CL_PUESTO", "NB_PUESTO", "NB_DEPARTAMENTO"] };
 
    var filtros = null;


    var INSERT_UPDATE = {
        ID_PUESTO: undefined
        , FG_ACTIVO: ''
        , FE_INACTIVO: ''
        , CL_PUESTO: ''
        , NB_PUESTO: ''
        , ID_PUESTO_JEFE: ''
        , ID_DEPARTAMENTO: ''
        , XML_CAMPOS_ADICIONALES: ''
        , ID_BITACORA: ''
        , NO_EDAD_MINIMA: ''
        , NO_EDAD_MAXIMA: ''
        , CL_GENERO: ''
        , CL_ESTADO_CIVIL: ''
        , XML_REQUERIMIENTOS: ''
        , XML_OBSERVACIONES: ''
        , XML_RESPONSABILIDAD: ''
        , XML_AUTORIDAD: ''
        , XML_CURSOS_ADICIONALES: ''
        , XML_MENTOR: ''
        , CL_TIPO_PUESTO: ''
        , ID_CENTRO_ADMINISTRATIVO: ''
        , ID_CENTRO_OPERATIVO: ''
        , ID_PAQUETE_PRESTACIONES: ''
        , NB_DEPARTAMENTO: ''
        , CL_DEPARTAMENTO: ''
        , DS_FILTRO: ''
    };

    //  console.info(campos_mostrar);


   var xml= "<root>" +
               "<row1>" +
                "<value ID=\"M_PUESTO\"></value>" + //ECLARAR TABLAS PARA GENERAR LA BUSQUEDA AVANZADA Y PERSONALIZAR DEL GRID DINAMICO
                "<value ID=\"M_DEPARTAMENTO\"></value>" +
               "</row1>" +
               "</root>";
    ////////////////////////////////////////////////
    $scope.Cargar_xml_Puestos = function () {
        $scope.Obtiene_Puestos(xml);
    }; //carga de xml  <root> </root>
    $scope.Obtiene_Puestos = function (xML) { //funcion obtiene que va ir a SPE_OBTIENE_C_CAMPO_ADICIONAL_XML para traer las tablas indicadas en el xml
        $scope.Datos_Puestos(xML, function (Objeto) { //SE CARGA EL XML MANDADO DEL CATALAGO CON $rootScope
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
    $scope.Datos_Puestos = function (Xml, response) { //Peticion a C_CAMPO_ADICIONAL_XML
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
    $scope.Cargar_xml_Puestos(); // EJECUTAMOS NUESTRA FUNCION DE Cargar xml_root()
    /////////////////////////////////////


    ///////////////////////////////////////
    $scope.Cargar_C_Puestos = function () {
        $scope.Obtiene_C_Puestos();
    };
    $scope.Obtiene_C_Puestos = function () { //funcion obtiene que va ir a SPE_OBTIENE_C_CAMPO_ADICIONAL_XML para traer las tablas indicadas en el xml
        $scope.Datos_C_Puestos(function (Objeto) { //
            if (Objeto.length != 0) {//checamos que traiga datos el objeto
                // alert(Objeto);
                $scope.array_puesto = Objeto; //si es verdad asignamos a la variable $scope.array_valores
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
    $scope.Datos_C_Puestos = function (response) { //
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_M_PUESTO",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: null,
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });

    }
    $scope.Cargar_C_Puestos(); // EJECUTAMOS   
    //////////////////////////////////////
   // console.info($scope.array_puesto);
    var arreg = new Array();
    for (var i = 0; i < $scope.array_valores.length; i++) {
        var Objeto = { nb_campo: $scope.array_valores[i].CL_CAMPO, tipo: $scope.array_valores[i].CL_TIPO_DATO };
        arreg.push(Objeto);
    }
    console.info("aqui");
    console.info(arreg);
    ////////////////////////////////////////////////////////////// 
    //inicia evento para agregar el registro  
    $scope.Add_Grid = function () {
        var Obj_ = INSERT_UPDATE;//$rootScope.atributos.ObjInsUpd;
        var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
            animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
            templateUrl: 'op_Puestos_c.html',
            controller: 'Modal_CatalogoPuestos',
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
            templateUrl: 'op_Puestos_c.html',
            controller: 'Modal_CatalogoPuestos',
            resolve: { //se le envia el objeto  
                OBJETO: function () { return Objeto_Edit; }
            }
        });
    };

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
            url: "ws/OperacionesGral.svc/Delete_M_PUESTO",
            method: "POST",
            data: { //se le pasan los parametros 
                ID_PUESTO: datos.ID_PUESTO, // se le pasa la llave primaria  OJO poner V_LISTA DINAMICA EN TU DELETE METODO
                usuario: TraerUsuario(), // se le pasa el usuario 
                programa: 'CatalogoPuestos.html'  //se le pasa  
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

    $scope.copiar = function () { alert("copiar algo"); }




    $rootScope.atributos =
       {
           id: 'Grid_CatalogoPuestos',
           titulo: $scope.titulo,
           grid: "#Grid_CatalogoPuestos",
           getgrid: "Get_M_PUESTO",
           deletegrid: "Delete_M_PUESTO",
           entidad: 'FILTRO_M_PUESTO',
           templateUrl: 'op_Puestos_c.html',
           controller: 'Modal_CatalogoPuestos',
           programa: 'CatalogoPuestos.html',
           campos: campos_mostrar.array,
           filtro: filtros,
           ObjInsUpd: INSERT_UPDATE,
           SPE_lista: "V_LISTA_DINAMICA"
          , btn_adicional: 'button'
            , array_get: arreg
            , datos: $scope.array_area
       };


    $rootScope.configuracion =
        {
            titulo: $scope.titulo,
            gridd: "#Grid_CatalogoPuestos",
            campos: campos_mostrar.array,
            xml: "<root>" +
                   "<row1>" +
                    "<value ID=\"M_PUESTO\"></value>" + //ECLARAR TABLAS PARA GENERAR LA BUSQUEDA AVANZADA Y PERSONALIZAR DEL GRID DINAMICO
                    "<value ID=\"M_DEPARTAMENTO\"></value>" +
                   "</row1>" +
                   "</root>"
        };


});


//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR C_ESCOLARIDAD 
app.controller('Modal_CatalogoPuestos', function ($scope, $modalInstance, $http, OBJETO, $rootScope) {
   
    
    $scope.modulo = tema.cssClass;
    $scope.NB_PUESTO = OBJETO.NB_PUESTO;
    $scope.CL_PUESTO = OBJETO.CL_PUESTO;
    $scope.ID_PUESTO = OBJETO.ID_PUESTO;


  
    //se generan las traducciones de las etiquetas, placeholder (ayuda en los componentes) 
    $scope.lbl_NB_PUESTO_ph = FILTRO_M_PUESTO.NB_PUESTO_ph;
    $scope.txt_NB_PUESTO = FILTRO_M_PUESTO.NB_PUESTO;
    $scope.lbl_CL_PUESTO_ph = FILTRO_M_PUESTO.CL_PUESTO_ph;
    $scope.txt_CL_PUESTO = FILTRO_M_PUESTO.CL_PUESTO;
    $scope.lblActivo = FILTRO_M_PUESTO.FG_ACTIVO;

    //se generan las traducciones Genericas 
    $scope.titulo = FILTRO_M_PUESTO.TITULO_PANTALLA;
    $scope.msjRequerido = Config.MSJREQUERIDO;
    $scope.btnSave = Config.BOTONGUARDAR;
    $scope.btnCancel = Config.BOTONCANCELAR;

   

    //////////////////////////////////////


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

        $scope.Datos_Filtrar(true, $scope.CL_PUESTO, function (Objeto) {

         
            if (Objeto.length != 0 && $scope.ID_PUESTO == undefined) {
               
                console.info(Objeto);


                $scope.ID_PUESTO = Objeto[0].$scope.ID_PUESTO;
                $scope.Valida_K_area($scope.ID_PUESTO);
            }
            else {

                var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
                if ($scope.ID_PUESTO != undefined)  //se verifica que la clave primaria sea diferente de nulo  
                    tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  
                var Obj_Puestos = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
                    ID_PUESTO: $scope.ID_PUESTO
                    , CL_PUESTO: $scope.CL_PUESTO
                    , NB_PUESTO: $scope.NB_PUESTO
                    , FG_ACTIVO: $scope.FG_ACTIVO
                    , ID_DEPARTAMENTO :1002
                    , FE_INACTIVO :null
                    , ID_PUESTO_JEFE :null
                    , XML_CAMPOS_ADICIONALES: null
                    , ID_BITACORA: null
                    , NO_EDAD_MINIMA: null
                    , NO_EDAD_MAXIMA: null
                    , CL_GENERO: null
                    , CL_ESTADO_CIVIL: null
                    , XML_REQUERIMIENTOS: null
                    , XML_OBSERVACIONES: null
                    , XML_RESPONSABILIDAD: null
                    , XML_AUTORIDAD: null
                    , XML_CURSOS_ADICIONALES: null
                    , XML_MENTOR: null
                    , CL_TIPO_PUESTO: null
                    , ID_CENTRO_ADMINISTRATIVO: null
                    , ID_CENTRO_OPERATIVO: null
                    , ID_PAQUETE_PRESTACIONES: null
                    , NB_DEPARTAMENTO: null
                    , CL_DEPARTAMENTO: null
                    , DS_FILTRO :''


                };
               // console.info("aqio");
               // console.info(Obj_Puestos);
                $http({
                    url: "ws/OperacionesGral.svc/Insert_update_M_PUESTO",
                    method: "POST",
                    data: {
                        V_M_PUESTO: Obj_Puestos,
                        usuario: TraerUsuario(),
                        programa: "InsertaM_Puestos.html",
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
                        $rootScope.Nb_puesto = $scope.NB_PUESTO;
                        $rootScope.Clavep = $scope.CL_PUESTO;
                        $rootScope.Id_puesto = $scope.ID_PUESTO;
                     
                        if ($scope.ID_PUESTO == undefined) {
                            LimpiarFormulario("newForm");

                            Navegacion(2, 'index.html#/Descrip');
                           
                           // $modalInstance.dismiss('cancel');

                         
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
            url: "ws/OperacionesGral.svc/Get_M_PUESTO",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ FG_ACTIVO: flag, CL_PUESTO: nombre }),
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });
    }
    //VALIDA SI LA DESCRIOCION ID_AREA_INTERES DE AREA INTERES YA EXISTE EN K_AREA_INTERES
    $scope.Filtro_K_ = function (id, response) {
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



    ///////////////////////////////////////





    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente 
        //1//
        $scope.ObtieneDatos();
    
    };
    $scope.cancel = function () {
        //  $("#Grid_CatalogoEscolaridad_Posgrado").data("kendoGrid").dataSource.read();
        $modalInstance.dismiss('cancel');
    };
});





