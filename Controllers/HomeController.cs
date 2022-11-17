using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp09proyectofinal.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace tp09proyectofinal.Controllers;

public class HomeController : Controller
{   
        private IWebHostEnvironment Enviroment;

    public HomeController(IWebHostEnvironment eviroment)
    {
        Enviroment=eviroment;
    }

    public IActionResult Index()
    {
        ViewBag.Error = null;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult CrearCuenta()
    {
        ViewBag.Error = null;
        return View();
    }


    public IActionResult Perfil()
    {
        return View();
    }

    public IActionResult IniciarSesion(string mail, string contraseña)
    {
        
        if (BD.IniciarSesion(mail, contraseña))
        {
            Usuario user = BD.ObtenerUsuario(mail, contraseña);
            BD.UsuarioLogueado = user;
            BD.UsuarioLogueado.Foto="/perfil.png";
            return RedirectToAction("HomePage");  
        }else{
            ViewBag.Error = "Error al iniciar sesion";
            return View("Index");
        }
        return RedirectToAction("HomePage");
    }

//          HACER QUE SI ALGUNA COSA ES NULL NO LA ACTUALICE 
    public IActionResult CrearNuevaCuenta(Usuario user)
    {

        if(user.Contraseña==user.Contraseña2)
        {
            BD.CrearNuevoUsuario(user);
            BD.CrearCarpetaPrincipal(user);
            return View("Index");
        }
        else
        {
            ViewBag.Error = "Escribio mal la contraseña en alguno de los dos campos";
            return View("CrearCuenta"); 
        }
    }
    
    public IActionResult HomePage()
    {  
        ViewBag.Usuario = BD.UsuarioLogueado;
        Carpeta CarpetaPrincipalActual = BD.ObtenerCarpeta(BD.UsuarioLogueado);
        string path = this.Enviroment.ContentRootPath + @"\wwwroot\" + CarpetaPrincipalActual.NombreCarpeta;
        if(!(Directory.Exists(path))){
            Directory.CreateDirectory(path);
        }
        ViewBag.ListaDocumentos = BD.ObtenerDocumentos(CarpetaPrincipalActual.IdCarpeta);
        return View("HomePage");
    }

    [HttpPost]
    public IActionResult ActualizarPerfil(Usuario user, IFormFile MyFile){
        BD.CambiarPerfil(user);
        BD.UsuarioLogueado = user;
        if(MyFile.Length>0)
        {
            string wwwRootLocal = this.Enviroment.ContentRootPath + @"\wwwroot\" + MyFile.FileName;
            using (var stream = System.IO.File.Create(wwwRootLocal))
            {
                MyFile.CopyTo(stream);
            }
            user.Foto=MyFile.FileName;
        }
        BD.CargarFoto(MyFile.FileName);
        return RedirectToAction("HomePage");
    }

    [HttpPost]
    public IActionResult SubirArchivo( int IdUsuario,IFormFile MyFile){
        Carpeta CarpetaPrincipalActual = BD.ObtenerCarpeta(BD.UsuarioLogueado);
        if(MyFile.Length>0){
            string wwwRootLocal = this.Enviroment.ContentRootPath + @"\wwwroot\" +CarpetaPrincipalActual.NombreCarpeta + @"\" + MyFile.FileName;
            using (var stream = System.IO.File.Create(wwwRootLocal))
            {
                MyFile.CopyTo(stream);
            }
            Documento NuevoDocumento = new Documento(IdUsuario,CarpetaPrincipalActual.IdCarpeta,MyFile.FileName,MyFile.ContentType,DateTime.UtcNow.ToString("MM-dd-yyyy"));
            BD.NuevoDocumento(NuevoDocumento);
        }
        return RedirectToAction("HomePage");
    } 

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
