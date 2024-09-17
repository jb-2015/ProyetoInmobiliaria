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
}
