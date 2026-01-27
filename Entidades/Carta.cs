using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Carta
    {
        [Key]
        public int Id { get; set; }
        public DateOnly FechaEmision { get; set; }
        public string CodEmpresa { get; set; }
        public bool Estado { get; set; }
    }
}