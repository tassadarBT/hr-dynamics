using hr_dynamics_server.Data;
using hr_dynamics_server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace hr_dynamics_server.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager, ILogger<HomeController> logger)
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
