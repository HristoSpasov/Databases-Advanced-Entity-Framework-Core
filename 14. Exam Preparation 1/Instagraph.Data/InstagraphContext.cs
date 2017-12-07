using Instagraph.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagraph.Data
{
    public class InstagraphContext : DbContext
    {
        public InstagraphContext() { }

        public InstagraphContext(DbContextOptions options)
            :base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<UserFollower> UsersFollowers { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Picture
            modelBuilder.Entity<Picture>()
                .HasMany(u => u.Users)
                .WithOne(p => p.ProfilePicture)
                .HasForeignKey(fk => fk.ProfilePictureId);

            modelBuilder.Entity<Picture>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.Picture)
                .HasForeignKey(fk => fk.PictureId);

            // User
            modelBuilder.Entity<User>()
                .HasAlternateKey(k => k.Username);

            modelBuilder.Entity<User>()
                .HasOne(pp => pp.ProfilePicture)
                .WithMany(u => u.Users)
                .HasForeignKey(fk => fk.ProfilePictureId);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Posts)
                .WithOne(u => u.User)
                .HasForeignKey(fk => fk.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(c => c.Comments)
                .WithOne(u => u.User)
                .HasForeignKey(fk => fk.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(f => f.Followers)
                .WithOne(u => u.User);

            modelBuilder.Entity<User>()
                .HasMany(f => f.UsersFollowing)
                .WithOne(u => u.Follower)
                .OnDelete(DeleteBehavior.Restrict);

            // Post
            modelBuilder.Entity<Post>()
                .HasMany(c => c.Comments)
                .WithOne(p => p.Post)
                .HasForeignKey(fk => fk.PostId);

            // Comment

            // UserFollower
            modelBuilder.Entity<UserFollower>()
                .HasKey(pk => new { pk.FollowerId, pk.UserId });

        }
    }
}
