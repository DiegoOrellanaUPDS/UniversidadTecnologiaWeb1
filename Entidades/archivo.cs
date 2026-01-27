using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class ConsistenciaArchivo
    {
        [Key]
        public int Id_Archivo {get;set;}
        public string Nombre {get;set;}
        public string Tipo {get;set;}
        public DateOnly Fecha_Creacion {get;set;}
        public string Estado {get;set;}
        
    }
}