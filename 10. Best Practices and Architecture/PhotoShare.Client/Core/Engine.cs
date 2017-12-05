namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;
    using System.Text;
    using Microsoft.Extensions.DependencyInjection;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Services.Interfaces;

    public class Engine : IEngine
    {
        private static bool isRunning = true;
        private readonly IServiceProvider provider;
        private StringBuilder sb = new StringBuilder();

        public Engine(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public void Run()
        {
            this.IntializeDb();

            while (isRunning)
            {
                Console.Write("Enter input command > ");
                string input = Console.ReadLine().Trim();
                string[] data = input.Split(' ');

                string cmdName = data.First();
                string[] cmdArgs = data.Skip(1).ToArray();

                if (cmdName == "Exit" && cmdArgs.Length == 0)
                {
                    isRunning = false;
                }

                string result = default(string);

                try
                {
                    ICommandFactory commandFactory = this.provider.GetService<ICommandFactory>();
                    ICommand command = commandFactory.GetCommand(this.provider, cmdName);
                    result = command.Execute(cmdArgs);
                }
                catch (InvalidOperationException ioe)
                {
                    result = ioe.Message;
                }

                this.sb.AppendLine(result);
            }

            Console.WriteLine(this.sb.ToString().Trim());
        }

        private void IntializeDb()
        {
            Console.WriteLine("Initializing database. Please wait...");
            IDatabaseInitializer dbInitializer = this.provider.GetService<IDatabaseInitializer>();
            dbInitializer.InitializeDatabase();
            Console.WriteLine("Database sucessfully initialized.");
        }
    }
}