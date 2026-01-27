using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Responsable { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        public bool Estado { get; set; }
    }


    
}