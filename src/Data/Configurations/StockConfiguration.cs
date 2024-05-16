using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Stocks.Domain;

namespace Data.Configurations;

internal class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.Property(x => x.Code)
            .IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired();
    }
}
