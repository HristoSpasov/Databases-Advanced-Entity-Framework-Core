namespace TeamBuilder.App.ModelDto.Validation.CreateEvent
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    internal class DateTimeAttribute : ValidationAttribute
    {
        private const string DateFormat = @"dd/MM/yyyy HH:mm";
        private const string DefaultErrorMessage = @"Please insert the dates in format: [dd/MM/yyyy HH:mm]!";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string stringDate = value.ToString();

            if (!DateTime.TryParseExact(stringDate, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDateTime))
            {
                return new ValidationResult(DefaultErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}