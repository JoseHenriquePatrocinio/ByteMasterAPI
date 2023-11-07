using ByteMasterAPI.Context;
using ByteMasterAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Route("ConsultarOrcamentos")]
        public async Task<ActionResult<IEnumerable<OrcamentoInfo>>> Getorcamentotb()
        {
            var query = from o in _context.orcamentotb
                        join c in _context.clientetb on o.IdCliente equals c.Id
                        join p in _context.produtotb on o.IdProduto equals p.Id
                        join s in _context.situacaotb on (int?)o.IdSituacao equals s.Id
                        select new OrcamentoInfo
                        {
                            ClienteNome = c.Nome,
                            ProdutoModelo = p.Modelo,
                            ProdutoValorUnitario = p.ValorUnit,
                            SituacaoDescricao = s.Descricao
                        };

            var result = await query.ToListAsync();

            if (result == null)
                return NotFound();

            return result;
        }

        [HttpGet]
        [Route("ConsultarOrcamento")]
        public async Task<ActionResult<IEnumerable<OrcamentoInfo>>> ConsultarOrcamento(int id)
        {
            var query = from o in _context.orcamentotb
                        join c in _context.clientetb on o.IdCliente equals c.Id
                        join p in _context.produtotb on o.IdProduto equals p.Id
                        join s in _context.situacaotb on (int?)o.IdSituacao equals s.Id
                        where o.Id == id
                        select new OrcamentoInfo
                        {
                            ClienteNome = c.Nome,
                            ProdutoModelo = p.Modelo,
                            ProdutoValorUnitario = p.ValorUnit,
                            SituacaoDescricao = s.Descricao
                        };

            var result = await query.ToListAsync();

            if (result == null)
                return NotFound();

            return result;
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
