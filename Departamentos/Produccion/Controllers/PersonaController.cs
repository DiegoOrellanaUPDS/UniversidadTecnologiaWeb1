using Microsoft.AspNetCore.Mvc;
using Universidad.Data;
using Universidad.Departamentos.Produccion.Entidades;

namespace Universidad.Departamentos.Produccion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Personas.ToList());
        }

        [HttpPost]
        public IActionResult Post(Persona persona)
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
            return Ok(persona);
        }
    }
}
