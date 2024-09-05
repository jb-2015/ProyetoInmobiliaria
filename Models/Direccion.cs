using System.ComponentModel.DataAnnotations;

public class Direccion{
    [Key]
    public int IdDireccion {get; set;}
    public string Calle {get; set;}
    public int Altura {get; set;}
    public string Cp {get; set;}
    public string Ciudad {get; set;}
    public string Coordenadas {get; set;}
}