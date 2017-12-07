namespace TeamBuilder.Models.Validation.RegisterUser
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class AgeAttribute : ValidationAttribute
    {
        private const string DefaultMessage = "Age not valid!";
        private const int MinAge = 0;
        private const int MaxAge = int.MaxValue;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!long.TryParse(value.ToString(), out long age))
            {
                return new ValidationResult(DefaultMessage);
            }

            if (age < MinAge || age > MaxAge)
            {
                return new ValidationResult(DefaultMessage);
            }

            return ValidationResult.Success;
        }
    }
}