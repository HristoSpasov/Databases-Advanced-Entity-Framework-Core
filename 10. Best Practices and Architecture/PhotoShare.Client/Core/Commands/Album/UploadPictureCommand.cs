namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Models;
    using PhotoShare.Services.Interfaces;

    public class UploadPictureCommand : ICommand
    {
        private readonly IAlbumService albumService;

        public UploadPictureCommand(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
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

                string albumName = data[0];
                string picTitle = data[1];
                string picturePath = data[2];

                // Only album owner can upload picture
                if (!this.albumService.UserIsOwner(albumName, Session.CurrentUser))
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                if (!this.albumService.AlbumExists(albumName))
                {
                    throw new ArgumentException($"Album {albumName} not found!");
                }

                int albumId = this.albumService.ByName(albumName).Id;

                Picture newPicture = new Picture()
                {
                    AlbumId = albumId,
                    Title = picTitle,
                    Path = picturePath
                };

                this.albumService.AddPicture(newPicture);

                result = $"Picture {picTitle} added to {albumName}!";
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
            if (args.Length != 3)
            {
                string cmdName = new string(this.GetType().Name.Take(this.GetType().Name.Length - "Command".Length).ToArray());

                throw new InvalidOperationException($"Command <{cmdName}> not valid!");
            }
        }
    }
}