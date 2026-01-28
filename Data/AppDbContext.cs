using Entidades;
using Microsoft.EntityFrameworkCore;
using Universidad.Entidades;


namespace Universidad.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<ModalidadGrado> ModalidadesGrado { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Reclutador> Reclutadores {get;set;}
        public DbSet<Actividad> Actividades { get; set; }
    }
}