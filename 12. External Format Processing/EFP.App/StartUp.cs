namespace EFP.App
{
    using System;
    using EFP.App.Core;

    public static class StartUp
    {
        public static void Main()
        {
            IServiceProvider serviceProvider = ServiceConfiguration.ConfigureServices();

            Engine engine = new Engine(serviceProvider);
            engine.Run();
        }
    }
}