using Microsoft.EntityFrameworkCore;

// Entidades en la raíz
using Entidades;

// Entidades con namespace Universidad
using Universidad.Entidades;

// Medicina
using Departamentos.Medicina.Entidades;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // ===== DbSets que usan los controladores =====

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<Reclutador> Reclutadores { get; set; }

        public DbSet<ModalidadGrado> ModalidadesGrado { get; set; }

        public DbSet<SolicitudLaboratorio> SolicitudesLaboratorio { get; set; }
    }
}
