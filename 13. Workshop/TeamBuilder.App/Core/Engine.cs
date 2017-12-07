namespace TeamBuilder.App.Core
{
    using System;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using TeamBuilder.App.Contracts;
    using TeamBuildere.Services.Contracts;

    internal class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IOutputStore outputStore;
        private readonly IServiceProvider serviceProvider;
        private bool isRunning;

        public Engine(IReader reader, IWriter writer, IOutputStore outputStore, IServiceProvider serviceProvider)
        {
            this.isRunning = true;
            this.reader = reader;
            this.writer = writer;
            this.outputStore = outputStore;
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            this.InitializeDatabase();

            while (this.isRunning)
            {
                this.writer.Write("Enter command > ");
                string input = this.reader.ReadLine().Trim();

                string commandName = input.Split().First();
                string[] cmdArgs = input.Split().Skip(1).ToArray();

                ICommandFactory commandFactory = this.serviceProvider.GetService<ICommandFactory>();
                ICommand cmd = commandFactory.GetCommand(commandName);
                string result = cmd.Execute(cmdArgs);

                this.outputStore.AppendLine(result);

                if (commandName == "Exit")
                {
                    this.isRunning = false;
                }
            }

            this.writer.WriteLine(this.outputStore.GetOutput());
        }

        private void InitializeDatabase()
        {
            Console.WriteLine("Initializing database. Please wait...");
            IDatabaseInitializerService databaseInitializerService = this.serviceProvider.GetService<IDatabaseInitializerService>();
            databaseInitializerService.DatabaseInitialize();
            Console.WriteLine("Database successfully initialized.");
        }
    }
}