using Microsoft.AspNetCore.Mvc;
using Universidad.Departamentos.RecursosHumanos.Entidades;
using System.Collections.Generic;

namespace Universidad.Departamentos.RecursosHumanos.Controllers
{
    [ApiController]
    [Route("api/recursos-humanos/empleados")]
    public class EmpleadoRRHHController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var empleados = new List<EmpleadoRRHH>
            {
                new EmpleadoRRHH
                {
                    Id = 1,
                    NombreCompleto = "Juan Perez",
                    Cargo = "Analista",
                    Area = "Recursos Humanos",
                    Activo = true
                }
            };

            return Ok(empleados);
        }
    }
}
