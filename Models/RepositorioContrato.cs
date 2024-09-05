using MySqlConnector;
using ProyetoInmobiliaria.Models;

public class RepositorioContrato: RepositorioBase{
    private readonly RepositorioInquilino repoInquilino;
    private readonly RepositorioInmueble repoInmueble;
    public RepositorioContrato():base(){
        repoInquilino = new RepositorioInquilino();
        repoInmueble = new RepositorioInmueble();
    }
        //CREAR
    public int Crear(Contrato contrato){
        int idCreado = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "INSERT INTO Contrato (idInquilino, idInmueble, monto, fechaInicio, fechaFin"+
            ", fechaAnulacion, estado)VALUES(@IdInquilino, @IdInmueble, @Monto, @FechaInicio, @FechaFin,"+
            "@FechaAnulacion, true); SELECT LAST_INSERT_ID();";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdInquilino", contrato.IdInquilino);
                command.Parameters.AddWithValue("@IdInmueble", contrato.IdInmueble);
                command.Parameters.AddWithValue("@Monto", contrato.Monto);
                command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
                command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin);
                command.Parameters.AddWithValue("@FechaAnulacion", contrato.FechaAnulacion);
                idCreado = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return idCreado;
    }

    //MODIFICAR
    public int Modificar(Contrato contrato){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE Contrato SET(idInquilino=@IdInquilino, idInmueble=@IdInmueble, monto=@Monto"+
            ", fechaInicio=@FechaInicio, fechaFin=@FechaFin, fechaAnulacion=@FechaAnulacion)"+
            "WHERE idContrato = @IdContrato";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdInquilino", contrato.IdInquilino);
                command.Parameters.AddWithValue("@IdInmueble", contrato.IdInmueble);
                command.Parameters.AddWithValue("@Monto", contrato.Monto);
                command.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
                command.Parameters.AddWithValue("@FechaFin", contrato.FechaFin);
                command.Parameters.AddWithValue("@FechaAnulacion", contrato.FechaAnulacion);
                command.Parameters.AddWithValue("@IdContrato", contrato.IdContrato);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    //LISTAR
    public List<Contrato> Listar(){
        List<Contrato> Contratoes = new List<Contrato>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Contrato";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Inquilino inquilino = repoInquilino.Obtener(reader.GetInt32("IdInquilino"));
                        Inmueble inmueble = repoInmueble.Obtener(reader.GetInt32("IdInmueble"));
                        Contrato Contrato = new Contrato{
                            IdContrato = reader.GetInt32("idContrato"),
                            Monto = reader.GetDouble("monto"),
                            FechaInicio = reader.GetDateTime("fechaInicio"),
                            FechaFin = reader.GetDateTime("fechaFin"),
                            FechaAnulacion = reader.GetDateTime("fechaAnulacion"),
                            Estado = reader.GetBoolean("estado")
                            };
                        Contratoes.Add(Contrato);
                    }
                }
            }
        }
        return Contratoes;
    }
    
    //OBTENER
    public Contrato Obtener(int idContrato){
        Contrato contrato = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Contrato WHERE idContrato = @IdContrato";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdContrato", idContrato);
                using(MySqlDataReader reader = command.ExecuteReader()){
                    if(reader.Read()){
                        Inquilino inquilino = repoInquilino.Obtener(reader.GetInt32("IdInquilino"));
                        Inmueble inmueble = repoInmueble.Obtener(reader.GetInt32("IdInmueble"));
                        contrato = new Contrato{
                            IdContrato = reader.GetInt32("idContrato"),
                            Monto = reader.GetDouble("monto"),
                            FechaInicio = reader.GetDateTime("fechaInicio"),
                            FechaFin = reader.GetDateTime("fechaFin"),
                            FechaAnulacion = reader.GetDateTime("fechaAnulacion"),
                            Estado = reader.GetBoolean("estado")
                            };
                    }
                }
            }
        }
        return contrato;
    }    

    //ELIMINAR
    public int Eliminar(int idContrato){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "DELETE FROM Contrato WHERE idContrato = @IdContrato";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdContrato", idContrato);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }    
}
// using ProyetoInmobiliaria.Models;
// public class RepositorioContrato{
//     private readonly InmobiliariaContext context;

//     public RepositorioContrato(){
//         context = new InmobiliariaContext();
//     }

//     //Crear
//     public int Crear(Contrato contrato){
//         var entidadCreada = context.Add(contrato);
//         context.SaveChanges();
//         return entidadCreada.Entity.IdContrato;
//     }

//     //Modificar
//     public int Modificar(Contrato contrato){
//         context.Update(contrato);
//         int filasAfectadas = context.SaveChanges();
//         return filasAfectadas;
//     }

//     //Listar
//     public List<Contrato> Listar(){
//         var contratos = context.Contrato.Where(e => e.Estado).ToList(); 
//         return contratos;
//     }

//     //Obtener
//     public Contrato Obtener(int IdContrato){
//         return context.Contrato.Find(IdContrato);
//     }

//     //Eliminar
//     public int Eliminar(int IdContrato){
//         Contrato contrato = Obtener(IdContrato);
//         if(contrato != null){
//             contrato.Estado = false;
//             return Modificar(contrato);
//         }
//         return -1;
//     }
// }