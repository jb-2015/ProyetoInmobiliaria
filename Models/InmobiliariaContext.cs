using Microsoft.EntityFrameworkCore;
using ProyetoInmobiliaria.Models;

public class InmobiliariaContext : DbContext
{
    public DbSet<Inquilino> Inquilino { get; set; }
    public DbSet<Propietario> Propietario { get; set; }
    public DbSet<Inmueble> Inmueble {get; set;}
    public DbSet<Contrato> Contrato {get; set;}
    public DbSet<Tipo> Tipo {get; set;}
    public DbSet<Direccion> Direccion {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;database=inmobiliaria;user=root;password=", new MySqlServerVersion(new Version(8, 4, 0)));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Inquilino>().HasKey(i => i.IdInquilino); // Define la clave primaria
        modelBuilder.Entity<Propietario>().HasKey(i => i.IdPropietario); // Define la clave primaria
        modelBuilder.Entity<Inmueble>().HasKey(i => i.IdInmueble);
        modelBuilder.Entity<Contrato>().HasKey(i => i.IdContrato);
        modelBuilder.Entity<Tipo>().HasKey(i => i.IdTipo);
        modelBuilder.Entity<Direccion>().HasKey(i => i.IdDireccion);
    }
}
