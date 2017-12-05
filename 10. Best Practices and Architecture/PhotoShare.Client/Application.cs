namespace PhotoShare.Client
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Core;
    using PhotoShare.Data;
    using PhotoShare.Services;
    using PhotoShare.Services.Interfaces;

    public class Application
    {
        public static void Main()
        {
            IServiceProvider serviceProvider = ConfigureServices();

            IEngine engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            ServiceCollection collection = new ServiceCollection();

            collection.AddDbContext<PhotoShareContext>(op => op.UseSqlServer(ServerConfig.ConnectionString));

            collection.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

            collection.AddTransient<ICommandFactory, CommandFactory>();

            collection.AddTransient<IUserService, UserService>();

            collection.AddTransient<ITownService, TownService>();

            collection.AddTransient<IAlbumService, AlbumService>();

            collection.AddTransient<ITagService, TagService>();

            var provider = collection.BuildServiceProvider();

            return provider;
        }
    }
}