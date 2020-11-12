using Domain.Accounts;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configuration
{
    public sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.Property(b => b.AccountId)
                .HasConversion(
                    value => value.Id,
                    value => new AccountId(value))
                .IsRequired();

            builder.Property(b => b.Currency)
                .HasConversion(
                    value => value.Code,
                    value => new Currency(value))
                .IsRequired();

            builder.Property(b => b.CustomerId)
                .HasConversion(
                    value => value.Id,
                    value => new CustomerId(value))
                .IsRequired();

            builder.Property(b => b.CustomerId)
                .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction);

            builder.HasMany(x => x.CreditsCollection)
                .WithOne(b => b.Account!)
                .HasForeignKey(b => b.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.DebitsCollection)
                .WithOne(b => b.Account!)
                .HasForeignKey(b => b.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
