namespace EFP.App.Core
{
    using System;
    using EFP.Services.Contracts;
    using Microsoft.Extensions.DependencyInjection;

    public class Engine
    {
        private readonly IServiceProvider provider;
        private string importOption = "json";
        private string exportOption = "json";

        public Engine(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public void Run()
        {
            Console.WriteLine("Set up your server name if necessary! (EFP.Data.Configuration)");
            this.ChooseImportExportOption();
            this.InitializeDb();
            this.SeedData();
            this.ExportData();

            Console.WriteLine(@"See ya! :-)");
        }

        private void ChooseImportExportOption()
        {
            // Select import from option
            while (true)
            {
                Console.Write("Choose import option between json/xml (case insensitive). To exit type 'exit'. Default is 'json' > ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    Environment.Exit(-1);
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                if (input.ToLower() == "json" || input.ToLower() == "xml")
                {
                    this.importOption = input.ToLower();
                    break;
                }

                Console.WriteLine("Invalid input! Please try again.");
            }

            // Select export to option
            while (true)
            {
                Console.Write("Choose export option between json/xml (case insensitive). To exit type 'exit'. Default is 'json' > ");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    Environment.Exit(-1);
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                if (input.ToLower() == "json" || input.ToLower() == "xml")
                {
                    this.exportOption = input.ToLower();
                    break;
                }

                Console.WriteLine("Invalid input! Please try again.");
            }
        }

        private void ExportData()
        {
            IDataExportService exportService = this.provider.GetService<IDataExportService>();

            Console.WriteLine("Exportind data! Please wait..");
            switch (this.exportOption)
            {
                case "json":
                    exportService.ExportToJson();
                    break;

                case "xml":
                    exportService.ExportToXml();
                    break;

                default:
                    break;
            }

            Console.WriteLine("Exporting data finished. You can find your files in the solution folder.");
        }

        private void SeedData()
        {
            IDatabaseSeedService seedService = this.provider.GetService<IDatabaseSeedService>();
            IDatabaseReportService reportService = this.provider.GetService<IDatabaseReportService>();

            Console.WriteLine($"Seeding and randomizing data from '{this.importOption}' files");
            switch (this.importOption)
            {
                case "json":
                    seedService.SeedFromJson();
                    break;

                case "xml":
                    seedService.SeedFromXml();
                    break;

                default:
                    break;
            }

            Console.WriteLine(reportService.GetTableRecordsStatus());
        }

        private void InitializeDb()
        {
            Console.WriteLine("Initializing database! Please wait...");
            IDatabaseInitializerService databasebInitializerService = this.provider.GetService<IDatabaseInitializerService>();
            databasebInitializerService.InitializeDb();
            Console.WriteLine("Database initialized successfully!");
        }
    }
}