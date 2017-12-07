namespace TeamBuildere.Services.Contracts
{
    using TeamBuilder.Models;

    public interface ITeamService
    {
        Team ById(int id);

        Team ByName(string name);

        bool TeamExists(string name);

        Team CreateTeam(Team neweTeam);

        void KickMember(string team, User toKick);

        void Disband(string team);

        Team ShowTeam(string teamName);

        User[] GetMembers(string teamName);

        bool IsPartOfTeam(string teamName, string userName);
    }
}