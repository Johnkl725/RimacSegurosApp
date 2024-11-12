using System;
using System.Collections.Generic;

namespace EntidadesProyecto
{
    public partial class Proveedor
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public string? Ruc { get; set; }

        public string? Telefono { get; set; }

        public string? Correo { get; set; }

        public string? Calificacion { get; set; }

        public string? Descripcion { get; set; }

        public virtual ICollection<Taller> Tallers { get; set; } = new List<Taller>();
    }
}