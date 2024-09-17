//homeController
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;

namespace Inmobiliaria.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private RepositorioInmueble _repoInmuebles= new RepositorioInmueble();
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        var inmuebles= _repoInmuebles.Listar();
        return View(inmuebles);
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
