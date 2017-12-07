namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;
    using TeamBuildere.Services.Contracts;

    internal class LoginCommand : ICommand
    {
        private const int ExpextedArgs = 2;
        private const string Success = "User {0} successfully logged in!";
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] args)
        {
            string result = default(string);

            try
            {
                if (!Validate.ArgsCountValidate(ExpextedArgs, args))
                {
                    throw new FormatException(Constants.FormatException);
                }

                string username = args[0];
                string password = args[1];

                if (!this.userService.UserExists(username))
                {
                    throw new ArgumentException(Constants.InvalidUserNameOrPasswordError);
                }

                User user = this.userService.ByUserName(username);

                if (user.IsDeleted == true || user.Password != password)
                {
                    throw new ArgumentException(Constants.InvalidUserNameOrPasswordError);
                }

                if (UserSession.LoggedInUser != null)
                {
                    throw new InvalidOperationException(Constants.LogOutFirstError);
                }

                // Login user
                UserSession.LoggedInUser = user;

                result = string.Format(Success, user.Username);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}