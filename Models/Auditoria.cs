using System.ComponentModel.DataAnnotations;
public class Auditoria{
    [Key]
    public int IdAuditoria{get; set;}
    public int IdUsuario{get;set;}
    public Usuario Usuario{get;set;}
    public string Accion{get; set;}
    public string Observacion{get; set;}
    public DateTime FechaYHora{get;set;}

}