using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace tp09proyectofinal.Models;

public static class BD{

    //CAMBIAR LA COMPUTADORA PARA QUE FUNCIONE
    private static string _conectionString = @"Server=A-PHZ2-CIDI-039;DataBase=TP09 REPOSITORY; Trusted_Connection=true;";
    public static Usuario UsuarioLogueado = null;
    
    private static Usuario UsuarioEnBD = new Usuario();
    //cuando inciassecion deberia buscar la carpeta principal del usuario
    public static bool IniciarSesion(string mail,string Contraseña){
        using(SqlConnection db = new SqlConnection(_conectionString)){
            string sql ="SELECT * FROM Usuario WHERE mail = @pmail AND Contraseña = @pContraseña";
            UsuarioEnBD = db.QueryFirstOrDefault<Usuario>(sql,new{pmail = mail, pContraseña = Contraseña});
        }
        if (UsuarioEnBD == null){
            return false;
        }else{
            return true;
        }
    }
    public static Usuario ObtenerUsuario(string mail,string Contraseña){
        using(SqlConnection db = new SqlConnection(_conectionString)){
            string sql ="SELECT * FROM Usuario WHERE mail = @pmail AND Contraseña = @pContraseña";
            UsuarioEnBD = db.QueryFirstOrDefault<Usuario>(sql,new{pmail = mail, pContraseña = Contraseña});
        }
        return UsuarioEnBD;
    }
    //deberia crearle una carpeta principal a cada uno de los usuarios cuando sean creados
    public static void CrearNuevoUsuario (Usuario user){
        string sql = "INSERT INTO Usuario (Nombre,mail,Contraseña) VALUES (@pNombre,@pMail,@pContraseña)";
        using(SqlConnection db = new SqlConnection(_conectionString)){
            db.Execute(sql,new {pNombre = user.Nombre, pMail = user.mail, pContraseña = user.Contraseña});
        }
    }
    public static void CrearCarpetaPrincipal (Usuario user){
        string sql = "INSERT INTO Carpeta (IdUsuario,NombreCarpeta) VALUES (@pIdusuario,@pNombreCarpeta)";
        using(SqlConnection db = new SqlConnection(_conectionString)){
            db.Execute(sql,new {pIdusuario = user.idUsuario, pNombreCarpeta = "CarpetaPrincipal" + user.Nombre});
        }
    }
    public static void CargarFoto (string foto){
        string sql = "INSERT INTO Usuario (Foto) VALUES (@pfoto)";
        using(SqlConnection db = new SqlConnection(_conectionString)){
            db.Execute(sql,new {pfoto = foto});
        }
    }

    public static void CambiarPerfil (Usuario user){
        string sql = "UPDATE Usuario SET Nombre= @pNombre, mail=@pMail, Contraseña=@pContraseña WHERE idUsuario=@pidUsuario";
        using(SqlConnection db = new SqlConnection(_conectionString)){
            db.Execute(sql,new {pNombre = user.Nombre, pMail = user.mail, pContraseña = user.Contraseña, pidUsuario = user.idUsuario});
        }
    }

    public static void NuevoDocumento (Documento doc){
        string sql = "INSERT INTO Documento (idUsuario,IdCarpeta,NombreDoc,TipoDoc,TiempoSubida) VALUES (@pidUsuario,@pIdCarpeta,@pNombreDoc,@pTipoDoc,@pTiempoSubida)";
        using(SqlConnection db = new SqlConnection(_conectionString)){
            db.Execute(sql,new {pidUsuario = doc.IdUsuario,pIdCarpeta = doc.IdCarpeta,pNombreDoc = doc.NombreDoc,pTipoDoc = doc.TipoDoc,pTiempoSubida = doc.TiempoSubida});
        }
    }
    public static List<Documento> ObtenerDocumentos(int idCarpeta){
        List<Documento> listaDocumento = null;
        string sql = "SELECT * FROM Documento WHERE idCarpeta=@pidCarpeta";
        using(SqlConnection db = new SqlConnection(_conectionString))
        {
            listaDocumento = db.Query<Documento>(sql,new{pidCarpeta = idCarpeta}).ToList();
        }
        return listaDocumento;
    }
    public static void NuevaCarpeta (Carpeta carp){
        string sql = "INSERT INTO Documento (pidUsuario, pNombreCarpeta) VALUES (@pidUsuario,@pNombreCarpeta)";
        using(SqlConnection db = new SqlConnection(_conectionString)){
            db.Execute(sql,new {pidUsuario = carp.IdUsuario, pNombreCarpeta = carp.NombreCarpeta});
        }
    }
    public static List<Carpeta> ObtenerCarpetas(){
        List<Carpeta> listaCarpeta = null;
        string sql = "SELECT * FROM Carpeta";
        using(SqlConnection db = new SqlConnection(_conectionString))
        {
            listaCarpeta = db.Query<Carpeta>(sql).ToList();
        }
        return listaCarpeta;
    }
}
