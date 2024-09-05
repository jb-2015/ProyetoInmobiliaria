using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;

public class RepositorioInquilino: RepositorioBase{
    public RepositorioInquilino():base(){

    }

    //CREAR
    public int Crear(Inquilino inquilino){
        int idCreado = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "INSERT INTO inquilino (dni, apellido, nombre, telefono, correo, estado)"+
            "VALUES (@Dni, @Apellido, @Nombre, @Telefono, @Correo, true); SELECT LAST_INSERT_ID();";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Dni", inquilino.Dni);
                command.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                command.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                command.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                command.Parameters.AddWithValue("@Correo", inquilino.Correo);
                idCreado = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return idCreado;
    }

    //MODIFICAR
    public int Modificar(Inquilino inquilino){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE inquilino SET(dni=@Dni, apellido=@Apellido, nombre=@Nombre, telefono=@Telefono, correo=@Correo)"+
            "WHERE idInquilino = @IdInquilino";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Dni", inquilino.Dni);
                command.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                command.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                command.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                command.Parameters.AddWithValue("@Correo", inquilino.Correo);
                command.Parameters.AddWithValue("@IdInquilino", inquilino.IdInquilino);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    //LISTAR
    public List<Inquilino> Listar(){
        List<Inquilino> inquilinos = new List<Inquilino>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM inquilino WHERE estado = true";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Inquilino Inquilino = new Inquilino{
                            IdInquilino = reader.GetInt32("IdInquilino"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                            Estado = reader.GetBoolean("estado")
                            };
                        inquilinos.Add(Inquilino);
                    }
                }
            }
        }
        return inquilinos;
    }
    
    //OBTENER
    public Inquilino Obtener(int idInquilino){
        Inquilino inquilino = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM inquilino WHERE idInquilino = @IdInquilino";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdInquilino", idInquilino);
                using(MySqlDataReader reader = command.ExecuteReader()){
                    if(reader.Read()){
                        inquilino = new Inquilino{
                            IdInquilino = idInquilino,
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                            Estado = reader.GetBoolean("estado")
                        };
                    }
                }
            }
        }
        return inquilino;
    }    

    //ELIMINAR
    public int Eliminar(int IdInquilino){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE inquilino SET(estado = false) WHERE idInquilino = @IdInquilino";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdInquilino", IdInquilino);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }
}

// //repositorioInquilino.cs
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using ProyetoInmobiliaria.Models;

// public class RepositorioInquilino
// {
//     private readonly InmobiliariaContext _context;

//     public RepositorioInquilino()
//     {
//         _context = new InmobiliariaContext();
//     }

//     public int Crear(Inquilino inquilino)
//     {
//         _context.Inquilino.Add(inquilino); // Asegúrate de que la propiedad sea plural: Inquilino
//         _context.SaveChanges();
//         return inquilino.IdInquilino;
//     }

//     public Inquilino Obtener(int id)
//     {
//         return _context.Inquilino.Find(id); // Debería ser Inquilino
//     }

//     public void Modificar(Inquilino inquilino)
//     {
//         _context.Inquilino.Update(inquilino); // Debería ser Inquilino
//         _context.SaveChanges();
//     }

//     public void Eliminar(int id)
//     {
//         Inquilino inquilino = _context.Inquilino.Find(id); // Debería ser Inquilino
//         if (inquilino != null)
//         {
//             inquilino.Estado = false;
//             _context.Inquilino.Update(inquilino); // Debería ser Inquilino
//             _context.SaveChanges();
//         }
//     }

//     public List<Inquilino> Listar()
//     {
//         return _context.Inquilino.Where(i => i.Estado==true).ToList(); // Debería ser Inquilino
//     }
// }
