using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

using Newtonsoft.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

using Instagraph.Data;
using Instagraph.Models;
using Instagraph.DataProcessor.Dto.Import;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private const string Error = "Error: Invalid data.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            PictureDto[] deserializedPictures = JsonConvert.DeserializeObject<PictureDto[]>(jsonString);
            List<Picture> validPictures = new List<Picture>();

            foreach (var p in deserializedPictures)
            {
                if (validPictures.Any(vp => vp.Path == p.Path) || string.IsNullOrWhiteSpace(p.Path))
                {
                    result.AppendLine(Error);
                    continue;
                }

                if (p.Size <= 0)
                {
                    result.AppendLine(Error);
                    continue;
                }

                Picture toAdd = Mapper.Map<Picture>(p);

                validPictures.Add(toAdd);
                result.AppendLine($"Successfully imported Picture {p.Path}.");
            }

            context.Pictures.AddRange(validPictures);
            context.SaveChanges();
            return result.ToString().Trim();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            UserDto[] deserializedUsers = JsonConvert.DeserializeObject<UserDto[]>(jsonString, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });

            List<User> validUsers = new List<User>();

            foreach (var u in deserializedUsers)
            {
                if (u.Password == null || u.ProfilePicture == null || u.Username == null || !IsValid(u))
                {
                    result.AppendLine(Error);
                    continue;
                }

                if (validUsers.Any(un => un.Username == u.Username))
                {
                    result.AppendLine(Error);
                    continue;
                }

                Picture pic = context.Pictures.SingleOrDefault(p => p.Path == u.ProfilePicture);

                if (pic == null)
                {
                    result.AppendLine(Error);
                    continue;
                }

                User mappedUser = new User
                {
                    Username = u.Username,
                    Password = u.Password,
                    ProfilePictureId = pic.Id
                };

                validUsers.Add(mappedUser);
                result.AppendLine($"Successfully imported User {u.Username}.");
            }

            context.Users.AddRange(validUsers);
            context.SaveChanges();
            return result.ToString();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            StringBuilder result = new StringBuilder();

            UserFollowerDto[] deserializerdUsersFollowers = JsonConvert.DeserializeObject<UserFollowerDto[]>(jsonString);
            List<UserFollower> validUsersFollowers = new List<UserFollower>();

            foreach (var uf in deserializerdUsersFollowers)
            {
                User user = context.Users.SingleOrDefault(u => u.Username == uf.User);
                User follower = context.Users.SingleOrDefault(u => u.Username == uf.Follower);

                if (user == null || follower == null)
                {
                    result.AppendLine(Error);
                    continue;
                }

                if (validUsersFollowers.Any(u => u.FollowerId == follower.Id && u.UserId == user.Id))
                {
                    result.AppendLine(Error);
                    continue;
                }

                UserFollower newFollower = new UserFollower
                {
                    FollowerId = follower.Id,
                    UserId = user.Id
                };

                validUsersFollowers.Add(newFollower);
                result.AppendLine($"Successfully imported Follower {follower.Username} to User {user.Username}.");
            }

            context.UsersFollowers.AddRange(validUsersFollowers);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            XDocument postsDoc = XDocument.Parse(xmlString);

            PostDto[] posts = postsDoc.Root.Elements()
                                      .Select(p => new PostDto
                                      {
                                          Caption = p.Element("caption").Value,
                                          User = p.Element("user").Value,
                                          Picture = p.Element("picture").Value
                                      })
                                      .ToArray();

            List<Post> validPosts = new List<Post>();

            foreach (var p in posts)
            {
                User user = context.Users.SingleOrDefault(u => u.Username == p.User);
                Picture picture = context.Pictures.SingleOrDefault(pi => pi.Path == p.Picture);

                if (user == null || picture == null)
                {
                    result.AppendLine(Error);
                    continue;
                }

                Post toAdd = new Post
                {
                    Caption = p.Caption,
                    UserId = user.Id,
                    PictureId = picture.Id
                };

                validPosts.Add(toAdd);
                result.AppendLine($"Successfully imported Post {p.Caption}.");
            }

            context.Posts.AddRange(validPosts);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            StringBuilder result = new StringBuilder();

            XDocument commentsXml = XDocument.Parse(xmlString);

            CommentDto[] comments = commentsXml.Root.Elements()
                                    .Select(c => new CommentDto
                                    {
                                        Content = c.Element("content")?.Value,
                                        User = c.Element("user")?.Value,
                                        PostId = c.Element("post") != null && c.Element("post").Attribute("id") != null ?
                                                 int.Parse(c.Element("post").Attribute("id").Value) : default(int?)
                                    })
                                    .ToArray();

            List<Comment> validComments = new List<Comment>();

            foreach (var c in comments)
            {
                User user = context.Users.SingleOrDefault(u => u.Username == c.User);
                Post post = context.Posts.SingleOrDefault(p => p.Id == c.PostId);

                if (user == null || post == null)
                {
                    result.AppendLine(Error);
                    continue;
                }

                Comment toAdd = new Comment
                {
                    Content = c.Content,
                    UserId = user.Id,
                    PostId = post.Id
                };

                validComments.Add(toAdd);
                result.AppendLine($"Successfully imported Comment {c.Content}.");
            }

            context.Comments.AddRange(validComments);
            context.SaveChanges();

            return result.ToString().Trim();
        }

        private static bool IsValid(object obj)
        {
            return Validator.TryValidateObject(obj, new System.ComponentModel.DataAnnotations.ValidationContext(obj), new List<ValidationResult>(), true);
        }
    }
}
