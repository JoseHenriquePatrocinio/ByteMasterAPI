using ByteMasterAPI.Context;
using ByteMasterAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ByteMasterAPI.Enum.SituacaoEnum;

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

        [HttpGet]
        [Route("ConsultarOrdens")]
        public async Task<ActionResult<IEnumerable<OrdemServicoInfo>>> ConsultarOrdens()
        {
            var query = from o in _context.ostb
                        join c in _context.clientetb on o.IdCliente_os equals c.Documento
                        join p in _context.produtotb on o.IdProduto_os equals p.Id
                        join s in _context.situacaotb on (int?)o.IdSituacao_os equals s.Id
                        where s.Id == 3
                        select new OrdemServicoInfo
                        {
                            ClienteNome = c.Nome,
                            ProdutoModelo = p.Modelo,
                            DescricaoProduto = p.Descricao,
                            SituacaoDescricao = s.Descricao
                        };

            var result = await query.ToListAsync();

            if (result == null)
                return NotFound();

            return result;
        }

        [HttpGet]
        [Route("ConsultarOrdem")]
        public async Task<ActionResult<IEnumerable<OrdemServicoInfo>>> ConsultarOrdem(int id)
        {
            var query = from o in _context.ostb
                        join c in _context.clientetb on o.IdCliente_os equals c.Documento
                        join p in _context.produtotb on o.IdProduto_os equals p.Id
                        join s in _context.situacaotb on (int?)o.IdSituacao_os equals s.Id
                        where o.Id == id
                        select new OrdemServicoInfo
                        {
                            ClienteNome = c.Nome,
                            ProdutoModelo = p.Modelo,
                            DescricaoProduto = p.Descricao,
                            SituacaoDescricao = s.Descricao
                        };

            var result = await query.ToListAsync();

            if (result == null)
                return NotFound();

            return result;
        }

        [HttpPut]
        [Route("CompletarOrdem/{id}")]
        public async Task<IActionResult> CompletarOrdem(int id)
        {
            var ordemservico = await _context.ostb.FindAsync(id);

            if (ordemservico == null)
                return NotFound();

            ordemservico.IdSituacao_os = Enum.SituacaoEnum.SituacaoOrdem.Completa;

            _context.Entry(ordemservico).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
