using MiAplicacion.Data;
using EntidadesProyecto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Azure.Core;
using System.Numerics;
using System.Text.RegularExpressions;
using BCrypt.Net;

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
        public Usuario RegistrarBeneficiario(Usuario usuario, string userType, string password)
        {
            try
            {
                var idParam = new SqlParameter("@IdUsuario", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // Ejecutar el procedimiento para registrar el usuario
                _context.Database.ExecuteSqlRaw(
                    "EXEC sp_RegistrarUsuario @Nombres, @Apellido1, @Apellido2, @DNI, @Telefono, @UserType, @IdUsuario OUTPUT",
                    new SqlParameter("@Nombres", usuario.Nombres),
                    new SqlParameter("@Apellido1", usuario.Apellido1),
                    new SqlParameter("@Apellido2", usuario.Apellido2 ?? (object)DBNull.Value),
                    new SqlParameter("@DNI", usuario.Dni),
                    new SqlParameter("@Telefono", usuario.Telefono ?? (object)DBNull.Value),
                    new SqlParameter("@UserType", userType), // Nuevo parámetro para userType
                    idParam
                );

                // Obtener el ID de usuario insertado
                int userId = (int)idParam.Value;

                // Comprobar si se obtuvo un ID válido
                if (userId <= 0)
                {
                    throw new Exception("El usuario no se registró correctamente, ID de usuario devuelto es 0.");
                }

                // Insertar en la tabla de Beneficiario sin asignar un IdVehiculo inicialmente
                var beneficiario = usuario as Beneficiario;
                if (beneficiario != null)
                {
                    _context.Database.ExecuteSqlRaw(
                        "EXEC sp_RegistrarBeneficiario @id_usuario, @IdVehiculo, @Password",
                        new SqlParameter("@id_usuario", userId),
                        new SqlParameter("@IdVehiculo", DBNull.Value), // Se pasa NULL para id_vehiculo
                        new SqlParameter("@Password", password)
                    );
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
                    "EXEC sp_RegistrarUsuario @Nombres, @Apellido1, @Apellido2, @DNI, @Telefono, @UserType, @IdUsuario OUTPUT",
                    new SqlParameter("@Nombres", usuario.Nombres),
                    new SqlParameter("@Apellido1", usuario.Apellido1),
                    new SqlParameter("@Apellido2", usuario.Apellido2 ?? (object)DBNull.Value),
                    new SqlParameter("@DNI", usuario.Dni),
                    new SqlParameter("@Telefono", usuario.Telefono ?? (object)DBNull.Value),
                    new SqlParameter("@UserType", userType), // Nuevo parámetro
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

        public IEnumerable<Usuario> ObtenerUsuariosPorTipo(string tipoUsuario)
        {
            return _context.Usuarios
                .Where(u => u.TipoUsuario == tipoUsuario)
                .ToList();
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

            // Paso 2: Obtener la contraseña hasheada del usuario
            string contraseñaHash = null;
            var paramContraseñaHash = new SqlParameter("@ContraseñaHash", SqlDbType.VarChar, 60) { Direction = ParameterDirection.Output };
            _context.Database.ExecuteSqlRaw("EXEC sp_ObtenerContraseñaHasheadaUsuario @IdUsuario = {0}, @ContraseñaHash = {1} OUTPUT",
                idUsuario, paramContraseñaHash);

            // Verificamos si el valor de la contraseña hash es DBNull antes de asignarlo
            if (paramContraseñaHash.Value == DBNull.Value)
            {
                return null; // Si es DBNull, significa que no se encontró una contraseña
            }

            contraseñaHash = (string)paramContraseñaHash.Value;

            // Si no se encuentra la contraseña hasheada, retornamos null
            if (string.IsNullOrWhiteSpace(contraseñaHash))
            {
                return null;
            }

            // Paso 3: Verificar si la contraseña ingresada coincide con el hash almacenado
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, contraseñaHash);

            if (!isPasswordValid)
            {
                return null; // La contraseña no es válida
            }

            // Paso 4: Obtener el tipo de usuario (si la contraseña es correcta)
            string tipoUsuario;
            var tipoParam = new SqlParameter("@TipoUsuario", SqlDbType.VarChar, 60)
            {
                Direction = ParameterDirection.Output
            };

            // Ejecutar el procedimiento almacenado con el parámetro de salida
            _context.Database.ExecuteSqlRaw("EXEC sp_AutenticarUsuario @IdUsuario = {0}, @TipoUsuario = @TipoUsuario OUTPUT", idUsuario, tipoParam);

            // Verificamos si el valor del tipo de usuario es DBNull antes de asignarlo
            if (tipoParam.Value == DBNull.Value)
            {
                return null; // Si es DBNull, significa que no se obtuvo el tipo de usuario
            }

            tipoUsuario = (string)tipoParam.Value;

            if (string.IsNullOrWhiteSpace(tipoUsuario))
            {
                return null; // Si no se obtiene el tipo de usuario, retornamos null
            }

            return tipoUsuario; // El tipo de usuario si la autenticación es exitosa
        }












    }
}
