using Microsoft.AspNetCore.Mvc;
using ProyectoAudiovisual.Produccion.Models;
using Universidad.Data;
using Universidad.Entidades;

namespace Universidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CancelacionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CancelacionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Cancelaciones.ToList());
        }

        [HttpPost]
        public IActionResult Post(Cancelacion cancelacion)
        {
            _context.Cancelaciones.Add(cancelacion);
            _context.SaveChanges();
            return Ok(cancelacion);
        }
    }
}
