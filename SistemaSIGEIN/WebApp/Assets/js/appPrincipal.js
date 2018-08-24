var app = angular.module('MiAplicacion', ['ngFileUpload', 'ngRoute', 'ui.bootstrap', 'kendo.directives', 'ngSanitize']);


var TEMA = function () {
    this.cssClass = "";
    switch (numModulo) {
        case "1": this.cssClass = "type-mod1"; break;
        case "2": this.cssClass = "type-mod2"; break;
        case "3": this.cssClass = "type-mod3"; break;
        case "4": this.cssClass = "type-mod4"; break;
        case "5": this.cssClass = "type-mod5"; break;
        default: this.cssClass = "type-primary"; break;
    }
    return this.cssClass;
}

//INSTANCIA DE LA CLASE DE JS
var Config = new Configuracion();
var GridKendo = new GridKendo();
var Depto = new M_DEPARTAMENTO();
var tema = new TEMA();
var C_NIVEL_ESCOLARIDAD = new C_NIVEL_ESCOLARIDAD();
var M_DEPARTAMENTO = new M_DEPARTAMENTO();
var C_ROL = new C_ROL();
var S_FUNCION = new S_FUNCION();
var S_TIPO_COMPETENCIA = new S_TIPO_COMPETENCIA();
var C_COMPETENCIA = new C_COMPETENCIA();
var C_EVALUADOR_EXTERNO = new C_EVALUADOR_EXTERNO();

var C_ESTADO = new C_ESTADO();
var C_MUNICIPIO = new C_MUNICIPIO();
var C_COLONIA = new C_COLONIA();
var C_CLASIFICACION_COMPETENCIA = new C_CLASIFICACION_COMPETENCIA();
var K_SOLICITUD = new K_SOLICITUD();
var FILTRO_M_PUESTO = new FILTRO_M_PUESTO();
var LOGIN = new LOGIN();
var C_CATALOGO_LISTA = new C_CATALOGO_LISTA();
var C_CATALOGO_VALOR = new C_CATALOGO_VALOR();
var SPE_OBTIENE_K_REQUISICION = new SPE_OBTIENE_K_REQUISICION();
var SPE_OBTIENE_C_ROL_FUNCION = new SPE_OBTIENE_C_ROL_FUNCION();
var C_AREA_INTERES = new C_AREA_INTERES();
var C_CARRERA_TECNICA = new C_CARRERA_TECNICA();
var C_EMPRESA = new C_EMPRESA();
var C_CARRERA_POSGRADO = new C_CARRERA_POSGRADO();
var C_CARRERA_PROFESIONAL = new C_CARRERA_PROFESIONAL();
var SELECCION_PERSONAL = new SELECCION_PERSONAL();
var M_EMPLEADO = new M_EMPLEADO();

//ENTIDAD DE ACCESO
var RouteAcceso =
{
    html: "Acceso.html",
    Controlador: "AccesoController"
}



//NAVEGACION
app.config(['$routeProvider', function ($routeProvider, $routerParams) {
    $routeProvider
        .when('/', {
            templateUrl: (VerificaSesionRoute()) ? 'plantillas/Inicio.html' : 'Salir.html',
            controller: (VerificaSesionRoute()) ? '' : 'SalirController'
        })
        .when('/CatDepto', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoDepartamento.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatLocaliza', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoLocalizacion.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatNivEsc', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoNivelEsc.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatComp', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoCompetencias.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatEvExt', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoEvaluadorExt.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatTec', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoCarrTecnica.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatEmplead', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoEmpleados.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatPus', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoPuestos.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatTipoRe', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoTipoRePu.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatAreaIn', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoAreaInteres.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatRol', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoRol.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatFun', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoFunciones.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatTipoCom', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoTipoCompetencia.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatCom', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoCompetencias.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/NvaSol', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/NuevaSolicitudEmp.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatClaCom', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoClasificacionCompetencia.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatLista', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoLista.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatVal', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoValores.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/Requisiciones', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/Requisiciones.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/Salir', {
            templateUrl: (VerificaSesionRoute()) ? 'Salir.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? 'SalirController' : RouteAcceso.Controlador
        })
        .when('/CatEmp', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoEmpresa.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
          .when('/CatPosg', {
              templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoCarrPosgrado.html' : RouteAcceso.html,
              controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
          })
        .when('/CatProf', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoCarrProfesional.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/GridDinamico', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/GridDinamico.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/Camposdinamicos', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/Camposdinamicos.html' : RouteAcceso.html,
        })
        .when('/CatEsc', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoEscolaridad.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/EvaExt', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoEvaluadorExt.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/CatFun', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoFunciones.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })

        .when('/CatPuestos', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoPuestos.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })

        .when('/SelPer', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/SeleccionPersonal.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })

        .when('/CatVal', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoValores.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
          .when('/Descrip', {
              templateUrl: (VerificaSesionRoute()) ? 'Administracion/DescriptivoPuestos.html' : RouteAcceso.html,
              controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
          })
        .when('/Requisiciones', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/Requisiciones.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/Salir', {
            templateUrl: (VerificaSesionRoute()) ? 'Salir.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? 'SalirController' : RouteAcceso.Controlador
        })
        .when('/CatEmp', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoEmpresa.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
          .when('/CatPosg', {
              templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoCarrPosgrado.html' : RouteAcceso.html,
              controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
          })
        .when('/CatProf', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/CatalogoCarrProfesional.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/GridDinamico', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/GridDinamico.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .when('/Camposdinamicos', {
            templateUrl: (VerificaSesionRoute()) ? 'Administracion/Camposdinamicos.html' : RouteAcceso.html,
            controller: (VerificaSesionRoute()) ? '' : RouteAcceso.Controlador
        })
        .otherwise({
            redirectTo: '/'

        })

}])

//METODOS PARALAS OPERACIONES DE SALIR DEL SISTEMA
app.controller("SalirController", function ($scope) {

    //SALIR DEL SISTEMA
    $scope.Salir = function () {
        sessionStorage.clear();
        Navegacion(2, "login.html");
    }

    $scope.Salir();
});

//FORMATO FECHA DIRECTIVA REALIZADA EN ANGULAR
app.directive('formatearFecha', function ($filter) {
    var directive = {};

    directive.restrict = "A";
    directive.require = "ngModel";

    directive.link = function ($scope, element, attributes, ngModel) {

        function fromUser(text) {
            return text = $filter('date')(text, attributes.formato);
        }

        function toUser(text) {
            return text = $filter('date')(text, attributes.formato);
        }

        ngModel.$parsers.push(fromUser);
        ngModel.$formatters.push(toUser);

    }

    return directive;
});

//METODOS QUE COMPETEN A MENU
app.controller('MenuController', function ($scope) {

    $scope.EfectoMenu = function (animacion, elemento) {
        $('#' + elemento).addClass(animacion + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass(animacion + ' animated');
        });
    }

    $scope.NavegacionMenu = function (modulo) {
        sessionStorage.setItem("modulo", modulo);
        Navegacion(2, 'index.html');
    }

    $scope.CargaInformacion = function () {
        CargarTextoDom();
    }

    $scope.IniciarLibrerias = function () {
        $('[data-toggle="popover"]').popover();
    }

    $scope.IniciarLibrerias = function () {
        $('[data-toggle="popover"]').popover();
    }
    $scope.IniciarLibrerias();
    $scope.EfectoMenu("bounceInDown", "Logo");
    $scope.EfectoMenu("bounceInDown", "MenuAccesoRapido");
    $scope.CargaInformacion();

    $scope.IniciarLibrerias();
});

//SE EJECUTA EL CONTROLADOR DE TEMA PARA CARGAR EL ELEGIDO (FUNCION PARA LLAMAR  EL TEMA SI PREVIAMENTE  YA ESTA REGISTRADO  EN EL NAVEGADOR)
app.controller('OperacionesController', function ($scope) {

    window.history.go(1);

    $scope.traerColorTema = function () {
        var color = "#428bca";
        var modulo = 0;

        //TRAER MODULO
        if (sessionStorage.getItem("modulo") != null) {
            modulo = parseInt(sessionStorage.getItem("modulo"));
        }

        switch (modulo) {
            case 1: color = "#4D8900"; break;
            case 2: color = "#FF7400"; break;
            case 3: color = "#A20804"; break;
            case 4: color = "#0087CF"; break;
            case 5: color = "#79026F"; break;
            case 6: color = "#E8023D"; break;
            default: color = "#428bca"; break;
        }

        return color;
    }

    $scope.CambioTema = function () {
        if (localStorage.getItem("tema") != null) {
            var tema = localStorage.getItem("tema");
            CambiarTema(tema);
        }
    };

    $scope.CambioLenguaje = function () {
        if (localStorage.getItem("idioma") != null) {
            var idioma = localStorage.getItem("idioma");
            CambiarLenguaje(idioma);
        }
    };

    $scope.Animar = function (animacion) {
        $('#nav-inner').addClass(animacion + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass().addClass("navbar navbar-default");
        });
    };

    //MOSTRAR USUARIO LOGEADO 
    $scope.MostrarUsuario = function () {
        $scope.getuUuario = TraerUsuario();
    }

    //GENERACION DEL MENU
    $scope.GeneracionMenu = function () {
        var idioma = "es";
        if (localStorage.getItem("idioma") != null) {
            idioma = localStorage.getItem("idioma");
        }

        var Modulo = 0;
        if (sessionStorage.getItem("modulo") != null) {
            Modulo = sessionStorage.getItem("modulo");
        }

        var menuizq = $("#menuEncabezadoIzquierda");
        var menuder = $("#menuEncabezadoDerecha");

        //MENUS DE LA DERECHA QUE DEBEN DE APARECER  ENT TODOS LOS MODULOS 

        if (VerificaSesionRoute() == true) {

            var liMenuSolicitudesEmpleo = "<li><a href=''><i class='fa fa-file' style='font-size:21px;color: #6F7172;float: left;margin-right: 5px;'></i><span class='visible-xs'>Solicitudes de empleo</span></a></li>";
            menuder.append(liMenuSolicitudesEmpleo);

            var liMenuInventarioPersonal = "<li><a href=''><i class='fa fa-users' style='font-size:21px;color: #6F7172;float: left;margin-right: 5px;'></i><span class='visible-xs'>Inventario de personal</span></a></li>";
            menuder.append(liMenuInventarioPersonal);


            var liMenuDescripcionespuesto = "<li><a href=''><i class='fa fa-book' style='font-size:21px;color: #6F7172;float: left;margin-right: 5px;'></i><span class='visible-xs'>Descripciones de puesto</span></a></li>";
            menuder.append(liMenuDescripcionespuesto);


            var liMenucompetencias = "<li><a href=''><i class='fa fa-line-chart' style='font-size:21px;color: #6F7172;float: left;margin-right: 5px;'></i><span class='visible-xs'>Competencias</span></a></li>";
            menuder.append(liMenucompetencias);

            var liMenuconfiguracion = "<li><a href=''><i class='fa fa-cog' style='font-size:21px;color: #6F7172;float: left;margin-right: 5px;'></i><span class='visible-xs'>Configuración</span></a></li>";
            menuder.append(liMenuconfiguracion);

        }

        $.getJSON("data/Menu.json", function (data) {

            //CONSULTA LINQ PARA  MOSTRAR LOS MENUS QUE CORRESPONDEN 
            var queryResult = Enumerable.From(data)
              .Where(function (x) { return (x.modulo == Modulo || x.modulo == "*") })
              .ToArray();


            for (var i = 0; i < queryResult.length; i++) {

                //CREACION DE LOS MENUS
                var _menu = queryResult[i].Menu;
                var _icono = queryResult[i].Icono;
                var _tipo = queryResult[i].Tipo;
                var _clave = queryResult[i].Clave;
                var _modulo = queryResult[i].modulo;

                var liMenud = $("<li/>");
                liMenud.addClass("dropdown");
                var liMenuI = $("<li/>");

                var _atributos = queryResult[i].Atributos;

                for (var j = 0; j < _atributos.attr.length; j++) {
                    var _attr = _atributos.attr[j];

                    var _href = _atributos.attr[j].href;
                    var _nombre = _atributos.attr[j].nombre;
                    var _tieneSubmenu = _atributos.attr[j].tieneSubmenu;
                    var _class = _atributos.attr[j].class;
                    var _role = _atributos.attr[j].role;
                    var _ariaHaspopup = _atributos.attr[j].ariaHaspopup;
                    var _ariaExpanded = _atributos.attr[j].ariaExpanded;
                    var _dataToogle = _atributos.attr[j].dataToggle;

                    if (_tipo == "Derecha") {
                        var icon = (_icono != "") ? "<i class='" + _icono + "' style='font-size: 20px'></i>" : "";
                        var aText = (_nombre = "{{}}") ? TraerUsuario() : _nombre;
                        var caret = (_tieneSubmenu == "1") ? "<span class='caret'></span>" : "";
                        var aMenud = "<a href='" + _href + "' data-toggle='" + _dataToogle + "' role='" + _role + "' aria-haspopup='" + _ariaHaspopup + "' aria-expanded='" + _ariaExpanded + "'  class='dropdown-toggle' >" + icon + " " + aText + " " + caret + "</a>";

                        liMenud.html(aMenud);

                    } else {
                        if (_tieneSubmenu == "0") {
                            var aMenuI = "<a href='" + _href + "'>" + _nombre + " </a>";
                        } else {
                            var icon = (_icono != "") ? "<i class='" + _icono + "' style='font-size: 17px'></i>" : "";
                            var aText = _nombre;
                            var caret = (_tieneSubmenu == "1") ? "<span class='caret'></span>" : "";
                            var aMenuI = "<a href='" + _href + "' data-toggle='" + _dataToogle + "' role='" + _role + "' aria-haspopup='" + _ariaHaspopup + "' aria-expanded='" + _ariaExpanded + "'   class='dropdown-toggle'>" + icon + " " + aText + " " + caret + "</a>";

                        }
                        liMenuI.html(aMenuI);
                    }

                }


                var _subMenu = queryResult[i].SubMenu;

                var submenud = (_tieneSubmenu == "0") ? "" : $("<ul/>").addClass("dropdown-menu");
                var submenuI = (_tieneSubmenu == "0") ? "" : $("<ul/>").addClass("dropdown-menu");

                for (var k = 0; k < _subMenu.smenu.length; k++) {
                    var _smenu = _subMenu.smenu[k];

                    var _hrefsub = _subMenu.smenu[k].href;
                    var _nombresub = _subMenu.smenu[k].nombre;
                    var _ngClicksub = _subMenu.smenu[k].ngclick;
                    var _clavesub = _subMenu.smenu[k].clave;

                    if (_tipo == "Derecha") {

                        if (_nombresub == "-") {
                            var lisubmenud = $("<li/>", {
                                'class': 'divider',
                                'role': 'separator'
                            });
                        } else {
                            var lisubmenud = $("<li/>");
                        }

                        if (_nombre != "-") {
                            var aSubmenud = "<a href='" + _hrefsub + "'  ng-click='" + _ngClicksub + "'>" + _nombresub + "</a>";
                            lisubmenud.append(aSubmenud);
                        }
                        submenud.append(lisubmenud);

                    } else {
                        if (_nombresub == "-") {
                            var lisubmenuI = $("<li/>", {
                                'class': 'divider',
                                'role': 'separator'
                            });
                        } else {
                            var lisubmenuI = $("<li/>");
                        }

                        if (_nombre != "-") {
                            var aSubmenuI = "<a href='" + _hrefsub + "' ng-click='" + _ngClicksub + "'>" + _nombresub + "</a>";
                            lisubmenuI.append(aSubmenuI);
                        }
                        submenuI.append(lisubmenuI);

                    }

                }

                liMenud.append(submenud);
                liMenuI.append(submenuI);

                // SE GENERAN  LOS MENUS  Y SUBMENUS DINAMICAMENTE
                if (VerificaSesionRoute() == true) {
                    menuizq.append(liMenuI);
                    menuder.append(liMenud);
                }

            }

        });
    }

    $scope.MostrarUsuario();
    $scope.Animar("fadeInDownBig");
    $scope.CambioTema();
    $scope.CambioLenguaje();
    $scope.GeneracionMenu();

});

//METODOS PARA EL ACCESO A LAS PANTALLAS
app.controller('AccesoController', function ($scope) {

    //VARIABLES
    $scope.mensaje = "";

    //METODOS  Y FUNCIONES QUE  TENDRA ACCESO CONTROLLER
    $scope.Cargando = function () {
        $scope.mensaje = Config.PERMISOSPAGINA;
    }

    $scope.salir = function () {
        sessionStorage.clear();
        Navegacion(2, "login.html");
    }

    //LLAMADA CUANDO CARGE EL CONTROLADOR DE ANGULAR
    $scope.Cargando();
});

//ACCIONES DE LA VENTANA DE INICIO
app.controller('InicioController', function ($scope) {

    $scope.Modulo = "Modulo";
    $scope.ModuloNombre = "";
    $scope.ModuloNombre = ["Integración personal", "Formación y desarrollo", "Evaluación organizacional", "Metodologia para la compesación", "Nomina"];
    $scope.Seccion = "Inicio";

    $scope.CargarpagInicio = function () {

        $scope.NombreModulo = "";
        $scope.Contenidohtml = "";
        $scope.ImagenModulo = "";

        var Modulo = 0;
        if (sessionStorage.getItem("modulo") != null) {
            Modulo = sessionStorage.getItem("modulo");
        }

        //CONSULTA LINQ PARA  MOSTRAR LOS MENUS QUE CORRESPONDEN 
        var queryResult = Enumerable.From(data.inicio)
          .Where(function (x) { return x.Modulo == Modulo })
          .ToArray();


        for (var i = 0; i < queryResult.length; i++) {
            var nombremodulo = queryResult[i].NombreModulo;
            var contenidoHtml = queryResult[i].Contenidohtml;
            var imagenmodulo = queryResult[i].Imagen;
        }

        $scope.NombreModulo = nombremodulo;
        $scope.Contenidohtml = contenidoHtml;
        $scope.ImagenModulo = imagenmodulo;

    }

    $scope.IniciarLibrerias = function () {
        $('[data-toggle="popover"]').popover();
    }

    $scope.CambioLenguaje = function () {
        if (localStorage.getItem("idioma") != null) {
            var idioma = localStorage.getItem("idioma");
            CambiarLenguaje(idioma);
        }
    }

    $scope.CargaModulo = function () {
        if (sessionStorage.getItem("modulo") != null) {
            $scope.Modulo = $scope.ModuloNombre[(parseInt(sessionStorage.getItem("modulo")) - 1)];
        }

    }

    $scope.CambioLenguaje();
    $scope.IniciarLibrerias();
    $scope.CargaModulo();

    $scope.CargarpagInicio();
});

//METODOS QUE COMPETEN A LOGIN
app.controller('LoginController', function ($scope, $http) {

    //INICIALIZADO DE VARIABLES EN VACIO 
    $scope.user = "";
    $scope.pass = "";
    $scope.empresa = "";
    $scope.logoUsuario = "";


    $scope.EfectoLogin = function (animacion) {
        $('#ContentLogin').addClass(animacion + ' animated').one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend', function () {
            $(this).removeClass().addClass("container");
        });
    }

    $scope.CargarLogo = function () {
        if (localStorage.getItem("logoUsuario") != null) {
            $scope.logoUsuario = localStorage.getItem("logoUsuario");
        } else {
            $scope.logoUsuario = "Assets/images/LoginUsuario.png";
        }
    }

    $scope.ErrorValidacion = function (campo, validacion) {
        if (validacion) {
            return ($scope.frmLogin[campo].$dirty && $scope.frmLogin[campo].$error[validacion]) || ($scope.submitted && $scope.frmLogin[campo].$error[validacion]);
        }
        return ($scope.frmLogin[campo].$dirty && $scope.frmLogin[campo].$invalid) || ($scope.submitted && $scope.frmLogin[campo].$invalid);
    };

    $scope.Login = function () {
        //ENTIDAD USUARIO
        if ($scope.frmLogin.$valid == false) {
            $scope.ErrorValidacion("Usuario", "required");
            $scope.ErrorValidacion("Password", "required");
        }
        console.log($scope.frmLogin.$valid);

        var Objusuario =
        {
            usuario:
                {
                    Usuario: $scope.user,
                    Password: $scope.pass,
                    Empresa: '',
                    BaseDatos: ''
                }

        }
        //EJECUTA LA BARRA DE PROGRESO
        NProgress.start();
        //console.log(JSON.stringify(Objusuario));
        console.log(Objusuario);
        $http({
            url: "ws/OperacionesGral.svc/Login",
            method: "POST",
            data: JSON.stringify(Objusuario),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            }
        })
        .then(function (response) {
            console.log(response);
            console.log(response.data);
            if (response.data != "Realizado") {
                BootstrapDialog.show({
                    title: Config.ACCESOALSISTEMA,
                    message: Config.ERRORACCESO,
                    size: 'size-small'
                });
            } else {
                //CREACION DE VARIABLES DE SESION
                sessionStorage.setItem("usuario", $scope.user);
                Navegacion(2, "menu.html");
            }
            NProgress.done();
        },
        function (response) {
            BootstrapDialog.show({
                title: Config.ACCESOALSISTEMA,
                message: Config.ERRORGENERICO,
                size: 'size-small'
            });
            //CONCLUYE CON LA BARRA DE PROGRESO
            NProgress.done();
        });

    }

    //RECUPERA CONTRASEÑA DEL USUARIO
    $scope.RecuperaPassword = function () {
        BootstrapDialog.show({
            title: Config.RECUPERAPASSWORD,
            message: $('<div></div>').html("<div style='margin: 5px;' ng-controller='GeneralController'> " +
                                           " <div class='row'> " +
                                           "     <div> " +
                                           "         <label>" + LOGIN.TITULO_CORREO + "</label> " +
                                           "         <input type='text' name='txtCorreo' id='txtCorreo' value='' class='form-control' placeholder='" + LOGIN.PLACEHOLDERCORREO + "' /> " +
                                           "     </div> " +
                                           "   </div> " +
                                           " </div> "),
            size: 'size-small',
            buttons: [{
                label: Config.BOTONENVIAR,
                action: function (dialog) {
                    alert($("#txtCorreo").val());
                }
            }, {
                label: Config.BOTONCANCELAR,
                action: function (dialog) {
                    dialog.close();
                }
            }]

        });
    }

    $scope.CargaInformacion = function () {
        CargarTextoDom();
    }

    $scope.EntrarTecla = function ($event) {
        if ($event.which == 13) {
            $scope.Login();
        }
    }

    $scope.CargarLogo();
    $scope.CargaInformacion();
    $scope.EfectoLogin("fadeIn");
});

//CAMBIAR EL TEMA EN LA APLICACION WEB
CambiarTema = function (tema) {
    var cssEstilo = "";
    var jsEstilo = "";
    if (parseInt(tema) === 1) //METRO
    {
        cssEstilo = "Assets/skin/metro/kendo.custom.css";
        jsEstilo = "Assets/skin/metro/kendo.custom.js";
    } else if (parseInt(tema) === 2) //BOOTSTRAP
    {
        cssEstilo = "Assets/skin/bootstrap/kendo.custom.css";
        jsEstilo = "Assets/skin/bootstrap/kendo.custom.js";
    } else if (parseInt(tema) === 3) //MATERIAL
    {
        cssEstilo = "Assets/skin/material/kendo.custom.css";
        jsEstilo = "Assets/skin/material/kendo.custom.js";
    } else if (parseInt(tema) === 4) //SHILVER
    {
        cssEstilo = "Assets/skin/shilver/kendo.custom.css";
        jsEstilo = "Assets/skin/shilver/kendo.custom.js";
    }
    //ASIGNACION DE ATRIBUTO  PARA EL CAMBIO DEL TEMA EN LA PAGINA  WEB
    document.getElementById("jsSkin").setAttribute("src", jsEstilo)
    document.getElementById("cssSkin").setAttribute("href", cssEstilo);

    //ASIGNACION EL TIPO DE TEMA A UN LOCALSTORAGE
    localStorage.setItem("tema", tema);

    //REMOVER LAS CLASES  DE LOS OTROS ATRIBUTOS  Y AGREGAR EL SELECCIONADO POR DEFAULT
    $("i.eventTema").removeClass("fa fa-check");
    $("#tema" + tema.toString()).addClass("fa fa-check");


}

//CAMBIA AL TEMA 2 QUE ES BOOTSTRAP
CambiarTema(2);

//EXTRAE LA PABRA CARGAR LOS TEXTOS QUE LES CORRESPONDE. 
CargarTextoDom = function () {

    var idioma = "es";
    if (localStorage.getItem("idioma") != null) {
        idioma = localStorage.getItem("idioma");
    }

    $.getJSON("data/Informacion.json", function (data) {

        $(".TextoDom").each(function (i, element) {

            var Modulo = $(element).attr("data-modulo");

            var queryResult = Enumerable.From(data)
              .Where(function (x) { return x.Modulo == Modulo })
              .ToArray();

            var info = LeerDatosArreglo(queryResult, element, idioma);

            //VALIDACION SI EXISTEN LOS ATRIBUTOS               
            if ($(element).hasAttr("placeholder")) {
                AsignarDatoAtributo(element, "placeholder", info.PlaceHolder);

            } else if ($(element).hasAttr("data-intro")) {
                AsignarDatoAtributo(element, "data-intro", info.DataIntro);

            } else if ($(element).hasAttr("data-original-title")) {
                AsignarDatoAtributo(element, "data-original-title", info.DataOriginalTitle);

            } else if ($(element).hasAttr("data-content")) {
                AsignarDatoAtributo(element, "data-content", info.DataContent);
            }
            else if ($(element).hasAttr("value")) {
                AsignarDatoAtributo(element, "value", info.Value);

            } else {
                AsignarDatoAtributo(element, "data-html", info.DataHtml);
            }

        });
    });
}

//ASIGNAR DATO AL ATRIBUTO CORRESPONDIENTE
AsignarDatoAtributo = function (elemento, atributo, valor) {
    if (atributo == "data-html") {
        $(elemento).html(valor);
    } else {
        $(elemento).attr(atributo, valor);
    }
}

//LEER LOS DATOS DE UN ARREGLO
LeerDatosArreglo = function (queryResult, element, idioma) {
    var Atributos =
    {
        DataHtml: "",
        PlaceHolder: "",
        Value: "",
        DataIntro: "",
        DataOriginalTitle: "",
        DataContent: ""
    }

    for (var i = 0; i < queryResult.length; i++) {
        if ($(element).attr("id") == queryResult[i].Key) {
            if (idioma == "es") {
                Atributos.Value = ($(element).hasAttr("value")) ? queryResult[i].EsMX : "";
                Atributos.DataIntro = ($(element).hasAttr("data-intro")) ? queryResult[i].EsMxGuiame : "";
                Atributos.DataOriginalTitle = ($(element).hasAttr("data-original-title")) ? queryResult[i].EsMxCabeceraTooltip : "";
                Atributos.DataContent = ($(element).hasAttr("data-content")) ? queryResult[i].EsMxCuerpoTooltip : "";
                Atributos.DataHtml = ($(element).hasAttr("data-html")) ? queryResult[i].EsMX : "";
                Atributos.PlaceHolder = ($(element).hasAttr("placeholder")) ? queryResult[i].EsMX : "";
                break;
            } else {
                Atributos.Value = ($(element).hasAttr("value")) ? queryResult[i].EnUS : "";
                Atributos.DataIntro = ($(element).hasAttr("data-intro")) ? queryResult[i].EnUsGuiame : "";
                Atributos.DataOriginalTitle = ($(element).hasAttr("data-original-title")) ? queryResult[i].EnUsGuiame : "";
                Atributos.DataContent = ($(element).hasAttr("data-content")) ? queryResult[i].EnUsCuerpoTooltip : "";
                Atributos.DataHtml = ($(element).hasAttr("data-html")) ? queryResult[i].EnUS : "";
                Atributos.PlaceHolder = ($(element).hasAttr("placeholder")) ? queryResult[i].EnUS : "";
                break;
            }
        }
    }
    return Atributos;
}

//SABER SI EXISTE UN ATRIBUTO EN UN TAG HTML
$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};

//FUNCION PARA REALIZAR CAMBIO DE LENGUAJE
CambioLenguaje = function () {
    if (localStorage.getItem("idioma") != null) {
        var idioma = localStorage.getItem("idioma");
        CambiarLenguaje(idioma);
    }
};

//FUNCION PARA DETERMINAR  EN QUE LENGUAJE  SE MOSTRARA  LA PAGINA WEB
CambiarLenguaje = function (lang) {
    //ASIGNACION EL TIPO DEL IDIOMA  A UN LOCALSTORAGE
    localStorage.setItem("idioma", lang);

    //REMOVER LAS CLASES  DE LOS OTROS ATRIBUTOS  Y AGREGAR EL SELECCIONADO POR DEFAULT
    $("i.eventIdioma").removeClass("fa fa-check");
    $("#idioma" + lang.toString()).addClass("fa fa-check");

    //CARGA EL LENGUAJE DE LOS ELEMENTOS DEL DOM
    CargarTextoDom();
}

//FUNCION PARA DETERMINAR  EN QUE LENGUAJE  SE MOSTRARA  LA PAGINA WEB DESDE LOGIN
CambiarLenguajeLogin = function (lang) {
    //ASIGNACION EL TIPO DEL IDIOMA  A UN LOCALSTORAGE
    localStorage.setItem("idioma", lang);

    //REMOVER LAS CLASES  DE LOS OTROS ATRIBUTOS  Y AGREGAR EL SELECCIONADO POR DEFAULT
    $("i.eventIdioma").removeClass("fa fa-check");
    $("#idioma" + lang.toString()).addClass("fa fa-check");

    window.location.reload();
    //CARGA EL LENGUAJE DE LOS ELEMENTOS DEL DOM
    CargarTextoDom();
}

// MENSAJE PARA TODAS LAS ACCIONES DEL SITIO WEB 
ControlMensaje = function (imagen, mensaje, segundos) {
    if (imagen == "Alerta") {
        $("#AlertaProceso").removeClass().addClass("alert alert-warning alert-dismissible");
        $("#IconoMensaje").removeClass().addClass("fa fa-exclamation-triangle");
    } else if (imagen == "Informativo") {
        $("#AlertaProceso").removeClass().addClass("alert alert-info alert-dismissible");
        $("#IconoMensaje").removeClass().addClass("fa fa-info-circle");
    } else if (imagen == "Cancelar") {
        $("#AlertaProceso").removeClass().addClass("alert alert-danger alert-dismissible");
        $("#IconoMensaje").removeClass().addClass("fa fa-ban");
    } else if (imagen == "Realizado") {
        $("#AlertaProceso").removeClass().addClass("alert alert-success alert-dismissible");
        $("#IconoMensaje").removeClass().addClass("fa fa-check-square");
    }

    $("#AlertaProceso").fadeIn("slow");
    $("#MensajeAlerta").text(mensaje);

    setTimeout(DesaparecerMensaje, (parseInt(segundos) * 1000));
}

// DESAPARECER EL MENSAJE MOSTRADO
DesaparecerMensaje = function () {
    $("#AlertaProceso").fadeOut("slow");
}

//NAVEGACION ENTRE PAGINAS
Navegacion = function (segundos, pagina) {
    $("body").fadeOut("slow");
    setTimeout(function () {
        location.href = pagina;
    }, (parseInt(segundos) * 1000));
}

//VALIDACION  SI ES NUMERO
EsNumero = function (Valor) {
    if (isNaN(Valor) === false) {
        return true;
    } else {
        return false;
    }
}

//VALIDACION SI ES UN CAMPO EMAIL
EsEmail = function (Valor) {
    var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
    return filter.test(Valor);
}

//VALIDACION EL CAMPO ESTA VACIO
EsNullOrEmpty = function (Valor) {
    if (Valor.length < 1) {
        return true;
    } else {
        return false;
    }
}

//VALIDACION SI EL CAMPO ES CADENA
EsString = function (Valor) {
    if (typeof Valor === 'string') {
        return true;
    } else {
        return false;
    }
}

//VALIDACION SI EL CAMPO ES BOLEANO
EsBoleano = function (Valor) {
    if (typeof Valor === 'boolean') {
        return true;
    } else {
        return false;
    }
}

//FUNCION PARA REGRESAR EL VALOR DE UN OBJETO HTML 
TraerValorCampo = function (id, atributo) {
    if ($("#" + id).hasAttr(atributo)) {
        return $("#" + id).attr(atributo);
    } else {
        return null;
    }
}

//TRAER EL USUARIO
TraerUsuario = function () {
    return (sessionStorage.getItem("usuario") != null) ? sessionStorage.getItem("usuario") : "";
}

//LIMPIA COMPINENTES DE FORMULARIO
LimpiarFormulario = function (formulario) {
    document.getElementById(formulario).reset();
}

//TRAER MODULO  DONDE ESTA EL USUARIO
TraerModulo = function () {
    return (sessionStorage.getItem("modulo") != null) ? sessionStorage.getItem("modulo") : "";
}

//EXPORTAR EN PDF 
ExportarPDF = function (grid) {
    $("#" + grid).data("kendoGrid").saveAsPDF();
}

//EXPORTAR EXCEL
ExportarExcel = function (grid) {
    $("#" + grid).data("kendoGrid").saveAsExcel();
}

//VALIDA SI EL NUMERO CAPTURADO ES NUMERICO
TecladoEsNumero = function (evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

//VERIFICA SESION MENU ROUTE
VerificaSesionRoute = function () {
    if (sessionStorage.getItem("usuario") == null) {
        return false;
    } else if (EsNullOrEmpty(sessionStorage.getItem("usuario"))) {
        return false;
    }
    return true;
}

//REGRESA LA FECHA ACTUAL EN FORMATO DD/MM/YYYY
FechaActual = function (elemento) {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //Enero es 0!

    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }
    var today = dd + '/' + mm + '/' + yyyy;

    document.getElementById(elemento).value = today;
}

//ASIGNA LA SECUENCIA OBTENIDA
Folio = function (elemento, parametro) {

    DatosFolio(parametro, function (Objeto) {
        document.getElementById(elemento).value = Objeto[0].NO_SECUENCIA;
    });

}

//REGRESA LA SECUENCIA SIGUIENTE QUE CORRESPONDE AL PARÁMETRO
DatosFolio = function (parametro, response) {
    $.ajax({
        type: "post",
        url: "ws/OperacionesGral.svc/ObtieneFolioSecuencia",
        data: JSON.stringify({ CL_SECUENCIA: parametro }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            response(data);
        }
    });
};

//SALIR DEL SISTEMA
Salir = function () {
    sessionStorage.clear();
    Navegacion(2, "login.html");
}

//VALIDAR RFC
ValidaRfc = function (rfcStr) {
    var strCorrecta;
    strCorrecta = rfcStr;
    if (strCorrecta.length == 12) {
        var valid = '^(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))';
    } else {
        var valid = '^(([A-Z]|[a-z]|\s){1})(([A-Z]|[a-z]){3})([0-9]{6})((([A-Z]|[a-z]|[0-9]){3}))';
    }
    var validRfc = new RegExp(valid);
    var matchArray = strCorrecta.match(validRfc);
    if (matchArray == null) {
        return false;
    }
    else {
        return true;
    }
}

//VALIDAR CURP
ValidaCurp = function (curp) {
    if (curp.match(/^([a-z]{4})([0-9]{6})([a-z]{6})([0-9]{2})$/i)) {
        return true;
    } else {
        return false;
    }
}

