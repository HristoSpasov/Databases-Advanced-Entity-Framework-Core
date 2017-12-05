namespace PhotoShare.Client.Core.Commands.User
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Services.Interfaces;

    public class AcceptFriendCommand : ICommand
    {
        private readonly IUserService userService;

        public AcceptFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AcceptFriend <username1> <username2>
        public string Execute(params string[] data)
        {
            string result = default(string);

            try
            {
                this.ValidateArgs(data);

                string username = data[0];
                string friendRequestUser = data[1];

                this.Validate(username, friendRequestUser);

                this.userService.AddFriend(username, friendRequestUser);

                result = $"{username} accepted {friendRequestUser} as a friend";
            }
            catch (ArgumentException ae)
            {
                result = ae.Message;
            }
            catch (InvalidOperationException ioe)
            {
                result = ioe.Message;
            }

            return result;
        }

        private void ValidateArgs(params string[] args)
        {
            if (args.Length != 2)
            {
                string cmdName = new string(this.GetType().Name.Take(this.GetType().Name.Length - "Command".Length).ToArray());

                throw new InvalidOperationException($"Command <{cmdName}> not valid!");
            }
        }

        private void Validate(string username, string friendRequestUser)
        {
            if (!this.userService.UserExists(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (!this.userService.UserExists(friendRequestUser))
            {
                throw new ArgumentException($"User {friendRequestUser} not found!");
            }

            if (this.userService.HasFriend(username, friendRequestUser))
            {
                throw new InvalidOperationException($"{username} is already a friend to {friendRequestUser}");
            }

            if (!this.userService.HasRequest(username, friendRequestUser))
            {
                throw new InvalidOperationException($"{username} has not added {friendRequestUser} as a friend");
            }
        }
    }
}