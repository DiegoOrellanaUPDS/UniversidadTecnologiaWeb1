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

        // Solo el DbSet de Taller
        public DbSet<Taller> Talleres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Taller>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500);
                entity.Property(e => e.FechaCreacion)
                    .HasDefaultValueSql("GETUTCDATE()");
            });
        }
    }
}
