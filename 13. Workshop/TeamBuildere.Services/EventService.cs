namespace TeamBuildere.Services
{
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    public class EventService : IEventService
    {
        private readonly TeamBuilderContext context;

        public EventService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public void AddTeamTo(string eventName, Team newTeam)
        {
            Event ev = this.ByName(eventName);

            EventTeam newEventTeam = new EventTeam
            {
                EventId = ev.Id,
                TeamId = newTeam.Id,
            };

            this.context.EventTeams.Add(newEventTeam);
            this.context.SaveChanges();
        }

        public Event ById(int id) => this.context.Events.Where(eId => eId.Id == id).SingleOrDefault();

        public Event ByName(string name) => this.context.Events.Where(n => n.Name == name).OrderByDescending(sd => sd.StartDate).FirstOrDefault();

        public Event CreateEvent(Event newEvent)
        {
            this.context.Events.Add(newEvent);
            this.context.SaveChanges();

            return newEvent;
        }

        public Team[] GetEventTeams(string eventName)
        {
            return this.context
                       .EventTeams
                       .Where(t => t.Event.Name == eventName)
                       .Select(t => t.Team)
                       .ToArray();
        }

        public bool TeamIsAddedToEvent(string teamName, string eventName)
        {
            return
            this.context.EventTeams
                .Where(t => t.Event.Name == eventName && t.Team.Name == teamName)
                .SingleOrDefault() != null;
        }
    }
}