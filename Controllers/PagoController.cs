using Microsoft.AspNetCore.Mvc;
using ProyetoInmobiliaria.Models;
public class PagoController:Controller{
    private RepositorioPago repo = new RepositorioPago();
    private RepositorioContrato repoContrato = new RepositorioContrato();
    public IActionResult Index(int id){
        var pagos = repo.ListarPorContrato(id);
        if(pagos == null){pagos = new List<Pago>();}
        PagoViewModel pvm = new PagoViewModel{
            pagos = pagos,
            IdContrato = id
        };
        return View(pvm);
    }
    public IActionResult Detalle(int id){
        var pago = repo.Obtener(id);
        if(pago == null){
            pago = new Pago();
        }
        return View(pago);
    }
    public IActionResult Crear(int idContrato){
        // var contrato = repoContrato.Obtener(idContrato);
        // if(contrato != null){
            Pago p = new Pago{
                IdContrato = idContrato
            };
            return View(p);
        // }else{
        //     return RedirectToAction("Index");
        // }
    }
    public IActionResult Editar(int id){
        var pago = repo.Obtener(id);
        if(pago == null){
            pago = new Pago();
        }
        return View(repo.Obtener(id));
    }

    [HttpPost]
    public IActionResult Guardar(int id, Pago pago){
        if(id == 0){
            repo.Crear(pago);
        }else{
            repo.Modificar(pago);
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult Borrar(int id){
        if(repo.Eliminar(id) == 1){
            return RedirectToAction("Index");
        }
        return RedirectToAction("Index", "Home");
    }

}