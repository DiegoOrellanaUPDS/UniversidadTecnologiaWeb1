using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Reclutador
    {
        [Key]
        public int Id { get; set; }

        public string CodigoReclutador { get; set; }

        public string Telefono { get; set; }

 
        public string CodigoPersona { get; set; }

        
    }
}