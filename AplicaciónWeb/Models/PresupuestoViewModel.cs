namespace AplicaciónWeb.Models
{
    public class PresupuestoViewModel
    {
        public int IdPresupuesto { get; set; } // ID Autogenerado
        public int IdSiniestro { get; set; } // Agrega esta propiedad
        public DateTime FechaEmision { get; set; } = DateTime.Now;
        public decimal MontoTotal { get; set; }
        public string Detalles { get; set; }
        public decimal Impuestos { get; set; }
        public decimal CostoSinImpuestos { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Estado inicial
        public decimal Descuento { get; set; } = 0;
        public string Enlace { get; set; } = "presupuesto.com";// Enlace opcional para referencia
    }
}
