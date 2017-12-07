namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class InviteToTeamCommand : ICommand
    {
        private const int ExpectedArgs = 2;
        private const string Success = "Team {0} invited {1}!";
        private const string InviteAlreadySent = "Invite is already sent!";
        private const string NotAllowed = "Not allowed!";
        private const string TeamOrUserNotExisting = "Team or user does not exist!";

        private readonly IInvitationService invitationService;
        private readonly ITeamService teamService;
        private readonly IUserService userService;

        public InviteToTeamCommand(IInvitationService invitationService, ITeamService teamService, IUserService userService)
        {
            this.invitationService = invitationService;
            this.teamService = teamService;
            this.userService = userService;
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
                string userName = args[1];

                // Check if either user or tem is not exesting
                Team team = this.teamService.ByName(teamName);
                User user = this.userService.ByUserName(userName);

                if (team == null || user == null)
                {
                    throw new ArgumentException(TeamOrUserNotExisting);
                }

                if (UserSession.LoggedInUser == null)
                {
                    throw new InvalidCastException(Constants.UserIsNotLoggedIn);
                }

                Invitation invitationExists = this.invitationService.GetInvitation(user.Id, team.Id);

                if (invitationExists != null)
                {
                    throw new InvalidOperationException(InviteAlreadySent);
                }

                // If user is team creator => add him directly
                if (team.Creator.Username == user.Username)
                {
                    Invitation invitation = new Invitation()
                    {
                        InvitedUserId = user.Id,
                        TeamId = team.Id,
                        IsActive = false
                    };

                    this.invitationService.InviteToTeam(invitation);
                }
                else
                {
                    Invitation invitation = new Invitation()
                    {
                        InvitedUserId = user.Id,
                        TeamId = team.Id,
                        IsActive = true
                    };

                    this.invitationService.InviteToTeam(invitation);
                }

                result = string.Format(Success, teamName, userName);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}