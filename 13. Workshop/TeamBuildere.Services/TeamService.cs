namespace TeamBuildere.Services
{
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    public class TeamService : ITeamService
    {
        private readonly TeamBuilderContext context;

        public TeamService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public Team ById(int id) => this.context.Teams.SingleOrDefault(uId => uId.Id == id);

        public Team ByName(string name) => this.context.Teams.SingleOrDefault(n => n.Name == name);

        public Team CreateTeam(Team newTeam)
        {
            this.context.Teams.Add(newTeam);
            this.context.SaveChanges();

            return newTeam;
        }

        public void Disband(string team)
        {
            Team toDisband = this.ByName(team);
            this.context.Teams.Remove(toDisband);
            this.context.SaveChanges();
        }

        public User[] GetMembers(string teamName)
        {
            return this.context
                       .Invitations
                       .Where(t => t.Team.Name == teamName && t.IsActive == false)
                       .Select(u => u.InvitedUser)
                       .ToArray();
        }

        public bool IsPartOfTeam(string teamName, string userName)
        {
            return this.context
                       .UserTeams
                       .Where(t => t.Team.Name == teamName)
                       .Any(u => u.User.Username == userName);
        }

        public void KickMember(string team, User toKick)
        {
            Team t = this.ByName(team);

            Invitation toRemove = this.context
                                    .Invitations
                                    .Where(i => i.TeamId == t.Id && i.InvitedUserId == toKick.Id)
                                    .SingleOrDefault();

            this.context.Invitations.Remove(toRemove);
            this.context.SaveChanges();
        }

        public Team ShowTeam(string teamName)
        {
            return this.ByName(teamName);
        }

        public bool TeamExists(string name) => this.ByName(name) != null;
    }
}