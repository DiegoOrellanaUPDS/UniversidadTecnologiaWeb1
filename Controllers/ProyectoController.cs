using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Universidad.Data;
using UniversidadTecnologiaWeb1.Entidades;

namespace UniversidadTecnologiaWeb1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectoController : ControllerBase
    {
        private readonly AppDbContext context;
        public ProyectoController(AppDbContext context)
        {
            this.context=context;
        }
        [HttpGet("listar")]
        public async Task<IActionResult> GetProyectos()
        {
            return Ok(await (from e in context.Proyectos
                             where e.estado == "Activo"
                             select e).ToListAsync());
        }
        [HttpGet("buscar")]
        public async Task<IActionResult> GetProyecto(string codigo)
        {
            var e = await context.Proyectos
                .FirstOrDefaultAsync(x => x.codigo == codigo && x.estado == "Activo");
        
            if (e == null)
                return NotFound("No se encontró el codigo.");
        
            return Ok(e);
        }
        [HttpPost("crear")]
        public async Task<IActionResult> PostProyecto(Proyecto pro)
        {
            var ver = await context.Proyectos
                .FirstOrDefaultAsync(x => x.codigo== pro.codigo);
        
            if (ver != null)
                return NotFound("Ese Proyecto con ese CodigoCampo ya existe.");
        
            pro.estado = "Activo";
            await context.Proyectos.AddAsync(pro);
            await context.SaveChangesAsync();
            return Ok(pro );
        }
        [HttpPut("actulizar")]
        public async Task<IActionResult> PutProyecto(Proyecto pro)
        {
            var db = await context.Proyectos
                .FirstOrDefaultAsync(x => x.codigo == pro.codigo && x.estado == "Activo");
        
            if (db == null)
                return NotFound("No existe ese codigo.");
        
            db.colaboradores=pro.colaboradores;
            db.descripcion=pro.descripcion;
            db.fechaInicio=pro.fechaInicio;
            db.fechaFin=pro.fechaFin;
            db.titulo=pro.titulo;            
        
            await context.SaveChangesAsync();
            return Ok($"Se actualizó el codigo: {pro.codigo}");
        }
        [HttpDelete("eliminar")]
        public async Task<IActionResult> DeleteProyecto(string codigo)
        {
            var pro = await context.Proyectos
                .FirstOrDefaultAsync(x => x.codigo == codigo && x.estado == "Activo");
        
            if (pro == null)
                return NotFound("No existe ese codigo para eliminar.");
        
            pro.estado = "Borrado";
            await context.SaveChangesAsync();
        
            return Ok($"Se eliminó el Proyecto con codigo: {codigo}");
        }
    }
}