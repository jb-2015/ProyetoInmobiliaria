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

    public IActionResult Crear()
{
    return View(); // Devuelve la vista para crear un nuevo propietario
}


    [HttpPost]
    public IActionResult Guardar(int Id, Propietario propietario){
        Id = propietario.IdPropietario;
        propietario.Estado= true;
        if(Id == 0){
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