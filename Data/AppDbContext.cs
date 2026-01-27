using Entidades;
using Microsoft.EntityFrameworkCore;
using ProyectoAudiovisual.Models;
using ProyectoAudiovisual.Produccion.Models;
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
        public DbSet<Persona> Personas { get; set; }
        public DbSet<SolicitudProduccion> Solicitudes { get; set; }
        public DbSet<ProduccionAudiovisual> Producciones { get; set; }
        public DbSet<HistorialCambios> Historiales { get; set; }
        public DbSet<Cancelacion> Cancelaciones { get; set; }
    }
}