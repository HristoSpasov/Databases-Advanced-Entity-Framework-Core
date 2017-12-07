namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class ShowEventCommand : ICommand
    {
        private const int ExpectedArgs = 1;
        private const string EventDoesNotExist = @"Event {0} not found!";
        private readonly IEventService eventService;

        public ShowEventCommand(IEventService eventService)
        {
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

                Event toShow = this.eventService.ByName(eventName);

                if (toShow == null)
                {
                    throw new ArgumentException(string.Format(EventDoesNotExist, eventName));
                }

                string[] eventTeams = this.eventService
                                          .GetEventTeams(eventName)
                                          .Select(e => "-" + e.Name)
                                          .ToArray();

                result = $"{toShow.Name} {toShow.StartDate.ToString(Constants.DateTimeFormat)} {toShow.EndDate.ToString(Constants.DateTimeFormat)}{Environment.NewLine}" +
                         $"{toShow.Description}{Environment.NewLine}" +
                         $"Teams:";

                if (eventTeams.Any())
                {
                    result += $"{Environment.NewLine}{string.Join(Environment.NewLine, eventTeams)}";
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}