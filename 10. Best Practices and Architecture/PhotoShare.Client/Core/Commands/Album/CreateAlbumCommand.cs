namespace PhotoShare.Client.Core.Commands.Album
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Contracts.Core;
    using PhotoShare.Client.Utilities;
    using PhotoShare.Models;
    using PhotoShare.Services.Interfaces;

    public class CreateAlbumCommand : ICommand
    {
        private readonly IAlbumService albumService;
        private readonly IUserService userService;
        private readonly ITagService tagService;

        public CreateAlbumCommand(IAlbumService albumService, IUserService userService, ITagService tagService)
        {
            this.albumService = albumService;
            this.userService = userService;
            this.tagService = tagService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
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

                string username = cmdArgs[0];
                string title = cmdArgs[1];
                string color = cmdArgs[2];
                string[] tags = cmdArgs.Skip(3).ToArray();

                // User can create album only if he is owner
                if (username != Session.CurrentUser)
                {
                    throw new InvalidOperationException("Invalid credentials!");
                }

                if (!this.userService.UserExists(username))
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (this.albumService.AlbumExists(title))
                {
                    throw new ArgumentException($"Album {title} exists!");
                }

                if (!this.ColorExists(color))
                {
                    throw new ArgumentException($"Color {color} not found!");
                }

                if (!this.TagsAreValid(tags))
                {
                    throw new ArgumentException($"Invalid tags!");
                }

                Album newAlbum = new Album()
                {
                    Name = title,
                    IsPublic = true,
                    BackgroundColor = (Color)Enum.Parse(typeof(Color), color)
                };

                this.albumService.AddAlbum(newAlbum);

                this.albumService.AddRole(username, title);

                this.AddTagsToAbum(tags, title);

                result = $"Album {title} successfully created!";
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

        private void AddTagsToAbum(string[] tags, string title)
        {
            foreach (string t in tags)
            {
                this.albumService.AddTagToAlbum(title, "#" + t);
            }
        }

        private bool TagsAreValid(string[] tags)
        {
            foreach (string tag in tags)
            {
                if (!this.tagService.TagExists("#" + tag))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ColorExists(string color)
        {
            if (Enum.TryParse(color, out Color parsedColor))
            {
                return true;
            }

            return false;
        }

        private void ValidateArgs(params string[] args)
        {
            if (args.Length < 3)
            {
                string cmdName = new string(this.GetType().Name.Take(this.GetType().Name.Length - "Command".Length).ToArray());

                throw new InvalidOperationException($"Command <{cmdName}> not valid!");
            }
        }
    }
}