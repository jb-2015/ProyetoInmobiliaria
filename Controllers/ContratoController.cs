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
    private readonly RepositorioPago _repoPago;
    public ContratoController(ILogger<ContratoController> logger){
        _logger = logger;
        _repo = new RepositorioContrato();
        _repoInmueble = new RepositorioInmueble();
        _repoInquilino = new RepositorioInquilino();
        _repoPago = new RepositorioPago();
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


    [HttpPost]
    public JsonResult AnularContrato([FromBody] AnularContratoRequest request){
        Console.WriteLine($@"IdContrato {request.IdContrato} Monto multa ${request.multa} fechaAnulacion {request.fechaAnulacion}");
        Contrato contrato = _repo.Obtener(request.IdContrato);
        bool ok = false;
        if(contrato != null){
            contrato.FechaAnulacion = request.fechaAnulacion;
            _repo.Modificar(contrato);
            int IdUsuario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            int idCreado = _repoPago.Crear(new Pago{
                contrato = contrato,
                Estado = true,
                Detalle = "Multa por contrato anulado",
                FechaPago = request.fechaAnulacion,
                Importe = request.multa,
                IdContrato = request.IdContrato,
                NumeroPago = "0"
            }, IdUsuario);
            if(idCreado > 0){ok=true;}else{ok=false;}
        }
        return Json(ok);
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
        if(ModelState.IsValid){
            if(cvm.Contrato.FechaInicio >= cvm.Contrato.FechaFin){
                return View("Crear", cvm);
            }else{
                if(cvm.Contrato.IdContrato == 0){
                    int IdUsuario = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    int IdCreado = _repo.Crear(cvm.Contrato, IdUsuario);
                    if(IdCreado > 0){
                        return RedirectToAction("Detalles", "Inmueble", new {id = cvm.Contrato.IdInmueble});
                    }
                }else{
                    _repo.Modificar(cvm.Contrato);
                    return RedirectToAction("Detalles", "Inmueble", new { id = cvm.Contrato.IdInmueble });
                }
            }
            return RedirectToAction("Home","Index");
        }
        cvm.Inquilinos = _repoInquilino.Listar();
        cvm.Inmuebles = _repoInmueble.Listar();
        return View("Crear", cvm);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult Borrar(int id){
        try{
            _repo.Eliminar(id);
            return RedirectToAction("Index");
        }catch (Exception ex){
            return RedirectToAction("Index");
        }
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
}