using MySql.Data.MySqlClient;
using ProyetoInmobiliaria.Models;

public class RepositorioInmueble: RepositorioBase{
RepositorioDireccion repoDire = new RepositorioDireccion();


    private readonly RepositorioPropietario repoPropie = new RepositorioPropietario();
    private readonly RepositorioTipo repoTipo = new RepositorioTipo();
    
    public RepositorioInmueble() : base(){



    }
    
    // Crear
    public int Crear(Inmueble inmueble){
        int idCreado = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "INSERT INTO inmueble (idPropietario, idDireccion, idTipo, metros2,"+
            " cantidadAmbientes, disponible, precio, descripcion, cochera, piscina, mascotas, estado,UrlImagen)"+
            " VALUES (@IdPropietario, @IdDireccion, @IdTipo, @Metros2, @CantidadAmbientes, @Disponible,"+
            " @Precio, @Descripcion, @Cochera, @Piscina, @Mascotas, true, @UrlImagen);  SELECT LAST_INSERT_ID();";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);
                command.Parameters.AddWithValue("@IdDireccion", inmueble.IdDireccion);
                command.Parameters.AddWithValue("@IdTipo", inmueble.IdTipo);
                command.Parameters.AddWithValue("@Metros2", inmueble.Metros2);
                command.Parameters.AddWithValue("@CantidadAmbientes", inmueble.CantidadAmbientes);
                command.Parameters.AddWithValue("@Disponible", inmueble.Disponible);
                command.Parameters.AddWithValue("@Precio", inmueble.Precio);
                command.Parameters.AddWithValue("@Descripcion", inmueble.Descripcion);
                command.Parameters.AddWithValue("@Cochera", inmueble.Cochera);
                command.Parameters.AddWithValue("@Piscina", inmueble.Piscina);
                command.Parameters.AddWithValue("@Mascotas", inmueble.Mascotas);
                command.Parameters.AddWithValue("@UrlImagen", inmueble.UrlImagen);
                idCreado = Convert.ToInt32(command.ExecuteScalar());
            }
        }
        return idCreado;
    } 

    // Modificar
    public int Modificar(Inmueble inmueble){
        int filasAfectadas = 0;
        Console.WriteLine(@$"Estado del inmueble {inmueble.Estado} idDelInmueble {inmueble.IdInmueble}");
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE inmueble SET idPropietario = @IdPropietario, idDireccion = @IdDireccion,"+
            "idTipo = @IdTipo, metros2 = @Metros2, cantidadAmbientes = @CantidadAmbientes, disponible = @Disponible, "+
            "precio = @Precio, descripcion = @Descripcion, cochera = @Cochera, piscina = @Piscina, mascotas = @Mascotas, UrlImagen=@UrlImagen, "+
            "estado = @Estado WHERE idInmueble = @IdInmueble";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPropietario", inmueble.IdPropietario);
                command.Parameters.AddWithValue("@IdDireccion", inmueble.IdDireccion);
                command.Parameters.AddWithValue("@IdTipo", inmueble.IdTipo);
                command.Parameters.AddWithValue("@Metros2", inmueble.Metros2);
                command.Parameters.AddWithValue("@CantidadAmbientes", inmueble.CantidadAmbientes);
                command.Parameters.AddWithValue("@Disponible", inmueble.Disponible);
                command.Parameters.AddWithValue("@Precio", inmueble.Precio);
                command.Parameters.AddWithValue("@Descripcion", inmueble.Descripcion);
                command.Parameters.AddWithValue("@Cochera", inmueble.Cochera);
                command.Parameters.AddWithValue("@Piscina", inmueble.Piscina);
                command.Parameters.AddWithValue("@Mascotas", inmueble.Mascotas);
                command.Parameters.AddWithValue("@IdInmueble", inmueble.IdInmueble);
                command.Parameters.AddWithValue("@UrlImagen",inmueble.UrlImagen);
                command.Parameters.AddWithValue("@Estado",inmueble.Estado);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    } 

    // Listar
    public List<Inmueble> Listar(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM inmueble WHERE estado = true";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Propietario p = repoPropie.Obtener(reader.GetInt32("IdPropietario"));
                        Direccion d = repoDire.Obtener(reader.GetInt32("IdDireccion"));
                        Tipo t = repoTipo.Obtener(reader.GetInt32("IdTipo"));
                        Inmueble inmueble = new Inmueble{
                            IdPropietario= p.IdPropietario,
                            IdDireccion= d.IdDireccion,
                            IdTipo = t.IdTipo,
                            IdInmueble = reader.GetInt32("IdInmueble"),
                            propietario = p,
                            direccion = d,
                            tipo = t,
                            Metros2 = reader.GetString("metros2"),
                            CantidadAmbientes = reader.GetInt32("cantidadAmbientes"),
                            Disponible = reader.GetBoolean("disponible"),
                            Precio = reader.GetDecimal("precio"),
                            Descripcion = reader.GetString("descripcion"),
                            Cochera = reader.GetBoolean("cochera"),
                            Piscina = reader.GetBoolean("piscina"),
                            Mascotas = reader.GetBoolean("mascotas"),
                            Estado = reader.GetBoolean("estado"),
                            UrlImagen= reader.GetString("UrlImagen")
                            };
                        inmuebles.Add(inmueble);
                    }
                }
            }
        }
        return inmuebles;
    }

    public List<Inmueble> ListarNoDisponibles(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM inmueble WHERE estado = false";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Propietario p = repoPropie.Obtener(reader.GetInt32("IdPropietario"));
                        Direccion d = repoDire.Obtener(reader.GetInt32("IdDireccion"));
                        Tipo t = repoTipo.Obtener(reader.GetInt32("IdTipo"));
                        Inmueble inmueble = new Inmueble{
                            IdPropietario= p.IdPropietario,
                            IdDireccion= d.IdDireccion,
                            IdTipo = t.IdTipo,
                            IdInmueble = reader.GetInt32("IdInmueble"),
                            propietario = p,
                            direccion = d,
                            tipo = t,
                            Metros2 = reader.GetString("metros2"),
                            CantidadAmbientes = reader.GetInt32("cantidadAmbientes"),
                            Disponible = reader.GetBoolean("disponible"),
                            Precio = reader.GetDecimal("precio"),
                            Descripcion = reader.GetString("descripcion"),
                            Cochera = reader.GetBoolean("cochera"),
                            Piscina = reader.GetBoolean("piscina"),
                            Mascotas = reader.GetBoolean("mascotas"),
                            Estado = reader.GetBoolean("estado"),
                            UrlImagen= reader.GetString("UrlImagen")
                            };
                        inmuebles.Add(inmueble);
                    }
                }
            }
        }
        return inmuebles;
    }
        public List<Inmueble> ListarPorPropietario(int id){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM inmueble WHERE idPropietario = @IdPropietario";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdPropietario", id);
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Propietario p = repoPropie.Obtener(reader.GetInt32("IdPropietario"));
                        Direccion d = repoDire.Obtener(reader.GetInt32("IdDireccion"));
                        Tipo t = repoTipo.Obtener(reader.GetInt32("IdTipo"));
                        Inmueble inmueble = new Inmueble{
                            IdPropietario= p.IdPropietario,
                            IdDireccion= d.IdDireccion,
                            IdTipo = t.IdTipo,
                            IdInmueble = reader.GetInt32("IdInmueble"),
                            propietario = p,
                            direccion = d,
                            tipo = t,
                            Metros2 = reader.GetString("metros2"),
                            CantidadAmbientes = reader.GetInt32("cantidadAmbientes"),
                            Disponible = reader.GetBoolean("disponible"),
                            Precio = reader.GetDecimal("precio"),
                            Descripcion = reader.GetString("descripcion"),
                            Cochera = reader.GetBoolean("cochera"),
                            Piscina = reader.GetBoolean("piscina"),
                            Mascotas = reader.GetBoolean("mascotas"),
                            Estado = reader.GetBoolean("estado"),
                            UrlImagen= reader.GetString("UrlImagen")
                            };
                        inmuebles.Add(inmueble);
                    }
                }
            }
        }
        return inmuebles;
    }

    public List<Inmueble> ListarPorFechas(DateTime fechaInicio, DateTime fechaFin){
        Console.WriteLine($"Fecha de Inicio: {fechaInicio}, Fecha de Fin: {fechaFin}");
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT i.* FROM inmueble i WHERE NOT EXISTS(SELECT 1 FROM contrato c WHERE c.idInmueble = i.idInmueble AND (c.fechaInicio < @FechaFin AND c.fechaFin > @FechaInicio)) AND i.estado = true;";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                command.Parameters.AddWithValue("@FechaFin", fechaFin);
                using(MySqlDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        Propietario p = repoPropie.Obtener(reader.GetInt32("IdPropietario"));
                        Direccion d = repoDire.Obtener(reader.GetInt32("IdDireccion"));
                        Tipo t = repoTipo.Obtener(reader.GetInt32("IdTipo"));
                        Inmueble inmueble = new Inmueble{
                            IdPropietario= p.IdPropietario,
                            IdDireccion= d.IdDireccion,
                            IdTipo = t.IdTipo,
                            IdInmueble = reader.GetInt32("IdInmueble"),
                            propietario = p,
                            direccion = d,
                            tipo = t,
                            Metros2 = reader.GetString("metros2"),
                            CantidadAmbientes = reader.GetInt32("cantidadAmbientes"),
                            Disponible = reader.GetBoolean("disponible"),
                            Precio = reader.GetDecimal("precio"),
                            Descripcion = reader.GetString("descripcion"),
                            Cochera = reader.GetBoolean("cochera"),
                            Piscina = reader.GetBoolean("piscina"),
                            Mascotas = reader.GetBoolean("mascotas"),
                            Estado = reader.GetBoolean("estado"),
                            UrlImagen= reader.GetString("UrlImagen")
                            };
                        inmuebles.Add(inmueble);
                    }
                }
            }
        }
        return inmuebles;
    }

    // Obtener
    public Inmueble Obtener(int idInmueble){
        Inmueble inmueble = null;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "SELECT * FROM inmueble WHERE idInmueble = @IdInmueble";
            using (MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdInmueble", idInmueble); // Corregido el nombre del parámetro
                using (MySqlDataReader reader = command.ExecuteReader()){
                    if (reader.Read()){
                        Propietario p = repoPropie.Obtener(reader.GetInt32("idPropietario"));
                        Direccion d = repoDire.Obtener(reader.GetInt32("idDireccion"));
                        Tipo t = repoTipo.Obtener(reader.GetInt32("idTipo"));
                        inmueble = new Inmueble{
                            IdPropietario= reader.GetInt32("idPropietario"),
                            IdDireccion = reader.GetInt32("idDireccion"),
                            IdTipo = reader.GetInt32("idTipo"),
                            IdInmueble = reader.GetInt32("idInmueble"),
                            propietario = p,
                            direccion = d,
                            tipo = t,
                            Metros2 = reader.GetString("metros2"),
                            CantidadAmbientes = reader.GetInt32("cantidadAmbientes"),
                            Disponible = reader.GetBoolean("disponible"),
                            Precio = reader.GetDecimal("precio"),
                            Descripcion = reader.GetString("descripcion"),
                            Cochera = reader.GetBoolean("cochera"),
                            Piscina = reader.GetBoolean("piscina"),
                            Mascotas = reader.GetBoolean("mascotas"),
                            Estado = reader.GetBoolean("estado"),
                            UrlImagen= reader.GetString("urlImagen")
                        };
                    }
                }
            }
        }
        return inmueble;
    }


      
    // Eliminar
    public int Eliminar(int IdInmueble){
        int filasAfectadas = 0;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
            connection.Open();
            string query = "UPDATE inmueble SET estado = false WHERE idInmueble = @IdInmueble";
            using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@IdInmueble", IdInmueble);
                filasAfectadas = command.ExecuteNonQuery();
            }
        }
        return filasAfectadas;
    }

}