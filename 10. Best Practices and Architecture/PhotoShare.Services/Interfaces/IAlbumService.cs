namespace PhotoShare.Services.Interfaces
{
    using PhotoShare.Models;

    public interface IAlbumService
    {
        Album ById(int id);

        Album ByName(string name);

        Album AddAlbum(Album newAlbum);

        void AddTagToAlbum(string album, string tag);

        bool AlbumExists(string album);

        bool AlbumExists(int albumId);

        void AddRole(string userName, string album);

        void AddRole(string userName, int album, string permission);

        void AddPicture(Picture picture);

        bool UserIsOwner(int albumId, string username);

        bool UserIsOwner(string album, string username);
    }
}