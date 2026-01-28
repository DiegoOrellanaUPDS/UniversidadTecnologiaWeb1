using System.ComponentModel.DataAnnotations;

namespace Modelos
{
    public class Materia
    {
        [Key]
        public int Id { get; set; }
        public string NombreMateria { get; set; }
        public decimal Codigo { get; set; }
        public string Estado { get; set; } = "Activo";
    }
}

