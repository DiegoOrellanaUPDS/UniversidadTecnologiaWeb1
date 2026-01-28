using Entidades;
using Microsoft.AspNetCore.Mvc;
using Universidad.Data;

namespace Controllers
{
    [ApiController]
    [Route("api")]
    public class TalentoHumanoController : ControllerBase
    {
        private readonly AppDbContext context;
        public TalentoHumanoController(AppDbContext context)
        {
            this.context=context;
        }
        [HttpPost]
        public async Task<IActionResult> PostReclutador(Reclutador reclutador)
        {
            if (string.IsNullOrWhiteSpace(reclutador.CodigoPersona) || string.IsNullOrEmpty(reclutador.CodigoPersona))
            {
                return BadRequest("Llene todos los campos");
            }
            context.Reclutadores.Add(reclutador);
            await context.SaveChangesAsync();
            return Ok(201);
        }
    }
}