using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;


public class RepositorioAuditoria:RepositorioBase{
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