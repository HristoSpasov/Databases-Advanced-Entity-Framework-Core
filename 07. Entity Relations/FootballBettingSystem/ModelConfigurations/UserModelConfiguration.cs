namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class UserModelConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");

            entity.HasKey(pk => pk.UserId);

            entity
                .HasMany(b => b.Bets)
                .WithOne(u => u.User)
                .HasForeignKey(fk => fk.UserId);
        }
    }
}
