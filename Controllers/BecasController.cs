using Microsoft.AspNetCore.Mvc;

namespace Universidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BecasController : ControllerBase
    {
        /// <summary>
        /// Endpoint POST para registrar una nueva beca
        /// Autor: Brian Marquez - Departamento de Becas
        /// </summary>
        [HttpPost]
        public IActionResult RegistrarBeca([FromBody] BecaRequest beca)
        {
            // Validar que el modelo no sea nulo
            if (beca == null)
            {
                return BadRequest(new 
                { 
                    mensaje = "Los datos de la beca no pueden estar vacíos",
                    codigo = 400
                });
            }

            // Validar campos requeridos
            if (string.IsNullOrWhiteSpace(beca.Nombre))
            {
                return BadRequest(new 
                { 
                    mensaje = "El nombre de la beca es requerido",
                    codigo = 400
                });
            }

            if (beca.Monto <= 0)
            {
                return BadRequest(new 
                { 
                    mensaje = "El monto debe ser mayor a cero",
                    codigo = 400
                });
            }

            // Simular registro exitoso
            var becaRegistrada = new
            {
                id = new Random().Next(1000, 9999),
                nombre = beca.Nombre,
                descripcion = beca.Descripcion,
                monto = beca.Monto,
                fechaInicio = beca.FechaInicio,
                fechaFin = beca.FechaFin,
                activa = true,
                mensaje = "Beca registrada exitosamente por Brian Marquez"
            };

            // Retornar 201 Created
            return StatusCode(201, becaRegistrada);
        }
    }

    /// <summary>
    /// Modelo para recibir datos de beca
    /// </summary>
    public class BecaRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}