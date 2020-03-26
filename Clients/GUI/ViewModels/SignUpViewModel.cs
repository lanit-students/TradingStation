using System.ComponentModel.DataAnnotations;

namespace GUI.GUIModels
{
    /// <summary>
    /// Model for user input in SignUp page
    /// </summary>
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
