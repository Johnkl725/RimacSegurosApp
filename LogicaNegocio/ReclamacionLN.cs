using AccesoDatos;
using EntidadesProyecto;
using MiAplicacion.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class ReclamacionLN
    {
        private readonly ReclamacionDA _reclamacionDA;

        public ReclamacionLN(MyDbContext context)
        {
            _reclamacionDA = new ReclamacionDA(context);
        }

        public void RegistrarReclamacion(Reclamacion reclamacion)
        {
            if (reclamacion == null)
                throw new ArgumentNullException(nameof(reclamacion));

            if (string.IsNullOrWhiteSpace(reclamacion.Tipo))
                throw new ArgumentException("El tipo es obligatorio.");

            if (string.IsNullOrWhiteSpace(reclamacion.Descripcion))
                throw new ArgumentException("La descripción es obligatoria.");

            _reclamacionDA.RegistrarReclamacion(reclamacion);
        }


        public List<Reclamacion> ObtenerReclamacionesPorSiniestro(int idSiniestro)
        {
            if (idSiniestro <= 0)
                throw new ArgumentException("Id de siniestro inválido.");

            return _reclamacionDA.ObtenerReclamacionesPorSiniestro(idSiniestro);
        }

        public async Task<List<Reclamacion>> ObtenerReclamacionesPorSiniestroAsync(int idSiniestro)
        {
            if (idSiniestro <= 0)
                throw new ArgumentException("Id del siniestro inválido.");

            var reclamaciones = await _reclamacionDA.ObtenerReclamacionesPorSiniestroAsync(idSiniestro);

            // Si no se encontraron reclamaciones, devolver una lista vacía en lugar de lanzar una excepción
            return reclamaciones ?? new List<Reclamacion>();
        }


        public int ObtenerIdBeneficiarioPorUsuario(int idUsuario)
        {
            return _reclamacionDA.ObtenerIdBeneficiarioPorUsuario(idUsuario);
        }

    }
}
