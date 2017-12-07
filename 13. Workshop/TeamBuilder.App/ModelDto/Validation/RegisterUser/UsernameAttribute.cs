namespace TeamBuilder.Models.Validation.RegisterUser
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class UsernameAttribute : ValidationAttribute
    {
        private const string DefaultMessage = "Username {0} not valid!";
        private const int MinLength = 3;
        private const int MaxLength = 25;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string username = value.ToString();

            if (username.Length < MinLength || username.Length > MaxLength)
            {
                return new ValidationResult(string.Format(DefaultMessage, username));
            }

            return ValidationResult.Success;
        }
    }
}