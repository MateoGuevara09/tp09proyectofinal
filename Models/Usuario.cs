namespace tp09proyectofinal.Models;

public class Usuario{
    private int _idUsuario;private string _Nombre;private string _Mail;private string _Contraseña;private string _foto;
    public Usuario(){}
    public Usuario(int pidUsuario,string pNombre,string pMail, string pContraseña, string pfoto){
        _idUsuario=pidUsuario; 
        _Nombre=pNombre; 
        _Mail=pMail; 
        _Contraseña=pContraseña;
        _foto=pfoto;
    }

    public int idUsuario{
        get{return _idUsuario;}
        set {_idUsuario = value;}
    }
    public string Nombre{
        get{return _Nombre;}
        set {_Nombre = value;}
    }
    public string mail{
        get{return _Mail;}
        set {_Mail = value;}
    }
    public string Contraseña{
        get{return _Contraseña;}
        set {_Contraseña = value;}
    }
    public string Foto{
        get{return _foto;}
        set {_foto = value;}
    }
}