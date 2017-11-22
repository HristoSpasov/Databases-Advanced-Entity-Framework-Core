namespace Payments.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Entities;

    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> builder)
        {
            builder.ToTable("CreditCards");

            builder.HasKey(pk => pk.CreditCardId);

            builder.HasOne(pm => pm.PaymentMethod)
                .WithOne(cc => cc.CreditCard)
                .HasForeignKey<PaymentMethod>(cc => cc.CreditCardId);

            builder.Ignore(p => p.LimitLeft);

            builder.Ignore(p => p.PaymentMethodId);

            builder.Ignore(p => p.PaymentMethod);
        }
    }
}