using CRUD.Models;
using CRUD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        public IConfiguration _configuration;
        private readonly ApiContext _context;

        public TokenController(IConfiguration config, ApiContext context)
        {
            _configuration = config;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AutenticaUsuario(User _user)
        {

            if (_user != null && _user.Email != null && _user.Senha != null)
            {
                var user = await GetUsuario(_user.Email, _user.Senha);

                if (user != null)
                {
                    //cria claims baseado nas informações do usuário
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Nome", user.Nome),
                    new Claim("Email", user.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                                 _configuration["Jwt:Audience"], claims,
                                 expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Credenciais inválidas");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUsuario(string email, string password)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email && u.Senha == GenericService.hashString(password));
        }
    }
}
