using ByteMasterAPI.Context;
using ByteMasterAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

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

            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(Usuario user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("68580e2222864915990c92f390d92426"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
