(function ($) {
    $.fn.Camposdinamicos = function(options,callback)
    {        
        var defaults =   {
            //PROPIEDADES DEL PLUGIN
            Url: "",
            Controlador:""
        };

        var options  =  $.extend(defaults,options);

        return this.each(function(){
              
            //CREAR ELEMENTOS
            var _divprincipal = $("<div/>",{
                'id':'divContenedor',
                'class': 'container',
                'data-ng-controller': options.Controlador
            });


            //URL DONDE LLEVA JSON DE LOS ELEMENTOS HTML
            $.getJSON(options.Url, function (data) {                               

                for (var i = 0; i < data.length; i++) {
                    

                    if (data[i].Tipo == "div") { //TIPO DIV
                        var div = _elementosHtml(data[i], "div");

                        if (data[i].Tienecontenedor == "SI") {
                            $("#" + data[i].IdContenedor).append(div);                            
                        } else {
                            _divprincipal.append(div);
                        }

                    } else if (data[i].Tipo == "legend") {  //TIPO LEGEND
                        var legend = _elementosHtml(data[i], "legend");

                        if (data[i].Tienecontenedor == "SI") {
                            $("#" + data[i].IdContenedor).append(legend);
                        } else {
                            _divprincipal.append(legend);
                        }                       
                    } else if (data[i].Tipo == "label") { //TIPO LABEL                        
                        var label = _elementosHtml(data[i], "label");

                        if (data[i].Tienecontenedor == "SI") {
                            $("#" + data[i].IdContenedor).append(label);
                        } else {
                            _divprincipal.append(label);
                        }

                    } else if (data[i].Tipo == "textbox") { //TIPO TEXTBOX
                        var textbox = _elementosHtml(data[i], "textbox");

                        if (data[i].Tienecontenedor == "SI") {
                             $("#" + data[i].IdContenedor).append(textbox);
                        } else {
                            _divprincipal.append(textbox);
                        }

                    } else if (data[i].Tipo == "select") { //TIPO SELECT
                        var select = _elementosHtml(data[i], "select");

                        if (data[i].Tienecontenedor == "SI") {
                             $("#" + data[i].IdContenedor).append(select);
                        } else {
                            _divprincipal.append(select);
                        }

                        //CUANDO SEA CON METODO EN EL CUAL ESTE EN JS
                        if (select.attr("data-modo") == "metodo") {

                            var str = select.attr("data-metodo");
                            var datos = eval("(" + str + ")");;

                            $("#" + data[i].Id).kendoComboBox({
                                dataTextField: select.attr("dataTextField"),
                                dataValueField: select.attr("dataValueField"),
                                dataSource: datos,
                                filter: "contains",
                                suggest: true,
                                index: 3
                            });

                        } else {
                            _combobox(select.attr("data-url"),"POST", null, data[i].Id,select);
                        }   

                    } else if (data[i].Tipo == "sistema") // CAMPO SISTEMA
                    {                        
                        var html = _elementosHtml(data[i], "html");

                        if (data[i].Tienecontenedor == "SI") {                            
                            $("#" + data[i].IdContenedor).append($.parseHTML(html));
                        } else {
                            _divprincipal.append($.parseHTML(html));
                        }
                    } 
                    else { //OTRO ELEMENTO HTML 
                        var elemento = _elementosHtml(data[i], data[i].Tipo);

                        if (data[i].Tienecontenedor == "SI") {
                            var objhtml = $("#" + data[i].IdContenedor).append(elemento);
                        } else {
                            _divprincipal.append(elemento);
                        }

                    }

                }

                //CARGA LOS TEXTOS
                CargarTextoDom();
            });

            $(this).append(_divprincipal);
           
        });
    }

    //**********************************METODOS*******************************// 

    //CARGA EL COMBOBOX DESDE WEB SERVICE
    _combobox = function (url, metodo, obj, id, select)
    {
        //CARGAR INFORMACION DEL COMBO
        _peticionesWs(url, metodo, obj, function (response) {
            $("#" + id).kendoComboBox({
                dataTextField: select.attr("dataTextField"),
                dataValueField: select.attr("dataValueField"),
                dataSource: response,
                filter: "contains",
                suggest: true,
                index: 3
            });
        });

    }

    //METODO PARA LA CREACION DE ELEMENTOS HTML
    _elementosHtml = function (obj, element) {

        var __element   = "";
        
        if (element == "html") {

            var _attr = obj.Atributos;
            for (var i = 0; i < _attr.attr.length; i++) {
                Object.getOwnPropertyNames(_attr.attr[i]).forEach(function (val, idx, array) {
                    if (val == "data-html") {
                        __element = _attr.attr[i][val];
                    } 
                    
                });

            }

        } else {

            __element = $("<" + element + "/>");
            var _attr = obj.Atributos;
            for (var i = 0; i < _attr.attr.length; i++) {
                Object.getOwnPropertyNames(_attr.attr[i]).forEach(function (val, idx, array) {
                    __element.attr(val, _attr.attr[i][val]);
                });

            }


        }

               
        return __element;
    }

    ///PETICIONES WS 
    _peticionesWs = function (url, metodo, obj, response)
    {
        $.ajax({
            type: metodo, //Ejemplo: post"
            url:  url,    //Ejemplo: ws/OperacionesGral.svc/ObtieneFolioSecuencia"
            data: obj ,   //Ejemplo: JSON.stringify({ CL_SECUENCIA: obj.parametro }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                response(data);
            }
        });
    }

    //SABER SI EXISTE UN ATRIBUTO EN UN TAG HTML
    $.fn.hasAttr = function (name) {
        return this.attr(name) !== undefined;
    };

})(jQuery);
