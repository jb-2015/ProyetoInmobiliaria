using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
public class PagoController:Controller{
    private RepositorioPago repo = new RepositorioPago();
    private RepositorioContrato repoContrato = new RepositorioContrato();
    public IActionResult Index(int Id){
        if(Id != 0){
            var contrato = repoContrato.Obtener(Id);
            var pagos = repo.ListarPorContrato(Id);
            if(pagos != null && contrato != null){
                var contratos = repoContrato.Listar();
                if(contratos != null){
                    PagoContrato pc = new PagoContrato{
                        Pagos = pagos,
                        Contratos = contratos,
                        IdContrato = Id,
                    };
                    return View(pc);
                }
                Console.WriteLine("CONTRATOS ES NULL EN INDEX");
            }else{ 
                Console.WriteLine("PAGOS ES NULL EN INDEX");
            }
        }
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Detalle(int id){
        var pago = repo.Obtener(id);
        if(pago != null){
            return View(pago);
        }
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Crear(int Id){
        Contrato c = repoContrato.Obtener(Id);
        if(c != null){
            Pago p = new Pago{
                IdContrato = Id,
                contrato = c
            };
            return View(p);
        }
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Editar(int id){
        var pago = repo.Obtener(id);
        if(pago != null){
            return View(pago);
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Guardar(Pago pago){
        if(pago.IdPago == 0){
            repo.Crear(pago, Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }else{
            repo.Modificar(pago);
        }
        return RedirectToAction("Index", "Pago", new { id = pago.IdContrato });
    }
    
    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public IActionResult Borrar(int Id, int IdContrato){
        int filasAfectadas = repo.Eliminar(Id);
        if(filasAfectadas == 1){
            return RedirectToAction("Index", "Pago", new {id = IdContrato});
        }
        return RedirectToAction("Index", "Home");
    }

    public JsonResult GetPorContrato(int IdContrato){
        List<Pago> pagos = repo.ListarPorContrato(IdContrato);
        return Json(pagos);
    }
}