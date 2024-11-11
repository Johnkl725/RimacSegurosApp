using AccesoDatos;
using Microsoft.EntityFrameworkCore;
using MiAplicacion.Data;
using EntidadesProyecto;
namespace LogicaNegocio
{
    public class UsuarioLN
    {
        private readonly UsuarioDA _usuarioDA;

        public UsuarioLN(MyDbContext context)
        {
            _usuarioDA = new UsuarioDA(context);
        }

        public Usuario CrearUsuario(string nombres, string apellido1, string apellido2, string dni, string telefono,string contraseña, string userType)
        {
            if (string.IsNullOrWhiteSpace(userType))
            {
                throw new ArgumentException("El tipo de usuario no puede estar vacío o nulo.");
            }

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
                        Telefono = telefono
                    };
                    break;
                default:
                    throw new ArgumentException("Tipo de usuario desconocido.");
            }

            return _usuarioDA.RegistrarBeneficiario(usuario, userType,contraseña);
        }
        public Usuario RegisterUser(string nombres, string apellido1, string apellido2, string dni, string telefono, string password, string userType)
        {
            // Verificar que userType no esté vacío o nulo
            if (string.IsNullOrWhiteSpace(userType))
            {
                throw new ArgumentException("El tipo de usuario no puede estar vacío o nulo.");
            }

            // Crear una instancia del objeto Usuario
            Usuario usuario;
            switch (userType.ToLower())
            {
                case "personal":
                    usuario = new Personal
                    {
                        Nombres = nombres,
                        Apellido1 = apellido1,
                        Apellido2 = apellido2,
                        Dni = dni,
                        Telefono = telefono
                    };
                    break;

                case "administrador":
                    usuario = new Administrador
                    {
                        Nombres = nombres,
                        Apellido1 = apellido1,
                        Apellido2 = apellido2,
                        Dni = dni,
                        Telefono = telefono
                    };
                    break;

                default:
                    throw new ArgumentException("Tipo de usuario desconocido.");
            }

            // Registrar el usuario en la base de datos
            return _usuarioDA.RegistrarUsuario(usuario, userType, password);
        }

        public string LoginUser(string username, string password)
        {
            // Llamar a UsuarioDA para autenticar el usuario
            return _usuarioDA.AuthenticateUser(username, password);
        }
        public PerfilViewModel ObtenerPerfilPorDni(string dni)
        {
            // Llama al método en UsuarioDA para obtener el perfil y lo retorna
            return _usuarioDA.ObtenerPerfilPorDni(dni);
        }

    }
}
