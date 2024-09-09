using System.ComponentModel.DataAnnotations;
using ProyetoInmobiliaria.Models;
public class Pago{
    [Key]
    public int IdPago{get; set;}
    public int IdContrato{get;set;}
    public Contrato? contrato{get;set;}
    public DateTime FechaPago{get;set;}
    [Required(ErrorMessage = "Campo obligatorio")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "El precio debe ser un número válido con hasta dos decimales.")]
    public Decimal Importe{get;set;}
    public string NumeroPago{get;set;}
    public string Detalle{get;set;}
    public bool Estado{get;set;}
}