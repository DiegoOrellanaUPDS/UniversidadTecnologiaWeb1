using System;
using System.ComponentModel.DataAnnotations;

namespace UniversidadTecnologiaWeb1.Models
{
    public class GestionAsistenteCarreraDto
    {
        [Required(ErrorMessage = "El tipo de gestión es obligatorio")]
        public string TipoGestion { get; set; }

        [Required(ErrorMessage = "La carrera es obligatoria")]
        public string Carrera { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El responsable es obligatorio")]
        public string Responsable { get; set; }

        public DateTime FechaGestion { get; set; } = DateTime.Now;

        public string Estado { get; set; } = "Pendiente";
    }
}


