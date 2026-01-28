using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Actividad
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public string Encargado { get; set; }
        public DateOnly HoraInicio { get; set; }
        public DateOnly HoraFin { get; set; }
        public DateOnly ActividadFecha { get; set; }
        public bool Estado { get; set; } = true;
    }
}