namespace PhotoShare.Client.Core.Commands.Album
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Services.Interfaces;

    public class AddTagToCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly ITagService tagService;

        public AddTagToCommand(IAlbumService albumService, ITagService tagService)
        {
            this.albumService = albumService;
            this.tagService = tagService;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(params string[] args)
        {
            string result = default(string);

            try
            {
                // Check if user is logged
                if (Session.CurrentUser == null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                this.ValidateArgs(args);

                string album = args.First();
                string tag = args.Last();

                // Only album owner can add tag
                if (!this.albumService.UserIsOwner(album, Session.CurrentUser))
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                if (!this.tagService.TagExists("#" + tag) || !this.albumService.AlbumExists(album))
                {
                    throw new ArgumentException("Either tag or album do not exist!");
                }

                this.albumService.AddTagToAlbum(album, "#" + tag);

                result = $"Tag {"#" + tag} added to {album}!";
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