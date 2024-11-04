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


        // Configuración del modelo
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

        }

    }
}
