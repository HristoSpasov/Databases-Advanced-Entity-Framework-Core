namespace EFP.App
{
    using System;
    using AutoMapper;
    using EFP.Data;
    using EFP.Services;
    using EFP.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ServiceConfiguration
    {
        public static IServiceProvider ConfigureServices()
        {
            ServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ProductsShopContext>(op => op.UseSqlServer(Configuration.ConnectionString));

            serviceCollection.AddTransient<IDatabaseInitializerService, DatabaseInitializerService>();
            serviceCollection.AddTransient<IDatabaseSeedService, DatabaseSeedService>();
            serviceCollection.AddTransient<IDataExportService, DataExportService>();
            serviceCollection.AddTransient<IDatabaseReportService, DatabaseReportService>();

            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            IServiceProvider provider = serviceCollection.BuildServiceProvider();

            return provider;
        }
    }
}