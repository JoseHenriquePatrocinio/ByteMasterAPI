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

        [HttpGet]
        [Route("ConsultarOrdens")]
        public async Task<ActionResult<IEnumerable<OrdemServicoInfo>>> ConsultarOrdens()
        {
            var query = from o in _context.ostb
                        join c in _context.clientetb on o.IdCliente_os equals c.Id
                        join p in _context.produtotb on o.IdProduto_os equals p.Id
                        select new OrdemServicoInfo
                        {
                            ClienteNome = c.Nome,
                            ProdutoModelo = p.Modelo,
                            DescricaoProduto = p.Descricao,
                            Situacao = o.Situacao
                        };

            var result = await query.ToListAsync();

            if (result == null)
                return NotFound();

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemServico>> GetOrdemServico(int id)
        {
            if (_context.ostb == null)
                return NotFound();

            var ordemServico = await _context.ostb.FindAsync(id);

            if (ordemServico == null)
                return NotFound();

            return ordemServico;
        }

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

        private bool OrdemServicoExists(int id) => (_context.ostb?.Any(e => e.Id == id)).GetValueOrDefault();

    }
}
