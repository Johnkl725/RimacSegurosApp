using AccesoDatos;
using EntidadesProyecto;
using MiAplicacion.Data;
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


    }
}
