/*$(document).ready(function () {
    
    $(".librerias").each(function () {
        var tipo = $(this).attr("data-tipo");
        if (tipo == "js")
        {
            $(this).attr("src", $(this).attr("src") + "?v=" + traerFecha());
        } else if (tipo == "css")
        {
            $(this).attr("href", $(this).attr("href") + "?v=" + traerFecha());
        }
    });

});*/

traerFecha = function () {
    var d = new Date();
    return d.getTime();
}
