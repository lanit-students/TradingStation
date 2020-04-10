using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GUI.ViewModels;

namespace GUI.CustomValidationAtributes
{
    public class NameValidation : ValidationAttribute
    {
        public string GetErrorMessageNoNumbers() =>
            $"Not allow number";
        public string GetErrorMessageTooShort() =>
            $"name too short";

        public string GetErrorMessage() =>
            $"The first character must be a capital letter, the rest are small";


        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var name = (String) value;

            if (!name.All(char.IsLetter))
            {
                return new ValidationResult(GetErrorMessageNoNumbers());
            }

            if (name.Length < 2)
            {
                return new ValidationResult(GetErrorMessageTooShort());
            }

            if ( !char.IsUpper(name[0]) || !name.Substring(1).All(char.IsLower))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
