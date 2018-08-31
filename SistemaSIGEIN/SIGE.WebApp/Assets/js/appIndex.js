//FUNCION PARA DETERMINAR  EN QUE LENGUAJE  SE MOSTRARA  LA PAGINA WEB
CambiarLenguaje = function (lang) {
    //ASIGNACION EL TIPO DEL IDIOMA  A UN LOCALSTORAGE
    localStorage.setItem("idioma", lang);
}

//FUNCION NAVEGACION DEL MENU PRINCIPAL
NavegacionMenu = function (modulo, fgActivo, clUsuario) {

    sessionStorage.setItem("modulo", modulo);
    var navigateURL = "";

    switch (modulo) {
        case 1:
            if (fgActivo == "1")
                navigateURL = 'IDP/Default.aspx';
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
        case 2:
            if (fgActivo == "1")
                navigateURL = 'FYD/Default.aspx';
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
        case 3:
            if (fgActivo == "1")
                navigateURL = 'MPC/Default.aspx';
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
        case 4:
            if (fgActivo == "1") {
                mnuPopUp.style.visibility = "visible";
                mnuPopUp.style.left = findPosX(document.getElementById("imgEvaluacionOrganizacional")) + 20 + "px";
                mnuPopUp.style.top = findPosY(document.getElementById("imgEvaluacionOrganizacional")) + 20 + "px";
                navigateURL = '';
            }
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
        case 5:
            if (fgActivo == "1") {
            var arrUrl = window.location.href.split('/');
            window.open(arrUrl[0] + '//' + arrUrl[2] + '/NOMINA/Menu.aspx?clUsuario=' + clUsuario , '_blank')
            }
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
        case 6:
            if (fgActivo == "1")
                navigateURL = 'PDE/VentanaInicioPDE.aspx';
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
        case 7:
            if (fgActivo == "1")
                navigateURL = 'Administracion/CatalogoConsultaInteligente.aspx';
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
        case 8:
            if (fgActivo == "1")
                navigateURL = 'ModulosApoyo/ReportesPersonalizados.aspx';
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;

    }

    if (navigateURL.length > 0)
        Navegacion(2, navigateURL);
}


//FUNCIÓN NAVEGACIÓN DEL SUBMENU EO
//Encuentra la posición X absoluta de un objeto (Imagen EO)
function findPosX(obj) {
    var curleft = 0;
    if (obj.offsetParent)
        while (1) {
            curleft += obj.offsetLeft;
            if (!obj.offsetParent)
                break;
            obj = obj.offsetParent;
        }
    else if (obj.x)
        curleft += obj.x;
    return curleft;
}

// Esta la Y
function findPosY(obj) {
    var curtop = 0;
    if (obj.offsetParent)
        while (1) {
            curtop += obj.offsetTop;
            if (!obj.offsetParent)
                break;
            obj = obj.offsetParent;
        }
    else if (obj.y)
        curtop += obj.y;
    return curtop;
}

function PopupIn(Cell) {
    Cell.style.backgroundColor = "#F0F3F4";
    Cell.style.color = "black";
}

function PopupOut(Cell) {
    Cell.style.backgroundColor = "";
    Cell.style.color = "white";
}

function ClosePopup() {
    ModulesEnabled = true;
    mnuPopUp.style.visibility = "hidden";

}

PopupClick = function (Cell, fgActivo) {
    var navigateURL = "";
    ClosePopup();
    var idTag = Cell.id.slice(6, 7);
    switch (idTag) {
        case "1":
            if (fgActivo == "1")
                navigateURL = 'EO/Default.aspx?m=CLIMA';
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
        case "2":
            if (fgActivo == "1")
                navigateURL = 'EO/Default.aspx?m=DESEMPENO';
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
        case "3":
            if (fgActivo == "1")
                navigateURL = 'EO/Default.aspx?m=ROTACION';
            else {
                alert(fgActivo)
                navigateURL = '';
            }
            break;
    }

    if (navigateURL.length > 0)
        Navegacion(2, navigateURL);
}

//NAVEGACION ENTRE PAGINAS
Navegacion = function (segundos, pagina) {
    $telerik.$("body").fadeOut("slow");
    setTimeout(function () {
        location.href = pagina;
    }, (parseInt(segundos) * 1000));
}

String.format = function () {
    // The string containing the format items (e.g. "{0}")
    // will and always has to be the first argument.
    var theString = arguments[0];

    // start with the second argument (i = 1)
    for (var i = 1; i < arguments.length; i++) {
        // "gm" = RegEx options for Global search (more than one instance)
        // and for Multiline search
        var regEx = new RegExp("\\{" + (i - 1) + "\\}", "gm");
        theString = theString.replace(regEx, arguments[i]);
    }

    return theString;
}