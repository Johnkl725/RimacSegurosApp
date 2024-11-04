using MiAplicacion.Data;
using EntidadesProyecto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Azure.Core;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AccesoDatos
{
    public class UsuarioDA
    {
        private readonly MyDbContext _context;

        public UsuarioDA(MyDbContext context)
        {
            _context = context;
        }

        // Método para registrar un usuario
        public Usuario RegistrarUsuario(Usuario usuario, string userType, string password, string placa, string marca, string modelo, string tipo, int tarjeta_vehiculo)
        {
            try
            {
                // Crear los parámetros para el procedimiento almacenado
                var idParam = new SqlParameter("@IdUsuario", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento para registrar el usuario
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_RegistrarUsuario @Nombres, @Apellido1, @Apellido2, @DNI, @Telefono, @LiderSogId, @IdUsuario OUTPUT",
                    new SqlParameter("@Nombres", usuario.Nombres),
                    new SqlParameter("@Apellido1", usuario.Apellido1),
                    new SqlParameter("@Apellido2", usuario.Apellido2 ?? (object)DBNull.Value),
                    new SqlParameter("@DNI", usuario.Dni),
                    new SqlParameter("@Telefono", usuario.Telefono ?? (object)DBNull.Value),
                    new SqlParameter("@LiderSogId", usuario.LiderSogId ?? (object)DBNull.Value),
                    idParam
                );

                // Obtener el ID de usuario insertado
                int userId = (int)idParam.Value;

                // Comprobar si se obtuvo un ID válido
                if (userId <= 0)
                {
                    throw new Exception("El usuario no se registró correctamente, ID de usuario devuelto es 0.");
                }

                // Insertar en la tabla correspondiente según el tipo de usuario
                switch (userType.ToLower())
                {
                    case "beneficiario":
                        var beneficiario = usuario as Beneficiario;
                        if (beneficiario != null)
                        {
                            beneficiario.Id = userId;
                            beneficiario.Contraseña = password;

                            var vehiculo = new Vehiculo
                            {
                                Placa = placa,
                                Marca = marca,
                                Modelo = modelo,
                                Tipo = tipo,
                                TarjetaVehiculo = tarjeta_vehiculo
                            };

                            var idVehiculoParam = new SqlParameter("@IdVehiculo", SqlDbType.Int)
                            {
                                Direction = ParameterDirection.Output
                            };

                            _context.Database.ExecuteSqlRaw(
                                "EXEC sp_InsertarVehiculo @Placa, @Marca, @Modelo, @Tipo, @TarjetaVehiculo, @IdVehiculo OUTPUT",
                                new SqlParameter("@Placa", vehiculo.Placa),
                                new SqlParameter("@Marca", vehiculo.Marca),
                                new SqlParameter("@Modelo", vehiculo.Modelo ?? (object)DBNull.Value),
                                new SqlParameter("@Tipo", vehiculo.Tipo ?? (object)DBNull.Value),
                                new SqlParameter("@TarjetaVehiculo", (object)vehiculo.TarjetaVehiculo ?? DBNull.Value),
                                idVehiculoParam
                            );

                            // Obtener el ID del vehículo insertado
                            int idVehiculo = (int)idVehiculoParam.Value;

                            // Ahora puedes usar el idVehiculo para registrar al beneficiario
                            _context.Database.ExecuteSqlRaw(
                                "EXEC sp_RegistrarBeneficiario @id_usuario, @IdVehiculo, @Password",
                                new SqlParameter("@id_usuario", userId),
                                new SqlParameter("@IdVehiculo", idVehiculo),
                                new SqlParameter("@Password", password)
                            );
                        }
                        break;

                    case "personal":
                        var personal = usuario as Personal;
                        if (personal != null)
                        {
                            personal.Id = userId;
                            personal.Contraseña = password;

                            _context.Database.ExecuteSqlRaw(
                                "EXEC sp_RegistrarPersonal @id_usuario, @Password",
                                new SqlParameter("@id_usuario", userId),
                                new SqlParameter("@Password", password)
                            );
                        }
                        break;

                    case "administrador":
                        var administrador = usuario as Administrador;
                        if (administrador != null)
                        {
                            administrador.Id = userId;
                            administrador.Contraseña = password;

                            _context.Database.ExecuteSqlRaw(
                                "EXEC sp_RegistrarAdministrador @id_usuario, @Password",
                                new SqlParameter("@id_usuario", userId),
                                new SqlParameter("@Password", password)
                            );
                        }
                        break;

                    default:
                        throw new ArgumentException("Tipo de usuario desconocido.");
                }

                // Asignar el ID de usuario al objeto usuario
                usuario.Id = userId;

                return usuario;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public Usuario RegistrarUsuario(Usuario usuario, string userType, string password)
        {
            try
            {
                // Crear los parámetros para el procedimiento almacenado
                var idParam = new SqlParameter("@IdUsuario", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento para registrar el usuario
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_RegistrarUsuario @Nombres, @Apellido1, @Apellido2, @DNI, @Telefono, @LiderSogId, @IdUsuario OUTPUT",
                    new SqlParameter("@Nombres", usuario.Nombres),
                    new SqlParameter("@Apellido1", usuario.Apellido1),
                    new SqlParameter("@Apellido2", usuario.Apellido2 ?? (object)DBNull.Value),
                    new SqlParameter("@DNI", usuario.Dni),
                    new SqlParameter("@Telefono", usuario.Telefono ?? (object)DBNull.Value),
                    new SqlParameter("@LiderSogId", usuario.LiderSogId ?? (object)DBNull.Value),
                    idParam
                );

                // Obtener el ID de usuario insertado
                int userId = (int)idParam.Value;

                // Comprobar si se obtuvo un ID válido
                if (userId <= 0)
                {
                    throw new Exception("El usuario no se registró correctamente, ID de usuario devuelto es 0.");
                }

                // Insertar en la tabla correspondiente según el tipo de usuario
                switch (userType.ToLower())
                {
                    case "personal":
                        var personal = usuario as Personal;
                        if (personal != null)
                        {
                            personal.Id = userId;
                            personal.Contraseña = password;

                            _context.Database.ExecuteSqlRaw(
                                "EXEC sp_RegistrarPersonal @id_usuario, @Password",
                                new SqlParameter("@id_usuario", userId),
                                new SqlParameter("@Password", password)
                            );
                        }
                        break;

                    case "administrador":
                        var administrador = usuario as Administrador;
                        if (administrador != null)
                        {
                            administrador.Id = userId;
                            administrador.Contraseña = password;

                            _context.Database.ExecuteSqlRaw(
                                "EXEC sp_RegistrarAdministrador @id_usuario, @Password",
                                new SqlParameter("@id_usuario", userId),
                                new SqlParameter("@Password", password)
                            );
                        }
                        break;

                    default:
                        throw new ArgumentException("Tipo de usuario desconocido.");
                }

                // Asignar el ID de usuario al objeto usuario
                usuario.Id = userId;

                return usuario;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public PerfilViewModel ObtenerPerfilPorDni(string dni)
        {
            PerfilViewModel perfil = null;

            // Crear el parámetro para el DNI
            var dniParam = new SqlParameter("@Dni", dni);

            // Usar ExecuteSqlRaw para ejecutar el procedimiento y obtener resultados
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "EXEC ObtenerUsuarioPorDni @Dni";
                command.Parameters.Add(dniParam);

                _context.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    // Leer los datos del SqlDataReader
                    if (result.Read())
                    {
                        perfil = new PerfilViewModel
                        {
                            Dni = result["Dni"].ToString(),
                            Nombres = result["nombres"].ToString(),
                            Apellido1 = result["apellido1"].ToString(),
                            Apellido2 = result["apellido2"].ToString()
                        };
                    }
                }
            }

            return perfil;
        }






        public string AuthenticateUser(string dni, string password)
        {
            // Paso 1: Obtener el ID del usuario
            int idUsuario;
            var idParam = new SqlParameter("@IdUsuario", SqlDbType.Int) { Direction = ParameterDirection.Output };
            _context.Database.ExecuteSqlRaw("EXEC sp_ObtenerIdPorDNI @DNI = {0}, @IdUsuario = {1} OUTPUT", dni, idParam);
            idUsuario = (int)idParam.Value;

            // Verificar si se encontró el usuario
            if (idUsuario <= 0)
            {
                return null; // No se encontró el usuario
            }

            // Paso 2: Autenticar al usuario
            string tipoUsuario;
            var tipoParam = new SqlParameter("@TipoUsuario", SqlDbType.VarChar, 50) { Direction = ParameterDirection.Output };
            _context.Database.ExecuteSqlRaw("EXEC sp_AutenticarUsuario @IdUsuario = {0}, @Contraseña = {1}, @TipoUsuario = {2} OUTPUT", idUsuario, password, tipoParam);
            tipoUsuario = (string)tipoParam.Value;

            if(string.IsNullOrWhiteSpace(tipoUsuario))
            {
                return null; // La contraseña es incorrecta
            }

            return tipoUsuario;


        }








    }
}
