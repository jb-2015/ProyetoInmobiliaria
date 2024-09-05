using System.ComponentModel.DataAnnotations;
using ProyetoInmobiliaria.Models;
public class Inmueble{
    [Key]
    public int IdInmueble {get; set;}
    public Propietario IdPropietario {get; set;}
    public Direccion IdDireccion {get; set;} 
    public Tipo IdTipo {get; set;}
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
    

    



}