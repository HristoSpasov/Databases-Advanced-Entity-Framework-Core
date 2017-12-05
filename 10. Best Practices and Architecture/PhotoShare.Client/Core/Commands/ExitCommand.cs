namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;

    public class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            try
            {
                this.ValidateArgs(args);
            }
            catch (InvalidOperationException ioe)
            {
                return ioe.Message;
            }

            return "Good Bye!";
        }

        private void ValidateArgs(params string[] args)
        {
            if (args.Length != 0)
            {
                string cmdName = new string(this.GetType().Name.Take(this.GetType().Name.Length - "Command".Length).ToArray());

                throw new InvalidOperationException($"Command <{cmdName}> not valid!");
            }
        }
    }
}