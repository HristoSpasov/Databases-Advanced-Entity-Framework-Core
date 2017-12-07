using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Instagraph.Data;
using Instagraph.DataProcessor.Dto.Export;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            UncommentedPosts[] uncomentedPosts = context.Posts
                 .Include(c => c.Comments)
                 .Include(u => u.User)
                 .Include(p => p.Picture)
                 .Where(c => !c.Comments.Any())
                 .Select(u => new UncommentedPosts
                 {
                     Id = u.Id,
                     Picture = u.Picture.Path,
                     User = u.User.Username
                 })
                 .OrderBy(i => i.Id)
                 .ToArray();

            string uncomentedPostsJson = JsonConvert.SerializeObject(uncomentedPosts);

            return uncomentedPostsJson;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            PopularUsers[] popularUsers = context.Users
                .Where(p => p.Posts
                    .Any(c => c.Comments
                        .Any(f => p.Followers.Any(fl => fl.FollowerId == f.UserId))))
                .OrderBy(i => i.Id)
                .Select(u => new PopularUsers
                {
                    Username = u.Username,
                    Followers = u.Followers.Count
                })
                .ToArray();

            string popularUsersJson = JsonConvert.SerializeObject(popularUsers);

            return popularUsersJson;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var users = context.Users
                .Select(u => new CommentsOnPostsDto
                {
                    Username = u.Username,
                    MostComments = u.Posts.Select(p => p.Comments.Count).Any() ? u.Posts.Select(p => p.Comments.Count).OrderByDescending(c => c).First() : 0
                })
                .OrderByDescending(u => u.MostComments)
                .ThenBy(u => u.Username)
                .ToArray();

            var xDoc = new XDocument(new XElement("users"));

            foreach (var u in users)
            {
                xDoc.Root.Add(new XElement("user",
                    new XElement("Username", u.Username),
                    new XElement("MostComments", u.MostComments)
                    ));
            }

            string result = xDoc.ToString();
            return result;
        }
    }
}
