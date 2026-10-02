using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PC01_Huaman_pillco.Core.Core.Entities;
using PC01_Huaman_pillco.Core.Infrastructure.Data;

namespace PC01_Huaman_Pillco.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoServiciosController : ControllerBase
    {
        private readonly TallerDbContext _context;

        public TipoServiciosController(TallerDbContext context)
        {
            _context = context;
        }

        // GET: api/TipoServicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoServicio>>> GetTipoServicios()
        {
            return await _context.TipoServicios.ToListAsync();
        }

        // GET: api/TipoServicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoServicio>> GetTipoServicio(int id)
        {
            var tipoServicio = await _context.TipoServicios.FindAsync(id);

            if (tipoServicio == null)
            {
                return NotFound();
            }

            return tipoServicio;
        }

        // PUT: api/TipoServicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoServicio(int id, TipoServicio tipoServicio)
        {
            if (id != tipoServicio.Id)
            {
                return BadRequest();
            }

            _context.Entry(tipoServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoServicioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TipoServicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TipoServicio>> PostTipoServicio(TipoServicio tipoServicio)
        {
            _context.TipoServicios.Add(tipoServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipoServicio", new { id = tipoServicio.Id }, tipoServicio);
        }

        // DELETE: api/TipoServicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoServicio(int id)
        {
            var tipoServicio = await _context.TipoServicios.FindAsync(id);
            if (tipoServicio == null)
            {
                return NotFound();
            }

            _context.TipoServicios.Remove(tipoServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipoServicioExists(int id)
        {
            return _context.TipoServicios.Any(e => e.Id == id);
        }
    }
}
