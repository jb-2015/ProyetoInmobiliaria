
using Microsoft.EntityFrameworkCore;


using MySql.Data.MySqlClient;
namespace ProyetoInmobiliaria.Models;

public class RepositorioPropietario{
    
    
    private readonly InmobiliariaContext _context = new InmobiliariaContext();

    public List<Propietario> Listar(){
        
        return _context.Propietario.Where(i => i.Estado == true).ToList();
    }
    public Propietario? Obtener(int Id){
        Propietario? propietario = _context.Propietario.Find(Id);
            return propietario;
        }

    
    public int Crear(Propietario propietario){
        
        _context.Propietario.Add(propietario);
        _context.SaveChanges();
        return propietario.IdPropietario;
    }

    public int Modificar(Propietario propietario){
        
        
        _context.Propietario.Update(propietario);
        _context.SaveChanges();
        return 1;

    }

    public void Eliminar(int Id){
        Propietario propietario = _context.Propietario.Find(Id); 
        if (propietario != null){
            propietario.Estado = false;
            _context.Propietario.Update(propietario); 
            _context.SaveChanges();
        }
    }
}