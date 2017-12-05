namespace PhotoShare.Client.Core.Commands.User
{
    using System;
    using System.Linq;
    using Models;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Services.Interfaces;

    public class RegisterUserCommand : ICommand
    {
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(params string[] data)
        {
            string result = default(string);

            try
            {
                // Check if user is logged
                if (Session.CurrentUser != null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                this.ValidateArgs(data);

                string username = data[0];
                string password = data[1];
                string repeatPassword = data[2];
                string email = data[3];

                if (this.userService.ByUsername(username) != null)
                {
                    throw new InvalidOperationException($"Username {username} is already taken!");
                }

                if (password != repeatPassword)
                {
                    throw new ArgumentException("Passwords do not match!");
                }

                User user = new User
                {
                    Username = username,
                    Password = password,
                    Email = email,
                    IsDeleted = false,
                    RegisteredOn = DateTime.Now,
                    LastTimeLoggedIn = DateTime.Now
                };

                this.userService.RegisterUser(user);

                result = $"User {username} was registered successfully!";
            }
            catch (ArgumentException aex)
            {
                result = aex.Message;
            }
            catch (InvalidOperationException ioe)
            {
                result = ioe.Message;
            }

            return result;
        }

        private void ValidateArgs(params string[] args)
        {
            if (args.Length != 4)
            {
                string cmdName = new string(this.GetType().Name.Take(this.GetType().Name.Length - "Command".Length).ToArray());

                throw new InvalidOperationException($"Command <{cmdName}> not valid!");
            }
        }
    }
}