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

        public (string TipoUsuario, int IdUsuario) LoginUser(string username, string password)
        {
            // Verificar credenciales
            var tipoUsuario = _usuarioDA.AuthenticateUser(username, password);
            if (tipoUsuario == null) return (null, 0);

            // Obtener IdUsuario
            int idUsuario = _usuarioDA.ObtenerIdUsuarioPorDni(username);

            return (tipoUsuario, idUsuario);
        }
        public PerfilViewModel ObtenerPerfilPorDni(string dni)
        {
            // Llama al método en UsuarioDA para obtener el perfil y lo retorna
            return _usuarioDA.ObtenerPerfilPorDni(dni);
        }
        public IEnumerable<Usuario> ObtenerUsuariosPorTipo(string tipoUsuario)
        {
            // Llama al método en UsuarioDA para obtener la lista de usuarios del tipo especificado y la retorna
            return _usuarioDA.ObtenerUsuariosPorTipo(tipoUsuario);
        }
        public IEnumerable<Usuario> ObtenerUsuariosPorDni(string dni)
        {
            return _usuarioDA.ObtenerUsuariosPorDni(dni);
        }

        public Usuario ObtenerUsuarioPorId(int id)
        {
            return _usuarioDA.ObtenerUsuarioPorId(id);
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            // Validar los datos del usuario si es necesario
            if (string.IsNullOrWhiteSpace(usuario.Nombres) || string.IsNullOrWhiteSpace(usuario.Dni))
            {
                throw new ArgumentException("Los campos Nombre y DNI son obligatorios.");
            }

            // Llamar a la capa de datos para actualizar
            return _usuarioDA.ActualizarUsuario(usuario);
        }

        public void EliminarUsuario(int id)
        {
            // Verificar si el usuario existe antes de intentar eliminarlo
            var usuario = _usuarioDA.ObtenerUsuarioPorId(id);
            if (usuario == null)
            {
                throw new InvalidOperationException("El usuario no existe.");
            }

            // Llamar a la capa de datos para eliminar
            _usuarioDA.EliminarUsuario(id);
        }



    }
}
