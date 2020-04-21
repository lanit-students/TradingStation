using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GUI.CustomValidationAttributes
{
    public class NameValidation : ValidationAttribute
    {
        private string name;

        public NameValidation(string name)
        {
            this.name = name;
        }

        public string GetErrorMessageNoNumbers() =>
            $"Not allow number in {name}";

        public string GetErrorMessageTooShort() =>
            $"{name} is too short";

        public string GetErrorMessage() =>
            $"The first character must be a capital letter, the rest are small in {name}";

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

            if (!char.IsUpper(name[0]) || !name.Substring(1).All(char.IsLower))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}