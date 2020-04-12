using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GUI.ViewModels;

namespace GUI.CustomValidationAttributes
{
    public class BirthdayAttribute : ValidationAttribute
    {
        public string GetErrorMessageTooYoung() =>
            $"You are to young.";

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var birthday = (DateTime) value;
            
            if (birthday.AddYears(18) > DateTime.Now)
            {
                return new ValidationResult(GetErrorMessageTooYoung());
            }

            return ValidationResult.Success;
        }
    }
}
