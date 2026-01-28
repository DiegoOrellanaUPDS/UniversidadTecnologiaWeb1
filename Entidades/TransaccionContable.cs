using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class TransaccionContable
    {
        [Key]
        public int Id { get; set; }
        public string? Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        
        public string? Tipo { get; set; }
        
        public string Categoria { get; set; } = "General";
        public string Estado { get; set; } = "Pendiente";
        
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaModificacion { get; set; }
    }
}