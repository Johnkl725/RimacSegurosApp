using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class SiniestroPresupuestoViewModel
    {
        public string NumeroSiniestro { get; set; } // Ejemplo: "SIN-001"
        public DateTime? FechaAsignacion { get; set; }
        public string NombreTaller { get; set; }
        public string TipoSiniestro { get; set; }
        public string Placa { get; set; }
        public int IdSiniestro { get; set; }
    }
}
