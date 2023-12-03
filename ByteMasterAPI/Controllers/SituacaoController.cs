using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByteMasterAPI.Context;
using ByteMasterAPI.Model;

namespace ByteMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SituacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SituacaoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Situacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Situacao>>> Getsituacaotb()
        {
          if (_context.situacaotb == null)
          {
              return NotFound();
          }
            return await _context.situacaotb.ToListAsync();
        }

        // GET: api/Situacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Situacao>> GetSituacao(int id)
        {
          if (_context.situacaotb == null)
          {
              return NotFound();
          }
            var situacao = await _context.situacaotb.FindAsync(id);

            if (situacao == null)
            {
                return NotFound();
            }

            return situacao;
        }

        // PUT: api/Situacao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSituacao(int id, Situacao situacao)
        {
            if (id != situacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(situacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SituacaoExists(id))
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

        // POST: api/Situacao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Situacao>> PostSituacao(Situacao situacao)
        {
          if (_context.situacaotb == null)
          {
              return Problem("Entity set 'AppDbContext.situacaotb'  is null.");
          }
            _context.situacaotb.Add(situacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSituacao", new { id = situacao.Id }, situacao);
        }

        // DELETE: api/Situacao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSituacao(int id)
        {
            if (_context.situacaotb == null)
            {
                return NotFound();
            }
            var situacao = await _context.situacaotb.FindAsync(id);
            if (situacao == null)
            {
                return NotFound();
            }

            _context.situacaotb.Remove(situacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SituacaoExists(int id)
        {
            return (_context.situacaotb?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
