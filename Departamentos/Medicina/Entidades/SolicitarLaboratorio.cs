using System;
using System.ComponentModel.DataAnnotations;

namespace Departamentos.Medicina.Entidades
{
    public class SolicitudLaboratorio
    {
        [Key]
        public int Id { get; set; }

        // Docente
        [Required]
        public string CiDocente { get; set; }

        [Required]
        public string NombreDocente { get; set; }

        // Materia
        [Required]
        public string Materia { get; set; }

        // Detalle de la solicitud
        [Required]
        public string Laboratorio { get; set; }

        public DateTime FechaSolicitud { get; set; }

        public string Estado { get; set; }
    }
}
