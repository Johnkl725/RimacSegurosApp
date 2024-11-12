namespace AplicaciónWeb.Models
{
    public class SiniestroViewModel
    {
        public int? IdDepartamento { get; set; }
        public int? IdProvincia { get; set; }
        public int? IdDistrito { get; set; }
        public int? IdDocumento { get; set; }
        public int? IdPoliza { get; set; }
        public int? IdTaller { get; set; }
        public int? IdPresupuesto { get; set; }

        public string Tipo { get; set; }
        public DateTime? FechaSiniestro { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }
    }

}
