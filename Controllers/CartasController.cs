using Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartasController : ControllerBase
    {
        private readonly AppDbContext context;
        public CartasController (AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCartas()
        {
            var cartas = await (from c in context.Cartas select c).ToListAsync();
            return Ok(cartas);
        }

        [HttpPost]
        public async Task<IActionResult> PostCarta(Carta carta)
        {
            var existing = await (from c in context.Cartas where c.Id == carta.Id select c).FirstOrDefaultAsync();
            if (existing != null) return BadRequest("ya existe pe");

            await context.Cartas.AddAsync(carta);
            await context.SaveChangesAsync();

            return Ok($"carta subida correctamente {carta}");
        }
    }
}