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
        public DbSet<Departamento> Departamento { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }


        // Configuración del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Vehiculo
            modelBuilder.Entity<Vehiculo>().ToTable("Vehiculo");

            modelBuilder.Entity<Taller>(entity =>
            {
                entity.ToTable("Taller"); // Nombre de la tabla en la base de datos
                entity.HasKey(t => t.Id); // Clave primaria

                // Propiedades de Taller
                entity.Property(t => t.IdProveedor).HasColumnName("id_proveedor");
                entity.Property(t => t.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Direccion).HasMaxLength(200);
                entity.Property(t => t.Telefono).HasMaxLength(15);
                entity.Property(t => t.Correo).HasMaxLength(100);
                entity.Property(t => t.Ciudad).HasMaxLength(50);
                entity.Property(t => t.Tipo).HasMaxLength(50);
                entity.Property(t => t.Capacidad);
                entity.Property(t => t.Descripcion).HasMaxLength(500);
                entity.Property(t => t.Calificacion);
                entity.Property(t => t.Estado).HasMaxLength(20);

                entity.HasMany(t => t.Siniestros)
              .WithOne() // No se requiere una propiedad de navegación en Siniestro
              .HasForeignKey(s => s.IdTaller)
              .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de Poliza
            modelBuilder.Entity<Poliza>(entity =>
            {
                entity.ToTable("Poliza");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.IdBeneficiario).HasColumnName("id_beneficiario");
                entity.Property(p => p.IdTipo).HasColumnName("id_tipo");
                entity.Property(p => p.FechaInicio).HasColumnName("fecha_inicio").HasColumnType("DATE");
                entity.Property(p => p.FechaFin).HasColumnName("fecha_fin").HasColumnType("DATE");
                entity.Property(p => p.Estado).HasColumnName("estado").HasMaxLength(50);
                entity.HasOne(p => p.Beneficiario)
                      .WithMany()
                      .HasForeignKey(p => p.IdBeneficiario)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(p => p.Tipo)
                      .WithMany()
                      .HasForeignKey(p => p.IdTipo)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración de TipoPoliza
            modelBuilder.Entity<TipoPoliza>(entity =>
            {
                entity.ToTable("TipoPoliza");
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Descripcion).HasMaxLength(100);
                entity.Property(t => t.MontoMensual).HasColumnName("monto_mensual").HasColumnType("DECIMAL(15,2)");
            });

            // Configuración de Presupuesto
            modelBuilder.Entity<Presupuesto>(entity =>
            {
                entity.ToTable("Presupuesto");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FechaEmision).HasColumnName("fecha_emision").HasColumnType("DATE");
                entity.Property(p => p.MontoTotal).HasColumnName("monto_total").HasColumnType("DECIMAL(15,2)");
                entity.Property(p => p.Detalles).HasColumnName("detalles").HasMaxLength(500);
                entity.Property(p => p.Impuestos).HasColumnName("impuestos").HasColumnType("DECIMAL(15,2)");
                entity.Property(p => p.CostoSinImpuestos).HasColumnName("costo_sin_impuestos").HasColumnType("DECIMAL(15,2)");
                entity.Property(p => p.Estado).HasColumnName("estado").HasMaxLength(50);
                entity.Property(p => p.Descuento).HasColumnName("descuento").HasColumnType("DECIMAL(15,2)");
                entity.Property(p => p.Enlace).HasColumnName("enlace").HasMaxLength(100);
            });

            // Configuración de Siniestro
            modelBuilder.Entity<Siniestro>(entity =>
            {
                entity.ToTable("Siniestro"); // Nombre de la tabla en la base de datos

                entity.HasKey(s => s.IdSiniestro);

                // Mapeo de propiedades de Siniestro a nombres de columnas en la base de datos
                entity.Property(s => s.IdSiniestro).HasColumnName("id_siniestro"); // Clave primaria
                entity.Property(s => s.IdDepartamento).HasColumnName("id_departamento");
                entity.Property(s => s.IdProvincia).HasColumnName("id_provincia");
                entity.Property(s => s.IdDistrito).HasColumnName("id_distrito");
                entity.Property(s => s.IdDocumento).HasColumnName("id_documento");
                entity.Property(s => s.IdPoliza).HasColumnName("id_poliza");
                entity.Property(s => s.IdTaller).HasColumnName("id_taller");
                entity.Property(s => s.IdPresupuesto).HasColumnName("id_presupuesto");
                entity.Property(s => s.Tipo).HasColumnName("tipo").HasMaxLength(20);
                entity.Property(s => s.FechaSiniestro).HasColumnName("fecha_siniestro");
                entity.Property(s => s.FechaCreacion).HasColumnName("fecha_creacion");
                entity.Property(s => s.FechaActualizacion).HasColumnName("fecha_actualizacion");
                entity.Property(s => s.Ubicacion).HasColumnName("ubicacion").HasMaxLength(30);
                entity.Property(s => s.Descripcion).HasColumnName("descripcion");




                // Configuración de relaciones
                entity.HasOne(s => s.Presupuesto)
                      .WithOne()
                      .HasForeignKey<Siniestro>(s => s.IdPresupuesto)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(s => s.Poliza)
                      .WithMany()
                      .HasForeignKey(s => s.IdPoliza)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Departamento>()
                      .WithMany()
                      .HasForeignKey(s => s.IdDepartamento)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Provincia>()
                      .WithMany()
                      .HasForeignKey(s => s.IdProvincia)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Distrito>()
                      .WithMany()
                      .HasForeignKey(s => s.IdDistrito)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.ToTable("Provincia");

                // Clave primaria
                entity.HasKey(p => p.Id);

                // Nombre de columna específico para id_departamento
                entity.Property(p => p.id_departamento).HasColumnName("id_departamento");

                // Configura la relación con Departamento usando id_departamento
                entity.HasOne(p => p.Departamento)
                      .WithMany()
                      .HasForeignKey(p => p.id_departamento)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Distrito>(entity =>
            {
                entity.ToTable("Distrito");

                // Clave primaria
                entity.HasKey(d => d.Id);

                // Nombre de columna específico para id_provincia
                entity.Property(d => d.id_provincia).HasColumnName("id_provincia");

                // Configura la relación con Provincia usando id_provincia
                entity.HasOne(d => d.Provincia)
                      .WithMany()
                      .HasForeignKey(d => d.id_provincia)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombres).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Apellido1).HasMaxLength(50);
                entity.Property(e => e.Apellido2).HasMaxLength(50);
                entity.Property(e => e.Dni).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Telefono).HasMaxLength(7);
            });

            // Configuración de Beneficiario
            modelBuilder.Entity<Beneficiario>(entity =>
            {
                entity.ToTable("Beneficiario");
                entity.Property(b => b.IdUsuario).HasColumnName("id_usuario");
                entity.Property(b => b.IdVehiculo).HasColumnName("id_vehiculo");
                entity.Property(b => b.Contraseña).HasColumnName("contraseña");
            });

            // Configuración de Personal
            modelBuilder.Entity<Personal>(entity =>
            {
                entity.ToTable("Personal");
                entity.Property(p => p.IdUsuario).HasColumnName("id_usuario");
                entity.Property(p => p.Contraseña).HasColumnName("contraseña");
            });

            // Configuración de Administrador
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.ToTable("Administrador");
                entity.Property(a => a.IdUsuario).HasColumnName("id_usuario");
                entity.Property(a => a.Contraseña).HasColumnName("contraseña");
            });
            // Configuración para Reclamacion
            modelBuilder.Entity<Reclamacion>(entity =>
            {
                entity.ToTable("Reclamacion");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Estado).HasMaxLength(10).IsRequired().HasDefaultValue("Pendiente");
                entity.Property(r => r.Tipo).HasMaxLength(20).IsRequired();
                entity.Property(r => r.Descripcion).IsRequired();

                entity.HasOne<Siniestro>()
                      .WithMany()
                      .HasForeignKey(r => r.IdSiniestro)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para DocumentosReclamacion
            modelBuilder.Entity<DocumentosReclamacion>(entity =>
            {
                entity.ToTable("DocumentosReclamacion");
                entity.HasKey(d => d.IdDocumento);

                entity.Property(d => d.Nombre).HasMaxLength(100).IsRequired();
                entity.Property(d => d.Extension).HasMaxLength(50).IsRequired();
                entity.Property(d => d.FechaSubida).HasDefaultValueSql("GETDATE()");

                entity.HasOne<Reclamacion>()
                      .WithMany(r => r.Documentos)
                      .HasForeignKey(d => d.IdReclamacion)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
