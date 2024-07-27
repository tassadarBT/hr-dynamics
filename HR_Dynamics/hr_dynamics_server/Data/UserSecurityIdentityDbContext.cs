using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace hr_dynamics_server.Data
{
    public class UserSecurityIdentityDbContext : IdentityDbContext
    {
        public UserSecurityIdentityDbContext(DbContextOptions<UserSecurityIdentityDbContext> options) : base(options)
        {
        }
    }
}
