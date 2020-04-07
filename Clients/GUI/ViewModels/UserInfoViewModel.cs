using System;
using System.ComponentModel.DataAnnotations;

namespace GUI.ViewModels
{
    public class UserInfoViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string LastName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime Birthday { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
