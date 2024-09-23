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
            string query = "UPDATE Propietario SET dni=@Dni, apellido=@Apellido, nombre=@Nombre, telefono=@Telefono, correo=@Correo"+
            ", estado=@Estado WHERE idPropietario = @IdPropietario";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Dni", propietario.Dni);
                command.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                command.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                command.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                command.Parameters.AddWithValue("@Correo", propietario.Correo);
                command.Parameters.AddWithValue("@IdPropietario", propietario.IdPropietario);
                command.Parameters.AddWithValue("@Estado", propietario.Estado);
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

    //LISTAR PROPIETARIOS QUE CONTIENEN AL MENOS UN INMUEBLE
    public List<Propietario> ListarPropietariosConInmuebles(){
        List<Propietario> Propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT DISTINCT p.* FROM propietario AS p JOIN inmueble AS i ON p.idPropietario = i.idPropietario WHERE i.estado = true;";
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
            string query = "UPDATE Propietario SET estado = false WHERE idPropietario = @IdPropietario";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPropietario", IdPropietario);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    //DADOS DE BAJA
    public List<Propietario> DadosDeBaja(){
        List<Propietario> Propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Propietario WHERE estado = false";
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

    //LISTAR POR DNI
    public List<Propietario> ListarPorDni(String Dni){
        List<Propietario> Propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Propietario WHERE dni LIKE @Dni AND estado = true";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Dni", Dni+"%");
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
    //LISTAR POR APELLIDO
    public List<Propietario> ListarPorApellido(String Apellido){
        List<Propietario> Propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Propietario WHERE apellido LIKE @Apellido AND estado = true";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Apellido", Apellido+"%");
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
    //LISTAR POR EMAIL
    public List<Propietario> ListarPorEmail(String Email){
        List<Propietario> Propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Propietario WHERE correo LIKE @Email AND estado = true";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Email", Email+"%");
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
} 