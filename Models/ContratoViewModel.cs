using ProyetoInmobiliaria.Models;
public class ContratoViewModel{
    public Contrato? Contrato {get; set;}
    public Inmueble? Inmueble {get; set;}
    public Inquilino? Inquilino {get; set;}
    public List<Inquilino> Inquilinos {get; set;} = new List<Inquilino>();
    public List<Inmueble> Inmuebles {get; set;} = new List<Inmueble>();
}