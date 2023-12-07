using ByteMasterAPI.Context;
using ByteMasterAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ConsultarClientes")]
        public async Task<ActionResult<IEnumerable<Cliente>>> ConsultarClientes()
        {
            if (_context.clientetb == null)
                return NotFound();

            return await _context.clientetb.ToListAsync();
        }

        [HttpGet]
        [Route("ConsultarCliente/{documento}")]
        public async Task<ActionResult<Cliente>> ConsultarCliente(string documento)
        {      
            var cliente = await _context.clientetb.SingleOrDefaultAsync(c => c.Documento == documento);

            if (cliente == null)           
                return NotFound("Cliente não encontrado");          

            return cliente;
        }

        [HttpPut]
        [Route("AtualizarCadastro/{documento}")]
        public async Task<IActionResult> AtualizarCadastro(string documento, Cliente cliente)
        {
            if (documento != cliente.Documento)
                return BadRequest();

            try
            {
                var clienteExistente = await _context.clientetb.FindAsync(documento);

                if (clienteExistente == null)
                    return NotFound();

                _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return ConcurrencyErrorResponse(ex);
            }

            return Ok();
        }

        [HttpPost]
        [Route("AdicionarCliente")]
        public async Task<ActionResult<Cliente>> AdicionarCliente(Cliente cliente)
        {
            if (_context.clientetb == null)
                return Problem("Entity set 'AppDbContext.clientetb' is null");

            cliente.Documento = RemoverPontuacao(cliente.Documento);
            cliente.Contato = RemoverPontuacao(cliente.Contato);

            _context.clientetb.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ConsultarCliente", new { documento = cliente.Documento }, cliente);
        }

        [HttpPut]
        [Route("SituacaoCliente/{documento}")]
        public async Task<IActionResult> SituacaoCliente(string documento)
        {
            if (_context.clientetb == null)          
                return NotFound();

            var cliente = await _context.clientetb.SingleOrDefaultAsync(c => c.Documento == documento);
            if (cliente == null)           
                return NotFound();

            cliente.Situacao = (cliente.Situacao == Enum.SituacaoEnum.SituacaoCliente.Ativo) ? Enum.SituacaoEnum.SituacaoCliente.Inativo : Enum.SituacaoEnum.SituacaoCliente.Ativo;
            
            await _context.SaveChangesAsync();

            return Ok();
        }

        private string RemoverPontuacao(string documento) => new string(documento.Where(char.IsDigit).ToArray());
        private IActionResult ConcurrencyErrorResponse(DbUpdateConcurrencyException ex) => StatusCode(409, "Conflito de concorrência. Os dados foram modificados por outra transação.");     
    }
}
