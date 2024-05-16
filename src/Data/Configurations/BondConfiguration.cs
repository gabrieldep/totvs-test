using Bonds.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

internal class BondConfiguration : IEntityTypeConfiguration<Bond>
{
    public void Configure(EntityTypeBuilder<Bond> builder)
    {
        builder.Property(x => x.Expiration)
            .IsRequired();

        builder.Property(x => x.AnnualInterestRate)
            .IsRequired();
    }
}
