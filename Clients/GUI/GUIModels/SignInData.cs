using System.ComponentModel.DataAnnotations;

namespace GUI.GUIModels
{
    public class SignInData
    {
        [Required (ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
