using Microsoft.AspNetCore.Mvc;

namespace UniversidadTecnologiaWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BibliotecaController : ControllerBase
    {
        public class LibroDto
        {
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public int Anio { get; set; }
        }

        [HttpPost]
        public IActionResult RegistrarLibro([FromBody] LibroDto libro)
        {
            if (libro == null)
            {
                return BadRequest("El libro no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(libro.Titulo) || string.IsNullOrEmpty(libro.Autor))
            {
                return BadRequest("Título y Autor son obligatorios.");
            }

            return Created("", new
            {
                mensaje = "Libro registrado correctamente",
                datos = libro
            });
        }
    }
}
