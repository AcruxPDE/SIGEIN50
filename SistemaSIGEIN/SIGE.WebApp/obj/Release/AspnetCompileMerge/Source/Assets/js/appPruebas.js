//FUNCIONES PARA DESHABILITAR EL CLICK DERECHO
//CASO INTERNET EXPLORER
function nrcIE() {
    if (document.all) {
        return false;
    }
}
//CASO OTROS NAVEGADORES
function nrcNS(e) {
    if (document.layers || (document.getElementById && !document.all)) {
        if (e.which == 2 || e.which == 3) {
            return false;
        }
    }
}

function JustificarTexto(text)
{
    var textoResultado = '<div style=\'text-align: justify;\'>'+text+'</div>';
    return textoResultado;
}

//APLICACION DE LOS METODOS O FUNCIONES  PARA DESHABILITAR SOBRE EL DOCUEMENTO O PAGINA HTML EL CLICK DERECHO.
if (document.layers) {
    document.captureEvents(Event.MOUSEDOWN);
    document.onmousedown = nrcNS;
} else {
    document.onmouseup = nrcNS;
    document.oncontextmenu = nrcIE;
}
document.oncontextmenu = new Function("return false");


//NO PERMITE LA NAVEGACION ENTRE LAS PAGINAS
//function noBack() {
//    window.history.forward();
//}


//FUNCION QUE AYUDA DETERMINAR EL TIEMPO  DE DURACION DE LA PRUEBAS
function Cronometro(duration, display) {
  
        var timer = duration, minutes, seconds;
        var time = setInterval(function () {
            minutes = parseInt(timer / 60, 10)
            seconds = parseInt(timer % 60, 10);
            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;
            display.textContent = minutes + ":" + seconds;

            if (--timer < 0) {

                clearInterval(time);
                mensajePruebaTerminada();
            }
        }, 1000);
        return time;
    }



//DESHABILITAR EL F5
//function checkKeyCode(evt) {
//    var evt = (evt) ? evt : ((event) ? event : null);
//    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
//    if (event.keyCode == 116) {
//        evt.keyCode = 0;
//        return false
//    }
//}
//document.onkeydown = checkKeyCode;


//FUNCION PARA CARGAR LOS METODOS DE JS QUE SEAN NECESARIOS
window.onload = function () {
    //noBack();
}

