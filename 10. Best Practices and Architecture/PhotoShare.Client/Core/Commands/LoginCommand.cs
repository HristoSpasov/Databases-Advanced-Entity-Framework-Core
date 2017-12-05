namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Services.Interfaces;

    public class LoginCommand : ICommand
    {
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] cmdArgs)
        {
            string result = default(string);

            try
            {
                // Check if user is logged
                if (Session.CurrentUser != null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                this.ValidateArgs(cmdArgs);

                string username = cmdArgs.First();
                string password = cmdArgs.Last();

                this.ValidateLogInCredentials(username, password);

                Session.CurrentUser = username;

                result = $"User {username} successfully logged in!";
            }
            catch (ArgumentException ae)
            {
                result = ae.Message;
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

        private void ValidateLogInCredentials(string username, string password)
        {
            if (!this.userService.UserExists(username) || !(this.userService.ByUsername(username).Password == password))
            {
                throw new ArgumentException("Invalid username or password!");
            }

            if (Session.CurrentUser == this.userService.ByUsername(username).Username)
            {
                throw new ArgumentException("You should logout first!");
            }
        }
    }
}