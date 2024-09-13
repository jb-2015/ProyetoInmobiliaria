using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.Extensions.Logging;

public class TipoController: Controller{
    
    private readonly ILogger<TipoController> _logger;
    private readonly RepositorioTipo _repo;

    public TipoController(ILogger<TipoController> logger)
    {
        _logger = logger;
        _repo = new RepositorioTipo();
    }

    // para la vista de detalles del tipo
    public IActionResult Detalles(int id)
    {
        var tipo = _repo.Obtener(id);
        if (tipo == null)
        {
            _logger.LogWarning("No se encontró el tipo con el id: {Id}", id);
            return NotFound(); // Devuelve un error 404 si el tipo no se encuentra
        }

        return View(tipo); 
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Se invoca el index.");
        var tipos = _repo.Listar();
        return View(tipos);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Guardar(Tipo tipo)
    {
        if (ModelState.IsValid){
            try{
                if (tipo.IdTipo == 0){
                    _repo.Crear(tipo);
                    _logger.LogInformation("Se ha creado un nuevo tipo con id: {Id}", tipo.IdTipo);
                }else{
                    _repo.Modificar(tipo);
                    _logger.LogInformation("Se ha modificado el tipo con id: {Id}", tipo.IdTipo);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex){
                _logger.LogError(ex, "Ha ocurrido un error al tratar de guardar el tipo con id: {Id}", tipo.IdTipo);
                ModelState.AddModelError("", "Oops ha ocurrido un error al intentar guardar el tipo");
            }
        }
        return View("Crear", tipo);
    }

    [HttpPost]
    public IActionResult Borrar(int id)
    {
        try
        {
            _repo.Eliminar(id);
            _logger.LogInformation("Se ha eliminado el tipo con id: {Id}", id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el tipo con id: {Id}", id);
            return RedirectToAction("Index");
        }
    }
    public IActionResult Editar(int Id){
        try{
            if(Id == 0){
                return View();
            }else{
                return View(_repo.Obtener(Id));
            }
        }catch(Exception e){
            return RedirectToAction("Index");
        }
    }
}