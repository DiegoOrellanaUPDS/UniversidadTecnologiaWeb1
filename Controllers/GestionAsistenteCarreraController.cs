using Microsoft.AspNetCore.Mvc;
using UniversidadTecnologiaWeb1.Models;

namespace UniversidadTecnologiaWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GestionAsistenteCarreraController : ControllerBase
    {
        [HttpPost]
        public IActionResult CrearGestion([FromBody] GestionAsistenteCarreraDto gestion)
        {
            if (gestion == null)
            {
                return BadRequest(new
                {
                    mensaje = "La gestión no puede ser nula"
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Created("", new
            {
                mensaje = "Gestión académica registrada correctamente",
                gestion
            });
        }
    }
}

