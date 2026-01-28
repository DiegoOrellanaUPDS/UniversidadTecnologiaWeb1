using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universidad.Entidades
{
    [Table("Talleres")]
    public class Taller
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "La descripción no puede exceder 500 caracteres")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La capacidad es obligatoria")]
        [Range(1, 1000, ErrorMessage = "La capacidad debe estar entre 1 y 1000")]
        public int CapacidadMaxima { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "El ID del profesor es obligatorio")]
        public int ProfesorId { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
