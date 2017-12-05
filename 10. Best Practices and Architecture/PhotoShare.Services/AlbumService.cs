namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Services.Interfaces;

    public class AlbumService : IAlbumService
    {
        private readonly PhotoShareContext context;

        public AlbumService(PhotoShareContext context)
        {
            this.context = context;
        }

        public Album AddAlbum(Album newAlbum)
        {
            this.context.Albums.Add(newAlbum);

            this.context.SaveChanges();

            return newAlbum;
        }

        public void AddTagToAlbum(string album, string tag)
        {
            Album alb = this.context.Albums.Single(a => a.Name == album);
            Tag t = this.context.Tags.Single(tn => tn.Name == tag);

            AlbumTag at = new AlbumTag()
            {
                AlbumId = alb.Id,
                TagId = t.Id
            };

            this.context.AlbumTags.Add(at);

            this.context.SaveChanges();
        }

        public bool AlbumExists(string album)
        {
            return this.context.Albums.Any(n => n.Name == album);
        }

        public void AddRole(string userName, string album)
        {
            User user = this.context.Users.Single(u => u.Username == userName);
            Album alb = this.context.Albums.Single(a => a.Name == album);

            AlbumRole ar = new AlbumRole()
            {
                AlbumId = alb.Id,
                UserId = user.Id,
                Role = Role.Owner
            };

            this.context.AlbumRoles.Add(ar);

            this.context.SaveChanges();
        }

        public bool AlbumExists(int albumId)
        {
            return this.context.Albums.Any(n => n.Id == albumId);
        }

        public void AddRole(string userName, int albumId, string permission)
        {
            User user = this.context.Users.Single(u => u.Username == userName);
            Album alb = this.context.Albums.Single(a => a.Id == albumId);
            Role role = (Role)Enum.Parse(typeof(Role), permission);

            AlbumRole ar = new AlbumRole()
            {
                AlbumId = alb.Id,
                UserId = user.Id,
                Role = role
            };

            this.context.AlbumRoles.Add(ar);

            this.context.SaveChanges();
        }

        public Album ById(int id)
        {
            return this.context.Albums.SingleOrDefault(i => i.Id == id);
        }

        public Album ByName(string name)
        {
            return this.context.Albums.SingleOrDefault(i => i.Name == name);
        }

        public void AddPicture(Picture picture)
        {
            this.context.Pictures.Add(picture);

            this.context.SaveChanges();
        }

        public bool UserIsOwner(int albumId, string username)
        {
            int userId = this.context.Users.Single(i => i.Username == username).Id;

            AlbumRole role = this.context.AlbumRoles.Single(id => id.AlbumId == albumId && id.UserId == userId);

            if (role.Role == Role.Owner)
            {
                return true;
            }

            return false;
        }

        public bool UserIsOwner(string album, string username)
        {
            int albumId = this.context.Albums.Single(n => n.Name == album).Id;
            int userId = this.context.Users.Single(i => i.Username == username).Id;

            AlbumRole role = this.context.AlbumRoles.Single(id => id.AlbumId == albumId && id.UserId == userId);

            if (role.Role == Role.Owner)
            {
                return true;
            }

            return false;
        }
    }
}