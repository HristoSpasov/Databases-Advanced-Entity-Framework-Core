namespace TeamBuilder.App
{
    using System;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Core;
    using TeamBuilder.App.IO;

    public class StartUp
    {
        public static void Main()
        {
            IReader consoleReader = new ConsoleReader();
            IWriter consoleWriter = new ConsoleWriter();
            IOutputStore outputStore = new OutputStore();

            IServiceProvider serviceProvider = ServiceConfigarion.GetServiceProvider();

            IEngine engine = new Engine(consoleReader, consoleWriter, outputStore, serviceProvider);
            engine.Run();
        }
    }
}