namespace TeamBuildere.Services.Contracts
{
    using TeamBuilder.Models;

    public interface IEventService
    {
        Event ById(int id);

        Event ByName(string name);

        Event CreateEvent(Event newEvent);

        void AddTeamTo(string eventName, Team newTeam);

        bool TeamIsAddedToEvent(string teamName, string eventName);

        Team[] GetEventTeams(string eventName);
    }
}