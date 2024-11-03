using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    public DbSet<Administrador> Administradores { get; set; }
    public DbSet<Beneficiario> Beneficiarios { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Personal> personal { get; set; }
}

