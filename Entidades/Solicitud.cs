using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Solicitud
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string CodigoSolicitud { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; } // información, trámite, beca

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int EstudianteId { get; set; }

        [ForeignKey("EstudianteId")]
        public Estudiante Estudiante { get; set; }
    }
}
