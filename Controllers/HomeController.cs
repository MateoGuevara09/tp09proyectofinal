using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp09proyectofinal.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace tp09proyectofinal.Controllers;

public class HomeController : Controller
{   
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult CrearCuenta()
    {
        return View();
    }

    public IActionResult HomePage()
    {
        return View();
    }

    public IActionResult Perfil()
    {
        return View();
    }

    public IActionResult IniciarSesion(string mail, string contraseña) //DEVOLVER ERROR SI ESTA MAL
    {
        Usuario user = BD.IniciarSesion(mail, contraseña);
        while (user==null)
        {
            return View("Index");
        }
        return View("HomePage");
    }
    
    public IActionResult CrearNuevaCuenta(string email, string nombre, string contraseña1, string contraseña2)
    {
        Usuario user = null;
        user.mail=email; //FIJARSE SI ESTA BIEN
        user.Nombre=nombre;
        if(contraseña1==contraseña2)
        {
            user.Contraseña=contraseña1;
            BD.CrearNuevoUsuario(user);
            return View("Index");
        }
        else
        {
            return View("CrearCuenta"); //CAMBIAR A QUE TIRE ERROR
        }
    }

    public IActionResult CargarFotoPerfil(IFormFile myFile){
        if(myFile.Length>0)
        {
            string wwwRootLocal = this.Enviroment.ContentRootPath + @"wwwroot\" + myFile.FileName;
            using (var stream = System.IO.File.Create(wwwRootLocal))
            {
                myFile.CopyTo(stream);
            }
            Usuario.Foto=myFile.FileName;
        }
        BD.CargarFoto(myFile.FileName);
        return View("Perfil");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
