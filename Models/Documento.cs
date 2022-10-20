namespace tp09proyectofinal.Models;

public class Documento{
    private int _idDoc;private string _NombreDoc;private int _idUsuario;private int _idCarpeta;
    public Documento(){}
    public Documento(int pidDoc, string pNombreDoc, int pidUsuario, int pidCarpeta){
        _idDoc=pidDoc; _NombreDoc=pNombreDoc; _idUsuario=pidUsuario; _idCarpeta=pidCarpeta;
    }

    public int IdDoc{
        get{return _idDoc;}
        set {_idDoc = value;}
    }
    public string NombreDoc{
        get{return _NombreDoc;}
        set {_NombreDoc = value;}
    }
    public int IdUsuario{
        get{return _idUsuario;}
        set {_idUsuario = value;}
    }
    public int IdCarpeta{
        get{return _idCarpeta;}
        set {_idCarpeta = value;}
    }
}