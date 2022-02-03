using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patrimonio.Contexts;
using Patrimonio.Domains;

namespace Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfisController : ControllerBase
    {
        private readonly PatrimonioContext _context;

        public PerfisController(PatrimonioContext context)
        {
            _context = context;
        }

        // GET: api/Perfis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Perfil>>> GetPerfils()
        {
            return await _context.Perfils.ToListAsync();
        }

        // GET: api/Perfis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Perfil>> GetPerfil(int id)
        {
            var perfil = await _context.Perfils.FindAsync(id);

            if (perfil == null)
            {
                return NotFound();
            }

            return perfil;
        }

        // PUT: api/Perfis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerfil(int id, Perfil perfil)
        {
            if (id != perfil.Id)
            {
                return BadRequest();
            }

            _context.Entry(perfil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilExists(id))
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

        // POST: api/Perfis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Perfil>> PostPerfil(Perfil perfil)
        {
            _context.Perfils.Add(perfil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerfil", new { id = perfil.Id }, perfil);
        }

        // DELETE: api/Perfis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerfil(int id)
        {
            var perfil = await _context.Perfils.FindAsync(id);
            if (perfil == null)
            {
                return NotFound();
            }

            _context.Perfils.Remove(perfil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerfilExists(int id)
        {
            return _context.Perfils.Any(e => e.Id == id);
        }
    }
}
