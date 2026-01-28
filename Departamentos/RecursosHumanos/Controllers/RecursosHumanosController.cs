using Microsoft.AspNetCore.Mvc;
using Universidad.Departamentos.RecursosHumanos.Entidades;

namespace Universidad.Departamentos.RecursosHumanos.Controllers
{
    [ApiController]
    [Route("api/recursos-humanos")]
    public class RecursosHumanosController : ControllerBase
    {
        [HttpPost]
        public IActionResult CrearEmpleado([FromBody] EmpleadoRRHH empleado)
        {
            if (empleado == null)
            {
                return BadRequest("El objeto empleado no puede ser nulo");
            }

            return Created("", new
            {
                mensaje = "Empleado de Recursos Humanos creado correctamente",
                data = empleado
            });
        }
    }
}

