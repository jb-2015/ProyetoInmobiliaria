using MySql.Data.MySqlClient;

public class RepositorioTipo:RepositorioBase{
    public RepositorioTipo():base(){
            
    }
    //CREAR
    public int Crear(Tipo tipo){
        int idCreado = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "INSERT INTO Tipo (observacion)VALUES(@Observacion); SELECT LAST_INSERT_ID();";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Observacion", tipo.Observacion);
                idCreado = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return idCreado;
    }

    //MODIFICAR
    public int Modificar(Tipo tipo){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE tipo SET observacion=@Observacion WHERE idTipo = @IdTipo";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@Observacion", tipo.Observacion);
                command.Parameters.AddWithValue("@IdTipo", tipo.IdTipo);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    //LISTAR
    public List<Tipo> Listar(){
        List<Tipo> Tipos = new List<Tipo>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Tipo";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Tipo tipo = new Tipo{
                            IdTipo = reader.GetInt32("IdTipo"),
                            Observacion = reader.GetString("Observacion")
                            };
                        Tipos.Add(tipo);
                    }
                }
            }
        }
        return Tipos;
    }
    
    //OBTENER
    public Tipo Obtener(int idTipo){
        Tipo tipo = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Tipo WHERE idTipo = @IdTipo";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdTipo", idTipo);
                using(MySqlDataReader reader = command.ExecuteReader()){
                    if(reader.Read()){
                        tipo = new Tipo{
                            IdTipo = idTipo,
                            Observacion = reader.GetString("observacion")
                        };
                    }
                }
            }
        }
        return tipo;
    }    

    //ELIMINAR
    public int Eliminar(int IdTipo){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "DELETE FROM Tipo WHERE idTipo = @IdTipo";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdTipo", IdTipo);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }
}
// public class RepositorioTipo{
//     private readonly InmobiliariaContext context;

//     public RepositorioTipo(){
//         context = new InmobiliariaContext();
//     }

//     //Crear
//     public int Crear(Tipo tipo){
//         var entidadCreada = context.Add(tipo);
//         context.SaveChanges();
//         return entidadCreada.Entity.IdTipo;
//     } 
    
//     //Modificar
//     public int Modificar(Tipo tipo){
//         context.Update(tipo);
//         int filasAfectadas = context.SaveChanges();
//         return filasAfectadas;
//     }

//     //Listar
//     public List<Tipo> Listar(){
//         List<Tipo> tipos = context.Tipo.ToList();
//         return tipos;
//     }
    
//     //Obtener
//     public Tipo Obtener(int IdTipo){
//         return context.Tipo.Find(IdTipo);
//     }

//     //Eliminar
//     public int Eliminar(int IdTipo){
//         Tipo tipo = Obtener(IdTipo);
//         int filasAfectadas = 0;
//         if(tipo != null){
//             context.Remove(tipo);
//             filasAfectadas = context.SaveChanges();
//         }
//         return filasAfectadas;
//     }
// }