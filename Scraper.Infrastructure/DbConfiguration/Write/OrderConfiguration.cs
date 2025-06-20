using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scraper.Domain.Entities;

namespace Scraper.Infrastructure.DbConfiguration.Write;

/// <summary>
/// Конфигурация записи ордера
/// </summary>
public class OrderConfiguration : IEntityTypeConfiguration<OrderToScrape>
{
    public void Configure(EntityTypeBuilder<OrderToScrape> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.CreatedAt).IsRequired();

        builder.Property(o => o.Urls).IsRequired();

        builder.HasMany(o => o.Notices).WithOne().IsRequired();
    }
}
