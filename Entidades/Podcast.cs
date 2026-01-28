using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universidad.Entidades
{
    [Table("podcasts")]
    public class Podcast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El autor/anfitrión es obligatorio")]
        [StringLength(100, ErrorMessage = "El autor no puede exceder 100 caracteres")]
        [Column("autor")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tema es obligatorio")]
        [Column("tema_id")]
        public int TemaId { get; set; }

        [Url(ErrorMessage = "Debe ser una URL válida")]
        [Column("url_imagen")]
        public string? UrlImagen { get; set; }

        [Url(ErrorMessage = "Debe ser una URL válida")]
        [Column("url_rss")]
        public string? UrlRss { get; set; }

        [Column("duracion_promedio")]
        public int DuracionPromedio { get; set; } // en minutos

        [Column("frecuencia")]
        [StringLength(50)]
        public string? Frecuencia { get; set; } // "semanal", "quincenal", "mensual"

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [ForeignKey("TemaId")]
        public virtual Tema? Tema { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Column("fecha_actualizacion")]
        public DateTime? FechaActualizacion { get; set; }

        // Relación con episodios
        public virtual ICollection<Episodio>? Episodios { get; set; }
    }
}