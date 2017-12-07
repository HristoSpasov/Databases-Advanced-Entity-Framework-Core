namespace TeamBuilder.Models.Validation
{
    using System.ComponentModel.DataAnnotations;

    internal class AcronymAttribute : ValidationAttribute
    {
        private const string InvalidAcronymMessage = "Acronym {0} not valid!";
        private const int MinLength = 3;
        private const int MaxLength = 3;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string acronym = value.ToString();

            if (acronym.Length < MinLength || acronym.Length > MaxLength)
            {
                return new ValidationResult(string.Format(InvalidAcronymMessage, value.ToString()));
            }

            return ValidationResult.Success;
        }
    }
}