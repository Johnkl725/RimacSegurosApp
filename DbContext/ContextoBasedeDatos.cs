using Microsoft.EntityFrameworkCore;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbContext
{
    public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        // Propiedades DbSet para las tablas
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Personal> Personales { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        // Aquí puedes agregar más DbSet según tus tablas

        // Configuración del modelo (opcional)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configura las relaciones, claves primarias y demás configuraciones aquí si es necesario
            base.OnModelCreating(modelBuilder);
        }
    }
}
