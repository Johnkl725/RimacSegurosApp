using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class DocumentosReclamacion
    {
        public int IdDocumento { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }


        [Required]
        [StringLength(50)]
        public string Extension { get; set; }

        public DateTime FechaSubida { get; set; } = DateTime.Now;

        [ForeignKey("Reclamacion")]
        public int IdReclamacion { get; set; }

        public string Url { get; set; }

        public Reclamacion Reclamacion { get; set; }
    }
}
