namespace PhotoShare.Services
{
    using System.Linq;
    using PhotoShare.Data;
    using PhotoShare.Models;
    using PhotoShare.Services.Interfaces;

    public class TagService : ITagService
    {
        private readonly PhotoShareContext context;

        public TagService(PhotoShareContext context)
        {
            this.context = context;
        }

        public void AddTag(Tag tag)
        {
            this.context.Tags.Add(tag);

            this.context.SaveChanges();
        }

        public Tag ByName(string name)
        {
            return this.context.Tags.SingleOrDefault(t => t.Name == name);
        }

        public bool TagExists(string name)
        {
            Tag tag = this.context.Tags.SingleOrDefault(n => n.Name == name);

            if (tag == null)
            {
                return false;
            }

            return true;
        }
    }
}