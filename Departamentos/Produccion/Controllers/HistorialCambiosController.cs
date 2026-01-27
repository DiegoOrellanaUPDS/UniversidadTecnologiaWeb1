using Microsoft.AspNetCore.Mvc;
using Universidad.Data;
using Universidad.Departamentos.Produccion.Entidades;

namespace Universidad.Departamentos.Produccion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialCambiosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HistorialCambiosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Historiales.ToList());
        }

        [HttpPost]
        public IActionResult Post(HistorialCambios historial)
        {
            _context.Historiales.Add(historial);
            _context.SaveChanges();
            return Ok(historial);
        }
    }
}
