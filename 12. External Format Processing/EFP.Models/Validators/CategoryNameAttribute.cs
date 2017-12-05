namespace EFP.Models.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    internal class CategoryNameAttribute : ValidationAttribute
    {
        private int minLength;
        private int maxLength;

        public CategoryNameAttribute(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        public override bool IsValid(object value)
        {
            string name = value.ToString();

            if (name.Length < this.minLength || name.Length > this.maxLength)
            {
                return false;
            }

            return true;
        }
    }
}