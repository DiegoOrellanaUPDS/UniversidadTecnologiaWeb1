using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universidad.Entidades
{
    [Table("episodios")]
    public class Episodio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
        [Column("titulo")]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(2000, ErrorMessage = "La descripción no puede exceder 2000 caracteres")]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El podcast es obligatorio")]
        [Column("podcast_id")]
        public int PodcastId { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        [Range(1, 1000, ErrorMessage = "La duración debe estar entre 1 y 1000 minutos")]
        [Column("duracion")]
        public int Duracion { get; set; } // en minutos

        [Required(ErrorMessage = "La fecha de publicación es obligatoria")]
        [Column("fecha_publicacion")]
        public DateTime FechaPublicacion { get; set; }

        [Url(ErrorMessage = "Debe ser una URL válida")]
        [Column("url_audio")]
        public string? UrlAudio { get; set; }

        [Url(ErrorMessage = "Debe ser una URL válida")]
        [Column("url_imagen")]
        public string? UrlImagen { get; set; }

        [Column("numero_episodio")]
        public int NumeroEpisodio { get; set; }

        [Column("activo")]
        public bool Activo { get; set; } = true;

        [ForeignKey("PodcastId")]
        public virtual Podcast? Podcast { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        [Column("reproducciones")]
        public int Reproducciones { get; set; } = 0;
    }
}