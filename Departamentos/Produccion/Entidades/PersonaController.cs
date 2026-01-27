using Microsoft.AspNetCore.Mvc;
using ProyectoAudiovisual.Produccion.Models;
using Universidad.Data;
using Universidad.Entidades;

namespace Universidad.Controllers
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

        [HttpPut]
        public IActionResult Put(Persona persona)
        {
            _context.Personas.Update(persona);
            _context.SaveChanges();
            return Ok(persona);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var persona = _context.Personas.Find(id);
            if (persona == null)
                return NotFound();

            _context.Personas.Remove(persona);
            _context.SaveChanges();
            return Ok();
        }
    }
}
