using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Universidad.Data;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsistenciaArchivoController : ControllerBase
    {
        private readonly AppDbContext context;

        public ConsistenciaArchivoController(AppDbContext context)
        {
            this.context = context;
        }

        /// GET: api/Archivos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsistenciaArchivo>>> GetArchivos()
        {
            return await context.ConsistenciArchivo.ToListAsync();
        }


        // POST: api/Archivos
        [HttpPost]
        public async Task<ActionResult<ConsistenciaArchivo>> PostArchivos(ConsistenciaArchivo archivo)
        {
            context.ConsistenciArchivo.Add(archivo);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetArchivos", new { id = archivo.Id_Archivo }, archivo);
        }

    }
}