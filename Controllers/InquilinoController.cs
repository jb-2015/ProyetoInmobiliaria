
using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize]
public class InquilinoController : Controller{
    private readonly ILogger<InquilinoController> _logger;
    private RepositorioInquilino repo;
    public InquilinoController(ILogger<InquilinoController> logger){
        _logger = logger;
        repo = new RepositorioInquilino();
    }        

    public IActionResult Index(){
        var inquilinos = repo.Listar();
        return View(inquilinos);
    }

    public JsonResult DadosDeBaja(){
        var inquilinos = repo.DadosDeBaja();
        return Json(inquilinos);
    }

    public JsonResult GetInquilinos(){
        var inquilinos = repo.Listar();
        return Json(inquilinos);
    }
    [HttpGet]
    public JsonResult GetInquilinosPorDni(string Dni){
        var inquilinos = repo.ListarPorDni(Dni);
        return Json(inquilinos);
    }
    
    [HttpGet]
    public JsonResult GetInquilinosPorApellido(string Apellido){
        var inquilinos = repo.ListarPorApellido(Apellido);
        return Json(inquilinos);
    }

    [HttpGet]
    public JsonResult GetInquilinosPorEmail(string Email){
        var inquilinos = repo.ListarPorEmail(Email);
        return Json(inquilinos);
    }


    public IActionResult Detalle(int id){
        Inquilino inquilino = repo.Obtener(id);
        if(inquilino != null){
            return View(inquilino);
        }
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Editar(int Id){
        Inquilino inquilino = repo.Obtener(Id);
        if(inquilino != null){
            return View(repo.Obtener(Id));
        }
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Crear(){
        return View();
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult Alta(int Id){
        Inquilino inquilino = null;
        try{
            inquilino = repo.Obtener(Id);
            inquilino.Estado = true;
            repo.Modificar(inquilino);
            return RedirectToAction("Index", "Inquilino");
        }catch (System.Exception){
            _logger.LogInformation("Inquilino/Alta error al dar de alta");
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Guardar(Inquilino inquilino){
        if(ModelState.IsValid){
            inquilino.Estado= true;
            if(inquilino.IdInquilino == 0){
                repo.Crear(inquilino);
            }else{
                repo.Modificar(inquilino);
            }
            return RedirectToAction("Index");
        }
        return View("Crear", inquilino);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult Borrar(int Id){
        repo.Eliminar(Id);
        return RedirectToAction("Index");
    }    
}