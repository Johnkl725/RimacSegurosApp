using System.ComponentModel.DataAnnotations;

namespace AplicaciónWeb.Models
{
    namespace EntidadesProyecto
    {
        public class GestionarPresupuestoViewModel
        {
            public string NumeroSiniestro { get; set; }
            public string TipoSiniestro { get; set; }
            public string PlacaVehiculo { get; set; }
            public int IdPresupuesto { get; set; }

            [Required]
            public decimal CostoReparacion { get; set; }

            [Required]
            public decimal CostoPiezas { get; set; }

            [Required]
            public decimal CostoManoObra { get; set; }

            public decimal PresupuestoTotal => CostoReparacion + CostoPiezas + CostoManoObra;
        }
    }

}
