using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class SeguimientoViewModel
    {
        public int IdSiniestro { get; set; }
        public string TipoSiniestro { get; set; }
        public DateTime FechaSiniestro { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
        public int? PresupuestoId { get; set; }
        public string EstadoPresupuesto { get; set; }
        public decimal? MontoTotalPresupuesto { get; set; }
        public int? TallerId { get; set; }
        public string NombreTaller { get; set; }
        public string DireccionTaller { get; set; }
        public string TelefonoTaller { get; set; }
        public ICollection<Reclamacion> Reclamaciones { get; set; } = new List<Reclamacion>();

    }
}
