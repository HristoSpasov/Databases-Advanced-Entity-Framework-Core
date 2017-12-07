namespace TeamBuilder.Models.Validation.RegisterUser
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TeamBuilder.Models.Enums;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class GenderAttribute : ValidationAttribute
    {
        private const string DefaultMessage = "Gender should be either “Male” or “Female”!";

        public bool ValueIsFromGenderEnum { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string gender = value.ToString();

            Gender parsedGender;
            if (!Enum.TryParse(gender, false, out parsedGender))
            {
                return new ValidationResult(DefaultMessage);
            }

            return ValidationResult.Success;
        }
    }
}