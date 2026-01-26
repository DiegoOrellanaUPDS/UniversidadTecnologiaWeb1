using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universidad.Entidades
{
    public class Contabilidad
    {
        [Key]
        public int Id { get; set; }


        public int IdSolicitud { get; set; }
        
        public string SolicitudDepartamento { get; set; }
        public decimal Monto { get; set; }

        public string TipoPago { get; set; } = string.Empty;

        public string EstadoPago { get; set; } = "Pendiente"; 


        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public DateTime FechaPago { get; set; }
        public string Descripcion { get; set; }

        public string MetodoPago { get; set; } = "Efectivo"; 

    }
}