using Microsoft.AspNetCore.Mvc;
using Universidad.Departamentos.RecursosHumanos.Entidades;

namespace Universidad.Departamentos.RecursosHumanos.Controllers
{
    [ApiController]
    [Route("api/recursos-humanos")]
    public class RecursosHumanosController : ControllerBase
    {
        [HttpGet("empleados")]
        public IActionResult GetEmpleados()
        {
            var empleados = new List<Empleado>
            {
                new Empleado
                {
                    Id = 1,
                    Nombre = "Juan Pérez",
                    Cargo = "Analista de RRHH"
                }
            };

            return Ok(empleados);
        }
    }
}
