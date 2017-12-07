namespace TeamBuilder.App.Utilities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    internal static class Validate
    {
        public static bool ArgsCountValidate(int expectedLength, string[] args) => args.Length == expectedLength;

        public static List<ValidationResult> EntityValidator(object obj)
        {
            var result = new List<ValidationResult>();
            var context = new ValidationContext(obj);
            var isValid = Validator.TryValidateObject(obj, context, result, true);

            if (!isValid)
            {
                return result;
            }

            return null;
        }
    }
}