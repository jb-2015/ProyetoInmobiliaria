using MySqlConnector;
using ProyetoInmobiliaria.Models;

public class RepositorioContrato: RepositorioBase{
    private readonly RepositorioInquilino repoInquilino;
    private readonly RepositorioInmueble repoInmueble;
    private readonly RepositorioAuditoria repoAuditoria; // se añadio
    public RepositorioContrato():base(){
        repoInquilino = new RepositorioInquilino();
        repoInmueble = new RepositorioInmueble();
        repoAuditoria = new RepositorioAuditoria();// se añadio
    }
        //CREAR
    public int Crear(Contrato contrato , int idUsuario){
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
            // Guardar auditoría
            if (idCreado > 0)
            {
                var auditoria = new Auditoria
                {
                    IdUsuario = idUsuario,
                    Accion = "Crear Contrato",
                    Observacion = $"Contrato creado para inquilino ID: {contrato.IdInquilino} en inmueble ID: {contrato.IdInmueble}.",
                    FechaYHora = DateTime.Now
                };
                repoAuditoria.GuardarAuditoria(auditoria);
            }
        return idCreado;
    }

    //MODIFICAR
    public int Modificar(Contrato contrato){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE Contrato SET idInquilino=@IdInquilino, idInmueble=@IdInmueble, monto=@Monto"+
            ", fechaInicio=@FechaInicio, fechaFin=@FechaFin, fechaAnulacion=@FechaAnulacion "+
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
        List<Contrato> Contratos = new List<Contrato>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Contrato";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Inquilino inquilino = repoInquilino.Obtener(reader.GetInt32("idInquilino"));
                        Inmueble inmueble = repoInmueble.Obtener(reader.GetInt32("idInmueble"));
                        Contrato Contrato = new Contrato{
                            IdContrato = reader.GetInt32("idContrato"),
                            inquilino = inquilino,
                            inmueble = inmueble,
                            IdInquilino = inquilino.IdInquilino,
                            IdInmueble = inmueble.IdInmueble,
                            Monto = reader.GetDouble("monto"),
                            FechaInicio = reader.GetDateTime("fechaInicio"),
                            FechaFin = reader.GetDateTime("fechaFin"),
                            FechaAnulacion = reader.GetDateTime("fechaAnulacion"),
                            Estado = reader.GetBoolean("estado")
                            };
                        Contratos.Add(Contrato);
                    }
                }
            }
        }
        return Contratos;
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
                            inquilino = inquilino,
                            inmueble = inmueble,
                            IdInmueble = inmueble.IdInmueble,
                            IdInquilino = inquilino.IdInquilino,
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

    public List<Contrato> ObtenerPorInmueble2(int id){
        Contrato contrato = null;
        List<Contrato> contratos = new List<Contrato>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Contrato WHERE idInmueble = @IdInmueble";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdInmueble", id);
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
                            contratos.Add(contrato);
                    }
                }
            }
        }
        return contratos;
    }
    public Contrato ObtenerPorInmueble(int id){
        Contrato contrato = null;
        
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM Contrato WHERE idInmueble = @IdInmueble";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdInmueble", id);
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
        return contrato;
    }
}
}

