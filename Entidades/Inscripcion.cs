namespace Entidades
{
    public class Inscripcion
    {
        public string CiEstudiante { get; set; } = string.Empty;
        public int MateriaId { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public string Estado { get; set; } = "Registrada";
    }
}
