namespace Payments.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Entities;

    public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> builder)
        {
            builder.ToTable("PaymentMethods");

            builder.HasKey(pk => pk.Id);

            builder.HasIndex(i => new { i.UserId, i.BankAccountId, i.CreditCardId }).IsUnique();

            builder.HasOne(u => u.User)
                .WithMany(pm => pm.PaymentMethods)
                .HasForeignKey(fk => fk.UserId);

            builder.HasOne(cc => cc.CreditCard)
                .WithOne(pm => pm.PaymentMethod);

            builder.HasOne(ba => ba.BankAccount)
                .WithOne(pm => pm.PaymentMethod);
        }
    }
}