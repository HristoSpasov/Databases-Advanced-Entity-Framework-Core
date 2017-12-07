namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class DisbandCommand : ICommand
    {
        private const int ExpectedArgs = 1;
        private const string Success = "{0} has disbanded!";
        private const string TeamNotFound = "Team {0} not found!";
        private const string CurrentUserNotCrator = "Not allowed!";
        private readonly ITeamService teamService;

        public DisbandCommand(ITeamService teamService)
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
                    throw new ArgumentException(string.Format(TeamNotFound), teamName);
                }

                if (UserSession.LoggedInUser == null)
                {
                    throw new InvalidOperationException(Constants.UserIsNotLoggedIn);
                }

                if (UserSession.LoggedInUser.Username != team.Creator.Username)
                {
                    throw new InvalidOperationException(CurrentUserNotCrator);
                }

                // Disband team
                this.teamService.Disband(teamName);
                result = string.Format(Success, teamName);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}