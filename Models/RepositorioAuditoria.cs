using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;


public class RepositorioAuditoria:RepositorioBase{
    private readonly RepositorioUsuario repoUser = new RepositorioUsuario();
    public List<Auditoria> Listar(){
        List<Auditoria> auditorias = new List<Auditoria>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
                connection.Open();
                string query = "SELECT * FROM auditoria";
                using(MySqlCommand command = new MySqlCommand(query, connection)){
                    using(MySqlDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                                Usuario user = repoUser.Obtener(reader.GetInt32("idUsuario"));
                                if(user != null){
                                    Auditoria auditoria = new Auditoria{
                                        Accion = reader.GetString("accion"),
                                        Usuario = user,
                                        FechaYHora = reader.GetDateTime("fechaYHora"),
                                        Observacion = reader.GetString("observacion"),
                                        IdAuditoria = reader.GetInt32("idAuditoria"),
                                        IdUsuario = reader.GetInt32("idUsuario")
                                    };
                                    auditorias.Add(auditoria);
                                }
                        }
                    }
                }
            }
        return auditorias;
    }
    public List<Auditoria> ListarPorIdUsuario(int id){
        List<Auditoria> auditorias = new List<Auditoria>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
                connection.Open();
                string query = "SELECT * FROM auditoria WHERE idUsuario = @id";
                using(MySqlCommand command = new MySqlCommand(query, connection)){
                    command.Parameters.AddWithValue("@id", id);
                    using(MySqlDataReader reader = command.ExecuteReader()){
                        while(reader.Read()){
                                Usuario user = repoUser.Obtener(id);
                                if(user != null){
                                    Auditoria auditoria = new Auditoria{
                                        Accion = reader.GetString("accion"),
                                        Usuario = user,
                                        FechaYHora = reader.GetDateTime("fechaYHora"),
                                        Observacion = reader.GetString("observacion"),
                                        IdAuditoria = reader.GetInt32("idAuditoria"),
                                        IdUsuario = reader.GetInt32("idUsuario")
                                    };
                                    auditorias.Add(auditoria);
                                }
                        }
                    }
                }
            }
        return auditorias;
    }
    public void GuardarAuditoria(Auditoria auditoria)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "INSERT INTO auditoria (idUsuario, accion, observacion, fechaYHora) VALUES (@IdUsuario, @Accion, @Observacion, @FechaYHora)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", auditoria.IdUsuario);
                    command.Parameters.AddWithValue("@Accion", auditoria.Accion);
                    command.Parameters.AddWithValue("@Observacion", auditoria.Observacion);
                    command.Parameters.AddWithValue("@FechaYHora", auditoria.FechaYHora);
                    command.ExecuteNonQuery();
                }
            }
        }
}