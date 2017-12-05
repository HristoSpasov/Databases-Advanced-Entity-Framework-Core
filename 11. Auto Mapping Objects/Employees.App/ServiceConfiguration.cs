namespace Employees.App
{
    using System;
    using AutoMapper;
    using Employees.App.Contracts.Core;
    using Employees.App.Core;
    using Employees.Data;
    using Employees.Services;
    using Employees.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceConfiguration
    {
        internal static IServiceProvider ConfigureServices()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeesContext>(op => op.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<ICommandFactory, CommandFactory>();
            serviceCollection.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            serviceCollection.AddTransient<IEmployeeService, EmployeeService>();

            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            IServiceProvider provider = serviceCollection.BuildServiceProvider();

            return provider;
        }
    }
}