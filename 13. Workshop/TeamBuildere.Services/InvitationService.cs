namespace TeamBuildere.Services
{
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    public class InvitationService : IInvitationService
    {
        private readonly TeamBuilderContext context;

        public InvitationService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public Invitation AcceptInvite(Invitation toAccept)
        {
            Invitation toUpdate = this.context
                                      .Invitations
                                      .SingleOrDefault(iId => iId.Id == toAccept.Id);

            toUpdate.IsActive = false;
            this.context.SaveChanges();

            return toUpdate;
        }

        public Invitation DeclineInvite(Invitation toDecline)
        {
            Invitation toUpdate = this.context
                                      .Invitations
                                      .SingleOrDefault(iId => iId.Id == toDecline.Id);

            this.context.Invitations.Remove(toUpdate);
            this.context.SaveChanges();

            return null;
        }

        public Invitation GetInvitation(int userId, int teamId)
        {
            return this.context
                       .Invitations
                       .Where(i => i.InvitedUserId == userId && i.TeamId == teamId)
                       .SingleOrDefault();
        }

        public Invitation InviteToTeam(Invitation newInvitation)
        {
            this.context.Invitations.Add(newInvitation);
            this.context.SaveChanges();

            return newInvitation;
        }
    }
}