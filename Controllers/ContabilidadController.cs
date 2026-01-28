
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universidad.Data;

namespace Entidades.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContabilidadController : ControllerBase
    {

        private static List<TransaccionContable> _transacciones = new List<TransaccionContable>
        {
            new TransaccionContable 
            { 
                Id = 1, 
                Descripcion = "Compra de materiales de oficina", 
                Monto = 1250.50m, 
                Fecha = DateTime.Now.AddDays(-5),
                Tipo = "Gasto",
                Categoria = "Materiales",
                Estado = "Aprobado"
            },
            new TransaccionContable 
            { 
                Id = 2, 
                Descripcion = "Venta de producto terminado", 
                Monto = 3500.00m, 
                Fecha = DateTime.Now.AddDays(-3),
                Tipo = "Ingreso",
                Categoria = "Ventas",
                Estado = "Aprobado"
            }
        };


        [HttpGet]
        public ActionResult<IEnumerable<TransaccionContable>> GetTransacciones()
        {
            return Ok(_transacciones.OrderByDescending(t => t.Fecha));
        }


        [HttpGet("{id}")]
        public ActionResult<TransaccionContable> GetTransaccion(int id)
        {
            var transaccion = _transacciones.FirstOrDefault(t => t.Id == id);
            
            if (transaccion == null)
            {
                return NotFound(new { mensaje = $"Transacción con ID {id} no encontrada" });
            }
            
            return Ok(transaccion);
        }
        [HttpGet("por-tipo/{tipo}")]
        public ActionResult<IEnumerable<TransaccionContable>> GetTransaccionesPorTipo(string tipo)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var transacciones = _transacciones
                .Where(t => t.Tipo.Equals(tipo, StringComparison.OrdinalIgnoreCase))
                .ToList();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            if (!transacciones.Any())
            {
                return NotFound(new { mensaje = $"No hay transacciones del tipo '{tipo}'" });
            }
            
            return Ok(transacciones);
        }
        [HttpPost]
        public ActionResult<TransaccionContable> CrearTransaccion([FromBody] TransaccionContable transaccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Generar nuevo ID
            transaccion.Id = _transacciones.Count > 0 ? _transacciones.Max(t => t.Id) + 1 : 1;
            transaccion.FechaCreacion = DateTime.Now;
            
            _transacciones.Add(transaccion);
            
            return CreatedAtAction(
                nameof(GetTransaccion), 
                new { id = transaccion.Id }, 
                new { 
                    mensaje = "Transacción creada exitosamente", 
                    transaccion 
                });
        }
        [HttpPut("{id}")]
        public ActionResult<TransaccionContable> ActualizarTransaccion(int id, [FromBody] TransaccionContable transaccionActualizada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaccionExistente = _transacciones.FirstOrDefault(t => t.Id == id);
            
            if (transaccionExistente == null)
            {
                return NotFound(new { mensaje = $"Transacción con ID {id} no encontrada" });
            }

            // Actualizar propiedades
            transaccionExistente.Descripcion = transaccionActualizada.Descripcion;
            transaccionExistente.Monto = transaccionActualizada.Monto;
            transaccionExistente.Fecha = transaccionActualizada.Fecha;
            transaccionExistente.Tipo = transaccionActualizada.Tipo;
            transaccionExistente.Categoria = transaccionActualizada.Categoria;
            transaccionExistente.Estado = transaccionActualizada.Estado;
            transaccionExistente.FechaModificacion = DateTime.Now;

            return Ok(new { 
                mensaje = $"Transacción {id} actualizada exitosamente", 
                transaccion = transaccionExistente 
            });
        }
        [HttpGet("resumen")]
        public ActionResult<object> GetResumenContable()
        {
            var totalIngresos = _transacciones
                .Where(t => t.Tipo == "Ingreso")
                .Sum(t => t.Monto);
                
            var totalGastos = _transacciones
                .Where(t => t.Tipo == "Gasto")
                .Sum(t => t.Monto);
                
            var balance = totalIngresos - totalGastos;

            return Ok(new
            {
                TotalIngresos = totalIngresos,
                TotalGastos = totalGastos,
                Balance = balance,
                TotalTransacciones = _transacciones.Count,
                TransaccionesPendientes = _transacciones.Count(t => t.Estado == "Pendiente"),
                UltimaActualizacion = DateTime.Now
            });
        }
    }
}