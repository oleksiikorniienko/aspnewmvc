using System.ComponentModel.DataAnnotations;

namespace aspnet2.Models
{
    public class LoginView
    {
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
