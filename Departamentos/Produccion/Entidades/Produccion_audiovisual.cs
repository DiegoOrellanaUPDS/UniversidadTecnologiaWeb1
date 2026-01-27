using System.ComponentModel.DataAnnotations;

namespace Universidad.Departamentos.Produccion.Entidades
{
    public class ProduccionAudiovisual
    {
        [Key]
        public int id { get; set; }
        public string estadoactual { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechapublicacion { get; set; }
        public string responsable { get; set; }
        public string versiondevideo { get; set; }
        public string descripcion { get; set; }
    }
}
