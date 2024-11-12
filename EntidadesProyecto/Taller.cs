using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesProyecto
{
    public class Taller
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; } // Relación con Proveedor, si existe
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Ciudad { get; set; }
        public string Tipo { get; set; }
        public int Capacidad { get; set; }
        public string Descripcion { get; set; }
        public int Calificacion { get; set; }
        public string Estado { get; set; }

        // Relación con Siniestro
        public ICollection<Siniestro> Siniestros { get; set; } // Un taller puede tener varios siniestros asignados
    }
}