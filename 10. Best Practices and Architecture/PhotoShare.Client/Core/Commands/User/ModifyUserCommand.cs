namespace PhotoShare.Client.Core.Commands.User
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Services.Interfaces;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;
        private readonly ITownService townService;

        public ModifyUserCommand(IUserService userService, ITownService townService)
        {
            this.userService = userService;
            this.townService = townService;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
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
                string property = data[1];
                string newValue = data.Last();

                // User can edit only his personal data
                if (username != Session.CurrentUser)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                if (!this.userService.UserExists(username))
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                switch (property)
                {
                    case "Password":
                        this.PasswordChangeRoutine(username, newValue);
                        break;

                    case "BornTown":
                        this.BornTownChangeRoutine(username, newValue);
                        break;

                    case "CurrentTown":
                        this.CurrentTownChangeRoutine(username, newValue);
                        break;

                    default: throw new ArgumentException($"Property {property} not supported!");
                }

                result = $"User {username} {property} is {newValue}.";
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
            if (args.Length != 3)
            {
                string cmdName = new string(this.GetType().Name.Take(this.GetType().Name.Length - "Command".Length).ToArray());

                throw new InvalidOperationException($"Command <{cmdName}> not valid!");
            }
        }

        private void CurrentTownChangeRoutine(string username, string currentTown)
        {
            if (!this.townService.TownExists(currentTown))
            {
                throw new ArgumentException($"Value {currentTown} not valid.{Environment.NewLine}Town {currentTown} not found!");
            }

            this.userService.ModifyCurrentTown(username, currentTown);
        }

        private void BornTownChangeRoutine(string username, string bornTown)
        {
            if (!this.townService.TownExists(bornTown))
            {
                throw new ArgumentException($"Value {bornTown} not valid.{Environment.NewLine}Town {bornTown} not found!");
            }

            this.userService.ModifyBornTown(username, bornTown);
        }

        private void PasswordChangeRoutine(string username, string password)
        {
            if (!password.Any(char.IsLower) || !password.Any(char.IsDigit))
            {
                throw new ArgumentException($"Value {password} not valid.{Environment.NewLine}Invalid Password");
            }

            this.userService.ModifyPassword(username, password);
        }
    }
}