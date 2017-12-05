namespace PhotoShare.Client.Core.Commands.User
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Services.Interfaces;

    public class ListFriendsCommand : ICommand
    {
        private readonly IUserService userService;

        public ListFriendsCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // PrintFriendsList <username>
        public string Execute(params string[] data)
        {
            string result = default(string);

            try
            {
                this.ValidateArgs(data);

                string username = data.First();

                if (!this.userService.UserExists(username))
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                result = this.userService.GetFriends(username);
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
            if (args.Length != 1)
            {
                string cmdName = new string(this.GetType().Name.Take(this.GetType().Name.Length - "Command".Length).ToArray());

                throw new InvalidOperationException($"Command <{cmdName}> not valid!");
            }
        }
    }
}