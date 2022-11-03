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
        ViewBag.usuario=user;
        return RedirectToAction("ObtenerCarpetas");
    }

/*                  ARREGLAR 
    public IActionResult ActualizarPerfil(Usuario user){
        CambiarPerfil(user);
        return View ("HomePage");
    }
*/
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
    
    public IActionResult ObtenerCarpetas()
    {
        ViewBag.ListaCarpetas=BD.ObtenerCarpetas();
        return View("HomePage");
    }

    [HttpPost]
    public IActionResult CargarFotoPerfil(Usuario user, IFormFile MyFile){
        if(MyFile.Length>0)
        {
            string wwwRootLocal = this.Enviroment.ContentRootPath + @"wwwroot\" + MyFile.FileName;
            using (var stream = System.IO.File.Create(wwwRootLocal))
            {
                MyFile.CopyTo(stream);
            }
            user.Foto=MyFile.FileName;
        }
        BD.CargarFoto(MyFile.FileName);
        return View("Perfil");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
