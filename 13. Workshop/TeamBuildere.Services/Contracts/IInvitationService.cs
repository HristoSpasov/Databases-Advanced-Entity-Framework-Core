namespace TeamBuildere.Services.Contracts
{
    using TeamBuilder.Models;

    public interface IInvitationService
    {
        Invitation InviteToTeam(Invitation newInvitation);

        Invitation AcceptInvite(Invitation toAccept);

        Invitation DeclineInvite(Invitation toDecline);

        Invitation GetInvitation(int userId, int teamId);
    }
}