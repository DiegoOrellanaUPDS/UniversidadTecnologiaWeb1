using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace UniversidadTecnologiaWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BibliotecaController : ControllerBase
    {
        // Lista estática para simular una base de datos en memoria
        private static List<Libro> libros = new List<Libro>
        {
            new Libro { Id = 1, Titulo = "Don Quijote de la Mancha", Autor = "Miguel de Cervantes", ISBN = "978-84-376-0494-7", Disponible = true },
            new Libro { Id = 2, Titulo = "Cien Años de Soledad", Autor = "Gabriel García Márquez", ISBN = "978-03-074-7422-7", Disponible = true },
            new Libro { Id = 3, Titulo = "1984", Autor = "George Orwell", ISBN = "978-84-9838-577-8", Disponible = false }
        };

        // GET: api/biblioteca
        // Obtener todos los libros
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> GetLibros()
        {
            return Ok(libros);
        }

        // GET: api/biblioteca/5
        // Obtener un libro por ID
        [HttpGet("{id}")]
        public ActionResult<Libro> GetLibro(int id)
        {
            var libro = libros.FirstOrDefault(l => l.Id == id);

            if (libro == null)
            {
                return NotFound(new { mensaje = $"Libro con ID {id} no encontrado" });
            }

            return Ok(libro);
        }

        // POST: api/biblioteca
        // Crear un nuevo libro
        [HttpPost]
        public ActionResult<Libro> CrearLibro([FromBody] Libro nuevoLibro)
        {
            if (nuevoLibro == null)
            {
                return BadRequest(new { mensaje = "Datos del libro inválidos" });
            }

            // Generar un nuevo ID
            nuevoLibro.Id = libros.Any() ? libros.Max(l => l.Id) + 1 : 1;
            nuevoLibro.Disponible = true;

            libros.Add(nuevoLibro);

            return CreatedAtAction(nameof(GetLibro), new { id = nuevoLibro.Id }, nuevoLibro);
        }

        // PUT: api/biblioteca/5
        // Actualizar un libro existente
        [HttpPut("{id}")]
        public ActionResult<Libro> ActualizarLibro(int id, [FromBody] Libro libroActualizado)
        {
            var libro = libros.FirstOrDefault(l => l.Id == id);

            if (libro == null)
            {
                return NotFound(new { mensaje = $"Libro con ID {id} no encontrado" });
            }

            libro.Titulo = libroActualizado.Titulo;
            libro.Autor = libroActualizado.Autor;
            libro.ISBN = libroActualizado.ISBN;
            libro.Disponible = libroActualizado.Disponible;

            return Ok(libro);
        }

        // DELETE: api/biblioteca/5
        // Eliminar un libro
        [HttpDelete("{id}")]
        public ActionResult EliminarLibro(int id)
        {
            var libro = libros.FirstOrDefault(l => l.Id == id);

            if (libro == null)
            {
                return NotFound(new { mensaje = $"Libro con ID {id} no encontrado" });
            }

            libros.Remove(libro);

            return Ok(new { mensaje = "Libro eliminado correctamente" });
        }

        // GET: api/biblioteca/disponibles
        // Obtener solo libros disponibles
        [HttpGet("disponibles")]
        public ActionResult<IEnumerable<Libro>> GetLibrosDisponibles()
        {
            var librosDisponibles = libros.Where(l => l.Disponible).ToList();
            return Ok(librosDisponibles);
        }

        // PUT: api/biblioteca/5/prestar
        // Prestar un libro (cambiar disponibilidad a false)
        [HttpPut("{id}/prestar")]
        public ActionResult PrestarLibro(int id)
        {
            var libro = libros.FirstOrDefault(l => l.Id == id);

            if (libro == null)
            {
                return NotFound(new { mensaje = $"Libro con ID {id} no encontrado" });
            }

            if (!libro.Disponible)
            {
                return BadRequest(new { mensaje = "El libro ya está prestado" });
            }

            libro.Disponible = false;

            return Ok(new { mensaje = "Libro prestado exitosamente", libro });
        }

        // PUT: api/biblioteca/5/devolver
        // Devolver un libro (cambiar disponibilidad a true)
        [HttpPut("{id}/devolver")]
        public ActionResult DevolverLibro(int id)
        {
            var libro = libros.FirstOrDefault(l => l.Id == id);

            if (libro == null)
            {
                return NotFound(new { mensaje = $"Libro con ID {id} no encontrado" });
            }

            if (libro.Disponible)
            {
                return BadRequest(new { mensaje = "El libro ya está disponible" });
            }

            libro.Disponible = true;

            return Ok(new { mensaje = "Libro devuelto exitosamente", libro });
        }
    }

    // Modelo de datos para Libro
    public class Libro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public bool Disponible { get; set; }
    }
}