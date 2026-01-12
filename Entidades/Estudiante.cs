using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Ci { get; set; }
    }
}