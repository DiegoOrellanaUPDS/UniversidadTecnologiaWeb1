using Microsoft.AspNetCore.Mvc;

namespace UniversidadTecnologiaWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TramitesArchivosController : ControllerBase
    {
        [HttpPost]
        public IActionResult CrearTramite([FromBody] TramiteArchivoDto tramite)
        {
            if (tramite == null)
            {
                return BadRequest("El trámite no puede ser nulo");
            }

            return Created("", tramite);
        }
    }

    public class TramiteArchivoDto
    {
        public string TipoTramite { get; set; }
        public string Descripcion { get; set; }
        public string Estudiante { get; set; }
    }
}
