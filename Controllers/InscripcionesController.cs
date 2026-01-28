namespace Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Entidades;
    using System;

    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionesController : ControllerBase
    {
        // POST: api/inscripciones
        [HttpPost]
        public IActionResult PostInscripcion([FromBody] Inscripcion inscripcion)
        {
            // Validar que el modelo no sea nulo
            if (inscripcion == null)
                return BadRequest(new { mensaje = "El body (JSON) no puede ser nulo." });

            // Validar datos
            if (string.IsNullOrWhiteSpace(inscripcion.CiEstudiante) ||
                inscripcion.MateriaId <= 0 ||
                string.IsNullOrWhiteSpace(inscripcion.Periodo))
            {
                return BadRequest(new { mensaje = "Datos inválidos. Revisa CiEstudiante, MateriaId y Periodo." });
            }

            // Simulación de creación (no BD para no romper)
            var respuesta = new
            {
                idInscripcion = Guid.NewGuid(),
                inscripcion.CiEstudiante,
                inscripcion.MateriaId,
                inscripcion.Periodo,
                estado = inscripcion.Estado,
                fechaRegistro = DateTime.UtcNow
            };

            return Created("api/inscripciones", respuesta);
        }
    }
}
        
