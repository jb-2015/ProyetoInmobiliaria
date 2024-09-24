//repositorioUsuario
using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;
using BCrypt.Net;

public class RepositorioUsuario:RepositorioBase{
public RepositorioUsuario(): base(){  

    }

    public int Guardar(Usuario usuario){

        int  idCreado=-1;
        using(MySqlConnection connection= new MySqlConnection(ConnectionString)){
            connection.Open();
            string query="INSERT INTO usuario (email,password,rol,avatar,nombre,apellido,estado)"+
            "VALUES(@EmailUsuario,@PasswordUsuario,@RolUsuario,@AvatarUsuario,@NombreUsuario,@ApellidoUsuario,@EstadoUsuario)";

            using (MySqlCommand command=  new MySqlCommand(query,connection)){
                command.Parameters.AddWithValue("@EmailUsuario", usuario.Email);
                command.Parameters.AddWithValue("@PasswordUsuario", encriptar(usuario.Password));
                command.Parameters.AddWithValue("@RolUsuario",usuario.Rol);
                command.Parameters.AddWithValue("@AvatarUsuario",usuario.Avatar);
                command.Parameters.AddWithValue("@NombreUsuario",usuario.Nombre);
                command.Parameters.AddWithValue("@ApellidoUsuario",usuario.Apellido);
                command.Parameters.AddWithValue("@EstadoUsuario","1");
                idCreado=  Convert.ToInt32(command.ExecuteScalar());
            };
            

        }
        return idCreado;
        
    }
    public string encriptar(string pass){
        int workFactor= 12;
        string salt=  BCrypt.Net.BCrypt.GenerateSalt(workFactor);
        string hashedPassword=  BCrypt.Net.BCrypt.HashPassword(pass,salt);
        return hashedPassword;
    }



    // listar todos los usuarios activos
    public List<Usuario> Listar()
    {
        List<Usuario> usuarios = new List<Usuario>();
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string query = "SELECT * FROM usuario WHERE estado = 1"; 
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            IdUsuario = reader.GetInt32("idUsuario"),
                            Email = reader.GetString("email"),
                            Password = reader.GetString("password"),
                            Rol = reader.GetString("rol"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Estado = reader.GetBoolean("estado")
                        };
                        usuarios.Add(usuario);
                    }
                }
            }
        }
        return usuarios;
    }


    public Usuario Obtener(int id){
        Usuario usuario = null;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string query = "SELECT * FROM usuario WHERE idUsuario = @IdUsuario"; 
            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdUsuario", id);
                using (MySqlDataReader reader = command.ExecuteReader()){
                    if (reader.Read()){
                        usuario = new Usuario{
                            IdUsuario = reader.GetInt32("idUsuario"),
                            Email = reader.GetString("email"),
                            Password = reader.GetString("password"),
                            Rol = reader.GetString("rol"),
                            Avatar = reader.IsDBNull(reader.GetOrdinal("avatar")) ? null : reader.GetString("avatar"),
                            Nombre = reader.GetString("nombre"),
                            Apellido = reader.GetString("apellido"),
                            Estado = reader.GetBoolean("estado")
                        };
                    }
                }
            }
        }
        return usuario;
    }

    //  actualizar un usuario 
    public int Actualizar(Usuario usuario)
    {
        int filasAfectadas = 0;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();
            string query = @"UPDATE usuario 
                            SET email = @EmailUsuario, password = @PasswordUsuario, rol = @RolUsuario, avatar = @AvatarUsuario, 
                                nombre = @NombreUsuario, apellido = @ApellidoUsuario 
                            WHERE id = @IdUsuario";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@EmailUsuario", usuario.Email);
                command.Parameters.AddWithValue("@PasswordUsuario", encriptar(usuario.Password));
                command.Parameters.AddWithValue("@RolUsuario", usuario.Rol);
                command.Parameters.AddWithValue("@AvatarUsuario", usuario.Avatar);
                command.Parameters.AddWithValue("@NombreUsuario", usuario.Nombre);
                command.Parameters.AddWithValue("@ApellidoUsuario", usuario.Apellido);
                command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

    // eliminar un usuario
    public int Eliminar(int id){
        int filasAfectadas = 0;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE usuario SET estado = false WHERE idUsuario = @IdUsuario"; 
            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdUsuario", id);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }
}
