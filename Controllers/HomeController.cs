﻿using System.Diagnostics;
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

    public IActionResult IniciarSesion(string mail, string contraseña) //DEVOLVER ERROR SI ESTA MAL
    {
        
        if (BD.IniciarSesion(mail, contraseña))
        {
            Usuario user = BD.ObtenerUsuario(mail, contraseña);
            BD.UsuarioLogueado = user;
            return RedirectToAction("HomePage");  
        }else{
            ViewBag.Error = "Error al iniciar sesion";
            return View("Index");
        }
        return RedirectToAction("ObtenerCarpetas");
    }

/*                  ARREGLAR 
*/
    public IActionResult ActualizarPerfil(Usuario user){
        BD.CambiarPerfil(user);
        Usuario user2 = BD.ObtenerUsuario(user.mail, user.Contraseña);
        BD.UsuarioLogueado = user2;
        return View ("Perfil");

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
    
    public IActionResult HomePage()
    {
        ViewBag.ListaCarpetas=BD.ObtenerCarpetas();
        ViewBag.Usuario = BD.UsuarioLogueado;
        return View("HomePage");
    }


    [HttpPost]
    public IActionResult CargarFotoPerfil(Usuario user, IFormFile MyFile){
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
        return View("Perfil");
    }
    [HttpPost]
    public IActionResult SubirArchivo( IFormFile MyFile){
        /*input type file solo te deja subir archivos por lo que en vez de subir tambien carpetas te deberia dejar crear una carpeta donde puedas guardar las distintas cosas*/
        /*Subir el nombre del archivo a la base de datos y en la clase(tiene que pasar el Usuario tambien el form)*/
        if(MyFile.Length>0){
            string wwwRootLocal = this.Enviroment.ContentRootPath + @"\wwwroot\" + MyFile.FileName;
            using (var stream = System.IO.File.Create(wwwRootLocal))
            {
                MyFile.CopyTo(stream);
            }
        }
        return View("Index");
    } 

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
