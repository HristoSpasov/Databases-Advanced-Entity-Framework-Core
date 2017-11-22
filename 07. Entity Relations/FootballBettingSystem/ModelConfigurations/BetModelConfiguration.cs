namespace P03_FootballBetting.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class BetModelConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> entity)
        {
            entity.ToTable("Bets");

            entity.HasKey(pk => pk.BetId);

            entity.HasOne(u => u.User)
                  .WithMany(b => b.Bets)
                  .HasForeignKey(u => u.UserId);

            entity.HasOne(g => g.Game)
                  .WithMany(b => b.Bets)
                  .HasForeignKey(fk => fk.GameId);
        }
    }
}
