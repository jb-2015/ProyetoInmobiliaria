using System.ComponentModel.DataAnnotations;
public class Tipo{
    [Key]
    public int IdTipo{get; set;}
    public string Observacion{get; set;}
    public override string ToString(){
        return $"IdTipo: {IdTipo}, Observacion: {Observacion}";
    }
}