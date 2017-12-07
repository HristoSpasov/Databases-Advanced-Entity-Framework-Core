namespace TeamBuilder.App
{
    using System;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.App.Core;
    using TeamBuilder.Data;
    using TeamBuildere.Services;
    using TeamBuildere.Services.Contracts;

    internal static class ServiceConfigarion
    {
        public static IServiceProvider GetServiceProvider()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<TeamBuilderContext>(cfg => cfg.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IEventService, EventService>();
            serviceCollection.AddTransient<ITeamService, TeamService>();
            serviceCollection.AddTransient<IInvitationService, InvitationService>();

            serviceCollection.AddTransient<ICommandFactory, CommandFactory>();

            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<Mapping>());

            IServiceProvider provider = serviceCollection.BuildServiceProvider();

            return provider;
        }
    }
}