using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class SeguimientoViewModel
    {
        public Siniestro Siniestro { get; set; } // Información del siniestro
        public Presupuesto Presupuesto { get; set; } // Detalles del presupuesto asociado
        public List<Reclamacion> Reclamaciones { get; set; } // Lista de reclamaciones asociadas al siniestro
        public string EstadoActual { get; set; } // Estado actual del siniestro o su gestión

        public SeguimientoViewModel()
        {
            Reclamaciones = new List<Reclamacion>();
        }
    }
}
