using static ByteMasterAPI.Enum.SituacaoEnum;

namespace ByteMasterAPI.Model
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public Role? Role { get; set; }

        public Usuario() { }

        public Usuario(LoginRequest user) 
        {
           UserName = user.UserName;
           PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
           Role = user.Role;
        }
    } 

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role? Role { get; set; }

    }
}
