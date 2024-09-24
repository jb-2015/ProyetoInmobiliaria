using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Authorize]
public class ContratoController : Controller{
    private readonly ILogger<ContratoController> _logger;
    private readonly RepositorioContrato _repo;
    private readonly RepositorioInmueble _repoInmueble;
    private readonly RepositorioInquilino _repoInquilino;
    public ContratoController(ILogger<ContratoController> logger){
        _logger = logger;
        _repo = new RepositorioContrato();
        _repoInmueble = new RepositorioInmueble();
        _repoInquilino = new RepositorioInquilino();
    }

    public IActionResult Detalles(int id){
        var contrato = _repo.Obtener(id);
        if (contrato != null){
            return View(contrato); // muestra a la vista
        }
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Index(){
        List<Contrato> contratos = _repo.Listar();
        List<Inmueble> inmuebles = _repoInmueble.Listar();
        InmuebleContrato ic = new InmuebleContrato{
            Contratos = contratos,
            Inmuebles = inmuebles
        };
        return View(ic);
    }

    //filtros 
    public JsonResult GetPorInmueble(int IdInmueble){
        List<Contrato>contratos = _repo.ListarPorInmueble(IdInmueble);
        return Json(contratos);
    }
    
    //filtros 
    public JsonResult GetVigentesPorFechas(DateTime fechaInicio, DateTime fechaFin){
        List<Contrato>contratos = _repo.ListarPorFechas(fechaInicio, fechaFin);
        return Json(contratos);
    }
    
    //filtros 
    public JsonResult GetVigentesDentroDe(DateTime hoy, DateTime tantosDias){
        List<Contrato>contratos = _repo.ListarPorVigencia(hoy, tantosDias);
        return Json(contratos);
    }

    

    public IActionResult Crear(int id){
        List<Inquilino> inquilinos = _repoInquilino.Listar();
        List<Inmueble> inmuebles = _repoInmueble.Listar();
        Inmueble inmueble = _repoInmueble.Obtener(id);
        ContratoViewModel cvm = new ContratoViewModel {
            Inquilinos = inquilinos,
            Inmuebles = inmuebles,
            Inmueble = inmueble
        };
        return View(cvm);
    }


    [HttpPost]
    public IActionResult Guardar(ContratoViewModel cvm){
            if(cvm.Contrato.FechaInicio >= cvm.Contrato.FechaFin){
                return RedirectToAction("Crear", "Contrato");
            }else{
                if(cvm.Contrato.IdContrato == 0){
                    int IdUsuario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    int IdCreado = _repo.Crear(cvm.Contrato, IdUsuario);
                    if(IdCreado > 0){
                        _logger.LogInformation("Se ha creado un nuevo contrato: {IdCreado}", IdCreado);
                        return RedirectToAction("Detalles", "Inmueble", new {id = cvm.Contrato.IdInmueble});
                    }
                }else{
                    _repo.Modificar(cvm.Contrato);
                }
            }
            return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Borrar(int id){
        try{
            _repo.Eliminar(id);
            _logger.LogInformation("Se ha eliminado el contrato con id: {Id}", id);
            return RedirectToAction("Index");
        }catch (Exception ex){
            _logger.LogError(ex, "Error al eliminar el inmueble", id);
            return RedirectToAction("Index");
        }
    }
}