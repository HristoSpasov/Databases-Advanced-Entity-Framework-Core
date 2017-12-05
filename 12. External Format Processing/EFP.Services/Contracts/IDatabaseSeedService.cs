namespace EFP.Services.Contracts
{
    public interface IDatabaseSeedService
    {
        void SeedFromJson();

        void SeedFromXml();
    }
}