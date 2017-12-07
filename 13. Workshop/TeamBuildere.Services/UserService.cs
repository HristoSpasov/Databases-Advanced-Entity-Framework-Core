namespace TeamBuildere.Services
{
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    public class UserService : IUserService
    {
        private readonly TeamBuilderContext context;

        public UserService(TeamBuilderContext context)
        {
            this.context = context;
        }

        public User ById(int id) => this.context.Users.SingleOrDefault(uId => uId.Id == id);

        public User ByUserName(string username) => this.context.Users.SingleOrDefault(u => u.Username == username);

        public User Deleteuser(User toDelete)
        {
            User fromDatebase = this.context.Users.SingleOrDefault(uId => uId.Id == toDelete.Id);
            fromDatebase.IsDeleted = true;
            this.context.SaveChanges();

            return fromDatebase;
        }

        public User RegisterUser(User newUser)
        {
            this.context.Users.Add(newUser);
            this.context.SaveChanges();

            return newUser;
        }

        public bool UserExists(string username) => this.ByUserName(username) != null;
    }
}