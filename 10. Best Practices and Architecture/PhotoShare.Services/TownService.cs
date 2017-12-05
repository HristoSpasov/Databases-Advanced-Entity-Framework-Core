namespace PhotoShare.Services
{
    using System.Linq;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Services.Interfaces;

    public class TownService : ITownService
    {
        private readonly PhotoShareContext context;

        public TownService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Town AddTown(Town town)
        {
            this.context.Towns.Add(town);

            this.context.SaveChanges();

            return town;
        }

        public Town ByNameAndCountry(string name, string country)
        {
            return this.context
                        .Towns
                        .Where(t => t.Name == name && t.Country == country)
                        .SingleOrDefault();
        }

        public bool TownExists(string name)
        {
            return this.context.Towns.Where(t => t.Name == name).Count() == 1;
        }
    }
}