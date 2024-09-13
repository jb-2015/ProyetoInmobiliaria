
using System.ComponentModel.DataAnnotations;

namespace ProyetoInmobiliaria.Models;
public class Propietario{
    [Key]
    public int IdPropietario { get; set; }
    public string Apellido { get; set; } = "";
    public string Nombre { get; set;} = "";
    public string Dni { get; set;} = "";
    public string Telefono { get; set; } = "";
    public string Correo { get; set; } = "";
    public Boolean Estado { get; set; }
    public string NombreYApellido{
        get{
            return $"{Nombre} {Apellido}";
        }
    }
}