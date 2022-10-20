namespace tp09proyectofinal.Models;

public class usuario{
    private int _idUsuario;private string _Nombre;private string _Mail;private string _Contraseña;
    public usuario(){}
    public usuario(int pidUsuario,string pNombre,string pMail, string pContraseña){
        _idUsuario=pidUsuario; 
        _Nombre=pNombre; 
        _Mail=pMail; 
        _Contraseña=pContraseña;
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
}