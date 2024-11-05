using System;

namespace EntidadesProyecto
{
    public class Siniestro
    {
        public int IdSiniestro { get; set; } // Clave primaria

        public int? IdDepartamento { get; set; }
        public int? IdProvincia { get; set; }
        public int? IdDistrito { get; set; }
        public int? IdDocumento { get; set; }
        public int? IdPoliza { get; set; }
        public int? IdTaller { get; set; }
        public int? IdPresupuesto { get; set; }


        public string Tipo { get; set; }
        public DateTime? FechaSiniestro { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string Ubicacion { get; set; }
        public string Descripcion { get; set; }


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
        public int id_departamento { get; set; }
        public Departamento Departamento { get; set; }
    }

    public class Distrito
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        // Relación con Provincia
        public int id_provincia { get; set; }
        public Provincia Provincia { get; set; }
    }
}
