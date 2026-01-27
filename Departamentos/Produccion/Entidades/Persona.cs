using System.ComponentModel.DataAnnotations;

namespace Universidad.Departamentos.Produccion.Entidades
{
    public class Persona
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string CI { get; set; }
        public string correo { get; set; }
        public string estado { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string VIT { get; set; }
        public string tipo { get; set; }
        public string rol { get; set; }
    }
}
