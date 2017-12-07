namespace TeamBuilder.App.ModelDto
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TeamBuilder.App.ModelDto.Validation.CreateEvent;

    internal class CreateEventDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [DateTime]
        public string StartDate { get; set; }

        [DateTime]
        public string EndDate { get; set; }
    }
}