using System.ComponentModel.DataAnnotations;
using ProyetoInmobiliaria.Models;
public class Contrato{
    [Key]
    public int IdContrato {get; set;}

    [Required(ErrorMessage = "El campo Inquilino es obligatorio.")]
    public int IdInquilino {get; set;}
    
    [Required(ErrorMessage = "El campo Inmueble es obligatorio.")]
    public int IdInmueble {get; set;}
    public Inquilino inquilino {get; set;}
    public Inmueble inmueble {get; set;}

    [Required(ErrorMessage = "El Monto es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El Monto debe ser mayor a 0.")]
    public Double Monto {get; set;}

    [Required(ErrorMessage = "La Fecha de Inicio es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaInicio {get; set;}

    [Required(ErrorMessage = "La Fecha de Inicio es obligatoria.")]
    [DataType(DataType.Date)]
    public DateTime FechaFin {get; set;}
    public DateTime FechaAnulacion {get; set;}
    public Boolean Estado {get; set;}
}