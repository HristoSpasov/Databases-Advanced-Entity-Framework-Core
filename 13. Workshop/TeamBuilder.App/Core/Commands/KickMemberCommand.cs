namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class KickMemberCommand : ICommand
    {
        private const int ExpectedArgs = 2;
        private const string Success = "User {0} was kicked from {1}!";
        private const string TeamNotFound = "Team {0} not found!";
        private const string UserNotFound = "User {0} not found!";
        private const string UserNotMember = "User {0} is not a member in {1}!";
        private const string UserNotCreator = "Not allowed!";
        private const string UserIsTeamCrator = "Command not allowed. Use DisbandTeam instead.";

        private readonly ITeamService teamService;
        private readonly IUserService userService;

        public KickMemberCommand(ITeamService teamService, IUserService userService)
        {
            this.teamService = teamService;
            this.userService = userService;
        }

        public string UserIsTeamCreator { get; private set; }

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
                string userName = args[1];

                Team t = this.teamService.ByName(teamName);

                if (t == null)
                {
                    throw new ArgumentException(string.Format(TeamNotFound, teamName));
                }

                User u = this.userService.ByUserName(userName);

                if (u == null)
                {
                    throw new ArgumentException(string.Format(UserNotFound, userName));
                }

                if (!u.Invitations.Any(us => us.InvitedUser.Id == u.Id && us.TeamId == t.Id))
                {
                    throw new ArgumentException(string.Format(UserNotMember, userName, teamName));
                }

                if (UserSession.LoggedInUser == null)
                {
                    throw new InvalidOperationException(Constants.UserIsNotLoggedIn);
                }

                if (UserSession.LoggedInUser.Username != t.Creator.Username)
                {
                    throw new InvalidOperationException(string.Format(UserNotCreator));
                }

                if (u.Username == UserSession.LoggedInUser.Username)
                {
                    throw new InvalidOperationException(this.UserIsTeamCreator);
                }

                // Kik member
                this.teamService.KickMember(teamName, u);
                result = string.Format(Success, userName, teamName);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}