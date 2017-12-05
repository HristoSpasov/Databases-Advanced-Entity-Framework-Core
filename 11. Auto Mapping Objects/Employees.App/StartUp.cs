namespace Employees.App
{
    using System;
    using Employees.App.Contracts.Core;
    using Employees.App.Contracts.IO;
    using Employees.App.Core;
    using Employees.App.IO;

    internal class StartUp
    {
        public static void Main()
        {
            IServiceProvider serviceProvider = ServiceConfiguration.ConfigureServices();

            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            IOutputStore outputStore = new OutputStore();

            IEngine engine = new Engine(reader, writer, outputStore, serviceProvider);
            engine.Run();
        }
    }
}