namespace TeamBuilder.Models.Validation.RegisterUser
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class PasswordAttribute : ValidationAttribute
    {
        private const string DefaultMessage = "Password {0} not valid!";
        private const int MinLength = 6;
        private const int MaxLength = 30;

        public bool ContainsDigit { get; set; }

        public bool ContainsUppercase { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = value.ToString();

            if (password.Length < MinLength || password.Length > MaxLength)
            {
                return new ValidationResult(string.Format(DefaultMessage, value.ToString()));
            }

            if (!password.Any(c => char.IsDigit(c)))
            {
                return new ValidationResult(string.Format(DefaultMessage, value.ToString()));
            }

            if (!password.Any(c => char.IsUpper(c)))
            {
                return new ValidationResult(string.Format(DefaultMessage, value.ToString()));
            }

            return ValidationResult.Success;
        }
    }
}