namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Models;
    using PhotoShare.Services.Interfaces;

    public class ShareAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;

        public ShareAlbumCommand(IAlbumService albumService, IUserService userService)
        {
            this.albumService = albumService;
            this.userService = userService;
        }

        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(params string[] cmdArgs)
        {
            string result = default(string);

            try
            {
                // Check if user is logged
                if (Session.CurrentUser == null)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                this.ValidateArgs(cmdArgs);

                int albumId = int.Parse(cmdArgs[0]);
                string userName = cmdArgs[1];
                string permission = cmdArgs[2];

                // Only album owner can share album
                if (!this.albumService.UserIsOwner(albumId, Session.CurrentUser))
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                if (!this.albumService.AlbumExists(albumId))
                {
                    throw new ArgumentException($"Album {albumId} not found!");
                }

                if (!this.userService.UserExists(userName))
                {
                    throw new ArgumentException($"User {userName} not found!");
                }

                if (!Enum.TryParse(permission, out Role parsedRole))
                {
                    throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
                }

                this.albumService.AddRole(userName, albumId, permission);

                result = $"User {userName} added to album {this.albumService.ById(albumId).Name} ({permission})";
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