using System.ComponentModel.DataAnnotations;
using ProyetoInmobiliaria.Models;
public class Inmueble{
    [Key]
    public int IdInmueble {get; set;}
    public int IdPropietario {get; set;}
    public int IdDireccion {get; set;}
    public int IdTipo {get; set;}

    public Propietario? propietario {get; set;}
    public Direccion? direccion {get; set;} 
    public Tipo? tipo {get; set;}
    public string Metros2 {get; set;}
    public int CantidadAmbientes {get; set;}
    public Boolean Disponible {get; set;}
    public Decimal Precio {get; set;}
    public string Descripcion {get; set;}
    public Boolean Cochera {get; set;}
    public Boolean Piscina {get; set;}
    public Boolean Mascotas {get; set;}
    public Boolean Estado {get; set;}
    public string UrlImagen {get; set;}
    // public string? TipoYDireccion {
    //     get {
    //         return $"{tipo.Observacion}, {direccion.DireccionCompleta} ";
    //     };
    //     set;
    // }
}