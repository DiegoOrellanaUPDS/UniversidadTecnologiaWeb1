using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidad.Data;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReclutadorController : ControllerBase
    {
        private readonly AppDbContext context;

        public ReclutadorController(AppDbContext context)
        {
            this.context=context;
        }
        [HttpGet("listar-reclutadores")]
        public async Task<IActionResult> GetUsuarios()
        {
            return Ok(await context.Reclutadores.ToListAsync());
        }
    }
}