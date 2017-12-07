namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Utilities;

    internal class ExitCommand : ICommand
    {
        private const int ExpectedArgs = 0;

        public string Execute(params string[] args)
        {
            string result = default(string);

            try
            {
                if (!Validate.ArgsCountValidate(ExpectedArgs, args))
                {
                    throw new FormatException(Constants.FormatException);
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            return result;
        }
    }
}