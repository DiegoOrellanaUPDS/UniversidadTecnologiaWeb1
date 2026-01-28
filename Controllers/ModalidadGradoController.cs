using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidad.Entidades;
using Universidad.Data;

namespace Universidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModalidadGradoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModalidadGradoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ModalidadGrado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModalidadGrado>>> Get()
        {
            return await _context.ModalidadesGrado.ToListAsync();
        }

        // GET: api/ModalidadGrado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModalidadGrado>> GetById(int id)
        {
            var modalidad = await _context.ModalidadesGrado.FindAsync(id);

            if (modalidad == null)
                return NotFound();

            return modalidad;
        }

        // POST: api/ModalidadGrado
        [HttpPost]
        public async Task<ActionResult> Post(ModalidadGrado modalidad)
        {
            _context.ModalidadesGrado.Add(modalidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = modalidad.Id }, modalidad);
        }

        // PUT: api/ModalidadGrado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ModalidadGrado modalidad)
        {
            if (id != modalidad.Id)
                return BadRequest();

            _context.Entry(modalidad).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/ModalidadGrado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var modalidad = await _context.ModalidadesGrado.FindAsync(id);

            if (modalidad == null)
                return NotFound();

            _context.ModalidadesGrado.Remove(modalidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
