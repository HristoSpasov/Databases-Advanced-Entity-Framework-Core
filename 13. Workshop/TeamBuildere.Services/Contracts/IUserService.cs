namespace TeamBuildere.Services.Contracts
{
    using TeamBuilder.Models;

    public interface IUserService
    {
        User ById(int id);

        User ByUserName(string username);

        User Deleteuser(User toDelete);

        User RegisterUser(User newUser);

        bool UserExists(string username);
    }
}