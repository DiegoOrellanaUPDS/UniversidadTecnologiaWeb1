using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidad.Data;
using Departamentos.Medicina.Entidades;

namespace Departamentos.Medicina.Controllers
{
    [ApiController]
    [Route("api/medicina/[controller]")]
    public class SolicitudLaboratorioController : ControllerBase
    {
        private readonly AppDbContext context;

        public SolicitudLaboratorioController(AppDbContext context)
        {
            this.context = context;
        }

        // Ver todas las solicitudes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await context.SolicitudesLaboratorio.ToListAsync());
        }

        // Crear solicitud (DOCENTE)
        [HttpPost]
        public async Task<IActionResult> Post(SolicitudLaboratorio solicitud)
        {
            solicitud.FechaSolicitud = DateTime.Now;
            solicitud.Estado = "Pendiente";

            context.SolicitudesLaboratorio.Add(solicitud);
            await context.SaveChangesAsync();

            return Ok(solicitud);
        }
    }
}
