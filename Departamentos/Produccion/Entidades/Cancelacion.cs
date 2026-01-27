using System.ComponentModel.DataAnnotations;

namespace Universidad.Departamentos.Produccion.Entidades
{
    public class Cancelacion
    {
        [Key]
        public int id { get; set; }
        public string motivo { get; set; }
        public DateTime fechacancelacion { get; set; }
        public string autorizadopor { get; set; }
    }
}
