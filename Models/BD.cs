using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Dapper;

namespace tp09proyectofinal.Models;

public static class BD{

    //CAMBIAR LA COMPUTADORA PARA QUE FUNCIONE
    private static string _conectionString = @"Server=A-PHZ2-CIDI-048;DataBase=TP09 REPOSITORY; Trusted_Connection=true;";
    
    private static usuario UsuarioEnBD;
    public static bool CheckearUsuario(string mail,string Contraseña){
        using(SqlConnection db = new SqlConnection(_conectionString)){
            string sql ="SELECT * FROM Usuario WHERE mail = @pmail AND Contraseña = @pContraseña";
            UsuarioEnBD = db.Query<usuario>(sql,new{pmail = mail, pContraseña = Contraseña});
            //Arreglar
        }
        return listaSeries;
    }
}
