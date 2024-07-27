using hr_dynamics_server.Models;
using hr_dynamics_server.Services.Shared.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace hr_dynamics_server.Controllers.Public
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILoginService _loginService;
        public AuthController(UserManager<IdentityUser> userManager, ILoginService loginService)
        {
            _userManager = userManager;
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userDb = await _userManager.FindByNameAsync(model.Username ?? string.Empty);
                if (userDb != null)
                {
                    if (await _userManager.CheckPasswordAsync(userDb, model.Password ?? string.Empty))
                    {
                        var tokenObject = _loginService.GenerateAccessToken(model.Username ?? string.Empty);
                        var tokenStr = new JwtSecurityTokenHandler().WriteToken(tokenObject);
                        return Ok(new
                        {
                            success = true,
                            token = tokenStr
                        });
                    }
                    else
                        return Ok(new { success = false, errorMessage = "Password is wrong!" });
                }
                else
                    return Ok(new { success = false, errorMessage = "Username is wrong!" });
            }
            else
                return Ok(new { success = false, errorMessage = "Validation failed!" });
        }
    }
}
