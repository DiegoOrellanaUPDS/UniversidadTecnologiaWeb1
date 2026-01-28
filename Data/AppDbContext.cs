using Departamentos.Medicina.Entidades;
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
        public DbSet<SolicitudLaboratorio> SolicitudesLaboratorio { get;set;}
        public DbSet<Factura> Facturas {get;set;}
	// DataTime (C#) == Date (PostreSQL)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Recorre todas las entidades y propiedades DateTime
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    // Si la propiedad es DateTime o DateTime?
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("date"); // Se guarda como "date" en PostgreSQL
                    }
                }
            }
        }
    }
}
