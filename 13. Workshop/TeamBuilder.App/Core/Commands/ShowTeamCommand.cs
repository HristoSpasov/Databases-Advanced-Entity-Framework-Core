namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class ShowTeamCommand : ICommand
    {
        private const string TeamNotFound = "Team {0} not found!";
        private const int ExpectedArgs = 1;
        private readonly ITeamService teamService;

        public ShowTeamCommand(ITeamService teamService)
        {
            this.teamService = teamService;
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

                string teamName = args[0];

                Team team = this.teamService.ByName(teamName);

                if (team == null)
                {
                    throw new ArgumentException(string.Format(TeamNotFound, teamName));
                }

                // Get team status
                string[] teamMembers = this.teamService
                                           .GetMembers(teamName)
                                           .Select(u => "--" + u.Username)
                                           .ToArray();

                result = $"{team.Name} {team.Acronym}{Environment.NewLine}" +
                         $"Members:";

                if (teamMembers.Any())
                {
                    result += $"{Environment.NewLine}" +
                         $"{string.Join(Environment.NewLine, teamMembers)}";
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result.Trim();
        }
    }
}