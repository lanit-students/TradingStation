using System;
using System.ComponentModel.DataAnnotations;

namespace GUI.ViewModels
{
    /// <summary>
    /// Model for user input in SignUp page
    /// </summary>
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(32, ErrorMessage = "LastName is too long.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        public SignUpViewModel()
        {
            Birthday = DateTime.Today.AddYears(-18);
        }
    }
}
