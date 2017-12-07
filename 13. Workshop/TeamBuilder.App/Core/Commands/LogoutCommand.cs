namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;

    public class LogoutCommand : ICommand
    {
        private const int ExpectedArgs = 0;
        private const string Success = "User {0} successfully logged out!";

        public string Execute(params string[] args)
        {
            string result = default(string);

            try
            {
                if (!Validate.ArgsCountValidate(ExpectedArgs, args))
                {
                    throw new FormatException(Constants.FormatException);
                }

                if (UserSession.LoggedInUser == null)
                {
                    throw new InvalidOperationException(Constants.UserIsNotLoggedIn);
                }

                // Logout
                string username = UserSession.LoggedInUser.Username;
                UserSession.LoggedInUser = null;

                result = string.Format(Success, username);
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}