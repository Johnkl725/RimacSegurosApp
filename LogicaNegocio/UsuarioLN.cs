using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using DbContext;
using Entidades;
namespace LogicaNegocio
{
    public class UsuarioLN
    {
        private readonly MyDbContext _context;

        public UsuarioLN(MyDbContext context)
        {
            _context = context;
        }

        public bool RegisterUser(string nombres, string apellido1, string apellido2, string dni, string telefono, string liderSogId, string password, string userType)
        {
            // Verificar que userType no esté vacío o nulo
            if (string.IsNullOrWhiteSpace(userType))
            {
                throw new ArgumentException("El tipo de usuario no puede estar vacío o nulo.");
            }

            // Crear una instancia del objeto Usuario según el tipo de usuario
            Usuario usuario;

            switch (userType.ToLower())
            {
                case "beneficiario":
                    usuario = new Beneficiario
                    {
                        Nombres = nombres,
                        Apellido1 = apellido1,
                        Apellido2 = apellido2,
                        Dni = dni,
                        Telefono = telefono,
                        LiderSogId = liderSogId
                    };
                    break;

                case "personal":
                    usuario = new Personal
                    {
                        Nombres = nombres,
                        Apellido1 = apellido1,
                        Apellido2 = apellido2,
                        Dni = dni,
                        Telefono = telefono,
                        LiderSogId = liderSogId
                    };
                    break;

                case "administrador":
                    usuario = new Administrador
                    {
                        Nombres = nombres,
                        Apellido1 = apellido1,
                        Apellido2 = apellido2,
                        Dni = dni,
                        Telefono = telefono,
                        LiderSogId = liderSogId
                    };
                    break;

                default:
                    throw new ArgumentException("Tipo de usuario desconocido.");
            }
            // Crear una instancia de UsuarioDA y registrar el usuario
            UsuarioDA usuarioDA = new UsuarioDA(_context);
            return usuarioDA.RegisterUser(usuario, password, userType);
        }
        public Usuario LoginUser(string dni, string password)
        {
            // Llamar a UsuarioDA para autenticar el usuario
            UsuarioDA usuarioDA = new UsuarioDA(_context);
            return usuarioDA.AuthenticateUser(dni, password);
        }
    }
}
