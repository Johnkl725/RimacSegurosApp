using EntidadesProyecto;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntidadesProyecto
{
    public class Siniestro
    {
        [Column("id_siniestro")]
        public int IdSiniestro { get; set; } // Clave primaria

        public int? IdDepartamento { get; set; }
        public int? IdProvincia { get; set; }
        public int? IdDistrito { get; set; }
        public int? IdDocumento { get; set; }
        public int? IdPoliza { get; set; } // Clave foránea hacia Poliza
        [Column("id_taller")]
        public int? IdTaller { get; set; }
        public int? IdPresupuesto { get; set; } // Clave foránea hacia Presupuesto

        [Column("tipo")]
        public string Tipo { get; set; }
        [Column("fecha_siniestro")]
        public DateTime? FechaSiniestro { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        [Column("ubicacion")]
        public string Ubicacion { get; set; }
        [Column("descripcion")]
        public string Descripcion { get; set; }

        // Propiedades de navegación
        public Presupuesto? Presupuesto { get; set; }
        public Poliza Poliza { get; set; }

        //[NotMapped]  Esto asegura que la propiedad no se considere en consultas SQL directas
        public Taller? Taller { get; set; } // Propiedad de navegación
        


        public ICollection<Reclamacion> Reclamaciones { get; set; } = new List<Reclamacion>();

    }



    public class Departamento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class Provincia
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        // Relación con Departamento
        public int id_departamento { get; set; } // Cambiado a id_departamento para coincidir con la base de datos
        public Departamento Departamento { get; set; }
    }

    public class Distrito
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        // Relación con Provincia
        public int id_provincia { get; set; } // Cambiado a id_provincia para coincidir con la base de datos
        public Provincia Provincia { get; set; }
    }
}
public class documentos
{
    public int Id { get; set; }
    public int IdSiniestro { get; set; } // Clave foránea
    public string Nombre { get; set; }
    public string Extension { get; set; }
    public string Url { get; set; }
    public DateTime FechaCreacion { get; set; }

    // Propiedad de navegación
    public Siniestro Siniestro { get; set; }
}
