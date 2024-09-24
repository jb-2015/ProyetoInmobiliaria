using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

[Authorize]
public class InmuebleController : Controller
{
    private readonly ILogger<InmuebleController> _logger;
    private readonly RepositorioInmueble _repo;
    private readonly RepositorioInquilino repoInquilino;
    private readonly RepositorioContrato _repoContrato;
    private readonly RepositorioPropietario _repoProp;
    private readonly RepositorioTipo _repoTipo;
    private readonly RepositorioDireccion _repoDire;


    public InmuebleController(ILogger<InmuebleController> logger){
        _logger = logger;
        _repo = new RepositorioInmueble();
        repoInquilino = new RepositorioInquilino();
        _repoContrato= new RepositorioContrato();
        _repoProp= new RepositorioPropietario();
        _repoTipo= new RepositorioTipo();
        _repoDire= new RepositorioDireccion();
    }
    public IActionResult Detalles(int id){
        var inmueble = _repo.Obtener(id);
        if(inmueble != null){
            Contrato contrato = _repoContrato.ObtenerPorInmueble(id);
            Inquilino inquilino = null;
            if(contrato != null){
                inquilino = repoInquilino.Obtener(contrato.IdInquilino);
            }
            ContratoViewModel Cvm = new ContratoViewModel{
                Inmueble= inmueble,
                Inmuebles = new List<Inmueble>(),
                Inquilinos = new List<Inquilino>(),
                Contrato= contrato,
                Inquilino = inquilino
            };
            return View(Cvm); // muestra a la vista
        }else{
            return RedirectToAction("Index", "Home");
        }
    }

    public IActionResult Index(){
        List<Inmueble> inmuebles = _repo.Listar();
        List<Propietario> propietarios = _repoProp.ListarPropietariosConInmuebles();
        InmuebleViewModel ivm = new InmuebleViewModel{
            Inmuebles = inmuebles,
            Propietarios = propietarios
        };
        return View(ivm);
    }

    //Filtros
    public JsonResult GetNoDisponibles(){
        List<Inmueble> inmuebles = _repo.ListarNoDisponibles();
        return Json(inmuebles);
    }
    public JsonResult GetDisponibles(){
        List<Inmueble> inmuebles = _repo.Listar();
        return Json(inmuebles);
    }
    public JsonResult GetPorPropietario(int idPropietario){
        List<Inmueble> inmuebles = _repo.ListarPorPropietario(idPropietario);
        return Json(inmuebles);
    }

    public JsonResult GetPorFechasDisponibles(DateTime fechaInicio, DateTime fechaFin){
        List<Inmueble> inmuebles = _repo.ListarPorFechas(fechaInicio, fechaFin);
        return Json(inmuebles);
    }

    public IActionResult Editar(int id){
        Inmueble inmueble = _repo.Obtener(id);
        if(inmueble != null){
            List<Propietario> propietarios = _repoProp.Listar();
            List<Tipo> tipos = _repoTipo.Listar();
            Direccion direccion = _repoDire.Obtener(inmueble.IdDireccion);
            InmuebleDireccion inDi = new InmuebleDireccion{
                Propietarios = propietarios,
                Tipos = tipos,
                Inmueble = inmueble,
                Direccion = direccion
            };
            return View(inDi);
        }else{
            return RedirectToAction("Index", "Home");
        }
    }
    public IActionResult Crear(){
        List<Propietario> propietarios = _repoProp.Listar() ?? new List<Propietario>();
        List<Tipo> tipos = _repoTipo.Listar() ?? new List<Tipo>();
        InmuebleDireccion inDi = new InmuebleDireccion{
            Propietarios = propietarios,
            Tipos = tipos
        };
        return View(inDi);
    }

    [HttpPost]
    public IActionResult Guardar(InmuebleDireccion modelo){
        Direccion direccion = new Direccion{
            Calle = modelo.Direccion.Calle,
            Altura  = modelo.Direccion.Altura,
            Cp = modelo.Direccion.Cp,
            Ciudad = modelo.Direccion.Ciudad,
            Coordenadas = modelo.Direccion.Coordenadas,
        };
        int keyDireccion = _repoDire.Crear(direccion);
        modelo.Inmueble.IdDireccion = keyDireccion;
        try{
            if (modelo.Inmueble.IdInmueble == 0){
                int idCreado= _repo.Crear(modelo.Inmueble);
            }else{
                _repo.Modificar(modelo.Inmueble);
            }
            return RedirectToAction("Index");
        }catch (Exception ex){
            return View("Crear", modelo.Inmueble);
        }
    }

    [HttpPost]
    public IActionResult Alta(int id){
        try{
            Inmueble inmueble = _repo.Obtener(id);
            inmueble.Estado = true;
            _repo.Modificar(inmueble);
        }catch (System.Exception){    
            return RedirectToAction("Index", "Home");
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Borrar(int id){
        try{
            _repo.Eliminar(id);
            return RedirectToAction("Index");
        }
        catch(Exception){
            return RedirectToAction("Index");
        }
    }
}