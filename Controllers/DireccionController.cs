using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

public class DireccionController : Controller
{
    private readonly ILogger<DireccionController> _logger;
    private readonly RepositorioDireccion _repo;

    public DireccionController(ILogger<DireccionController> logger)
    {
        _logger = logger;
        _repo = new RepositorioDireccion();
    }

    // para la vista de detalles 
    public IActionResult Detalles(int id)
    {
        var direccion = _repo.Obtener(id);
        if (direccion == null)
        {
            _logger.LogWarning("No se encontró la dirección con el id: {Id}", id);
            return NotFound(); // Devuelve un error 404 si la dirección no se encuentra
        }

        return View(direccion); 
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Se invoca el index.");
        var direcciones = _repo.Listar();
        return View(direcciones);
    }

    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Guardar(Direccion direccion)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (direccion.IdDireccion == 0)
                {
                    _repo.Crear(direccion);
                    _logger.LogInformation("Se ha creado una nueva dirección con id: {Id}", direccion.IdDireccion);
                }
                else
                {
                    _repo.Modificar(direccion);
                    _logger.LogInformation("Se ha modificado la dirección con id: {Id}", direccion.IdDireccion);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ha ocurrido un error al tratar de guardar la dirección con id: {Id}", direccion.IdDireccion);
                ModelState.AddModelError("", "Oops ha ocurrido un error al intentar guardar la dirección");
            }
        }
        return View("Crear", direccion);
    }

    [HttpPost]
    public IActionResult Borrar(int id)
    {
        try
        {
            _repo.Eliminar(id);
            _logger.LogInformation("Se ha eliminado la dirección con id: {Id}", id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar la dirección con id: {Id}", id);
            return RedirectToAction("Index");
        }
    }
}