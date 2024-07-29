using hr_dynamics_server.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace hr_dynamics_server.Controllers
{
    public class BackendSurveyController : Controller
    {
        private readonly UserSecurityIdentityDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<BackendSurveyController> _logger;
        public BackendSurveyController(UserSecurityIdentityDbContext applicationDbContext, UserManager<IdentityUser> userManager, ILogger<BackendSurveyController> logger)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var userDb = await _applicationDbContext.Users.FirstOrDefaultAsync(t => t.UserName == "marian.hluscu@gmail.com", cancellationToken);
            var token = await _userManager.GeneratePasswordResetTokenAsync(userDb);
            var res = await _userManager.ResetPasswordAsync(userDb, token, "Password123$567");

            return Json(new { res });
        }
    }
}
