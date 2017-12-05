namespace PhotoShare.Services.Interfaces
{
    using PhotoShare.Models;

    public interface ITownService
    {
        Town ByNameAndCountry(string name, string country);

        Town AddTown(Town town);

        bool TownExists(string name);
    }
}