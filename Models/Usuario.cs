using System.ComponentModel.DataAnnotations;
public class Usuario {
    [Key]
    public int IdUsuario  { get; set; }

    public string Nombre  { get; set; }
    public  string Apellido  { get; set; }

    public String Email   {get; set;}

    public String Password  {get; set;}

    public String Rol {get; set;}
    public String Avatar {get; set;}
    public bool  Estado {get; set;}
}