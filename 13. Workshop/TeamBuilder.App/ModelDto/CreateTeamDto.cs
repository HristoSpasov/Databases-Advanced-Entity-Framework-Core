namespace TeamBuilder.App.ModelDto
{
    using System.ComponentModel.DataAnnotations;
    using TeamBuilder.Models.Validation;

    public class CreateTeamDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Acronym]
        public string Acronym { get; set; }

        [MaxLength(32)]
        public string Description { get; set; }
    }
}