using ByteMasterAPI.Context;
using ByteMasterAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ByteMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route ("ConsultarProdutos")]
        public async Task<ActionResult<IEnumerable<Produto>>> ConsultarProdutos()
        {
            if (_context.produtotb == null)       
                return NotFound();
            
            return await _context.produtotb.ToListAsync();
        }

        [HttpGet]
        [Route("ConsultarProduto")]
        public async Task<ActionResult<Produto>> ConsultarProduto(int id)
        {           
            var produto = await _context.produtotb.FindAsync(id);

            if (produto == null)          
                return NotFound();
            
            return produto;
        }

        [HttpPut]
        [Route("EditarProduto")]
        public async Task<IActionResult> EditarProduto(int id,Produto produto)
        {
            if (id != produto.Id)           
                return BadRequest();        

            _context.Entry(produto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("AdicionarNovoProduto")]
        public async Task<ActionResult<Produto>> AdicionarNovoProduto(Produto produto)
        {
            if (_context.produtotb == null)          
                return Problem("Entity set 'AppDbContext.produtotb'  is null.");
            
            _context.produtotb.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("ConsultarProduto", new { id = produto.Id }, produto);
        }

        private bool ProdutoExists(int id) => (_context.produtotb?.Any(e => e.Id == id)).GetValueOrDefault();  
    }
}
