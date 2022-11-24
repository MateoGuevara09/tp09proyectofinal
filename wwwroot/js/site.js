function MostrarInfoArchivo(IdDoc,nombre){
    $.ajax({
        url: '/Home/MostrarInfoArchivoAjax',
        data:{IdDocumento:IdDoc},
        type: 'POST',
        dataType: 'json',
        success : function(response){
            $("#NombreArchivo").html("Nombre del documento: " + response.nombreDoc);
            $("#NombreUsuario").html("Dueño: " + nombre);
            $("#TipoDocumento").html("Tipo de archivo: " + response.tipoDoc);
            $("#TiempoSubida").html("Fecha de subida: " + response.tiempoSubida);
        }
    });
}

//Obtener alto de la pantalla
$(document).ready(function(){

    var height = $(window).height();

    $('.divlistaarchivos').height(height);
});