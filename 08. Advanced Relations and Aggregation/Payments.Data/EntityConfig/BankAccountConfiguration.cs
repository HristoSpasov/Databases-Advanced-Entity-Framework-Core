namespace Payments.Data.EntityConfig
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Payments.Entities;

    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.ToTable("BankAccounts");

            builder.HasKey(pk => pk.BankAccountId);

            builder.HasOne(pm => pm.PaymentMethod)
                .WithOne(ba => ba.BankAccount)
                .HasForeignKey<PaymentMethod>(ba => ba.BankAccountId);

            builder.Ignore(pm => pm.PaymentMethod);
            builder.Ignore(pm => pm.PaymentMethodId);

            builder.Property(p => p.BankName)
                .IsRequired(true)
                .HasColumnType("nvarchar(50)");

            builder.Property(p => p.SwiftCode)
                .IsRequired(true)
                .HasColumnType("varchar(20)");
        }
    }
}