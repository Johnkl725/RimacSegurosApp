using DbContext;
using Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;


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
        public bool RegistrarUsuario(Usuario usuario, string userType, string password)
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
                        if (beneficiario == null)
                        {
                            throw new InvalidOperationException("El tipo de usuario no es válido para Beneficiario.");
                        }
                        _context.Database.ExecuteSqlRaw(
                            "EXEC sp_RegistrarBeneficiario @id_usuario, @IdVehiculo, @Password",
                            new SqlParameter("@id_usuario", userId),
                            new SqlParameter("@IdVehiculo", beneficiario.IdVehiculo),
                            new SqlParameter("@Password", password)
                        );
                        break;

                    case "personal":
                        _context.Database.ExecuteSqlRaw(
                            "EXEC sp_RegistrarPersonal @id_usuario, @Password",
                            new SqlParameter("@id_usuario", userId),
                            new SqlParameter("@Password", password)
                        );
                        break;

                    case "administrador":
                        _context.Database.ExecuteSqlRaw(
                            "EXEC sp_RegistrarAdministrador @id_usuario, @Password",
                            new SqlParameter("@id_usuario", userId),
                            new SqlParameter("@Password", password)
                        );
                        break;

                    default:
                        throw new ArgumentException("Tipo de usuario desconocido.");
                }

                return true;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public Usuario AuthenticateUser(string dni, string password)
        {
            try
            {
                // Ejecutar el procedimiento almacenado para validar al usuario general
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Dni == dni && u.Contraseña == password);

                if (usuario != null)
                {
                    // Consultar si el usuario es Beneficiario
                    var beneficiario = _context.Beneficiarios.FirstOrDefault(b => b.IdUsuario == usuario.Id);
                    if (beneficiario != null)
                    {
                        return beneficiario; // Devolver Beneficiario
                    }

                    // Consultar si el usuario es Personal
                    var personal = _context.Personales.FirstOrDefault(p => p.IdUsuario == usuario.Id);
                    if (personal != null)
                    {
                        return personal; // Devolver Personal
                    }

                    // Consultar si el usuario es Administrador
                    var administrador = _context.Administradores.FirstOrDefault(a => a.IdUsuario == usuario.Id);
                    if (administrador != null)
                    {
                        return administrador; // Devolver Administrador
                    }
                }

                return null; // Si no se encontró el usuario o no coincide la contraseña
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
