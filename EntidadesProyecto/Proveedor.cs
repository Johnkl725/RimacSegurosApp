
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EntidadesProyecto
{
    public class Proveedor
    {
        public int Id { get; set; } // Asegúrate de que coincida con la base de datos
        public string Nombre { get; set; }
        public string Ruc { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string? Calificacion { get; set; } // Usa nullable si Calificacion puede ser nulo
        public string Descripcion { get; set; }

        // Relación con Talleres, si aplica
        // Relación opcional con Talleres
        //public virtual ICollection<Taller> Talleres { get; set; } //= new List<Taller>();
    }
}
