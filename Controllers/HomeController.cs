//homeController
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Inmobiliaria.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private RepositorioInmueble _repoInmuebles= new RepositorioInmueble();
    private RepositorioTipo _repoTipos= new RepositorioTipo();
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public JsonResult EsAdministrador(){
        bool isAdmin = User.IsInRole("Administrador");
        return Json(isAdmin);
    }
    
    public JsonResult GetInmueblesPorTipoYFechas(int idTipo, DateTime fechaDesde, DateTime fechaHasta){
        var inmuebles = _repoInmuebles.ListarPorTipoYFechas(idTipo, fechaDesde, fechaHasta);
        return Json(inmuebles);
    }
    public JsonResult GetTodos(){
        var inmuebles = _repoInmuebles.Listar();
        return Json(inmuebles);
    }

    public IActionResult Index()
    {
       
        var inmuebles= _repoInmuebles.Listar();
        var tipos = _repoTipos.Listar();
        InmuebleTipos it = new InmuebleTipos{
            Inmuebles = inmuebles,
            Tipos = tipos
        };
        return View(it);
    }

    public IActionResult Privacy()
    {
        return View();
    }

/*----------------NUEVO-------------------*/
    public IActionResult PagoDetalle(){
        return View();
    }

    public IActionResult PagoIndex(){
        return View();
    }

    public IActionResult PagoCrear(){
        return View();
    }
/*-----------------------------------*/
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
