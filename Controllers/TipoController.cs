using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

public class TipoController: Controller{
    
    private readonly ILogger<TipoController> _logger;
    private readonly RepositorioTipo _repo;

    public TipoController(ILogger<TipoController> logger)
    {
        _logger = logger;
        _repo = new RepositorioTipo();
    }

    public IActionResult Index(){
        var tipos = _repo.Listar();
        return View(tipos);
    }

    public IActionResult Crear(){
        return View();
    }

    [HttpPost]
    public IActionResult Guardar(Tipo tipo){
        if (ModelState.IsValid){
            try{
                if (tipo.IdTipo == 0){
                    _repo.Crear(tipo);
                }else{
                    _repo.Modificar(tipo);
                }
                return RedirectToAction("Index");
            }
            catch (Exception){
                ModelState.AddModelError("", "Oops ha ocurrido un error al intentar guardar el tipo");
            }
        }
        return View("Crear", tipo);
    }

    [HttpPost]
    public IActionResult Borrar(int id){
        try{
            _repo.Eliminar(id);
            _logger.LogInformation("Se ha eliminado el tipo con id: {Id}", id);
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex, "Error al eliminar el tipo con id: {Id}", id);
            return RedirectToAction("Index");
        }
    }
    
    public IActionResult Editar(int Id){
        Tipo tipo = _repo.Obtener(Id);
        if(tipo != null){
            return View(tipo);
        }
        return RedirectToAction("Index");
    }
}