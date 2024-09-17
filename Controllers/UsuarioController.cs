using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using ProyetoInmobiliaria.Models;

[Authorize]
public class UsuarioController: Controller{  
       private readonly ILogger _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }
        
    public IActionResult Index(){
        return View();
    }
    [AllowAnonymous]
    public IActionResult Crear(){        
        
        return View();
    }
    public IActionResult Detalles(int id){
        return View();
    }
    public IActionResult Editar(int id){
        return View();
    }
    [AllowAnonymous]
    public IActionResult Login(){
        return View();
    }
    [AllowAnonymous]
    public IActionResult Guardar(Usuario usuario){
        
        RepositorioUsuario repositorio = new RepositorioUsuario();
        repositorio.Guardar(usuario);
        return RedirectToAction( "Index", "Login");
    }
    
}