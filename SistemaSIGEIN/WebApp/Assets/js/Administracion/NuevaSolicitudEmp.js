app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);

app.service('fileUpload', ['$http', function ($http) {
    this.uploadFileToUrl = function (file, uploadUrl) {
        var fd = new FormData();
        fd.append('file', file);
        $http.post(uploadUrl, fd, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
        .success(function () {
        })
        .error(function () {
        });
    }
}]);

app.controller('K_SOLICITUDController', function ($scope, $http, $modal, fileUpload) {

    //habilita la animacion del popup para que se despliegue de arriba para abajo  
    $scope.animationsEnabled = true;

    //TEXTOS DESDE CONFIGURACION
    $scope.modulo = tema.cssClass;
    $scope.OBLIGATORIO = K_SOLICITUD.OBLIGATORIO;
    $scope.TAB1 = K_SOLICITUD.TABULADOR1;
    $scope.TAB2 = K_SOLICITUD.TABULADOR2;
    $scope.TAB3 = K_SOLICITUD.TABULADOR3;
    $scope.TAB4 = K_SOLICITUD.TABULADOR4;
    $scope.TAB5 = K_SOLICITUD.TABULADOR5;
    $scope.TAB6 = K_SOLICITUD.TABULADOR6;
    $scope.TAB7 = K_SOLICITUD.TABULADOR7;

    $scope.SAVE = Config.BOTONGUARDAR;
    $scope.CANCEL = Config.BOTONCANCELAR;
    $scope.ADD = Config.BOTONAGREGAR;
    $scope.EDIT = Config.BOTONMODIFICAR;
    $scope.DELETE = Config.BOTONELIMINAR;

    $scope.YES = " " + Config.BOTONSI;
    $scope.NO = " " + Config.BOTONNO;

    $scope.DE = K_SOLICITUD.DE;
    $scope.A = K_SOLICITUD.A;
    $scope.SELECCIONAR = Config.SELECCIONAR;

    $scope.p_NB_PARIENTE = K_SOLICITUD.NB_PARIENTE;
    $scope.p_CL_PARENTEZCO = K_SOLICITUD.CL_PARENTEZCO;
    $scope.p_FE_NACIMIENTO = K_SOLICITUD.FE_NACIMIENTO;
    $scope.p_CL_OCUPACION = K_SOLICITUD.CL_OCUPACION;
    $scope.p_FG_DEPENDIENTE = K_SOLICITUD.FG_DEPENDIENTE;


    //////////////////LOS SIGUIENTES PARAMETROS VENDRÁN DESDE CONFIGURACIÓN DEL SISTEMA
    $scope.ID_NIVEL_PROFESIONAL = 8;
    $scope.ID_NIVEL_POSGRADO = 10;

    $(document).ready(function () {
        $("#tbsNuevaSolicitud").on("keydown", function (e) {
            if (e.keyCode == kendo.keys.DOWN || e.keyCode == kendo.keys.UP) {
                e.stopImmediatePropagation();
                e.preventDefault();
                var visibleContainer = $(e.target).data("kendoTabStrip").wrapper.children(".k-content").filter(":visible");
                if (e.keyCode == kendo.keys.DOWN) {
                    visibleContainer.scrollTop(visibleContainer.scrollTop() + 50);
                } else {
                    visibleContainer.scrollTop(visibleContainer.scrollTop() - 50);
                }
            }
        });
    });


    $scope.imagenesDefault = function () {

        if ($scope.UserPhoto != undefined) {
            $scope.photoDefault = $scope.UserPhoto;
        }
        else {
            $scope.photoDefault = "Assets/images/no-image.png";
        }

        if ($scope.EmpresaPhoto != undefined) {
            $scope.logoDefault = $scope.EmpresaPhoto;
        }
        else {
            $scope.logoDefault = "Assets/images/Logo.png";
        }
    };

    $scope.imagenesDefault();

    $scope.Configuracion = function () {
        FechaActual("txtFechaSolicitud");
        // Folio("txtFolioSolicitud", "K_SOLICITUD");
    };

    //PREVISUALIZAR IMAGEN A INSERTAR
    $scope.readURL = function (input) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#img1').attr('src', e.target.result);

                var canvas = document.createElement("canvas");
                var imageElement = document.createElement("img");

                imageElement.setAttribute('src', e.target.result);

                canvas.width = imageElement.width;
                canvas.height = imageElement.height;
                var context = canvas.getContext("2d");
                context.drawImage(imageElement, 0, 0);
                var base64Image = canvas.toDataURL();
                $scope.UserPhoto = base64Image.replace(/data:image\/jpeg;base64,/, '')
            }
            reader.readAsDataURL(input.files[0]);
        }
    };

    $("#archFtografia").change(function () {
        $scope.readURL(this);
    });

    //BOTON UPLOAD FOTOGRAFÍA
    $(":file").filestyle({ buttonBefore: true, buttonText: K_SOLICITUD.EXAMINAR });

    //LIMPIAR FILE
    $scope.borrarFile = function () {
        $('#img1').attr('src', '');
        $(":file").filestyle('clear');
    };

    //GUARDAR IMAGEN 

    $scope.adjuntaFotografia = function () { //en caso de que le den click en aceptar generara lo siguiente  
        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
        if ($scope.ID_DOCUMENTO != undefined)  //se verifica que la clave primaria sea diferente de nulo  
            tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  

        var Obj_Documento = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_DOCUMENTO: "FOTO"
 			, CL_TIPO_RUTA: "FILETABLE"
 			, FG_ACTIVO: true
 			, NB_DOCUMENTO: $scope.NB_DOCUMENTO
            , ID_DOCUMENTO: $scope.ID_DOCUMENTO
        };

        console.log("datos " + $scope.UserPhoto);

        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_DOCUMENTO",
            method: "POST",
            data: {
                V_C_DOCUMENTO: Obj_Documento,
                usuario: TraerUsuario(),
                programa: "InsertaDocumento.html",
                tipo_transaccion: tipoOperacion,
                archivo: $scope.UserPhoto,
                ruta: "FOTO"
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

    ///////////////////////////////////////////////////////////////////////////////////////// DATOS DE COMBOS EN NUEVA SOLICITUD
    $scope.cargarPais = [
        { NB_PAIS: "México" }
    ];


    $scope.paisOptions = {
        dataSource: $scope.cargarPais,
        dataTextField: "NB_PAIS", //texto a mostrar el combo cuando se seleccione
        dataValueField: "NB_PAIS", //valor del combo cuando se seleccione
        filter: "contains",
        value: "México"
    };

    $scope.cargarEstados =
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_C_ESTADO"
                        , dataType: "json"
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        };

    $scope.customOptions_ESTADOS = {
        dataSource: $scope.cargarEstados,
        dataTextField: "NB_ESTADO", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_ESTADO", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.customOptions_MUNICIPIO = {
        dataSource: $scope.DS_MUNICIPIOS,
        dataTextField: "NB_MUNICIPIO", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_MUNICIPIO", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.DS_MUNICIPIOS = //new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_C_MUNICIPIO"
                        , dataType: "json"
                        , data: JSON.stringify({ CL_ESTADO: 'none' })
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        };

    $scope.CargaMunicipios = function (PARAMETRO_CL_ESTADO) {
        //se obtiene el listado de MUNICIPIOS FILTRADO POR CL_ESTADO
        $scope.DS_MUNICIPIOS = //new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_C_MUNICIPIO"
                        , dataType: "json"
                        , data: JSON.stringify({ CL_ESTADO: PARAMETRO_CL_ESTADO })
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        };

        var txt_municipio = $("#txt_MUNICIPIO").data("kendoDropDownList");
        txt_municipio.setDataSource($scope.DS_MUNICIPIOS);
        txt_municipio.value("");
        txt_municipio.refresh();
        $scope.CL_MUNICIPIO = '';
        $scope.CL_COLONIA = '';
    };

    $scope.customOptions_COLONIA = {
        dataSource: $scope.DS_COLONIAS,
        dataTextField: "NB_COLONIA", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_COLONIA", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.DS_COLONIAS = //new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_C_COLONIA"
                        , dataType: "json"
                        , data: JSON.stringify({ CL_MUNICIPIO: 'none' })
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        };

    $scope.CargaColonias = function (PARAMETRO_CL_COLONIA) {
        //se obtiene el listado de MUNICIPIOS FILTRADO POR CL_ESTADO
        if (PARAMETRO_CL_COLONIA == undefined) {
            return false;
        }

        $scope.DS_COLONIAS = //new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_C_COLONIA"
                        , dataType: "json"
                        , data: JSON.stringify({ CL_MUNICIPIO: PARAMETRO_CL_COLONIA })
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        };

        var txt_colonia = $("#txtColonia").data("kendoDropDownList");
        txt_colonia.setDataSource($scope.DS_COLONIAS);
        txt_colonia.value("");
        txt_colonia.refresh();
        $scope.CL_COLONIA = '';

    };

    $scope.CargaCodigoPostal = function (PARAMETRO_CL_POSTAL) {
        //se obtiene el listado de MUNICIPIOS FILTRADO POR CL_ESTADO

        if (PARAMETRO_CL_POSTAL == undefined) {
            return false;
        }

        $scope.traeCodigo = function () {

            $scope.DatosFolio(PARAMETRO_CL_POSTAL, function (Objeto) {
                if (Objeto.length != 0) {
                    $scope.CL_CODIGO_POSTAL = Number(Objeto[0].CL_CODIGO_POSTAL);
                }
            });

        }

        $scope.DatosFolio = function (param, response) {

            if (param == undefined) {
                return false;
            }

            $.ajax({
                type: "post",
                url: "ws/OperacionesGral.svc/Get_C_COLONIA",
                data: JSON.stringify({ CL_COLONIA: param }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response(data);
                }
            });
        };

        $scope.traeCodigo();
    };

    $scope.cargarDatosLocalizacion = function (PARAMETRO_CL_POSTAL) {

        if (PARAMETRO_CL_POSTAL == undefined) {
            return false;
        }

        $scope.traeCodigo = function () {

            $scope.DatosFolio(PARAMETRO_CL_POSTAL, function (Objeto) {
                if (Objeto.length != 0) {
                    $scope.CL_CODIGO_POSTAL = Number(Objeto[0].CL_CODIGO_POSTAL);
                    $scope.CL_PAIS = Objeto[0].CL_PAIS;
                    $scope.CL_ESTADO = Objeto[0].CL_ESTADO;
                    $scope.CL_MUNICIPIO = Objeto[0].CL_MUNICIPIO;

                    var txt_pais = $("#txtPais").data("kendoDropDownList");
                    txt_pais.value($scope.CL_PAIS);
                    txt_pais.text($scope.CL_PAIS);
                    txt_pais.refresh();

                    var txt_estado = $("#txt_ESTADOS").data("kendoDropDownList");
                    txt_estado.value($scope.CL_ESTADO);
                    txt_estado.text($scope.NB_ESTADO);
                    txt_estado.refresh();

                    var txt_municipio = $("#txt_MUNICIPIO").data("kendoDropDownList");
                    txt_municipio.setDataSource($scope.DS_MUNICIPIOS_CP);
                    txt_municipio.value($scope.CL_MUNICIPIO);
                    txt_municipio.text($scope.NB_MUNICIPIO);
                    txt_estado.refresh();

                    var txt_colonia = $("#txtColonia").data("kendoDropDownList");
                    txt_colonia.setDataSource($scope.DS_COLONIAS_CP);
                    txt_colonia.value("");
                    txt_colonia.text("");
                    txt_colonia.refresh();
                    $scope.CL_COLONIA = '';
                }
            });

        };

        $scope.DS_MUNICIPIOS_CP = //new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_C_MUNICIPIO"
                        , dataType: "json"
                        , data: JSON.stringify({ CL_ESTADO: $scope.CL_ESTADO })
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        };

        $scope.DS_COLONIAS_CP = //new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_C_COLONIA"
                        , dataType: "json"
                        , data: JSON.stringify({ CL_CODIGO_POSTAL: PARAMETRO_CL_POSTAL })
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        };

        $scope.DatosFolio = function (param, response) {

            if (param == undefined) {
                return false;
            }

            $.ajax({
                type: "post",
                url: "ws/OperacionesGral.svc/Get_Colonia_CP",
                data: JSON.stringify({ CL_CODIGO_POSTAL: param }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response(data);
                }
            });
        };

        $scope.traeCodigo();
    };

    //Plantillas
    var plantillas = [
        { CL_PLANTILLA: "General", NB_PLANTILLA: "General" }
    ];

    $scope.plantillaOptions = {
        dataSource: plantillas,
        dataTextField: "NB_PLANTILLA", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_PLANTILLA", //valor del combo cuando se seleccione
        filter: "contains"
    };

    var data = [
        { CL_GENERO: "F", DS_GENERO: K_SOLICITUD.GENFEMENINO },
        { CL_GENERO: "M", DS_GENERO: K_SOLICITUD.GENMASCULINO }
    ];

    $scope.generoOptions = {
        dataSource: data,
        dataTextField: "DS_GENERO", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_GENERO", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.estadosCiviles = new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_C_CATALOGO_VALOR"
                        , data: JSON.stringify({ ID_CATALOGO_LISTA: 1 })
                        , dataType: "json"
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        });

    $scope.estadosCivilesOptions = {
        dataSource: $scope.estadosCiviles,
        dataTextField: "NB_CATALOGO_VALOR", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_CATALOGO_VALOR", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.nacionalidad = [
        { name: Config.NACIONALIDAD },
        { name: Config.OTRA }
    ];

    $scope.nacionalidadOptions = {
        dataSource: $scope.nacionalidad,
        dataTextField: "name", //texto a mostrar el combo cuando se seleccione
        dataValueField: "name", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.estadosLicencia = new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_VW_TIPO_LICENCIA"
                        , data: JSON.stringify({ ID_CATALOGO_LISTA: 1 })
                        , dataType: "json"
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        });

    $scope.estadosLicenciaOptions = {
        dataSource: $scope.estadosLicencia,
        dataTextField: "NB_LICENCIA", //texto a mostrar el combo cuando se seleccione
        dataValueField: "NB_LICENCIA", //valor del combo cuando se seleccione
        filter: "contains"
    };

    //////////////////////////////////////////////////////////////////////////////////////////// COMBOS DATOS FAMILIARES

    $scope.estadosParentezco = new kendo.data.DataSource(
        {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "post"
                        , url: "ws/OperacionesGral.svc/Get_VW_PARENTEZCO"
                        , data: JSON.stringify({ ID_CATALOGO_LISTA: 1 })
                        , dataType: "json"
                        , contentType: "application/json; charset=utf-8"
                        , success: function (result) {
                            options.success(result);
                        }
                    });
                }
            }
        });

    $scope.estadosParentezcoOptions = {
        dataSource: $scope.estadosParentezco,
        dataTextField: "NB_PARENTEZCO", //texto a mostrar el combo cuando se seleccione
        dataValueField: "NB_PARENTEZCO", //valor del combo cuando se seleccione
        filter: "contains"
    };

    /////////////////////////////////////////////////////////////////////////////////////// COMBOS FORMACIÓN ACADÉMICA

    $scope.cargarMeses = {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_VW_OBTIENE_MESES"
                    , dataType: "json"
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        options.success(result);
                    }
                });
            }
        }
    };

    $scope.mesesOptions = {
        dataSource: $scope.cargarMeses,
        dataTextField: "MES", //texto a mostrar el combo cuando se seleccione
        dataValueField: "MES", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.selectAnio = "--";
    $scope.selectMes = "--";

    $scope.cargarAnios = {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_OBTIENE_ANIO"
                    , dataType: "json"
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        options.success(result);
                    }
                });
            }
        }
    };

    $scope.aniosOptions = {
        dataSource: $scope.cargarAnios,
        dataTextField: "list_year", //texto a mostrar el combo cuando se seleccione
        dataValueField: "list_year", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.datosProfesional = new kendo.data.DataSource(
    {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_C_ESCOLARIDAD"
                    , data: JSON.stringify({ ID_NIVEL_ESCOLARIDAD: $scope.ID_NIVEL_PROFESIONAL })
                    , dataType: "json"
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        options.success(result);
                    }
                });
            }
        }
    });

    $scope.profesionalOptions = {
        dataSource: $scope.datosProfesional,
        dataTextField: "NB_ESCOLARIDAD", //texto a mostrar el combo cuando se seleccione
        dataValueField: "ID_ESCOLARIDAD", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.datosPosgrado = new kendo.data.DataSource(
    {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_C_ESCOLARIDAD"
                    , data: JSON.stringify({ ID_NIVEL_ESCOLARIDAD: $scope.ID_NIVEL_POSGRADO })
                    , dataType: "json"
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        options.success(result);
                    }
                });
            }
        }
    });

    $scope.posgradoOptions = {
        dataSource: $scope.datosPosgrado,
        dataTextField: "NB_ESCOLARIDAD", //texto a mostrar el combo cuando se seleccione
        dataValueField: "ID_ESCOLARIDAD", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.cargarPorcentaje =
    {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_VW_OBTIENE_PORCENTAJES_IDIOMAS"
                    , dataType: "json"
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        options.success(result);
                    }
                });
            }
        }
    };

    $scope.porcentajeIdiomaOptions = {
        dataSource: $scope.cargarPorcentaje,
        dataTextField: "NB_PORCENTAJE", //texto a mostrar el combo cuando se seleccione
        dataValueField: "CL_PORCENTAJE", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.cargarExamen = new kendo.data.DataSource(
    {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_C_CATALOGO_VALOR"
                    , data: JSON.stringify({ ID_CATALOGO_LISTA: 4 })
                    , dataType: "json"
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        options.success(result);
                    }
                });
            }
        }
    });

    $scope.examenOptions = {
        dataSource: $scope.cargarExamen,
        dataTextField: "CL_CATALOGO_VALOR", //texto a mostrar el combo cuando se seleccione
        dataValueField: "ID_CATALOGO_VALOR", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.caragarIdiomas = new kendo.data.DataSource(
    {
        type: "json",
        transport: {
            read: function (options) {
                $.ajax({
                    type: "post"
                    , url: "ws/OperacionesGral.svc/Get_C_IDIOMA"
                    , dataType: "json"
                    , contentType: "application/json; charset=utf-8"
                    , success: function (result) {
                        options.success(result);
                    }
                });
            }
        }
    });

    $scope.idiomaOptions = {
        dataSource: $scope.caragarIdiomas,
        dataTextField: "NB_IDIOMA", //texto a mostrar el combo cuando se seleccione
        dataValueField: "ID_IDIOMA", //valor del combo cuando se seleccione
        filter: "contains"
    };

    $scope.eligeNacionalidad = function () {
        if ($scope.S_NACIONALIDAD == "Otra") {
            $('#txtNacionalidad').attr('disabled', false);
        } else {
            $('#txtNacionalidad').attr('disabled', true);
        }
    };

    //////////////////////////////////////////////////////////////////////////////////////////// CARGAR INFORMACIÓN PERSONAL

    // Carga los datos de la solicitud
    $scope.CargaSolicitud = function () {

        $scope.ID_CANDIDATO = 3;

        $scope.Candidato = function () {

            $scope.datosCandidato($scope.ID_CANDIDATO, function (Objeto) {

                if (Objeto.length > 0) {

                    for (i = 0; i < Objeto.length; i++) {
                        var fecha = new Date(parseInt(Objeto[i].FE_NACIMIENTO.substr(6)));
                        Objeto[i].FE_NACIMIENTO = fecha;
                    }

                    $scope.CL_CARTILLA_MILITAR = Objeto[0].CL_CARTILLA_MILITAR;
                    $scope.CL_CEDULA_PROFESIONAL = Objeto[0].CL_CEDULA_PROFESIONAL;
                    $scope.CL_CODIGO_POSTAL = parseInt(Objeto[0].CL_CODIGO_POSTAL);
                    $scope.CL_CORREO_ELECTRONICO = Objeto[0].CL_CORREO_ELECTRONICO;
                    $scope.CL_CURP = Objeto[0].CL_CURP;
                    $scope.CL_DISPONIBILIDAD_VIAJE = Objeto[0].CL_DISPONIBILIDAD_VIAJE;
                    $scope.CL_ESTADO_CIVIL = Objeto[0].CL_ESTADO_CIVIL;
                    $scope.CL_GENERO = Objeto[0].CL_GENERO;
                    $scope.CL_NACIONALIDAD = Objeto[0].CL_NACIONALIDAD;
                    $scope.CL_NSS = Objeto[0].CL_NSS || undefined;
                    $scope.CL_RFC = Objeto[0].CL_RFC;
                    $scope.CL_TIPO_SANGUINEO = Objeto[0].CL_TIPO_SANGUINEO;
                    $scope.DS_COMENTARIO = Objeto[0].DS_COMENTARIO;
                    $scope.DS_DISPONIBILIDAD = Objeto[0].DS_DISPONIBILIDAD;
                    $scope.DS_LUGAR_NACIMIENTO = Objeto[0].DS_LUGAR_NACIMIENTO;
                    $scope.DS_NACIONALIDAD = Objeto[0].DS_NACIONALIDAD;
                    $scope.DS_VEHICULO = Objeto[0].DS_VEHICULO;
                    $scope.FE_NACIMIENTO = Objeto[0].FE_NACIMIENTO;
                    $scope.FG_ACTIVO = Objeto[0].FG_ACTIVO,
                    $scope.ID_CANDIDATO = Objeto[0].ID_CANDIDATO;
                    $scope.MN_SUELDO = Objeto[0].MN_SUELDO;
                    $scope.NB_APELLIDO_MATERNO = Objeto[0].NB_APELLIDO_MATERNO;
                    $scope.NB_APELLIDO_PATERNO = Objeto[0].NB_APELLIDO_PATERNO;
                    $scope.NB_CALLE = Objeto[0].NB_CALLE;
                    $scope.NB_CANDIDATO = Objeto[0].NB_CANDIDATO;
                    $scope.NB_COLONIA = Objeto[0].NB_COLONIA;
                    $scope.NB_CONYUGUE = Objeto[0].NB_CONYUGUE;
                    $scope.NB_ESTADO = Objeto[0].NB_ESTADO;
                    $scope.NB_LICENCIA = Objeto[0].NB_LICENCIA;
                    $scope.NB_MUNICIPIO = Objeto[0].NB_MUNICIPIO;
                    $scope.NB_PAIS = Objeto[0].NB_PAIS;
                    $scope.NO_EXTERIOR = parseInt(Objeto[0].NO_EXTERIOR);
                    $scope.NO_INTERIOR = Objeto[0].NO_INTERIOR;
                    $scope.XML_EGRESOS = Objeto[0].XML_EGRESOS;
                    $scope.XML_INGRESOS = Objeto[0].XML_INGRESOS;
                    $scope.XML_PATRIMONIO = Objeto[0].XML_PATRIMONIO;
                    $scope.XML_PERFIL_RED_SOCIAL = Objeto[0].XML_PERFIL_RED_SOCIAL;
                    $scope.XML_TELEFONOS = Objeto[0].XML_TELEFONOS;

                    if ($scope.CL_CODIGO_POSTAL != undefined) {
                        $scope.cargarDatosLocalizacion($scope.CL_CODIGO_POSTAL);
                    } else if ($scope.CL_ESTADO != undefined) {
                        $scope.CargaMunicipios($scope.CL_ESTADO);
                    }

                    if ($scope.CL_NACIONALIDAD == "Mexicana") {
                        $scope.S_NACIONALIDAD = $scope.CL_NACIONALIDAD;
                    }
                    else {
                        $scope.eligeNacionalidad();
                        $scope.S_NACIONALIDAD = "Otra";
                        $scope.M_NACIONALIDAD = $scope.CL_NACIONALIDAD;
                    }
                }
            });
        };

        $scope.datosCandidato = function (param, response) {

            if (param == undefined) {
                return false;
            }

            $.ajax({
                type: "post",
                url: "ws/OperacionesGral.svc/Get_C_CANDIDATO",
                data: JSON.stringify({ ID_CANDIDATO: param }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response(data);
                }
            });
        };

        $scope.Candidato();

    };

    $scope.CargaSolicitud();

    // Regresa los datos de la solicitud
    $scope.DatosSolicitud = function (parametro, response) {

        $.ajax({
            type: "post",
            url: "ws/OperacionesGral.svc/Get_C_CANDIDATO",
            data: JSON.stringify({ ID_CANDIDATO: parametro }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                response(data);
            }
        });
    };

    $scope.Configuracion();

    //////////////////////////////////////////////////////////////////////////////////////////// CARGAR DATOS FAMILIARES

    // Carga Tabla de Parientes
    $scope.cargarParientes = function () {

        $scope.ID_CANDIDATO = 3;

        if ($scope.ID_CANDIDATO == undefined) {
            return false;
        }

        $scope.functionParientes = function () {
            $scope.traerDatosPariente($scope.ID_CANDIDATO, function (Objeto) {
                if (Objeto.length != 0) {
                    //Convertir fecha
                    for (i = 0; i < Objeto.length; i++) {
                        var fecha = new Date(parseInt(Objeto[i].FE_NACIMIENTO.substr(6)));
                        var dd = fecha.getDate();
                        var mm = fecha.getMonth() + 1;
                        var yyyy = fecha.getFullYear();
                        if (dd < 10) { dd = '0' + dd }
                        if (mm < 10) { mm = '0' + mm }
                        var fecha = dd + '/' + mm + '/' + yyyy;
                        Objeto[i].FE_NACIMIENTO = fecha;
                    }

                    $scope.familiar = undefined;
                    $scope.familiar = Objeto;
                }
            });
        };

        $scope.traerDatosPariente = function (param, response) {

            if (param == undefined) {
                return false;
            }

            $.ajax({
                type: "post",
                url: "ws/OperacionesGral.svc/Get_C_PARIENTE",
                data: JSON.stringify({ ID_CANDIDATO: param }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response(data);
                }
            });
        };

        $scope.functionParientes();

    };

    $scope.cargarParientes();

    /////////////////////////////////////////////////////////////////////////////////////////// CARGAR FORMACIÓN ACADÉMICA

    // Cargar vaolores de estudios concluidos

    if ($scope.CL_ESTADO_ESCOLARIDAD_PRIMARIA == undefined) {
        $scope.CL_ESTADO_ESCOLARIDAD_PRIMARIA = "Si";
    }

    if ($scope.CL_ESTADO_ESCOLARIDAD_SECUNDARIA == undefined) {
        $scope.CL_ESTADO_ESCOLARIDAD_SECUNDARIA = "Si";
    }

    if ($scope.CL_ESTADO_ESCOLARIDAD_PREPA == undefined) {
        $scope.CL_ESTADO_ESCOLARIDAD_PREPA = "Si";
    }

    if ($scope.CL_ESTADO_ESCOLARIDAD_UNI1 == undefined) {
        $scope.CL_ESTADO_ESCOLARIDAD_UNI1 = "Si";
    }

    if ($scope.CL_ESTADO_ESCOLARIDAD_UNI2 == undefined) {
        $scope.CL_ESTADO_ESCOLARIDAD_UNI2 = "Si";
    }

    if ($scope.CL_ESTADO_ESCOLARIDAD_POS1 == undefined) {
        $scope.CL_ESTADO_ESCOLARIDAD_POS1 = "Si";
    }

    if ($scope.CL_ESTADO_ESCOLARIDAD_POS2 == undefined) {
        $scope.CL_ESTADO_ESCOLARIDAD_POS2 = "Si";
    }


    // Carga tabla de Idiomas
    $scope.cargarIdiomas = function () {

        $scope.ID_CANDIDATO = 3;
        if ($scope.ID_CANDIDATO == undefined) {
            return false;
        }

        $scope.functionIdiomas = function () {
            $scope.traerDatosIdiomas($scope.ID_CANDIDATO, function (Objeto) {
                if (Objeto.length != 0) {

                    $scope.idiomas = undefined;
                    $scope.idiomas = Objeto;
                }
            });
        };

        $scope.traerDatosIdiomas = function (param, response) {

            if (param == undefined) {
                return false;
            }

            $.ajax({
                type: "post",
                url: "ws/OperacionesGral.svc/Get_C_EMPLEADO_IDIOMA",
                data: JSON.stringify({ ID_CANDIDATO: param }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response(data);
                }
            });
        };

        $scope.functionIdiomas();
    };

    $scope.cargarIdiomas();

    $scope.cargarEscolaridad = function () {

        $scope.functionEscolaridad = function (cadena) {
            $scope.traerDatosEsc(cadena, function (Objeto) {

                if (cadena == "PRIMARIA") {
                    $scope.ID_PRIMARIA = Objeto[0].ID_ESCOLARIDAD;
                } else if (cadena == "SECUNDARIA") {
                    $scope.ID_SECUNDARIA = Objeto[0].ID_ESCOLARIDAD;
                } else if (cadena == "PREPARATORIA") {
                    $scope.ID_PREPARATORIA = Objeto[0].ID_ESCOLARIDAD;
                }

                return Objeto[0].ID_ESCOLARIDAD;
            });
        };

        $scope.traerDatosEsc = function (param, response) {
            $.ajax({
                type: "post",
                url: "ws/OperacionesGral.svc/Get_C_ESCOLARIDAD",
                data: JSON.stringify({ NB_ESCOLARIDAD: param }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response(data);
                }
            });
        };

        $scope.NB_NIVEL_PRIMARIA = "PRIMARIA";
        $scope.NB_NIVEL_SECUNDARIA = "SECUNDARIA";
        $scope.NB_NIVEL_PREPARATORIA = "PREPARATORIA";

        $scope.ID_PRIMARIA = $scope.functionEscolaridad($scope.NB_NIVEL_PRIMARIA);
        $scope.ID_SECUNDARIA = $scope.functionEscolaridad($scope.NB_NIVEL_SECUNDARIA);
        $scope.ID_PREPARATORIA = $scope.functionEscolaridad($scope.NB_NIVEL_PREPARATORIA);
    };

    $scope.obtieneEscolaridadCandidato = function () {

        $scope.functionEscCandidato = function () {
            $scope.datosEscolaridad($scope.ID_CANDIDATO, function (Objeto) {

                for (i = 0; i < Objeto.length; i++) {
                    var fecha = new Date(parseInt(Objeto[i].FE_PERIODO_INICIO.substr(6)));
                    var dd = fecha.getDate();
                    var mm = fecha.getMonth() + 1;
                    var yyyy = fecha.getFullYear();
                    Objeto[i].FE_PERIODO_INICIO = yyyy;

                    var fecha2 = new Date(parseInt(Objeto[i].FE_PERIODO_FIN.substr(6)));
                    var dd2 = fecha2.getDate();
                    var mm2 = fecha2.getMonth() + 1;
                    var yyyy2 = fecha2.getFullYear();
                    Objeto[i].FE_PERIODO_FIN = yyyy2;
                }

                var PRIMARIA = $.grep(Objeto, function (e) { return e.CL_NIVEL_ESCOLARIDAD == "PRIMARIA" });
                var SECUNDARIA = $.grep(Objeto, function (e) { return e.CL_NIVEL_ESCOLARIDAD == "SECUNDARIA" });
                var PREPARATORIA = $.grep(Objeto, function (e) { return e.CL_NIVEL_ESCOLARIDAD == "PREPARATORIA" });
                var UNI = $.grep(Objeto, function (e) { return e.CL_NIVEL_ESCOLARIDAD == "PROFESIONAL" });
                var POS = $.grep(Objeto, function (e) { return e.CL_NIVEL_ESCOLARIDAD == "POSGRADO" });


                if (PRIMARIA.length > 0) {
                    $scope.CL_ESTADO_ESCOLARIDAD_PRIMARIA = PRIMARIA[0].CL_ESTADO_ESCOLARIDAD;
                    $scope.FE_TERMINO_PRIMARIA = PRIMARIA[0].FE_PERIODO_FIN;
                    $scope.FE_INICIO_PRIMARIA = PRIMARIA[0].FE_PERIODO_INICIO;
                    $scope.ID_EMPLEADO_ESCOLARIDAD_PRIMARIA = PRIMARIA[0].ID_EMPLEADO_ESCOLARIDAD;
                    $scope.ID_PRIMARIA = PRIMARIA[0].ID_ESCOLARIDAD;
                    $scope.NB_INSTITUCION_PRIMARIA = PRIMARIA[0].NB_INSTITUCION;
                }

                if (SECUNDARIA.length > 0) {
                    $scope.CL_ESTADO_ESCOLARIDAD_SECUNDARIA = SECUNDARIA[0].CL_ESTADO_ESCOLARIDAD;
                    $scope.FE_TERMINO_SECUNDARIA = SECUNDARIA[0].FE_PERIODO_FIN;
                    $scope.FE_INICIO_SECUNDARIA = SECUNDARIA[0].FE_PERIODO_INICIO;
                    $scope.ID_EMPLEADO_ESCOLARIDAD_SECUNDARIA = SECUNDARIA[0].ID_EMPLEADO_ESCOLARIDAD;
                    $scope.ID_SECUNDARIA = SECUNDARIA[0].ID_ESCOLARIDAD;
                    $scope.NB_INSTITUCION_SECUNDARIA = SECUNDARIA[0].NB_INSTITUCION;
                }

                if (PREPARATORIA.length > 0) {
                    $scope.CL_ESTADO_ESCOLARIDAD_PREPA = PREPARATORIA[0].CL_ESTADO_ESCOLARIDAD;
                    $scope.FE_TERMINO_PREPA = PREPARATORIA[0].FE_PERIODO_FIN;
                    $scope.FE_INICIO_PREPA = PREPARATORIA[0].FE_PERIODO_INICIO;
                    $scope.ID_EMPLEADO_ESCOLARIDAD_PREPARATORIA = PREPARATORIA[0].ID_EMPLEADO_ESCOLARIDAD;
                    $scope.ID_PREPARATORIA = PREPARATORIA[0].ID_ESCOLARIDAD;
                    $scope.NB_INSTITUCION_PREPARATORIA = PREPARATORIA[0].NB_INSTITUCION;
                }

                if (UNI.length > 0) {
                    $scope.CL_ESTADO_ESCOLARIDAD_UNI1 = UNI[0].CL_ESTADO_ESCOLARIDAD;
                    $scope.FE_TERMINO_UNIVERSIDAD1 = UNI[0].FE_PERIODO_FIN;
                    $scope.FE_INICIO_UNIVERSIDAD1 = UNI[0].FE_PERIODO_INICIO;
                    $scope.ID_EMPLEADO_ESCOLARIDAD_UNI1 = UNI[0].ID_EMPLEADO_ESCOLARIDAD;
                    $scope.CL_INSTITUCION_UNI1 = UNI[0].ID_ESCOLARIDAD;
                    $scope.NB_INSTITUCION_UNI1 = UNI[0].NB_INSTITUCION;
                }

                if (UNI.length > 1) {
                    $scope.CL_ESTADO_ESCOLARIDAD_UNI2 = UNI[1].CL_ESTADO_ESCOLARIDAD;
                    $scope.FE_TERMINO_UNIVERSIDAD2 = UNI[1].FE_PERIODO_FIN;
                    $scope.FE_INICIO_UNIVERSIDAD2 = UNI[1].FE_PERIODO_INICIO;
                    $scope.ID_EMPLEADO_ESCOLARIDAD_UNI1 = UNI[1].ID_EMPLEADO_ESCOLARIDAD;
                    $scope.CL_INSTITUCION_UNI2 = UNI[1].ID_ESCOLARIDAD;
                    $scope.NB_INSTITUCION_UNI2 = UNI[1].NB_INSTITUCION;
                }

                if (POS.length > 0) {
                    $scope.CL_ESTADO_ESCOLARIDAD_POS1 = POS[0].CL_ESTADO_ESCOLARIDAD;
                    $scope.FE_TERMINO_POS1 = POS[0].FE_PERIODO_FIN;
                    $scope.FE_INICIO_POS1 = POS[0].FE_PERIODO_INICIO;
                    $scope.ID_EMPLEADO_ESCOLARIDAD_POS1 = POS[0].ID_EMPLEADO_ESCOLARIDAD;
                    $scope.CL_INSTITUCION_POS1 = POS[0].ID_ESCOLARIDAD;
                    $scope.NB_INSTITUCION_POS1 = POS[0].NB_INSTITUCION;
                }

                if (POS.length > 1) {
                    $scope.CL_ESTADO_ESCOLARIDAD_POS2 = POS[1].CL_ESTADO_ESCOLARIDAD;
                    $scope.FE_TERMINO_POS2 = POS[1].FE_PERIODO_FIN;
                    $scope.FE_INICIO_POS2 = POS[1].FE_PERIODO_INICIO;
                    $scope.ID_EMPLEADO_ESCOLARIDAD_POS2 = POS[1].ID_EMPLEADO_ESCOLARIDAD;
                    $scope.CL_INSTITUCION_POS2 = POS[1].ID_ESCOLARIDAD;
                    $scope.NB_INSTITUCION_POS2 = POS[1].NB_INSTITUCION;
                }

            });
        };

        $scope.datosEscolaridad = function (param, response) {
            $.ajax({
                type: "post",
                url: "ws/OperacionesGral.svc/Get_C_EMPLEADO_ESCOLARIDAD",
                data: JSON.stringify({ ID_CANDIDATO: $scope.ID_CANDIDATO }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response(data);
                }
            });
        };

        $scope.functionEscCandidato();
    };

    $scope.obtieneEscolaridadCandidato();
    $scope.cargarEscolaridad();

    /////////////////////////////////////////////////////////////////////////////////////////// CARGAR EXPERIENCIA LABORAL

    $scope.obtieneExperienciaLaboral = function () {

        $scope.cargarExperienciaLaborl = function () {
            $scope.getExperienciaLaboral($scope.ID_CANDIDATO, function (Objeto) {

                if (Objeto.length > 0) {
                    var fechaInicio = new Date(parseInt(Objeto[0].FE_PERIODO_INICIO.substr(6)));
                    var fechaFin = new Date(parseInt(Objeto[0].FE_PERIODO_FIN.substr(6)));

                    var yyyyInicio = fechaInicio.getFullYear();
                    $scope.ANIO_INICIO_ULTIMO = yyyyInicio;
                    $scope.MES_INICIO_ULTIMO = mmInicio;

                    var mmFin = fechaFin.getMonth() + 1;
                    var yyyyFin = fechaFin.getFullYear();
                    $scope.ANIO_FIN_ULTIMO = yyyyFin;
                    $scope.MES_FIN_ULTIMO = mmFin;

                    $scope.CL_CORREO_ELECTRONICO_ULTIMO = Objeto[0].CL_CORREO_ELECTRONICO;
                    $scope.DS_COMENTARIOS_ULTIMO = Objeto[0].DS_COMENTARIOS;
                    $scope.DS_DOMICILIO_ULTIMO = Objeto[0].DS_DOMICILIO;
                    $scope.DS_FUNCIONES_ULTIMO = Objeto[0].DS_FUNCIONES;
                    $scope.ID_CANDIDATO_ULTIMO = Objeto[0].ID_CANDIDATO;
                    $scope.ID_EXPERIENCIA_LABORAL_ULTIMO = Objeto[0].ID_EXPERIENCIA_LABORAL;
                    $scope.MN_PRIMER_SUELDO_ULTIMO = Objeto[0].MN_PRIMER_SUELDO;
                    $scope.MN_ULTIMO_SUELDO_ULTIMO = Objeto[0].MN_ULTIMO_SUELDO;
                    $scope.NB_CONTACTO_ULTIMO = Objeto[0].NB_CONTACTO;
                    $scope.NB_EMPRESA_ULTIMO = Objeto[0].NB_EMPRESA;
                    $scope.NB_FUNCION_ULTIMO = Objeto[0].NB_FUNCION;
                    $scope.NB_GIRO_ULTIMO = Objeto[0].NB_GIRO;
                    $scope.NB_PUESTO_ULTIMO = Objeto[0].NB_PUESTO;
                    $scope.NB_PUESTO_CONTACTO_ULTIMO = Objeto[0].NB_PUESTO_CONTACTO;
                    $scope.NO_TELEFONO_CONTACTO_ULTIMO = Objeto[0].NO_TELEFONO_CONTACTO;

                }

                if (Objeto.length > 1) {
                    var fechaInicio = new Date(parseInt(Objeto[1].FE_PERIODO_INICIO.substr(6)));
                    var fechaFin = new Date(parseInt(Objeto[1].FE_PERIODO_FIN.substr(6)));

                    var yyyyInicio = fechaInicio.getFullYear();
                    $scope.ANIO_INICIO_ANT1 = yyyyInicio;
                    $scope.MES_INICIO_ANT1 = mmInicio;

                    var mmFin = fechaFin.getMonth() + 1;
                    var yyyyFin = fechaFin.getFullYear();
                    $scope.ANIO_FIN_ANT1 = yyyyFin;
                    $scope.MES_FIN_ANT1 = mmFin;

                    $scope.CL_CORREO_ELECTRONICO_ANT1 = Objeto[1].CL_CORREO_ELECTRONICO;
                    $scope.DS_COMENTARIOS_ANT1 = Objeto[1].DS_COMENTARIOS;
                    $scope.DS_DOMICILIO_ANT1 = Objeto[1].DS_DOMICILIO;
                    $scope.DS_FUNCIONES_ANT1 = Objeto[1].DS_FUNCIONES;
                    $scope.ID_CANDIDATO_ANT1 = Objeto[1].ID_CANDIDATO;
                    $scope.ID_EXPERIENCIA_LABORAL_ANT1 = Objeto[1].ID_EXPERIENCIA_LABORAL;
                    $scope.MN_PRIMER_SUELDO_ANT1 = Objeto[1].MN_PRIMER_SUELDO;
                    $scope.MN_ULTIMO_SUELDO_ANT1 = Objeto[1].MN_ULTIMO_SUELDO;
                    $scope.NB_CONTACTO_ANT1 = Objeto[1].NB_CONTACTO;
                    $scope.NB_EMPRESA_ANT1 = Objeto[1].NB_EMPRESA;
                    $scope.NB_FUNCION_ANT1 = Objeto[1].NB_FUNCION;
                    $scope.NB_GIRO_ANT1 = Objeto[1].NB_GIRO;
                    $scope.NB_PUESTO_ANT1 = Objeto[1].NB_PUESTO;
                    $scope.NB_PUESTO_CONTACTO_ANT1 = Objeto[1].NB_PUESTO_CONTACTO;
                    $scope.NO_TELEFONO_CONTACTO_ANT1 = Objeto[1].NO_TELEFONO_CONTACTO;

                }

                if (Objeto.length > 2) {
                    var fechaInicio = new Date(parseInt(Objeto[2].FE_PERIODO_INICIO.substr(6)));
                    var fechaFin = new Date(parseInt(Objeto[2].FE_PERIODO_FIN.substr(6)));

                    var yyyyInicio = fechaInicio.getFullYear();
                    $scope.ANIO_INICIO_ANT2 = yyyyInicio;
                    $scope.MES_INICIO_ANT2 = mmInicio;

                    var mmFin = fechaFin.getMonth() + 1;
                    var yyyyFin = fechaFin.getFullYear();
                    $scope.ANIO_FIN_ANT2 = yyyyFin;
                    $scope.MES_FIN_ANT2 = mmFin;

                    $scope.CL_CORREO_ELECTRONICO_ANT2 = Objeto[2].CL_CORREO_ELECTRONICO;
                    $scope.DS_COMENTARIOS_ANT2 = Objeto[2].DS_COMENTARIOS;
                    $scope.DS_DOMICILIO_ANT2 = Objeto[2].DS_DOMICILIO;
                    $scope.DS_FUNCIONES_ANT2 = Objeto[2].DS_FUNCIONES;
                    $scope.ID_CANDIDATO_ANT2 = Objeto[2].ID_CANDIDATO;
                    $scope.ID_EXPERIENCIA_LABORAL_ANT2 = Objeto[2].ID_EXPERIENCIA_LABORAL;
                    $scope.MN_PRIMER_SUELDO_ANT2 = Objeto[2].MN_PRIMER_SUELDO;
                    $scope.MN_ULTIMO_SUELDO_ANT2 = Objeto[2].MN_ULTIMO_SUELDO;
                    $scope.NB_CONTACTO_ANT2 = Objeto[2].NB_CONTACTO;
                    $scope.NB_EMPRESA_ANT2 = Objeto[2].NB_EMPRESA;
                    $scope.NB_FUNCION_ANT2 = Objeto[2].NB_FUNCION;
                    $scope.NB_GIRO_ANT2 = Objeto[2].NB_GIRO;
                    $scope.NB_PUESTO_ANT2 = Objeto[2].NB_PUESTO;
                    $scope.NB_PUESTO_CONTACTO_ANT2 = Objeto[2].NB_PUESTO_CONTACTO;
                    $scope.NO_TELEFONO_CONTACTO_ANT2 = Objeto[2].NO_TELEFONO_CONTACTO;

                }

                if (Objeto.length > 3) {
                    var fechaInicio = new Date(parseInt(Objeto[3].FE_PERIODO_INICIO.substr(6)));
                    var fechaFin = new Date(parseInt(Objeto[3].FE_PERIODO_FIN.substr(6)));

                    var yyyyInicio = fechaInicio.getFullYear();
                    $scope.ANIO_INICIO_ANT3 = yyyyInicio;
                    $scope.MES_INICIO_ANT3 = mmInicio;

                    var mmFin = fechaFin.getMonth() + 1;
                    var yyyyFin = fechaFin.getFullYear();
                    $scope.ANIO_FIN_ANT3 = yyyyFin;
                    $scope.MES_FIN_ANT3 = mmFin;

                    $scope.CL_CORREO_ELECTRONICO_ANT3 = Objeto[3].CL_CORREO_ELECTRONICO;
                    $scope.DS_COMENTARIOS_ANT3 = Objeto[3].DS_COMENTARIOS;
                    $scope.DS_DOMICILIO_ANT3 = Objeto[3].DS_DOMICILIO;
                    $scope.DS_FUNCIONES_ANT3 = Objeto[3].DS_FUNCIONES;
                    $scope.ID_CANDIDATO_ANT3 = Objeto[3].ID_CANDIDATO;
                    $scope.ID_EXPERIENCIA_LABORAL_ANT3 = Objeto[3].ID_EXPERIENCIA_LABORAL;
                    $scope.MN_PRIMER_SUELDO_ANT3 = Objeto[3].MN_PRIMER_SUELDO;
                    $scope.MN_ULTIMO_SUELDO_ANT3 = Objeto[3].MN_ULTIMO_SUELDO;
                    $scope.NB_CONTACTO_ANT3 = Objeto[3].NB_CONTACTO;
                    $scope.NB_EMPRESA_ANT3 = Objeto[3].NB_EMPRESA;
                    $scope.NB_FUNCION_ANT3 = Objeto[3].NB_FUNCION;
                    $scope.NB_GIRO_ANT3 = Objeto[3].NB_GIRO;
                    $scope.NB_PUESTO_ANT3 = Objeto[3].NB_PUESTO;
                    $scope.NB_PUESTO_CONTACTO_ANT3 = Objeto[3].NB_PUESTO_CONTACTO;
                    $scope.NO_TELEFONO_CONTACTO_ANT3 = Objeto[3].NO_TELEFONO_CONTACTO;

                }

            })
        };

        $scope.getExperienciaLaboral = function (param, response) {
            $.ajax({
                type: "post",
                url: "ws/OperacionesGral.svc/Get_K_EXPERIENCIA_LABORAL",
                data: JSON.stringify({ ID_CANDIDATO: $scope.ID_CANDIDATO }),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response(data);
                }
            });
        };

    };

    $scope.obtieneExperienciaLaboral();

    ////////////////////////////////////////////////////////////////////////////////////////// FUNCIONES

    $scope.Abrir_Texto = function () {

        var ObjSolicitud = {
            DS_COMENTARIO: $scope.DS_COMENTARIO,
            ID_CANDIDATO: $scope.ID_CANDIDATO
        };

        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'opSolicitud.html',
            controller: 'ModalSolicitud',
            resolve: {
                OBJETO: function () { return ObjSolicitud; }
            }
        });
    };

    $scope.Abrir_Comentarios = function () {

        var ObjDepartamento = {
            CL_DEPARTAMENTO: ''
        };

        var modalInstance = $modal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'opSolicitud.html',
            controller: 'ModalSolicitud',
            resolve: {
                OBJETO: function () { return ObjDepartamento; }
            }
        });
    };

    $scope.formatDate = function (date) {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('-');
    }

    $scope.getItem = function (target) {
        var tabStrip = $("#tbsNuevaSolicitud").data("kendoTabStrip");
        return tabStrip.tabGroup.children("li").eq(target);
    };

    /////////////////////////////////////////////////////////////////////////////////////////////////// GUARDAR TAB1 //////////////

    $scope.guardarTab1 = function () { //en caso de que le den click en aceptar generara lo siguiente  
        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
        if ($scope.ID_CANDIDATO != undefined)  //se verifica que la clave primaria sea diferente de nulo  
            tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  

        if ($scope.S_NACIONALIDAD == "Otra") {
            $scope.DS_NACIONALIDAD = $scope.M_NACIONALIDAD;
        }
        else {
            $scope.DS_NACIONALIDAD = $scope.S_NACIONALIDAD;
        }

        if ($scope.CL_RFC != undefined) {
            var validaRfc = ValidaRfc($scope.CL_RFC);

            if (!validaRfc) {
                BootstrapDialog.show({
                    title: K_SOLICITUD.TITULO_PRINCIPAL,
                    message: K_SOLICITUD.MSJRFC,
                    size: 'size-small',
                    type: tema.cssClass
                });
                return false;
            }
        }

        if ($scope.CL_CURP != undefined) {
            var validaCurp = ValidaCurp($scope.CL_CURP);

            if (!validaCurp) {
                BootstrapDialog.show({
                    title: K_SOLICITUD.TITULO_PRINCIPAL,
                    message: K_SOLICITUD.MSJCURP,
                    size: 'size-small',
                    type: tema.cssClass
                });
                return false;
            }
        }

        EsEmail($scope.CL_CORREO_ELECTRONICO);

        var xml = document.createElement("TELEFONOS");
        telPrincipal = document.createElement("DESCRIPCION")
        telPrincipal.setAttribute("TEL_PRINCIPAL", $scope.DS_TELEFONO);
        telPrincipal.setAttribute("TEL_MOVIL", $scope.DS_TELMOVIL);
        telPrincipal.setAttribute("TEL_OTRO", $scope.DS_OTROTEL);
        xml.appendChild(telPrincipal);
        $scope.XML_TELEFONOS = xml;

        var txt_pais = $("#txtPais").data("kendoDropDownList");
        var txt_estado = $("#txt_ESTADOS").data("kendoDropDownList");
        var txt_municipio = $("#txt_MUNICIPIO").data("kendoDropDownList");
        var txt_colonia = $("#txtColonia").data("kendoDropDownList");
        $scope.NB_PAIS = txt_pais.text();
        $scope.NB_ESTADO = txt_estado.text();
        $scope.NB_MUNICIPIO = txt_municipio.text();
        $scope.NB_COLONIA = txt_colonia.text();

        var Obj_Candidato = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            ID_CANDIDATO: $scope.ID_CANDIDATO
            , CL_CARTILLA_MILITAR: $scope.CL_CARTILLA_MILITAR
 			, CL_CEDULA_PROFESIONAL: $scope.CL_CEDULA_PROFESIONAL
 			, CL_CODIGO_POSTAL: $scope.CL_CODIGO_POSTAL
 			, CL_CORREO_ELECTRONICO: $scope.CL_CORREO_ELECTRONICO
 			, CL_CURP: $scope.CL_CURP
 			, CL_DISPONIBILIDAD_VIAJE: $scope.CL_DISPONIBILIDAD_VIAJE
 			, CL_ESTADO_CIVIL: $scope.CL_ESTADO_CIVIL
 			, CL_GENERO: $scope.CL_GENERO
 			, CL_NACIONALIDAD: $scope.DS_NACIONALIDAD
 			, CL_NSS: $scope.CL_NSS
 			, CL_RFC: $scope.CL_RFC
 			, CL_TIPO_SANGUINEO: $scope.CL_TIPO_SANGUINEO
 			, CL_USUARIO_APP_CREA: $scope.CL_USUARIO_APP_CREA
 			, CL_USUARIO_APP_MODIFICA: $scope.CL_USUARIO_APP_MODIFICA
 			, DS_COMENTARIO: $scope.DS_COMENTARIO
 			, DS_DISPONIBILIDAD: $scope.DS_DISPONIBILIDAD
 			, DS_LUGAR_NACIMIENTO: $scope.DS_LUGAR_NACIMIENTO
 			, DS_NACIONALIDAD: $scope.DS_NACIONALIDAD
 			, DS_VEHICULO: $scope.DS_VEHICULO
 			, FE_CREACION: $scope.FE_CREACION
 			, FE_MODIFICACION: $scope.FE_MODIFICACION
            //,FE_NACIMIENTO: nuevaFecha
 			, FG_ACTIVO: true
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, MN_SUELDO: $scope.MN_SUELDO
 			, NB_APELLIDO_MATERNO: $scope.NB_APELLIDO_MATERNO
 			, NB_APELLIDO_PATERNO: $scope.NB_APELLIDO_PATERNO
 			, NB_CALLE: $scope.NB_CALLE
 			, NB_CANDIDATO: $scope.NB_CANDIDATO
 			, NB_COLONIA: $scope.NB_COLONIA
 			, NB_CONYUGUE: $scope.NB_CONYUGUE
 			, NB_ESTADO: $scope.NB_ESTADO
 			, NB_LICENCIA: $scope.NB_LICENCIA
 			, NB_MUNICIPIO: $scope.NB_MUNICIPIO
 			, NB_PAIS: $scope.NB_PAIS
 			, NB_PROGRAMA_CREA: $scope.NB_PROGRAMA_CREA
 			, NB_PROGRAMA_MODIFICA: $scope.NB_PROGRAMA_MODIFICA
 			, NO_EXTERIOR: $scope.NO_EXTERIOR
 			, NO_INTERIOR: $scope.NO_INTERIOR
 			, XML_EGRESOS: $scope.XML_EGRESOS
 			, XML_INGRESOS: $scope.XML_INGRESOS
 			, XML_PATRIMONIO: $scope.XML_PATRIMONIO
 			, XML_PERFIL_RED_SOCIAL: $scope.XML_PERFIL_RED_SOCIAL
 			, XML_TELEFONOS: $scope.XML_TELEFONOS
        };

        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_CANDIDATO",
            method: "POST",
            data: {
                V_C_CANDIDATO: Obj_Candidato,
                usuario: TraerUsuario(),
                programa: "InsertaCandidato.html",
                tipo_transaccion: tipoOperacion,
                fechaNacimiento: $scope.FE_NACIMIENTO
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
 		        if ($scope.ID_CANDIDATO == undefined) {
 		            $scope.ID_CANDIDATO = response.data;
 		            //MANDAR A TABINDEX CORRESPONDIENTE
 		            var tabStrip = $("#tbsNuevaSolicitud").kendoTabStrip().data("kendoTabStrip");
 		            tabStrip.select($scope.getItem(1));
 		            var tab = tabStrip.select();
 		            tabStrip.enable(tabStrip.tabGroup.children().eq(1), true);
 		            $scope.FG_DEPENDIENTE = "true";
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

    $scope.cancelarTab1 = function () {
        LimpiarFormulario("newForm");
    };

    /////////////////////////////////////////////////////////////////////////////////////////////////// GUARDAR TAB2 //////////////

    $scope.guardarTab2 = function () {
        var tabStrip = $("#tbsNuevaSolicitud").kendoTabStrip().data("kendoTabStrip");
        tabStrip.select($scope.getItem(2));
        var tab = tabStrip.select();
        tabStrip.enable(tabStrip.tabGroup.children().eq(2), true);
    };

    $scope.cancelarTab2 = function () {

        console.log("entra");
        var tabStrip = $("#tbsNuevaSolicitud").kendoTabStrip().data("kendoTabStrip");
        tabStrip.select($scope.getItem(0));
        var tab = tabStrip.select();
        tabStrip.enable(tabStrip.tabGroup.children().eq(0), true);
    };

    ///////////////////////////////////////////////////////////////////////////// GUARDAR DEPENDIENTE

    $scope.guardarDependiente = function () {
        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
        if ($scope.ID_PARIENTE != undefined)  //se verifica que la clave primaria sea diferente de nulo  
            tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  

        if ($scope.FG_DEPENDIENTE_RAD == "Si") {
            $scope.FG_DEPENDIENTE = true;
        } else if ($scope.FG_DEPENDIENTE_RAD == "No") {
            $scope.FG_DEPENDIENTE = false;
        }

        var Obj_Pariente = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            ID_PARIENTE: $scope.ID_PARIENTE
            , CL_GENERO: $scope.CL_GENERO
 			, CL_OCUPACION: $scope.CL_OCUPACION_FAMILIAR
 			, CL_PARENTEZCO: $scope.CL_PARENTEZCO
            //, FE_NACIMIENTO: $scope.FE_NACIMIENTO_FAMILIAR
 			, FG_ACTIVO: true
 			, FG_DEPENDIENTE: $scope.FG_DEPENDIENTE
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, NB_PARIENTE: $scope.NB_PARIENTE
        };
        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_PARIENTE",
            method: "POST",
            data: {
                V_C_PARIENTE: Obj_Pariente,
                usuario: TraerUsuario(),
                programa: "NuevaSolicitudEmp.html",
                tipo_transaccion: tipoOperacion,
                fecha: $scope.FE_NACIMIENTO_FAMILIAR
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

 		        LimpiarFormulario("newTab2");
 		        $scope.cargarParientes();
 		        $scope.ID_PARIENTE = undefined;
 		        $scope.cargarParientes();
 		        $scope.FG_DEPENDIENTE = "true";
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

    $scope.editPariente = function (id) {
        $scope.ID_PARIENTE = $scope.familiar[id].ID_PARIENTE;
        $scope.NB_PARIENTE = $scope.familiar[id].NB_PARIENTE;
        $scope.CL_PARENTEZCO = $scope.familiar[id].CL_PARENTEZCO;
        $scope.FE_NACIMIENTO_FAMILIAR = new Date($scope.familiar[id].FE_NACIMIENTO.split("/").reverse().join("/"));
        $scope.CL_OCUPACION_FAMILIAR = $scope.familiar[id].CL_OCUPACION;
        $scope.FG_DEPENDIENTE = $scope.familiar[id].FG_DEPENDIENTE.toString();
    };

    $scope.deletePariente = function (id) {

        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/Delete_C_PARIENTE",
            method: "POST",
            data: { //se le pasan los parametros 
                ID_PARIENTE: $scope.familiar[id].ID_PARIENTE, // se le pasa la llave primaria  
                usuario: TraerUsuario(), // se le pasa el usuario 
                programa: "NuevaSolicitud.html"  //se le pasa  
            },
            headers: {  //SE ESPECIFICAN LAS CABECERAS QUE SE ENVIAN AL WEB SERVICE, ES DECIR COMO SE ENVIAN LOS DATOS: JSON, XML, TEXTO PLANO O FILE 
                'Accept': 'application/json',  //SE INDICA QUE EL EVENTO ACEPTARA EL TIPO JSON  
                'Content-Type': 'application/json' // SE INDICA QUE TIPO CONTENDRA LA INFORMACION QUE SE ENVIA   
            }
        })
        .then(function (response) { // ejecucion de WS 
            if (response.data != true) {  //si el procedimiento regresa falso 
                BootstrapDialog.show({ //se mostrara un mensaje de error  
                    title: '',  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
                    message: Config.ERRORELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo ERRORGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.RGB //se le indica el color dependiendo del modulo en que se encuentre 
                });
            } else {
                $scope.cargarParientes();
                $scope.ID_PARIENTE = undefined;
                $scope.cargarParientes();
                BootstrapDialog.show({  //si el procedimiento regresa true 
                    title: '',  //el titulo del mensaje  
                    message: Config.MENSAJEELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
                    size: 'size-small',  //se especifica el tamaño del mensaje de error 
                    type: tema.RGB,  //se le indica el color dependiendo del modulo en que se encuentre 
                });
            }
        },
        function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
            BootstrapDialog.show({
                title: C_PARIENTE.TITULO_PANTALLA,
                message: Config.ERRORGENERICO,
                size: 'size-small',
                type: tema.RGB,
            });
        });

    };

    $scope.cancelarDependiente = function () {
        LimpiarFormulario("newTab2");
        $scope.ID_PARIENTE = undefined;
        $scope.NB_PARIENTE = undefined;
        $scope.CL_PARENTEZCO = undefined;
        $scope.FE_NACIMIENTO_FAMILIAR = undefined;
        $scope.CL_OCUPACION_FAMILIAR = undefined;
        $scope.FG_DEPENDIENTE = undefined;
    };

    /////////////////////////////////////////////////////////////////////////////////////////////////// GUARDAR TAB3 ////////////

    //////////////////////////////////////////////////////////////////////////////////////// GUARDAR ESCOLARIDAD

    $scope.guardarEscolaridad = function () {

        var xmlEscolaridad = document.createElement("ESCOLARIDAD");

        var Obj_EscolaridadPrimaria = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_ESTADO_ESCOLARIDAD: $scope.CL_ESTADO_ESCOLARIDAD_PRIMARIA
 			, FE_PERIODO_FIN: $scope.FE_TERMINO_PRIMARIA
 			, FE_PERIODO_INICIO: $scope.FE_INICIO_PRIMARIA
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, ID_EMPLEADO_ESCOLARIDAD: $scope.ID_EMPLEADO_ESCOLARIDAD_PRIMARIA
 			, ID_ESCOLARIDAD: $scope.ID_PRIMARIA
 			, NB_INSTITUCION: $scope.NB_INSTITUCION_PRIMARIA
        };

        var Obj_EscolaridadSecundaria = {
            CL_ESTADO_ESCOLARIDAD: $scope.CL_ESTADO_ESCOLARIDAD_SECUNDARIA
 			, FE_PERIODO_FIN: $scope.FE_TERMINO_SECUNDARIA
 			, FE_PERIODO_INICIO: $scope.FE_INICIO_SECUNDARIA
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, ID_EMPLEADO_ESCOLARIDAD: $scope.ID_EMPLEADO_ESCOLARIDAD_SECUNDARIA
 			, ID_ESCOLARIDAD: $scope.ID_SECUNDARIA
 			, NB_INSTITUCION: $scope.NB_INSTITUCION_SECUNDARIA
        };

        var Obj_EscolaridadPreparatoria = {
            CL_ESTADO_ESCOLARIDAD: $scope.CL_ESTADO_ESCOLARIDAD_PREPA
 			, FE_PERIODO_FIN: $scope.FE_TERMINO_PREPA
 			, FE_PERIODO_INICIO: $scope.FE_INICIO_PREPA
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, ID_EMPLEADO_ESCOLARIDAD: $scope.ID_EMPLEADO_ESCOLARIDAD_PREPARATORIA
 			, ID_ESCOLARIDAD: $scope.ID_PREPARATORIA
 			, NB_INSTITUCION: $scope.NB_INSTITUCION_PREPARATORIA
        };

        var Obj_EscolaridadUni1 = {
            CL_ESTADO_ESCOLARIDAD: $scope.CL_ESTADO_ESCOLARIDAD_UNI1
 			, FE_PERIODO_FIN: $scope.FE_TERMINO_UNIVERSIDAD1
 			, FE_PERIODO_INICIO: $scope.FE_INICIO_UNIVERSIDAD1
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, ID_EMPLEADO_ESCOLARIDAD: $scope.ID_EMPLEADO_ESCOLARIDAD_UNI1
 			, ID_ESCOLARIDAD: $scope.CL_INSTITUCION_UNI1
            , NB_INSTITUCION: $scope.NB_INSTITUCION_UNI1
        };

        var Obj_EscolaridadUni2 = {
            CL_ESTADO_ESCOLARIDAD: $scope.CL_ESTADO_ESCOLARIDAD_UNI2
 			, FE_PERIODO_FIN: $scope.FE_TERMINO_UNIVERSIDAD2
 			, FE_PERIODO_INICIO: $scope.FE_INICIO_UNIVERSIDAD2
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, ID_EMPLEADO_ESCOLARIDAD: $scope.ID_EMPLEADO_ESCOLARIDAD_UNI2
 			, ID_ESCOLARIDAD: $scope.CL_INSTITUCION_UNI2
            , NB_INSTITUCION: $scope.NB_INSTITUCION_UNI2
        };

        var Obj_EscolaridadPos1 = {
            CL_ESTADO_ESCOLARIDAD: $scope.CL_ESTADO_ESCOLARIDAD_POS1
 			, FE_PERIODO_FIN: $scope.FE_TERMINO_POS1
 			, FE_PERIODO_INICIO: $scope.FE_INICIO_POS1
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, ID_EMPLEADO_ESCOLARIDAD: $scope.ID_EMPLEADO_ESCOLARIDAD_POS1
 			, ID_ESCOLARIDAD: $scope.CL_INSTITUCION_POS1
            , NB_INSTITUCION: $scope.NB_INSTITUCION_POS1
        };

        var Obj_EscolaridadPos2 = {
            CL_ESTADO_ESCOLARIDAD: $scope.CL_ESTADO_ESCOLARIDAD_POS2
 			, FE_PERIODO_FIN: $scope.FE_TERMINO_POS2
 			, FE_PERIODO_INICIO: $scope.FE_INICIO_POS2
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, ID_EMPLEADO_ESCOLARIDAD: $scope.ID_EMPLEADO_ESCOLARIDAD_POS2
 			, ID_ESCOLARIDAD: $scope.CL_INSTITUCION_POS2
            , NB_INSTITUCION: $scope.NB_INSTITUCION_POS2
        };

        if ($scope.NB_INSTITUCION_PRIMARIA == undefined) { Obj_EscolaridadPrimaria = null }
        if ($scope.NB_INSTITUCION_SECUNDARIA == undefined) { Obj_EscolaridadSecundaria = null }
        if ($scope.NB_INSTITUCION_PREPARATORIA == undefined) { Obj_EscolaridadPreparatoria = null }
        if ($scope.NB_INSTITUCION_PREPARATORIA == undefined) { Obj_EscolaridadUni1 = null }
        if ($scope.NB_INSTITUCION_UNI1 == undefined) { Obj_EscolaridadUni2 = null }
        if ($scope.NB_INSTITUCION_POS1 == undefined) { Obj_EscolaridadPos1 = null }
        if ($scope.NB_INSTITUCION_POS2 == undefined) { Obj_EscolaridadPos2 = null }

        console.log(Obj_EscolaridadPrimaria);
        console.log(Obj_EscolaridadSecundaria);
        console.log(Obj_EscolaridadPreparatoria);
        console.log(Obj_EscolaridadUni1);
        console.log(Obj_EscolaridadUni2);
        console.log(Obj_EscolaridadUni2);

        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_EMPLEADO_ESCOLARIDAD",
            method: "POST",
            data: {
                PRIMARIA: Obj_EscolaridadPrimaria,
                SECUNDARIA: Obj_EscolaridadSecundaria,
                PREPA: Obj_EscolaridadPreparatoria,
                UNI1: Obj_EscolaridadUni1,
                UNI2: Obj_EscolaridadUni2,
                POS1: Obj_EscolaridadPos1,
                POS2: Obj_EscolaridadPos2,
                usuario: TraerUsuario(),
                programa: "NuevaSolicitud.html",
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
 		        if ($scope.ID_EMPLEADO_ESCOLARIDAD == undefined) {
 		            var tabStrip = $("#tbsNuevaSolicitud").kendoTabStrip().data("kendoTabStrip");
 		            tabStrip.select($scope.getItem(3));
 		            var tab = tabStrip.select();
 		            tabStrip.enable(tabStrip.tabGroup.children().eq(3), true);
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

    //////////////////////////////////////////////////////////////////////////////////////// GUARDAR IDIOMAS

    $scope.guardarIdioma = function () {

        $scope.ID_CANDIDATO = 3;

        var tipoOperacion = "I" //por default se pone como si se ingresara un nuevo registro  
        if ($scope.ID_EMPLEADO_IDIOMA != undefined)  //se verifica que la clave primaria sea diferente de nulo  
            tipoOperacion = "A" //si se tiene la clave primaria se hara una modificacion en vez de insercion  
        var Obj_EmpleadoIdioma = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_INSTITUCION: $scope.CL_INSTITUCION
 			, ID_CANDIDATO: $scope.ID_CANDIDATO
 			, ID_EMPLEADO: $scope.ID_EMPLEADO
 			, ID_EMPLEADO_IDIOMA: $scope.ID_EMPLEADO_IDIOMA
 			, ID_IDIOMA: $scope.ID_IDIOMA
 			, NO_PUNTAJE: $scope.NO_PUNTAJE
 			, PR_CONVERSACIONAL: $scope.PR_CONVERSACIONAL
 			, PR_ESCRITURA: $scope.PR_ESCRITURA
 			, PR_LECTURA: $scope.PR_LECTURA
 			, CL_INSTITUCION: $scope.CL_INSTITUCION
        };
        $http({
            url: "ws/OperacionesGral.svc/Insert_update_C_EMPLEADO_IDIOMA",
            method: "POST",
            data: {
                V_C_EMPLEADO_IDIOMA: Obj_EmpleadoIdioma,
                usuario: TraerUsuario(),
                programa: "InsertaEmpleadoIdioma.html",
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
 		        $scope.cargarIdiomas();
 		        $scope.ID_EMPLEADO_IDIOMA = undefined;
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

    $scope.editIdioma = function (id) {
        $scope.ID_EMPLEADO_IDIOMA = $scope.idiomas[id].ID_PARIENTE;
        $scope.ID_IDIOMA = $scope.idiomas[id].ID_IDIOMA;
        $scope.PR_CONVERSACIONAL = $scope.idiomas[id].PR_CONVERSACIONAL;
        $scope.PR_ESCRITURA = $scope.idiomas[id].PR_ESCRITURA;
        $scope.CL_INSTITUCION = $scope.idiomas[id].CL_INSTITUCION;
        $scope.NO_PUNTAJE = $scope.idiomas[id].NO_PUNTAJE;
    };

    $scope.deleteIdioma = function (id) {
        $http({ //se especifica cual sera nuestro web service para ejecutarlo 
            url: "ws/OperacionesGral.svc/Delete_C_IDIOMA",
            method: "POST",
            data: { //se le pasan los parametros 
                ID_IDIOMA: $scope.idiomas[id].ID_PARIENTE, // se le pasa la llave primaria  
                usuario: TraerUsuario(), // se le pasa el usuario 
                programa: "CatalogoIdioma.html"  //se le pasa  
            },
            headers: {  //SE ESPECIFICAN LAS CABECERAS QUE SE ENVIAN AL WEB SERVICE, ES DECIR COMO SE ENVIAN LOS DATOS: JSON, XML, TEXTO PLANO O FILE 
                'Accept': 'application/json',  //SE INDICA QUE EL EVENTO ACEPTARA EL TIPO JSON  
                'Content-Type': 'application/json' // SE INDICA QUE TIPO CONTENDRA LA INFORMACION QUE SE ENVIA   
            }
        })
          .then(function (response) { // ejecucion de WS 
              if (response.data != true) {  //si el procedimiento regresa falso 
                  BootstrapDialog.show({ //se mostrara un mensaje de error  
                      title: C_IDIOMA.TITULO_PANTALLA,  //el titulo del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEERROR  
                      message: Config.ERRORELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo ERRORGUARDAR 
                      size: 'size-small',  //se especifica el tamaño del mensaje de error 
                      type: tema.RGB //se le indica el color dependiendo del modulo en que se encuentre 
                  });
              } else {
                  $("#Grid_Idioma").data("kendoGrid").dataSource.read(); //se manda a refrescar el grid  
                  $("#modalDelete").modal('toggle');
                  BootstrapDialog.show({  //si el procedimiento regresa true 
                      title: C_IDIOMA.TITULO_PANTALLA,  //el titulo del mensaje  
                      message: Config.MENSAJEELIMINAR,  //el contenido del mensaje esta en la clase Config que esta instanciada en appPrincipal.js en el atributo MENSAJEGUARDAR 
                      size: 'size-small',  //se especifica el tamaño del mensaje de error 
                      type: tema.RGB,  //se le indica el color dependiendo del modulo en que se encuentre 
                  });
              }
          },
          function (response) {  //funcion que mostrara el mensaje si ocurre un error antes de ejecutar el sp  
              BootstrapDialog.show({
                  title: C_IDIOMA.TITULO_PANTALLA,
                  message: Config.ERRORGENERICO,
                  size: 'size-small',
                  type: tema.RGB,
              });
          });
    };

    $scope.cancelarIdioma = function () {
        LimpiarFormulario("formIngles");
        $scope.ID_EMPLEADO_IDIOMA = undefined;
        $scope.ID_IDIOMA = undefined;
        $scope.PR_CONVERSACIONAL = undefined;
        $scope.PR_ESCRITURA = undefined;
        $scope.CL_INSTITUCION = undefined;
        $scope.NO_PUNTAJE = undefined;
    };

    /////////////////////////////////////////////////////////////////////////////////////////////////// GUARDAR TAB 4 ///////////

    $scope.guardarTab4 = function () {

        //ULTIMO
        var Obj_Empleo = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_CORREO_ELECTRONICO: $scope.CL_CORREO_ELECTRONICO_ULTIMO
 			, DS_COMENTARIOS: $scope.DS_COMENTARIOS_ULTIMO
 			, DS_DOMICILIO: $scope.DS_DOMICILIO_ULTIMO
 			, DS_FUNCIONES: $scope.DS_FUNCIONES_ULTIMO
 			, FE_FIN_ANIO: $scope.ANIO_FIN_ULTIMO
            , FE_FIN_MES: $scope.MES_FIN_ULTIMO
 			, FE_INICIO_ANIO: $scope.ANIO_INICIO_ULTIMO
            , FE_INICIO_MES: $scope.FE_INICIO_ULTIMO
 			, ID_CANDIDATO: $scope.ID_CANDIDATO_ULTIMO
 			, ID_EXPERIENCIA_LABORAL: $scope.ID_EXPERIENCIA_LABORAL_ULTIMO
 			, MN_PRIMER_SUELDO: $scope.MN_PRIMER_SUELDO_ULTIMO
 			, MN_ULTIMO_SUELDO: $scope.MN_ULTIMO_SUELDO_ULTIMO
 			, NB_CONTACTO: $scope.NB_CONTACTO_ULTIMO
 			, NB_EMPRESA: $scope.NB_EMPRESA_ULTIMO
 			, NB_FUNCION: $scope.NB_FUNCION_ULTIMO
 			, NB_GIRO: $scope.NB_GIRO_ULTIMO
 			, NB_PUESTO: $scope.NB_PUESTO_ULTIMO
 			, NB_PUESTO_CONTACTO: $scope.NB_PUESTO_CONTACTO_ULTIMO
 			, NO_TELEFONO_CONTACTO: $scope.NO_TELEFONO_CONTACTO_ULTIMO
        };

        var Obj_Ant1 = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_CORREO_ELECTRONICO: $scope.CL_CORREO_ELECTRONICO_ANT1
 			, DS_COMENTARIOS: $scope.DS_COMENTARIOS_ANT1
 			, DS_DOMICILIO: $scope.DS_DOMICILIO_ANT1
 			, DS_FUNCIONES: $scope.DS_FUNCIONES_ANT1
 			, FE_FIN_ANIO: $scope.ANIO_FIN_ANT1
            , FE_FIN_MES: $scope.MES_FIN_ANT1
 			, FE_INICIO_ANIO: $scope.ANIO_INICIO_ANT1
            , FE_INICIO_MES: $scope.FE_INICIO_ANT1
 			, ID_CANDIDATO: $scope.ID_CANDIDATO_ANT1
 			, ID_EXPERIENCIA_LABORAL: $scope.ID_EXPERIENCIA_LABORAL_ANT1
 			, MN_PRIMER_SUELDO: $scope.MN_PRIMER_SUELDO_ANT1
 			, MN_ULTIMO_SUELDO: $scope.MN_ULTIMO_SUELDO_ANT1
 			, NB_CONTACTO: $scope.NB_CONTACTO_ANT1
 			, NB_EMPRESA: $scope.NB_EMPRESA_ANT1
 			, NB_FUNCION: $scope.NB_FUNCION_ANT1
 			, NB_GIRO: $scope.NB_GIRO_ANT1
 			, NB_PUESTO: $scope.NB_PUESTO_ANT1
 			, NB_PUESTO_CONTACTO: $scope.NB_PUESTO_CONTACTO_ANT1
 			, NO_TELEFONO_CONTACTO: $scope.NO_TELEFONO_CONTACTO_ANT1
        };

        var Obj_Ant2 = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_CORREO_ELECTRONICO: $scope.CL_CORREO_ELECTRONICO_ANT2
 			, DS_COMENTARIOS: $scope.DS_COMENTARIOS_ANT2
 			, DS_DOMICILIO: $scope.DS_DOMICILIO_ANT2
 			, DS_FUNCIONES: $scope.DS_FUNCIONES_ANT2
 			, FE_FIN_ANIO: $scope.ANIO_FIN_ANT2
            , FE_FIN_MES: $scope.MES_FIN_ANT2
 			, FE_INICIO_ANIO: $scope.ANIO_INICIO_ANT2
            , FE_INICIO_MES: $scope.FE_INICIO_ANT2
 			, ID_CANDIDATO: $scope.ID_CANDIDATO_ANT2
 			, ID_EXPERIENCIA_LABORAL: $scope.ID_EXPERIENCIA_LABORAL_ANT2
 			, MN_PRIMER_SUELDO: $scope.MN_PRIMER_SUELDO_ANT2
 			, MN_ULTIMO_SUELDO: $scope.MN_ULTIMO_SUELDO_ANT2
 			, NB_CONTACTO: $scope.NB_CONTACTO_ANT2
 			, NB_EMPRESA: $scope.NB_EMPRESA_ANT2
 			, NB_FUNCION: $scope.NB_FUNCION_ANT2
 			, NB_GIRO: $scope.NB_GIRO_ANT2
 			, NB_PUESTO: $scope.NB_PUESTO_ANT2
 			, NB_PUESTO_CONTACTO: $scope.NB_PUESTO_CONTACTO_ANT2
 			, NO_TELEFONO_CONTACTO: $scope.NO_TELEFONO_CONTACTO_ANT2
        };

        var Obj_Ant3 = {// se declara el objeto con la estructura del result para insertar o actualizar el registro 
            CL_CORREO_ELECTRONICO: $scope.CL_CORREO_ELECTRONICO_ANT3
 			, DS_COMENTARIOS: $scope.DS_COMENTARIOS_ANT3
 			, DS_DOMICILIO: $scope.DS_DOMICILIO_ANT3
 			, DS_FUNCIONES: $scope.DS_FUNCIONES_ANT3
 			, FE_FIN_ANIO: $scope.ANIO_FIN_ANT3
            , FE_FIN_MES: $scope.MES_FIN_ANT3
 			, FE_INICIO_ANIO: $scope.ANIO_INICIO_ANT3
            , FE_INICIO_MES: $scope.FE_INICIO_ANT3
 			, ID_CANDIDATO: $scope.ID_CANDIDATO_ANT3
 			, ID_EXPERIENCIA_LABORAL: $scope.ID_EXPERIENCIA_LABORAL_ANT3
 			, MN_PRIMER_SUELDO: $scope.MN_PRIMER_SUELDO_ANT3
 			, MN_ULTIMO_SUELDO: $scope.MN_ULTIMO_SUELDO_ANT3
 			, NB_CONTACTO: $scope.NB_CONTACTO_ANT3
 			, NB_EMPRESA: $scope.NB_EMPRESA_ANT3
 			, NB_FUNCION: $scope.NB_FUNCION_ANT3
 			, NB_GIRO: $scope.NB_GIRO_ANT3
 			, NB_PUESTO: $scope.NB_PUESTO_ANT3
 			, NB_PUESTO_CONTACTO: $scope.NB_PUESTO_CONTACTO_ANT3
 			, NO_TELEFONO_CONTACTO: $scope.NO_TELEFONO_CONTACTO_ANT3
        };

        if ($scope.NB_EMPRESA_ULTIMO == undefined) { Obj_Empleo = null }
        if ($scope.NB_EMPRESA_ANT1 == undefined) { Obj_Ant1 = null }
        if ($scope.NB_EMPRESA_ANT2 == undefined) { Obj_Ant2 = null }
        if ($scope.NB_EMPRESA_ANT3 == undefined) { Obj_Ant3 = null }

        $http({
            url: "ws/OperacionesGral.svc/Insert_update_K_EXPERIENCIA_LABORAL",
            method: "POST",
            data: {
                ULTIMO: Obj_Empleo,
                ANTERIOR1: Obj_Ant1,
                ANTERIOR2: Obj_Ant2,
                ANTERIOR3: Obj_Ant3,
                usuario: TraerUsuario(),
                programa: "NuevaSolicitud.html",
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
                    if ($scope.ID_EMPLEADO_ESCOLARIDAD == undefined) {
                        var tabStrip = $("#tbsNuevaSolicitud").kendoTabStrip().data("kendoTabStrip");
                        tabStrip.select($scope.getItem(3));
                        var tab = tabStrip.select();
                        tabStrip.enable(tabStrip.tabGroup.children().eq(3), true);
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

    /////////////////////////////////////////////////////////////////////////////////////////////////// GUARDAR TAB 5 ///////////



    /////////////////////////////////////////////////////////////////////////////////////////////////// GUARDAR TAB 6 ///////////



    /////////////////////////////////////////////////////////////////////////////////////////////////// SELECCIÓN EMPLEADOS

    $scope.seleccionPersonal = function () {

        if ($scope.ID_CANDIDATO == undefined) { // se verifica que el registro este seleccionado  
            BootstrapDialog.show({ //se muestra el mensaje de error  
                title: SELECCION_PERSONAL.TITULO_PANTALLA, //se trae el nombre de la pantalla  
                message: SELECCION_PERSONAL.MENSAJESELECCION, // se envia el mensaje de error que falta el registro  
                size: 'size-small', //se especifica el tamaño de la ventana del mensaje  
                type: tema.cssClass //se especifica el color en base al modulo que se tiene  
            });
            return false; //se devuelve nulo para que no sigua con la funcion  
        }

        NavegacionInterna('SeleccionPersonal.html#/CatVal');
    };


});


app.controller('ModalSolicitud', function ($scope, $modalInstance, $http, OBJETO) {

    $scope.btnSave = Config.BOTONGUARDAR;
    $scope.btnCancel = Config.BOTONCANCELAR;
    $scope.modulo = tema.cssClass;

    OBJETO.DS_COMENTARIO = "<h1>Kendo Editor</h1>\n\n";

    $scope.DS_COMENTARIO = OBJETO.DS_COMENTARIO;
    $scope.ID_CANDIDATO = OBJETO.ID_CANDIDATO;

    $scope.ok = function () {
        console.log($scope.html);
    };


    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };


});
