using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;
public class RepositorioPropietario:RepositorioBase{
    public RepositorioPropietario():base(){

    }
    //CREAR
    public int Crear(Propietario propietario){
        int idCreado = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "INSERT INTO Propietario (dni, apellido, nombre, telefono, correo, estado)"+
            "VALUES (@Dni, @Apellido, @Nombre, @Telefono, @Correo, true); SELECT LAST_INSERT_ID();";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Dni", propietario.Dni);
                command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Correo", propietario.Correo);
                idCreado = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return idCreado;
    }

    //MODIFICAR
    public int Modificar(Propietario propietario){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE Propietario SET(dni=@Dni, apellido=@Apellido, nombre=@Nombre, telefono=@Telefono, correo=@Correo)"+
            "WHERE idPropietario = @IdPropietario";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Dni", propietario.Dni);
                command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Correo", propietario.Correo);
                command.Parameters.AddWithValue("@IdPropietario", propietario.IdPropietario);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    //LISTAR
    public List<Propietario> Listar(){
        List<Propietario> Propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Propietario WHERE estado = true";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Propietario Propietario = new Propietario{
                            IdPropietario = reader.GetInt32("IdPropietario"),
                            Dni = reader.GetString("dni"),
                            Apellido = reader.GetString("apellido"),
                            Nombre = reader.GetString("nombre"),
                            Telefono = reader.GetString("telefono"),
                            Correo = reader.GetString("correo"),
                            Estado = reader.GetBoolean("estado")
                            };
                        Propietarios.Add(Propietario);
                    }
                }
            }
        }
        return Propietarios;
    }
    
    //OBTENER
    public Propietario Obtener(int idPropietario){
        Propietario Propietario = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Propietario WHERE idPropietario = @IdPropietario";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPropietario", idPropietario);
                using(MySqlDataReader reader = command.ExecuteReader()){
                    if(reader.Read()){
                        Propietario = new Propietario{
                            IdPropietario = idPropietario,
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
        return Propietario;
    }    

    //ELIMINAR
    public int Eliminar(int IdPropietario){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE Propietario SET(estado = false) WHERE idPropietario = @IdPropietario";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPropietario", IdPropietario);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }
} 
// //repositorioPropietario.cs
// using Microsoft.EntityFrameworkCore;
// using MySql.Data.MySqlClient;
// namespace ProyetoInmobiliaria.Models;

// public class RepositorioPropietario{
    
    
//     private readonly InmobiliariaContext _context = new InmobiliariaContext();

//     public List<Propietario> Listar(){
        
//         return _context.Propietario.Where(i => i.Estado == true).ToList();
//     }
//     public Propietario? Obtener(int Id){
//         Propietario? propietario = _context.Propietario.Find(Id);
//             return propietario;
//         }

    
//     public int Crear(Propietario propietario){
        
//         _context.Propietario.Add(propietario);
//         _context.SaveChanges();
//         return propietario.IdPropietario;
//     }

//     public int Modificar(Propietario propietario){
        
        
//         _context.Propietario.Update(propietario);
//         _context.SaveChanges();
//         return 1;

//     }

//     public void Eliminar(int Id){
//         Propietario propietario = _context.Propietario.Find(Id); 
//         if (propietario != null){
//             propietario.Estado = false;
//             _context.Propietario.Update(propietario); 
//             _context.SaveChanges();
//         }
//     }
// }