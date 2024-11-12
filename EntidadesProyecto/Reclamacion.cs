using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class Reclamacion
    {
        public int Id { get; set; }

        [Required]
        public int IdSiniestro { get; set; }

        [Required]
        public DateTime FechaReclamacion { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; } = "Pendiente";

        [Required]
        [StringLength(20)]
        public string Tipo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public List<DocumentosReclamacion> Documentos { get; set; }
    }
}
