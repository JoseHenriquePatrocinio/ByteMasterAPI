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
    public class OrcamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrcamentosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orcamento>>> Getorcamentotb()
        {
          if (_context.orcamentotb == null)
          {
              return NotFound();
          }
            return await _context.orcamentotb.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Orcamento>> GetOrcamento(int id)
        {
          if (_context.orcamentotb == null)
          {
              return NotFound();
          }
            var orcamento = await _context.orcamentotb.FindAsync(id);

            if (orcamento == null)
            {
                return NotFound();
            }

            return orcamento;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrcamento(int id, Orcamento orcamento)
        {
            if (id != orcamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(orcamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrcamentoExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Orcamento>> PostOrcamento(Orcamento orcamento)
        {
          if (_context.orcamentotb == null)
          {
              return Problem("Entity set 'AppDbContext.orcamentotb'  is null.");
          }
            _context.orcamentotb.Add(orcamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrcamento", new { id = orcamento.Id }, orcamento);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrcamento(int id)
        {
            if (_context.orcamentotb == null)
            {
                return NotFound();
            }
            var orcamento = await _context.orcamentotb.FindAsync(id);
            if (orcamento == null)
            {
                return NotFound();
            }

            _context.orcamentotb.Remove(orcamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrcamentoExists(int id)
        {
            return (_context.orcamentotb?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
