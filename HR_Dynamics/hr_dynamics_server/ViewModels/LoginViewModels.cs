using System.ComponentModel.DataAnnotations;

namespace hr_dynamics_server.Models
{
    public class LoginViewModel
    {
        [Required, MaxLength(256)]
        public string? Username { get; set; }
        [Required, MaxLength(256)]
        public string? Password { get; set; }
    }
}
