namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class DeclineInviteCommand : ICommand
    {
        private const int ExpectedArgs = 1;
        private const string Success = "Invite from {0} declined.";
        private const string TeamDoesNotexists = "Team {0} not found!";
        private const string InviteNotFound = "Invite from {0} is not found!";
        private readonly IInvitationService invitationService;
        private readonly ITeamService teamService;

        public DeclineInviteCommand(IInvitationService invitationService, ITeamService teamService)
        {
            this.invitationService = invitationService;
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

                if (UserSession.LoggedInUser == null)
                {
                    throw new InvalidOperationException(Constants.UserIsNotLoggedIn);
                }

                Team team = this.teamService.ByName(teamName);

                if (team == null)
                {
                    throw new ArgumentException(string.Format(TeamDoesNotexists, teamName));
                }

                Invitation toDecline = this.invitationService.GetInvitation(UserSession.LoggedInUser.Id, team.Id);

                if (toDecline == null || !toDecline.IsActive)
                {
                    throw new ArgumentException(InviteNotFound, teamName);
                }

                // Accept invitation
                this.invitationService.DeclineInvite(toDecline);
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