using hr_dynamics_server.Services.Shared.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hr_dynamics_server.Services.Shared.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;

        public LoginService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public JwtSecurityToken GenerateAccessToken(string userName)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, userName)
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8), 
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? string.Empty)), SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
    }
}
