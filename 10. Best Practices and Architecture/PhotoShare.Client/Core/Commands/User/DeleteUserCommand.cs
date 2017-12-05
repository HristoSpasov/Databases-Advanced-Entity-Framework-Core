namespace PhotoShare.Client.Core.Commands.User
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Models;
    using PhotoShare.Services.Interfaces;

    public class DeleteUserCommand : ICommand
    {
        private readonly IUserService userService;

        public DeleteUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // DeleteUser <username>
        public string Execute(params string[] data)
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

                string username = data.First();

                if (Session.CurrentUser != username)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                User user = this.userService.ByUsername(username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (user.IsDeleted == true)
                {
                    throw new InvalidOperationException($"User {username} is already deleted!");
                }

                this.userService.DeleteUser(username);

                result = $"User {username} was deleted from the database!";
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