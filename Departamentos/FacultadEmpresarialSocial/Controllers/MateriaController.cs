using Universidad.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proyecto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriaController : ControllerBase
    {
        private AppDbContext context;
        public  MateriaController (AppDbContext context)
        {
            this.context = context;
        }
        private static List<Materia> materias = new List<Materia>();



        [HttpPost]
        public async Task<IActionResult> CreateMateria(Materia materia)
        {
            var m = await (from mat in context.Materias
                           where mat.Codigo == mat.Codigo && mat.Estado != "Borrado"
                           select mat).FirstOrDefaultAsync();
            if (m != null)
            {
                return BadRequest("La materia ya existe.");
            }
            await context.Materias.AddAsync(materia);
            await context.SaveChangesAsync();
            return Ok(materia);
        }
    }
}