
app.controller('M_EMPLEADOController', function ($scope, $http, $modal, $rootScope) {


    $scope.animationsEnabled = true;
    //se instancia el objeto que contiene el titulo en español y ingles de las columnas  

    
    //se le asignan los nombres a los botones y el titulo a nuestro asp   
    $scope.titulo = M_EMPLEADO.TITULO_PANTALLA;
    //$scope.btnActivarDesactivar = Config.BOTONACTIVARDESACTIVAR;   
    $scope.btnAgregar = Config.BOTONAGREGAR;
    $scope.btnModificar = Config.BOTONMODIFICAR;
    $scope.btnEliminar = Config.BOTONELIMINAR;
  

    var campos_mostrar = { "array": ["M_EMPLEADO_ID_EMPLEADO", "M_EMPLEADO_CL_EMPLEADO", "NB_EMPLEADO_COMPLETO", "NB_DEPARTAMENTO", "NB_EMPRESA"] };
   // var alias = { "array": ["MD", "CE", "MP","CC","ME"] };

    //"MP_NB_PUESTO", "MD_NB_DEPARTAMENTO", "ME_NB_EMPRESA", "ME_ACTIVO"
    var filtros = null;

    xml= "<root>" +
              "<row1>" +
               "<value ID=\"C_EMPRESA\"></value>" + //ECLARAR TABLAS PARA GENERAR LA BUSQUEDA AVANZADA Y PERSONALIZAR DEL GRID DINAMICO
              "<value ID=\"M_EMPLEADO\"></value>" +
               "<value ID=\"M_PUESTO\"></value>" +
               "<value ID=\"M_DEPARTAMENTO\"></value>" +
               "<value ID=\"C_CANDIDATO\"></value>" +
              "</row1>" +
              "</root>";


   ////////////////////////////////////////////////
    $scope.Cargar_xml_root = function () {
        $scope.Obtiene(xml);
    }; //carga de xml  <root> </root>
    $scope.Obtiene = function (xML) { //funcion obtiene que va ir a SPE_OBTIENE_C_CAMPO_ADICIONAL_XML para traer las tablas indicadas en el xml
        $scope.Datos(xML, function (Objeto) { //SE CARGA EL XML MANDADO DEL CATALAGO CON $rootScope
            if (Objeto.length != 0) {//checamos que traiga datos el objeto
                $scope.array_valores = Objeto; //si es verdad asignamos a la variable $scope.array_valores
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
    $scope.Datos = function (Xml, response) { //Peticion a C_CAMPO_ADICIONAL_XML
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
    $scope.Cargar_xml_root(); // EJECUTAMOS NUESTRA FUNCION DE Cargar xml_root()
    /////////////////////////////////////


    ///////////////////////////////////////
    $scope.Cargar_M_Empleado = function () {
        $scope.Obtiene_M_Empleado();
    }; 
    $scope.Obtiene_M_Empleado = function () { //funcion obtiene que va ir a SPE_OBTIENE_C_CAMPO_ADICIONAL_XML para traer las tablas indicadas en el xml
        $scope.Datos_M_Empleado(function (Objeto) { //
            if (Objeto.length != 0) {//checamos que traiga datos el objeto
               // alert(Objeto);
                $scope.array_empleado = Objeto; //si es verdad asignamos a la variable $scope.array_valores
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
    $scope.Datos_M_Empleado = function (response) { //
        $.ajax({
            async: false,
            url: "ws/OperacionesGral.svc/Get_M_EMPLEADO",
            method:"POST",
            contentType: "application/json; charset=utf-8",
            data: null,
            dataType: "json",
            success: function (data) {
                response(data);
            }
        });

   
    }
    $scope.Cargar_M_Empleado(); // EJECUTAMOS   
    //////////////////////////////////////
    var arreg = new Array();
    for (var i = 0; i < $scope.array_valores.length; i++) {
        var Objeto = { nb_campo: $scope.array_valores[i].CL_CAMPO, tipo: $scope.array_valores[i].CL_TIPO_DATO };
        arreg.push(Objeto);
    }

    ///console.info(arreg);

    var INSERT_UPDATE = {
          M_EMPLEADO_ID_EMPLEADO : undefined
        , M_EMPLEADO_CL_EMPLEADO : ''
        , M_EMPLEADO_NB_EMPLEADO_COMPLETO : ''
        , M_EMPLEADO_NB_EMPLEADO : ''
        , M_EMPLEADO_NB_APELLIDO_PATERNO : ''
        , M_EMPLEADO_NB_APELLIDO_MATERNO : ''
        , M_EMPLEADO_CL_ESTADO_EMPLEADO : ''
        , M_EMPLEADO_CL_GENERO : ''
        , M_EMPLEADO_CL_ESTADO_CIVIL : ''
        , M_EMPLEADO_NB_CONYUGUE : ''
        , M_EMPLEADO_CL_RFC : ''
        , M_EMPLEADO_CL_CURP : ''
        , M_EMPLEADO_CL_NSS : ''
        , M_EMPLEADO_CL_TIPO_SANGUINEO : ''
        , M_EMPLEADO_CL_NACIONALIDAD : ''
        , M_EMPLEADO_NB_PAIS : ''
        , M_EMPLEADO_NB_ESTADO : ''
        , M_EMPLEADO_NB_MUNICIPIO : ''
        , M_EMPLEADO_NB_COLONIA : ''
        , M_EMPLEADO_NB_CALLE : ''
        , M_EMPLEADO_NO_INTERIOR : ''
        , M_EMPLEADO_NO_EXTERIOR : ''
        , M_EMPLEADO_CL_CODIGO_POSTAL : ''
        , M_EMPLEADO_XML_TELEFONOS : ''
        , M_EMPLEADO_CL_CORREO_ELECTRONICO : ''
        , M_EMPLEADO_ACTIVO : ''
        , M_EMPLEADO_FE_NACIMIENTO : ''
        , M_EMPLEADO_DS_LUGAR_NACIMIENTO : ''
        , M_EMPLEADO_FE_ALTA : ''
        , M_EMPLEADO_FE_BAJA : ''
        , M_EMPLEADO_ID_PUESTO : ''
        ,M_EMPLEADO_MN_SUELDO : ''
        , M_EMPLEADO_MN_SUELDO_VARIABLE : ''
        , M_EMPLEADO_DS_SUELDO_COMPOSICION : ''
        , M_EMPLEADO_ID_CANDIDATO : ''
        , M_EMPLEADO_XML_CAMPOS_ADICIONALES : ''
        , M_EMPLEADO_ID_EMPRESA : ''
        , M_PUESTO_ACTIVO : ''
        , M_PUESTO_FE_INACTIVO : ''
        , M_PUESTO_CL_PUESTO : ''
        , M_PUESTO_NB_PUESTO : ''
        , M_PUESTO_ID_PUESTO_JEFE : ''
        , M_PUESTO_ID_DEPARTAMENTO : ''
        , M_PUESTO_XML_CAMPOS_ADICIONALES : ''
        , M_PUESTO_ID_BITACORA : ''
        , C_CANDIDATO_NB_CANDIDATO : ''
        , C_CANDIDATO_NB_APELLIDO_PATERNO : ''
        , C_CANDIDATO_NB_APELLIDO_MATERNO : ''
        , C_CANDIDATO_CL_GENERO : ''
        , C_CANDIDATO_CL_RFC : ''
        , C_CANDIDATO_CL_CURP : ''
        , C_CANDIDATO_CL_ESTADO_CIVIL : ''
        , C_CANDIDATO_NB_CONYUGUE : ''
        , C_CANDIDATO_CL_NSS : ''
        , C_CANDIDATO_CL_TIPO_SANGUINEO : ''
        , C_CANDIDATO_NB_PAIS : ''
        , C_CANDIDATO_NB_ESTADO : ''
        , C_CANDIDATO_NB_MUNICIPIO : ''
        , C_CANDIDATO_NB_COLONIA : ''
        , C_CANDIDATO_NB_CALLE : ''
        , C_CANDIDATO_NO_INTERIOR : ''
        , C_CANDIDATO_NO_EXTERIOR : ''
        , C_CANDIDATO_CL_CODIGO_POSTAL : ''
        , C_CANDIDATO_CL_CORREO_ELECTRONICO : ''
        , C_CANDIDATO_FE_NACIMIENTO : ''
        , C_CANDIDATO_DS_LUGAR_NACIMIENTO : ''
        , C_CANDIDATO_MN_SUELDO : ''
        , C_CANDIDATO_CL_NACIONALIDAD : ''
        , C_CANDIDATO_DS_NACIONALIDAD : ''
        , C_CANDIDATO_NB_LICENCIA : ''
        , C_CANDIDATO_DS_VEHICULO : ''
        , C_CANDIDATO_CL_CARTILLA_MILITAR : ''
        , C_CANDIDATO_CL_CEDULA_PROFESIONAL : ''
        , C_CANDIDATO_XML_TELEFONOS : ''
        , C_CANDIDATO_XML_INGRESOS : ''
        , C_CANDIDATO_XML_EGRESOS : ''
        , C_CANDIDATO_XML_PATRIMONIO : ''
        , C_CANDIDATO_DS_DISPONIBILIDAD : ''
        , C_CANDIDATO_CL_DISPONIBILIDAD_VIAJE : ''
        , C_CANDIDATO_XML_PERFIL_RED_SOCIAL : ''
        , C_CANDIDATO_DS_COMENTARIO : ''
        , C_CANDIDATO_ACTIVO : ''
        , C_EMPRESA_CL_EMPRESA : ''
        , C_EMPRESA_NB_EMPRESA : ''
        , C_EMPRESA_NB_RAZON_SOCIAL : ''
        , M_DEPARTAMENTO_ACTIVO : ''
        , M_DEPARTAMENTO_FE_INACTIVO : ''
        , M_DEPARTAMENTO_CL_DEPARTAMENTO : ''
        , M_DEPARTAMENTO_NB_DEPARTAMENTO : ''
        , M_DEPARTAMENTO_XML_CAMPOS_ADICIONALES : ''
        , DS_FILTRO : ''
    

    };

    //  var str = '{a:"www"}';
    //  var obj = eval("(" + str + ")");
    //  console.log(obj);

    $rootScope.atributos =
        {
            id: 'Grid_CatalogoEmpleados',
            titulo: $scope.titulo,
            grid: "#Grid_CatalogoEmpleados",
            getgrid: "Get_M_EMPLEADO",
            deletegrid: "Delete_M_EMPLEADO",
            entidad: 'M_EMPLEADO',
            templateUrl: 'op_Empleados_c.html',
            controller: 'Modal_CatalogoEmpleados',
            programa: 'CatalogoEmpleados.html',
            filtrar:null,
            campos: campos_mostrar.array,
            ObjInsUpd: INSERT_UPDATE,
            SPE_lista: "V_M_EMPLEADO"
             , btn_adicional: 'hidden'
            ,array_get: arreg    ///////felipe aqui solamente mando una lista de objetos con el nombre y el tipo
            , datos: $scope.array_empleado
        };


    $rootScope.configuracion =
        {
            titulo: $scope.titulo,
            gridd: "#Grid_CatalogoEmpleados",
            campos: campos_mostrar.array,
            xml: "<root>" +
                   "<row1>" +
                     "<value ID=\"C_EMPRESA\"></value>" + //ECLARAR TABLAS PARA GENERAR LA BUSQUEDA AVANZADA Y PERSONALIZAR DEL GRID DINAMICO
                     "<value ID=\"M_EMPLEADO\"></value>" +
                     "<value ID=\"M_PUESTO\"></value>" +
                     "<value ID=\"M_DEPARTAMENTO\"></value>" +
                     "<value ID=\"C_CANDIDATO\"></value>" +
                   "</row1>" +
                   "</root>"
        };






    ////////////////////////////////////////////////////////////// 
    //inicia evento para agregar el registro  
    $scope.Add_Grid = function () {

        var Obj_ = INSERT_UPDATE;//$rootScope.atributos.ObjInsUpd;
        var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
            animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
            templateUrl: 'op_Empleados_c.html',
            controller: 'Modal_CatalogoEmpleados',
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
        if ($scope.entidad == undefined) { // se verifica que el registro este seleccionado  
            BootstrapDialog.show({ //se muestra el mensaje de error  
                title: $scope.titulo, //se trae el nombre de la pantalla  
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
            templateUrl: 'op_Empleados_c.html', //se le indica el html que se abrira  
            controller: 'Modal_CatalogoEmpleados', //se le indica el controlador del html  
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

        var datos = $scope.entidad;  //se extrae el registro obtenido en el evento change del grid 

        console.info(datos);
        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/Delete_M_EMPLEADO",
            method: "POST",
            data: { //se le pasan los parametros 
                ID_EMPLEADO: datos.M_EMPLEADO_ID_EMPLEADO, // se le pasa la llave primaria  OJO poner V_LISTA DINAMICA EN TU DELETE METODO
                CL_EMPLEADO: datos.M_EMPLEADO_CL_EMPLEADO,
                usuario: TraerUsuario(), // se le pasa el usuario 
                programa: 'CatalogoEmpleados.html'  //se le pasa  
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


    //termina eventos para eliminar el registro  
    ////////////////////////////////////////////////////////////// 




});


//MODAL QUE ABRE LA VENTANA DE INSERTAR/EDITAR C_ESCOLARIDAD 
app.controller('Modal_CatalogoEmpleados', function ($scope, $modalInstance, $http, OBJETO, $rootScope) {
    alert(OBJETO.M_EMPLEADO_ID_EMPLEADO);
    /*
    $scope.modulo = tema.cssClass;
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

    */
    

   


    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente 
        //1//
        $scope.ObtieneDatos();
    };
    $scope.cancel = function () {
        //  $("#Grid_CatalogoEscolaridad_Posgrado").data("kendoGrid").dataSource.read();
        $modalInstance.dismiss('cancel');
    };
});





