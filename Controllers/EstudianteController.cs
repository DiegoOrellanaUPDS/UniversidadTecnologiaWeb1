namespace Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Entidades;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Universidad.Data;

    [ApiController]
    [Route("api/[controller]")]
    public class EstudianteController : ControllerBase
    {
        private readonly AppDbContext context;

        public EstudianteController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstudiantes()
        {
            return Ok(await context.Estudiantes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstudiante(string ci)
        {
            var estudiante = await( from e in context.Estudiantes
                             where e.Ci == ci
                             select e).FirstOrDefaultAsync();

            if (estudiante == null)
            {
                return NotFound();
            }

            return Ok(estudiante);
        }

        [HttpPost]
        public async Task<IActionResult> PostEstudiante(Estudiante estudiante)
        {
            context.Estudiantes.Add(estudiante);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEstudiante), new { Ci = estudiante.Ci }, estudiante);
        }
    }
}