namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;
    using TeamBuildere.Services.Contracts;

    internal class DeleteUserCommand : ICommand
    {
        private const int ExpectedArgs = 0;
        private const string Success = "User {0} was deleted successfully!";
        private readonly IUserService userservice;

        public DeleteUserCommand(IUserService userservice)
        {
            this.userservice = userservice;
        }

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

                this.userservice.Deleteuser(UserSession.LoggedInUser);
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