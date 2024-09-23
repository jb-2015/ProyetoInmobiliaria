using System.ComponentModel.DataAnnotations;
using ProyetoInmobiliaria.Models;
public class Pago{
    [Key]
    public int IdPago{get; set;}
    public int IdContrato{get;set;}
    public Contrato? contrato{get;set;}
    public DateTime FechaPago{get;set;}
    public Decimal Importe{get;set;}
    public string NumeroPago{get;set;}
    public string Detalle{get;set;}
    public bool Estado{get;set;}
}