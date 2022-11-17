using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tp09proyectofinal.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

/*
AJAX LISTA:
- MOSTRAR ARCHIVO
- MOSTRAR ARCHIVO SUBIDO
*/

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
            BD.UsuarioLogueado.Foto="perfil.png";
            return RedirectToAction("HomePage");  
        }else{
            ViewBag.Error = "Error al iniciar sesion";
            return View("Index");
        }
        return RedirectToAction("HomePage");
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
    public IActionResult ActualizarPerfil(Usuario user, IFormFile MyFile){
        
        BD.UsuarioLogueado = user;
        if(MyFile==null){
            BD.UsuarioLogueado.Foto="perfil.png";
        }
        else{
            if(MyFile.Length>0)
            {
                string wwwRootLocal = this.Enviroment.ContentRootPath + @"\wwwroot\" + MyFile.FileName;
                using (var stream = System.IO.File.Create(wwwRootLocal))
                {
                    MyFile.CopyTo(stream);
                }
                user.Foto=MyFile.FileName;
            }
        }
        BD.CambiarPerfil(user);
        return View("Perfil");
    }

    /*input type file solo te deja subir archivos por lo que en vez de subir tambien carpetas te deberia dejar crear una carpeta donde puedas guardar las distintas cosas*/
    /*la carpeta que deberia ser pasada tendria que ser una que sea la "carpetaactual" osea que si tocas una dentro de la lista entonces cambiaria la carpeta actual y si no tocas ninguna entonces deberia volver a la principal AYUDAAAAAAAAAAAAAAAA*/
    [HttpPost]
    public IActionResult SubirArchivo( int IdUsuario,IFormFile MyFile){
        if(MyFile.Length>0){
            /*cambiar donde se guarda el archivo dependiendo de donde se sube en las carpetas y la carpeta principal de cada usuario*/
            string wwwRootLocal = this.Enviroment.ContentRootPath + @"\wwwroot\" + MyFile.FileName;
            using (var stream = System.IO.File.Create(wwwRootLocal))
            {
                MyFile.CopyTo(stream);
            }
            /*Tiene que pasar tambien la id de la carpeta dependendiendo de donde fue hecho el archivo con la "carpetaactual"*/
            Documento NuevoDocumento = new Documento(IdUsuario,3,MyFile.FileName,MyFile.ContentType,DateTime.Now);
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
