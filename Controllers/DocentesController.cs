namespace Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Universidad.Data;
    using Entidades;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    [ApiController]
    [Route("api/[controller]")]
    public class DocentesController : ControllerBase
    {
        private readonly AppDbContext context;

        public DocentesController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/docentes
        [HttpGet]
        public async Task<IActionResult> GetDocentes()
        {
            return Ok(await context.Docentes.ToListAsync());
        }

        // GET: api/docentes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocente(int id)
        {
            var docente = await context.Docentes.FindAsync(id);

            if (docente == null)
            {
                return NotFound();
            }

            return Ok(docente);
        }

        // POST: api/docentes/reporte-pagos
        [HttpPost("reporte-pagos")]
        public async Task<IActionResult> PostReportePagos([FromBody] Docente docente)
        {
            if (docente == null)
            {
                return BadRequest("Los datos del docente son requeridos");
            }

            context.Docentes.Add(docente);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocente), new { id = docente.Id }, docente);
        }

        // POST: api/docentes (crear docente)
        [HttpPost]
        public async Task<IActionResult> PostDocente([FromBody] Docente docente)
        {
            context.Docentes.Add(docente);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocente), new { id = docente.Id }, docente);
        }
    }
}


