namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.ModelDto;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class CreateTeamCommand : ICommand
    {
        private const string Success = "Team {0} successfully created!";
        private const string TeamExists = "Team {0} exists!";
        private readonly ITeamService teamService;
        private int[] expectedArgs = new[] { 2, 3 };

        public CreateTeamCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(params string[] args)
        {
            string result = default(string);

            try
            {
                if (!Validate.ArgsCountValidate(this.expectedArgs[0], args) && !Validate.ArgsCountValidate(this.expectedArgs[1], args))
                {
                    throw new FormatException(Constants.FormatException);
                }

                string name = args[0];
                string acronym = args[1];
                string description = default(string);
                if (args.Length == 3)
                {
                    description = args[2];
                }

                // Check if team exists
                if (this.teamService.TeamExists(name))
                {
                    throw new ArgumentException(string.Format(TeamExists, name));
                }

                // Create Do and validate properties
                CreateTeamDto teamDto = new CreateTeamDto
                {
                    Name = name,
                    Acronym = acronym,
                    Description = description
                };

                List<ValidationResult> validationResult = Validate.EntityValidator(teamDto);

                if (validationResult != null)
                {
                    result = validationResult.First().ErrorMessage;
                }

                // Chack if there is any logged in user
                if (UserSession.LoggedInUser == null)
                {
                    throw new InvalidOperationException(Constants.UserIsNotLoggedIn);
                }

                // Map to model
                Team newTeam = Mapper.Map<Team>(teamDto);
                newTeam.Creator = UserSession.LoggedInUser;

                this.teamService.CreateTeam(newTeam);

                result = string.Format(Success, newTeam.Name);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}