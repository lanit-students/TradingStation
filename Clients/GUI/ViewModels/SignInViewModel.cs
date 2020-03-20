using System.ComponentModel.DataAnnotations;

namespace GUI.ViewModels
{
    // Model for user input in SignIn page
    public class SignInViewModel
    {
        [Required (ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required (ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
