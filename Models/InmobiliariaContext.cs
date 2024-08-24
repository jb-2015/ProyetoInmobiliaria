using Microsoft.EntityFrameworkCore;
using ProyetoInmobiliaria.Models;

public class InmobiliariaContext : DbContext
{
    public DbSet<Inquilino> Inquilino { get; set; }
    public DbSet<Propietario> Propietario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;database=inmobiliaria;user=root;password=", new MySqlServerVersion(new Version(8, 4, 0)));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Inquilino>()
            .HasKey(i => i.IdInquilino); // Define la clave primaria
        modelBuilder.Entity<Propietario>()
            .HasKey(i => i.IdPropietario); // Define la clave primaria
    }

}
