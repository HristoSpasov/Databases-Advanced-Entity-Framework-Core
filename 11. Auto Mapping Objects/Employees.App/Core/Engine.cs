namespace Employees.App.Core
{
    using System;
    using System.Linq;
    using Employees.App.Contracts.Core;
    using Employees.App.Contracts.IO;
    using Employees.Services.Contracts;
    using Microsoft.Extensions.DependencyInjection;

    internal class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IOutputStore outputStore;
        private readonly IServiceProvider serviceProvider;
        private bool isRunning;

        public Engine(IReader reader, IWriter writer, IOutputStore outputStore, IServiceProvider serviceProvider)
        {
            this.reader = reader;
            this.writer = writer;
            this.outputStore = outputStore;
            this.serviceProvider = serviceProvider;
            this.isRunning = true;
        }

        public void Run()
        {
            this.InitializeDatabase();

            while (this.isRunning)
            {
                this.writer.Write("Enter command > ");

                string[] inputTokens = this.reader.ReadLine().Split();

                string cmdName = inputTokens.First();
                string[] cmdArgs = inputTokens.Skip(1).ToArray();

                if (cmdName == "Exit")
                {
                    this.isRunning = false;
                }

                ICommandFactory cmdFactory = this.serviceProvider.GetService<ICommandFactory>();
                ICommand command = cmdFactory.GetCommand(cmdName, this.serviceProvider);
                string result = command.Execute(cmdArgs);

                this.outputStore.AppendLine(result);
                this.outputStore.AppendLine("--------------------------------");
            }

            this.writer.Write(this.outputStore.GetOutput());
        }

        private void InitializeDatabase()
        {
            Console.WriteLine("Initializing database. Please wait...");
            IDatabaseInitializer databaseInitializer = this.serviceProvider.GetService<IDatabaseInitializer>();
            databaseInitializer.InitializeDatabase();
            Console.WriteLine("Database initialized successfully!");
        }
    }
}