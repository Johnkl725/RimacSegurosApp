namespace AplicaciónWeb.Models
{
    public class DetalleReporteViewModel
    {
        public string NumeroSiniestro { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }
        public string TallerAsignado { get; set; }
        public decimal CostoMantenimiento { get; set; }
        public string TipoSiniestro { get; set; }
        public string Descripcion { get; set; }
    }
}
