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
    public class PodcastsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PodcastsController> _logger;

        public PodcastsController(AppDbContext context, ILogger<PodcastsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene todos los podcasts
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Podcast>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Podcast>>> GetPodcasts()
        {
            _logger.LogInformation("Obteniendo todos los podcasts");
            return await _context.Podcasts
                .Include(p => p.Tema)
                .Where(p => p.Activo)
                .OrderByDescending(p => p.FechaCreacion)
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un podcast por su ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Podcast), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Podcast>> GetPodcast(int id)
        {
            var podcast = await _context.Podcasts
                .Include(p => p.Tema)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (podcast == null || !podcast.Activo)
            {
                _logger.LogWarning($"Podcast con ID {id} no encontrado");
                return NotFound(new { message = $"Podcast con ID {id} no encontrado" });
            }

            return Ok(podcast);
        }

        /// <summary>
        /// Crea un nuevo podcast
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Podcast), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Podcast>> PostPodcast([FromBody] Podcast podcast)
        {
            // Validación: Modelo no sea nulo
            if (podcast == null)
            {
                return BadRequest(new { message = "Los datos del podcast no pueden ser nulos" });
            }

            // Validación automática de DataAnnotations
            if (!ModelState.IsValid)
            {
                return BadRequest(new { 
                    message = "Error de validación",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            // Validación: Tema existe
            var temaExiste = await _context.Temas
                .AnyAsync(t => t.Id == podcast.TemaId && t.Activo);
            
            if (!temaExiste)
            {
                return BadRequest(new { 
                    message = "El tema seleccionado no existe o no está activo",
                    field = "TemaId"
                });
            }

            try
            {
                podcast.Activo = true;
                podcast.FechaCreacion = DateTime.UtcNow;
                podcast.FechaActualizacion = DateTime.UtcNow;

                _context.Podcasts.Add(podcast);
                await _context.SaveChangesAsync();

                // Cargar relaciones para la respuesta
                await _context.Entry(podcast)
                    .Reference(p => p.Tema)
                    .LoadAsync();

                _logger.LogInformation($"Podcast creado: {podcast.Titulo} (ID: {podcast.Id})");

                return CreatedAtAction(
                    nameof(GetPodcast), 
                    new { id = podcast.Id }, 
                    new {
                        message = "Podcast creado exitosamente",
                        data = podcast
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear podcast: {Titulo}", podcast.Titulo);
                return StatusCode(500, new { 
                    message = "Error interno del servidor al crear el podcast",
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtiene los podcasts por tema
        /// </summary>
        [HttpGet("por-tema/{temaId}")]
        [ProducesResponseType(typeof(IEnumerable<Podcast>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Podcast>>> GetPodcastsPorTema(int temaId)
        {
            var podcasts = await _context.Podcasts
                .Include(p => p.Tema)
                .Where(p => p.TemaId == temaId && p.Activo)
                .OrderByDescending(p => p.FechaCreacion)
                .ToListAsync();

            return Ok(podcasts);
        }

        /// <summary>
        /// Obtiene los episodios de un podcast
        /// </summary>
        [HttpGet("{id}/episodios")]
        [ProducesResponseType(typeof(IEnumerable<Episodio>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Episodio>>> GetEpisodiosPorPodcast(int id)
        {
            var podcast = await _context.Podcasts.FindAsync(id);

            if (podcast == null || !podcast.Activo)
            {
                return NotFound(new { message = $"Podcast con ID {id} no encontrado" });
            }

            var episodios = await _context.Episodios
                .Where(e => e.PodcastId == id && e.Activo)
                .OrderByDescending(e => e.FechaPublicacion)
                .ToListAsync();

            return Ok(episodios);
        }
    }
}
