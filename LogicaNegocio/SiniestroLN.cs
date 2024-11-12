using AccesoDatos;
using EntidadesProyecto;
using MiAplicacion.Data;
using Microsoft.EntityFrameworkCore;
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
        public Task<List<Departamento>> ObtenerDepartamentosAsync()
        {
            return _siniestroDA.ObtenerDepartamentosAsync();
        }

        public Task<List<Provincia>> ObtenerProvinciasPorDepartamentoAsync(int departamentoId)
        {
            return _siniestroDA.ObtenerProvinciasPorDepartamentoAsync(departamentoId);
        }

        public Task<List<Distrito>> ObtenerDistritosPorProvinciaAsync(int provinciaId)
        {
            return _siniestroDA.ObtenerDistritosPorProvinciaAsync(provinciaId);
        }
        public async Task<List<Siniestro>> ObtenerTodosLosSiniestros()
        {
            return await _siniestroDA.ObtenerTodosLosSiniestrosAsync();
        }

    }
}
