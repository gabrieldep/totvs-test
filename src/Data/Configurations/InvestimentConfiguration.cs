using Bonds.Domain;

using Investiments.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Stocks.Domain;

namespace Data.Configurations;
internal class InvestimentConfiguration : IEntityTypeConfiguration<Investiment>
{
    public void Configure(EntityTypeBuilder<Investiment> builder)
    {
        builder.ToTable(nameof(Investiment));

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Type);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.DeletedAt)
            .IsRequired(false);

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired();

        builder.Property(x => x.WalletId)
            .IsRequired();

        builder.Property(x => x.Type)
            .HasConversion<EnumToStringConverter<InvestimentType>>()
            .IsRequired();

        builder.HasDiscriminator(x => x.Type)
            .HasValue<Bond>(InvestimentType.Bond)
            .HasValue<Stock>(InvestimentType.Stock);

        builder.HasOne(x => x.Wallet)
            .WithMany()
            .HasForeignKey(x => x.WalletId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
