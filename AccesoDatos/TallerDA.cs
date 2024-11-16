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
                            Id = reader["Id"] != DBNull.Value ? (int)reader["Id"] : 0,
                            IdProveedor = reader["id_proveedor"] != DBNull.Value ? (int)reader["id_proveedor"] : 0,
                            Nombre = reader["Nombre"] != DBNull.Value ? reader["Nombre"].ToString() : string.Empty,
                            Direccion = reader["Direccion"] != DBNull.Value ? reader["Direccion"].ToString() : string.Empty,
                            Telefono = reader["Telefono"] != DBNull.Value ? reader["Telefono"].ToString() : string.Empty,
                            Correo = reader["Correo"] != DBNull.Value ? reader["Correo"].ToString() : string.Empty,
                            Ciudad = reader["Ciudad"] != DBNull.Value ? reader["Ciudad"].ToString() : string.Empty,
                            Tipo = reader["Tipo"] != DBNull.Value ? reader["Tipo"].ToString() : string.Empty,
                            Capacidad = reader["Capacidad"] != DBNull.Value ? (int)reader["Capacidad"] : 0,
                            Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : string.Empty,
                            Calificacion = reader["Calificacion"] != DBNull.Value ? reader["Calificacion"].ToString() : string.Empty,
                            Estado = reader["Estado"] != DBNull.Value ? reader["Estado"].ToString() : string.Empty
                        };
                    }
                    return null;
                }
            }
        }



        public List<Taller> ObtenerTodosLosTalleres()
        {
            List<Taller> talleres = new List<Taller>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spObtenerTalleres", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Taller taller = new Taller
                        {
                            Id = reader["id"] != DBNull.Value ? (int)reader["id"] : 0,
                            IdProveedor = reader["id_proveedor"] != DBNull.Value ? (int)reader["id_proveedor"] : 0,
                            Nombre = reader["nombre"] != DBNull.Value ? reader["nombre"].ToString() : string.Empty,
                            // Añadir todas las propiedades
                        };

                        talleres.Add(taller);
                    }
                }
            }

            // Prueba si se están obteniendo datos
            Console.WriteLine($"Total talleres: {talleres.Count}");
            return talleres;
        }


    }
}