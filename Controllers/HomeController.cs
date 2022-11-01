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
            return View("HomePage");    
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

    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
