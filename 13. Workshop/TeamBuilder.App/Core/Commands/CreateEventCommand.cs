namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.ModelDto;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class CreateEventCommand : ICommand
    {
        private const string StartDateIsAfterEndDateErrorMessage = "Start date should be before end date.";
        private const string MessageSuccess = @"Event {0} was created successfully!";
        private const int ExpectedArgs = 6; // Due to splittind, actually it is 4
        private readonly IEventService eventService;

        public CreateEventCommand(IEventService eventService)
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

                string name = args[0];
                string description = args[1];
                string startDate = args[2] + " " + args[3];
                string endDate = args[4] + " " + args[5];

                CreateEventDto newEventDto = new CreateEventDto
                {
                    Name = name,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate
                };

                // Validate
                List<ValidationResult> validationResult = Validate.EntityValidator(newEventDto);

                if (validationResult != null)
                {
                    result = validationResult.First().ErrorMessage;
                    return result;
                }

                // Create event and make additional validations
                Event newEvent = new Event
                {
                    Name = newEventDto.Name,
                    Description = newEventDto.Description,
                    StartDate = DateTime.ParseExact(newEventDto.StartDate, Constants.DateTimeFormat, CultureInfo.InvariantCulture),
                    EndDate = DateTime.ParseExact(newEventDto.EndDate, Constants.DateTimeFormat, CultureInfo.InvariantCulture),
                };

                if (newEvent.StartDate > newEvent.EndDate)
                {
                    throw new ArgumentException(StartDateIsAfterEndDateErrorMessage);
                }

                if (UserSession.LoggedInUser == null)
                {
                    throw new InvalidOperationException(Constants.UserIsNotLoggedIn);
                }

                // Add event to context
                newEvent.Creator = UserSession.LoggedInUser;
                this.eventService.CreateEvent(newEvent);

                // Print report message
                result = string.Format(MessageSuccess, newEvent.Name);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}