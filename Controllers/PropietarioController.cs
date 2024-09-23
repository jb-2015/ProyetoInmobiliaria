//propietarioController
using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;

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
        if(Id == 0){
            return View();
        }else{
            return View(repo.Obtener(Id));
        }
    }


    public IActionResult Detalle(int id){
        Propietario propietario = null;
        try{
            propietario = repo.Obtener(id);
        }catch (System.Exception){
            _logger.LogInformation("Propietario/Detalle/Error al obtener el propietario.");
        }
        return View(propietario);
    }

    public IActionResult Crear(){
        return View(); // Devuelve la vista para crear un nuevo propietario
    }

    [HttpPost]
    public IActionResult Alta(int Id){
        Propietario p = null; 
        try{
            p = repo.Obtener(Id);
            _logger.LogInformation("Estado: "+p.Estado);            
            p.Estado = true;
            _logger.LogInformation("Estado: "+p.Estado);            
            repo.Modificar(p);
        }catch (System.Exception){
            _logger.LogInformation("Propietario/Alta/ error al dar de alta al propietario");            
        }
        return RedirectToAction("Index", "Propietario");
    }

    [HttpPost]
    public IActionResult Guardar(int Id, Propietario propietario){
        Id = propietario.IdPropietario;
        propietario.Estado= true;
        if(Id == 0){
            propietario.Estado = true; // Asigna estado solo al crear
            repo.Crear(propietario);
        }else{
            repo.Modificar(propietario);
        }
        return RedirectToAction("Index");
    }

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
}