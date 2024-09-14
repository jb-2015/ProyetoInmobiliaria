using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.Extensions.Logging; // me sirve para alertar LogWarning

public class ContratoController : Controller
{
    private readonly ILogger<ContratoController> _logger;
    private readonly RepositorioContrato _repo;

    public ContratoController(ILogger<ContratoController> logger)
    {
        _logger = logger;
        _repo = new RepositorioContrato();
    }

  // para ver vista de detalles //creo que no se hace un detalleController, manejamos desde aqui
    public IActionResult Detalles(int id)
    {
        var contrato = _repo.Obtener(id);
        if (contrato == null)
        {
            _logger.LogWarning("No se encontro el contrato con el id: {Id}", id);
            return NotFound(); //  me devuelve error si no hay contrato 
        }

        return View(contrato); // muestra a la vista
    }

    public IActionResult Index()
    {
        _logger.LogInformation("Se invoca el index.");
        var contratos = _repo.Listar();
        return View(contratos);
    }

    // public IActionResult Crear()
    // {
    //    /* RepositorioInmueble _repoInmueble= new RepositorioInmueble();
    //     RepositorioInquilino _repoInquilino = new RepositorioInquilino();
    //     Contrato contrato = new Contrato();

    //     List<Inquilino> inquilinos = _repoInquilino.Listar();
    //     List<Inmueble> inmuebles = _repoInmueble.Listar();
    //     ContratoViewModel cvm = new ContratoViewModel {
    //         Inquilinos = inquilinos,
    //         Inmuebles = inmuebles,
    //         Contrato = new Contrato(),
    //         Inmueble = new  Inmueble(),
    //         Inquilino = new Inquilino()
    //     };
    //     return View(cvm);*/
    //     return View();
    // }
    public IActionResult Crear(int id_i){
        
        RepositorioInmueble _repoInmueble= new RepositorioInmueble();
        RepositorioInquilino _repoInquilino = new RepositorioInquilino();
        Contrato contrato = new Contrato();

        List<Inquilino> inquilinos = _repoInquilino.Listar() ?? new List<Inquilino>();
        List<Inmueble> inmuebles = _repoInmueble.Listar() ?? new List<Inmueble>();

        Inmueble inmueble = _repoInmueble.Obtener(id_i);

        ContratoViewModel cvm = new ContratoViewModel {
            Inquilinos = inquilinos,
            Inmuebles = inmuebles,
            Contrato = contrato,
            Inmueble = inmueble,
            Inquilino = new Inquilino()
        };
        return View(cvm);
    }

    [HttpPost]
    public IActionResult Guardar(ContratoViewModel cvm){
        try{
            if(cvm.Contrato.IdContrato == 0){
                int idCreado = _repo.Crear(cvm.Contrato);
                if(idCreado > 0){
                    _logger.LogInformation("Se ha creado un nuevo contrato: ", cvm.Contrato.IdContrato);
                }
                // _logger.LogInformation("Se ha creado un nuevo contrato", cvm.Contrato.IdContrato);
            }else{
                _repo.Modificar(cvm.Contrato);
                _logger.LogInformation("Se ha modificado el contrato con id: {Id}", cvm.Contrato.IdContrato);
            }
        }catch (Exception ex){
            _logger.LogError(ex, "Ha ocurrido un error al tratar de guardar el contrato", cvm.Contrato.IdContrato);
            ModelState.AddModelError("",  "Oops ha ocurrido un error al intentar guardar el inmueble"); // muetsro model de aviso
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Borrar(int id)
    {
        try
        {
            _repo.Eliminar(id);
            _logger.LogInformation("Se ha eliminado el contrato con id: {Id}", id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el inmueble", id);
            return RedirectToAction("Index");
        }
    }
}