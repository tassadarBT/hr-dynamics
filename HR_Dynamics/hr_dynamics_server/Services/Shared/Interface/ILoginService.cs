using System.IdentityModel.Tokens.Jwt;

namespace hr_dynamics_server.Services.Shared.Interface
{
    public interface ILoginService
    {
        JwtSecurityToken GenerateAccessToken(string userName);
    }
}
