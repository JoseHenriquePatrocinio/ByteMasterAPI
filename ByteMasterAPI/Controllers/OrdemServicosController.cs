using ByteMasterAPI.Context;
using ByteMasterAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdemServicosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdemServicosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/OrdemServicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdemServico>>> Getostb()
        {
            if (_context.ostb == null)
            {
                return NotFound();
            }
            return await _context.ostb.ToListAsync();
        }

        // GET: api/OrdemServicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemServico>> GetOrdemServico(int id)
        {
            if (_context.ostb == null)
            {
                return NotFound();
            }
            var ordemServico = await _context.ostb.FindAsync(id);

            if (ordemServico == null)
            {
                return NotFound();
            }

            return ordemServico;
        }

        // PUT: api/OrdemServicos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrdemServico(int id, OrdemServico ordemServico)
        {
            if (id != ordemServico.Id)
            {
                return BadRequest();
            }

            _context.Entry(ordemServico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdemServicoExists(id))
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

        // POST: api/OrdemServicos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrdemServico>> PostOrdemServico(OrdemServico ordemServico)
        {
            if (_context.ostb == null)
            {
                return Problem("Entity set 'AppDbContext.ostb'  is null.");
            }
            _context.ostb.Add(ordemServico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrdemServico", new { id = ordemServico.Id }, ordemServico);
        }

        // DELETE: api/OrdemServicos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdemServico(int id)
        {
            if (_context.ostb == null)
            {
                return NotFound();
            }
            var ordemServico = await _context.ostb.FindAsync(id);
            if (ordemServico == null)
            {
                return NotFound();
            }

            _context.ostb.Remove(ordemServico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrdemServicoExists(int id)
        {
            return (_context.ostb?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
