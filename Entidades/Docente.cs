using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Docente
    {
        [Key]
        public int Id { get; set; }
        public int Telefono { get; set; }
        public string Especialidad { get; set; }
        public string TituloProfesional { get; set; }
        public string Correo { get; set; }
        public string Codigo { get; set; }
        public string Estado { get; set; }
    }
}