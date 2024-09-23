using ProyetoInmobiliaria.Models;

public class InmuebleDireccion{
    public Inmueble Inmueble {get; set;}
    public Direccion Direccion {get; set;}
    public List<Tipo> Tipos {get;set;}
    public List<Propietario> Propietarios {get;set;}
}

