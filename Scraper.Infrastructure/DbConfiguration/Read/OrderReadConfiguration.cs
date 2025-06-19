using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.DbConfiguration.Read;
public class OrderReadConfiguration : IEntityTypeConfiguration<OrderToScrape>
{
    public void Configure(EntityTypeBuilder<OrderToScrape> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);

        builder
            .HasMany(o => o.Notices)
            .WithOne();
    }
}
