using Microsoft.AspNetCore.Mvc;

namespace UniversidadTecnologiaWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosExternosController : ControllerBase
    {
        public class ServicioExternoDto
        {
            public string? Nombre { get; set; }
            public string? Proveedor { get; set; }
            public string? Descripcion { get; set; }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ServicioExternoDto? dto)
        {
            if (dto == null)
                return BadRequest("El cuerpo JSON no puede ser nulo");

            return Created("", new
            {
                mensaje = "Servicio externo recibido correctamente",
                data = dto
            });
        }
    }
}
