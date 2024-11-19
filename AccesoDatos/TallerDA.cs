using System.Collections.Generic;
using EntidadesProyecto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace AccesoDatos
{
    public class TallerDA
    {
        private readonly string connectionString;

        // Constructor que recibe la configuración
        public TallerDA(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int AgregarTaller(Taller taller)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAgregarTaller", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_proveedor", taller.IdProveedor);
                cmd.Parameters.AddWithValue("@nombre", taller.Nombre);
                cmd.Parameters.AddWithValue("@direccion", taller.Direccion);
                cmd.Parameters.AddWithValue("@telefono", taller.Telefono);
                cmd.Parameters.AddWithValue("@correo", taller.Correo);
                cmd.Parameters.AddWithValue("@ciudad", taller.Ciudad);
                cmd.Parameters.AddWithValue("@tipo", taller.Tipo);
                cmd.Parameters.AddWithValue("@capacidad", taller.Capacidad);
                cmd.Parameters.AddWithValue("@descripcion", taller.Descripcion);
                cmd.Parameters.AddWithValue("@calificacion", taller.Calificacion);
                cmd.Parameters.AddWithValue("@estado", taller.Estado);

                SqlParameter outputId = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outputId);

                conn.Open();
                cmd.ExecuteNonQuery();
                return (int)outputId.Value;
            }
        }

        public void ActualizarTaller(Taller taller)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spActualizarTaller", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", taller.Id);
                cmd.Parameters.AddWithValue("@nombre", taller.Nombre);
                cmd.Parameters.AddWithValue("@direccion", taller.Direccion);
                cmd.Parameters.AddWithValue("@telefono", taller.Telefono);
                cmd.Parameters.AddWithValue("@correo", taller.Correo);
                cmd.Parameters.AddWithValue("@ciudad", taller.Ciudad);
                cmd.Parameters.AddWithValue("@tipo", taller.Tipo);
                cmd.Parameters.AddWithValue("@capacidad", taller.Capacidad);
                cmd.Parameters.AddWithValue("@descripcion", taller.Descripcion);
                cmd.Parameters.AddWithValue("@calificacion", taller.Calificacion);
                cmd.Parameters.AddWithValue("@estado", taller.Estado);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarTaller(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spEliminarTaller", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Taller ObtenerTallerPorId(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spObtenerTallerPorId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Taller
                        {
                            Id = (int)reader["Id"],
                            IdProveedor = (int)reader["id_proveedor"], // Asegúrate de que es "ProveedorId" aquí
                            Nombre = reader["Nombre"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Ciudad = reader["Ciudad"].ToString(),
                            Tipo = reader["Tipo"].ToString(),
                            Capacidad = (int)reader["Capacidad"],
                            Descripcion = reader["Descripcion"].ToString(),
                            Calificacion = reader["Calificacion"].ToString(),
                            Estado = reader["Estado"].ToString()
                        };
                    }
                    return null;
                }
            }
        }

        public async Task<List<Taller>> ObtenerTodosLosTalleresAsync()
        {
            List<Taller> talleres = new List<Taller>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spObtenerTalleres", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                await conn.OpenAsync(); // Abrir la conexión de forma asíncrona
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync()) // Ejecutar el lector de forma asíncrona
                {
                    while (await reader.ReadAsync()) // Leer cada fila de forma asíncrona
                    {
                        Taller taller = new Taller
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            IdProveedor = reader["id_proveedor"] != DBNull.Value ? (int)reader["id_proveedor"] : 0,
                            Nombre = reader["nombre"] != DBNull.Value ? reader["nombre"].ToString() : string.Empty,
                            Direccion = reader["direccion"] != DBNull.Value ? reader["direccion"].ToString() : string.Empty,
                            Telefono = reader["telefono"] != DBNull.Value ? reader["telefono"].ToString() : string.Empty,
                            Correo = reader["correo"] != DBNull.Value ? reader["correo"].ToString() : string.Empty,
                            Ciudad = reader["ciudad"] != DBNull.Value ? reader["ciudad"].ToString() : string.Empty,
                            Tipo = reader["tipo"] != DBNull.Value ? reader["tipo"].ToString() : string.Empty,
                            Capacidad = reader["capacidad"] != DBNull.Value ? (int)reader["capacidad"] : 0,
                            Descripcion = reader["descripcion"] != DBNull.Value ? reader["descripcion"].ToString() : string.Empty,
                            Calificacion = reader["calificacion"] != DBNull.Value ? reader["calificacion"].ToString() : string.Empty,
                            Estado = reader["estado"] != DBNull.Value ? reader["estado"].ToString() : string.Empty
                        };

                        talleres.Add(taller);
                    }
                }
            }

            return talleres;
        }

    }
}
