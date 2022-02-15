using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecuConcesionario.Models;

namespace RecuConcesionario.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConcesionarioController : ControllerBase
    {
        private readonly ConcesionarioContext db;

        public ConcesionarioController(ConcesionarioContext context)
        {
            db = context;
        }

        // GET: api/Concesionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concesionario>>> GetConcesionario()
        {
            return await db.Concesionario.ToListAsync();
        }

        // GET: api/Concesionario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Concesionario>> GetConcesionario(int id)
        {
            var concesionario = await db.Concesionario.FindAsync(id);

            if (concesionario == null)
            {
                return NotFound();
            }

            return concesionario;
        }


        [HttpGet("3")]
        public async Task<ActionResult> Query3()
        {
            // LA QUERY
            var list2 = await db.Concesionario
            .SelectMany(s => s.Smarka, (c, s) =>  new
                {
                    Concesionario = c.ConcesionarioId,
                    Marca = c.Marca,
                    Total = s.Cantidad * s.Modelo.Importe
                })
            .GroupBy(c1 => new { c1.Concesionario, c1.Marca})
            .Select(g => new {
                Concesionario = g.Key.Concesionario,
                TotalConcesionario = g.Sum(c1 => c1.Total)
            })
            .ToListAsync();

            return Ok(new
            {
                ValorActual = 3,
                Descripcion = "Ejemplo en MODO NO ASYNC - DEBE SER ASÍNCRONOS",
                Valores = list2,
            });
        }
    
        [HttpGet("4")]
        public async Task<ActionResult> Query4()
        {
            var list2 = await db.Concesionario
            .SelectMany(s => s.Smarka, (c, s) =>  new
                {
                    Concesionario = c.ConcesionarioId,
                    Marca = c.Marca,
                    Total = s.Cantidad * s.Modelo.Importe
                })
            .GroupBy(c1 => new { c1.Concesionario, c1.Marca})
            .Select(g => new {
                Concesionario = g.Key.Concesionario,
                Marca = g.Key.Marca,
                TotalConcesionario = g.Sum(c1 => c1.Total)
            })
            .ToListAsync();

            // LA QUERY
            var list =  list2
            .GroupBy(g => g.Marca)
            .Select( a => new{
                Marca = a.Key,
                TotalImporte = a.Sum(c1 => c1.TotalConcesionario),
                Concesionarios = a.Count()
            })
            .ToList();

            return Ok(new
            {
                ValorActual = 4,
                Descripcion = "Ejemplo en MODO NO ASYNC - DEBE SER ASÍNCRONOS",
                Valores = list,
            });
        }


        private bool ConcesionarioExists(int id)
        {
            return db.Concesionario.Any(e => e.ConcesionarioId == id);
        }
    }
}
