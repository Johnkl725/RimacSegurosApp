using AccesoDatos;
using EntidadesProyecto;
using MiAplicacion.Data;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class SiniestroLN
    {
        private readonly SiniestroDA _siniestroDA;

        public SiniestroLN(MyDbContext context)
        {
            _siniestroDA = new SiniestroDA(context);
        }

        public async Task RegistrarSiniestro(Siniestro siniestro)
        {
            // Validaciones de negocio (por ejemplo, verificar que los campos requeridos no estén vacíos)
            if (string.IsNullOrEmpty(siniestro.Tipo) || string.IsNullOrEmpty(siniestro.Ubicacion))
            {
                throw new ArgumentException("El tipo de siniestro y la ubicación son obligatorios.");
            }

            // Registrar el siniestro en la base de datos
            await _siniestroDA.RegistrarSiniestro(siniestro);
        }
        // SiniestroLN.cs
        public async Task<List<Departamento>> ObtenerDepartamentosAsync()
        {
            return await _siniestroDA.ObtenerDepartamentosAsync();
        }

        public async Task<List<Provincia>> ObtenerProvinciasPorDepartamentoAsync(int idDepartamento)
        {
            return await _siniestroDA.ObtenerProvinciasPorDepartamentoAsync(idDepartamento);
        }

        public async Task<List<Distrito>> ObtenerDistritosPorProvinciaAsync(int idProvincia)
        {
            return await _siniestroDA.ObtenerDistritosPorProvinciaAsync(idProvincia);
        }

    }
}
