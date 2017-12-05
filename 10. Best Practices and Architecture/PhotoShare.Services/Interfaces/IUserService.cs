namespace PhotoShare.Services.Interfaces
{
    using PhotoShare.Models;

    public interface IUserService
    {
        User ByUsername(string username);

        User RegisterUser(User user);

        User DeleteUser(string username);

        bool UserExists(string username);

        User ModifyPassword(string username, string password);

        User ModifyBornTown(string username, string bornTown);

        User ModifyCurrentTown(string username, string currentTown);

        bool HasFriend(string username, string friendName);

        string GetFriends(string username);

        void AddFriend(string user, string friendUser);

        bool HasRequest(string username, string friendRequestUser);
    }
}