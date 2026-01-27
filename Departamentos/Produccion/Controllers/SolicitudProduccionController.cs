using Microsoft.AspNetCore.Mvc;
using Universidad.Data;
using Universidad.Departamentos.Produccion.Entidades;
namespace Universidad.Departamentos.Produccion.Controllers
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
