namespace PhotoShare.Services.Interfaces
{
    using PhotoShare.Models;

    public interface ITagService
    {
        Tag ByName(string name);

        bool TagExists(string name);

        void AddTag(Tag tag);
    }
}