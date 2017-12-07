namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class AddTeamToCommand : ICommand
    {
        private const int ExpectedArgs = 2;
        private const string TeamAlreadyAddedToEvent = "Cannot add same team twice!";
        private const string NotCreator = "Not allowed!";
        private const string TeamNotFound = "Team {0} not found!";
        private const string EventNotFound = "Event {0} not found!";
        private const string Success = "Team {0} added for {1}!";
        private readonly ITeamService teamService;
        private readonly IEventService eventService;

        public AddTeamToCommand(ITeamService teamService, IEventService eventService)
        {
            this.teamService = teamService;
            this.eventService = eventService;
        }

        public string Execute(params string[] args)
        {
            string result = default(string);

            try
            {
                if (!Validate.ArgsCountValidate(ExpectedArgs, args))
                {
                    throw new FormatException(Constants.FormatException);
                }

                string eventName = args[0];
                string teamName = args[1];

                Event ev = this.eventService.ByName(eventName);

                if (ev == null)
                {
                    throw new ArgumentException(string.Format(EventNotFound, eventName));
                }

                Team team = this.teamService.ByName(teamName);

                if (team == null)
                {
                    throw new ArgumentException(string.Format(TeamNotFound, teamName));
                }

                if (UserSession.LoggedInUser == null)
                {
                    throw new InvalidOperationException(Constants.UserIsNotLoggedIn);
                }

                if (UserSession.LoggedInUser.Username != ev.Creator.Username)
                {
                    throw new InvalidOperationException(NotCreator);
                }

                if (this.eventService.TeamIsAddedToEvent(teamName, eventName))
                {
                    throw new InvalidOperationException(TeamAlreadyAddedToEvent);
                }

                // Add team to event
                this.eventService.AddTeamTo(eventName, team);
                result = string.Format(Success, team.Name, ev.Name);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}