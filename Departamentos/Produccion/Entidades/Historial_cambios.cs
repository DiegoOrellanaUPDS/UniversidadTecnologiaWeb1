using System.ComponentModel.DataAnnotations;

namespace Universidad.Departamentos.Produccion.Entidades
{
    public class HistorialCambios
    {
        [Key]
        public int id { get; set; }
        public DateTime fechacambio { get; set; }
        public string descripcioncambio { get; set; }
        public string realizadopor { get; set; }
    }
}
