namespace tp09proyectofinal.Models;

public class Carpeta{
    private int _idCarpeta;private string _idUsuario;private string _NombreCarpeta;
    public Carpeta(){}
    public Carpeta(int pidCarpeta,string pidUsuario,string pNombreCarpeta){
        _idCarpeta = pidCarpeta;
        _idUsuario = pidUsuario;
        _NombreCarpeta = pNombreCarpeta;
    }

    public int IdCarpeta{
        get{return _idCarpeta;}
        set {_idCarpeta = value;}
    }
    public string IdUsuario{
        get{return _idUsuario;}
        set {_idUsuario = value;}
    }
    public string NombreCarpeta{
        get{return _NombreCarpeta;}
        set {_NombreCarpeta = value;}
    }
}