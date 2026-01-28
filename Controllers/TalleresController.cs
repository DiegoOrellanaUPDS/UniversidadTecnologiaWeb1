using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidad.Entidades;
using Universidad.Data;

namespace Universidad.Controllers
{
    /// <summary>
    /// Controlador para gestionar talleres
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TalleresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TalleresController> _logger;

        public TalleresController(AppDbContext context, ILogger<TalleresController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los talleres
        /// </summary>
        /// <returns>Lista de talleres</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Taller>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Taller>>> GetTalleres()
        {
            _logger.LogInformation("Obteniendo todos los talleres");
            return await _context.Talleres.ToListAsync();
        }

        /// <summary>
        /// Obtiene un taller por su ID
        /// </summary>
        /// <param name="id">ID del taller</param>
        /// <returns>El taller solicitado</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Taller), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Taller>> GetTaller(int id)
        {
            var taller = await _context.Talleres.FindAsync(id);

            if (taller == null)
            {
                _logger.LogWarning($"Taller con ID {id} no encontrado");
                return NotFound(new { message = $"Taller con ID {id} no encontrado" });
            }

            return Ok(taller);
        }

        /// <summary>
        /// Crea un nuevo taller
        /// </summary>
        /// <remarks>
        /// Ejemplo de solicitud:
        /// 
        ///     POST /api/Talleres
        ///     {
        ///         "nombre": "Taller de Programación",
        ///         "descripcion": "Aprende a programar en C#",
        ///         "capacidadMaxima": 30,
        ///         "fechaInicio": "2024-03-01T09:00:00",
        ///         "fechaFin": "2024-03-15T18:00:00",
        ///         "profesorId": 1
        ///     }
        /// </remarks>
        /// <param name="taller">Datos del nuevo taller</param>
        /// <returns>El taller creado</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Taller), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Taller>> PostTaller([FromBody] Taller taller)
        {
            // Validación 1: Modelo no sea nulo
            if (taller == null)
            {
                _logger.LogWarning("Intento de crear taller con datos nulos");
                return BadRequest(new { 
                    message = "Los datos del taller no pueden ser nulos",
                    status = 400 
                });
            }

            // Validación 2: Validación automática de DataAnnotations
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Validación fallida: {@Errors}", ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(new { 
                    message = "Error de validación",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)),
                    status = 400 
                });
            }

            // Validación 3: Fechas coherentes
            if (taller.FechaFin <= taller.FechaInicio)
            {
                _logger.LogWarning("Fecha fin menor o igual a fecha inicio");
                return BadRequest(new { 
                    message = "La fecha de fin debe ser posterior a la fecha de inicio",
                    field = "FechaFin",
                    status = 400 
                });
            }

            // Validación 4: No duplicados (opcional)
            var existeTaller = await _context.Talleres
                .AnyAsync(t => t.Nombre.ToLower() == taller.Nombre.ToLower());
            
            if (existeTaller)
            {
                _logger.LogWarning($"Intento de crear taller duplicado: {taller.Nombre}");
                return BadRequest(new { 
                    message = "Ya existe un taller con ese nombre",
                    field = "Nombre",
                    status = 400 
                });
            }

            try
            {
                // Asignar valores automáticos
                taller.FechaCreacion = DateTime.UtcNow;
                taller.FechaActualizacion = DateTime.UtcNow;
                taller.Activo = true;

                _context.Talleres.Add(taller);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Taller creado exitosamente: {taller.Nombre} (ID: {taller.Id})");

                // Retornar 201 Created con URL del nuevo recurso
                return CreatedAtAction(
                    nameof(GetTaller), 
                    new { id = taller.Id }, 
                    new {
                        message = "Taller creado exitosamente",
                        data = taller,
                        links = new {
                            self = Url.Action(nameof(GetTaller), new { id = taller.Id }),
                            all = Url.Action(nameof(GetTalleres))
                        }
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear taller: {Nombre}", taller.Nombre);
                return StatusCode(500, new { 
                    message = "Error interno del servidor al crear el taller",
                    error = ex.Message,
                    status = 500 
                });
            }
        }

        /// <summary>
        /// Verifica si el servicio está funcionando
        /// </summary>
        /// <returns>Estado del servicio</returns>
        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult HealthCheck()
        {
            return Ok(new { 
                status = "healthy", 
                service = "Talleres API",
                timestamp = DateTime.UtcNow,
                database = _context.Database.CanConnect() ? "connected" : "disconnected"
            });
        }
    }
}
