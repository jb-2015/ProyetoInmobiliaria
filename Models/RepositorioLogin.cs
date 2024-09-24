//repositorioLogin

using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;
using Microsoft.AspNetCore.Http;


public class RepositorioLogin : RepositorioBase
{
    public RepositorioLogin() : base() { //llamo repo base

    }

    public Usuario Verificar(LoginViewModel model) // acepto objetp
    {
        Usuario usuarioEncontrado = null;

        try
        {
            using(MySqlConnection connection = new MySqlConnection(ConnectionString)){//ConnectionStrin es heredado del RepositorioBase
                connection.Open(); 

                string query = "SELECT * FROM usuario WHERE email = @email";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", model.Email);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) // busca datos, si encuentra devuelve true
                {
                    string passwordHash = reader["password"].ToString(); // recupero passs
                    if (VerificarHashPassword(model.Password, passwordHash))
                    {
                        usuarioEncontrado = new Usuario //  pass correcto creo objeto y lo paso
                        {
                            IdUsuario = reader.GetInt32("idUsuario"),
                            Nombre = reader["nombre"].ToString(),
                            Email = reader["email"].ToString(),
                            Rol = reader["rol"].ToString(),
                            Apellido= reader["apellido"].ToString(),
                            Avatar = reader.GetString("avatar")
                            
                        };
                    }
                }

                reader.Close(); // cierro conex
            }
        }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar usuario", ex); // MANEJO ERROR
            }
        

        return usuarioEncontrado;
    }

    private bool VerificarHashPassword(string passwordProporcionada, string hashAlmacenado)
    {
        // Usar BCrypt para verificar la contraseña
        bool verificar = BCrypt.Net.BCrypt.Verify(passwordProporcionada, hashAlmacenado);
        // bool verificar = BCrypt.Net.BCrypt.Verify(passwordProporcionada, hashAlmacenado); 
        return verificar;
    }

}
