using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidad.Entidades;
using Universidad.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Universidad.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(AppDbContext context, ILogger<CategoriasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todas las categorías
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Categoria>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            _logger.LogInformation("Obteniendo todas las categorías");
            return await _context.Categorias
                .Where(c => c.Activo)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene una categoría por su ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null || !categoria.Activo)
            {
                _logger.LogWarning($"Categoría con ID {id} no encontrada");
                return NotFound(new { message = $"Categoría con ID {id} no encontrada" });
            }

            return Ok(categoria);
        }

        /// <summary>
        /// Crea una nueva categoría
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Categoria>> PostCategoria([FromBody] Categoria categoria)
        {
            // Validación: Modelo no sea nulo
            if (categoria == null)
            {
                return BadRequest(new { message = "Los datos de la categoría no pueden ser nulos" });
            }

            // Validación automática de DataAnnotations
            if (!ModelState.IsValid)
            {
                return BadRequest(new { 
                    message = "Error de validación",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            // Validación: No duplicados
            var existeCategoria = await _context.Categorias
                .AnyAsync(c => c.Nombre.ToLower() == categoria.Nombre.ToLower());
            
            if (existeCategoria)
            {
                return BadRequest(new { 
                    message = "Ya existe una categoría con ese nombre",
                    field = "Nombre"
                });
            }

            try
            {
                categoria.Activo = true;
                categoria.FechaCreacion = DateTime.UtcNow;

                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Categoría creada: {categoria.Nombre} (ID: {categoria.Id})");

                return CreatedAtAction(
                    nameof(GetCategoria), 
                    new { id = categoria.Id }, 
                    new {
                        message = "Categoría creada exitosamente",
                        data = categoria
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear categoría: {Nombre}", categoria.Nombre);
                return StatusCode(500, new { 
                    message = "Error interno del servidor al crear la categoría",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtiene los temas de una categoría específica
        /// </summary>
        [HttpGet("{id}/temas")]
        [ProducesResponseType(typeof(IEnumerable<Tema>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Tema>>> GetTemasPorCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null || !categoria.Activo)
            {
                return NotFound(new { message = $"Categoría con ID {id} no encontrada" });
            }

            var temas = await _context.Temas
                .Where(t => t.CategoriaId == id && t.Activo)
                .OrderBy(t => t.Titulo)
                .ToListAsync();

            return Ok(temas);
        }
    }
}
