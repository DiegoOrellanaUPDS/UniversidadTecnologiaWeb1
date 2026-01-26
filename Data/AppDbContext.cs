namespace Data
{
    using Microsoft.EntityFrameworkCore;
    using Entidades;
    using Departamentos.Medicina.Entidades;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<SolicitudLaboratorio> SolicitudesLaboratorio { get; set; }
    }
}