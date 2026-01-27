using Microsoft.AspNetCore.Mvc;
using Universidad.Data;
using Universidad.Departamentos.Produccion.Entidades;

namespace Universidad.Departamentos.Produccion.Controllers
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
