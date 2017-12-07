namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using TeamBuilder.Models.Enums;

    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<UserTeam> UserTeams { get; set; }

        public ICollection<Team> Teams { get; set; }

        public ICollection<Invitation> Invitations { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}