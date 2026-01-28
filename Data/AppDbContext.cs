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

        // Tablas del sistema de Podcasts
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Tema> Temas { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Episodio> Episodios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para Categoria
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descripcion).HasMaxLength(500);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.Nombre).IsUnique();
            });

            // Configuración para Tema
            modelBuilder.Entity<Tema>(entity =>
            {
                entity.ToTable("temas");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Descripcion).HasMaxLength(1000);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                // Relación con Categoria
                entity.HasOne(t => t.Categoria)
                      .WithMany()
                      .HasForeignKey(t => t.CategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuración para Podcast
            modelBuilder.Entity<Podcast>(entity =>
            {
                entity.ToTable("podcasts");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).HasMaxLength(1000);
                entity.Property(e => e.Autor).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Frecuencia).HasMaxLength(50);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                // Relación con Tema
                entity.HasOne(p => p.Tema)
                      .WithMany()
                      .HasForeignKey(p => p.TemaId)
                      .OnDelete(DeleteBehavior.Restrict);
                
                // Relación con Episodios
                entity.HasMany(p => p.Episodios)
                      .WithOne(e => e.Podcast)
                      .HasForeignKey(e => e.PodcastId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración para Episodio
            modelBuilder.Entity<Episodio>(entity =>
            {
                entity.ToTable("episodios");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Descripcion).HasMaxLength(2000);
                entity.Property(e => e.FechaCreacion).HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                // Relación con Podcast
                entity.HasOne(e => e.Podcast)
                      .WithMany(p => p.Episodios)
                      .HasForeignKey(e => e.PodcastId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                // Índices para búsquedas
                entity.HasIndex(e => e.PodcastId);
                entity.HasIndex(e => e.FechaPublicacion);
                entity.HasIndex(e => e.NumeroEpisodio);
            });
        }
    }
}
