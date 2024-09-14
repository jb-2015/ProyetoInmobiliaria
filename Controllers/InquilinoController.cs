
using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;

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

    public IActionResult Detalle(int id){
        Inquilino inquilino = null;
        try{
            inquilino = repo.Obtener(id);
        }catch (System.Exception){
            _logger.LogInformation("inquilino/Detalle/Error al obtener el inquilino.");
        }
        return View(inquilino);
    }

    public IActionResult Editar(int Id){
        if(Id == 0){
            return View();
        }else{
            return View(repo.Obtener(Id));
        }
    }

    public IActionResult Crear(){
        return View();
    }

    [HttpPost]
    public IActionResult Alta(int Id){
        Inquilino inquilino = null;
        try{
            inquilino = repo.Obtener(Id);
            inquilino.Estado = true;
            repo.Modificar(inquilino);
            return RedirectToAction("Detalle", "Inquilino", new {Id = inquilino.IdInquilino});
        }catch (System.Exception){
            _logger.LogInformation("Inquilino/Alta error al dar de alta");
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Guardar(int Id, Inquilino inquilino){
        Id = inquilino.IdInquilino;
        inquilino.Estado= true;
        if(Id == 0){
            repo.Crear(inquilino);
        }else{
            repo.Modificar(inquilino);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Borrar(int Id){
        repo.Eliminar(Id);
        return RedirectToAction("Index");
    }    
}