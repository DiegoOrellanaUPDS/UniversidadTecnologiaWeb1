using Microsoft.AspNetCore.Mvc;
using ProyectoAudiovisual.Produccion.Models;
using Universidad.Data;
using Universidad.Entidades;

namespace Universidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudProduccionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SolicitudProduccionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Solicitudes.ToList());
        }

        [HttpPost]
        public IActionResult Post(SolicitudProduccion solicitud)
        {
            _context.Solicitudes.Add(solicitud);
            _context.SaveChanges();
            return Ok(solicitud);
        }
    }
}
