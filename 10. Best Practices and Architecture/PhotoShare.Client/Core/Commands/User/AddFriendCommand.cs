namespace PhotoShare.Client.Core.Commands.User
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Services.Interfaces;

    public class AddFriendCommand : ICommand
    {
        private readonly IUserService userService;

        public AddFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            string result = default(string);

            try
            {
                // Check if user is logged
                if (Session.CurrentUser == null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                this.ValidateArgs(data);

                string firstUser = data[0];
                string secondUser = data[1];

                // User can add friends only to himself
                if (firstUser != Session.CurrentUser)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                this.Validate(firstUser, secondUser);

                this.userService.AddFriend(firstUser, secondUser);

                result = $"Friend {secondUser} added to {firstUser}!";
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

        private void Validate(string firstUser, string secondUser)
        {
            if (!this.userService.UserExists(firstUser))
            {
                throw new ArgumentException($"User {firstUser} not found!");
            }

            if (!this.userService.UserExists(secondUser))
            {
                throw new ArgumentException($"User {secondUser} not found!");
            }

            if (this.userService.HasFriend(firstUser, secondUser))
            {
                throw new InvalidOperationException($"{secondUser} is already a friend to {firstUser}");
            }
        }
    }
}