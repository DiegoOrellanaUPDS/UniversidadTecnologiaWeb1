using Microsoft.AspNetCore.Mvc;
using Entidades;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecretariaGeneralController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult CrearSolicitud([FromBody] Solicitud solicitud)
        {
            if (solicitud == null)
            {
                return BadRequest("La solicitud no puede ser nula.");
            }
            return CreatedAtAction(nameof(CrearSolicitud), new { id = solicitud.Id }, solicitud);
        }
    }
}
