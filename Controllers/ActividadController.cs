using Entidades;
using Universidad.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FacultadIngenieria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActividadController : ControllerBase
    {
        private AppDbContext context;
        public ActividadController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetActividades()
        {
            return Ok(await context.Actividades.Where(act => act.Estado != false).ToListAsync());
        }

        [HttpGet("{cod}")]
        public async Task<IActionResult> GetActividadCod(string cod)
        {
            var actividad = await (from act in context.Actividades
                                   where act.Codigo == cod && act.Estado != false
                                   select act).FirstOrDefaultAsync();
            if (actividad == null)
                return NotFound();
            return Ok(actividad);
        }

        [HttpPost]
        public async Task<IActionResult> PostCreacionActividad(Actividad actividad)
        {
            var e = await (from act in context.Actividades
                           where act.Codigo == actividad.Codigo && act.Estado != false
                           select act).FirstOrDefaultAsync();
            if (e != null)
                return BadRequest("La actividad ya se encuentra creada en la base de datos");
        
            await context.Actividades.AddAsync(actividad);
            await context.SaveChangesAsync();
            return 
            Ok(actividad);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutActividad(string codigo, [FromBody] Actividad actividad)
        {
            var existing = await (from act in context.Actividades
                                   where act.Codigo == codigo && act.Estado != false
                                   select act).FirstOrDefaultAsync();

            var pe = await context.Actividades.FirstOrDefaultAsync(e => e.Codigo == codigo);
            if (pe == null)
                return NotFound();
            pe.Nombre = actividad.Nombre;
            pe.Codigo = actividad.Codigo;
            pe.Descripcion = actividad.Descripcion;
            pe.Lugar = actividad.Lugar;
            pe.Encargado = actividad.Encargado;
            pe.HoraInicio = actividad.HoraInicio;
            pe.HoraFin = actividad.HoraFin;
            pe.ActividadFecha = actividad.ActividadFecha;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{cod}")]
        public async Task<IActionResult> DelActividad(string cod)
        {
            var actividad = await (from act in context.Actividades
                                   where act.Codigo == cod && act.Estado != false
                                   select act).FirstOrDefaultAsync();
            if (actividad == null)
                return NotFound();
        
            actividad.Estado = false;
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}