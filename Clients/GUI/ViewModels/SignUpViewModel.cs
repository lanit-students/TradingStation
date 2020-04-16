﻿using System;
using System.ComponentModel.DataAnnotations;
using GUI.CustomValidationAttributes;

namespace GUI.ViewModels
{
    /// <summary>
    /// Model for user input in SignUp page
    /// </summary>
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(32, ErrorMessage = "Name is too long.")]
        [NameValidation("Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(32, ErrorMessage = "Surname is too long.")]
        [NameValidation("surname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [BirthdayAttribute]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public byte[] Avatar { get; set; }
        public string AvatarExtension { get; set; }
        public SignUpViewModel()
        {
            Birthday = DateTime.Today.AddYears(-18);
        }
    }
}
