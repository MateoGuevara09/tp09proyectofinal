function MostrarInfoArchivo(IdDoc,nombre){
    $.ajax({
        url: '/Home/MostrarInfoArchivoAjax',
        data:{IdDocumento:IdDoc},
        type: 'POST',
        dataType: 'json',
        success : function(response){
            $("#NombreArchivo").html("Nombre documento:" + response.nombreDoc);
            $("#NombreUsuario").html("Archivo de: " + nombre);
            $("#TipoDocumento").html("Tipo de archivo: " + response.tipoDoc);
            $("#TiempoSubida").html("Cuando fue subido: " + response.tiempoSubida);
        }
    });
}