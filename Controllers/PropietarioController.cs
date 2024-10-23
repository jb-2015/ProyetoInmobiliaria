//propietarioController
using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

[Authorize]

public class PropietarioController : Controller{
    private readonly ILogger<PropietarioController> _logger;
    private RepositorioPropietario repo;

    public PropietarioController(ILogger<PropietarioController> logger){
        _logger = logger;
        repo = new RepositorioPropietario();  //CREAR DESDE EL MODELO
    }

    public IActionResult Index(){
        var propietarios = repo.Listar();
        return View(propietarios);
    }

    public IActionResult Editar(int Id){
        Propietario propietario = repo.Obtener(Id);
        if(propietario != null){
            return View(propietario);
        }
        return RedirectToAction("Index", "Home");
    }
    


    public IActionResult Detalle(int id){
        Propietario propietario = repo.Obtener(id);
        if(propietario != null){
            return View(propietario);
        }
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Crear(){
        return View(); // Devuelve la vista para crear un nuevo propietario
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult Alta(int Id){
        try{
            Propietario p = repo.Obtener(Id);
            p.Estado = true;
            int filasAfectadas = repo.Modificar(p);
        }catch (System.Exception){
            _logger.LogInformation("Propietario/Alta/ error al dar de alta al propietario");            
        }
        return RedirectToAction("Index", "Propietario");
    }

    [HttpPost]
    public IActionResult Guardar(Propietario propietario){
        if(ModelState.IsValid){
            propietario.Estado = true;
            if(propietario.IdPropietario == 0){
                repo.Crear(propietario);
            }else{
                repo.Modificar(propietario);
            }
            return RedirectToAction("Index");
        }
        if(propietario.IdPropietario == 0){
            return View("Crear", propietario);
        }else{
            return View("Editar", propietario);
        }
    }


    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult Borrar(int Id){
        repo.Eliminar(Id);
        return RedirectToAction("Index");
    }    

    public JsonResult DadosDeBaja(){
        var propietarios = repo.DadosDeBaja();
        return Json(propietarios);
    }

    public JsonResult GetPropietarios(){
        var propietarios = repo.Listar();
        return Json(propietarios);
    }
    public JsonResult GetPropietariosPorDni(String Dni){
        var propietarios = repo.ListarPorDni(Dni);
        return Json(propietarios);
    }
    public JsonResult GetPropietariosPorApellido(String Apellido){
        var propietarios = repo.ListarPorApellido(Apellido);
        return Json(propietarios);
    }
    public JsonResult GetPropietariosPorEmail(String Email){
        var propietarios = repo.ListarPorEmail(Email);
        return Json(propietarios);
    }

    // Endpoint para listar todos los propietarios
    [HttpGet("Api_propietarios")]
    public IActionResult Api_propietarios()
    {
        var propietarios = repo.Listar();
        return Json(propietarios);
    }

    // Endpoint para obtener un propietario por ID
    [HttpGet("Api_propietarios/{id}")]
    public IActionResult Api_propietarios(int id)
    {
        var propietario = repo.Obtener(id);
        return Json(propietario);
    }
    

    
}