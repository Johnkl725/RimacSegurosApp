using Microsoft.EntityFrameworkCore;
using EntidadesProyecto;

namespace MiAplicacion.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options) { }

        // Propiedades DbSet para las tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<TipoUsuarioDto> TipoUsuarios { get; set; }
        public DbSet<Siniestro> Siniestros { get; set; }
        public DbSet<Departamento> Departamento { get; set; } // Cambio a plural para consistencia
        public DbSet<Provincia> Provincia { get; set; } // Cambio a plural
        public DbSet<Distrito> Distrito { get; set; } // Cambio a plural

        // Configuración del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para la entidad Siniestro
            modelBuilder.Entity<Siniestro>(entity =>
            {
                entity.HasKey(s => s.IdSiniestro);

                // Configuración de columnas opcionales y tipos de datos
                entity.Property(s => s.Tipo).HasMaxLength(20);
                entity.Property(s => s.Ubicacion).HasMaxLength(30);
                entity.Property(s => s.Descripcion); // Configuración básica de Descripción

                // Configuración de relaciones con llaves foráneas
                entity.HasOne<Departamento>()
                      .WithMany()
                      .HasForeignKey(s => s.IdDepartamento)
                      .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Provincia>()
      .HasOne(p => p.Departamento)
      .WithMany()
      .HasForeignKey(p => p.id_departamento)
      .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Distrito>()
                    .HasOne(d => d.Provincia)
                    .WithMany()
                    .HasForeignKey(d => d.id_provincia)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Usuario con herencia TPH
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombres).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Apellido1).HasMaxLength(50);
                entity.Property(e => e.Apellido2).HasMaxLength(50);
                entity.Property(e => e.Dni).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Telefono).HasMaxLength(7);
                entity.Property(e => e.LiderSogId).HasMaxLength(15);

                // Discriminador para diferenciar los tipos
                entity.HasDiscriminator<string>("TipoUsuario")
                    .HasValue<Usuario>("Usuario")
                    .HasValue<Personal>("Personal")
                    .HasValue<Administrador>("Administrador")
                    .HasValue<Beneficiario>("Beneficiario");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
