using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;
public class RepositorioPago:RepositorioBase{
    public RepositorioContrato repoContrato;
    private readonly RepositorioAuditoria repoAuditoria;// se añidio
    public RepositorioPago():base(){
        repoContrato = new RepositorioContrato();
        repoAuditoria = new RepositorioAuditoria(); // se añidio
    }

    public Pago Obtener(int IdPago){
        Pago pago = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM pago WHERE idPago = @IdPago";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPago", IdPago);
                using(MySqlDataReader reader = command.ExecuteReader()){
                    if(reader.Read()){
                        var c = repoContrato.Obtener(reader.GetInt32("idContrato"));
                        pago = new Pago{
                            IdPago = reader.GetInt32("idPago"),
                            contrato = c,
                            IdContrato = reader.GetInt32("idContrato"),
                            FechaPago = reader.GetDateTime("fechaPago"),
                            Importe = reader.GetDecimal("importe"),
                            NumeroPago = reader.GetString("numeroPago"),
                            Detalle = reader.GetString("detalle"),
                            Estado = reader.GetBoolean("estado")
                        };
                    }
                }
            }
        }
        return pago;
    }
    public List<Pago> Listar(){
        List<Pago> pagos = new List<Pago>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM pago";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        var c = repoContrato.Obtener(reader.GetInt32("IdContrato"));
                        Pago pago = new Pago{
                            IdPago = reader.GetInt32("idPago"),
                            IdContrato = reader.GetInt32("idContrato"),
                            contrato = c,
                            FechaPago = reader.GetDateTime("fechaPago"),
                            Importe = reader.GetDecimal("importe"),
                            NumeroPago = reader.GetString("numeroPago"),
                            Detalle = reader.GetString("detalle"),
                            Estado = reader.GetBoolean("estado")
                        };
                        pagos.Add(pago);
                    }
                }
            }
        }
        return pagos;
    }
    public List<Pago> ListarPorContrato(int IdContrato){
        List<Pago> pagos = new List<Pago>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM pago WHERE idContrato = @IdContrato";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdContrato", IdContrato);
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        var c = repoContrato.Obtener(IdContrato);
                        Pago pago = new Pago{
                            IdPago = reader.GetInt32("idPago"),
                            IdContrato =IdContrato,
                            contrato = c,
                            FechaPago = reader.GetDateTime("fechaPago"),
                            Importe = reader.GetDecimal("importe"),
                            NumeroPago = reader.GetString("numeroPago"),
                            Detalle = reader.GetString("detalle"),
                            Estado = reader.GetBoolean("estado")
                        };
                        pagos.Add(pago);
                    }
                }
            }
        }
        return pagos;
    }

    public int Crear(Pago pago, int idUsuario){ // agrego isuario
        int idCreado = 0;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "INSERT INTO pago (idContrato, fechaPago, importe, numeroPago, detalle, estado)"+
            "VALUES(@IdContrato, @FechaPago, @Importe, @NumeroPago, @Detalle, true); SELECT LAST_INSERT_ID()";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                command.Parameters.AddWithValue("@Importe", pago.Importe);
                command.Parameters.AddWithValue("@NumeroPago", pago.NumeroPago);
                command.Parameters.AddWithValue("@Detalle", pago.Detalle);
                idCreado = Convert.ToInt32(command.ExecuteScalar());


                 // Guardar auditoría
                if (idCreado > 0)
                {
                    var auditoria = new Auditoria
                    {
                        IdUsuario = idUsuario,
                        Accion = "Crear Pago",
                        Observacion = $"Pago creado para contrato ID: {pago.IdContrato}. Importe: {pago.Importe}.",
                        FechaYHora = DateTime.Now
                    };
                    repoAuditoria.GuardarAuditoria(auditoria);
                }
            }    
        }
        return idCreado;
    }
    public int Modificar(Pago pago){
        int filasAfectadas = 0;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE pago SET idContrato=@IdContrato, fechaPago=@FechaPago, importe=@Importe,"+
            "numeroPago=@NumeroPago, detalle=@Detalle WHERE idPago = @IdPago";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                command.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                command.Parameters.AddWithValue("@Importe", pago.Importe);
                command.Parameters.AddWithValue("@NumeroPago", pago.NumeroPago);
                command.Parameters.AddWithValue("@Detalle", pago.Detalle);
                command.Parameters.AddWithValue("@IdPago", pago.IdPago);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    public int Eliminar(int IdPago){
        int filasAfectadas = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE pago SET estado=false WHERE idPago = @IdPago";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPago", IdPago);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }
}