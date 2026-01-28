using Microsoft.AspNetCore.Mvc;

namespace UniversidadTecnologiaWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcademicaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] object modelo)
        {
            // Validar que el modelo no sea nulo
            if (modelo == null)
            {
                return BadRequest("El modelo no puede ser nulo."); // Retorna 400
            }

            // Aquí iría la lógica para guardar los datos
            
            return Ok(new { mensaje = "Datos recibidos correctamente por Beymar Vasquez", datos = modelo }); // Retorna 200
        }
    }
}
