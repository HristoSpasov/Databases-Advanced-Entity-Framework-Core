namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Models;
    using Services.Interfaces;

    public class AddTownCommand : ICommand
    {
        private readonly ITownService townService;

        public AddTownCommand(ITownService townService)
        {
            this.townService = townService;
        }

        // AddTown <townName> <countryName>
        public string Execute(params string[] data)
        {
            string result = default(string);

            try
            {
                // Check if user is logged
                if (Session.CurrentUser == null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                this.ValidateArgs(data);

                string townName = data.First();
                string country = data.Last();

                Town town = this.townService.ByNameAndCountry(townName, country);

                if (town != null)
                {
                    throw new ArgumentException($"Town {townName} was already added!");
                }

                Town newTown = new Town()
                {
                    Name = townName,
                    Country = country
                };

                this.townService.AddTown(newTown);

                result = $"Town {townName} was added successfully!";
            }
            catch (ArgumentException ae)
            {
                result = ae.Message;
            }
            catch (InvalidOperationException ioe)
            {
                result = ioe.Message;
            }

            return result;
        }

        private void ValidateArgs(params string[] args)
        {
            if (args.Length != 2)
            {
                string cmdName = new string(this.GetType().Name.Take(this.GetType().Name.Length - "Command".Length).ToArray());

                throw new InvalidOperationException($"Command <{cmdName}> not valid!");
            }
        }
    }
}