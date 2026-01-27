namespace ProyectoAudiovisual.Produccion.Models
{
    public class Persona
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string CI { get; set; }
        public string correo { get; set; }
        public string estado { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string VIT { get; set; }
        public string tipo { get; set; }
        public string rol { get; set; }
    }
}
