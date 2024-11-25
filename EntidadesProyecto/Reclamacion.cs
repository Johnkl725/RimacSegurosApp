using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class Reclamacion
    {
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_siniestro")]
        public int IdSiniestro { get; set; }

        [Required]
        [Column("fecha_reclamacion")]
        public DateTime FechaReclamacion { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; } = "Pendiente";

        [Required]
        [StringLength(20)]
        [Column("tipo")]
        public string Tipo { get; set; }

        [Required]
        [Column("descripcion")]
        public string Descripcion { get; set; }

       

        public List<DocumentosReclamacion> Documentos { get; set; }
        
        
        public Siniestro Siniestro { get; set; } // Propiedad de navegación
    }
}
