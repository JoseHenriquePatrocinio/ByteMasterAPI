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
                        join c in _context.clientetb on o.IdCliente equals c.Documento
                        join p in _context.produtotb on o.IdProduto equals p.Id
                        join s in _context.situacaotb on (int?)o.IdSituacao equals s.Id
                        where s.Id == 0
                        select new OrcamentoInfo
                        {
                            Id = o.Id,
                            ClienteNome = c.Nome,
                            ProdutoModelo = p.Modelo,
                            ProdutoValorUnitario = p.ValorUnit,
                            SituacaoDescricao = s.Descricao,
                            Data = o.Data
                        };

            var result = await query.ToListAsync();

            if (result == null)
                return NotFound();

            return result;
        }

        [HttpGet]
        [Route("ConsultarOrcamento")]
        public async Task<ActionResult<OrcamentoInfo>> ConsultarOrcamento(int id)
        {
            var query = from o in _context.orcamentotb
                        join c in _context.clientetb on o.IdCliente equals c.Documento
                        join p in _context.produtotb on o.IdProduto equals p.Id
                        join s in _context.situacaotb on (int?)o.IdSituacao equals s.Id
                        where o.Id == id
                        select new OrcamentoInfo
                        {
                            ClienteNome = c.Nome,
                            ProdutoModelo = p.Modelo,
                            ProdutoValorUnitario = p.ValorUnit,
                            SituacaoDescricao = s.Descricao,
                            Data = o.Data
                        };

            var result = await query.FirstOrDefaultAsync();

            if (result == null)
                return NotFound();

            return result;
        }

        [HttpPost]
        [Route("AdicionarOrcamento")]
        public async Task<ActionResult<Orcamento>> AdicionarOrcamento(Orcamento orcamento)
        {
            if (_context.orcamentotb == null)           
                return Problem("Entity set 'AppDbContext.orcamentotb'  is null.");
            
            _context.orcamentotb.Add(orcamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ConsultarOrcamento", new { id = orcamento.Id }, orcamento);
        }

        [HttpPut]
        [Route("AprovarOrcamento/{id}")]
        public async Task<IActionResult> AprovarOrcamento(int id)
        {
            var orcamento = await _context.orcamentotb.FindAsync(id);

            if (orcamento == null)
                return NotFound();

            orcamento.IdSituacao = Enum.SituacaoEnum.SituacaoOrcamento.Aprovado;

            _context.Entry(orcamento).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        [Route("ReprovarOrcamento/{id}")]
        public async Task<IActionResult> ReprovarOrcamento(int id)
        {   
            var orcamento = await _context.orcamentotb.FindAsync(id);

            if (orcamento == null)   
                return NotFound();

            orcamento.IdSituacao = Enum.SituacaoEnum.SituacaoOrcamento.Reprovado;

            _context.Entry(orcamento).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
