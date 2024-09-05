using MySql.Data.MySqlClient;

public class RepositorioDireccion:RepositorioBase{
    public RepositorioDireccion():base(){
    
    }
    
    //CREAR
    public int Crear(Direccion direccion){
        int idCreado = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "INSERT INTO direccion (calle, altura, cp, ciudad, coordenadas)"+
            "VALUES(@Calle, @Altura, @Cp, @Ciudad, @Coordenadas); SELECT LAST_INSERT_ID();";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Calle", direccion.Calle);
                command.Parameters.AddWithValue("@Altura", direccion.Altura);
                command.Parameters.AddWithValue("@Cp", direccion.Cp);
                command.Parameters.AddWithValue("@Ciudad", direccion.Ciudad);
                command.Parameters.AddWithValue("@Coordenadas", direccion.Coordenadas);
                idCreado = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return idCreado;
    }

    //MODIFICAR
    public int Modificar(Direccion Direccion){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE Direccion SET(calle=@Calle, altura=@Altura, cp=@Cp, ciudad=@Ciudad, coordenadas=@Coordenadas)"+
            "WHERE idDireccion = @IdDireccion";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Calle", Direccion.Calle);
                command.Parameters.AddWithValue("@Altura", Direccion.Altura);
                command.Parameters.AddWithValue("@Cp", Direccion.Cp);
                command.Parameters.AddWithValue("@Ciudad", Direccion.Ciudad);
                command.Parameters.AddWithValue("@Coordenadas", Direccion.Coordenadas);
                command.Parameters.AddWithValue("@IdDireccion", Direccion.IdDireccion);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    //LISTAR
    public List<Direccion> Listar(){
        List<Direccion> Direcciones = new List<Direccion>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Direccion";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Direccion direccion = new Direccion{
                            IdDireccion = reader.GetInt32("idDireccion"),
                            Calle = reader.GetString("calle"),
                            Altura = reader.GetInt32("altura"),
                            Cp = reader.GetString("cp"),
                            Ciudad = reader.GetString("ciudad"),
                            Coordenadas = reader.GetString("coordenadas")
                            };
                        Direcciones.Add(direccion);
                    }
                }
            }
        }
        return Direcciones;
    }
    
    //OBTENER
    public Direccion Obtener(int idDireccion){
        Direccion direccion = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Direccion WHERE idDireccion = @IdDireccion";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdDireccion", idDireccion);
                using(MySqlDataReader reader = command.ExecuteReader()){
                    if(reader.Read()){
                        direccion = new Direccion{
                            IdDireccion = idDireccion,
                            Calle = reader.GetString("calle"),
                            Altura = reader.GetInt32("altura"),
                            Cp = reader.GetString("cp"),
                            Ciudad = reader.GetString("ciudad"),
                            Coordenadas = reader.GetString("coordenadas")
                        };
                    }
                }
            }
        }
        return direccion;
    }    

    //ELIMINAR
    public int Eliminar(int idDireccion){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "DELETE FROM direccion WHERE idDireccion = @IdDireccion";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdDireccion", idDireccion);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }    
}
// public class RepositorioDireccion{
//     private readonly InmobiliariaContext context;

//     public RepositorioDireccion(){
//         context = new InmobiliariaContext();
//     }

//     //Crear
//     public int Crear(Direccion direccion){
//         var entidadCreada = context.Add(direccion);
//         context.SaveChanges();
//         return entidadCreada.Entity.IdDireccion;
//     }

//     //Modificar
//     public int Modificar(Direccion direccion){
//         context.Update(direccion);
//         int filasAfectadas = context.SaveChanges();
//         return filasAfectadas;
//     }

//     //Listar
//     public List<Direccion> Listar(){
//         return context.Direccion.ToList();
//     }

//     //Obtener
//     public Direccion Obtener(int IdDireccion){
//         return context.Direccion.Find(IdDireccion);
//     }

//     //Eliminar
//     public int Eliminar(int IdDireccion){
//         Direccion direccion = Obtener(IdDireccion);
//         int filasAfectadas = 0;
//         if(direccion != null){
//             context.Remove(direccion);
//             filasAfectadas = context.SaveChanges();
//         }
//         return filasAfectadas;
//     }

// }