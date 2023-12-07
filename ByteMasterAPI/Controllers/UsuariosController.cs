using ByteMasterAPI.Context;
using ByteMasterAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ByteMasterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ConsultarUsuario/{userName}")]
        public async Task<ActionResult<Usuario>> ConsultarUsuario(string userName)
        {
            var usuario = await _context.usuariotb.FindAsync(userName);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        [Route("CadastrarUsuario")]
        public async Task<ActionResult<Usuario>> CadastrarUsuario(LoginRequest request)
        {
            var usuario = new Usuario(request);
            _context.usuariotb.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ConsultarUsuario), new { userName = usuario.UserName }, usuario);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var user = _context.usuariotb.FirstOrDefault(u => u.UserName == model.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                return Unauthorized("Usuário ou senha incorretos");
            }

            return Ok();
        }

    }
}
