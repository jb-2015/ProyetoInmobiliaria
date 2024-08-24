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

    public IActionResult Editar(int Id){
        if(Id == 0){
            return View();
        }else{
            return View(repo.Obtener(Id));
        }
    }

    [HttpPost]
    public IActionResult Guardar(int Id, Inquilino inquilino){
        Id = inquilino.IdInquilino;
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