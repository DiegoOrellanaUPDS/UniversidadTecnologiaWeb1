using Microsoft.AspNetCore.Mvc;
using ProyectoAudiovisual.Models;
using Universidad.Data;
using Universidad.Entidades;

namespace Universidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduccionAudiovisualController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProduccionAudiovisualController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Producciones.ToList());
        }

        [HttpPost]
        public IActionResult Post(ProduccionAudiovisual produccion)
        {
            _context.Producciones.Add(produccion);
            _context.SaveChanges();
            return Ok(produccion);
        }
    }
}
