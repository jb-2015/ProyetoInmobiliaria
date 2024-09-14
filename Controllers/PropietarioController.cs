//propietarioController
using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
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
            _logger.LogInformation("Propietario: "+p.NombreYApellido);            
            _logger.LogInformation("Estado: "+p.Estado);            
            p.Estado = true;
            _logger.LogInformation("Estado: "+p.Estado);            
            repo.Modificar(p);
        }catch (System.Exception){
            _logger.LogInformation("Propietario/Alta/ error al dar de alta al propietario");            
        }
        return RedirectToAction("Detalle", "Propietario", new {Id = p.IdPropietario});
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
}