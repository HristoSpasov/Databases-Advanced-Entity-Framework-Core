namespace TeamBuilder.App.ModelDto
{
    using System.ComponentModel.DataAnnotations;
    using TeamBuilder.Models.Validation.RegisterUser;

    public class RegisterUserDto
    {
        [Required]
        [Username]
        public string Username { get; set; }

        [Required]
        [Password(ContainsDigit = true, ContainsUppercase = true)]
        public string Password { get; set; }

        [MaxLength(25)]
        public string FirstName { get; set; }

        [MaxLength(25)]
        public string LastName { get; set; }

        [Age]
        public int Age { get; set; }

        [Gender]
        public string Gender { get; set; }

        public string RepeatPassword { get; set; }
    }
}