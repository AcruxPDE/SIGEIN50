

app.controller('BusquedaAvanzadaController', function ($scope, $http, $modal, $rootScope) {
    //habilita la animacion del popup para que se despliegue de arriba para abajo  
    $scope.animationsEnabled = true;
    //se instancia el objeto que contiene el titulo en español y ingles de las columnas  
    
    //console.info("Busqueda" + $rootScope.configuracion);
    //se le asignan los nombres a los botones y el titulo a nuestro asp   
  
    $scope.FILTROCONDICIONESDEFILTRADO = Config.FILTROCONDICIONESDEFILTRADO;
    $scope.FILTROVALORES = Config.FILTROVALORES;
    $scope.FILTROCAMPOSPARAFILTRAR = Config.FILTROCAMPOSPARAFILTRAR;
    $scope.FILTROCONDICIONESAFILTRAR = Config.FILTROCONDICIONESAFILTRAR;

    $scope.btnavanzado = Config.BTNFILTROAVANZADO;
    $scope.btnpersonalizar = Config.BOTONPERSONALIZAR;

    //inicia evento para agregar el registro  
    $scope.BusquedaAvanzada = function () {

        $scope.getdata = function () {
            //Metodo para traer los datos del $rootScope        
        }

        $scope.getdata();
        var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
            animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
            templateUrl: 'op_BusquedaAvanz.html', //se le indica el html que se abrira 
            controller: 'Modal_FiltroAvanzadoPersonalizar', //se le indica el controlador del html 
            windowClass: 'app-modal-window',
            resolve: { //se le envia el objeto 
                OBJETO: { valor: "Busqueda"}
            }

        });
    };

    $scope.Personalizar = function () { //se ejecuta en caso de que se de click en el boton de personalizar

        var modalInstance = $modal.open({ //se manda abrir la ventana modal para ingresar el registro 
            animation: $scope.animationsEnabled,  //se le asigna la variable que nos indica si tiene animacion o no 
            templateUrl: 'op_Personalizar.html', //se le indica el html que se abrira 
            controller: 'Modal_FiltroAvanzadoPersonalizar', //se le indica el controlador del html 
            resolve: { //se le envia el objeto 
                OBJETO: { valor: "Personalizar" }    } //se manda un objeto con un valor de Personalizar esto para el sessionStoraged a tomar
        });
    };

});

app.controller('Modal_FiltroAvanzadoPersonalizar', function ($scope, $modalInstance, $http, OBJETO,$rootScope) {
   
    
    $scope.titulo = $rootScope.configuracion.titulo;  //titulo asignado al root rootScope ,cada catalogo a usar grid dinamico debe de incluir su $rootScoop.titulo
    $scope.modulo = tema.cssClass;//color del modal
    $scope.FILTROCONDICIONESDEFILTRADO = Config.FILTROCONDICIONESDEFILTRADO;
    $scope.FILTROVALORES = Config.FILTROVALORES;
    $scope.FILTROCAMPOSPARAFILTRAR = Config.FILTROCAMPOSPARAFILTRAR;
    $scope.FILTROCONDICIONESAFILTRAR = Config.FILTROCONDICIONESAFILTRAR;
   
    /////////////////////////////ETIQUETAS PARA LA TABLA DE PERSONALIZAR Y BUSQUEDA AVANZADA/////////////////////////////////7
    $scope.BUSQUEDAAVANZADACAMPO = Config.BUSQUEDAAVANZADACAMPO;
    $scope.BUSQUEDAAVANZADACONDICION = Config.BUSQUEDAAVANZADACONDICION;
    $scope.BUSQUEDAAVANZADAVALOR1 = Config.BUSQUEDAAVANZADAVALOR1;
    $scope.BUSQUEDAAVANZADAVALOR2 = Config.BUSQUEDAAVANZADAVALOR2;
    $scope.BUSQUEDAAVANZADAELIMINAR = Config.BUSQUEDAAVANZADAELIMINAR;
    $scope.BUSQUEDAAVANZADAOPCIONES = Config.BUSQUEDAAVANZADAOPCIONES;
    ////////////////////////////////////////FIN ETIQUETAS PARA TABLAS/////////////////////////////////////////////////////////////////


    $scope.msjRequerido = Config.MSJREQUERIDO;
    $scope.btnSave = Config.BOTONGUARDAR;
    $scope.btnCancel = Config.BOTONCANCELAR;
    $scope.Filtrar = Config.BOTONFILTRAR;
    //////////////CONFIGURACION PARA AMBOS/////////////////////



    //////////////////////////////////////////////////////////

    $scope.Cargar_xml_root = function () {
        $scope.Obtiene();
    }; //carga de xml  <root> </root>
    $scope.Obtiene = function () { //funcion obtiene que va ir a SPE_OBTIENE_C_CAMPO_ADICIONAL_XML para traer las tablas indicadas en el xml
        $scope.Datos($rootScope.configuracion.xml, function (Objeto) { //SE CARGA EL XML MANDADO DEL CATALAGO CON $rootScope
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
    /////////////////////////////////////////////////////////
    

   // console.info($scope.array_valores);

    //REMPLAZA LOS TEXTOS INGLES- ESPAÑOL
    Remplazartextos = function (property, entidad,cl_tabla) {
        var obj = eval("(" + entidad + ")");
        //  console.log(property+","+entidad);
        Object.getOwnPropertyNames(obj).forEach(function (val, idx, array) {
            if (val == property) {
                property = obj[val];
            }
            else if (val == cl_tabla)
            {
                property = obj[val];
            }
        });

        return property;
    }


    var lista_campos = new Array(); //declaracion de arreglo para las listas de Buaqueda avanzada y Personalizar
    if ($scope.array_valores != null) { //validamos que no sea null
       var columns = $scope.array_valores; //asignamos a colums lo que regreso el response de GET_C_CAMPO_ADICIONAL_XML
       
        for (var i = 0; i < columns.length; i++) {
            //
            var Objeto = { posicion: (i + 1), field: columns[i].CL_TABLE_CAMPO, title: Remplazartextos(columns[i].CL_CAMPO, $rootScope.atributos.entidad, columns[i].CL_TABLE_CAMPO), hidden: columns[i].FG_MOSTRAR, tipo: columns[i].CL_TIPO_DATO, disable: "" };
            lista_campos.push(Objeto);//metemos los atributos seleccionados en el array lista_campos
        }
        function compare(a, b) {
            if (a.title < b.title)
                return -1;
            if (a.title > b.title)
                return 1;
            return 0;
        }

        lista_campos.sort(compare);
    }
    else { var columns = []; }// en un dado caso que no regrese nada el GET_C_CAMPO_ADICIONAL_XML dejamos columns vacio para que no marque error

    $scope.myDirectiveData = lista_campos; //asignamos a la lista de valores en busqueda avanzada
    $scope.ColumnasPersonalizar = lista_campos;//asignamos a la lista de valores en personalizar
    ///////////////////////////// MODAL DE BUSQUEDA AVANZADA//////////////////////////


    if (OBJETO.valor == "Busqueda") {
        $scope.TYPE_TEXT = "";  //textos del input text en BuquedaAvanzada.html
        $scope.TYPE_TEXT_NAME = "";//textos del input text en BuquedaAvanzada.html
        $scope.TYPE_TEXT2 = "";//textos del input text en BuquedaAvanzada.html
        $scope.TYPE_TEXT_NAME2 = "";//textos del input text en BuquedaAvanzada.html
        $scope.LABEL_VISIBLE = "hidden";// propiedad oculta para el tipo de dato date


        $scope.listaCondiciones = [//Se carga la lista de los operadores
               { title: GridKendo.EQ, valor: convert_operator(GridKendo.EQ) },
               { title: GridKendo.CONTAINS, valor: convert_operator(GridKendo.CONTAINS) },
               { title: GridKendo.DOESNOTCONTAIN, valor: convert_operator(GridKendo.DOESNOTCONTAIN) },
               { title: GridKendo.ENDSWITH, valor: convert_operator(GridKendo.ENDSWITH) },
               { title: GridKendo.NEQ, valor: convert_operator(GridKendo.NEQ) },
               { title: GridKendo.STARTSWITH, valor: convert_operator(GridKendo.STARTSWITH) },
               { title: Config.FILTROCONDICIONESFILTRARENTRE, valor: "Between" }
        ];

        function convert_operator(x) { //convierte al tipo de dato soportado por kendo
            var operador = "";
            if (x == "igual" || x == "equal") { operador = "eq"; } // : "equal"
            else if (x == "contiene" || x == "contains") { operador = "contains"; }// "contiene" : "contains"
            else if (x == "no contiene" || x == "does not contain") { operador = "doesnotcontain"; }// "no contiene" : "does not contain"
            else if (x == "finaliza con" || x == "ends with") { operador = "endswith"; }//"finaliza con" : "ends with"
            else if (x == "inicializa con" || x == "starts with") { operador = "startswith"; } //"inicializa con" : "starts with"
            else if (x == "Entre" || x == "Between") { } // "Entre" : "Between"
            else if (x == "no igual" || x == "does not equal") { operador = "neq"; }//"no igual" : "does not equal"
            else { }

            return operador;
        }

        var retrievedData = sessionStorage.getItem("valores"); //se trae el arreglo que esta en el sessionstoraged "valores"
        var array = JSON.parse(retrievedData);  //se parsea el arreglo para su manipulacion tipo Objeto
        if (array != null) {
            $scope.SessionStoragedData = array; //lo asignamos a la lista SessionStoragedData localizada en BusquedaAvanzada.html
        }
        else {
            $scope.SessionStoragedData = []; // si no encontramos datos en el sessionStoraged dejamos vacio
        }

    }


        ///////////////// Personalizar Modal ///////////////////////////////
    else {  var lista_personalizar = new Array();

        $scope.PERSONALIZARENCABEZADO = Config.PERSONALIZARENCABEZADO;
        $scope.PERSONALIZARGRILLA = Config.PERSONALIZARGRILLA;
        var a = $.parseJSON(sessionStorage.getItem("personalizar"));
        var json_local = $.parseJSON(localStorage.getItem("local catalogos"));
        //console.info(json_local);
        
        // alert(json_local.catalagos);
        var flag = true;
        if (json_local != null) {
           
            for (var i = 0; i < json_local.catalagos.length; i++) {

                //  alert("aquiiiii" + json_local.catalagos[i].name + "," + $rootScope.atributos.id);
                if (json_local.catalagos[i].name.indexOf($rootScope.atributos.id) > -1)
                {
                    flag = false;
                    lista_personalizar = json_local.catalagos[i].objeto;
                    $scope.TablaPersonalizar = lista_personalizar;
                    break;
                }
               
                
            }

            if(flag !=false){

                for (var i = 0; i < lista_campos.length; i++) {

                    if ($rootScope.configuracion.campos.indexOf(lista_campos[i].field) > -1) {
                        var index = $rootScope.configuracion.campos.indexOf(lista_campos[i].field);
                        //lista_campos[i].disable = "disabled";
                        lista_personalizar.splice(index, 0, lista_campos[i]);

                    }
                    else { }

                }
            $scope.ColumnasPersonalizar = lista_campos;

            myJSON = {
                "prueba": lista_personalizar
            };

            //funcion de inicio valor habilitados por defecto
            var queryResult = Enumerable.From(myJSON.prueba)
                .Where(function (x) {
                    return x.hidden == true
                })//restricciones
                .OrderBy(function (x) {
                    return x.index
                })//orden
                .Select(function (x) {
                    return x
                })//seleccion puede ser dinamico solo escoger x.element unicamente
                .ToArray(); //comvierte arreglo


            for (var i = 0; i < queryResult.length; i++) { //recorremos el arreglo
                queryResult[i].posicion = i + 1;
                myJSON.prueba[i].posicion = i + 1;
            }

            $scope.TablaPersonalizar = queryResult;// asignamos el arreglo a la tabla que tenemos para mostrar la busqueda avanzada
            sessionStorage.setItem("personalizar", JSON.stringify($scope.TablaPersonalizar));  //guardamos en el sessionStoraged
        
        }

            //
        }

            else if (a != null) {
                
            var myJSON = { "prueba": a };
            $scope.TablaPersonalizar = myJSON.prueba;
        }
        else {
            
            for (var i = 0; i < lista_campos.length; i++) {

                if ($rootScope.configuracion.campos.indexOf(lista_campos[i].field) > -1) {
                   var index =$rootScope.configuracion.campos.indexOf(lista_campos[i].field);
                  //lista_campos[i].disable = "disabled";
                   lista_personalizar.splice(index, 0, lista_campos[i]);
                   
                }
                else {  }

            }
            $scope.ColumnasPersonalizar = lista_campos;

            myJSON = {
                "prueba": lista_personalizar
            };

            //funcion de inicio valor habilitados por defecto
            var queryResult = Enumerable.From(myJSON.prueba)
                .Where(function (x) {
                    return x.hidden == true
                })//restricciones
                .OrderBy(function (x) {
                    return x.index
                })//orden
                .Select(function (x) {
                    return x
                })//seleccion puede ser dinamico solo escoger x.element unicamente
                .ToArray(); //comvierte arreglo


            for (var i = 0; i < queryResult.length; i++) { //recorremos el arreglo
                queryResult[i].posicion = i + 1;
                myJSON.prueba[i].posicion = i + 1;
            }

            $scope.TablaPersonalizar = queryResult;// asignamos el arreglo a la tabla que tenemos para mostrar la busqueda avanzada
            sessionStorage.setItem("personalizar", JSON.stringify($scope.TablaPersonalizar));  //guardamos en el sessionStoraged
        }

    }
   
    $scope.delete_Personalizar = function ($parentIndex, $index) {


        $scope.selectPersonalizar = {
            parent: $parentIndex,
            index: $index
        };

        $scope.TablaPersonalizar.splice($scope.selectPersonalizar.index, 1);
        for (var i = 0; i < $scope.TablaPersonalizar.length; i++) {
            $scope.TablaPersonalizar[i].posicion = i + 1;
        }

        sessionStorage.removeItem("personalizar");
        sessionStorage.setItem("personalizar", JSON.stringify($scope.TablaPersonalizar));

        function SaveDataToLocalStorage(data) {

            var a = $.parseJSON(sessionStorage.getItem("valores"));
            if (a != null) {
                sessionStorage.removeItem("valores");
                sessionStorage.setItem("valores", JSON.stringify(data));
            }
            else { sessionStorage.setItem("valores", JSON.stringify(data)); }
        }
        /*
               var arreglo = new Array();
               for (i = 0; i <= array.tabladinamica.length - 1; i++) {
                   arreglo.push(array.tabladinamica[i]);
               }
               $scope.SessionStoragedData = arreglo;
               */
    };
    $scope.add_Customize = function () {

        var campo = JSON.parse($scope.condicion[0]);

        var Objeto = { posicion: campo.posicion, field: campo.field, title: campo.title, hidden: false, tipo: campo.tipo };
        $scope.TablaPersonalizar.push(Objeto);
       
        for (var i = 0; i < $scope.TablaPersonalizar.length; i++) {
            $scope.TablaPersonalizar[i].posicion = i + 1;
        }
        sessionStorage.setItem("personalizar", JSON.stringify($scope.TablaPersonalizar));




    };
    $scope.ok_personalizar = function () { //en caso de que le den click en aceptar generara lo siguiente 

        //var displayedData = $(OBJETO.grid).data().kendoGrid.dataSource.view()
        var grid = $("#Grid_Dinamico").data("kendoGrid");
       // console.info($rootScope.atributos.campos);
        $rootScope.atributos.campos = [];

      
        var a = $.parseJSON(sessionStorage.getItem("personalizar"));
       for (var i = 0; i < a.length; i++) {
           $rootScope.atributos.campos.splice(i,0,a[i].field);
       }

       $rootScope.Llena_Grid_Dinamico();

       var Objeto = { name: $rootScope.atributos.id, objeto: a };


       var json_l = $.parseJSON(localStorage.getItem("local catalogos"));
      // console.info("debugging");
      // alert(json_l);
       if (json_l != null) {

           //  alert("entro");
           for (var i = 0; i < json_l.catalagos.length; i++) {
              
               if (json_l.catalagos[i].name.indexOf($rootScope.atributos.id) > -1) {
                   var index = json_l.catalagos[i].name.indexOf($rootScope.atributos.id);
                  // alert(index);
                    json_l.catalagos.splice(index, 1);
               }
           }
          
           json_l.catalagos.push(Objeto);
        
           localStorage.removeItem("local catalogos");
           localStorage.setItem("local catalogos", JSON.stringify(json_l));
           sessionStorage.removeItem("personalizar");
       } else {
           json_l = { "catalagos": [Objeto] }
           localStorage.setItem("local catalogos", JSON.stringify(json_l));
           sessionStorage.removeItem("personalizar");
       }

       $modalInstance.dismiss('cancel');
    };


    ///////////////////busqueda avanzada//////////////////////////
    $scope.add = function () {

        var campo = JSON.parse($scope.condicion[0]);
        var operador = JSON.parse($scope.operador[0]);
        var value1 = document.getElementById('txt_FILTRO_TEXTO').value;
        var value2 = document.getElementById('txt_FILTRO_FECHA').value;


        var flag = false;
        if (campo.field == "FG_ACTIVO") {
            if (value1 == "true" || value1 == "false") { value1 = Boolean(value1);  }
            else {
                flag = true;
                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: "No es un valor tipo Boolean (true/false)",
                    size: 'size-small',
                    type: tema.cssClass
                });
            }
        }
        else if (operador.valor == "Between") {
            if (value1 >= value2)
            {
                flag = true;
                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: Config.BUSQUEDAAVANZADAERROR,
                    size: 'size-small',
                    type: tema.cssClass
                });
            }
        }

        if (flag != true) {
            var Objeto = { field: campo.field, title: campo.title, operador: operador.title, operador_value: operador.valor, value: value1, value2: value2 };
            $scope.SessionStoragedData.push(Objeto);
            sessionStorage.removeItem("valores");
            sessionStorage.setItem("valores", JSON.stringify($scope.SessionStoragedData));
        }
    };

    $scope.change = function () {

        $scope.TYPE_TEXT = "text";
        $scope.TYPE_TEXT_NAME = "";
        $scope.TYPE_TEXT2 = "text";
        $scope.TYPE_TEXT_NAME2 = "";

        if ($scope.condicion != undefined) {
            var variable = JSON.parse($scope.condicion);

      
        }


        if ($scope.operador != undefined) {
            var xx = JSON.parse($scope.operador);

                //"Entre" : "Between"
            if ((xx.title == "Entre" || xx.title == "Between") && variable.tipo == "datetime") {
                $scope.LABEL_VISIBLE = "none";
               
                $scope.TYPE_TEXT = "date";
                $scope.TYPE_TEXT_NAME = "De";
                $scope.TYPE_TEXT2 = "date";
                $scope.TYPE_TEXT_NAME2 = "A";

            }
            else if ((xx.title == "Entre" || xx.title == "Between") && variable.tipo != "datetime") {
                $scope.LABEL_VISIBLE = "none";

                $scope.TYPE_TEXT = "text";
                $scope.TYPE_TEXT_NAME = "menor";
                $scope.TYPE_TEXT2 = "text";
                $scope.TYPE_TEXT_NAME2 = "mayor";


            }


            else if (variable.tipo == "datetime") {
                $scope.LABEL_VISIBLE = "hidden";

                $scope.TYPE_TEXT = "date";
                $scope.TYPE_TEXT_NAME = "De";
           

            } 
           
            else {
               $scope.LABEL_VISIBLE = "hidden";
                $scope.TYPE_TEXT2 = "text";
                $scope.TYPE_TEXT_NAME2 = "";
                $scope.TYPE_TEXT = "text";
                $scope.TYPE_TEXT_NAME = "";
              
            }

        }
       
        if ($scope.condicion != undefined)
        {
            var y = JSON.parse($scope.condicion);

            if (y.title != undefined) {
                if (stringStartsWith(y.title, "Fecha") || endsWith(y.title, "Date")) {

                }
            }
           
        }
        
        
     
        function stringStartsWith(string, prefix) {
            return string.slice(0, prefix.length) == prefix;
        }
        function endsWith(str, suffix) {
            return str.indexOf(suffix, str.length - suffix.length) !== -1;
        }
    };//metodo para ocultar etiquetas dependiendo si son fechas
    $scope.delete_ = function ($parentIndex, $index) {


        $scope.selectedPosition = {
            parent: $parentIndex,
            index: $index
        };

        // console.info($scope.selectedPosition);

        var retrievedData = sessionStorage.getItem("valores");
        var array = JSON.parse(retrievedData);

        //var index =$scope.SessionStoragedData.indexOf($scope.selectedPosition.parent);
        $scope.SessionStoragedData.splice($scope.selectedPosition.index, 1);

        sessionStorage.removeItem("valores");
        sessionStorage.setItem("valores", JSON.stringify($scope.SessionStoragedData));

    };
    $scope.ok = function () { //en caso de que le den click en aceptar generara lo siguiente 

        var contact = true;


        $filter = new Array();
        if (contact == true) {

            try {
                for (var i = 0; i < $scope.SessionStoragedData.length; i++) {

                    if ($scope.SessionStoragedData[i].operador_value != "Between") {

                        if (isNaN($scope.SessionStoragedData[i].value) == false) {
                            //  alert("si");

                            $filter.push({ field: $scope.SessionStoragedData[i].field, operator: $scope.SessionStoragedData[i].operador_value, value: parseInt($scope.SessionStoragedData[i].value) });

                        }
                        else {
                            $filter.push({ field: $scope.SessionStoragedData[i].field, operator: $scope.SessionStoragedData[i].operador_value, value: $scope.SessionStoragedData[i].value });

                        }
                     }
                    else {
                        
                        if (isNaN($scope.SessionStoragedData[i].value) == false) {
                            $filter.push({ field: $scope.SessionStoragedData[i].field, operator: "gt", value: parseInt($scope.SessionStoragedData[i].value) });
                            $filter.push({ field: $scope.SessionStoragedData[i].field, operator: "lt", value: parseInt($scope.SessionStoragedData[i].value2) });
                        }
                        else if (($scope.SessionStoragedData[i].value instanceof Date)) {//case para date Object
                            $filter.push({ field: $scope.SessionStoragedData[i].field, operator: "gt", value: $scope.SessionStoragedData[i].value });
                            $filter.push({ field: $scope.SessionStoragedData[i].field, operator: "lt", value: $scope.SessionStoragedData[i].value2 });

                        } else { //Exception}
                        }
                    }

                }

                var ds = $("#Grid_Dinamico").data("kendoGrid").dataSource;
               ds.filter($filter);
                $modalInstance.dismiss('cancel');

                sessionStorage.removeItem("valores");
            } catch (err) {
                BootstrapDialog.show({
                    title: Config.MENSAJEERROR,
                    message: err.message,
                    size: 'size-small',
                    type: tema.cssClass
                });
            }

        }
    
    };


    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };//se sale del modal




});


