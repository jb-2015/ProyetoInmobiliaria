using System.ComponentModel.DataAnnotations;
using ProyetoInmobiliaria.Models;
public class Contrato{
    [Key]
    public int IdContrato {get; set;}
    public Inquilino IdInquilino {get; set;}
    public Inmueble IdInmueble {get; set;}
    public Double Monto {get; set;}
    public DateTime FechaInicio {get; set;}
    public DateTime FechaFin {get; set;}
    public DateTime FechaAnulacion {get; set;}
    public Boolean Estado {get; set;}
}