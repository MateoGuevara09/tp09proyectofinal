namespace tp09proyectofinal.Models;

public class Documento{
    private int _idDoc;private string _NombreDoc;private int _idUsuario;private int _idCarpeta; private string _TipoDoc; private string _TiempoSubida;
    public Documento(){}
    public Documento(int pidUsuario, int pidCarpeta,string pNombreDoc,string pTipoDoc, string pTiempoSubida){
          _idUsuario=pidUsuario; _idCarpeta=pidCarpeta;_NombreDoc=pNombreDoc; _TipoDoc = pTipoDoc; _TiempoSubida = pTiempoSubida;
    }

    public int IdDoc{
        get{return _idDoc;}
        set {_idDoc = value;}
    }
    public int IdUsuario{
        get{return _idUsuario;}
        set {_idUsuario = value;}
    }
    public int IdCarpeta{
        get{return _idCarpeta;}
        set {_idCarpeta = value;}
    }
    public string NombreDoc{
        get{return _NombreDoc;}
        set {_NombreDoc = value;}
    }
    public string TipoDoc{
        get{return _TipoDoc;}
        set{_TipoDoc = value;}
    }
    public string TiempoSubida{
        get{return _TiempoSubida;}
        set{_TiempoSubida = value;}
    }
    
}