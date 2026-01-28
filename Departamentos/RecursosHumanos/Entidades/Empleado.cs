namespace Universidad.Departamentos.RecursosHumanos.Entidades
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public DateTime FechaIngreso { get; set; } = DateTime.UtcNow;
    }
}
