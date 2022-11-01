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
        if (BD.IniciarSesion(mail, contraseña))
        {
            return RedirectToAction("ObtenerCarpetas");  
        }else{
            ViewBag.Error = "Error al iniciar sesion";
            return View("Index");
        }
    }
    
    public IActionResult CrearNuevaCuenta(Usuario user)
    {

        if(user.Contraseña==user.Contraseña2)
        {
            BD.CrearNuevoUsuario(user);
            return View("Index");
        }
        else
        {
            ViewBag.Error = "Escribio mal la contraseña en alguno de los dos campos";
            return View("CrearCuenta"); 
        }
    }
    
    public IActionResult ObtenerCarpetas()
    {
        ViewBag.ListaCarpetas=BD.ObtenerCarpetas();
        return View("HomePage");
    }
/*
    public IActionResult CargarFotoPerfil(Usuario user, IFormFile myFile){
        if(myFile.Length>0)
        {
            string wwwRootLocal = this.Enviroment.ContentRootPath + @"wwwroot\" + myFile.FileName;
            using (var stream = System.IO.File.Create(wwwRootLocal))
            {
                myFile.CopyTo(stream);
            }
            User.Foto=myFile.FileName;
        }
        BD.CargarFoto(myFile.FileName);
        return View("Perfil");
    }
    */
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
