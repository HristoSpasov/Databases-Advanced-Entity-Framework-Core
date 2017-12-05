namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Services.Interfaces;

    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;

        public UserService(PhotoShareContext context)
        {
            this.context = context;
        }

        public User ByUsername(string username)
        {
            return this.context.Users.SingleOrDefault(u => u.Username == username);
        }

        public User RegisterUser(User user)
        {
            this.context.Users.Add(user);

            this.context.SaveChanges();

            return user;
        }

        public User DeleteUser(string username)
        {
            User userToDelete = this.context.Users.SingleOrDefault(un => un.Username == username);

            if (userToDelete != null)
            {
                userToDelete.IsDeleted = true;
                this.context.SaveChanges();
            }

            return userToDelete;
        }

        public bool UserExists(string username)
        {
            return this.context
                .Users
                .Where(u => u.Username == username)
                .Count() == 1;
        }

        public User ModifyPassword(string username, string password)
        {
            User toUpdate = this.ByUsername(username);

            toUpdate.Password = password;

            this.context.SaveChanges();

            return toUpdate;
        }

        public User ModifyBornTown(string username, string bornTown)
        {
            User toUpdate = this.ByUsername(username);
            int townId = this.context.Towns.Single(n => n.Name == bornTown).Id;

            toUpdate.BornTownId = townId;

            this.context.SaveChanges();

            return toUpdate;
        }

        public User ModifyCurrentTown(string username, string currentTown)
        {
            User toUpdate = this.ByUsername(username);
            int townId = this.context.Towns.Single(n => n.Name == currentTown).Id;

            toUpdate.CurrentTownId = townId;

            this.context.SaveChanges();

            return toUpdate;
        }

        public bool HasFriend(string username, string friendName)
        {
            User user = this.ByUsername(username);
            User friend = this.ByUsername(friendName);

            return user.FriendsAdded.Any(u => u.Friend == friend);
        }

        public string GetFriends(string username)
        {
            int userId = this.ByUsername(username).Id;

            string[] friends = this.context
                                   .Friendships.Where(i => i.UserId == userId)
                                   .Select(n => "-" + n.Friend.Username)
                                   .ToArray();

            return friends.Any() ? "Friends" + Environment.NewLine + string.Join(Environment.NewLine, friends) : "No friends for this user. :(";
        }

        public void AddFriend(string username, string friendUser)
        {
            int userId = this.ByUsername(username).Id;
            int friendId = this.ByUsername(friendUser).Id;

            Friendship friendship = new Friendship()
            {
                UserId = userId,
                FriendId = friendId
            };

            this.context.Friendships.Add(friendship);

            this.context.SaveChanges();
        }

        public bool HasRequest(string username, string friendRequestUser)
        {
            User user = this.ByUsername(username);
            User requesterUser = this.ByUsername(friendRequestUser);

            int requesterId = requesterUser.Id;
            int userToAcceptId = user.Id;

            return this.context.Friendships.Any(i => i.UserId == requesterId && i.FriendId == userToAcceptId);
        }
    }
}