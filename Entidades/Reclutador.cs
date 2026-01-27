using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Reclutador
    {
        [Key]
        public int Id{get;set;}
        public string Codigo {get;set;}=string.Empty;
        public string Telefono {get;set;}=string.Empty;
        public int IdPersona {get;set;}
    }

}