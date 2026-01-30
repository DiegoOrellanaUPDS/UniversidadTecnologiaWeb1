using Microsoft.AspNetCore.Mvc;

namespace UniversidadTecnologiaWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosExternosController : ControllerBase
    {
        [HttpPost]
        public IActionResult CrearServicioExterno([FromBody] ServicioExternoDto servicio)
        {
            if (servicio == null)
            {
                return BadRequest("El objeto servicio no puede ser nulo");
            }

            return Ok(new
            {
                mensaje = "Servicio externo registrado correctamente",
                data = servicio
            });
        }
    }

    public class ServicioExternoDto
    {
        public string NombreProveedor { get; set; }
        public string TipoServicio { get; set; }
        public decimal Costo { get; set; }
    }
}
